using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Test
    {
        [Key]
        public int TestID { get; set; }
        public string? Question { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? CorrectAnswer { get; set; }
        public string? CorrectDescription { get; set; }
        public int PracticeID {get; set;}
        [ForeignKey(nameof(PracticeID))]
        public virtual Practice? practice { get; set; }

    }
}
