using Clinic.Models.CCMModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Controllers.CCM_Note
{
    public class CCMController : Controller
    {
        // GET: CCM
        public ActionResult Index(Int64 PatientId)
        {
            CCMModel model = new CCMModel();
            List<CCMModel> listmodel = new List<CCMModel>();
            listmodel = model.GetCCMNoteData();
            return View(listmodel);
        }

        [HttpPost]
        public void SaveCCMDetails(string PatientId, string datepicker, string timepicker, string inputTimeSpent, string HdnDescription, bool IsBillable, bool IsInitiatinVisit)
        {
            CCMModel model = new CCMModel();
            model.PatientID = Convert.ToInt64(PatientId);
            if (datepicker != null)
            {
                model.CurrentDate = new DateTime(Convert.ToInt32(datepicker.Split('/')[2]),
                    Convert.ToInt32(datepicker.Split('/')[1]),
                    Convert.ToInt32(datepicker.Split('/')[0]));
            }
            model.CurrentTime = timepicker;
            DateTime TimeSpent = DateTime.ParseExact(inputTimeSpent, "HH:mm:ss", CultureInfo.InvariantCulture);
            model.MinuteSpent = TimeSpent;
            if (HdnDescription.StartsWith(","))
            {
                model.Description = HdnDescription.Substring(1);
            }
            model.IsBillable = IsBillable;
            model.IsInitialVisit = IsInitiatinVisit;
            Int64 RecordId = model.SaveCCMNoteDetails();
            if (RecordId > 0)
            {

            }
        }

        public ActionResult AddNewNoteForm()
        {
            return PartialView("AddNewNoteForm");
        }


        public string SaveCCMNoteDetails(CCMModel model)
        {
            Int64 RecordID = model.SaveCCMNoteInfoDetails();
            if (RecordID > 0)
            {

            }
            return "";
        }




        [HttpPost]
        public JsonResult getCCMNoteDescription(string ID)
        {
            CCMModel model = new CCMModel();
            model.CCMNoteID = Convert.ToInt64(ID);
            string CcmDescription = model.getCCMNoteDescription(model);
            if (CcmDescription != "" || CcmDescription != null)
            {
                return Json(CcmDescription);
            }
            return Json(string.Empty);
        }
    }
}