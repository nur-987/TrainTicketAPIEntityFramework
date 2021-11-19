using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using TrainTicket.API.Data;
using TrainTicket.API.Models;
using TrainTicket.Common.DTO;

namespace TrainTicket.API.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        public List<User> userList = new List<User>();

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
        public IEnumerable<UserDTO> GetAllUser()
        {
            IEnumerable<UserDTO> result = new List<UserDTO>();
            foreach (User user in dbContext.Users)
            {
                result.Append(ToDTO(user));
            }
            return result;
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
                Name = name,
                TicketHistory = new List<Ticket>()

            };

            dbContext.Users.Add(user1);
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
        public TicketDTO GetSelectedUserDetail(int userId)
        {
           Ticket ticket = dbContext.Tickets.Include("SelectedTrain").Include("User").Where(t => t.User.UserId == userId)
                            .OrderByDescending(t => t.BookingTime).FirstOrDefault();
            return ToDTO(ticket);

        }

        /// <summary>
        /// gets detail of selected user's ALL train history 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>user details with complete train history</returns>
        [HttpGet]
        [Route("getalldetails/{userId}")]       //checked in postman
        public IEnumerable<TicketDTO> GetSelectedUserAllDetail(int userId)
        {
            IEnumerable<TicketDTO> result = new List<TicketDTO>();
            foreach (Ticket ticket in dbContext.Tickets.Include("SelectedTrain").Include("User")
                                        .Where(t => t.User.UserId == userId))
            {
                result.Append(ToDTO(ticket));
            }
            return result;

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
            if (user!= null)
            {
                return true;
            }

            return false;
        }

        private TicketDTO ToDTO(Ticket ticket)
        {
            var dto = new TicketDTO();
            dto.BookingTime = ticket.BookingTime;
            dto.GrandTotal = ticket.GrandTotal;
            dto.NumOfTickets = ticket.NumOfTickets;
            dto.SelectedClass = ticket.SelectedClass;
            dto.SelectedTrain = ToDTO(ticket.SelectedTrain);
            dto.TicketId = ticket.TicketId;  
            return dto;
        }
        private TrainDTO ToDTO(Train train)
        {
            var dto = new TrainDTO();
            dto.ArrivalTime = train.ArrivalTime;
            dto.BusinessClassFare = train.BusinessClassFare;
            dto.DepartureTime = train.DepartureTime;
            dto.Distance = train.Distance;
            dto.EconomyClassFare = train.EconomyClassFare;
            dto.EndDestination = train.EndDestination;
            dto.FirstClassFare = train.FirstClassFare;
            dto.StartDestination = train.StartDestination;
            dto.TrainId = train.TrainId;
            return dto;
        }
        private UserDTO ToDTO(User user)
        {
            var dto = new UserDTO();
            dto.Name = user.Name;
            dto.UserId = user.UserId;
            dto.TicketHistory = new List<TicketDTO>();
            foreach (Ticket ticket in user.TicketHistory)
{
                dto.TicketHistory.Add(ToDTO(ticket));
            }
            return dto;
        }
    }
}
