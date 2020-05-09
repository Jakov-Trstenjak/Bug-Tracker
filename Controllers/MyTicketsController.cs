using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bug_Tracker.Controllers
{
    public class MyTicketsController : Controller
    {
        // GET: MyTickets
        public ActionResult Edit(int ID)
        {
            return View();
        }
  
        //GET: User/Heklijo/MyTickets/TicketDetails/5
        public ActionResult TicketDetails(int ID)
        {
            return View();
        }
    }

}