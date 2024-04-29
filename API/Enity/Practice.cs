using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Practice
    {
        [Key]
        public int PracticeID { get; set; }
        public string? PracticeDescription { get;set; }
        public int? Exam { get; set; }
        public virtual Exam? exam { get; set; }
        public ICollection<Sentence>? sentences { get; set; }
    }
}
