using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bug_Tracker.Models;

namespace Bug_Tracker.ViewModels
{
    public class NewTicketViewModel
    {
        public  Ticket Ticket;
        public List<String> Priorities = new List<String>();

        

        public void addPriorities()
        {
            Priorities.Add("Niski");
            Priorities.Add("Srednji");
            Priorities.Add("Visoki");
            Priorities.Add("Vrlo visoki");
        }
    }
}