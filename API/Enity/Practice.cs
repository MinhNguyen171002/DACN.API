using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Practice
    {
        [Key]
        public int PracticeID { get; set; }
        public string? PracticeDescription { get;set; }
        public int ExamID { get; set; }
        [ForeignKey(nameof(ExamID))]
        public virtual Exam? exam { get; set; }
        public virtual ICollection<Test>? tests { get; set; }
    }
}
