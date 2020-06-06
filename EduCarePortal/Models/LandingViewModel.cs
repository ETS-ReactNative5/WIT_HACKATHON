using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduCarePortal.Models
{
    public class TeacherStudentViewModel
    {
        public List<Subject> Subjects { get; set; }
        public List<Tutorials> Tutorials { get; set; }
        public List<Quizes> Quizes { get; set; }
        public List<Reports> Reports { get; set; }
    }

    public class Tutorials
    {
        public string Subject { get; set; }
        public int TutotialCount { get; set; }
    }
    public class Quizes
    {
        public string Subject { get; set; }
        public int QuizCount { get; set; }
    }
    public class Reports
    {
        public string Subject { get; set; }
        public int ReportCount { get; set; }
    }

    public class ParentsViewModel
    {
        public List<Student> Students { get; set; }
        public List<Reports> Reports { get; set; }
    }
}