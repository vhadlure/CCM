using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinic.Models.LoginModel;
using Clinic.Controllers.Utility;

namespace Clinic.Controllers.Authentication
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Login()
        {
            return View();
        }

        public string AuthorizeUser(LoginModel model)
        {
            model.Password = UtilityController.Encrypt(model.Password, "CCMClinic");
            model = model.Authorise();
            if (model.UserName != null)
            {
                Session["User"] = model;
                //return RedirectToAction("Index", "Home");
                return "1";
            }
            return "0";
            //return View(model);
        }

        public void signout()
        {
            Session["User"] = null;
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public string GetForgotPasswordDetails(LoginModel model)
        {
            model = model.GetForgotPasswordDetails();

            if (model != null && model.ID > 0)
            {
                UtilityController uController = new UtilityController();

                uController.SendEmail(model.UserName, UtilityController.Decrypt(model.Password, "CCMClinic"), model.EmailID, "Forget Password");
            }
            return null;

        }
    }
}
