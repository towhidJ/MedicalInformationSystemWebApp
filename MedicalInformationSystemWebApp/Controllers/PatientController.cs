﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class PatientController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();

        // GET: Patient
        public ActionResult Index()
        {
            var patientTBs = db.PatientTBs.Include(p => p.SeatTB).Include(p => p.WardTB);
            return View(patientTBs.ToList());
        }

        // GET: Patient/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientTB patientTB = db.PatientTBs.Find(id);
            if (patientTB == null)
            {
                return HttpNotFound();
            }
            return View(patientTB);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            ViewBag.SeatId = new SelectList(db.SeatTBs, "Id", "SeatNo");
            ViewBag.WardId = new SelectList(db.WardTBs, "Id", "WardNo");
            return View();
        }

        // POST: Patient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,Gender,Address,AdmitDate,WardId,SeatId,ImagePath,Problem")] PatientTB patientTB, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                patientTB.Action = 1;
                Random r = new Random();
                int random = r.Next();
                if (UploadImage != null)
                {

                    if (UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/png" ||
                        UploadImage.ContentType == "image/jpeg")
                    {
                        string fileName = random + UploadImage.FileName;
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/EmployeeImage/" + fileName);
                        patientTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View(patientTB);
                    }
                }
                else
                {
                    patientTB.ImagePath = "profile.png";
                }

                //--------------Update ward Available seat------------ -//
                int avSeat = Convert.ToInt32(db.WardTBs.Single(c => c.Id == patientTB.WardId).AvailableSeat);
                avSeat = avSeat - 1;
                WardTB sWardTb = (from wt in db.WardTBs
                    where wt.Id == patientTB.WardId
                    select wt).SingleOrDefault();
                sWardTb.AvailableSeat = avSeat;
                db.SaveChanges();
                db.PatientTBs.Add(patientTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SeatId = new SelectList(db.SeatTBs, "Id", "SeatNo", patientTB.SeatId);
            var ward = db.WardTBs.Select(c => new {c.Id, c.WardNo});
            ViewBag.WardId = new SelectList(ward, "Id", "WardNo", patientTB.WardId);
            return View(patientTB);
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientTB patientTB = db.PatientTBs.Find(id);
            if (patientTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.SeatId = new SelectList(db.SeatTBs, "Id", "SeatNo", patientTB.SeatId);
            ViewBag.WardId = new SelectList(db.WardTBs, "Id", "WardNo", patientTB.WardId);
            return View(patientTB);
        }

        // POST: Patient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Gender,Address,AdmitDate,WardId,SeatId,ImagePath,Problem")] PatientTB patientTB, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                patientTB.Action = 1;
                Random r = new Random();
                int random = r.Next();
                if (UploadImage != null)
                {

                    if (UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/png" ||
                        UploadImage.ContentType == "image/jpeg")
                    {
                        string fileName = random + UploadImage.FileName;
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/EmployeeImage/" + fileName);
                        patientTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    string k = null;
                    var id = patientTB.Id;
                    //var i = from im in db.DoctorTBs
                    //    where im.DoctorId == doctorTB.DoctorId
                    //    select new {img = im.ImagePath};

                    var i = db.PatientTBs.Where(c => c.Id == patientTB.Id).Select(c => c.ImagePath);

                    foreach (var m in i)
                    {
                        k = m;
                    }
                    patientTB.ImagePath = k;

                    //doctorTB.ImagePath = "profile.png";
                }

                //--------------Update ward Available seat------------ -//
                int avSeat = Convert.ToInt32(db.WardTBs.Single(c => c.Id == patientTB.WardId).AvailableSeat);
                avSeat = avSeat - 1;
                WardTB sWardTb = (from wt in db.WardTBs
                    where wt.Id == patientTB.WardId
                    select wt).SingleOrDefault();
                sWardTb.AvailableSeat = avSeat;
                db.SaveChanges();
                db.Entry(patientTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SeatId = new SelectList(db.SeatTBs, "Id", "SeatNo", patientTB.SeatId);
            ViewBag.WardId = new SelectList(db.WardTBs, "Id", "WardNo", patientTB.WardId);
            return View(patientTB);
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientTB patientTB = db.PatientTBs.Find(id);
            if (patientTB == null)
            {
                return HttpNotFound();
            }
            return View(patientTB);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientTB patientTB = db.PatientTBs.Find(id);
            db.PatientTBs.Remove(patientTB);
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


        public JsonResult GetSeatByWardId(int wardId)
        {
            var seat = db.SeatTBs.Where(c => c.WardId == wardId).Select(c => new{ Id=c.Id,SeatNo=c.SeatNo}).ToList();
            return Json(seat);
        }

        public JsonResult IsSeatAvailable(int wardId)
        {
            int avs = Convert.ToInt32(db.WardTBs.Single(c => c.Id == wardId).AvailableSeat);
            return Json(avs); // 1 for True
        }


        public JsonResult IsSeatIsAvailable(int seatId)
        {
            int isAvail = db.PatientTBs.Where(c => c.SeatId == seatId).Select(c => c.SeatId).Count();
            if (isAvail>0)
            {
                return Json(0);
            }
            return Json(1);
        }
    }
}