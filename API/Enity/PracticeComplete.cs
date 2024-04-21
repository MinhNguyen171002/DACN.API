using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Enity
{
    public class PracticeComplete
    {
        [Key]
        public int PCId { get; set; }
        public string UserID { get; set; }
        public int PracticeID { get; set; }
        public TimeSpan Totaltime { get; set; }
        public bool? Status {  get; set; }
        public virtual ICollection<TestComplete>? TestCompletes { get; set; }
        [ForeignKey(nameof(UserID))]
        public virtual User? User { get; set; }
        [ForeignKey(nameof(PracticeID))]
        public virtual Practice? Practice { get;set; }
    }
}
