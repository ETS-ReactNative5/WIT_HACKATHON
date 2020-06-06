using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduCarePortal.Models
{
    public class QuestionVM
    {
        public string QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string Anwser { get; set; }
        public string CorrectAnwser { get; set; }
        public int Marks { get; set; }
        public IEnumerable<SelectListItem> Choices { get; set; }
    }

    public class ChoiceVM
    {
        public int ChoiceID { get; set; }
        public string ChoiceText { get; set; }
    }

    public class TakeQuizModel
    {
        public string QuizID { get; set; }
        public string QuizName { get; set; }
        public string Subject { get; set; }
        public string StudentEmail { get; set; }
        public QuestionVM[] Questions { get; set; }

    }
    
}