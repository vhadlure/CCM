using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clinic.Models.CommonModel;
using Clinic.Controllers.Utility;
using System.Reflection;

namespace Clinic.Controllers.Common
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult GenderDDL()
        {
            return PartialView("GenderDDL");
        }

        public ActionResult CityDDL()
        {
            return PartialView("CityDDL");
        }

        public ActionResult ClinicDDL()
        {
            return PartialView("ClinicDDL");
        }
               
        public ActionResult StateDDL()
        {
            return PartialView("StateDDL");
        }

        public ActionResult CountryDDL()
        {
            return PartialView("CountryDDL");
        }

        public ActionResult BloodGroupDDL()
        {
            return PartialView("BloodGroupDDL");
        }

        public JsonResult GetCountries(string term)
        {
            try
            {
                CommonModel model = new CommonModel();
                model.SearchString = term;
                List<CommonModel> countryList = model.GetCountries();
                var jsonData = new
                {
                    rows = (from data in countryList
                            select new
                            {
                                i = data.CountryID,
                                cell = new string[] {
                            data.CountryName.ToString(),
                       }
                            }).ToList()
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }

        public JsonResult GetClinic(string term)
        {
            try
            {
                CommonModel model = new CommonModel();
                model.SearchString = term;
                List<CommonModel> clinicList = model.GetClinic();
                var jsonData = new
                {
                    rows = (from data in clinicList
                            select new
                            {
                                i = data.ClinicID,
                                cell = new string[] {
                            data.ClinicName.ToString(),
                       }
                            }).ToList()
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
        public JsonResult GetCities(string term)
        {
            try
            {
                CommonModel model = new CommonModel();
                model.SearchString = term;
                List<CommonModel> cityList = model.GetCities();
                var jsonData = new
                {
                    rows = (from data in cityList
                            select new
                            {
                                i = data.CityID,
                                cell = new string[] {
                            data.CityName.ToString(),
                       }
                            }).ToList()
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
        public JsonResult GetStates(string term)
        {
            try
            {
                CommonModel model = new CommonModel();
                model.SearchString = term;
                List<CommonModel> stateList = model.GetStates();
                var jsonData = new
                {
                    rows = (from data in stateList
                            select new
                            {
                                i = data.StateID,
                                cell = new string[] {
                            data.StateName.ToString(),
                       }
                            }).ToList()
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return null;
            }
        }
    }
}