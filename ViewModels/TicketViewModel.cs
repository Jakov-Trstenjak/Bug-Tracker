using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bug_Tracker.Models;

namespace Bug_Tracker.ViewModels
{
    public class TicketViewModel
    {
        public IEnumerable<string> priorityNames { get; set; }
        public Ticket ticket { get; set; }


    }
}