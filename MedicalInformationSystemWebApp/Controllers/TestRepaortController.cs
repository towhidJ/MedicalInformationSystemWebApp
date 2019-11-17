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
    public class TestRepaortController : Controller
    {
        private MedicalInfoSys db = new MedicalInfoSys();

        // GET: TestRepaort
        public ActionResult Index()
        {
            var testRepaortTBs = db.TestRepaortTBs.Include(t => t.TestTB);
            return View(testRepaortTBs.ToList());
        }

        // GET: TestRepaort/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRepaortTB testRepaortTB = db.TestRepaortTBs.Find(id);
            if (testRepaortTB == null)
            {
                return HttpNotFound();
            }
            return View(testRepaortTB);
        }

        // GET: TestRepaort/Create
        public ActionResult Create()
        {
            ViewBag.TestId = new SelectList(db.TestTBs, "Id", "Id");
            return View();
        }

        // POST: TestRepaort/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Report,TestId")] TestRepaortTB testRepaortTB)
        {
            if (ModelState.IsValid)
            {
                db.TestRepaortTBs.Add(testRepaortTB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestId = new SelectList(db.TestTBs, "Id", "Id", testRepaortTB.TestId);
            return View(testRepaortTB);
        }

        // GET: TestRepaort/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRepaortTB testRepaortTB = db.TestRepaortTBs.Find(id);
            if (testRepaortTB == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestId = new SelectList(db.TestTBs, "Id", "Id", testRepaortTB.TestId);
            return View(testRepaortTB);
        }

        // POST: TestRepaort/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Report,TestId")] TestRepaortTB testRepaortTB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testRepaortTB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestId = new SelectList(db.TestTBs, "Id", "Id", testRepaortTB.TestId);
            return View(testRepaortTB);
        }

        // GET: TestRepaort/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRepaortTB testRepaortTB = db.TestRepaortTBs.Find(id);
            if (testRepaortTB == null)
            {
                return HttpNotFound();
            }
            return View(testRepaortTB);
        }

        // POST: TestRepaort/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestRepaortTB testRepaortTB = db.TestRepaortTBs.Find(id);
            db.TestRepaortTBs.Remove(testRepaortTB);
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

        public JsonResult GetTestReportInfoByPtId(int testId)
        {
            var TestInfo = db.TestTBs.Where(c => c.Id == testId)
                .Select(c => new
                {
                    name = c.PrescribeTestTB.PatientTB.Name, testName = c.PrescribeTestTB.TestName.ToString()

                });
            return Json(TestInfo);
        }

        public ActionResult TestResult(int id)
        {
            var testRepaort = db.TestRepaortTBs.Where(c => c.Id == id).Select(c => c);
            List<string> TestReport = new List<string>();
            foreach (var TR in testRepaort)
            {
                ViewBag.Name = TR.TestTB.PrescribeTestTB.PatientTB.Name;
                ViewBag.TD = TR.TestTB.TestDate.ToString("yyyy-M-d dddd");
                ViewBag.DD = TR.TestTB.DeliveryDate.ToString("yyyy-M-d dddd");
                ViewBag.Age = TR.TestTB.PrescribeTestTB.PatientTB.Age;
                ViewBag.Sex = TR.TestTB.PrescribeTestTB.PatientTB.Gender;

                var tN = TR.Report.Split(',');

                for (int i = 0; i < tN.Length; i++)
                {
                    TestReport.Add(tN[i].ToString() + "\n");
                }

            }

            ViewBag.Result = TestReport;
            return View(testRepaort);
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("AppointmentSlip", new { id = id });
        }
    }
}
