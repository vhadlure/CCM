using Clinic.Controllers.Utility;
using Clinic.Models.PatientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Controllers.Patient
{
    public class PatientController : Controller
    {
        // GET: Patient
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

        public ActionResult AddVitalForm()
        {
            return PartialView("AddVitalForm");
        }

        public ActionResult AddMedicationForm()
        {
            return PartialView("AddMedicationForm");
        }

        [HttpGet]
        public JsonResult GetAllVitalDetails_ForList(int? page, int? limit, string sortBy, string direction, string searchString = null, string patientID = null)
        {
            try
            {
                PatientModel model = new PatientModel();
                model.CurrentPage = page.Value;
                model.NumberOfRecords = limit.Value;
                model.OrderBy = string.Format("{0} {1}", sortBy, direction);
                model.PatientID = Convert.ToInt64(patientID);
                var records = model.GetVitalData_ForList();//new GridModel().GetPlayers(page, limit, sortBy, direction, searchString, out total);
                int total = records.Count > 0 ? records.FirstOrDefault().TotalCount : 0;

                return Json(new { records, total }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PatientMaster()
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

        public ActionResult PatientProfileView(string clinicname)
        {
            PatientModel model = new PatientModel();
            model.ClinicName = clinicname;
            List<PatientModel> listmodel = new List<PatientModel>();
            listmodel = model.GetPatientProfileData();
            return View(listmodel);
        }

        public ActionResult PatientIndividualProfile(Int64 PatientID)
        {
            PatientModel model = new PatientModel();
            model.PatientID = PatientID;

            model = model.GetPatientDataByPatientId();
            return View(model);
        }

        public string SaveMedicationDetails(string PatientId, string MedicationName, string MedicationDescription)
        {
            PatientModel model = new PatientModel();
            Int64 RecordID = 0;
            model.PatientID = Convert.ToInt64(PatientId);
            model.MedicationName = MedicationName;
            model.MedicationDescription = MedicationDescription;
            RecordID = model.SaveMedicationDetails();
            return "";
        }
        public string SavePatientDetails(PatientModel model)
        {
            Int64 RecordID = 0;
            if (model.DOBStr != null)
            {
                model.DOB = new DateTime(Convert.ToInt32(model.DOBStr.Split('/')[2]),
                Convert.ToInt32(model.DOBStr.Split('/')[1]),
                Convert.ToInt32(model.DOBStr.Split('/')[0]));

                RecordID = model.SavePatientDetails();
            }
            if (RecordID > 0)
            {

            }
            return "";
        }

        [HttpGet]
        public JsonResult GetAllPatientDetails_ForList(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            try
            {
                PatientModel model = new PatientModel();
                model.CurrentPage = page.Value;
                model.NumberOfRecords = limit.Value;
                model.OrderBy = string.Format("{0} {1}", sortBy, direction);
                var records = model.GetPatientData_ForList();//new GridModel().GetPlayers(page, limit, sortBy, direction, searchString, out total);
                int total = records.Count > 0 ? records.FirstOrDefault().TotalCount : 0;

                return Json(new { records, total }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                UtilityController.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public string SaveVitalDetails(string PatientId, string BloodPressure, string ChectCircumference, string Height, string Weight, string Pain, string Respiration, string PulseOximetry, string Pulse, string Temperature)
        {
            PatientModel model = new PatientModel();
            model.PatientID = Convert.ToInt64(PatientId); model.BloodPressure = Convert.ToInt32(BloodPressure);
            model.ChectCircumference = Convert.ToInt32(ChectCircumference);
            model.Height = Convert.ToInt32(Height);
            model.Weight = Convert.ToInt32(Weight);
            model.Pain = Convert.ToInt32(Pain);
            model.Respiration = Convert.ToInt32(Respiration);
            model.PulseOximetry = Convert.ToInt32(PulseOximetry);
            model.Pulse = Convert.ToInt32(Pulse);
            model.Temperature = Convert.ToDecimal(Temperature);
            Int64 RecordID = model.SaveVitalDetails();
            if (RecordID > 0)
            {

            }
            return "";
        }
    }
}