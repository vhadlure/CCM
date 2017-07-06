using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Clinic.Models.UserModel
{
    public class UserModel : Clinic.Models.UtilityModel.UtilityModel
    {
        public Int64 UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string AddressLine { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string Pincode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string ClinicName { get; set; }
        public string FullName { get; set; }
        public string GenderStr { get; set; }

        public static string SaveUserDetails_SP = "SP_InsertUserData";
        public static string GetUserDataForList_SP = "SP_GetUserData_Forlist";

        public Int64 SaveUserDetails()
        {
            Int64 RecordID = -1;
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@UserID", UserID);
            parameters.Add("@FirstName", FirstName);
            parameters.Add("@MiddleName", MiddleName);
            parameters.Add("@LastName", LastName);
            parameters.Add("@Gender", Gender);
            parameters.Add("@EmailID", EmailID);
            parameters.Add("@MobileNumber", MobileNumber);
            parameters.Add("@Address", AddressLine);
            parameters.Add("@CityID", CityName);
            parameters.Add("@CountryID", CountryName);
            parameters.Add("@StateID", StateName);
            parameters.Add("@Pincode", Pincode);
            parameters.Add("@Password", Password);
            parameters.Add("@ClinicName", ClinicName);
            DBManager.CreateUpdateData(SaveUserDetails_SP, parameters, out RecordID);
            return RecordID;
        }

        public List<UserModel> GetUserData_ForList()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CurrentPage", CurrentPage);
            parameters.Add("@NumberOfRecords", NumberOfRecords);
            parameters.Add("@OrderBy", OrderBy);
            return TransformPatientData(DBManager.GetData(GetUserDataForList_SP, parameters));
        }
        public List<UserModel> TransformPatientData(DataTable data)
        {
            List<UserModel> userList = new List<UserModel>();
            if (data.Rows.Count > 0)
            {
                foreach (var item in data.AsEnumerable())
                {
                    UserModel obj = new UserModel();
                    obj.UserID = Convert.ToInt64(item["NUMBER"]);
                    obj.FullName = Convert.ToString(item["Name"]);
                    obj.AddressLine = Convert.ToString(item["Address"]);
                    obj.Gender = Convert.ToInt32(item["Gender"]);
                    obj.GenderStr = (obj.Gender == 1) ? "Male" : "Female";
                    obj.MobileNumber = Convert.ToString(item["MobileNumber"]);
                    obj.TotalCount = Convert.ToInt16(item["TotalCount"]);
                    userList.Add(obj);
                }
            }
            return userList;
        }
    }
}