using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainTicket.API.Data;
using TrainTicket.API.Models;
using TrainTicket.API.Utility;

namespace TrainTicket.API.Controllers
{
    [RoutePrefix("api/train")]
    public class TrainController : ApiController
    {
        private readonly ITrainTicketDataContext dbContext;

        public TrainController()   
        {
            dbContext = new TrainTicketDataContext();
        }

        public TrainController(ITrainTicketDataContext dbcontext)
        {
            dbContext = dbcontext;
        }

        [HttpGet]
        [Route("")]         //checked in postman
        public IQueryable<Train> DisplayAllTrain()
        {
            return dbContext.Trains;
        }

        /// <summary>
        /// gets a list of start stations
        /// </summary>
        /// <returns>a list of start stations</returns>
        [HttpGet]
        [Route("getstart")]         //checked in postman
        public List<string> GetAllStartStations()
        {
            List<Train> AvailableTrainList = dbContext.Trains.ToList();

            List<string> startStationList = new List<string>();
            foreach (Train train in AvailableTrainList)
            {
                if (!startStationList.Contains(train.StartDestination))
                {
                    startStationList.Add(train.StartDestination);
                }

            }
            return startStationList;
        }

        /// <summary>
        /// gets a list of end stations
        /// </summary>
        /// <returns>a list of end stations</returns>
        [HttpGet]
        [Route("getend")]           //checked in postman
        public List<string> GetAllEndStations()
        {
            List<Train> AvailableTrainList = dbContext.Trains.ToList();

            List<string> endStationList = new List<string>();
            foreach (Train train in AvailableTrainList)
            {
                if (!endStationList.Contains(train.EndDestination))
                {
                    endStationList.Add(train.EndDestination);
                }
                   
            }
            return endStationList;
        }

        /// <summary>
        /// gets a list of availabe trains and thier timings based on selected start and end desitination
        /// </summary>
        /// <param name="start">start station as chosen by user</param>
        /// <param name="end">end station as chosen by user</param>
        /// <returns>list of available routes between the chossen stations</returns>
        /// if no result, return empty list
        [HttpGet]
        [Route("getbetween/{start}/{end}")]         //checked in postman
        public List<Train> GetTrainsBetweenStations(string start, string end)
        {
            List<Train> AvailableTrainList = dbContext.Trains.ToList();

            return AvailableTrainList.Where(x => string.Equals(x.EndDestination, end, StringComparison.OrdinalIgnoreCase)
            && string.Equals(x.StartDestination, start, StringComparison.OrdinalIgnoreCase)).ToList();
        }

    }
}
