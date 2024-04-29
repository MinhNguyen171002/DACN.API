using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class QuestionComplete
    {
        [Key]
        public int QuestionID { get; set; }
        public string? QuestionChoose { get; set; }
        public bool? IsCorrect { get; set; }
        public int? Sentence { get; set; }
        public virtual Question? test { get; set; }
        public virtual SentenceComplete? SenCom { get; set; }

    }
}
