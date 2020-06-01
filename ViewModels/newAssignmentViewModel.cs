using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModels
{
    public class newAssignmentViewModel
    {

        public Ticket Ticket { get; set; }
        public string username { get; set; }

        public int zadatakID { get; set; }

        public string zadatakIme { get; set; }


        public int timID { get; set; }

        public string rokDo { get; set; }

       

        public string zadatakOpis { get; set; }
    }
}