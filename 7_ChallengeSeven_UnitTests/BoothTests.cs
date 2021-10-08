using _7_ChallengeSeven_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _7_ChallengeSeven_UnitTests
{
    [TestClass]
    public class BoothTests
    {
        private Booth _booth = new Booth();

        [TestInitialize]
        public void Arrange()
        {
            // Need some test data
            _booth = new Booth("Hamburger booth");
            Product hamburger = new Product("Hamburger");
            Ingredient ingredient1 = new Ingredient("Burger patty", 2.50d);
            Ingredient ingredient2 = new Ingredient("Bun", 0.50d);
            hamburger.AddIngredient(ingredient1);
            hamburger.AddIngredient(ingredient2);
            hamburger.ExchangeTickets(10);
            _booth.AddProduct(hamburger);
        }

        [TestMethod]
        public void AddProduct_ProductIsNull_ReturnFalse()
        {
            // Arrange
            Product newProduct = null;

            // Act
            bool success = _booth.AddProduct(newProduct);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddProduct_ProductAlreadyExists_ReturnFalse()
        {
            // Arrange
            Product newProduct = _booth.Products[0];

            // Act
            bool success = _booth.AddProduct(newProduct);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddProduct_ProductExists_ReturnTrue()
        {
            // Arrange
            Product newProduct = new Product("Hot dog");

            // Act
            bool success = _booth.AddProduct(newProduct);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void RemoveProduct_ProductIsNull_ReturnFalse()
        {
            // Arrange
            Product product = null;

            // Act
            bool success = _booth.RemoveProduct(product);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveProduct_ProductDoesntExist_ReturnFalse()
        {
            // Arrange
            Product product = new Product("Hot dog");

            // Act
            bool success = _booth.RemoveProduct(product);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveProduct_ProductExists_ReturnTrue()
        {
            // Arrange
            Product product = _booth.Products[0];

            // Act
            bool success = _booth.RemoveProduct(product);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void UpdateProductAtIndex_NewProductIsNull_ReturnFalse()
        {
            // Arrange
            int index = 0;
            Product product = null;

            // Act
            bool success = _booth.UpdateProductAtIndex(index, product);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateProductAtIndex_ProductIsOutOfBounds_ReturnFalse()
        {
            // Arrange
            int index = 10;
            Product product = new Product("Hot dog");

            // Act
            bool success = _booth.UpdateProductAtIndex(index, product);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateProductAtIndex_ProductExists_ReturnTrue()
        {
            // Arrange
            int index = 0;
            Product product = new Product("Hot dog");

            // Act
            bool success = _booth.UpdateProductAtIndex(index, product);

            // Assert
            Assert.IsTrue(success);
        }
    }
}
