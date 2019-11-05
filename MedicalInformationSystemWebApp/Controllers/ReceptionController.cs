using System;
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
    public class ReceptionController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();

        // GET: Reception
        public ActionResult Index()
        {
            var receptionTBs = db.ReceptionTBs.Include(r => r.RoleTB);
            return View(receptionTBs.ToList());
        }

        // GET: Reception/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceptionTB receptionTB = db.ReceptionTBs.Find(id);
            if (receptionTB == null)
            {
                return HttpNotFound();
            }
            return View(receptionTB);
        }

        // GET: Reception/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role");
            return View();
        }

        // POST: Reception/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Address,Dob,Gender,Email,Password,RoleId,Joined")] ReceptionTB receptionTB, HttpPostedFileBase UploadImage)
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
                        receptionTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View(receptionTB);
                    }
                }
                else
                {
                    receptionTB.ImagePath = "profile.png";
                }
                db.ReceptionTBs.Add(receptionTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", receptionTB.RoleId);
            return View(receptionTB);
        }

        // GET: Reception/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceptionTB receptionTB = db.ReceptionTBs.Find(id);
            if (receptionTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", receptionTB.RoleId);
            return View(receptionTB);
        }

        // POST: Reception/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Address,Dob,Gender,ImagePath,Email,Password,RoleId,Joined")] ReceptionTB receptionTB, HttpPostedFileBase UploadImage)
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
                        receptionTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View(receptionTB);
                    }
                }
                else
                {
                    string img = null;
                    //var i = from im in db.DoctorTBs
                    //    where im.DoctorId == doctorTB.DoctorId
                    //    select new {img = im.ImagePath};

                    var i = db.ReceptionTBs.Where(c => c.Id == receptionTB.Id).Select(c => c.ImagePath);

                    foreach (var m in i)
                    {
                        img = m;
                    }
                    receptionTB.ImagePath = img;
                }
                db.Entry(receptionTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", receptionTB.RoleId);
            return View(receptionTB);
        }

        // GET: Reception/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReceptionTB receptionTB = db.ReceptionTBs.Find(id);
            if (receptionTB == null)
            {
                return HttpNotFound();
            }
            return View(receptionTB);
        }

        // POST: Reception/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReceptionTB receptionTB = db.ReceptionTBs.Find(id);
            db.ReceptionTBs.Remove(receptionTB);
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
    }
}
