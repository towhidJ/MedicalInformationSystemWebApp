using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MedicalInformationSystemWebApp.Models;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class SeatController : Controller
    {
        PasswordHelper passwordHelper = new PasswordHelper();
        private MedicalInfoSys db = new MedicalInfoSys();
        private AesManaged aes = new AesManaged();
        // GET: Seat
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var seatTBs = db.SeatTBs.Include(s => s.WardTB);
            return View(seatTBs.ToList());
        }

        // GET: Seat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatTB seatTB = db.SeatTBs.Find(id);
            if (seatTB == null)
            {
                return HttpNotFound();
            }
            return View(seatTB);
        }

        // GET: Seat/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.WardId = new SelectList(db.WardTBs, "Id", "wwardno");
            return View();
        }

        // POST: Seat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WardId,SeatNo")] SeatTB seatTB)
        {
            if (ModelState.IsValid)
            {
                //--------------Update ward Available seat------------ -//
                seatTB.SeatNo = passwordHelper.AesEncryption(seatTB.SeatNo);
                int avSeat = Convert.ToInt32(db.WardTBs.Single(c => c.Id == seatTB.WardId).SeatQuentity);
                avSeat = avSeat + 1;
                WardTB sWardTb = (from wt in db.WardTBs
                                  where wt.Id == seatTB.WardId
                                  select wt).SingleOrDefault();
                sWardTb.SeatQuentity = avSeat;
                sWardTb.AvailableSeat = avSeat;


                db.SaveChanges();
                //---------------------------------------------------//
                db.SeatTBs.Add(seatTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WardId = new SelectList(db.WardTBs, "Id", "wwardno", seatTB.WardId);
            return View(seatTB);
        }

        // GET: Seat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatTB seatTB = db.SeatTBs.Find(id);
            if (seatTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.WardId = new SelectList(db.WardTBs, "Id", "wwardno", seatTB.WardId);
            return View(seatTB);
        }

        // POST: Seat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WardId,SeatNo")] SeatTB seatTB)
        {
            if (ModelState.IsValid)
            {
                if (seatTB.SeatNo.Length < 7)
                {
                    seatTB.SeatNo = passwordHelper.AesEncryption(seatTB.SeatNo);
                }

                db.Entry(seatTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WardId = new SelectList(db.WardTBs, "Id", "wwardno", seatTB.WardId);
            return View(seatTB);
        }

        // GET: Seat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatTB seatTB = db.SeatTBs.Find(id);
            if (seatTB == null)
            {
                return HttpNotFound();
            }
            return View(seatTB);
        }

        // POST: Seat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //--------------Update ward Available seat-------------//
            int wardId = (db.SeatTBs.Single(c => c.Id == id).WardId);
            int avSeat = Convert.ToInt32(db.WardTBs.Single(c => c.Id == wardId).SeatQuentity);
            avSeat = avSeat - 1;
            WardTB sWardTb = (from wt in db.WardTBs
                              where wt.Id == wardId
                              select wt).SingleOrDefault();
            sWardTb.SeatQuentity = avSeat;
            sWardTb.AvailableSeat = avSeat;
            db.SaveChanges();
            //---------------------------------------------------//
            SeatTB seatTB = db.SeatTBs.Find(id);
            db.SeatTBs.Remove(seatTB);
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
        public JsonResult IsSeatUnique(string SeatNo)
        {
            string seat = passwordHelper.AesEncryption(SeatNo);
            bool isSeatValid = db.SeatTBs.Any(x => x.SeatNo == seat);

            if (isSeatValid)
            {
                return Json(!isSeatValid, JsonRequestBehavior.AllowGet);
            }

            return Json(!isSeatValid, JsonRequestBehavior.AllowGet);
            //return Json( !isAdminValid || !isDoctorValid, JsonRequestBehavior.AllowGet);
        }

        //Available seat check

        public JsonResult IsSeatAvailable(int wardId)
        {
            int totalSeat = Convert.ToInt32(db.WardTBs.Single(c => c.Id == wardId).TotalSeat);
            int seatQnt = Convert.ToInt32(db.WardTBs.Single(c => c.Id == wardId).SeatQuentity);
            if (totalSeat == seatQnt)
            {
                return Json(0);// 0 for false
            }
            return Json(1); // 1 for True
        }
    }
}
