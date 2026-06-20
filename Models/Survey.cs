using System.ComponentModel.DataAnnotations;

namespace AKAnkietyProject.Models
{
    public class Survey
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł ankiety jest wymagany.")]
        [Display(Name = "Tytuł ankiety")]
        public string Title { get; set; } = string.Empty;

        public string OwnerId { get; set; } = string.Empty;
        public ApplicationUser? Owner { get; set; }

        public List<Option> Options { get; set; } = new List<Option>();
    }
}
