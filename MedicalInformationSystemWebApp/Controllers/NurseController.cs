using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MedicalInformationSystemWebApp.Models;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class NurseController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();

        PasswordHelper passwordHelper = new PasswordHelper();
        // GET: Nurse
        public ActionResult Index()
        {
            //var t = db.NurseTBs.Where(
            //    n => n.Name == "Rani Das"
            //).Include(n => n.RoleTB);
            var nurseTBs = db.NurseTBs.Include(n => n.RoleTB);
            return View(nurseTBs.ToList());
        }

        // GET: Nurse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NurseTB nurseTB = db.NurseTBs.Find(id);
            if (nurseTB == null)
            {
                return HttpNotFound();
            }
            return View(nurseTB);
        }

        // GET: Nurse/Create
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role");
            return View();
        }

        // POST: Nurse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NurseId,Name,Address,Phone,Dob,Qualification,Gender,Joined,Email,Password,RoleId")] NurseTB nurseTB, HttpPostedFileBase UploadImage)
        {

            Random r = new Random();
            int random = r.Next();


            if (ModelState.IsValid)
            {
                nurseTB.Name = passwordHelper.AesEncryption(nurseTB.Name);
                nurseTB.Email = passwordHelper.AesEncryption(nurseTB.Email);
                nurseTB.Address = passwordHelper.AesEncryption(nurseTB.Address);
                nurseTB.Phone = passwordHelper.AesEncryption(nurseTB.Phone);
                if (UploadImage != null)
                {

                    if (UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/png" ||
                        UploadImage.ContentType == "image/jpeg")
                    {
                        string fileName = random + UploadImage.FileName;
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/EmployeeImage/" + fileName);
                        nurseTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View(nurseTB);
                    }
                }
                else
                {
                    nurseTB.ImagePath = "profile.png";
                }
                
                nurseTB.Password = passwordHelper.Encode(nurseTB.Password);
                db.NurseTBs.Add(nurseTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", nurseTB.RoleId);
            return View(nurseTB);
        }

        // GET: Nurse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NurseTB nurseTB = db.NurseTBs.Find(id);
            if (nurseTB == null)
            {
                return HttpNotFound();
            }

            //var role = from s in db.NurseTBs where s.Name == "Nurse" select new{k=s.Name} ;
            var r = db.RoleTBs.Where(c => c.Role == "Nurse").Select(c=>c);
            ViewBag.RoleId = new SelectList(r, "Id", "Role", nurseTB.RoleId);
            return View(nurseTB);
        }

        // POST: Nurse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NurseId,Name,Address,Phone,Dob,Qualification,Gender,Joined,ImagePath,Email,Password,RoleId")] NurseTB nurseTB, HttpPostedFileBase UploadImage,string password)
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
                        nurseTB.ImagePath = fileName;
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    string img = null;
                    var i = db.NurseTBs.Where(c => c.NurseId == nurseTB.NurseId).Select(c => c.ImagePath);

                    foreach (var m in i)
                    {
                        img = m;
                    }
                    
                    nurseTB.ImagePath = img;
                }

                if (nurseTB.Password!=null)
                {
                    nurseTB.Password = passwordHelper.Encode(nurseTB.Password);
                }
                else
                {
                    string pass = null;
                    var i = db.NurseTBs.Where(c=>c.NurseId==nurseTB.NurseId).Select(c => c.Password);
                    foreach (var m in i)
                    {
                        pass = m;
                    }

                    nurseTB.Password = pass;
                }
                if (nurseTB.Name.Length < 15)
                {
                    nurseTB.Name = passwordHelper.AesEncryption(nurseTB.Name);
                }
                db.Entry(nurseTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.RoleTBs, "Id", "Role", nurseTB.RoleId);
            return View(nurseTB);
        }

        // GET: Nurse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NurseTB nurseTB = db.NurseTBs.Find(id);
            if (nurseTB == null)
            {
                return HttpNotFound();
            }
            return View(nurseTB);
        }

        // POST: Nurse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NurseTB nurseTB = db.NurseTBs.Find(id);
            db.NurseTBs.Remove(nurseTB);
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




        //public async Task<ActionResult> Proile()
        //{
        //    NurseTB nurseTb = new NurseTB();
        //    var email = Session["EmailId"].ToString();
        //    var nn = db.NurseTBs.Where(c => c.Email == email).Select(c=>new
        //    {
        //        c.Name,c.Address,c.NurseId
        //    });
        //    foreach (var n in nn)
        //    {
        //        ViewBag.Name = n.Name;
        //        ViewBag.Address = n.Address;
        //        ViewBag.NurseId = n.NurseId;
        //    }
        //    return View();
        //}

    }
}
