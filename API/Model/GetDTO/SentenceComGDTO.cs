namespace API.Model.GetDTO
{
    public class SentenceComGDTO
    {
        public string SentenceID { get; set; }
        public TimeSpan Totaltime { get; set; }
        public bool? Status { get; set; }
        public string? CorrectDescription { get; set; }
        public int? CorrectQuestion { get; set; }
        public int? CorrectPercent { get; set; }
    }
}
