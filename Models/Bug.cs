using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Bug
    {
        public int BugID { get; set; }
    
        public string Description { get; set; }

        public int PriorityID { get; set; }
    
    }
}