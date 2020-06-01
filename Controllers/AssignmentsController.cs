using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker.ViewModels;
using Bug_Tracker.Models;

namespace Bug_Tracker.Controllers
{
    public class AssignmentsController : Controller
    {
        // GET: Assignments
        private static int currentID = 0;

        [HttpGet]
        public ActionResult assignTicketToTeam(int id)
        {
            MyTicketViewModel model = DapperORM.getSingleTicketData(id);
            model.teams = DapperORM.getTeamData();

            currentID = id;
            var assignment = new AssignmentViewModel
            {
                Ticket = model,
                teams = DapperORM.getTeamData()
            };
            return View(assignment);
        }

        [HttpPost]
        public ActionResult assignTicketToTeam(newAssignmentViewModel assignment)
        {
            assignment.username = DapperORM.getUsername();
            assignment.zadatakID = currentID;
            
            if(assignment.zadatakIme==null || assignment.rokDo==null)
            {
                assignTicketToTeam(currentID);
            }

            DapperORM.saveAssignment(assignment);
            return RedirectToAction("addAssignments", "Assignments");
        }

        public ActionResult addAssignments()
        {
            List<MyTicketViewModel> tickets = DapperORM.getAllTickets();
            return View(tickets);
        }
    }
}