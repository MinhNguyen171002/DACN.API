namespace API.Model
{
    public class ExamDTO
    {
        public string? Username { get; set; }
        public int ExamID { get; set; }
        public string? ExamDescription { get; set; }
        public TimeSpan ExamDuration { get; set; }
        public string? Skill { get; set; }
        public int PracticeCount {  get; set; }  
    }
    public class QuestionDTO
    {
        public string? Username { get; set; }
        public int QuestionID { get; set; }
        public string? QuestionContext { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? CorrectAnswer { get; set; }
        public int SentenceID { get; set; }
    }
    public class ResultDTO
    {
        public string Username { get; set; }
        public int ResultID { get; set; }
        public int Score { get; set; }
        public int TestPass {  get; set; }
        public int PracticeID { get; set; }
    }
    public class TestCompleteDTO
    {
        public string Username { get; set;}
        public int TCId { get; set; }
        public int TestID { get; set; }
        public bool? IsCorrect { get; set; }
    }
    public class PracticeDTO
    {
        public string Username { get; set; }
        public int PracticeID { get; set; }
        public string? PracticeDescription { get; set; }
        public int ExamID { get; set; }
        public int TestCount {  get; set; }
    }
    public class PracticeCompleteDTO
    {
        public string Username { get; set; }       
        public int PCId { get; set; }
        public int CorrectCount { get; set; }
        public decimal CorrectPercent { get; set; }
        public TimeSpan Totaltime { get; set; }
    }
}
