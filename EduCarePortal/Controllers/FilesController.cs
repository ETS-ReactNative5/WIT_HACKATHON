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
using System.IO;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using EduCarePortal.MethodHelper;

namespace EduCarePortal.Controllers
{
    public class FilesController : Controller
    {
        private EduCareDBEntities db = new EduCareDBEntities();

        // GET: Files
        public async Task<ActionResult> Index()
        {
            var files = db.Files.Include(f => f.Lesson);
            return View(await files.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = await db.Files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create(string lessonID)
        {
            TutorialUploadModel model = new TutorialUploadModel();
            model.LessionID = Guid.Parse(lessonID);
            Dictionary<string, string> contentTypesDict = new Dictionary<string, string>();
            contentTypesDict.Add("Video", "Video");
            contentTypesDict.Add("PDF", "PDF File");
            SelectList contentTypeList = new SelectList(contentTypesDict.Select(x => new { Value = x.Key, Text = x.Value }), "Value", "Text");
            ViewBag.ContentTypeList = contentTypeList;
            return View(model);
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TutorialUploadModel file)
        {
            string FileName = Path.GetFileNameWithoutExtension(file.FileContent.FileName);
            string FileExtension = Path.GetExtension(file.FileContent.FileName);
            FileName = FileName.Trim() + FileExtension;
            byte[] Bytes = new byte[file.FileContent.ContentLength];
            file.FileContent.InputStream.Read(Bytes, 0, Bytes.Length);

            //upload to blob
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=ibmhackathonblobstorage;AccountKey=UXNuGcVonynbneCODLG/DLzXh9qeO2T7Ws3l0Fr4/G7l1wQHXmX71rKyvf/oMJLgHhHSlzI03fHAreitXt9TyA==;EndpointSuffix=core.windows.net");
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("tutorialvideos");
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(FileName); 
            cloudBlockBlob.Properties.ContentType = file.FileContent.ContentType;
            await cloudBlockBlob.UploadFromByteArrayAsync(Bytes,0, Bytes.Length);

            Models.File _file = new Models.File();
            _file.FileName = FileName;
            _file.FileType = file.FileType;
            _file.LessionID = file.LessionID;
            _file.Lesson = await db.Lessons.FindAsync(file.LessionID);
            _file.UploadDate = DateTime.Now;
            _file.ContentURL = cloudBlockBlob.Uri.AbsoluteUri;
            _file.ContentDetails = "";
            _file.FileID = Guid.NewGuid();
            db.Files.Add(_file);
            await db.SaveChangesAsync();
            if(_file.FileType == "Video")
            {
                MethodHelpers methodHelper = new MethodHelpers();
                methodHelper.TriggerTranscribe(_file.FileID.ToString());
            }
            return RedirectToAction("Index", "Lessons");
        }

        // GET: Files/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = await db.Files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.LessionID = new SelectList(db.Lessons, "LessionID", "LessonName", file.LessionID);
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FileID,FileName,FileType,ContentDetails,ContentURL,UploadDate,LessionID")] Models.File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LessionID = new SelectList(db.Lessons, "LessionID", "LessonName", file.LessionID);
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = await db.Files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Models.File file = await db.Files.FindAsync(id);
            db.Files.Remove(file);
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
