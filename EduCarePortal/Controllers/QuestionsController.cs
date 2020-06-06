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
using LinqToExcel;
using System.IO;

namespace EduCarePortal.Controllers
{
    public class QuestionsController : Controller
    {
        private EduCareDBEntities db = new EduCareDBEntities();

        // GET: Questions
        public async Task<ActionResult> Index(string quizID)
        {
            Guid _quizID = Guid.Parse(quizID);
            ViewBag.ReturnQuizID = quizID;
            var questions = db.Questions.Include(q => q.Quizze).Where(d=>d.QuizID == _quizID);
            return View(await questions.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> UploadQuestion(string quizID)
        {
            ViewBag.QuizID = quizID;
            return PartialView("UploadQuestion");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UploadQuestionDetails(string quizID)
        {
            bool upload = false;
            foreach (string file in Request.Files)
            {
                var fileContent = Request.Files[file];
                if (fileContent.ContentType == "application/vnd.ms-excel" || fileContent.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    if (fileContent.FileName.Contains(".xlsx") || fileContent.FileName.Contains(".xls"))
                    {
                        if (fileContent != null && fileContent.ContentLength > 0 && fileContent.ContentLength < (100 * 1024 * 1024))
                        {
                            string filename = fileContent.FileName;
                            string targetpath = Server.MapPath("~/QuestionFormatInExcel/");
                            if (!Directory.Exists(targetpath))
                            {
                                Directory.CreateDirectory(targetpath);
                            }
                            fileContent.SaveAs(targetpath + filename);
                            string pathToExcelFile = targetpath + filename;

                            string sheetName = "Sheet1";

                            var excelFile = new ExcelQueryFactory(pathToExcelFile);
                            var questionDetails = from a in excelFile.Worksheet<CustomQuestionModel>(sheetName) select a;
                            List<Question> questions = new List<Question>();
                            foreach (var a in questionDetails)
                            {
                                questions.Add(new Question()
                                {
                                    QuiestionID = Guid.NewGuid(),
                                    Question1 = a.Question,
                                    DifficultyLevel = a.DifficultyLevel,
                                    Answer = a.Answer,
                                    Choices = a.Choices,
                                    QuizID = Guid.Parse(quizID),
                                    Quizze = await db.Quizzes.FindAsync(Guid.Parse(quizID))
                                });
                            }
                            foreach(var item in questions)
                            {
                                if(item.Answer!=null && item.Question1!= null && item.Choices !=null && item.DifficultyLevel > 0)
                                {
                                    db.Questions.Add(new Question()
                                    {
                                        QuiestionID = item.QuiestionID,
                                        Question1 = item.Question1,
                                        DifficultyLevel = item.DifficultyLevel,
                                        Answer = item.Answer,
                                        Choices = item.Choices,
                                        QuizID = item.QuizID,
                                        Quizze = item.Quizze
                                    });
                                }
                            }
                            await db.SaveChangesAsync();
                            upload = true;
                        }
                    }
                }
            }
            if (upload)
            {
                Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
                response.Add("Success", true);
                response.Add("QuizID", quizID);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
                response.Add("Success", false);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Questions/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create(string quizID)
        {
            ViewBag.ReturnQuizID = quizID;
            ViewBag.QuizID = new SelectList(db.Quizzes, "QuizID", "QuizName");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuiestionID,DifficultyLevel,Question1,Answer,QuizID,Choices")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.QuiestionID = Guid.NewGuid();
                db.Questions.Add(question);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.QuizID = new SelectList(db.Quizzes, "QuizID", "QuizName", question.QuizID);
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizID = new SelectList(db.Quizzes, "QuizID", "QuizName", question.QuizID);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuiestionID,DifficultyLevel,Question1,Answer,QuizID,Choices")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.QuizID = new SelectList(db.Quizzes, "QuizID", "QuizName", question.QuizID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Question question = await db.Questions.FindAsync(id);
            db.Questions.Remove(question);
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
