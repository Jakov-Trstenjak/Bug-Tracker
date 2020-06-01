using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace Bug_Tracker.Models
{
    public class Bug
    {
        static int nextID=DapperORM.getTicketCount();

        public  int BugID { get; private set; }
    
        [Required]
        public string Description { get; set; }

        [Display(Name = "Prioritet")]
        public  int PriorityID { get; set; }

        // auto-ge nerate BugID
        public Bug()
        {
            BugID = Interlocked.Increment(ref nextID);
        }
  
  

    }
}