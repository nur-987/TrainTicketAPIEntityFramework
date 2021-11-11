using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainTicket.API.Utility
{
    public interface IFileReadWrite
    {
        string ReadAllText(string FileName);
        bool WriteAllText(string FileName, string InputDetails);

    }
}