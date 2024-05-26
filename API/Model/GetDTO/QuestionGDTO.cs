namespace API.Model.GetDTO
{
    public class QuestionGDTO
    {
        public string? QuestionID { get; set; }
        public int? QuestionSerial { get; set; }
        public string? QuestionContext { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}
