using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalInformationSystemWebApp.Models;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class WardController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();
        PasswordHelper passwordHelper = new PasswordHelper();

        // GET: Ward
        public ActionResult Index()
        {
            var wardTBs = db.WardTBs.Include(w => w.DepartmentTB);
            return View(wardTBs.ToList());
        }

        // GET: Ward/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardTB wardTB = db.WardTBs.Find(id);
            if (wardTB == null)
            {
                return HttpNotFound();
            }
            return View(wardTB);
        }

        // GET: Ward/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName");
            return View();
        }

        // POST: Ward/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WardNo,DepartmentId,TotalSeat")] WardTB wardTB)
        {
            if (ModelState.IsValid)
            {
                //wardTB.AvailableSeat = wardTB.SeatQuentity;
                wardTB.WardNo = passwordHelper.AesEncryption(wardTB.WardNo);
                wardTB.AvailableSeat = 0;
                wardTB.SeatQuentity = 0;
                db.WardTBs.Add(wardTB);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName", wardTB.DepartmentId);
            return View(wardTB);
        }

        // GET: Ward/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardTB wardTB = db.WardTBs.Find(id);
            if (wardTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName", wardTB.DepartmentId);
            return View(wardTB);
        }

        // POST: Ward/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WardNo,DepartmentId,SeatQuentity,AvailableSeat,TotalSeat")] WardTB wardTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wardTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName", wardTB.DepartmentId);
            return View(wardTB);
        }

        // GET: Ward/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WardTB wardTB = db.WardTBs.Find(id);
            if (wardTB == null)
            {
                return HttpNotFound();
            }
            return View(wardTB);
        }

        // POST: Ward/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WardTB wardTB = db.WardTBs.Find(id);
            db.WardTBs.Remove(wardTB);
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

        //Seat Unique Cheak
        [AllowAnonymous]
        public JsonResult IswardUnique(string WardNo)
        {
            string wardNo = passwordHelper.AesEncryption(WardNo);
            bool isWardValid = db.WardTBs.Any(x => x.WardNo == wardNo);

            if (isWardValid)
            {
                return Json(!isWardValid, JsonRequestBehavior.AllowGet);
            }

            return Json(!isWardValid, JsonRequestBehavior.AllowGet);
            //return Json( !isAdminValid || !isDoctorValid, JsonRequestBehavior.AllowGet);
        }



        //total Seat Update check///
        //public int seat(int availableSeat,int seatQnt,int totalSeat)
        //{

        //    int beforUpdateSeat = availableSeat+seatQnt;
        //    int aSeat = 0;
        //    if (totalSeat > beforUpdateSeat)
        //    {
        //        totalSeat = totalSeat - beforUpdateSeat;
        //        aSeat = availableSeat+totalSeat;
        //        return aSeat;
        //    }
        //    else if (totalSeat<beforUpdateSeat)
        //    {
        //        totalSeat = beforUpdateSeat - totalSeat;
        //        aSeat = availableSeat - totalSeat;
        //        return aSeat;
        //    }

        //    return Convert.ToInt32(availableSeat);
        //}
    }
}
