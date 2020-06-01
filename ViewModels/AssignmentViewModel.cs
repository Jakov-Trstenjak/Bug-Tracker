using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Bug_Tracker.Models;

namespace Bug_Tracker.ViewModels
{
    public class AssignmentViewModel
    {


        public int managerID{ get; set; } 

        public int zadatakID { get; set; }


        [Display(Name = "Naziv zadatka")]
        public string zadatakIme { get; set; }


        [Display(Name = "Opis zadatka")]
        public string zadatakOpis { get; set; }


        [Display(Name ="Tim")]
        public string nazivTim { get; set; }

        public MyTicketViewModel Ticket { get; set; }

        public int rokID { get; set; }

        

   
        public List<Team> teams { get; set; }
        
        
        [Display(Name = "Timovi")]
        public int timID { get; set; }

        [Display(Name = "Rok ispravljanja pogreške")]
        public string datum { get; set; }

        public string Submitter { get; set; }
    }
}