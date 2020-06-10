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

        static int nextTicketID = DapperORM.getTicketCount();
        public int TicketID { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [Display(Name ="Naziv")]
        public string TicketTitle { get; set; }

        public  string UserID { get; set; }

        [Display(Name = "Vrijeme")]
        public string Time { get; set; }

        [Display(Name = "URL slike")]
        public string ImageURL { get; set; }

        [Required]
        [Display(Name ="Projekt")]
        public int projektID { get; set; }

        public Bug Bug { get; set; }

        public int teamID { get; set; }


        public Ticket()
        {
            Time = DateTime.Now.ToString(@"dd\/MM\/yyyy h\:mm tt");
            var user = HttpContext.Current.User;
            UserID = user.Identity.GetUserName();
        }
    }
}