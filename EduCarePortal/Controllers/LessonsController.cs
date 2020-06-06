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
using EduCarePortal.MethodHelper;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.LanguageTranslator.v3;

namespace EduCarePortal.Controllers
{
    public class LessonsController : Controller
    {
        private EduCareDBEntities db = new EduCareDBEntities();

        // GET: Lessons
        public async Task<ActionResult> Index()
        {
            var lessons = db.Lessons.Include(l => l.Subject).Include(d=>d.Files);
            return View(await lessons.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<ActionResult> Details(Guid? id, string languageFilter)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = await db.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            MethodHelpers methodHelpers = new MethodHelpers();
            TutorialViewModel viewModel = new TutorialViewModel();
            viewModel.LessionID = lesson.LessionID;
            viewModel.LessonName = lesson.LessonName;
            viewModel.IssueDate = lesson.IssueDate;
            viewModel.Subject = lesson.Subject;
            List<File> files = await db.Files.Where(d => d.LessionID == lesson.LessionID).ToListAsync();
            ViewBag.VideoContent = false;
            ViewBag.IsTranscript = false;
            foreach (var item in files)
            {
                if (item.FileType == "Video")
                {
                    if (languageFilter != null && item.ContentDetails != null)
                    {
                        item.ContentDetails = await methodHelpers.TranslateContent(item.ContentDetails, languageFilter);
                    }
                    ViewBag.VideoContent = true;
                    if (!string.IsNullOrEmpty(item.ContentDetails))
                    {
                        ViewBag.IsTranscript = true;
                    }
                }
            }
            
            viewModel.Files = files;
            Dictionary<string, string> languageList = methodHelpers.GetSupportedLangs();
            SelectList langs = new SelectList(languageList.Select(x => new { Value = x.Key, Text = x.Value }), "Value", "Text");
            ViewBag.languageFilter = langs;
            ViewBag.LessionID = id;
            return View(viewModel);
        }

        //[HttpGet]
        //public ActionResult TranslateContent(string toLang, string content)
        //{
        //    Dictionary<string, string> response = new Dictionary<string, string>();
        //    MethodHelpers methodHelpers = new MethodHelpers();
        //    string translatedtext = methodHelpers.TranslateContent(content, toLang);
        //    if (translatedtext != null)
        //    {
        //        response.Add("Success", "True");
        //        response.Add("TranslatedText", translatedtext);
        //        response.Add("ToLang", toLang);
        //    }
        //    else
        //    {
        //        response.Add("Success", "False");
        //        response.Add("TranslatedText", content);
        //        response.Add("ToLang", toLang);
        //    }
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult RedirectToDoc(string url)
        {
            return Redirect(url);
        }
        // GET: Lessons/Create
        public ActionResult Create()
        {
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LessionID,LessonName,IssueDate,SubjectID")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                lesson.LessionID = Guid.NewGuid();
                db.Lessons.Add(lesson);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", lesson.SubjectID);
            return View(lesson);
        }

        // GET: Lessons/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = await db.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", lesson.SubjectID);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LessionID,LessonName,IssueDate,SubjectID")] Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lesson).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", lesson.SubjectID);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = await db.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Lesson lesson = await db.Lessons.FindAsync(id);
            db.Lessons.Remove(lesson);
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
