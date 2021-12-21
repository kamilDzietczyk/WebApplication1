using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Question
    {
        public int questionId { get; set; }
        public String quizId { get; set; }
        public String questionText { get; set; }
        public String answerOne { get; set; }
        public String answerTwo { get; set; }
        public String answerThree { get; set; }
        public String correctAnswer { get; set; }
        public String answerPoint { get; set; }
    }
}
