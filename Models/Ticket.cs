using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Bug_Tracker.Models
{
    public class Ticket
    {

        
        public int TicketID { get; set; }

        [Display(Name ="Title")]
        public string TicketTitle { get; set; }

        public  string UserID { get; set; }
        

        [Display(Name = "Describe occured problem")]
        public string Description { get; set; }

        public string Date { get; set; }

        public string ImageURL { get; set; }

        public string BugID { get; set; }

        public string Priority { get; set; }


    }
}