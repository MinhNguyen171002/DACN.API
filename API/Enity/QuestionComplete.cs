using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class QuestionComplete
    {
        [Key]
        public string QuestionID { get; set; }
        public int QuestionSerial { get; set; }
        public string? QuestionChoose { get; set; }
        public bool? IsCorrect { get; set; }
        public string? Sentence { get; set; }
        public string? User { get; set; }
        public virtual Question? test { get; set; }
        public virtual User? user { get; set; }
        public virtual SentenceComplete? SenCom { get; set; }

    }
}
