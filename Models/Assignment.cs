using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Assignment
    {
        public int korisnikID { get; set; }

        public int zadatakID { get; set; }

        public string zadatakIme { get; set; }


        public int timID { get; set; }


        public int rokID { get; set; }

        public string zadatakOpis { get; set; }
    }
}