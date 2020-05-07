using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bug_Tracker.Models;

namespace Bug_Tracker.ViewModels
{
    public class TicketViewModel

    {

        public IEnumerable<Priority> Priorities { get; set; }
        public Ticket Ticket { get; set; }


        public TicketViewModel()
        {

        }


    }


}