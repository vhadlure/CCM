using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Clinic.Models.CCMModel
{
    public class CCMModel : Clinic.Models.UtilityModel.UtilityModel
    {

        public Int64 CCMNoteID { get; set; }
        public string CCMNoteText { get; set; }
        public string CCMNoteDescrption { get; set; }
        public DateTime CurrentDateTime { get; set; }

        public Int64 PatientID { get; set; }
        public DateTime CurrentDate { get; set; }
        public string CurrentTime { get; set; }
        public DateTime MinuteSpent { get; set; }
        public string Description { get; set; }
        public bool IsBillable { get; set; }
        public bool IsInitialVisit { get; set; }

       

        public static string SaveCCMNoteInfoDetails_SP = "InsertCCMNotesInfoData";
        public static string GetCCMNoteData_SP = "SP_GetCCMNoteData";
        public static string GetCCMNoteDescription_SP = "SP_GetCCMNoteDescription";
        public static string InsertCCMNoteData_SP = "SP_InsertCCMNoteData";
        

        internal long SaveCCMNoteInfoDetails()
        {
            Int64 RecordID = -1;
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CCMNoteText", CCMNoteText);
            parameters.Add("@CCMNoteDescription", CCMNoteDescrption);
            DBManager.CreateUpdateData(SaveCCMNoteInfoDetails_SP, parameters, out RecordID);
            return RecordID;
        }

        internal List<CCMModel> GetCCMNoteData()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            return TransformCCMNoteData(DBManager.GetData(GetCCMNoteData_SP, parameters));
        }

        private List<CCMModel> TransformCCMNoteData(DataTable dataTable)
        {
            List<CCMModel> ccmnotesList = new List<CCMModel>();
            if (dataTable.Rows.Count > 0)
            {
                foreach (var item in dataTable.AsEnumerable())
                {
                    CCMModel obj = new CCMModel();
                    obj.CCMNoteID = Convert.ToInt64(item["CCMNoteInfoId"]);
                    obj.CCMNoteDescrption = Convert.ToString(item["CCMNoteText"]);
                    obj.CCMNoteText = Convert.ToString(item["CCMNoteDescription"]);
                    obj.CurrentDateTime = DateTime.Now;
                    ccmnotesList.Add(obj);
                }
            }
            return ccmnotesList;
        }

        internal string getCCMNoteDescription(CCMModel model)
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CCMNoteID", CCMNoteID);
            return TransformCCMNoteDesscription(DBManager.GetData(GetCCMNoteDescription_SP, parameters));
        }

        private string TransformCCMNoteDesscription(DataTable dataTable)
        {
            CCMModel obj = new CCMModel();

            if (dataTable.Rows.Count > 0)
            {
                return Convert.ToString(dataTable.Rows[0]["CCMNoteDescription"]);
            }
            return "";
        }

        internal long SaveCCMDetails()
        {
            Int64 RecordID = -1;
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CCMNoteText", CCMNoteText);
            parameters.Add("@CCMNoteDescription", CCMNoteDescrption);
            DBManager.CreateUpdateData(SaveCCMNoteInfoDetails_SP, parameters, out RecordID);
            return RecordID;
        }

        internal long SaveCCMNoteDetails()
        {
            Int64 RecordID = -1;
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CCMNoteID", CCMNoteID);
            parameters.Add("@PatientID", PatientID);
            parameters.Add("@Description", Description);
            parameters.Add("@CCMDate", CurrentDate);
            parameters.Add("@CCMTime", CurrentTime);
            parameters.Add("@TimeSpent",MinuteSpent);
            parameters.Add("@IsBillable", IsBillable);
            parameters.Add("@IsInitialVisti", IsInitialVisit);
            DBManager.CreateUpdateData(InsertCCMNoteData_SP, parameters, out RecordID);
            return RecordID;
        }

       
    }
}