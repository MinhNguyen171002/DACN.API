using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class QuestionComplete
    {
        [Key]
        public int QuestionID { get; set; }
        public bool? IsCorrect { get; set; }
        [ForeignKey(nameof(QuestionID))]
        public virtual Question? test { get; set; }

    }
}
