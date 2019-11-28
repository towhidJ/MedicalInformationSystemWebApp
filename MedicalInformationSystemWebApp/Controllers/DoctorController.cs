using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalInformationSystemWebApp.Models;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;
using MedicalInformationSystemWebApp.Models.ModelView;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class DoctorController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();
        PasswordHelper passwordHelper = new PasswordHelper();

        // GET: Doctor
        public ActionResult Index()
        {
            var doctorTBs = db.DoctorTBs.Include(d => d.DepartmentTB).Include(d => d.DesignationTB).Include(d => d.RoleTB).Include(d => d.SpealizationTB);
            return View(doctorTBs.ToList());
        }

        // GET: Doctor/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorTB doctorTB = db.DoctorTBs.Find(id);
            if (doctorTB == null)
            {
                return HttpNotFound();
            }
            return View(doctorTB);
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName");
            ViewBag.DesignationId = new SelectList(db.DesignationTBs, "Id", "DesignationName");
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role");
            ViewBag.SpealizationId = new SelectList(db.SpealizationTBs, "Id", "Type");
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorId,DepartmentId,SpealizationId,DesignationId,RoleId,Name,Address,Phone,Dob,Gender,VisitingTimeStart,VisitingTimeEnd,VisitDay,Joined,Email,Password")] DoctorTB doctorTB, HttpPostedFileBase UploadImage)
        {
            Random r = new Random();
            int random = r.Next();
            if (ModelState.IsValid)
            {
                doctorTB.Name = passwordHelper.AesEncryption(doctorTB.Name);
                doctorTB.Email = passwordHelper.AesEncryption(doctorTB.Email);
                doctorTB.Address = passwordHelper.AesEncryption(doctorTB.Address);
                doctorTB.Phone = passwordHelper.AesEncryption(doctorTB.Phone);
                if (UploadImage != null)
                {

                    if (UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/png" ||
                        UploadImage.ContentType == "image/jpeg")
                    {
                        string fileName = random + UploadImage.FileName;
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/EmployeeImage/" + fileName);
                        doctorTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View(doctorTB);
                    }
                }
                else
                {
                    doctorTB.ImagePath = "profile.png";
                }
                //else
                //    return View(doctorTB);
                doctorTB.Password = passwordHelper.Encode(doctorTB.Password);
                db.DoctorTBs.Add(doctorTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName", doctorTB.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.DesignationTBs, "Id", "DesignationName", doctorTB.DesignationId);
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", doctorTB.RoleId);
            ViewBag.SpealizationId = new SelectList(db.SpealizationTBs, "Id", "Type", doctorTB.SpealizationId);
            return View(doctorTB);
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DoctorTB doctorTB = db.DoctorTBs.Find(id);
            if (doctorTB == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName", doctorTB.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.DesignationTBs, "Id", "DesignationName", doctorTB.DesignationId);
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", doctorTB.RoleId);
            ViewBag.SpealizationId = new SelectList(db.SpealizationTBs, "Id", "Type", doctorTB.SpealizationId);
            return View(doctorTB);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorId,DepartmentId,SpealizationId,DesignationId,RoleId,Name,Address,Phone,Dob,Gender,VisitingTimeStart,VisitingTimeEnd,ImagePath,Joined,Email,Password")] DoctorTB doctorTB, HttpPostedFileBase UploadImage)
        {

            Random r = new Random();
            int random = r.Next();
            if (ModelState.IsValid)
            {


                if (UploadImage != null)
                {

                    if (UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/png" ||
                        UploadImage.ContentType == "image/jpeg")
                    {
                        string fileName = random + UploadImage.FileName;
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/EmployeeImage/" + fileName);
                        doctorTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    string k = null;
                    var id = doctorTB.DoctorId;
                    //var i = from im in db.DoctorTBs
                    //    where im.DoctorId == doctorTB.DoctorId
                    //    select new {img = im.ImagePath};

                    var i = db.DoctorTBs.Where(c => c.DoctorId == doctorTB.DoctorId).Select(c => c.ImagePath);

                    foreach (var m in i)
                    {
                        k = m;
                    }
                    doctorTB.ImagePath = k;

                    //doctorTB.ImagePath = "profile.png";
                }

                if (doctorTB.VisitingTimeStart != null && doctorTB.VisitingTimeEnd != null && doctorTB.VisitDay != null)
                {
                    doctorTB.VisitingTimeStart = doctorTB.VisitingTimeStart;
                    doctorTB.VisitingTimeEnd = doctorTB.VisitingTimeEnd;
                    doctorTB.VisitDay = doctorTB.VisitDay;
                }
                else
                {
                    string vDay = null, vtimeStart = null, vtimeEnd = null;
                    var i = db.DoctorTBs.Where(c => c.DoctorId == doctorTB.DoctorId).Select(c => new
                    {
                        c.VisitingTimeEnd,
                        c.VisitingTimeStart,
                        c.VisitDay
                    });
                    foreach (var m in i)
                    {
                        //vDay = m.VisitDay;
                        vtimeStart = m.VisitingTimeStart;
                        vtimeEnd = m.VisitingTimeEnd;
                    }

                    doctorTB.VisitingTimeStart = vtimeStart;
                    doctorTB.VisitingTimeEnd = vtimeEnd;
                    doctorTB.VisitDay = vDay;
                }


                if (doctorTB.Name.Length < 15)
                {
                    doctorTB.Name = passwordHelper.AesEncryption(doctorTB.Name);
                }
                db.Entry(doctorTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName", doctorTB.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.DesignationTBs, "Id", "DesignationName", doctorTB.DesignationId);
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", doctorTB.RoleId);
            ViewBag.SpealizationId = new SelectList(db.SpealizationTBs, "Id", "Type", doctorTB.SpealizationId);
            return View(doctorTB);
        }

        // GET: Doctor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorTB doctorTB = db.DoctorTBs.Find(id);
            if (doctorTB == null)
            {
                return HttpNotFound();
            }
            return View(doctorTB);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorTB doctorTB = db.DoctorTBs.Find(id);
            db.DoctorTBs.Remove(doctorTB);
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


        [HttpGet]
        public ActionResult GetAllDoctorByDep()
        {
            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName");
            return View();
        }

        public JsonResult GetAllDoctorByDepartmentId(int departmentId)
        {
            ViewBag.DepartmentId = new SelectList(db.DepartmentTBs, "Id", "DepartmentName");
            //var df = new List<DoctorTB>();
            var na = db.DoctorTBs.Single(c => c.DepartmentId == departmentId).Name.ToString();
            na = passwordHelper.AesDecryption(na);
            var GetAllDoctor = from dc in db.DoctorTBs
                               where dc.DepartmentId == departmentId
                               select new
                               {
                                   Img = dc.ImagePath.ToString(),
                                   Name = na,
                                   Start = dc.VisitingTimeStart.ToString(),
                                   End = dc.VisitingTimeEnd.ToString(),
                                   dc.VisitDay,
                               };
            //var GetAllDoctor =
            //    db.DoctorTBs.Where(c => c.DepartmentId == departmentId).Select(c => c.Name);
            //foreach (var dn in GetAllDoctor)
            //{

            //   df.Add(dn);
            //}
            return Json(GetAllDoctor);
        }
    }
}
