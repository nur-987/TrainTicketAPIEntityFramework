using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTicket.Common
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        [JsonProperty(Required = Required.Default)]
        public List<Ticket> TicketHistory { get; set; }

    }
}
