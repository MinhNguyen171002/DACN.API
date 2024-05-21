using System.ComponentModel.DataAnnotations;

namespace API.Enity
{
    public class QuestionFile
    {
        [Key]
        public string Id { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public byte[] FileData { get; set; }
        public string Question { get; set; }
        public virtual Question? ques { get; set; }
    }
}
