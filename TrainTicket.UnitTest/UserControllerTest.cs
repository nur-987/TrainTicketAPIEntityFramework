using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.Results;
using TrainTicket.API.Controllers;
using TrainTicket.API.Data;
using TrainTicket.API.Models;

namespace TrainTicket.UnitTest
{
    [TestClass]
    public class UserControllerTest
    {
        private readonly UserController userController;
        private readonly Mock<ITrainTicketDataContext> dbContextMock = new Mock<ITrainTicketDataContext>();

        public UserControllerTest()
        {
            userController = new UserController(dbContextMock.Object);
        }

        [TestMethod]
        public void GetAllUser_ReturnedListOfUsers()
        {
            //arrange
            var userList = new List<User>
            {
                new User{UserId =1, Name= "AAA", TicketHistory = null},
                new User{UserId =2, Name= "BBB", TicketHistory = null},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());
            
            dbContextMock.Setup(x => x.Users).Returns(mockSet.Object);

            //act
            var result = userController.GetAllUser();

            //assert
            Assert.AreEqual(2, result.Count);
            Console.WriteLine("returned correct number of users");

        }

        [TestMethod]
        public void AddNewUser_ReturnsBadRequest()
        {
            //arrange
            var user = new User() { UserId = 1, Name = " ", TicketHistory = null };
            dbContextMock.Setup(x => x.Users.Add(user)).Returns(user);


            //act
            var ActionResult = userController.AddNewUser(" ");

            //Assert
            Assert.IsInstanceOfType(ActionResult, typeof(BadRequestResult));
            Console.WriteLine("whitespace name, returned bad request");
        }

        [TestMethod]
        public void GetSelectedUserDetail_NotFoundId()
        {
            //arrange
            var ticketList1 = new List<Ticket>
            {
                new Ticket{TicketId =1, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()},
                 new Ticket{TicketId =2, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()}
            };
            var ticketList2 = new List<Ticket>
            {
                new Ticket{TicketId =1, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()},
                 new Ticket{TicketId =2, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()}
            };

            var userList = new List<User>
            {
                new User{UserId =1, Name= "AAA", TicketHistory = ticketList1 },
                new User{UserId =2, Name= "BBB", TicketHistory = ticketList2},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            dbContextMock.Setup(x => x.Users).Returns(mockSet.Object);

            //act
            var ActionResult = userController.GetSelectedUserDetail(100);

            //Assert
            Assert.IsInstanceOfType(ActionResult, typeof(NotFoundResult));
            Console.WriteLine("content not found, returned not found response");
        }

        [TestMethod]
        public void CheckUserExist_ReturnTrue()
        {
            //arrange
            var userList = new List<User>
            {
                new User{UserId =1, Name= "AAA", TicketHistory = null},
                new User{UserId =2, Name= "BBB", TicketHistory = null},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            dbContextMock.Setup(x => x.Users).Returns(mockSet.Object);

            //act
            var result = userController.CheckUserExist(1);

            //Assert
            Assert.AreEqual(true, result);
            Console.WriteLine("returned true");
        }

        [TestMethod]
        public void CheckUserExist_ReturnFalse()
        {
            //arrange
            var userList = new List<User>
            {
                new User{UserId =1, Name= "AAA", TicketHistory = null},
                new User{UserId =2, Name= "BBB", TicketHistory = null},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            dbContextMock.Setup(x => x.Users).Returns(mockSet.Object);

            //act
            var result = userController.CheckUserExist(100);

            //Assert
            Assert.AreEqual(false, result);
            Console.WriteLine("returned false");
        }

        #region
        //[TestMethod]
        //public void AddNewUser_ReturnsOkUser()
        //{
        //    //arrange
        //    var userList = new List<User>
        //    {
        //        new User{UserId =1, Name= "AAA", TicketHistory = null},

        //    }.AsQueryable();

        //    var user = new User() { UserId = 1, Name = "BBB", TicketHistory = null };

        //    var mockSet = new Mock<DbSet<User>>();
        //    mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

        //    var mockDb = new Mock<ITrainTicketDataContext>();
        //    //dbContextMock.Setup(x => x.Add(user));

        //    //act
        //    var result = userController.AddNewUser("BBB");
        //    var contentResult = result as OkNegotiatedContentResult<int>;

        //    //Assert
        //    Assert.IsNotNull(contentResult);
        //    Assert.IsNotNull(contentResult.Content);
        //    Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<int>));
        //    Console.WriteLine("returned Ok Result");
        //    Assert.AreEqual("1", contentResult.Content);
        //    Console.WriteLine("returned User item");

        //}
        //[TestMethod]
        //public void GetSelectedUserDetail_FoundId()
        //{
        //    //arrange
        //    var ticketList1 = new List<Ticket>
        //    {
        //        new Ticket{TicketId =1, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()},
        //         new Ticket{TicketId =2, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()}
        //    };
        //    var ticketList2 = new List<Ticket>
        //    {
        //        new Ticket{TicketId =1, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()},
        //         new Ticket{TicketId =2, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()}
        //    };

        //    var userList = new List<User>
        //    {
        //        new User{UserId =1, Name= "AAA", TicketHistory = ticketList1 },
        //        new User{UserId =2, Name= "BBB", TicketHistory = ticketList2},
        //    }.AsQueryable();

           
        //    var mockSet = new Mock<DbSet<User>>();
        //    mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

        //    dbContextMock.Setup(x => x.Users).Returns(mockSet.Object);

        //    //act
        //    var result = userController.GetSelectedUserDetail(1);
        //    var contentResult = result as OkNegotiatedContentResult<Ticket>;

        //    //Assert
        //    Assert.IsInstanceOfType(result, typeof(OkResult));
        //    Console.WriteLine("returned Ok Result");

        //    Assert.IsNotNull(contentResult.Content);
        //    Console.WriteLine("content is not null");

        //    Assert.AreEqual(1, contentResult.Content.TicketId);
        //    Console.WriteLine("returned ticket id for the selected user");
        //}
        //[TestMethod]
        //public void GetSelectedUserAllDetail()
        //{
        //    //arrange
        //    var train = new List<Train>
        //    {
        //        new Train{TrainId=1, StartDestination="AAA" , EndDestination="BBB", DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now, Distance = 200}
        //    }.AsQueryable();

        //    var ticketList1 = new List<Ticket>
        //    {
        //        new Ticket{TicketId =1, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()},
        //         new Ticket{TicketId =2, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()}
        //    }.AsQueryable();

        //    var ticketList2 = new List<Ticket>
        //    {
        //        new Ticket{TicketId =1, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()},
        //         new Ticket{TicketId =2, NumOfTickets = 1, BookingTime = DateTime.Now, SelectedTrain = new Train()}
        //    };

        //    var userList = new List<User>
        //    {
        //        new User{UserId =1, Name= "AAA", TicketHistory = ticketList2 },
        //        new User{UserId =2, Name= "BBB", TicketHistory = ticketList2},
        //    }.AsQueryable();

        //    var mockSet = new Mock<DbSet<User>>();
        //    mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
        //    mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

        //    var mockSetTicket = new Mock<DbSet<Ticket>>();
        //    mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(ticketList1.Provider);
        //    mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(ticketList1.Expression);
        //    mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(ticketList1.ElementType);
        //    mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(ticketList1.GetEnumerator());

        //    var mockSetTrain = new Mock<DbSet<Train>>();
        //    mockSet.As<IQueryable<Train>>().Setup(m => m.Provider).Returns(train.Provider);
        //    mockSet.As<IQueryable<Train>>().Setup(m => m.Expression).Returns(train.Expression);
        //    mockSet.As<IQueryable<Train>>().Setup(m => m.ElementType).Returns(train.ElementType);
        //    mockSet.As<IQueryable<Train>>().Setup(m => m.GetEnumerator()).Returns(train.GetEnumerator());

        //    dbContextMock.Setup(x => x.Users).Returns(mockSet.Object);
        //    dbContextMock.Setup(x => x.Tickets).Returns(mockSetTicket.Object);
        //    dbContextMock.Setup(x => x.Trains).Returns(mockSetTrain.Object);

        //    //act
        //    var ActionResult = userController.GetSelectedUserAllDetail(1);
        //    var contentResult = ActionResult as OkNegotiatedContentResult<IQueryable<Ticket>>;

        //    //Assert
        //    Assert.IsInstanceOfType(ActionResult, typeof(OkResult));
        //    Console.WriteLine("returned Ok Result");

        //    Assert.IsNotNull(contentResult.Content);
        //    Console.WriteLine("content is not null");

        //    //Assert.AreEqual(userList.Count(), contentResult.Content.Count);
        //    Console.WriteLine("returned a count of tickets for the selected user");

        //}
        #endregion

    }

}
