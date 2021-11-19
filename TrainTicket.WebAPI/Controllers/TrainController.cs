using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TrainTicket.API.Data;
using TrainTicket.API.Models;
using TrainTicket.API.Utility;
using TrainTicket.Common.DTO;

namespace TrainTicket.API.Controllers
{
    [RoutePrefix("api/train")]
    public class TrainController : ApiController
    {
        IAppConfiguration _config;
        TrainTicketDataContext dbContext;
        public TrainController()      //interface boxing; allow for mocking
        {
            _config = new AppConfiguration();
            _config.Initialize(300, 250, 150, 3.5, 2.5, 1.5);
            dbContext = new TrainTicketDataContext();
        }


        [HttpGet]
        [Route("")]         //checked in postman
        public IEnumerable<TrainDTO> DisplayAllTrain()
        {
            IEnumerable<TrainDTO> result = new List<TrainDTO>();
            foreach(Train train in dbContext.Trains)
            {
                result.Append(ToDTO(train));
            }
            return result;
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
        public IEnumerable<TrainDTO> GetTrainsBetweenStations(string start, string end)
        {
            IEnumerable<TrainDTO> result = new List<TrainDTO>();
            foreach (Train train in dbContext.Trains.Where(x => string.Equals(x.EndDestination, end, StringComparison.OrdinalIgnoreCase)
            && string.Equals(x.StartDestination, start, StringComparison.OrdinalIgnoreCase)))
            {
                result.Append(ToDTO(train));
            }
            return result;
        }

        private TrainDTO ToDTO(Train train)
        {
            var dto = new TrainDTO();
            dto.ArrivalTime = train.ArrivalTime;
            dto.BusinessClassFare = train.BusinessClassFare;
            dto.DepartureTime= train.DepartureTime;
            dto.Distance = train.Distance;
            dto.EconomyClassFare   =    train.EconomyClassFare;
            dto.EndDestination = train.EndDestination;
            dto.FirstClassFare= train.FirstClassFare;
            dto.StartDestination = train.StartDestination;
            dto.TrainId = train.TrainId;
            return dto;
        }

    }
}
