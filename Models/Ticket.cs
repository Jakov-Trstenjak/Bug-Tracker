using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using Microsoft.AspNet.Identity;

namespace Bug_Tracker.Models
{
    public class Ticket
    {

        static int nextID;
        public int TicketID { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [Display(Name ="Title")]
        public string TicketTitle { get; set; }

        public  string UserID { get; set; }
        

        public string Time { get; set; }

        public string ImageURL { get; set; }

        public Bug Bug { get; set; }

        public Ticket()
        {
            TicketID = Interlocked.Increment(ref nextID);
            Time = DateTime.Now.ToString(@"dd\/MM\/yyyy h\:mm tt");
            var user = HttpContext.Current.User;
            UserID = user.Identity.GetUserName();
        }
    }
}