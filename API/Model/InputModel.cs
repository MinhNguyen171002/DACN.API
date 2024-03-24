namespace API.Model
{
    public class MLevel
    {
        public string Username { get; set; }
        public string LevelID { get; set; }
        public string LevelName { get; set; }
    }
    public class MExam
    {
        public string Username { get; set; }
        public string ExamID { get; set; }
        public string ExamName { get; set; }
        public string ExamDescription { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public string LevelID { get; set; }

    }
    public class MQuestion
    {
        public string Username { get; set; }
        public string QuestionID { get; set; }
        public string QuestionContext { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
        public string Question4 { get; set; }
        public string CorrectAnswer { get; set; }
        public string ExamID { get; set; }
    }
    public class MResult
    {
        public string Username { get; set; }
        public string ResultID { get; set; }
        public int Score { get; set; }
        public string UserID { get; set; }
        public string ExamID { get; set; }
    }
}
