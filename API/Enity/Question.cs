using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string? QuestionContext { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? CorrectAnswer { get; set; }
        public int SentenceID {get; set;}
        [ForeignKey(nameof(SentenceID))]
        public virtual Sentence? sentences { get; set; }

    }
}
