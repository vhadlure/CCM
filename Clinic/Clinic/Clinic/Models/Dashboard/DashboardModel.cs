using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Clinic.Models.Dashboard
{
    public class DashboardModel : Clinic.Models.UtilityModel.UtilityModel
    {
        public int TotalClinic { get; set; }
        public int TotalPatient { get; set; }
        public int TotalUser { get; set; }

        public static string GetDashboardData_SP = "SP_GetDashboardData";

        public DashboardModel GetDashboardData()
        {
            Dictionary<object, object> parameters = new Dictionary<object, object>();
            parameters.Add("@CreatedBy", CreatedBy);
            return TransformDashboardData(DBManager.GetData(GetDashboardData_SP, parameters));
        }

        public DashboardModel TransformDashboardData(DataTable data)
        {
            DashboardModel obj = new DashboardModel();
            obj.TotalClinic = Convert.ToInt16(data.Rows[0]["TotalClinic"]);
            obj.TotalUser = Convert.ToInt16(data.Rows[0]["TotalUser"]);
            obj.TotalPatient = Convert.ToInt16(data.Rows[0]["TotalPatient"]);
            return obj;
        }
    }
}