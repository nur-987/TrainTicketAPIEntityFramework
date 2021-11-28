using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TrainTicket.API.Data;
using TrainTicket.API.Models;
using TrainTicket.API.Utility;

namespace TrainTicket.API.Controllers
{
    [RoutePrefix("api/ticket")]
    public class TicketController : ApiController
    {
        private ITrainTicketDataContext dbContext;

        public TicketController()
        {
            dbContext = new TrainTicketDataContext();
        }
        public TicketController(ITrainTicketDataContext dbcontext)
        {
            dbContext = dbcontext;
        }

        [HttpGet]
        [Route("")]         //checked in postman
        public IQueryable<Ticket> DisplayAllTicket()
        {
            return dbContext.Tickets;
        }

        [HttpPost]
        [Route("buy/{userId}/{numOfTicket}/{selectedClass}")]           //checked in postman
        [ResponseType(typeof(User))]
        public IHttpActionResult BuyTicket(int userId, int numOfTicket, TrainClassEnum selectedClass, Train selectedTrain)
        {
            User user = dbContext.Users.Find(userId);
            Train train = dbContext.Trains.Find(selectedTrain.TrainId);

            if (user != null && train !=null)
            {
                Ticket ticket = new Ticket()
                {
                    SelectedTrain = train,
                    SelectedClass = selectedClass,
                    BookingTime = DateTime.Now,
                    NumOfTickets = numOfTicket,
                    User = user,
                    UserId = user.UserId
                };

                dbContext.Tickets.Add(ticket);
                dbContext.SaveChanges();

                return Ok(user);
            }

            return InternalServerError();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>final cost based on chosen route, class and num of tickets</returns>
        [HttpPut]
        [Route("finalcost/{userId}")]       //checked in postman
        public double GrandTotal(int userId)
        {
            double finalCost = 0;
            double price = 0;

            var ticketHistory = dbContext.Tickets.Include("SelectedTrain").Include("User").Where(t => t.User.UserId == userId)
                            .OrderByDescending(t => t.BookingTime).FirstOrDefault();

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

            //dbContext.Entry(ticketHistory).State = System.Data.Entity.EntityState.Modified;

            dbContext.SaveChanges();

            return finalCost;

        }

    }
}
