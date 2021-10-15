using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class InsurerController : Controller
    {
        Repository repo = new Repository();
        // GET: Insurer
        [HttpGet]
        public ActionResult Index()
        {
            List<DAL.Insurer> e = repo.GetAllInsurers();
            List<Models.Insurer> v = new List<Models.Insurer>();

            foreach(var ev in e)
            {
                Models.Insurer temp = new Models.Insurer();
                temp.InsurerName = ev.InsurerName;
                temp.RegistrationNo = ev.RegistrationNo;
                temp.HeadOffice = ev.HeadOffice;

                v.Add(temp);
            }
            return View(v);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Insurer insurer)
        {
            if (ModelState.IsValid)
            {
                DAL.Insurer v = new DAL.Insurer()
                {
                    InsurerName = insurer.InsurerName,
                    RegistrationNo = insurer.RegistrationNo,
                    HeadOffice = insurer.HeadOffice
                };
                bool result = repo.AddInsurer(v);
                if (!result)
                {
                    return View("Error");
                }
            }
                return RedirectToAction("Index");
        }
    }
}