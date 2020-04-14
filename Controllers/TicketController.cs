using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bug_Tracker.ViewModels;
using System.Data.Entity;

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
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewTicketViewModel viewModel)
        {
            return View();

        }


    }
}