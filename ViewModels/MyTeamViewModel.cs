using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.ViewModels
{
    public class MyTeamViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string roleID { get; set; }

        public string roleName { get; set; }

        public string Avatar { get; set; }

        public int TicketsCommitted { get; set; }

        public string teamName { get; set; }
    }

}