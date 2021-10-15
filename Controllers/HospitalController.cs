using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HospitalController : Controller
    {
        Repository repo = new Repository();
        // GET: Hospital
        [HttpGet]
        public ActionResult Index()
        {
            List<DAL.Hospital> e = repo.GetAllHospitals();
            List<Models.Hospital> v = new List<Models.Hospital>();

            foreach(var ev in e)
            {
                Models.Hospital temp = new Models.Hospital();
                temp.HospitalId = ev.HospitalId;
                temp.HospitalName = ev.HospitalName;
                temp.Address = ev.Address;
                temp.City = ev.City;
                temp.State = ev.State;
                temp.Pincode = ev.Pincode;
                temp.InsurerName = ev.InsurerName;
                v.Add(temp);
            }
            return View(v);
        }
       [HttpGet]
        public ActionResult Create()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login","User");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                DAL.Hospital e = new DAL.Hospital()
                {
                    HospitalName = hospital.HospitalName,
                    Address = hospital.Address,
                    City = hospital.City,
                    State = hospital.State,
                    Pincode = hospital.Pincode,
                    InsurerName = hospital.InsurerName,
                    InsurerId=hospital.InsurerId
                };
                bool result = repo.AddHospital(e);
                if (!result)
                {
                    return View("Error");
                }
            }
                return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var businessEntity = repo.GetHospital(id);
            Models.Hospital hospital = new Models.Hospital()
            {
                HospitalId=businessEntity.HospitalId,
                InsurerId = businessEntity.InsurerId,
                HospitalName = businessEntity.HospitalName,
                Address = businessEntity.Address,
                City = businessEntity.City,
                State = businessEntity.State,
                Pincode = businessEntity.Pincode
            };
            return View(hospital);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(Models.Hospital hospital)
        {
            DAL.Hospital v = new DAL.Hospital()
            {
                HospitalId = hospital.HospitalId,
                HospitalName = hospital.HospitalName,
                Address = hospital.Address,
                City = hospital.City,
                State = hospital.State,
                Pincode = hospital.Pincode,
                InsurerId = hospital.InsurerId
            };
            bool result = repo.UpdateHospital(v);
            if (!result)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var businessEntity = repo.GetHospital(id);
            Models.Hospital v = new Models.Hospital()
            {
                HospitalName = businessEntity.HospitalName,
                Address = businessEntity.Address,
                City = businessEntity.City,
                State = businessEntity.State,
                Pincode = businessEntity.Pincode
            };
            return View(v);

        }
        [HttpGet]
        public ActionResult SearchHospitals()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult SearchHospitals(FormCollection Nameform)
        {
            string Name = Nameform["Name"];
            var result = repo.SearchHospitals(Name);
            List<Models.Hospital> hosdata = new List<Models.Hospital>();
            foreach(var hp in result)
            {
                Models.Hospital temp = new Models.Hospital();
                temp.HospitalName = hp.HospitalName;
                temp.Address = hp.Address;
                temp.City = hp.City;
                temp.State = hp.State;
                temp.Pincode = hp.Pincode;

                hosdata.Add(temp);
            }
            return View("SearchHospitalsResult", hosdata);
        }

        [HttpGet]
        public ActionResult SearchHospitalsByPincode()
        {
            return View();
        }
        [HttpPost]


        public ActionResult SearchHospitalsByPincode(FormCollection pincodeform)
        {
            string pincode = pincodeform["pin"];
            // int pin = Int32.Parse(pincode);
            var result = repo.SearchHospitalByPincode(pincode);
            List<Models.Hos_pin_Result> hp = new List<Models.Hos_pin_Result>();
            foreach (Hos_pin_Result r in result)
            {
                Models.Hos_pin_Result temp = new Models.Hos_pin_Result();
                temp.HospitalName = r.HospitalName;
                temp.Address = r.Address;
                temp.City = r.City;
                temp.State = r.State;
                temp.InsurerName = r.InsurerName;
                hp.Add(temp);
            }
            return View("SearchHospitalsByPincodeR", hp);
        }

        }
}