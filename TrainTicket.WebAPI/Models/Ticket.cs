using System;
using System.ComponentModel.DataAnnotations.Schema;
using TrainTicket.Common;

namespace TrainTicket.API.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public Train SelectedTrain { get; set; }
        public TrainClassEnum SelectedClass { get; set; }
        public DateTime BookingTime { get; set; }
        public int NumOfTickets { get; set; }
        public double GrandTotal { get; set; }

        [ForeignKey(nameof(Ticket.User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}