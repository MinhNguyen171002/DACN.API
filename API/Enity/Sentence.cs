using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Sentence
    {
        [Key]
        public int SentenceId { get; set; }
        public string? FilePath { get; set; }
        public int PracticeID { get; set; }
        [ForeignKey(nameof(PracticeID))]
        public virtual Practice? Practice { get; set; }
        public virtual ICollection<Question>? questions { get; set; }
    }
}
