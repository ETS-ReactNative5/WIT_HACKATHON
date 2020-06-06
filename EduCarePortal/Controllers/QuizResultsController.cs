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
    public class QuizResultsController : Controller
    {
        private EduCareDBEntities db = new EduCareDBEntities();

        // GET: QuizResults
        public async Task<ActionResult> Index()
        {
            List<QuizResult> quizResults = new List<QuizResult>();
            if(Session["Role"].ToString() == "Teacher")
            {
                Teacher teacher = Session["TeacherObject"] as Teacher;
                List<Guid> subjectIDs = new List<Guid>();
                subjectIDs = db.Subjects.Where(d => d.TeacherID == teacher.TeacherID).Select(d => d.SubjectID).ToList();
                List<Guid> quizIDs = new List<Guid>();
                quizIDs = db.Quizzes.Where(q => subjectIDs.Contains(q.SubjectID)).Select(q => q.QuizID).ToList();
                quizResults = db.QuizResults.Include(q => q.Quizze).Include(q => q.Student).Where(q => quizIDs.Contains(q.QuizID)).ToList();
            }
            else if (Session["Role"].ToString() == "Student")
            {
                Student student = Session["StudentObject"] as Student;
                quizResults = db.QuizResults.Include(q => q.Quizze).Include(q => q.Student).Where(q =>q.StudentID == student.StudentID).ToList();
            }
            else if (Session["Role"].ToString() == "Parents")
            {
                Parent parent = Session["ParentObject"] as Parent;
                List<Guid> studentIDs = db.Students.Where(s => s.ParentID == parent.ParentID).Select(s => s.StudentID).ToList();
                quizResults = db.QuizResults.Include(q => q.Quizze).Include(q => q.Student).Where(q => studentIDs.Contains(q.StudentID)).ToList();
            }
            else
            {
                quizResults = db.QuizResults.Include(q => q.Quizze).Include(q => q.Student).ToList();
            }
            return View(quizResults);
        }

        // GET: QuizResults/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizResult quizResult = await db.QuizResults.FindAsync(id);
            if (quizResult == null)
            {
                return HttpNotFound();
            }
            return View(quizResult);
        }

        // GET: QuizResults/Create
        public ActionResult Create()
        {
            ViewBag.QuizID = new SelectList(db.Quizzes, "QuizID", "QuizName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: QuizResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ResultID,ScoreObtained,QuizID,StudentID,AssessmentDate")] QuizResult quizResult)
        {
            if (ModelState.IsValid)
            {
                quizResult.ResultID = Guid.NewGuid();
                db.QuizResults.Add(quizResult);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuizID = new SelectList(db.Quizzes, "QuizID", "QuizName", quizResult.QuizID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", quizResult.StudentID);
            return View(quizResult);
        }

        // GET: QuizResults/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizResult quizResult = await db.QuizResults.FindAsync(id);
            if (quizResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizID = new SelectList(db.Quizzes, "QuizID", "QuizName", quizResult.QuizID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", quizResult.StudentID);
            return View(quizResult);
        }

        // POST: QuizResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ResultID,ScoreObtained,QuizID,StudentID,AssessmentDate")] QuizResult quizResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizResult).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuizID = new SelectList(db.Quizzes, "QuizID", "QuizName", quizResult.QuizID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", quizResult.StudentID);
            return View(quizResult);
        }

        // GET: QuizResults/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizResult quizResult = await db.QuizResults.FindAsync(id);
            if (quizResult == null)
            {
                return HttpNotFound();
            }
            return View(quizResult);
        }

        // POST: QuizResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            QuizResult quizResult = await db.QuizResults.FindAsync(id);
            db.QuizResults.Remove(quizResult);
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
