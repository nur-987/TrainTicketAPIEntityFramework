using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        public List<User> userList = new List<User>();

        private readonly ITrainTicketDataContext dbContext;

        public UserController()
        {
            dbContext = new TrainTicketDataContext();
        }

        public UserController(ITrainTicketDataContext dbcontext)
        {
            dbContext = dbcontext;
        }

        /// <summary>
        /// gets a list of all users in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]     //checked in postman
        public List<User> GetAllUser()
        {
            return dbContext.Users.ToList();
        }

        /// <summary>
        /// adds a new user if user does not exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns>user ID</returns>
        [HttpPost]
        [Route("adduser/{name}")]       //checked in postman
        [ResponseType(typeof(User))]
        public IHttpActionResult AddNewUser(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            User user1 = new User()
            {
                //ID is auto
                Name = name,
                TicketHistory = new List<Ticket>()

            };
            try
            {
                dbContext.Users.Add(user1);
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
            return Ok(user1.UserId);

        }

        /// <summary>
        /// gets detail of selected user's LATEST train history 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>user details with complete train history</returns>
        [HttpGet]
        [Route("getdetails/{userId}")]      //checked in postman
        [ResponseType(typeof(Ticket))]
        public IHttpActionResult GetSelectedUserDetail(int userId)
        {
            try
            {
                Ticket ticket = dbContext.Tickets.Include("SelectedTrain").Include("User").Where(t => t.User.UserId == userId)
                 .OrderByDescending(t => t.BookingTime).FirstOrDefault();

                return Ok(ticket);

            }
            catch
            {
                return NotFound();
            }

        }

        /// <summary>
        /// gets detail of selected user's ALL train history 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>user details with complete train history</returns>
        [HttpGet]
        [Route("getalldetails/{userId}")]       //checked in postman
        public IQueryable<Ticket> GetSelectedUserAllDetail(int userId)
        {
            IQueryable<Ticket> ListOfticket = dbContext.Tickets.Include("SelectedTrain").Include("User")
                                        .Where(t => t.User.UserId == userId);
            return ListOfticket;

        }

        /// <summary>
        /// checks if user exist in file
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>bool value</returns>
        [HttpGet]
        [Route("checkexist/{userId}")]          //checked in postman
        public bool CheckUserExist(int userId)
        {
            User user = dbContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
