using Clinic.Controllers.Utility;
using Clinic.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Controllers.User
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                try
                {
                    return View();
                }
                catch (Exception ex)
                {
                    UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return Redirect("/Authentication/Login");
        }

        public ActionResult AddUser()
        {
            if (Session["User"] != null)
            {
                try
                {
                    return View();
                }
                catch (Exception ex)
                {
                    UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                }
            }
            return Redirect("/Authentication/Login");
        }

        public ActionResult AddUserForm()
        {
            return PartialView("AddUserForm");
        }


        public string SaveUserDetails(UserModel model)
        {
            try
            {
                Random rnd = new Random();
                int myRandomNo = rnd.Next(10000000, 99999999);
                model.Password = UtilityController.Encrypt(Convert.ToString(myRandomNo), "CRYPTI");

                Int64 RecordID = model.SaveUserDetails();
                if (RecordID > 0)
                {
                    model.UserID = RecordID;
                    //model = model.GetUserDetails();
                    //SendUserDetailsInEmail(model.Username, UtilityController.Decrypt(Convert.ToString(model.Password), "CRYPTI"), model.EmailID, model.FirstName, model.LastName);
                }
            }
            catch (Exception ex)
            {
                UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }
            return "";
        }

        [HttpGet]
        public JsonResult GetAllUserDetails_ForList(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            try
            {
                UserModel model = new UserModel();
                model.CurrentPage = page.Value;
                model.NumberOfRecords = limit.Value;
                model.OrderBy = string.Format("{0} {1}", sortBy, direction);
                var records = model.GetUserData_ForList();//new GridModel().GetPlayers(page, limit, sortBy, direction, searchString, out total);
                int total = records.Count > 0 ? records.FirstOrDefault().TotalCount : 0;

                return Json(new { records, total }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}