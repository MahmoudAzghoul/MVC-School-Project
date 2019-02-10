using HomeWork_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork_2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(HomeWork_2.Models.Student studentModel)
        {
            using (StudentContext db = new StudentContext())
            {
                var studentDetails = db.Students.Where(x => x.UserName == studentModel.UserName
                                       && x.Password == studentModel.Password).FirstOrDefault();
                if (studentDetails == null)
                {
                    studentModel.LoginErrorMessage = "Wrong UserName or Password.";
                    return View("Index", studentModel);
                }
                else
                {
                    Session["StudentID"] = studentDetails.StudentID;
                    Session["UserName"] = studentDetails.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            int StdID = (int)Session["StudentID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}