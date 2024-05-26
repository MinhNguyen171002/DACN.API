namespace API.Model.DTO
{
    public class SentenceComDTO
    {
        public int? CorrectQuestion { get;set; }
        public TimeSpan Totaltime { get; set; }
        public bool? Status { get; set; }
    }
    public class QuestionComDTO
    {
        public string? QuestionID { get; set; }
        public int? QuestionSerial { get; set; }
        public string? QuestionChoose { get; set; }
        public bool? IsCorrect { get; set; }
    }
    public class SubmitDTO
    {
        public string UserID { get; set; }
        public string SentenceID { get; set; }
        public List<QuestionComDTO>? questionComs { get; set; } = new List<QuestionComDTO>();
        public SentenceComDTO sentenceCom { get; set; }
    }
}
