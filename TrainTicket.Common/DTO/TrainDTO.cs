using Newtonsoft.Json;
using System;

namespace TrainTicket.Common.DTO
{
    public class TrainDTO
    {
        public int TrainId { get; set; }
        public string StartDestination { get; set; }
        public string EndDestination { get; set; }
        public int Distance { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }


        [JsonProperty(Required = Required.Default)]
        public double FirstClassFare { get; set; }
        [JsonProperty(Required = Required.Default)]
        public double BusinessClassFare { get; set; }
        [JsonProperty(Required = Required.Default)]
        public double EconomyClassFare { get; set; }

    }
}
