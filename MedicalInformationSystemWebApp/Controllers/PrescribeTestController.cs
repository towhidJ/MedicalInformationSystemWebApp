using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalInformationSystemWebApp.Models;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;
using Rotativa;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class PrescribeTestController : Controller
    { 
        PasswordHelper passwordHelper = new PasswordHelper();
        private MedicalInfoSys db = new MedicalInfoSys();

        // GET: PrescribeTest
        [Authorize(Roles = "Doctor")]
        public ActionResult Index()
        {
            var prescribeTestTBs = db.PrescribeTestTBs.Include(p => p.DoctorTB).Include(p => p.PatientTB);
            return View(prescribeTestTBs.ToList());
        }

        // GET: PrescribeTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescribeTestTB prescribeTestTB = db.PrescribeTestTBs.Find(id);
            if (prescribeTestTB == null)
            {
                return HttpNotFound();
            }
            return View(prescribeTestTB);
        }

        // GET: PrescribeTest/Create
        [Authorize(Roles = "Doctor")]
        public ActionResult Create()
        {
            ViewBag.RefferDoctorId = new SelectList(db.DoctorTBs, "DoctorId", "NameED");
            ViewBag.PatientId = new SelectList(db.PatientTBs, "Id", "NameED");
            return View();
        }

        // POST: PrescribeTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientId,RefferDoctorId,TestName,Midkit")] PrescribeTestTB prescribeTestTB)
        {
            if (ModelState.IsValid)
            {
                prescribeTestTB.Midkit = passwordHelper.AesEncryption(prescribeTestTB.Midkit);
                prescribeTestTB.TestName = passwordHelper.AesEncryption(prescribeTestTB.TestName);
                db.PrescribeTestTBs.Add(prescribeTestTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefferDoctorId = new SelectList(db.DoctorTBs, "DoctorId", "NameED", prescribeTestTB.RefferDoctorId);
            ViewBag.PatientId = new SelectList(db.PatientTBs, "Id", "Name", prescribeTestTB.PatientId);
            return View(prescribeTestTB);
        }

        // GET: PrescribeTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescribeTestTB prescribeTestTB = db.PrescribeTestTBs.Find(id);
            if (prescribeTestTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefferDoctorId = new SelectList(db.DoctorTBs, "DoctorId", "Name", prescribeTestTB.RefferDoctorId);
            ViewBag.PatientId = new SelectList(db.PatientTBs, "Id", "Name", prescribeTestTB.PatientId);
            return View(prescribeTestTB);
        }

        // POST: PrescribeTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientId,RefferDoctorId,TestName,Midkit")] PrescribeTestTB prescribeTestTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prescribeTestTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefferDoctorId = new SelectList(db.DoctorTBs, "DoctorId", "Name", prescribeTestTB.RefferDoctorId);
            ViewBag.PatientId = new SelectList(db.PatientTBs, "Id", "Name", prescribeTestTB.PatientId);
            return View(prescribeTestTB);
        }

        // GET: PrescribeTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrescribeTestTB prescribeTestTB = db.PrescribeTestTBs.Find(id);
            if (prescribeTestTB == null)
            {
                return HttpNotFound();
            }
            return View(prescribeTestTB);
        }

        // POST: PrescribeTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrescribeTestTB prescribeTestTB = db.PrescribeTestTBs.Find(id);
            db.PrescribeTestTBs.Remove(prescribeTestTB);
            db.SaveChanges();
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


        public ActionResult TestPrescribtation(int id)
        {
            var pTest = db.PrescribeTestTBs.Where(c => c.Id == id).Select(c => c);
            List<string> TestName = new List<string>();
            List<string> Midkit = new List<string>();
            foreach (var prescribeTest in pTest)
            {
                if (prescribeTest.MidkitED!=null)
                {
                    var mk = prescribeTest.MidkitED.Split(',');
                    for (int j = 0; j < mk.Length; j++)
                    {
                        Midkit.Add(mk[j].ToString() + "\n");
                    }
                }
                var tN = prescribeTest.TestNameED.Split(',');
                
                for (int i = 0; i < tN.Length; i++)
                {
                    TestName.Add(tN[i].ToString() + "\n");
                }
                
                ViewBag.Name = prescribeTest.PatientTB.NameED;
                ViewBag.Date = DateTime.Now.ToString("yyyy-M-d dddd");
                ViewBag.Age = prescribeTest.PatientTB.Age;
                ViewBag.Doctor = prescribeTest.DoctorTB.NameED;
                ViewBag.DoctorTitle = prescribeTest.DoctorTB.DesignationTB.DesignationName;
                ViewBag.DoctorS = prescribeTest.DoctorTB.SpealizationTB.Type;
                ViewBag.Day = prescribeTest.DoctorTB.VisitDay;
                ViewBag.Time = prescribeTest.DoctorTB.VisitingTimeStart + "-" + prescribeTest.DoctorTB.VisitingTimeEnd;
                ViewBag.Sex = prescribeTest.PatientTB.Gender;
            }

            ViewBag.TestName = TestName.ToList();
            ViewBag.Midkit = Midkit.ToList();
            return View(pTest);
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("TestPrescribtation", new { id = id });
        }
    }
}
