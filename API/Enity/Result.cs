using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{ 
    public class Result
    {
        [Key]
        public int ResultID { get; set; }
        public int? Score { get; set; }
        public string? User { get; set; }
        public int? Sentence { get; set; }
        public virtual User? user {get; set;}
        public virtual SentenceComplete? sentenceCom {get; set;}
    }
}
