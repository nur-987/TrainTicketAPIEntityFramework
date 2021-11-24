using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using TrainTicket.API.Controllers;
using TrainTicket.API.Models;

namespace TrainTicket.UnitTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestInitialize]
        public void TestUserInitialize()
        {

        }

        [TestMethod]
        public void GetAllUser_ReturnedListOfUsers()
        {
            //arrange
            var controller = new UserController();

            //act
            var result = controller.GetAllUser() as List<User>;

            //assert
            Assert.AreEqual(XXX, result.Count);
            Console.WriteLine("returned correct number of users");

            Assert.IsInstanceOfType(result.GetType(), typeof(List<User>));
            Console.WriteLine("returned a List of Users");

        }

        [TestMethod]
        public void AddNewUser_ReturnsOkUser()
        {
            //arrange
            var controller = new UserController();

            //act
            var ActionResult = controller.AddNewUser("Danial");
            var contentResult = ActionResult as OkNegotiatedContentResult<User>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(ActionResult, typeof(OkResult));
            Console.WriteLine("returned Ok Result");
            Assert.AreEqual("Danial", contentResult.Content.Name);
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

            Assert.AreEqual(XXX, contentResult.Content.TicketId);
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

            Assert.AreEqual(XXX, contentResult.Content.Count);
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
