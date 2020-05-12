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
        [HttpGet]
        public ActionResult EditUser(string username)
        {
            userViewModel user =DapperORM.GetSingleUser(username);
            user.teams = DapperORM.getTeamData();
            user.roles = DapperORM.getRoleData();
            return View(user);
        }
            
        [HttpPost]
        public ActionResult EditUser(userViewModel model)
        {
            DapperORM.updateUser(model);
            return RedirectToAction("ManageUsers", "User", new { id = DapperORM.getUsername()});
        }


    }
}