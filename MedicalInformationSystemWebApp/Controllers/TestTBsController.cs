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
    public class TestTBsController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();

        // GET: TestTBs
        public ActionResult Index()
        {
            var testTBs = db.TestTBs.Include(t => t.PrescribeTestTB);
            return View(testTBs.ToList());
        }

        // GET: TestTBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTB testTB = db.TestTBs.Find(id);
            if (testTB == null)
            {
                return HttpNotFound();
            }
            return View(testTB);
        }

        // GET: TestTBs/Create
        public ActionResult Create()
        {
            //var patient = db.PrescribeTestTBs.Select(c => new { c.PatientId, c.PatientTB.Name });
            //ViewBag.PrescribeTestId = new SelectList(patient, "PatientId", "Name");
            ViewBag.PrescribeTestId = new SelectList(db.PrescribeTestTBs, "Id", "Id");
            return View();
        }

        // POST: TestTBs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TestDate,DeliveryDate,PrescribeTestId,TestFee")] TestTB testTB)
        {
            if (ModelState.IsValid)
            {
                db.TestTBs.Add(testTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //var patient = db.PrescribeTestTBs.Select(c => new {c.PatientId, c.PatientTB.Name});
            ViewBag.PrescribeTestId = new SelectList(db.PrescribeTestTBs, "Id", "Id", testTB.PrescribeTestId);
            return View(testTB);
        }

        // GET: TestTBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTB testTB = db.TestTBs.Find(id);
            if (testTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrescribeTestId = new SelectList(db.PrescribeTestTBs, "Id", "TestName", testTB.PrescribeTestId);
            return View(testTB);
        }

        // POST: TestTBs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TestDate,DeliveryDate,PrescribeTestId,TestFee")] TestTB testTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrescribeTestId = new SelectList(db.PrescribeTestTBs, "Id", "Id", testTB.PrescribeTestId);
            return View(testTB);
        }

        // GET: TestTBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTB testTB = db.TestTBs.Find(id);
            if (testTB == null)
            {
                return HttpNotFound();
            }
            return View(testTB);
        }

        // POST: TestTBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestTB testTB = db.TestTBs.Find(id);
            db.TestTBs.Remove(testTB);
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


        public JsonResult GetTestInfoByPtId(int prescribeTestId)
        {
            var TestInfo = db.PrescribeTestTBs.Where(c => c.Id == prescribeTestId)
                .Select(c => new {name = c.PatientTB.Name, testName = c.TestName.ToString()});
            return Json(TestInfo);
        }
    }
}
