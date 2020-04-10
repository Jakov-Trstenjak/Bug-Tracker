using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    [Table("Prioritet",Schema ="public")]
    public class Priority
    {
        [Key]
        public int PriorityID { get; set; }

        public string PriorityName { get; set; }
    }
}