namespace TrainTicket.API.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TrainTicket.API.Models;
    using TrainTicket.API.Utility;

    internal sealed class Configuration : DbMigrationsConfiguration<TrainTicket.API.Data.TrainTicketDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TrainTicket.API.Data.TrainTicketDataContext context)
        {
            IAppConfiguration _config = new AppConfiguration();
            _config.Initialize(300, 250, 150, 3.5, 2.5, 1.5);

            Train train1 = new Train()
            {
                TrainId = 1,
                StartDestination = "London",
                EndDestination = "Birmingham",
                Distance = 204,
                DepartureTime = new DateTime(2021, 12, 01, 8, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 10, 00, 00)

            };
            Train train2 = new Train()
            {
                TrainId = 2,
                StartDestination = "London",
                EndDestination = "Birmingham",
                Distance = 204,
                DepartureTime = new DateTime(2021, 12, 01, 20, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 22, 00, 00)

            };
            Train train3 = new Train()
            {
                TrainId = 3,
                StartDestination = "Aberdeen",
                EndDestination = "Edinburgh",
                Distance = 203,
                DepartureTime = new DateTime(2021, 12, 01, 9, 30, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 12, 00, 00)

            };
            Train train4 = new Train()
            {
                TrainId = 4,
                StartDestination = "Aberdeen",
                EndDestination = "Edinburgh",
                Distance = 203,
                DepartureTime = new DateTime(2021, 12, 01, 19, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 21, 30, 00)

            };
            Train train5 = new Train()
            {
                TrainId = 5,
                StartDestination = "Dublin",
                EndDestination = "Westport",
                Distance = 263,
                DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00)

            };
            Train train6 = new Train()
            {
                TrainId = 6,
                StartDestination = "Dublin",
                EndDestination = "Westport",
                Distance = 263,
                DepartureTime = new DateTime(2021, 12, 01, 02, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 06, 00, 00)

            };
            Train train7 = new Train()
            {
                TrainId = 7,
                StartDestination = "Brussels",
                EndDestination = "London",
                Distance = 379,
                DepartureTime = new DateTime(2021, 12, 01, 17, 30, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 19, 45, 00)

            };
            Train train8 = new Train()
            {
                TrainId = 8,
                StartDestination = "Brussels",
                EndDestination = "London",
                Distance = 379,
                DepartureTime = new DateTime(2021, 12, 01, 07, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 09, 15, 00)

            };
            Train train9 = new Train()
            {
                TrainId = 9,
                StartDestination = "Paris",
                EndDestination = "Frankfurt",
                Distance = 658,
                DepartureTime = new DateTime(2021, 12, 01, 18, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 22, 00, 00)

            };
            Train train10 = new Train()
            {
                TrainId = 10,
                StartDestination = "Paris",
                EndDestination = "Frankfurt",
                Distance = 658,
                DepartureTime = new DateTime(2021, 12, 01, 03, 00, 00),
                ArrivalTime = new DateTime(2021, 12, 01, 07, 00, 00),

            };

            List<Train> AvailableTrainList = new List<Train>();
            AvailableTrainList.Add(train1);
            AvailableTrainList.Add(train2);
            AvailableTrainList.Add(train3);
            AvailableTrainList.Add(train4);
            AvailableTrainList.Add(train5);
            AvailableTrainList.Add(train6);
            AvailableTrainList.Add(train7);
            AvailableTrainList.Add(train8);
            AvailableTrainList.Add(train9);
            AvailableTrainList.Add(train10);

            foreach (Train train in AvailableTrainList)
            {
                train.BusinessClassFare = _config.BusinessClassBasePrice + train.Distance * _config.BusinessClassDistanceMultiplier;
                train.EconomyClassFare = _config.EconomyClassBasePrice + train.Distance * _config.EconomyClassDistanceMultiplier;
                train.FirstClassFare = _config.FirstClassBasePrice + train.Distance * _config.FirstClassDistanceMultiplier;

                context.Trains.Add(train);
            }

            User user1 = new User()
            {
                UserId = 1,
                Name = "Genny"
            };
            Ticket ticket = new Ticket()
            {
                TicketId = 1,
                SelectedTrain = train1,
                SelectedClass = 0,
                BookingTime = DateTime.Now,
                NumOfTickets = 2,
                GrandTotal = 500,
                UserId = 1

            };
            Ticket ticket2 = new Ticket()
            {
                TicketId = 2,
                SelectedTrain = train1,
                SelectedClass = 0,
                BookingTime = DateTime.Now,
                NumOfTickets = 2,
                GrandTotal = 500,
                UserId = 1

            };

            context.User.Add(user1);
            context.Ticket.Add(ticket);
            context.Ticket.Add(ticket2);

            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
