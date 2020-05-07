using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModels
{
    public class MyTicketViewModel
    {
        int listićID { get; set; }

        string Username { get; set; }


        string slika { get; set; }

        string datum { get; set; }

        string listićIme{ get; set; }
      
        string opisPogreška { get; set; }

        string nazivPrioritet { get; set; }

    }
}