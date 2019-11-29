using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;
using Rotativa;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class BillController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();

        // GET: Bill
        public ActionResult Index()
        {
            var billTBs = db.BillTBs.Include(b => b.PatientTB);
            return View(billTBs.ToList());
        }

        // GET: Bill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillTB billTB = db.BillTBs.Find(id);
            if (billTB == null)
            {
                return HttpNotFound();
            }
            return View(billTB);
        }

        // GET: Bill/Create
        public ActionResult Create(int Id)
        {
            var patient = db.PatientTBs.Where(c => c.Id == Id).Select(c => c);
            ViewBag.PatientId = new SelectList(patient, "Id", "NameED");
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientId,BillNo,DoctorFee,MedicalFee,Testfee,TotalAmmount")] BillTB billTB)
        {
            if (ModelState.IsValid)
            {
                billTB.BillNo = BillNumber(billTB.PatientId);
                PatientTB patient = (from pt in db.PatientTBs
                    where pt.Id == billTB.PatientId
                    select pt).SingleOrDefault();
                patient.Action = 0;
                db.SaveChanges();
                db.BillTBs.Add(billTB);
                db.SaveChanges();

                //Get Submite Id//
                db.Entry(billTB).GetDatabaseValues();
                int id = billTB.Id;
                return RedirectToAction("ShowBill", "Bill", new { id = id });
            }

            ViewBag.PatientId = new SelectList(db.PatientTBs, "Id", "Name", billTB.PatientId);
            return View(billTB);
        }

        // GET: Bill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillTB billTB = db.BillTBs.Find(id);
            if (billTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = new SelectList(db.PatientTBs, "Id", "Name", billTB.PatientId);
            return View(billTB);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientId,BillNo,DoctorFee,MedicalFee,Testfee,TotalAmmount")] BillTB billTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientId = new SelectList(db.PatientTBs, "Id", "Name", billTB.PatientId);
            return View(billTB);
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillTB billTB = db.BillTBs.Find(id);
            if (billTB == null)
            {
                return HttpNotFound();
            }
            return View(billTB);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillTB billTB = db.BillTBs.Find(id);
            db.BillTBs.Remove(billTB);
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

        public string BillNumber(int PatientId)
        {
            string depName = db.PatientTBs.Single(c => c.Id == PatientId).DoctorTB.DepartmentTB.DepartmentName;
            string month = db.PatientTBs.Single(c => c.Id == PatientId).AdmitDate.ToString("yyyy");

            string countString = PatientId.ToString();
            if (countString.Length == 1)
            {
                string newNumber = "00" + countString;
                string billNumber = depName.Substring(0, 1) + "-" + month + newNumber;
                return billNumber;
            }
            else if (countString.Length == 2)
            {
                string newNumber = "0" + countString;
                string billNumber = depName.Substring(0, 1) + "-" + month + newNumber;
                return billNumber;
            }
            else
            {
                string newNumber = countString;
                string billNumber = depName.Substring(0, 1) + "-" + month + newNumber;
                return billNumber;
            }





        }


        public ActionResult ShowBill(int id)
        {
            var appL = db.BillTBs.Where(c => c.Id == id).Select(c => c);
            return View(appL);
        }

        public ActionResult BillSlip(int id)
        {
            var appL = db.BillTBs.Where(c => c.Id == id).Select(c => c);
            foreach (var appLL in appL)
            {
                ViewBag.Name = appLL.PatientTB.NameED;
                ViewBag.BillNo = appLL.BillNo;
                ViewBag.TestFee = appLL.Testfee;
                ViewBag.MedicalFee = appLL.MedicalFee;
                ViewBag.Age = appLL.PatientTB.Age;
                ViewBag.DoctorFee = appLL.DoctorFee;
                ViewBag.TotalAmmount = appLL.TotalAmmount;
                ViewBag.Doctor = appLL.PatientTB.DoctorTB.NameED;
            }
            return View(appL);
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("BillSlip", new { id = id });
        }
    }
}
