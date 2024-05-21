namespace API.Model.GetDTO
{
    public class UserSentenceDTO
    {
        public string? SentenceId { get; set; }
        public int? SentenceSerial { get; set; }
        public string? Description { get; set; }
        public int? CorrectPercent { get; set; }
        public int QuestionCount { get; set; }
        public int? CorrectQuestion { get; set; }
    }
}
