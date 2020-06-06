using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduCarePortal.Models
{
    public class TutorialViewModel
    {
        public Guid LessionID { get; set; }
        public string LessonName { get; set; }
        public DateTime IssueDate { get; set; }
        public Subject Subject { get; set; }
        public List<File> Files { get; set; }
    }
}