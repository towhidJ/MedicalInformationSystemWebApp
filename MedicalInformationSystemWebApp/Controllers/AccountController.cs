using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MedicalInformationSystemWebApp.Models;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;

namespace MedicalInformationSystemWebApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        MedicalInfoSys db = new MedicalInfoSys();
        PasswordHelper passwordHelper = new PasswordHelper();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(MemberShip  model)
        {

                using (var context = new MedicalInfoSys())
                {
                    var Adpass = passwordHelper.Encode(model.Password);
                    
                    bool isAdminValid = context.AdminTBs.AsEnumerable().Any(x => x.Email == model.Email && passwordHelper.Decode(x.Password) == model.Password);
                    bool isDoctorValid = context.DoctorTBs.AsEnumerable().Any(x => x.Email == model.Email && passwordHelper.Decode(x.Password) == model.Password);
                    bool isNurseValid = context.NurseTBs.AsEnumerable().Any(x => x.Email == model.Email && passwordHelper.Decode(x.Password) == model.Password);
                    bool isReceptionValid = context.ReceptionTBs.AsEnumerable().Any(x => x.Email == model.Email && passwordHelper.Decode(x.Password) == model.Password);
                if (isAdminValid)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (isDoctorValid)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (isNurseValid)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        //Session["EmailId"] = model.Email.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else if (isReceptionValid)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, false);
                        return RedirectToAction("Index", "Home");
                    }
                ModelState.AddModelError("", "Invalid Email or Password");
                    return View();
                }


        }
        [AllowAnonymous]
        public JsonResult IsEmailUnique(string Email)
        {
            bool isAdminValid = db.AdminTBs.Any(x => x.Email==Email );
            bool isDoctorValid = db.DoctorTBs.Any(x => x.Email ==Email);
            bool isNurseValid = db.NurseTBs.Any(x => x.Email ==Email);
            bool isReceptionValid = db.ReceptionTBs.Any(x => x.Email ==Email);

            if (isAdminValid)
            {
                return Json(!isAdminValid, JsonRequestBehavior.AllowGet);
            }
            else if (isDoctorValid)
            {
                return Json(!isDoctorValid, JsonRequestBehavior.AllowGet);
            }
            else if (isNurseValid)
            {
                return Json(!isNurseValid, JsonRequestBehavior.AllowGet);
            }
            else if (isReceptionValid)
            {
                return Json(!isReceptionValid, JsonRequestBehavior.AllowGet);
            }
            return Json(!isAdminValid || !isDoctorValid || !isNurseValid || !isReceptionValid,JsonRequestBehavior.AllowGet);
            //return Json( !isAdminValid || !isDoctorValid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}