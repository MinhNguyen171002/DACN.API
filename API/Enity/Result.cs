using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{ 
    public class Result
    {
        [Key]
        public string ResultID { get; set; }
        public int Score { get; set; }
        [ForeignKey(nameof(ResultID))]
        public string UserID { get; set; }
        [ForeignKey(nameof(ResultID))]
        public string ExamID { get; set; }
    }
}
