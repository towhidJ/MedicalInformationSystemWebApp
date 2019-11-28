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
            var ptId = db.PrescribeTestTBs.Where(c => c.TestName != null).Select(c => c);
            ViewBag.PrescribeTestId = new SelectList(ptId, "Id", "Id");
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
                int id = testTB.Id;
                return RedirectToAction("PreviewTestSubmite", "TestTBs", new { id = id });
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
            var TestInfo = db.PrescribeTestTBs.Where(c => c.Id == prescribeTestId  && c.TestName!=null)
                .Select(c => new {name = c.PatientTB.Name, testName = c.TestName.ToString()});

            return Json(TestInfo);
        }

        //Test Recipe

        public ActionResult PreviewTestSubmite(int id)
        {
            var pTest = db.TestTBs.Where(c => c.Id == id).Select(c => c);
            return View(pTest);
        }

        public ActionResult TestRecipt(int id)
        {
            var pTest = db.TestTBs.Where(c => c.Id == id).Select(c => c);
            List<string> TestName = new List<string>();
            foreach (var pt in pTest)
            {
                var tN = pt.PrescribeTestTB.TestNameED.Split(',');

                for (int i = 0; i < tN.Length; i++)
                {
                    TestName.Add(tN[i].ToString() + "\n");
                }
                ViewBag.Name = pt.PrescribeTestTB.PatientTB.NameED;
                ViewBag.TD = pt.TestDate.ToString("yyyy-M-d dddd");
                ViewBag.DD = pt.DeliveryDate.ToString("yyyy-M-d dddd");
                ViewBag.TestFee = pt.TestFee;
                ViewBag.Sex = pt.PrescribeTestTB.PatientTB.Gender;
                ViewBag.TestName = TestName;
            }
            return View(pTest);
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("TestRecipt", new { id = id });
        }


        //Compare Test date to deliveryDate
        public JsonResult CompareDate(DateTime devDate,DateTime tDate)
        {
            string DeliveryD = devDate.ToString("MM/dd/yyyy");
            string testd = tDate.ToString("MM/dd/yyyy");
            DateTime to, from;

            to = DateTime.Parse(DeliveryD);
            from = DateTime.Parse(testd);
            if (to >= from)
            {
                return Json(1);
            }
            return Json(0);
        }
    }
}
