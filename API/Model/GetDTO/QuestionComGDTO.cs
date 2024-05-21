namespace API.Model.GetDTO
{
    public class QuestionComGDTO
    {
        public int? QuestionSerial { get; set; }
        public string? QuestionChoose { get; set; }
        public bool? IsCorrect { get; set; }
        public string[]? CorrectDescription { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}
