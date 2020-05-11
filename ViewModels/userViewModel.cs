using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModels
{
    public class userViewModel
    {
        public int korisnikID { get; set; }

        public int ulogaID { get; set; }

        public string username { get; set; }

        public string lozinka{ get; set; }
        
        public string email { get; set; }

        public string ime { get; set; }

        public string prezime { get; set; }

        public int mjestoID { get; set; }

        public int timID { get; set; }

        public string Avatar { get; set; }

        public string UlogaNaziv { get; set; }

        public string nazivTim { get; set; }

    }
}