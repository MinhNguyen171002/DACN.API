using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Exam 
    {
        [Key]
        public int ExamID {  get; set; }        
        public string? ExamDescription { get; set;}
        public string? Skill { get; set; }
        public TimeSpan ExamDuration {  get; set;}
        public virtual ICollection<Practice>? practices { get; set; }
    }
}
