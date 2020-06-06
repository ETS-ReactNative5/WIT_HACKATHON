using EduCarePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduCarePortal.Controllers
{
    public class HomeController : Controller
    {
        private EduCareDBEntities dbContext = new EduCareDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Landing()
        {
            if(Session["Role"].ToString() == "Admin")
            {
                return RedirectToAction("AdminLanding");
            }
            else if (Session["Role"].ToString() == "Student")
            {
                return RedirectToAction("StudentLanding");
            }
            else if (Session["Role"].ToString() == "Parents")
            {
                return RedirectToAction("ParentsLanding");
            }
            else if (Session["Role"].ToString() == "Teacher")
            {
                return RedirectToAction("TeacherLanding");
            }
            else
            {
                return RedirectToAction("LogOff","Account");
            }
        }

        [Authorize]
        public ActionResult StudentLanding()
        {
            TeacherStudentViewModel viewModel = new TeacherStudentViewModel();
            viewModel.Subjects = dbContext.Subjects.ToList();
            Student student = Session["StudentObject"] as Student;
            viewModel.Tutorials = new List<Tutorials>();
            viewModel.Quizes = new List<Quizes>();
            viewModel.Reports = new List<Reports>();
            foreach (var sub in viewModel.Subjects)
            {
                List<Lesson> tutorials = new List<Lesson>();
                tutorials = dbContext.Lessons.Where(d => d.SubjectID == sub.SubjectID).ToList();
                viewModel.Tutorials.Add(new Tutorials()
                {
                    Subject = sub.SubjectName,
                    TutotialCount = tutorials.Count()
                });
                List<Quizze> quizzes = new List<Quizze>();
                quizzes = dbContext.Quizzes.Where(d => d.SubjectID == sub.SubjectID).ToList();
                viewModel.Quizes.Add(new Quizes()
                {
                    Subject = sub.SubjectName,
                    QuizCount = quizzes.Count()
                });
                int reportCount = 0;
                foreach(var quiz in quizzes)
                {
                    List<QuizResult> results = new List<QuizResult>();
                    results = dbContext.QuizResults.Where(d => d.QuizID == quiz.QuizID && d.StudentID == student.StudentID).ToList();
                    reportCount = reportCount + results.Count();
                }
                viewModel.Reports.Add(new Reports()
                {
                    Subject = sub.SubjectName,
                    ReportCount = reportCount
                });
            }
            
            return View(viewModel);
        }

        [Authorize]
        public ActionResult ParentsLanding()
        {
            Parent parent = Session["ParentObject"] as Parent;
            ParentsViewModel viewModel = new ParentsViewModel();
            viewModel.Students = dbContext.Students.Where(s => s.ParentID == parent.ParentID).ToList();
            List<Guid> studentIDs = viewModel.Students.Select(d => d.StudentID).ToList();
            List<QuizResult> quizResults = dbContext.QuizResults.Where(d => studentIDs.Contains(d.StudentID)).ToList();
            viewModel.Reports = new List<Reports>();
            List<Guid> subjectIDs = new List<Guid>();
            var groupedReports = quizResults.GroupBy(d => d.Quizze.SubjectID);
            foreach(var item in groupedReports)
            {
                subjectIDs.Add(item.Key);
            }
            foreach (var item in subjectIDs)
            {
                viewModel.Reports.Add(new Reports()
                {
                    Subject = dbContext.Subjects.Where(d => d.SubjectID == item).FirstOrDefault().SubjectName,
                    ReportCount = quizResults.Where(d => d.Quizze.SubjectID == item).ToList().Count()
                });
            }
            return View(viewModel);
        }

        [Authorize]
        public ActionResult TeacherLanding()
        {
            TeacherStudentViewModel viewModel = new TeacherStudentViewModel();
            Teacher teacher = Session["TeacherObject"] as Teacher;
            viewModel.Subjects = dbContext.Subjects.Where(d => d.TeacherID == teacher.TeacherID).ToList();
            viewModel.Tutorials = new List<Tutorials>();
            viewModel.Quizes = new List<Quizes>();
            viewModel.Reports = new List<Reports>();
            foreach (var sub in viewModel.Subjects)
            {
                List<Lesson> tutorials = new List<Lesson>();
                tutorials = dbContext.Lessons.Where(d => d.SubjectID == sub.SubjectID).ToList();
                viewModel.Tutorials.Add(new Tutorials()
                {
                    Subject = sub.SubjectName,
                    TutotialCount = tutorials.Count()
                });
                List<Quizze> quizzes = new List<Quizze>();
                quizzes = dbContext.Quizzes.Where(d => d.SubjectID == sub.SubjectID).ToList();
                viewModel.Quizes.Add(new Quizes()
                {
                    Subject = sub.SubjectName,
                    QuizCount = quizzes.Count()
                });
                int reportCount = 0;
                foreach (var quiz in quizzes)
                {
                    List<QuizResult> results = new List<QuizResult>();
                    results = dbContext.QuizResults.Where(d => d.QuizID == quiz.QuizID).ToList();
                    reportCount = reportCount + results.Count();
                }
                viewModel.Reports.Add(new Reports()
                {
                    Subject = sub.SubjectName,
                    ReportCount = reportCount
                });
            }
            return View(viewModel);
        }

        [Authorize]
        public ActionResult AdminLanding()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}