﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Enity
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string? QuestionContext { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? CorrectAnswer { get; set; }
        public string? CorrectDescription { get; set; }
        public int? Sentences { get; set; }
        public virtual QuestionComplete quescom { get; set; }
        public virtual Sentence? sentences { get; set; }

    }
}
