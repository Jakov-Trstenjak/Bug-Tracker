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
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int MyProperty { get; set; }
        public byte Gender { get; set; }
        public int MjestoID { get; set; }
        public int TeamID { get; set; }
        public int ProjectID { get; set; }
    }

}