using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainTicket.API.Utility
{
    /// <summary>
    /// This is an enum which represents different classes of tickets available for a user to buy
    /// </summary>
    public enum TrainClassEnum
    {
        /// <summary>
        /// Represents the First Class
        /// </summary>
        FirstClass,
        BusinessClass,
        Economy
    }
}