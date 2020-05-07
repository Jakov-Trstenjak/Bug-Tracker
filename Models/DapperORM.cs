﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Npgsql;
using System.Web.UI.WebControls.WebParts;
using System.Web.Mvc.Ajax;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace Bug_Tracker.Models
{
    
    public static class DapperORM
    {
       
        public static  List<Priority> GetPriority()
        {
            var priorityNames = new List<Priority>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var priorityName = connection.Query<string>("SELECT  \"nazivPrioritet\" FROM Public.\"Prioritet\"");
                
                priorityName = priorityName.ToList();
              
                for(int i=0; i < 4; ++i)
                {
                    Priority prior = new Priority
                    {
                        PriorityID = i + 1,
                        PriorityName = priorityName.ElementAt(i)
                    };
                    priorityNames.Add(prior);

                }
            };

            return priorityNames;
            
        }

        public static string getUsername()
        {
            string username = "";
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var user = HttpContext.Current.User;
                var usernames   = connection.Query<string>("SELECT \"username\" FROM Public.\"Korisnik\" WHERE email = '"+ user.Identity.GetUserName()+"'");
                username = usernames.First();
            };

            return username;
        }

        public static void getTeamData()
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                var priority = connection.Query<Team>("SELECT timID, nazivTIM, projektID FROM public.\"Tim\"");
            };
        }

        public static void getTicketData()
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                var priority = connection.Query<Ticket>("SELECT listićID, korisnikID, opisProblema, slika, datum, pogreškaID FROM public.\"Listić\"");
            };
        }


    }

}