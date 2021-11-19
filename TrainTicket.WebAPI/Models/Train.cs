using System;

namespace TrainTicket.API.Models
{
    public class Train
    {
        public int TrainId { get; set; }
        public string StartDestination { get; set; }
        public string EndDestination { get; set; }
        public int Distance { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }


        public double FirstClassFare { get; set; }
        public double BusinessClassFare { get; set; }
        public double EconomyClassFare { get; set; }

    }
}