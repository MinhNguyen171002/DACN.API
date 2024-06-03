using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Exam 
    {
        [Key]
        public string? ExamID {  get; set; }
        public int? ExamSerial { get; set; }
        public int? Part {  get; set; }
        public string? ExamDescription { get; set;}
        public string? Skill { get; set; }
        public TimeSpan ExamDuration {  get; set;}
        public virtual ICollection<Sentence>? sentences { get; set; }
    }
}
