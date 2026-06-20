using AKAnkietyProject.Data;
using AKAnkietyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AKAnkietyProject.Controllers
{
    [Authorize]
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SurveysController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var surveys = await _context.Surveys
                .Include(s => s.Options)
                .ToListAsync();
            return View(surveys);
        }

        [HttpGet]
        [Authorize(Roles = "Ankieter")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Ankieter")]
        public async Task<IActionResult> Create(string title, List<string> options)
        {
            var cleanOptions = options?
                .Where(o => !string.IsNullOrWhiteSpace(o))
                .ToList() ?? new List<string>();

            if (string.IsNullOrWhiteSpace(title) || cleanOptions.Count < 2)
            {
                ModelState.AddModelError("", "Podaj tytuł i co najmniej dwie opcje.");
                return View();
            }

            var survey = new Survey
            {
                Title = title,
                OwnerId = _userManager.GetUserId(User)!,
                Options = cleanOptions.Select(o => new Option { Text = o }).ToList()
            };

            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Vote(int id)
        {
            var survey = await _context.Surveys
                .Include(s => s.Options)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (survey == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User)!;
            bool alreadyVoted = await _context.Votes
                .AnyAsync(v => v.SurveyId == id && v.VoterId == userId);

            if (alreadyVoted)
            {
                return RedirectToAction(nameof(Results), new { id });
            }

            return View(survey);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(int id, int optionId)
        {
            var userId = _userManager.GetUserId(User)!;

            var survey = await _context.Surveys
                .Include(s => s.Options)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            bool alreadyVoted = await _context.Votes
                .AnyAsync(v => v.SurveyId == id && v.VoterId == userId);
            if (alreadyVoted)
            {
                return RedirectToAction(nameof(Results), new { id });
            }

            bool optionOk = survey.Options.Any(o => o.Id == optionId);
            if (!optionOk)
            {
                ModelState.AddModelError("", "Wybierz poprawną opcję.");
                return View(survey);
            }

            var vote = new Vote
            {
                OptionId = optionId,
                SurveyId = id,
                VoterId = userId
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Results), new { id });
        }

        public async Task<IActionResult> Results(int id)
        {
            var survey = await _context.Surveys
                .Include(s => s.Options)
                .ThenInclude(o => o.Votes)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        [Authorize(Roles = "Ankieter")]
        public async Task<IActionResult> Details(int id)
        {
            var survey = await _context.Surveys
                .Include(s => s.Options)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (survey == null)
            {
                return NotFound();
            }

            var votes = await _context.Votes
                .Where(v => v.SurveyId == id)
                .Join(_context.Options,
                    v => v.OptionId,
                    o => o.Id,
                    (v, o) => new { v.VoterId, OptionText = o.Text })
                .Join(_context.Users,
                    x => x.VoterId,
                    u => u.Id,
                    (x, u) => new VoteDetail { VoterEmail = u.Email!, OptionText = x.OptionText })
                .ToListAsync();

            ViewBag.SurveyTitle = survey.Title;
            return View(votes);
        }
    }

    // Model pomocniczy do widoku szczegolow glosow.
    public class VoteDetail
    {
        public string VoterEmail { get; set; } = string.Empty;
        public string OptionText { get; set; } = string.Empty;
    }
}
