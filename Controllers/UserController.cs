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
        //dohvati iz baze podataka Username trenutnog korisnika.
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Dashboard(string Username)
        {
            return View();
        }
        public ActionResult MyTickets(string Username)
        {
            return View();
        }

        public ActionResult MyTeam(string Username)
        {
            return View();
        }

        public ActionResult UserProfile(string Username)
        {
            return View();
        }

        public ActionResult ReportABug()
        {
            var ticketViewModel = new TicketViewModel {
                priorityNames = DapperORM.GetPriorityNames(),
                ticket = new Ticket()
                
            };
           
           
            return View(ticketViewModel);
        }




    }

}