using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//SELECT        "nazivPrioritet"
//FROM Public."Prioritet"

namespace Bug_Tracker.Models
{
    public class Priority
    {
        public int PriorityID { get; set; }
        public string   PriorityName { get; set; }
    }
}