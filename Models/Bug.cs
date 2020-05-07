﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace Bug_Tracker.Models
{
    public class Bug
    {
        static int nextID;

        public  int BugID { get; private set; }
    
        [Required]
        public string Description { get; set; }

        public  int PriorityID { get; set; }

        // auto-generate BugID
        public Bug()
        {
            BugID = Interlocked.Increment(ref nextID);
        }
  
  

    }
}