using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTicket.API.Controllers;
using TrainTicket.API.Data;
using TrainTicket.API.Models;

namespace TrainTicket.UnitTest
{
    [TestClass]
    public class TrainControllerTest
    {
        private readonly TrainController trainController;
        private readonly Mock<ITrainTicketDataContext> dbContextMock = new Mock<ITrainTicketDataContext>();

        public TrainControllerTest()
        {
            trainController = new TrainController(dbContextMock.Object);
        }

        [TestMethod]
        public void DisplayAllTrain_ReturnListOfTrain()
        {
            //Arrange
            var trainList = new List<Train>
            {
                new Train {TrainId = 1, StartDestination = "Dublin", EndDestination = "Westport", Distance = 263, 
                    DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00), ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00) },
                new Train {TrainId = 2, StartDestination = "Dublin", EndDestination = "Westport", Distance = 263,
                    DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00), ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00) }
            }.AsQueryable();


            var mockSet = new Mock<DbSet<Train>>();
            mockSet.As<IQueryable<Train>>().Setup(m => m.Provider).Returns(trainList.Provider);
            mockSet.As<IQueryable<Train>>().Setup(m => m.Expression).Returns(trainList.Expression);
            mockSet.As<IQueryable<Train>>().Setup(m => m.ElementType).Returns(trainList.ElementType);
            mockSet.As<IQueryable<Train>>().Setup(m => m.GetEnumerator()).Returns(trainList.GetEnumerator());

            dbContextMock.Setup(x => x.Trains).Returns(mockSet.Object);

            //Act
            var result = trainController.DisplayAllTrain();

            //Assert
            Assert.AreEqual(2, result.Count());
            Console.WriteLine("return correct number of list");

        }

        [TestMethod]
        public void GetAllStartStation_ReturnListOfString()
        {
            //Arrange
            var trainList = new List<Train>
            {
                new Train {TrainId = 1, StartDestination = "AAA", EndDestination = "Westport", Distance = 263,
                    DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00), ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00) },
                new Train {TrainId = 2, StartDestination = "BBB", EndDestination = "Westport", Distance = 263,
                    DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00), ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00) }
            }.AsQueryable();


            var mockSet = new Mock<DbSet<Train>>();
            mockSet.As<IQueryable<Train>>().Setup(m => m.Provider).Returns(trainList.Provider);
            mockSet.As<IQueryable<Train>>().Setup(m => m.Expression).Returns(trainList.Expression);
            mockSet.As<IQueryable<Train>>().Setup(m => m.ElementType).Returns(trainList.ElementType);
            mockSet.As<IQueryable<Train>>().Setup(m => m.GetEnumerator()).Returns(trainList.GetEnumerator());

            dbContextMock.Setup(x => x.Trains).Returns(mockSet.Object);

            //Act
            var result = trainController.GetAllStartStations();

            //Assert
            Assert.IsNotNull(result);
            Console.WriteLine("returned not null");

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Console.WriteLine("returned a list");

            Assert.AreEqual(2,result.Count());
            Console.WriteLine("returned correct number or items in list");

        }

        [TestMethod]
        public void GetAllEndStation_ReturnListOfString()
        {
            //Arrange
            var trainList = new List<Train>
            {
                new Train {TrainId = 1, StartDestination = "AAA", EndDestination = "CCC", Distance = 263,
                    DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00), ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00) },
                new Train {TrainId = 2, StartDestination = "BBB", EndDestination = "DDD", Distance = 263,
                    DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00), ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00) }
            }.AsQueryable();


            var mockSet = new Mock<DbSet<Train>>();
            mockSet.As<IQueryable<Train>>().Setup(m => m.Provider).Returns(trainList.Provider);
            mockSet.As<IQueryable<Train>>().Setup(m => m.Expression).Returns(trainList.Expression);
            mockSet.As<IQueryable<Train>>().Setup(m => m.ElementType).Returns(trainList.ElementType);
            mockSet.As<IQueryable<Train>>().Setup(m => m.GetEnumerator()).Returns(trainList.GetEnumerator());

            dbContextMock.Setup(x => x.Trains).Returns(mockSet.Object);

            //Act
            var result = trainController.GetAllEndStations();

            //Assert
            Assert.IsNotNull(result);
            Console.WriteLine("returned not null");

            Assert.IsInstanceOfType(result, typeof(List<string>));
            Console.WriteLine("returned a list");

            Assert.AreEqual(2, result.Count());
            Console.WriteLine("returned correct number or items in list");

        }

        [TestMethod]
        public void GetTrainsBetweenStation_ReturnListOfTrain()
        {
            //Arrange
            var trainList = new List<Train>
            {
                new Train {TrainId = 1, StartDestination = "AAA", EndDestination = "CCC", Distance = 263,
                    DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00), ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00) },
                new Train {TrainId = 2, StartDestination = "BBB", EndDestination = "DDD", Distance = 263,
                    DepartureTime = new DateTime(2021, 12, 01, 15, 00, 00), ArrivalTime = new DateTime(2021, 12, 01, 19, 00, 00) }
            }.AsQueryable();


            var mockSet = new Mock<DbSet<Train>>();
            mockSet.As<IQueryable<Train>>().Setup(m => m.Provider).Returns(trainList.Provider);
            mockSet.As<IQueryable<Train>>().Setup(m => m.Expression).Returns(trainList.Expression);
            mockSet.As<IQueryable<Train>>().Setup(m => m.ElementType).Returns(trainList.ElementType);
            mockSet.As<IQueryable<Train>>().Setup(m => m.GetEnumerator()).Returns(trainList.GetEnumerator());

            dbContextMock.Setup(x => x.Trains).Returns(mockSet.Object);

            var start = "AAA";
            var end = "CCC";

            //Act
            var result = trainController.GetTrainsBetweenStations(start, end);

            //Assert
            Assert.IsNotNull(result);
            Console.WriteLine("returned not null");

            Assert.IsInstanceOfType(result, typeof(List<Train>));
            Console.WriteLine("returned a list");

            //Assert.AreEqual(result.Count, xxx);
            Console.WriteLine("returned correct number or items in list");

        }
    }
}
