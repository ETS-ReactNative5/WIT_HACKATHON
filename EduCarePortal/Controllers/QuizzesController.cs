using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EduCarePortal.Models;

namespace EduCarePortal.Controllers
{
    public class QuizzesController : Controller
    {
        private EduCareDBEntities db = new EduCareDBEntities();

        // GET: Quizzes
        public async Task<ActionResult> Index()
        {
            List<Quizze> quizzesToView = new List<Quizze>();
            if(Session["Role"].ToString() == "Student")
            {
                string studentEMail = Session["UserEmail"].ToString();
                Student student = db.Students.Where(s => s.Email == studentEMail).FirstOrDefault();
                List<Quizze> quizzes = await db.Quizzes.Include(q => q.Subject).ToListAsync();

                foreach (var item in quizzes)
                {
                    var quizResult = db.QuizResults.Where(r => r.StudentID == student.StudentID && r.QuizID == item.QuizID).FirstOrDefault();
                    if (quizResult == null)
                    {
                        quizzesToView.Add(item);
                    }
                }
            }
            else if (Session["Role"].ToString() == "Teacher")
            {
                string teacherEMail = Session["UserEmail"].ToString();
                Teacher teacher = db.Teachers.Where(s => s.Email == teacherEMail).FirstOrDefault();
                List<Guid> subjectIDs = db.Subjects.Where(d => d.TeacherID == teacher.TeacherID).Select(d => d.SubjectID).ToList();
                quizzesToView = db.Quizzes.Where(d=> subjectIDs.Contains(d.SubjectID)).ToList();
            }
            else if (Session["Role"].ToString() == "Admin")
            {
                quizzesToView = db.Quizzes.ToList();
            }
            return View(quizzesToView);
        }

        public async Task<ActionResult> TakeQuiz(string quizID)
        {
            Guid _quizID = Guid.Parse(quizID);
            Quizze quizze = await db.Quizzes.FindAsync(_quizID);
            List<Question> questionsFromDB = await db.Questions.Where(d => d.QuizID == _quizID).ToListAsync();

            TakeQuizModel takeQuizModel = new TakeQuizModel();
            takeQuizModel.QuizID = quizID;
            takeQuizModel.StudentEmail = Session["UserEmail"].ToString();

            List<QuestionVM> questionList = new List<QuestionVM>();
            if (questionsFromDB != null)
            {
                foreach(var item in questionsFromDB)
                {
                    Dictionary<string, string> choiceDict = new Dictionary<string, string>();
                    List<string> choiceOptions = item.Choices.Split(',').ToList();
                    foreach(var opt in choiceOptions)
                    {
                        choiceDict.Add(opt, opt);
                    }
                    questionList.Add(new QuestionVM()
                    {
                        QuestionID = item.QuiestionID.ToString(),
                        QuestionText = item.Question1,
                        Choices = new SelectList(choiceDict.Select(x => new { Value = x.Key, Text = x.Value }), "Value", "Text"),
                        CorrectAnwser = item.Answer,
                        Marks = item.Marks
                    });
                }
                takeQuizModel.Questions = questionList.ToArray();
            }
            takeQuizModel.QuizName = quizze.QuizName;
            takeQuizModel.Subject = quizze.Subject.SubjectName;
            return View(takeQuizModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeQuiz(TakeQuizModel resultQuiz)
        {
            QuizResult quizResult = new QuizResult();
            if (resultQuiz != null)
            {
                quizResult.AssessmentDate = DateTime.Now;
                quizResult.QuizID = Guid.Parse(resultQuiz.QuizID);
                quizResult.StudentID = db.Students.Where(s => s.Email == resultQuiz.StudentEmail).FirstOrDefault().StudentID;
                quizResult.Quizze = db.Quizzes.Find(quizResult.QuizID);
                quizResult.ResultID = Guid.NewGuid();
                quizResult.Student = db.Students.Find(quizResult.StudentID);
                quizResult.ScoreObtained = 0;
                quizResult.TotalMarks = 0;
                foreach (var item in resultQuiz.Questions)
                {
                    quizResult.TotalMarks = quizResult.TotalMarks + item.Marks;
                    if (item.Anwser == item.CorrectAnwser)
                    {
                        quizResult.ScoreObtained = quizResult.ScoreObtained + item.Marks;
                    }
                }
                quizResult.Percentage = (quizResult.ScoreObtained / quizResult.TotalMarks) * 100;
                db.QuizResults.Add(quizResult);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Quizzes/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quizze quizze = await db.Quizzes.FindAsync(id);
            if (quizze == null)
            {
                return HttpNotFound();
            }
            return View(quizze);
        }

        // GET: Quizzes/Create
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName");
            return View();
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuizID,QuizName,QuizDate,SubjectID")] Quizze quizze)
        {
            if (ModelState.IsValid)
            {
                quizze.QuizID = Guid.NewGuid();
                db.Quizzes.Add(quizze);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", quizze.SubjectID);
            return View(quizze);
        }

        // GET: Quizzes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quizze quizze = await db.Quizzes.FindAsync(id);
            if (quizze == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", quizze.SubjectID);
            return View(quizze);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuizID,QuizName,QuizDate,SubjectID")] Quizze quizze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizze).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", quizze.SubjectID);
            return View(quizze);
        }

        // GET: Quizzes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quizze quizze = await db.Quizzes.FindAsync(id);
            if (quizze == null)
            {
                return HttpNotFound();
            }
            return View(quizze);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Quizze quizze = await db.Quizzes.FindAsync(id);
            db.Quizzes.Remove(quizze);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
