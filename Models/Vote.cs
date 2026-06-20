namespace AKAnkietyProject.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public int OptionId { get; set; }
        public Option? Option { get; set; }

        public string VoterId { get; set; } = string.Empty;

        public int SurveyId { get; set; }
    }
}
