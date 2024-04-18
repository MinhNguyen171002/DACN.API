namespace API.Model
{
    public class ExamDTO
    {
        public string Username { get; set; }
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public string ExamDescription { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public string Level { get; set; }

    }
    public class QuestionDTO
    {
        public string Username { get; set; }
        public int QuestionID { get; set; }
        public string QuestionContext { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
        public string Question4 { get; set; }
        public string CorrectAnswer { get; set; }
        public int ExamID { get; set; }
    }
    public class ResultDTO
    {
        public string Username { get; set; }
        public int ResultID { get; set; }
        public int Score { get; set; }
        public string UserID { get; set; }
        public int ExamID { get; set; }
    }
}
