using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModels
{
    public class MyTicketViewModel
    {
        public int listićID { get; set; }

        [Display(Name = "Submitter")]
        public string Username { get; set; }


        public string slika { get; set; }


        [Display(Name = "Datum stvaranja listića")]
        public string datum { get; set; }

        [Display(Name = "Naziv listića")]
        public string listićIme{ get; set; }

        [Display(Name = "Status")]
        public string statusName { get; set; }

        [Display(Name = "Opis pogreške")]
        public string opisPogreška { get; set; }

        [Display(Name = "Prioritet")]
        public string nazivPrioritet { get; set; }
        
        public string nazivProjekta { get; set; }

        public List<Team> teams { get; set; }

        public int timID { get; set; }

        public string nazivTim { get; set; }
    }
}