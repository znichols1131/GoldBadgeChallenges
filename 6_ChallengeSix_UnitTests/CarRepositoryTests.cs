using _6_ChallengeSix_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace _6_ChallengeSix_UnitTests
{
    [TestClass]
    public class CarRepositoryTests
    {
        private CarRepository _repo = new CarRepository();

        [TestInitialize]
        public void Arrange()
        {
            // Need some test data
            Car carOne = new ElectricCar("Tesla", "Model S", 2020);
            _repo.CreateCar(carOne);
        }

        // Create
        [TestMethod]
        public void CreateCar_CarIsNull_ReturnFalse()
        {
            // Arrange
            Car car = null;

            // Act
            bool success = _repo.CreateCar(car);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void CreateCar_CarExists_ReturnTrue()
        {
            // Arrange
            Car car = new GasCar("Pontiac", "Grand Prix", 2001);

            // Act
            bool success = _repo.CreateCar(car);

            // Assert
            Assert.IsTrue(success);
        }


        // Read
        [TestMethod]
        public void GetAllCars_ListExists_ReturnList()
        {
            // Arrange (nothing on this one)

            // Act
            List<Car> list = _repo.GetAllCars();

            // Assert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void GetCarForID_CarDoesNotExist_ReturnNull()
        {
            // Arrange
            int carID = 1;

            // Act
            Car car = _repo.GetCarForID(carID);

            // Assert
            Assert.IsNull(car);
        }

        [TestMethod]
        public void GetCarForID_CarExists_ReturnItem()
        {
            // Arrange
            int carID = 0;

            // Act
            Car car = _repo.GetCarForID(carID);

            // Assert
            Assert.IsNotNull(car);
        }


        // Update
        [TestMethod]
        public void UpdateCarForID_CarDoesNotExist_ReturnFalse()
        {
            // Arrange
            int carID = 1;  // Out of range
            Car newCar = new GasCar("Pontiac", "Grand Prix", 2001);

            // Act
            bool success = _repo.UpdateCarForID(carID, newCar);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateCarForID_NewCarIsNull_ReturnFalse()
        {
            // Arrange
            int carID = 0;
            Car newCar = null;

            // Act
            bool success = _repo.UpdateCarForID(carID, newCar);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateCarForID_CarIsWrongFuelType_ReturnFalse()
        {
            // Arrange
            int carID = 0;
            Car newCar = new GasCar("Pontiac", "Grand Prix", 2001);

            // Act
            bool success = _repo.UpdateCarForID(carID, newCar);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateCarForID_CarIsUpdated_ReturnTrue()
        {
            // Arrange
            int carID = 0;
            Car newCar = new ElectricCar("Hot Wheels", "Z Mobile", 2030);

            // Act
            bool success = _repo.UpdateCarForID(carID, newCar);

            // Assert
            Assert.IsTrue(success);
        }


        // Delete
        [TestMethod]
        public void DeleteCarForID_CarDoesNotExist_ReturnFalse()
        {
            // Arrange
            int carID = 1;  // Out of range

            // Act
            bool success = _repo.DeleteCarForID(carID);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void DeleteCarForID_CarExists_ReturnTrue()
        {
            // Arrange
            int carID = 0;

            // Act
            bool success = _repo.DeleteCarForID(carID);

            // Assert
            Assert.IsTrue(success);
        }
    }
}
