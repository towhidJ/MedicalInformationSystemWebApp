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
    public class StaffController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();

        // GET: Staff
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var staffTBs = db.StaffTBs.Include(s => s.RoleTB);
            return View(staffTBs.ToList());
        }

        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTB staffTB = db.StaffTBs.Find(id);
            if (staffTB == null)
            {
                return HttpNotFound();
            }
            return View(staffTB);
        }

        // GET: Staff/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role");
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Dob,Phone,Joined,Address,Gender,RoleId")] StaffTB staffTB,HttpPostedFileBase UploadImage)
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
                        staffTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View(staffTB);
                    }
                }
                else
                {
                    staffTB.ImagePath = "profile.png";
                }
                db.StaffTBs.Add(staffTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", staffTB.RoleId);
            return View(staffTB);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTB staffTB = db.StaffTBs.Find(id);
            if (staffTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", staffTB.RoleId);
            return View(staffTB);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Dob,Phone,Joined,Address,Gender,ImagePath,RoleId")] StaffTB staffTB,HttpPostedFileBase UploadImage)
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
                        staffTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View(staffTB);
                    }
                }
                else
                {
                    string k = null;
                    var id = staffTB.Id;
                    //var i = from im in db.DoctorTBs
                    //    where im.DoctorId == doctorTB.DoctorId
                    //    select new {img = im.ImagePath};

                    var i = db.StaffTBs.Where(c => c.Id == staffTB.Id).Select(c => c.ImagePath);

                    foreach (var m in i)
                    {
                        k = m;
                    }
                    staffTB.ImagePath = k;

                    //doctorTB.ImagePath = "profile.png";
                }

                db.Entry(staffTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", staffTB.RoleId);
            return View(staffTB);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffTB staffTB = db.StaffTBs.Find(id);
            if (staffTB == null)
            {
                return HttpNotFound();
            }
            return View(staffTB);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffTB staffTB = db.StaffTBs.Find(id);
            db.StaffTBs.Remove(staffTB);
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

        [AllowAnonymous]
        public JsonResult IsPhoneUnique(string Phone)
        {
            bool isStaffValid = db.StaffTBs.Any(x => x.Phone == Phone);

            if (isStaffValid)
            {
                return Json(!isStaffValid, JsonRequestBehavior.AllowGet);
            }
            return Json(!isStaffValid,JsonRequestBehavior.AllowGet);
            //return Json( !isAdminValid || !isDoctorValid, JsonRequestBehavior.AllowGet);
        }
    }
}
