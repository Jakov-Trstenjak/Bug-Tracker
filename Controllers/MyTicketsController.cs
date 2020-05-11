using Bug_Tracker.Models;
using Bug_Tracker.ViewModels;
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
        public ActionResult TicketDetails(int id)
        {
            MyTicketViewModel model = DapperORM.getSingleTicketData(id);
            return View(model);
        }

        public ActionResult allTicketsForProject(int id)
        {
            List<MyTicketViewModel> model = DapperORM.getAllTickets(id);
            return View(model);
        }
    }

}