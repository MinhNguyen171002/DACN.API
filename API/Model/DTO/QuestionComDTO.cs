namespace API.Model.DTO
{
    public class QuestionComDTO
    {
        public string? QuestionID { get; set; }
        public int? QuestionSerial { get; set; }
        public string? QuestionChoose { get; set; }
        public bool? IsCorrect { get; set; }
        public string? User { get; set; }
        public string? Sentence { get; set; }
    }
}
