using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduCarePortal.Models
{
    public class CustomQuestionModel
    {
        public int DifficultyLevel { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Choices { get; set; }
    }
}