namespace API.Model.GetDTO
{
    public class ExamGDTO
    {
        public int ExamID { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public string ExamDescription { get; set; }
        public string Skill {  get; set; }
        public int PracticeCount { get; set; }

    }
}
