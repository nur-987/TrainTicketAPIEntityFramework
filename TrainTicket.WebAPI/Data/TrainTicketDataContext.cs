using System.Data.Entity;
using TrainTicket.API.Models;
using TrainTicket.WebAPI.Data;

namespace TrainTicket.API.Data
{
    public class TrainTicketDataContext: DbContext
    {
        public TrainTicketDataContext():base("Name=TrainTicketConnectionString")
        {
            Database.SetInitializer(new TrainDbInitializer());
        }
        public DbSet<Train> Trains { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}