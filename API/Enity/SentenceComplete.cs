using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Enity
{
    public class SentenceComplete
    {
        [Key]
        public string SentenceID { get; set; }
        public TimeSpan Totaltime { get; set; }
        public bool? Status {  get; set; }
        public int? CorrectQuestion { get; set; }
        public ICollection<QuestionComplete>? QuestionCompletes { get; set; }
        public virtual User? user { get; set; }
        public virtual Sentence? sentence { get;set; }
    }
}
