using System.ComponentModel.DataAnnotations;

namespace AKAnkietyProject.Models
{
    public class Option
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Treść opcji jest wymagana.")]
        [Display(Name = "Opcja")]
        public string Text { get; set; } = string.Empty;

        public int SurveyId { get; set; }
        public Survey? Survey { get; set; }

        public List<Vote> Votes { get; set; } = new List<Vote>();
    }
}
