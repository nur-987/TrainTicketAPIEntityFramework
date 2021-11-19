using Newtonsoft.Json;
using System.Collections.Generic;

namespace TrainTicket.Common.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        [JsonProperty(Required = Required.Default)]
        public List<TicketDTO> TicketHistory { get; set; }

    }
}
