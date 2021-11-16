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

        public FileManager FileManager = new FileManager();

        TrainTicketDataContext dbContext;

        public UserController()
        {
            dbContext = new TrainTicketDataContext();
        }

        /// <summary>
        /// gets a list of all users in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]     //checked in postman
        public List<User> GetAllUser()
        {
            return dbContext.User.ToList();
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
            User user1 = new User()
            {
                //ID is auto
                Name = name

            };

            dbContext.User.Add(user1);
            dbContext.SaveChanges();
            
            return Ok(user1.UserId);

        }

        /// <summary>
        /// gets detail of selected user's LATEST train history 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>user details with complete train history</returns>
        [HttpGet]
        [Route("getdetails/{userId}")]      //checked in postman
        public Ticket GetSelectedUserDetail(int userId)
        {
           Ticket ticket = dbContext.Ticket.Where(t => t.User.UserId == userId).LastOrDefault();
           return ticket;

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
            IQueryable<Ticket> ListOfticket = dbContext.Ticket.Where(t => t.User.UserId == userId);
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
            User user = dbContext.User.Where(u => u.UserId == userId).FirstOrDefault();
            if (user!= null)
            {
                return true;
            }

            return false;
        }
    }
}
