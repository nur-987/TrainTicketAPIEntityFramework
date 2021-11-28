using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using TrainTicket.API.Controllers;
using TrainTicket.API.Data;
using TrainTicket.API.Models;

namespace TrainTicket.UnitTest
{
    [TestClass]
    public class TicketControllerTest
    {
        private readonly TicketController ticketController;
        private readonly Mock<ITrainTicketDataContext> dbContextMock = new Mock<ITrainTicketDataContext>();
        public TicketControllerTest()
        {
            ticketController = new TicketController(dbContextMock.Object);
        }

        [TestMethod]
        public void BuyTicket_NullUser_ReturnInternalServerError()
        {
            //arrange
            var user = new User { UserId = 100, Name = "Ben" };
            var train = new Train { TrainId = 1, StartDestination = "ZZZ", EndDestination = "XXX" };
            var ticketList = new List<Ticket>
            {
                new Ticket{TicketId =1, BookingTime= DateTime.Now, GrandTotal=0, NumOfTickets=1,SelectedTrain = train, User = user},
                new Ticket{TicketId =2, BookingTime= DateTime.Now, GrandTotal=0, NumOfTickets=1,SelectedTrain = train, User = user}
            }.AsQueryable();

            var userList = new List<User>
            {
                user
            }.AsQueryable();

            var trainList = new List<Train>
            {
                train
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(ticketList.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(ticketList.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(ticketList.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(ticketList.GetEnumerator());

            dbContextMock.Setup(x => x.Tickets).Returns(mockSet.Object);

            var mockUserSet = new Mock<DbSet<User>>();
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            dbContextMock.Setup(x => x.Users).Returns(mockUserSet.Object);

            var mockTrainSet = new Mock<DbSet<Train>>();
            mockTrainSet.As<IQueryable<Train>>().Setup(m => m.Provider).Returns(trainList.Provider);
            mockTrainSet.As<IQueryable<Train>>().Setup(m => m.Expression).Returns(trainList.Expression);
            mockTrainSet.As<IQueryable<Train>>().Setup(m => m.ElementType).Returns(trainList.ElementType);
            mockTrainSet.As<IQueryable<Train>>().Setup(m => m.GetEnumerator()).Returns(trainList.GetEnumerator());

            dbContextMock.Setup(x => x.Trains).Returns(mockTrainSet.Object);

            //act
            var result = ticketController.BuyTicket(1, 2, 0, train);

            //assert
            Assert.IsInstanceOfType(result, typeof(InternalServerErrorResult));
            Console.WriteLine("user not found, returned http error");

        }

        [TestMethod]
        public void DisplayAllTicket_ReturnList()
        {
            //arrange
            var user = new User { UserId = 1, Name = "Ben" };
            var train = new Train { TrainId = 1, StartDestination = "ZZZ", EndDestination = "XXX" };
            var ticketList = new List<Ticket>
            {
                new Ticket{TicketId =1, BookingTime= DateTime.Now, GrandTotal=0, NumOfTickets=1,SelectedTrain = train, User = user},
                new Ticket{TicketId =2, BookingTime= DateTime.Now, GrandTotal=0, NumOfTickets=1,SelectedTrain = train, User = user}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(ticketList.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(ticketList.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(ticketList.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(ticketList.GetEnumerator());

            dbContextMock.Setup(x => x.Tickets).Returns(mockSet.Object);

            //act
            var result = ticketController.DisplayAllTicket();

            //assert
            Assert.AreEqual(2, result.Count());
            Console.WriteLine("return correct number of list");

        }
    }


    
}
