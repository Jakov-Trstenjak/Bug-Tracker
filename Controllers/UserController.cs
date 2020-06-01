using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker.Models;
using Bug_Tracker.ViewModels;

namespace Bug_Tracker.Controllers
{
   
    public class UserController : Controller
    {
        private string username = DapperORM.getUsername();
        //dohvati iz baze podataka Username trenutnog korisnika.
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Dashboard(string Username)
        {
            List<int> listOfTicketPriorities = DapperORM.getNumberOfTicketPrioritiesForDashboard();
            return View(listOfTicketPriorities);
        }
        public ActionResult MyTickets(string Username)
        {
             List<MyTicketViewModel> myTickets = DapperORM.getTicketData();

            return View(myTickets);
        }

        public ActionResult MyTeam(string Username)
        {
            var teamData = DapperORM.getMyTeamData(username);

            return View(teamData);
           
        }

        [HttpGet]
        public ActionResult UserProfile(string Username)
        {
            userViewModel currentUser = DapperORM.GetSingleUser(username);
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult UserProfile(userViewModel model)
        {
            DapperORM.updateUser(model);
            return RedirectToAction("UserProfile", "User", new { id =username });
        }


        public ActionResult ManageUsers(string Username)
        {
            List<userViewModel> users = DapperORM.GetUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult ReportABug(string username)
        {
            var ticketViewModel = new TicketViewModel {
                Priorities = DapperORM.GetPriority(),
                Projects = DapperORM.GetProjectsForReportABug(),
                Ticket = new Ticket()
                
            };

         
            return View(ticketViewModel);
        }

        [HttpPost]
        public ActionResult CreateTicket(Ticket Ticket)
        {
            if (!ModelState.IsValid)
            {
                var ticketViewModel = new TicketViewModel
                {
                    Priorities = DapperORM.GetPriority(),
                    Projects = DapperORM.GetProjectsForReportABug(),
                    Ticket = Ticket
                };
                return View("ReportABug", ticketViewModel);
            }
            DapperORM.saveTicket(Ticket);
            
            return RedirectToAction("MyTickets", "User", new { id =username });

        }
        
        public  ActionResult Assignments()
        {

            List<AssignmentViewModel> tickets = DapperORM.getAssignments(DapperORM.getUsername());
            return View(tickets);
        }

    



    }

}