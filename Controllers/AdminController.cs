using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker.ViewModels;

namespace Bug_Tracker.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult EditUser(int id)
        {
            userViewModel user =DapperORM.GetSingleUser(id);
            return View(user);
        }
    }
}