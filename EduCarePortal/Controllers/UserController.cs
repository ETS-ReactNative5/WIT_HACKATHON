using EduCarePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduCarePortal.Controllers
{
    public class UserController : Controller
    {
        private EduCareDBEntities db = new EduCareDBEntities();
        // GET: User
        public ActionResult Index()
        {
            List<UserModel> userModels = new List<UserModel>();
            List<AspNetUser> users = db.AspNetUsers.ToList();
            foreach(var item in users)
            {
                UserModel userModel = new UserModel();
                userModel.UserEmail = item.Email;
                userModel.Role = item.AspNetRoles.FirstOrDefault().Name;
                if(userModel.Role == "Teacher")
                {
                    var data = db.Teachers.Where(d => d.Email == userModel.UserEmail).FirstOrDefault();
                    if (data != null)
                    {
                        userModel.UserName = data.FirstName + " " + data.LastName;
                    }
                    
                }
                else if(userModel.Role == "Student")
                {
                    var data = db.Students.Where(d => d.Email == userModel.UserEmail).FirstOrDefault();
                    if (data != null)
                    {
                        userModel.UserName = data.FirstName + " " + data.LastName;
                    }
                }
                else if (userModel.Role == "Parents")
                {
                    var data = db.Parents.Where(d => d.Email == userModel.UserEmail).FirstOrDefault();
                    if (data != null)
                    {
                        userModel.UserName = data.FirstName + " " + data.LastName;
                    }
                }
                else if (userModel.Role == "Admin")
                {
                    var data = db.Admins.Where(d => d.Email == userModel.UserEmail).FirstOrDefault();
                    if (data != null)
                    {
                        userModel.UserName = data.Name;
                    }
                }
                userModels.Add(userModel);
            }
            return View(userModels);
        }
    }
}