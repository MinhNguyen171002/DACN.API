using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{ 
    public class Result
    {
        [Key]
        public int ResultID { get; set; }
        public int Score { get; set; }
        public string UserID { get; set; }
        public int PracticeID { get; set; }
        [ForeignKey(nameof(UserID))]
        public virtual User? user {get; set;}
        [ForeignKey(nameof(PracticeID))]
        public virtual Practice? practice {get; set;}
    }
}
