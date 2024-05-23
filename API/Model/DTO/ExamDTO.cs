namespace API.Model.DTO
{
    public class ExamDTO
    { 
        public string UserID { get; set; }
        public int ExamID { get; set; }
        public string? ExamDescription { get; set; }
        public string? Skill { get; set; }
        public TimeSpan ExamDuration { get; set; }
    }
}
