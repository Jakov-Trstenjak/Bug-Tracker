using Dapper;
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

namespace Bug_Tracker.Models
{
    
    public static class DapperORM
    {

        public static  List<string> GetPriorityNames()
        {
            var priorityNames = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var priority = connection.Query<string>("SELECT  \"nazivPrioritet\" FROM Public.\"Prioritet\"");
                priorityNames = priority.ToList();
            };

            return priorityNames;
            
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