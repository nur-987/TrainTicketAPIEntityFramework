using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicket.Common
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public Train SelectedTrain { get; set; }
        public TrainClassEnum SelectedClass { get; set; }
        public DateTime BookingTime { get; set; }
        public int NumOfTickets { get; set; }

        public double GrandTotal { get; set; }
    }
}
