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
    public class AppointmentController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();

        // GET: Appointment
        public ActionResult Index()
        {
            var appointmentTBs = db.AppointmentTBs.Include(a => a.DoctorTB);
            return View(appointmentTBs.ToList());
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentTB appointmentTB = db.AppointmentTBs.Find(id);
            if (appointmentTB == null)
            {
                return HttpNotFound();
            }
            return View(appointmentTB);
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.DoctorTBs, "DoctorId", "NameED");
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,Name,Type,DoctorId,AppointmentDate,Age,AppointmentNumber")] AppointmentTB appointmentTB)
        {
            if (ModelState.IsValid)
            {
                appointmentTB.AppointmentNumber=AppointMentNumber(appointmentTB.DoctorId, appointmentTB.AppointmentDate);
                appointmentTB.AppointmentFee = 500.00;
                db.AppointmentTBs.Add(appointmentTB);
                db.SaveChanges();

                //Get Submite Id//
                db.Entry(appointmentTB).GetDatabaseValues();
                int id = appointmentTB.AppointmentId;
                return RedirectToAction("ShowAppoinment","Appointment",new {id=id});
            }

            ViewBag.DoctorId = new SelectList(db.DoctorTBs, "DoctorId", "NameED", appointmentTB.DoctorId);
            return View(appointmentTB);
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentTB appointmentTB = db.AppointmentTBs.Find(id);
            if (appointmentTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.DoctorTBs, "DoctorId", "Name", appointmentTB.DoctorId);
            return View(appointmentTB);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentId,Name,Type,DoctorId,AppointmentDate,AppointmentFee,Age,AppointmentNumber")] AppointmentTB appointmentTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointmentTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.DoctorTBs, "DoctorId", "Name", appointmentTB.DoctorId);
            return View(appointmentTB);
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentTB appointmentTB = db.AppointmentTBs.Find(id);
            if (appointmentTB == null)
            {
                return HttpNotFound();
            }
            return View(appointmentTB);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppointmentTB appointmentTB = db.AppointmentTBs.Find(id);
            db.AppointmentTBs.Remove(appointmentTB);
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


        public string AppointMentNumber(int DoctorId,DateTime AppointmentDate)
        {
            string depName = db.DoctorTBs.Single(c => c.DoctorId == DoctorId).DepartmentTB.DepartmentName;
            string month = AppointmentDate.ToString("yyyyMM");

            int count = db.AppointmentTBs.Where(c => c.DoctorId == DoctorId && c.AppointmentDate == AppointmentDate)
                .Select(c => c).Count();

            string countString = count.ToString();
            if (countString.Length == 1)
            {
                string newNumber = "0" + countString;
                string appNumber = depName.Substring(0, 3) + "-" + month + newNumber;
                return appNumber;
            }
            else
            {
                string newNumber = countString;
                string appNumber = depName.Substring(0, 3) + "-" + month + newNumber;
                return appNumber;
            }

        }

        public JsonResult CountAppointMent(int doctorId, DateTime appointmentDate)
        {
            int count = db.AppointmentTBs.Where(c => c.DoctorId == doctorId && c.AppointmentDate == appointmentDate)
                .Select(c => c).Count();
            return Json(count);
        }

        public JsonResult CurrentDate(DateTime appointmentDate)
        {
            string appdate = appointmentDate.ToString("MM/dd/yyyy");
            string CurrDate = DateTime.Now.ToString("MM/dd/yyyy");
            DateTime to, from;

            to =DateTime.Parse(appdate);
            from = DateTime.Parse(CurrDate);
            if (to>=from)
            {
                return Json(1);
            }
            return Json(0);

        }

        public ActionResult ShowAppoinment(int id)
        {
            var appL = db.AppointmentTBs.Where(c => c.AppointmentId == id).Select(c => c);
            return View(appL);
        }

        public ActionResult AppointmentSlip(int id)
        {
            var appL = db.AppointmentTBs.Where(c => c.AppointmentId == id).Select(c => c);
            foreach (var appLL in appL)
            {
                ViewBag.Name = appLL.Name;
                ViewBag.Date = appLL.AppointmentDate.ToString("yyyy-M-d dddd");
                ViewBag.SerialNo = appLL.AppointmentNumber;
                ViewBag.Type = appLL.Type;
                ViewBag.Fee = appLL.AppointmentFee;
                ViewBag.Age = appLL.Age;
                ViewBag.Doctor = appLL.DoctorTB.NameED;
            }
            return View(appL);
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("AppointmentSlip",new {id=id});
        }

    }
}
