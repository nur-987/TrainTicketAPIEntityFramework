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

        /// <summary>
        /// create the json file and first user with ticket history if one does not exist
        /// </summary>
        /// <param name="ticket"></param>
        [HttpGet]
        [Route("")]
        public void Initialize(Ticket ticket)
        {
            if (!File.Exists("User.json"))
            {
                User user = new User()
                {
                    UserId = 1,
                    Name = "Genny",
                    TicketHistory = new List<Ticket>() { ticket}

                };

                userList.Add(user);
                string userJsonInput = JsonConvert.SerializeObject(userList);
                FileManager.WriteAllText("User.json", userJsonInput);
            }

        }
        /// <summary>
        /// aads a new user if user does not exist
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ticket"></param>
        /// <returns>user ID</returns>
        [HttpPost]
        [Route("adduser/{name}")]
        public int AddNewUser(string name)
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
            int userId = user.UserId;
            userlistTemp.Add(user);

            //add to jsonFile
            string userJsonInput = JsonConvert.SerializeObject(userlistTemp);
            FileManager.WriteAllText("User.json", userJsonInput);

            return userId;
        }

        /// <summary>
        /// gets detail of selected user and all of users train history 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>user details with complete train history</returns>
        [HttpGet]
        [Route("getcurrentdetails/{userId}")]
        public User GetSelectedUserFinalDetail(int userId)
        {
            //detail for that selected train only
            string userFromJson = FileManager.ReadAllText("User.json");
            List<User> userlistTemp = JsonConvert.DeserializeObject<List<User>>(userFromJson);

            var Useritem = userlistTemp.First(x => x.UserId == userId);
            return Useritem;

        }

        /// <summary>
        /// checks if user exist in file
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>bool value</returns>
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
