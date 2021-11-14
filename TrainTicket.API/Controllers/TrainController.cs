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
    [RoutePrefix("api/train")]
    public class TrainController : ApiController
    {
        public FileManager FileManager = new FileManager();
        private List<Train> _trainlistJson;
        IAppConfiguration _config;
        public TrainController()      //interface boxing; allow for mocking
        {
            _config = new AppConfiguration();
            _config.Initialize(300, 250, 150, 3.5, 2.5, 1.5);
        }

        /// <summary>
        /// creates the train list and add to json file
        /// </summary>
        /// <returns>list of available trains</returns>
        [HttpPost]
        [Route("addtrain")]
        public List<Train> CreateTrainList()
        {
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

            //add to json file
            string trainListJson = JsonConvert.SerializeObject(AvailableTrainList);
            FileManager.WriteAllText("TrainList.json", trainListJson);

            return AvailableTrainList;
        }

        /// <summary>
        /// calls the createTrain method if there is no train.json file
        /// calls the config to calculate the train fares for all classess and routes
        /// </summary>
        [HttpGet]
        [Route("")]
        public void Initialize()
        {
            if (!File.Exists("TrainList.json"))
            {
                CreateTrainList();
            }
            string trainFromJson = FileManager.ReadAllText("TrainList.json");
            _trainlistJson = JsonConvert.DeserializeObject<List<Train>>(trainFromJson);
            foreach (Train train in _trainlistJson)
            {
                train.BusinessClassFare = _config.BusinessClassBasePrice + train.Distance * _config.BusinessClassDistanceMultiplier;
                train.EconomyClassFare = _config.EconomyClassBasePrice + train.Distance * _config.EconomyClassDistanceMultiplier;
                train.FirstClassFare = _config.FirstClassBasePrice + train.Distance * _config.FirstClassDistanceMultiplier;
            }
        }

        /// <summary>
        /// gets a list of start stations
        /// </summary>
        /// <returns>a list of start stations</returns>
        [HttpGet]
        [Route("getstart")]
        public List<string> GetAllStartStations()
        {
            string trainFromJson = FileManager.ReadAllText("TrainList.json");
            _trainlistJson = JsonConvert.DeserializeObject<List<Train>>(trainFromJson);

            List<string> startStationList = new List<string>();
            foreach (Train train in _trainlistJson)
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
        [Route("getend")]
        public List<string> GetAllEndStations()
        {
            string trainFromJson = FileManager.ReadAllText("TrainList.json");
            _trainlistJson = JsonConvert.DeserializeObject<List<Train>>(trainFromJson);

            List<string> endStationList = new List<string>();
            foreach (Train train in _trainlistJson)
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
        [HttpGet]
        [Route("getbetween")]
        public List<Train> GetTrainsBetweenStations(string start, string end)
        {
            string trainFromJson = FileManager.ReadAllText("TrainList.json");
            _trainlistJson = JsonConvert.DeserializeObject<List<Train>>(trainFromJson);

            return _trainlistJson.Where(x => string.Equals(x.EndDestination, end, StringComparison.OrdinalIgnoreCase)
            && string.Equals(x.StartDestination, start, StringComparison.OrdinalIgnoreCase)).ToList();
        }

    }
}
