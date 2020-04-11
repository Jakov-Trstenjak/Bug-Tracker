using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class User
    {
        public User()
        {

        }

        public int UserID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

    }
}