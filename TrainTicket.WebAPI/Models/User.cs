using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainTicket.API.Models
{
    public class User
    {

        public int UserId { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Ticket.TicketId))]
        public List<Ticket> TicketHistory { get; set; }
    }
}