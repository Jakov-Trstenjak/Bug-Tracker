using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker.ViewModels;

namespace Bug_Tracker.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewTicket()
        {
            var ticketViewModel = new NewTicketViewModel { 
            };
            ticketViewModel.addPriorities();
            
            return View(ticketViewModel);
        }


    }
}