using System.Data.Entity;
using TrainTicket.API.Models;

namespace TrainTicket.API.Data
{
    public class TrainTicketDataContext: DbContext
    {
        public TrainTicketDataContext():base("Name=TrainTicketConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TrainTicketDataContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public DbSet<Train> Trains { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}