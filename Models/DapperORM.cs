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
using Microsoft.AspNet.Identity;
using Bug_Tracker.ViewModels;
using System.Data.Entity.Core.Objects;

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

        public static List<Projekt> GetProjectsForReportABug()
        {
            var projectNames = new List<Projekt>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                projectNames = (List<Projekt>) connection.Query<Projekt>("SELECT  \"nazivProjekta\",\"projektID\" FROM Public.\"Projekt\"");
            }
            return projectNames;

        }



        public static int getUserCount()
        {
            int counter;
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var user = HttpContext.Current.User;
                var numberOfTickets = connection.Query<int>("SELECT COUNT(*) FROM public.\"Korisnik\"");
                counter = numberOfTickets.First();
            };

            return counter;
        }

        public static int getTicketCount()
        {
            int counter;
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var user = HttpContext.Current.User;
                var numberOfTickets = connection.Query<int>("SELECT COUNT(*) FROM public.\"Listić\"");
                counter = numberOfTickets.First();
            };

            return counter;
        }


        public static string getUsername()
        {
            string username = "";
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var user = HttpContext.Current.User;
                var usernames   = connection.Query<string>("SELECT \"username\" FROM Public.\"Korisnik\" WHERE email = '"+ user.Identity.GetUserName()+"'");
                if (usernames.Any())
                {
                    username = usernames.First();
                }
            };

            return username;
        }

        public static void saveTicket(Ticket Ticket)
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                var user = HttpContext.Current.User;
                var listOfUserID = connection.Query<int>("SELECT \"korisnikID\" FROM public.\"Korisnik\" WHERE \"email\"='"+user.Identity.GetUserName()+"'");
                int userID = 0;
                
                foreach (var item in listOfUserID)
                {
                    userID = item;
                }
                
                
                var saveBug = connection.Query<string>("INSERT INTO public.\"Pogreška\" VALUES("+ Ticket.Bug.BugID+",'"+Ticket.Bug.Description+"',"+Ticket.Bug.PriorityID+")");
                var saveTicket = connection.Query<Team>("INSERT INTO public.\"Listić\" VALUES(" +Ticket.Bug.BugID +"," +userID+",'"+Ticket.ImageURL+"',"+Ticket.Bug.BugID+",'"+Ticket.TicketTitle+"','"+Ticket.Time+"',"+Ticket.projektID+")");
                System.Diagnostics.Debug.WriteLine("hi");
            };
        }
        public static void getTeamData()
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                var priority = connection.Query<Team>("SELECT timID, nazivTIM, projektID FROM public.\"Tim\"");
            };
        }

        public static List<ProjectViewModel> getProjects()
        {
            var listOfProjects = new List<ProjectViewModel>();
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                listOfProjects = (List<ProjectViewModel>)connection.Query<ProjectViewModel>(@"SELECT ""Projekt"".""projektID"",""Projekt"".""opisProjekta"",""Projekt"".""nazivProjekta"",""Projekt"".""datumPokretanja"", count(""listićID"") AS ""brojListica""
                                     FROM ""Projekt"" LEFT JOIN ""Listić""  
                                     ON ""Projekt"".""projektID"" =""Listić"".""projektID"" 
                                     GROUP BY ""Projekt"".""projektID""
                                     ");
                                    
            };

            listOfProjects.Reverse();
            return listOfProjects;
        }





        public static List<MyTicketViewModel> getAllTickets(int projectId)
        {
            List<MyTicketViewModel> myTickets;

            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                var user = HttpContext.Current.User;
                string query = @"SELECT ""Listić"".""listićID"",  ""Korisnik"".""username"",""Listić"".""slika"",""Listić"".""datum"",""Listić"".""listićIme"", ""Pogreška"".""opisPogreška"",""Prioritet"".""nazivPrioritet"",""Projekt"".""nazivProjekta""
                                     FROM ""Listić"" INNER JOIN ""Pogreška""  
                                     ON ""Listić"".""pogreskaID"" = ""Pogreška"".""pogreskaID""
                                     INNER JOIN ""Prioritet""
                                     ON ""Pogreška"".""prioritetID"" = ""Prioritet"".""prioritetID""
                                     INNER JOIN ""Korisnik""
                                     ON ""Listić"".""korisnikID"" = ""Korisnik"".""korisnikID""
                                     INNER JOIN ""Projekt""
                                     ON ""Projekt"".""projektID"" = ""Listić"".""projektID""
                                     WHERE ""Listić"".""projektID""="+projectId;
;

                myTickets = (List<MyTicketViewModel>)connection.Query<MyTicketViewModel>(query);

                //Reverse list so that the latest Tickets get displayed first
                myTickets.Reverse();


            };
            return myTickets;
        }
        public static List<MyTicketViewModel> getTicketData()
        {
            List<MyTicketViewModel> myTickets;

            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                var user = HttpContext.Current.User;
                string query = @"SELECT ""Listić"".""listićID"",  ""Korisnik"".""username"",""Listić"".""slika"",""Listić"".""datum"",""Listić"".""listićIme"", ""Pogreška"".""opisPogreška"",""Prioritet"".""nazivPrioritet"", ""Projekt"".""nazivProjekta""
                                     FROM ""Listić"" INNER JOIN ""Pogreška""  
                                     ON ""Listić"".""pogreskaID"" = ""Pogreška"".""pogreskaID""
                                     INNER JOIN ""Prioritet""
                                     ON ""Pogreška"".""prioritetID"" = ""Prioritet"".""prioritetID""
                                     INNER JOIN ""Korisnik""
                                     ON ""Listić"".""korisnikID"" = ""Korisnik"".""korisnikID""
                                     INNER JOIN ""Projekt""
                                     ON ""Projekt"".""projektID"" = ""Listić"".""projektID""  
                                     WHERE ""Korisnik"".""email""= '" + user.Identity.GetUserName()+"'" +
                                     "ORDER BY \"Listić\".\"datum\" DESC";

                myTickets= (List<MyTicketViewModel>)connection.Query<MyTicketViewModel>(query);
                
                //Reverse list so that the latest Tickets get displayed first

              
            
            };
            return myTickets;
        }
        //+ "AND \"Pogreška\".\"pogreskaID\"="+id

        public static MyTicketViewModel getSingleTicketData(int id)
        {
            List<MyTicketViewModel> myTickets;

            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                var user = HttpContext.Current.User;
                string query = @"SELECT ""Listić"".""listićID"",  ""Korisnik"".""username"",""Listić"".""slika"",""Listić"".""datum"",""Listić"".""listićIme"", ""Pogreška"".""opisPogreška"",""Prioritet"".""nazivPrioritet"",""Projekt"".""nazivProjekta""
                                     FROM ""Listić"" INNER JOIN ""Pogreška""  
                                     ON ""Listić"".""pogreskaID"" = ""Pogreška"".""pogreskaID""
                                     INNER JOIN ""Prioritet""
                                     ON ""Pogreška"".""prioritetID"" = ""Prioritet"".""prioritetID""
                                     INNER JOIN ""Korisnik""
                                     ON ""Listić"".""korisnikID"" = ""Korisnik"".""korisnikID""
                                      INNER JOIN ""Projekt""
                                     ON ""Projekt"".""projektID"" = ""Listić"".""projektID""  
                                     WHERE ""Korisnik"".""email""= '" + user.Identity.GetUserName() + "'" + "AND \"Pogreška\".\"pogreskaID\"=" + id;

                myTickets = (List<MyTicketViewModel>)connection.Query<MyTicketViewModel>(query);

            };
            return myTickets.First();
        }
        public static void saveUser(User newUser)
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();

                string query = @"INSERT INTO public.""Korisnik"" (""Avatar"",""korisnikID"",""username"",""lozinka"",""email"") VALUES('"+ newUser.Avatar+"',"+ newUser.UserID+",'"+newUser.Username+"','"+ newUser.Password+"','"+ newUser.Email+"')";
                var saveUser = connection.Query<bool>(query);
                
            };
        }

        public static List<string> getRoles()
        {
            var roleNames = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var priorityName = connection.Query<string>("SELECT  \"nazivUloga\" FROM Public.\"Uloga\"");

            };

            return roleNames;
        }






    }

}