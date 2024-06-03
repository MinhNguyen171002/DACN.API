namespace API.Model.GetDTO
{
    public class ExamGDTO
    {
        public string? ExamID { get; set; }
        public int? ExamSerial { get; set; }
        public int? Part { get; set; }
        public string? ExamDescription { get; set; }
        public string? Skill { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public int? PracticeCount { get; set; }

    }
}
