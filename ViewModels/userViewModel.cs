using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Bug_Tracker.Models;

namespace Bug_Tracker.ViewModels
{
    public class userViewModel
    {
        public int korisnikID { get; set; }

        [Display(Name = "Uloga")]
        public int ulogaID { get; set; }


        [Display(Name = "Username")]
        public string username { get; set; }

        [Display(Name = "Lozinka")]
        public string lozinka{ get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "Ime")]
        public string ime { get; set; }

        [Display(Name = "Prezime")]
        public string prezime { get; set; }


        public int mjestoID { get; set; }

        [Display(Name = "Tim")]
        public int timID { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        [Display(Name = "Uloga")]
        public string UlogaNaziv { get; set; }

        [Display(Name = "Tim")]
        public string nazivTim { get; set; }

        public List<Team> teams { get; set; }

        public List<Role> roles { get; set; }

    }
}