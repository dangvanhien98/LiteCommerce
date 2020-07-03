using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteComemerce.Admin.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Input(int id = 0)
        {
            if(id != 0)
            {
                ViewBag.Title = "Create new Reports";
            }
            else
            {
                ViewBag.Title = "Edit a Reports";
            }
            return View();
        }

    }
}