﻿using Microsoft.AspNetCore.Identity;
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
        public int ExamID { get; set; }
        [ForeignKey(nameof(UserID))]
        public User user { get; set; }
        [ForeignKey(nameof(ExamID))]
        public Exam exam { get; set; }
    }
}
