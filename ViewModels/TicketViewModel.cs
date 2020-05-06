using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bug_Tracker.Models;

namespace Bug_Tracker.ViewModels
{
    public class TicketViewModel
    {
        public TicketViewModel()
        {

        }
        public TicketViewModel(IEnumerable<Priority> Priorities, Ticket Ticket)
        {
            this.Ticket = Ticket;
            this.Priorities = Priorities;

        }

        public IEnumerable<Priority> Priorities { get; set; }
        public Ticket Ticket { get; set; }


    }


}