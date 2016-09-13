using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GateManager.Controllers
{
    public class GateController : Controller
    {
        // GET: Gate
        public ActionResult Index()
        {
            return View();
        }
    }
}