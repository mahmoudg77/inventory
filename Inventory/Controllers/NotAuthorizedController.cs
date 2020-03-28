using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class NotAuthorizedController : Controller
    {
 
        // GET: NotAuthorized
        public ActionResult Index()
        {
            return View();
        }

    }
}
