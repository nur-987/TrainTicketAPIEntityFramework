﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class UserControllerTest2
    {
        //private readonly UserController userController;
        private readonly Mock<ITrainTicketDataContext> dbContextMock = new Mock<ITrainTicketDataContext>();

        //public UserControllerTest()
        //{
        //    userController = new UserController(dbContextMock.Object);
        //}

        [TestInitialize]
        public void TestUserInitialize()
        {

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

            UserController userController = new UserController(dbContextMock.Object);

            //act
            var result = userController.GetAllUser();

            //assert
            Assert.AreEqual(2, result.Count);
            Console.WriteLine("returned correct number of users");

        }

        [TestMethod]
        public void AddNewUser_ReturnsOkUser()
        {
            //arrange
            var userList = new List<User>
            {
                new User{UserId =1, Name= "AAA", TicketHistory = null},

            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());

            dbContextMock.Setup(x => x.Users).Returns(mockSet.Object);
            UserController userController = new UserController(dbContextMock.Object);

            //act
            var result = userController.AddNewUser("AAA");
            var contentResult = result as OkNegotiatedContentResult<int>;

            //Assert
            //Assert.IsNotNull(contentResult);
            //Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<int>));
            Console.WriteLine("returned Ok Result");
            Assert.AreEqual("1", contentResult.Content);
            Console.WriteLine("returned User item");

        }

        [TestMethod]
        public void AddNewUser_ReturnsBadRequest()
        {
            //arrange
            var controller = new UserController();

            //act
            var ActionResult = controller.AddNewUser(" ");

            //Assert
            Assert.IsInstanceOfType(ActionResult, typeof(BadRequestResult));
            Console.WriteLine("whitespace name, returned bad request");
        }

        [TestMethod]
        public void GetSelectedUserDetail_FoundId()
        {
            //arrange
            var controller = new UserController();

            //act
            var ActionResult = controller.GetSelectedUserDetail(1);
            var contentResult = ActionResult as OkNegotiatedContentResult<Ticket>;

            //Assert
            Assert.IsInstanceOfType(ActionResult, typeof(OkResult));
            Console.WriteLine("returned Ok Result");

            Assert.IsNotNull(contentResult.Content);
            Console.WriteLine("content is not null");

            //Assert.AreEqual(XXX, contentResult.Content.TicketId);
            Console.WriteLine("returned ticket id for the selected user");
        }

        [TestMethod]
        public void GetSelectedUserDetail_NotFoundId()
        {
            //arrange
            var controller = new UserController();

            //act
            var ActionResult = controller.GetSelectedUserDetail(100);

            //Assert
            Assert.IsInstanceOfType(ActionResult, typeof(NotFoundResult));
            Console.WriteLine("content not found, returned not found response");
        }

        [TestMethod]
        public void GetSelectedUserAllDetail()
        {
            //arrange
            var controller = new UserController();

            //act
            var ActionResult = controller.GetSelectedUserAllDetail(1);
            var contentResult = ActionResult as OkNegotiatedContentResult<IQueryable<Ticket>>;

            //Assert
            Assert.IsInstanceOfType(ActionResult, typeof(OkResult));
            Console.WriteLine("returned Ok Result");

            Assert.IsNotNull(contentResult.Content);
            Console.WriteLine("content is not null");

            //Assert.AreEqual(XXX, contentResult.Content.Count);
            Console.WriteLine("returned a count of tickets for the selected user");

        }

        [TestMethod]
        public void CheckUserExist_ReturnTrue()
        {
            //arrange
            var controller = new UserController();

            //act
            var result = controller.CheckUserExist(1);

            //Assert
            Assert.IsInstanceOfType(result.GetType(), typeof(bool));
            Console.WriteLine("returned a boolean type");
        }

    }

}
