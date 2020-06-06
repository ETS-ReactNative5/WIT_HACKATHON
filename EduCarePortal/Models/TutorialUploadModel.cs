using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace EduCarePortal.Models
{
    public class TutorialUploadModel
    {
        public System.Guid FileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }

        public HttpPostedFileBase FileContent { get; set; }
        public System.Guid LessionID { get; set; }
    }
}