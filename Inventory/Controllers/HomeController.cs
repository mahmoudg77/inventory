using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    [LoginFilter]
    public class HomeController : Controller
    {

        //public ActionResult Index()
        //{
        //    return View();
        //}

        private InventoryEntities db = new InventoryEntities();
        public ActionResult Index()
        {
            Dashboard dashboard = new Dashboard();

            dashboard.assets_count = db.Assets.Count();
            dashboard.users_count = db.Users.Count();
            dashboard.requests_app_count = db.Requests.Count(g => g.Req_Status == 1);
            dashboard.requests_pen_count = db.Requests.Count(g => g.Req_Status == -1);
            dashboard.requests_rej_count = db.Requests.Count(g => g.Req_Status == 0);
            dashboard.ast_pc_count = db.Assets.Count(g => g.Ast_Type == "PC");
            dashboard.ast_ip_count = db.Assets.Count(g => g.Ast_Type == "IP PHONE");
            dashboard.ast_kp_count = db.Assets.Count(g => g.Ast_Type == "KEYPOARD");
            dashboard.ast_lp_count = db.Assets.Count(g => g.Ast_Type == "LAPTOP");
            dashboard.ast_ma_count = db.Assets.Count(g => g.Ast_Type == "MAUSE");
            dashboard.ast_tp_count = db.Assets.Count(g => g.Ast_Type == "TABLET");

            



            return View(dashboard);
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