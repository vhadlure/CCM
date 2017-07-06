using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Clinic.Models.PatientModel
{
    public class PatientModel : Clinic.Models.UtilityModel.UtilityModel
    {
        public Int64 PatientID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string GenderStr { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string Pincode { get; set; }
        public DateTime DOB { get; set; }
        public string DOBStr { get; set; }
        public int age { get; set; }
        public bool IsActive { get; set; }
        public string ClinicName { get; set; }
        public string BloodGroup { get; set; }
        public string FullName { get; set; }
        public string DoctorsName { get; set; }

        public string TimeSpentstr { get; set; }
        public Int64 VitalID { get; set; }
        public Int64 BloodPressure { get; set; }
        public int Height { get; set; }
        public int Pain { get; set; }
        public int Respiration { get; set; }
        public decimal Temperature { get; set; }
        public int Weight { get; set; }
        public int ChectCircumference { get; set; }
        public int Pulse { get; set; }
        public int PulseOximetry { get; set; }
        public Int64 MedicationID { get; set; }
        public string MedicationName { get; set; }
        public string MedicationDescription { get; set; }

        public static string GetPatientDataForList_SP = "SP_GetPatientData_Forlist";
        public static string SavePatientDetails_SP = "SP_InsertPatientData";
        public static string GetPatientProfileData_SP = "SP_GetPatientProfileData";
        public static string GetPatientDataByPatientID_SP = "SP_GetPatientDataByPatientID";
        public static string InsertVitalData_SP = "SP_AddVitalData";
        public static string GetVitalDataForList_SP = "SP_GetVitalDataForList";
        public static string InsertMedicationData_SP = "SP_AddMedicationData";
        public static string GetMedicationDataByPatientID_SP = "SP_GetMedicationDataByPatientID";

        public List<PatientModel> GetPatientData_ForList()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CurrentPage", CurrentPage);
            parameters.Add("@NumberOfRecords", NumberOfRecords);
            parameters.Add("@OrderBy", OrderBy);
            return TransformPatientData(DBManager.GetData(GetPatientDataForList_SP, parameters));
        }
        public List<PatientModel> TransformPatientData(DataTable data)
        {
            List<PatientModel> patientList = new List<PatientModel>();
            if (data.Rows.Count > 0)
            {
                foreach (var item in data.AsEnumerable())
                {
                    PatientModel obj = new PatientModel();
                    obj.PatientID = Convert.ToInt64(item["NUMBER"]);
                    obj.FullName = Convert.ToString(item["Name"]);
                    obj.TimeSpentstr = Convert.ToString(item["TimeSpent"]);
                    obj.Gender = Convert.ToInt32(item["Gender"]);
                    obj.GenderStr = (obj.Gender == 1) ? "Male" : "Female";
                    obj.DOBStr = Convert.ToString(item["DOB"]);
                    obj.BloodGroup = Convert.ToString(item["BloodGroup"]);
                    obj.age = Convert.ToInt32(item["Age"]);
                    obj.TotalCount = Convert.ToInt16(item["TotalCount"]);
                    patientList.Add(obj);
                }
            }
            return patientList;
        }

        public Int64 SavePatientDetails()
        {
            Int64 RecordID = -1;
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@PatientID", PatientID);
            parameters.Add("@FirstName", FirstName);
            parameters.Add("@MiddleName", MiddleName);
            parameters.Add("@LastName", LastName);
            parameters.Add("@Gender", Gender);
            parameters.Add("@DOB", DOB);
            parameters.Add("@Age", age);
            parameters.Add("@MobileNumber", MobileNumber);
            parameters.Add("@AddressLine1", Address);
            parameters.Add("@CityID", CityName);
            parameters.Add("@CountryID", CountryName);
            parameters.Add("@StateID", StateName);
            parameters.Add("@Pincode", Pincode);
            parameters.Add("@ClinicName", ClinicName);
            parameters.Add("@BloodGroup", BloodGroup);
            parameters.Add("@DoctorsName", DoctorsName);
            DBManager.CreateUpdateData(SavePatientDetails_SP, parameters, out RecordID);
            return RecordID;
        }
        internal PatientModel GetPatientDataByPatientId()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@PatientID", PatientID);
            return TransformPatientDataByPatientID(DBManager.GetData(GetPatientDataByPatientID_SP, parameters));
        }

        private PatientModel TransformPatientDataByPatientID(DataTable dataTable)
        {
            PatientModel obj = new PatientModel();

            if (dataTable.Rows.Count > 0)
            {
                obj.PatientID = Convert.ToInt64(dataTable.Rows[0]["PatientID"]);
                obj.FullName = Convert.ToString(dataTable.Rows[0]["Name"]);
                obj.Address = Convert.ToString(dataTable.Rows[0]["address"]);
                obj.Gender = Convert.ToInt32(dataTable.Rows[0]["Gender"]);
                obj.GenderStr = (obj.Gender == 1) ? "Male" : "Female";
                obj.DOBStr = Convert.ToString(dataTable.Rows[0]["DOB"]);
                obj.BloodGroup = Convert.ToString(dataTable.Rows[0]["BloodGroup"]);
                obj.age = Convert.ToInt32(dataTable.Rows[0]["Age"]);
                obj.MobileNumber = Convert.ToString(dataTable.Rows[0]["MobileNumber"]);
                obj.MedicationName = Convert.ToString(dataTable.Rows[0]["MedicationName"]);
                obj.MedicationDescription = Convert.ToString(dataTable.Rows[0]["MedicationDescription"]);
            }
            return obj;
        }

        internal PatientModel GetPatientMedicationData()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@PatientID", PatientID);
            return TransformPatientMedicationData(DBManager.GetData(GetMedicationDataByPatientID_SP, parameters));
        }

        private PatientModel TransformPatientMedicationData(DataTable dataTable)
        {
            PatientModel obj = new Models.PatientModel.PatientModel();
            obj.PatientID = Convert.ToInt64(dataTable.Rows[0]["PatientID"]);
            obj.MedicationID = Convert.ToInt64(dataTable.Rows[0]["MedicationID"]);
            obj.MedicationName = dataTable.Rows[0]["MedicationName"].ToString();
            obj.MedicationDescription = dataTable.Rows[0]["MedicationDescription"].ToString();
            return obj;
        }
        internal List<PatientModel> GetPatientProfileData()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@ClinicName", ClinicName);
            return TransformPatientProfileData(DBManager.GetData(GetPatientProfileData_SP, parameters));
        }

        private List<PatientModel> TransformPatientProfileData(DataTable dataTable)
        {
            List<PatientModel> patientList = new List<PatientModel>();
            if (dataTable.Rows.Count > 0)
            {
                foreach (var item in dataTable.AsEnumerable())
                {
                    PatientModel obj = new PatientModel();
                    obj.PatientID = Convert.ToInt64(item["PatientID"]);
                    obj.FullName = Convert.ToString(item["Name"]);
                    obj.Address = Convert.ToString(item["address"]);
                    obj.Gender = Convert.ToInt32(item["Gender"]);
                    obj.GenderStr = (obj.Gender == 1) ? "Male" : "Female";
                    obj.DOBStr = Convert.ToString(item["DOB"]);
                    obj.BloodGroup = Convert.ToString(item["BloodGroup"]);
                    obj.age = Convert.ToInt32(item["Age"]);
                    obj.MobileNumber = Convert.ToString(item["MobileNumber"]);
                    patientList.Add(obj);
                }
            }
            return patientList;
        }

        internal long SaveVitalDetails()
        {
            Int64 RecordID = -1;
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@VitalID", VitalID);
            parameters.Add("@PatientID", PatientID);
            parameters.Add("@BloodPressure", BloodPressure);
            parameters.Add("@Height", Height);
            parameters.Add("@Pain", Pain);
            parameters.Add("@Respiration", Respiration);
            parameters.Add("@Temperature", Temperature);
            parameters.Add("@Weight", Weight);
            parameters.Add("@ChectCircumference", ChectCircumference);
            parameters.Add("@Pulse", Pulse);
            parameters.Add("@PulseOximetry", PulseOximetry);

            DBManager.CreateUpdateData(InsertVitalData_SP, parameters, out RecordID);
            return RecordID;
        }

        internal long SaveMedicationDetails()
        {
            Int64 RecordID = -1;
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@MedicationID", MedicationID);
            parameters.Add("@PatientID", PatientID);
            parameters.Add("@MedicationName", MedicationName);
            parameters.Add("@MedicationDescription", MedicationDescription);
            DBManager.CreateUpdateData(InsertMedicationData_SP, parameters, out RecordID);
            return RecordID;
        }

        public List<PatientModel> GetVitalData_ForList()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CurrentPage", CurrentPage);
            parameters.Add("@NumberOfRecords", NumberOfRecords);
            parameters.Add("@OrderBy", OrderBy);
            parameters.Add("@PatientID", PatientID);
            return TransformVitalData(DBManager.GetData(GetVitalDataForList_SP, parameters));
        }

        private List<PatientModel> TransformVitalData(DataTable dataTable)
        {
            List<PatientModel> patientVitalList = new List<PatientModel>();
            if (dataTable.Rows.Count > 0)
            {
                foreach (var item in dataTable.AsEnumerable())
                {
                    PatientModel obj = new PatientModel();
                    obj.VitalID = Convert.ToInt64(item["Number"]);
                    obj.BloodPressure = Convert.ToInt64(item["BloodPressure"]);
                    obj.Pulse = Convert.ToInt32(item["Pulse"]);
                    obj.Temperature = Convert.ToDecimal(item["Temperature"]);
                    obj.PulseOximetry = Convert.ToInt32(item["PulseOximetry"]);
                    obj.Height = Convert.ToInt32(item["Height"]);
                    obj.Weight = Convert.ToInt32(item["Weight"]);
                    obj.Pain = Convert.ToInt32(item["Pain"]);
                    obj.Respiration = Convert.ToInt32(item["Respiration"]);
                    obj.PulseOximetry = Convert.ToInt32(item["PulseOximetry"]);
                    obj.ChectCircumference = Convert.ToInt32(item["ChectCircumference/Girth"]);
                    obj.TotalCount = Convert.ToInt16(item["TotalCount"]);
                    patientVitalList.Add(obj);
                }
            }
            return patientVitalList;
            throw new NotImplementedException();
        }
    }
}