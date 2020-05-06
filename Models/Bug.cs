using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace Bug_Tracker.Models
{
    public class Bug
    {
        static int nextID;

        public  int BugID { get; private set; }
    
        public string Description { get; set; }

        public  int PriorityID { get; set; }


        // auto-generate BugID
        Bug()
        {
            BugID = Interlocked.Increment(ref nextID);
        }
    
    }
}