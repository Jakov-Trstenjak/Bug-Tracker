using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Team
    {

        public int teamID { get; set; }

        public string teamName { get; set; }

        public int projectID { get; set; }
    }
}