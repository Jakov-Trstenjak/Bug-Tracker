using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace Bug_Tracker.Models
{
    public class Priority
    {
        [Required]
        public int PriorityID { get; set; }

        [Display(Name ="Prioritet")]
        public string   PriorityName { get; set; }
    }
}