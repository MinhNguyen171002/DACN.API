using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Enity
{
    public class SentenceComplete
    {
        [Key]
        public int SentenceID { get; set; }
        public TimeSpan Totaltime { get; set; }
        public bool? Status {  get; set; }
        public string? User { get; set; }
        public double? Result { get; set; }
        public ICollection<QuestionComplete>? QuestionCompletes { get; set; }
        public virtual User? user { get; set; }
        public virtual Sentence? Sentence { get;set; }
        public virtual Result? result { get; set; }
    }
}
