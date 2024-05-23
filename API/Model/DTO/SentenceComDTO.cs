namespace API.Model.DTO
{
    public class SentenceComDTO
    {
        public string? UserID { get; set; }
        public string? SentenceID { get; set; }
        public TimeSpan Totaltime { get; set; }
        public bool? Status { get; set; }       
    }
}
