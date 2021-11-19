using System;

namespace TrainTicket.Common.DTO
{
    public class TicketDTO
    {
        public int TicketId { get; set; }
        public TrainDTO SelectedTrain { get; set; }
        public TrainClassEnum SelectedClass { get; set; }
        public DateTime BookingTime { get; set; }
        public int NumOfTickets { get; set; }
        public double GrandTotal { get; set; }
    }
}
