using _7_ChallengeSeven_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _7_ChallengeSeven_UnitTests
{
    [TestClass]
    public class ProductTests
    {
        private Product _product = new Product();

        [TestInitialize]
        public void Arrange()
        {
            // Need some test data
            _product = new Product("Hamburger");
            Ingredient ingredient1 = new Ingredient("Burger patty", 2.50d);
            Ingredient ingredient2 = new Ingredient("Bun", 0.50d);
            _product.AddIngredient(ingredient1);
            _product.AddIngredient(ingredient2);
            _product.ExchangeTickets(10);
        }

        [TestMethod]
        public void ExchangeTickets_TicketsIncrease_ReturnTrue()
        {
            // Arrange
            int ticketsBefore = _product.TicketsExchanged();
            int ticketsToAdd = 10;

            // Act
            _product.ExchangeTickets(ticketsToAdd);
            int ticketsAfter = _product.TicketsExchanged();

            // Assert
            Assert.IsTrue(ticketsAfter > ticketsBefore);
            Assert.IsTrue(ticketsAfter - ticketsToAdd == ticketsBefore);
        }

        [TestMethod]
        public void ResetTickets_TicketsReset_ReturnTrue()
        {
            // Arrange
            int ticketsBefore = _product.TicketsExchanged();

            // Act
            _product.ResetTickets();
            int ticketsAfter = _product.TicketsExchanged();

            // Assert
            Assert.IsTrue(ticketsAfter == 0);
            Assert.IsTrue(ticketsAfter <= ticketsBefore);
        }

        [TestMethod]
        public void AddIngredient_IngredientIsNull_ReturnFalse()
        {
            // Arrange
            Ingredient newIngredient = null;

            // Act
            bool success = _product.AddIngredient(newIngredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddIngredient_IngredientAlreadyExists_ReturnFalse()
        {
            // Arrange
            Ingredient newIngredient = _product.Ingredients[0];

            // Act
            bool success = _product.AddIngredient(newIngredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddIngredient_IngredientExists_ReturnTrue()
        {
            // Arrange
            Ingredient newIngredient = new Ingredient("Cheese", 0.10d);

            // Act
            bool success = _product.AddIngredient(newIngredient);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void RemoveIngredient_IngredientIsNull_ReturnFalse()
        {
            // Arrange
            Ingredient ingredient = null;

            // Act
            bool success = _product.RemoveIngredient(ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveIngredient_IngredientDoesntExist_ReturnFalse()
        {
            // Arrange
            Ingredient ingredient = new Ingredient("Cheese", 0.10d);

            // Act
            bool success = _product.RemoveIngredient(ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveIngredient_IngredientExists_ReturnTrue()
        {
            // Arrange
            Ingredient ingredient = _product.Ingredients[0];

            // Act
            bool success = _product.RemoveIngredient(ingredient);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void UpdateIngredientAtIndex_NewIngredientIsNull_ReturnFalse()
        {
            // Arrange
            int index = 0;
            Ingredient ingredient = null;

            // Act
            bool success = _product.UpdateIngredientAtIndex(index, ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateIngredientAtIndex_IndexIsOutOfBounds_ReturnFalse()
        {
            // Arrange
            int index = 10;
            Ingredient ingredient = new Ingredient("Veggie patty", 1.50d);

            // Act
            bool success = _product.UpdateIngredientAtIndex(index, ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateIngredientAtIndex_IngredientExists_ReturnTrue()
        {
            // Arrange
            int index = 0;
            Ingredient ingredient = new Ingredient("Veggie patty", 1.50d);

            // Act
            bool success = _product.UpdateIngredientAtIndex(index, ingredient);

            // Assert
            Assert.IsTrue(success);
        }
    }
}
