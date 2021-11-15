using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainTicket.API.Models;
using TrainTicket.API.Utility;

namespace TrainTicket.API.Controllers
{
    [RoutePrefix("api/ticket")]
    public class TicketController : ApiController
    {
        FileManager FileManager = new FileManager();

        [HttpPatch]
        [Route("buy")]
        public User BuyTicket(int userId, int numofTickets, Train selectedTrain, TrainClassEnum selectedClass)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            var currentUser = userlistTemp.Where(x => x.UserId == userId).FirstOrDefault();
            if (currentUser == null)
            {
                return null;
            }

            int idCount = currentUser.TicketHistory.Count();

            userlistTemp.Remove(currentUser);
            currentUser.TicketHistory.Add(new Ticket()
            {
                TicketId = ++idCount,
                BookingTime = DateTime.Now,
                SelectedClass = selectedClass,
                SelectedTrain = selectedTrain,
                NumOfTickets = numofTickets
            });

            var updatedString = JsonConvert.SerializeObject(userlistTemp, Formatting.Indented);
            FileManager.WriteAllText("User.json", updatedString);

            return currentUser;
        }
            

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>final cost based on chosen route, class and num of tickets</returns>
        [HttpPatch]
        [Route("finalcost/{userId}")]
        public double GrandTotal(int userId)
        {
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);
            double finalCost = 0;
            double price = 0;

            var currentUser = userlistTemp.Where(x => x.UserId == userId).FirstOrDefault();
            var ticketHistory = currentUser.TicketHistory.Last();


            if (ticketHistory.SelectedClass == TrainClassEnum.FirstClass)
            {
                price = ticketHistory.SelectedTrain.FirstClassFare;
            }
            else if (ticketHistory.SelectedClass == TrainClassEnum.BusinessClass)
            {
                price = ticketHistory.SelectedTrain.BusinessClassFare;
            }
            else if (ticketHistory.SelectedClass == TrainClassEnum.Economy)
            {
                price = ticketHistory.SelectedTrain.EconomyClassFare;
            }

            finalCost = price * ticketHistory.NumOfTickets;
            ticketHistory.GrandTotal = finalCost;
            return finalCost;

        }

    }
}
