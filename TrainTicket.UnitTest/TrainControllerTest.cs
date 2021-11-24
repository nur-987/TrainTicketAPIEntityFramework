using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.API.Controllers;
using TrainTicket.API.Models;

namespace TrainTicket.UnitTest
{
    [TestClass]
    public class TrainControllerTest
    {
        [TestInitialize]
        public void TestTrainInitialize()
        {

        }

        [TestMethod]
        public void DisplayAllTrain_ReturnListOfTrain()
        {
            //Arrange
            var controller = new TrainController();

            //Act
            var result = controller.DisplayAllTrain();

            //Assert
            Assert.AreEqual(xxx, result.Count);
            Console.WriteLine("return correct number of list");

            Assert.IsInstanceOfType(result.GetType(), typeof(List<Train>));
            Console.WriteLine("returned list of trains");

        }

        [TestMethod]
        public void GetAllStartStation_ReturnListOfString()
        {
            //Arrange
            var controller = new TrainController();

            //Act
            var result = controller.GetAllStartStations();
            //var result = actionResult as List<string>;

            //Assert
            Assert.IsNotNull(result);
            Console.WriteLine("returned not null");

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Console.WriteLine("returned a list");

            Assert.AreEqual(result.Count, xxx);
            Console.WriteLine("returned correct number or items in list");

        }

        [TestMethod]
        public void GetAllEndStation_ReturnListOfString()
        {
            //Arrange
            var controller = new TrainController();

            //Act
            var result = controller.GetAllEndStations();
            //var result = actionResult as List<string>;

            //Assert
            Assert.IsNotNull(result);
            Console.WriteLine("returned not null");

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Console.WriteLine("returned a list");

            Assert.AreEqual(result.Count, xxx);
            Console.WriteLine("returned correct number or items in list");

        }

        [TestMethod]
        public void GetTrainsBetweenStation_ReturnListOfTrain()
        {
            //Arrange
            var controller = new TrainController();
            string start = "xxx";
            string end = "yyy";

            //Act
            var result = controller.GetTrainsBetweenStations(start, end);
            //var result = actionResult as List<Train>;

            //Assert
            Assert.IsNotNull(result);
            Console.WriteLine("returned not null");

            Assert.IsInstanceOfType(result, typeof(List<Train>));
            Console.WriteLine("returned a list");

            Assert.AreEqual(result.Count, xxx);
            Console.WriteLine("returned correct number or items in list");

        }
    }
}
