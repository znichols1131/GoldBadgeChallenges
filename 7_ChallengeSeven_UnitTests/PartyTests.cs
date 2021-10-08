using _7_ChallengeSeven_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _7_ChallengeSeven_UnitTests
{
    [TestClass]
    public class PartyTests
    {
        private Party _party = new Party();

        [TestInitialize]
        public void Arrange()
        {
            // Need some test data
            _party = new Party("Zach's graduation", DateTime.Now);
            Booth booth1 = new Booth("Hamburger booth");
            Product hamburger = new Product("Hamburger");
            Ingredient ingredient1 = new Ingredient("Burger patty", 2.50d);
            Ingredient ingredient2 = new Ingredient("Bun", 0.50d);
            hamburger.AddIngredient(ingredient1);
            hamburger.AddIngredient(ingredient2);
            hamburger.ExchangeTickets(10);
            booth1.AddProduct(hamburger);
            _party.AddBooth(booth1);
        }

        [TestMethod]
        public void AddBooth_BoothIsNull_ReturnFalse()
        {
            // Arrange
            Booth newBooth = null;

            // Act
            bool success = _party.AddBooth(newBooth);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddBooth_BoothAlreadyExists_ReturnFalse()
        {
            // Arrange
            Booth newBooth = _party.Booths[0];

            // Act
            bool success = _party.AddBooth(newBooth);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddBooth_BoothExists_ReturnTrue()
        {
            // Arrange
            Booth newBooth = new Booth("Hot dog stand");

            // Act
            bool success = _party.AddBooth(newBooth);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void RemoveBooth_BoothIsNull_ReturnFalse()
        {
            // Arrange
            Booth booth = null;

            // Act
            bool success = _party.RemoveBooth(booth);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveBooth_BoothDoesntExist_ReturnFalse()
        {
            // Arrange
            Booth booth = new Booth("Hot dog stand");

            // Act
            bool success = _party.RemoveBooth(booth);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveBooth_BoothExists_ReturnTrue()
        {
            // Arrange
            Booth booth = _party.Booths[0];

            // Act
            bool success = _party.RemoveBooth(booth);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void UpdateBoothAtIndex_NewBoothIsNull_ReturnFalse()
        {
            // Arrange
            int index = 0;
            Booth booth = null;

            // Act
            bool success = _party.UpdateBoothAtIndex(index, booth);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateBoothAtIndex_BoothIsOutOfBounds_ReturnFalse()
        {
            // Arrange
            int index = 10;
            Booth booth = new Booth("Hot dog stand");

            // Act
            bool success = _party.UpdateBoothAtIndex(index, booth);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateBoothAtIndex_BoothExists_ReturnTrue()
        {
            // Arrange
            int index = 0;
            Booth booth = new Booth("Hot dog stand");

            // Act
            bool success = _party.UpdateBoothAtIndex(index, booth);

            // Assert
            Assert.IsTrue(success);
        }
    }
}
