using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Bug_Tracker.Models
{
    public class DataBaseContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Priority> Priorities { get; set; }
    

    
    }
}