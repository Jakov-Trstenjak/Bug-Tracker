using Bug_Tracker.Models;
using Bug_Tracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bug_Tracker.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ActionResult ViewProjects(string username)
        {
            List<ProjectViewModel> allProjects = DapperORM.getProjects();

            return View(allProjects);
        }
    }
}