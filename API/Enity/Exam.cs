using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Exam 
    {
        [Key]
        public int ExamID {  get; set; }
        public string ExamName { get; set;}
        public string ExamDescription { get; set;}
        public string Level { get; set;}
        public TimeSpan ExamDuration {  get; set;}
    }
}
