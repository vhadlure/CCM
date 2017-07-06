using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clinic.Models.UtilityModel;
using System.Data;

namespace Clinic.Models.ClinicModel
{
    public class ClinicModel : Clinic.Models.UtilityModel.UtilityModel
    {
        public Int64 ClinicID { get; set; }
        public string Name { get; set; }
        public string  Address { get; set; }

        public static string SaveClinicDetails_SP = "SP_InsertClinicData";
        public static string GetClinicDataForList_SP = "SP_GetClinicData_Forlist";

        internal long SaveClinicDetails()
        {
            Int64 RecordID = -1;
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@ClinicID", ClinicID);
            parameters.Add("@Name", Name);
            parameters.Add("@Address", Address);
            DBManager.CreateUpdateData(SaveClinicDetails_SP, parameters, out RecordID);
            return RecordID;
        }

        public List<ClinicModel> GetClinicData_ForList()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CurrentPage", CurrentPage);
            parameters.Add("@NumberOfRecords", NumberOfRecords);
            parameters.Add("@OrderBy", OrderBy);
            return TransformCategoryData(DBManager.GetData(GetClinicDataForList_SP, parameters));
        }
        public List<ClinicModel> TransformCategoryData(DataTable data)
        {
            List<ClinicModel> clinicList = new List<ClinicModel>();
            if (data.Rows.Count > 0)
            {
                foreach (var item in data.AsEnumerable())
                {
                    ClinicModel obj = new ClinicModel();
                    obj.ClinicID = Convert.ToInt64(item["NUMBER"]);
                    obj.Name = Convert.ToString(item["Name"]);
                    obj.Address = Convert.ToString(item["Address"]);
                    obj.TotalCount = Convert.ToInt16(item["TotalCount"]);
                    clinicList.Add(obj);
                }
            }
            return clinicList;
        }
    }
}