namespace API.Model.DTO
{
    public class FileDTO
    {
        public string? UserID { get; set; }
        public string? fileName { get; set; }
        public string? fileType { get; set; }
        public IFormFile? FileData { get; set; }
        public string? Question { get; set; }
    }
}
