using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainTicket.API.Data;
using TrainTicket.API.Models;
using TrainTicket.API.Utility;

namespace TrainTicket.API.Controllers
{
    [RoutePrefix("api/ticket")]
    public class TicketController : ApiController
    {
        FileManager FileManager = new FileManager();
        private TrainTicketDataContext dbContext = new TrainTicketDataContext();

        [HttpGet]
        [Route("")]         //checked in postman
        public IQueryable<Ticket> DisplayAllTicket()
        {
            return dbContext.Ticket;
        }

        [HttpPatch]
        [Route("buy/{userId}/{numOfTicket}/{selectedClass}")]
        public User BuyTicket1(int userId, int numOfTicket, TrainClassEnum selectedClass, Train selectedTrain)
        {
            User user = dbContext.User.Find(userId);

            Ticket ticket = new Ticket()
            {
                SelectedTrain = selectedTrain,
                SelectedClass = selectedClass,
                BookingTime = DateTime.Now,
                NumOfTickets = numOfTicket,
                UserId = user.UserId
            };

            dbContext.Ticket.Add(ticket);
            dbContext.SaveChanges();

            return user;
        }

        [HttpPatch]
        [Route("buy2")]
        public Ticket BuyTicket2(int userId, TrainClassEnum selectedClass)
        {
            Ticket ticket = dbContext.Ticket.Where(t => t.User.UserId == userId).LastOrDefault();
            ticket.SelectedClass = selectedClass;
            ticket.BookingTime = DateTime.Now;

            dbContext.Ticket.Add(ticket);
            dbContext.SaveChanges();

            return ticket;

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
            double finalCost = 0;
            double price = 0;
            //fares are empty now. fares are from TrainCotroller
            //trainSelected = null!!
            Ticket ticketHistory = dbContext.Ticket.Where(t => t.User.UserId == userId && t.TicketId==8).FirstOrDefault();

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
            dbContext.Ticket.Add(ticketHistory);
            dbContext.SaveChanges();

            return finalCost;

        }

    }
}
