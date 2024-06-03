namespace API.Model.DTO
{
    public class ExamDTO
    { 
        public string? UserID { get; set; }
        public string? ExamID { get; set; }
        public int? ExamSerial { get; set; }
        public int? Part { get; set; }
        public string? ExamDescription { get; set; }
        public string? Skill { get; set; }
        public TimeSpan ExamDuration { get; set; }
    }
}
