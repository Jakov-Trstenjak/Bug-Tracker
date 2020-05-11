using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Projekt
    {
        public int projektID { get; set; }
        public string opisProjekta { get; set; }
        public string nazivProjekta { get; set; }
        public string datumPokretanja { get; set; }
    }
}