using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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
        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

    }
}