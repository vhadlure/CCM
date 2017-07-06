using Clinic.Models.Dashboard;
using Clinic.Models.LoginModel;
using Clinic.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()      
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            LoginModel loginuser = (LoginModel)Session["User"];
            DashboardModel model = new DashboardModel();
            model = model.GetDashboardData();
            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}