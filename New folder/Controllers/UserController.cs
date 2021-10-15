using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        Repository repo = new Repository();
        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection loginForm)
        {
            string emailId = loginForm["txtEmailId"];
            string password = loginForm["txtPassword"];

            User user = repo.ValidateUser(emailId, password);

            if(user==null)
            {
                ViewBag.ErrorMsg = "Invalid Credentials. Please try again.";
                return View("Login");
            }
            else
            {
                Session["emailId"] = emailId;
                Session["user"] = user;
                return RedirectToAction("Index", "Hospital");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult Register(Models.User user)
        {
            DAL.User userinfo = new DAL.User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailId = user.EmailId,
                Password = user.Password
            };
            bool result = repo.AddUser(userinfo);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Login", "User");
        }

    }

}