using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionContext { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
        public string Question4 { get; set; }
        public string CorrectAnswer { get; set; }
        public int ExamID { get; set; }
        [ForeignKey(nameof(ExamID))]
        public Exam exam { get; set; }

    }
}
