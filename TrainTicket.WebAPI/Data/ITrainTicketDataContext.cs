using System.Data.Entity;
using TrainTicket.API.Models;

namespace TrainTicket.API.Data
{
    public interface ITrainTicketDataContext
    {
        DbSet<Ticket> Tickets { get; set; }
        DbSet<Train> Trains { get; set; }
        DbSet<User> Users { get; set; }

        void SaveChanges();
    }
}