using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModels
{
    public class MyTicketViewModel
    {
        public int listićID { get; set; }

        public string Username { get; set; }


        public string slika { get; set; }

        public string datum { get; set; }

        public string listićIme{ get; set; }
      
        public string opisPogreška { get; set; }

        public string nazivPrioritet { get; set; }

    }
}