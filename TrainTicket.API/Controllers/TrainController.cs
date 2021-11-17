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
        public IQueryable<Train> DisplayAllTrain()
        {
            return dbContext.Trains;
        }

        /// <summary>
        /// creates the train list and add to json file
        /// </summary>
        /// <returns>list of available trains</returns>
        [HttpPost]
        [Route("addtrain")]         //checked in postman
        public List<Train> CreateTrainList()
        {
            //move to seed? 
            Train train1 = new Train()
            {
                TrainId = 1,
                StartDestination = "London",
                EndDestination = "Birmingham",
                Distance = 204,
                DepartureTime = new DateTime(2021, 12, 01, 8, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 10, 00, 00)

            };
            Train train2 = new Train()
            {
                TrainId = 2,
                StartDestination = "London",
                EndDestination = "Birmingham",
                Distance = 204,
                DepartureTime = new DateTime(2021, 12, 01, 20, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 22, 00, 00)

            };
            Train train3 = new Train()
            {
                TrainId = 3,
                StartDestination = "Aberdeen",
                EndDestination = "Edinburgh",
                Distance = 203,
                DepartureTime = new DateTime(2021, 12, 01, 9, 30, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 12, 00, 00)

            };
            Train train4 = new Train()
            {
                TrainId = 4,
                StartDestination = "Aberdeen",
                EndDestination = "Edinburgh",
                Distance = 203,
                DepartureTime = new DateTime(2021, 12, 01, 19, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 21, 30, 00)

            };
            Train train5 = new Train()
            {
                TrainId = 5,
                StartDestination = "Dublin",
                EndDestination = "Westport",
                Distance = 263,
                DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00)

            };
            Train train6 = new Train()
            {
                TrainId = 6,
                StartDestination = "Dublin",
                EndDestination = "Westport",
                Distance = 263,
                DepartureTime = new DateTime(2021, 12, 01, 02, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 06, 00, 00)

            };
            Train train7 = new Train()
            {
                TrainId = 7,
                StartDestination = "Brussels",
                EndDestination = "London",
                Distance = 379,
                DepartureTime = new DateTime(2021, 12, 01, 17, 30, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 19, 45, 00)

            };
            Train train8 = new Train()
            {
                TrainId = 8,
                StartDestination = "Brussels",
                EndDestination = "London",
                Distance = 379,
                DepartureTime = new DateTime(2021, 12, 01, 07, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 09, 15, 00)

            };
            Train train9 = new Train()
            {
                TrainId = 9,
                StartDestination = "Paris",
                EndDestination = "Frankfurt",
                Distance = 658,
                DepartureTime = new DateTime(2021, 12, 01, 18, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 22, 00, 00)

            };
            Train train10 = new Train()
            {
                TrainId = 10,
                StartDestination = "Paris",
                EndDestination = "Frankfurt",
                Distance = 658,
                DepartureTime = new DateTime(2021, 12, 01, 03, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 07, 00, 00),

            };

            List<Train> AvailableTrainList = new List<Train>();
            AvailableTrainList.Add(train1);
            AvailableTrainList.Add(train2);
            AvailableTrainList.Add(train3);
            AvailableTrainList.Add(train4);
            AvailableTrainList.Add(train5);
            AvailableTrainList.Add(train6);
            AvailableTrainList.Add(train7);
            AvailableTrainList.Add(train8);
            AvailableTrainList.Add(train9);
            AvailableTrainList.Add(train10);

            foreach (Train train in AvailableTrainList)
            {
                train.BusinessClassFare = _config.BusinessClassBasePrice + train.Distance * _config.BusinessClassDistanceMultiplier;
                train.EconomyClassFare = _config.EconomyClassBasePrice + train.Distance * _config.EconomyClassDistanceMultiplier;
                train.FirstClassFare = _config.FirstClassBasePrice + train.Distance * _config.FirstClassDistanceMultiplier;

                dbContext.Trains.Add(train);
            }

            dbContext.SaveChanges();

            //displays all trains
            return dbContext.Trains.ToList();
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
