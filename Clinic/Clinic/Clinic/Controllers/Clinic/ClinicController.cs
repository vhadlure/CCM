using Clinic.Controllers.Utility;
using Clinic.Models.ClinicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Controllers.Clinic
{
    public class ClinicController : Controller
    {
        // GET: Clinic
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

        public ActionResult ClinicMaster()
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

        public ActionResult ClinicMasterForm()
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

        public string SaveClinicDetails(ClinicModel model)
        {
            Int64 RecordID = model.SaveClinicDetails();
            if (RecordID > 0)
            {

            }
            return "";
        }

        [HttpGet]
        public JsonResult GetAllClinicDetails_ForList(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            try
            {
                ClinicModel model = new ClinicModel();
                model.CurrentPage = page.Value;
                model.NumberOfRecords = limit.Value;
                model.OrderBy = string.Format("{0} {1}", sortBy, direction);
                var records = model.GetClinicData_ForList();//new GridModel().GetPlayers(page, limit, sortBy, direction, searchString, out total);
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