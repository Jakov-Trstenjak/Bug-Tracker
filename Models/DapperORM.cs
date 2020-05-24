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
using System.Drawing;

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
        public static List<Team> getTeamData()
        {
            var teams = new List<Team>();
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                teams = (List<Team>)connection.Query<Team>("SELECT * FROM public.\"Tim\"");
            };

            return teams;
        }

        public static List<string> getTeamNames()
        {
            var teamNames = new List<string>();
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                teamNames = (List<string>)connection.Query<string>("SELECT \"nazivTim\" FROM public.\"Tim\"");
            };

            return teamNames;
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

                string query = @"INSERT INTO public.""Korisnik"" (""Avatar"",""korisnikID"",""username"",""lozinka"",""email"",""timID"",""ulogaID"") VALUES('"+ newUser.Avatar+"',"+ newUser.UserID+",'"+newUser.Username+"','"+ newUser.Password+"','"+ newUser.Email+"',1,1)";
                var saveUser = connection.Query<bool>(query);
                
            };
        }

        public static List<string> getRoles()
        {
            var roleNames = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                roleNames = (List<string>)connection.Query<string>("SELECT  \"UlogaNaziv\" FROM Public.\"Uloga\"");

            };

            return roleNames;
        }

        public static List<Role> getRoleData()
        {
            var teams = new List<Role>();
            using (var connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=nikola000;Database=BugTrackerDB"))
            {
                connection.Open();
                teams = (List<Role>)connection.Query<Role>("SELECT * FROM public.\"Uloga\"");
            };

            return teams;
        }

        public static bool checkIfAdminOrManager(string Username)
        {
            bool isAdminOrManager = false;

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var AdminAndManagerUsers = connection.Query<string>("SELECT \"Korisnik\".\"username\" FROM \"Korisnik\" WHERE \"ulogaID\">1");

                if (AdminAndManagerUsers.Contains(Username))
                {
                    isAdminOrManager = true;
                }
            };

            return isAdminOrManager;
        }

        public static bool checkIfAdmin(string Username)
        {
            bool isAdminOrManager = false;

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var AdminAndManagerUsers = connection.Query<string>("SELECT \"Korisnik\".\"username\" FROM \"Korisnik\" WHERE \"ulogaID\">2");

                if (AdminAndManagerUsers.Contains(Username))
                {
                    isAdminOrManager = true;
                }
            };

            return isAdminOrManager;
        }




        public static List<userViewModel> GetUsers()
        {
            var users = new List<userViewModel>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var query = @" SELECT ""Korisnik"".*, ""nazivTim"" AS ""nazivTim"",""UlogaNaziv"" as ""UlogaNaziv"" 
                               FROM ""Korisnik"" 
                               INNER JOIN public.""Tim"" 
                               ON ""Korisnik"".""timID"" = ""Tim"".""timID""
                               INNER JOIN public.""Uloga""
                               ON ""Korisnik"".""ulogaID"" = ""Uloga"".""UlogaID""";
                users= (List<userViewModel>)connection.Query<userViewModel>(query);

            };

            return users;


        }

        public static userViewModel GetSingleUser(string username)
        {
            var users = new List<userViewModel>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var query = @" SELECT ""Korisnik"".*, ""nazivTim"" AS ""nazivTim"",""UlogaNaziv"" as ""UlogaNaziv"" 
                               FROM ""Korisnik"" 
                               INNER JOIN public.""Tim"" 
                               ON ""Korisnik"".""timID"" = ""Tim"".""timID""
                               INNER JOIN public.""Uloga""
                               ON ""Korisnik"".""ulogaID"" = ""Uloga"".""UlogaID""
                               WHERE ""Korisnik"".""username""="+"'"+username+"'";

                users = (List<userViewModel>)connection.Query<userViewModel>(query);

            };

            return users.First();


        }


        public static void updateUser(userViewModel updateUser)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var query = @"UPDATE ""Korisnik"" 
                              SET ""username"" = '" + updateUser.username + "',\"lozinka\"='" + updateUser.lozinka + "',\"email\"='" + updateUser.email + "',\"timID\"=" + updateUser.timID + ",\"Avatar\"='" + updateUser.Avatar + "',\"ulogaID\"=" + updateUser.ulogaID
                            + "WHERE \"korisnikID\"=" + updateUser.korisnikID;
     
                connection.Query<userViewModel>(query);

            };
        }

        public static string getImage(string username)
        {
            var imageURL = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                imageURL=(List<string>) connection.Query<string>("SELECT \"Avatar\" FROM \"public\".\"Korisnik\" WHERE username='"+ username+"'");

            };
            if(imageURL.Count>0)
            return imageURL.First();
        
            return "https://eu.ui-avatars.com/api/?name="+username+"&size=512&uppercase=false";
        }


        public static List<int> getNumberOfTicketPrioritiesForDashboard()
        {
            var listOfTicketPriorities = new List<int>();
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var query = @"SELECT  COUNT( ""prioritetID"") 
                          FROM ""Listić""
                          INNER JOIN ""Pogreška""
                          ON ""Listić"".""pogreskaID"" = ""Pogreška"".""pogreskaID""
                          GROUP BY ""prioritetID""";

              listOfTicketPriorities= (List<int>)connection.Query<int>(query);

            };

            return listOfTicketPriorities;
        }


        public static List<MyTeamViewModel> getMyTeamData(string username)
        {
            var myTeamData = new List<MyTeamViewModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var query = @"SELECT ""username"",""email"",""Avatar"",""UlogaID"" as ""roleID"",""UlogaNaziv"" as ""roleName"",COUNT(""listićID"") as ""TicketsCommitted"", ""nazivTim"" as ""teamName""
                              FROM ""Korisnik""
                              INNER JOIN ""Uloga""
                              on ""Korisnik"".""ulogaID"" = ""Uloga"".""UlogaID""
                              left JOIN ""Listić""
                              on ""Listić"".""korisnikID"" = ""Korisnik"".""korisnikID""
                              left JOIN ""Tim""
                              ON ""Tim"".""timID"" = ""Korisnik"".""timID""
                              WHERE ""Korisnik"".""timID""='"+getMyTeam(username)+"'"+
                              "GROUP BY \"username\",\"email\",\"Avatar\",\"roleID\",\"roleName\",\"teamName\"";




                myTeamData = (List<MyTeamViewModel>)connection.Query<MyTeamViewModel>(query);

            };

            return myTeamData;

        }

        public static int getMyTeam(string username)
        {
            var myTeamID = new List<int>();
            using (NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=nikola000;Database=BugTrackerDB;"))
            {
                connection.Open();
                var query = @"SELECT ""timID""
                              FROM ""Korisnik""
                              WHERE ""username""=" +"\'"+username+"\'";


                myTeamID = (List<int>)connection.Query<int>(query);

            };
            
            return myTeamID.First();
        }

    }

}