using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrainTicket.API.Models;

namespace TrainTicket.API.Data
{
    public class TrainTicketDataContext : DbContext, ITrainTicketDataContext
    {
        public TrainTicketDataContext() : base("Name=TrainTicketConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TrainTicketDataContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        void ITrainTicketDataContext.SaveChanges()
        {
            SaveChanges();
        }

        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

    }
}