using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Sentence
    {
        [Key]
        public string SentenceId { get; set; }
        public int? SentenceSerial { get; set; }
        public string? Description { get;set; }
        public int? Exam { get; set; }
        public virtual SentenceComplete sencom { get; set; }
        public virtual Exam? exam { get; set; }
        public virtual ICollection<Question>? questions { get; set; }
    }
}
