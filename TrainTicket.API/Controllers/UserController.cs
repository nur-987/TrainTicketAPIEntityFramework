using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainTicket.API.Models;
using TrainTicket.API.Utility;

namespace TrainTicket.API.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        public List<User> userList = new List<User>();

        public FileManager FileManager = new FileManager();

        [HttpGet]
        [Route("")]
        public void Initialize()
        {
            if (!File.Exists("User.json"))
            {
                User user = new User()
                {
                    UserId = 1,
                    Name = "Genny",
                    TicketHistory = new List<Ticket>()

                };

                userList.Add(user);
                //from List => JsonFile
                string userJsonInput = JsonConvert.SerializeObject(userList);
                FileManager.WriteAllText("User.json", userJsonInput);
            }

        }

        [HttpPost]
        [Route("adduser")]
        public void AddNewUser(string name, out int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            int num = userlistTemp.Count();
            User user = new User()
            {
                UserId = num + 1,
                Name = name,
                TicketHistory = new List<Ticket>()
            };
            userId = user.UserId;
            userlistTemp.Add(user);

            //add to jsonFile
            string userJsonInput = JsonConvert.SerializeObject(userlistTemp);
            FileManager.WriteAllText("User.json", userJsonInput);
        }

        [HttpGet]
        [Route("getcurrentdetails/{userId}")]
        public void GetSelectedUserFinalDetail(int userId)
        {
            //detail for that selected train only
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            var Useritem = userlistTemp.First(x => x.UserId == userId);
            if (Useritem != null)
            {
                Console.WriteLine("ID: " + Useritem.UserId);
                Console.WriteLine("Name: " + Useritem.Name);

                //choose which history to display
                int counter = Useritem.TicketHistory.Count; //choosing the last item in history

                //foreach ticket = print ticket history
                foreach (var ticketHistoryItem in Useritem.TicketHistory)
                {
                    if (ticketHistoryItem.TicketId == counter)
                    {
                        Console.WriteLine("Booking Time: " + ticketHistoryItem.BookingTime);
                        Console.WriteLine("Origin Station: " + ticketHistoryItem.SelectedTrain.StartDestination);
                        Console.WriteLine("End Station: " + ticketHistoryItem.SelectedTrain.EndDestination);
                        Console.WriteLine("Departure Time: " + ticketHistoryItem.SelectedTrain.DepartureTime.ToShortTimeString());
                        Console.WriteLine("Arrival Time: " + ticketHistoryItem.SelectedTrain.ArrivalTime.ToShortTimeString());
                        Console.WriteLine("Number of tickets: " + ticketHistoryItem.NumOfTickets);
                        Console.WriteLine("Travel Class: " + ticketHistoryItem.SelectedClass);

                    }

                }

            }

        }

        [HttpGet]
        [Route("getalldetails/{userId}")]
        public void GetSelectedUserAllDetails(int userId)
        {
            //detail for all trains purchased
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            var Useritem = userlistTemp.First(x => x.UserId == userId);
            if (Useritem != null)
            {
                Console.WriteLine("ID: " + Useritem.UserId);
                Console.WriteLine("Name: " + Useritem.Name);

                //foreach ticket = print ticket history
                foreach (var ticketHistoryItem in Useritem.TicketHistory)
                {
                    Console.WriteLine("Ticket Id: " + ticketHistoryItem.TicketId);
                    Console.WriteLine("Booking Time: " + ticketHistoryItem.BookingTime);
                    Console.WriteLine("Origin Station: " + ticketHistoryItem.SelectedTrain.StartDestination);
                    Console.WriteLine("End Station: " + ticketHistoryItem.SelectedTrain.EndDestination);
                    Console.WriteLine("Departure Time: " + ticketHistoryItem.SelectedTrain.DepartureTime.ToShortTimeString());
                    Console.WriteLine("Arrival Time: " + ticketHistoryItem.SelectedTrain.ArrivalTime.ToShortTimeString());
                    Console.WriteLine("Number of tickets: " + ticketHistoryItem.NumOfTickets);
                    Console.WriteLine("Travel Class: " + ticketHistoryItem.SelectedClass);
                    Console.WriteLine("-------------------------------------");
                }

            }

        }

        [HttpGet]
        [Route("checkexist/{userId}")]
        public bool CheckUserExist(int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            foreach (User item in userlistTemp)
            {
                if (item.UserId == userId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
