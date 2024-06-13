namespace API.Model.GetDTO
{
    public class SentencesGDTO
    {
        public string? SentenceId { get; set; }
        public string? ExamId { get; set; }
        public string? ExamDetail { get; set; }
        public int? SentenceSerial { get; set; }
        public string? Description { get; set; }
        public int? QuestionCount { get;set; }
        public string? Skill { get;set; }
    }
}
