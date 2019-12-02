using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class DeshboardController : Controller
    {
        
        MedicalInfoSys db = new MedicalInfoSys();
        // GET: Deshboard
        [Authorize]
        [AllowAnonymous]
        public ActionResult Chart()
        {
            int m1 = 0, m2 = 0, m3 = 0, m4 = 0, m5 = 0, m6 = 0, m7 = 0, m8 = 0, m9 = 0, m10 = 0, m11 = 0, m12 = 0;
            var list = db.PatientTBs.Select(x => x.AdmitDate.ToString()).ToList();
            foreach (var item in list)
            {
                var dateSplit = item.Split('-');
                string year = dateSplit[0];
                string month = dateSplit[1];
                if (DateTime.Now.ToString("yyyy") == year)
                {
                    if (month == "01")
                    {
                        m1 += 1;
                    }
                    else if (month == "02")
                    {
                        m2 += 1;
                    }
                    else if (month == "03")
                    {
                        m3 += 1;
                    }
                    else if (month == "04")
                    {
                        m4 += 1;
                    }
                    else if (month == "05")
                    {
                        m5 += 1;
                    }
                    else if (month == "06")
                    {
                        m6 += 1;
                    }
                    else if (month == "07")
                    {
                        m7 += 1;
                    }
                    else if (month == "08")
                    {
                        m8 += 1;
                    }
                    else if (month == "09")
                    {
                        m9 += 1;
                    }
                    else if (month == "10")
                    {
                        m10 += 1;
                    }
                    else if (month == "11")
                    {
                        m11 += 1;
                    }
                    else if (month == "12")
                    {
                        m12 += 1;
                    }
                }
            }

            List<int> date = new List<int>() { m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12 };
            List<string> monList = new List<string>()
                {"JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"};

            ViewBag.Month = monList;
            ViewBag.Rep = date;
            ViewBag.AppCh = AppoinmentChart();
            return View();
        }




        public List<int> AppoinmentChart()
        {
            int m1 = 0, m2 = 0, m3 = 0, m4 = 0, m5 = 0, m6 = 0, m7 = 0, m8 = 0, m9 = 0, m10 = 0, m11 = 0, m12 = 0;
            
            var list = db.AppointmentTBs.Select(x => x.AppointmentDate.ToString()).ToList();
            foreach (var item in list)
            {
                var dateSplit = item.Split('-');
                string year = dateSplit[0];
                string month = dateSplit[1];
                if (DateTime.Now.ToString("yyyy") == year)
                {
                    if (month == "01")
                    {
                        m1 += 1;
                    }
                    else if (month == "02")
                    {
                        m2 += 1;
                    }
                    else if (month == "03")
                    {
                        m3 += 1;
                    }
                    else if (month == "04")
                    {
                        m4 += 1;
                    }
                    else if (month == "05")
                    {
                        m5 += 1;
                    }
                    else if (month == "06")
                    {
                        m6 += 1;
                    }
                    else if (month == "07")
                    {
                        m7 += 1;
                    }
                    else if (month == "08")
                    {
                        m8 += 1;
                    }
                    else if (month == "09")
                    {
                        m9 += 1;
                    }
                    else if (month == "10")
                    {
                        m10 += 1;
                    }
                    else if (month == "11")
                    {
                        m11 += 1;
                    }
                    else if (month == "12")
                    {
                        m12 += 1;
                    }
                }
            }

            List<int> date = new List<int>() { m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12 };
            return date;
        }
    }
}