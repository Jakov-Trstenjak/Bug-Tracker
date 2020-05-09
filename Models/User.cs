using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace Bug_Tracker.Models
{
    public class User
    {
        static int nextID = DapperORM.getUserCount();


        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    
        public string Avatar { get; set; }
        public int MjestoID { get; set; }
        public int TeamID { get; set; }



        public User()
        {
            UserID = Interlocked.Increment(ref nextID);
        }

    }

}