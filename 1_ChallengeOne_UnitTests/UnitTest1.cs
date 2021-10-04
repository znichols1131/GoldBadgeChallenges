using ChallengeOne_Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _1_ChallengeOne_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly MenuItemRepository _repo = new MenuItemRepository();

        [TestInitialize]
        public void Arrange()
        {
            // Need some test data
            List<string> ingredientListOne = new List<string>();
            ingredientListOne.Add("buns");
            ingredientListOne.Add("burger patty");
            ingredientListOne.Add("ketchup");
            MenuItem itemOne = new MenuItem("Hamburger", "Plain ol' hamburger", ingredientListOne, 4.99d);
            _repo.CreateMenuItem(itemOne);
        }

        // Create
        [TestMethod]
        public void CreateMenuItem_ItemIsNull_ReturnFalse()
        {
            // Arrange
            MenuItem item = null;

            // Act
            bool success = _repo.CreateMenuItem(item);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void CreateMenuItem_ItemExists_ReturnTrue()
        {
            // Arrange
            MenuItem item = new MenuItem("Hamburger", 7.99d);

            // Act
            bool success = _repo.CreateMenuItem(item);

            // Assert
            Assert.IsTrue(success);
        }


        // Read
        [TestMethod]
        public void GetAllMenuItems_ListExists_ReturnList()
        {
            // Arrange (nothing on this one)

            // Act
            List<MenuItem> list = _repo.GetAllMenuItems();

            // Assert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void GetMenuItemForMealNumber_ItemDoesNotExist_ReturnNull()
        {
            // Arrange
            int mealNumber = 2;

            // Act
            MenuItem item = _repo.GetMenuItemForMealNumber(mealNumber);

            // Assert
            Assert.IsNull(item);
        }

        [TestMethod]
        public void GetMenuItemForMealNumber_ItemExists_ReturnItem()
        {
            // Arrange
            int mealNumber = 1; // We initialize with at least one menu item, I chose to start MealNumber at 1 and not 0.

            // Act
            MenuItem item = _repo.GetMenuItemForMealNumber(mealNumber);

            // Assert
            Assert.IsNotNull(item);
        }


        // Update
        [TestMethod]
        public void UpdateMenuItemForMealNumber_ItemDoesNotExist_ReturnFalse()
        {
            // Arrange
            int mealNumber = 2;
            MenuItem newItem = new MenuItem("Hamburger", 7.99d);

            // Act
            bool success = _repo.UpdateMenuItemForMealNumber(mealNumber, newItem);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateMenuItemForMealNumber_NewItemIsNull_ReturnFalse()
        {
            // Arrange
            int mealNumber = 1;
            MenuItem newItem = null;

            // Act
            bool success = _repo.UpdateMenuItemForMealNumber(mealNumber, newItem);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void UpdateMenuItemForMealNumber_ItemIsUpdated_ReturnTrue()
        {
            // Arrange
            int mealNumber = 1;
            MenuItem newItem = new MenuItem("Hamburger", 7.99d);

            // Act
            bool success = _repo.UpdateMenuItemForMealNumber(mealNumber, newItem);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void AddIngredientToMealNumber_ItemDoesNotExist_ReturnFalse()
        {
            // Arrange
            int mealNumber = 2;
            string ingredient = "cheese";

            // Act
            bool success = _repo.AddIngredientToMealNumber(mealNumber, ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddIngredientToMealNumber_IngredientIsNull_ReturnFalse()
        {
            // Arrange
            int mealNumber = 1;
            string ingredient = null;

            // Act
            bool success = _repo.AddIngredientToMealNumber(mealNumber, ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddIngredientToMealNumber_IngredientAlreadyExists_ReturnFalse()
        {
            // Arrange
            int mealNumber = 1;
            string ingredient = "buns";

            // Act
            bool success = _repo.AddIngredientToMealNumber(mealNumber, ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void AddIngredientToMealNumber_IngredientAdded_ReturnTrue()
        {
            // Arrange
            int mealNumber = 1;
            string ingredient = "cheese";

            // Act
            bool success = _repo.AddIngredientToMealNumber(mealNumber, ingredient);

            // Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void RemoveIngredientFromMealNumber_ItemDoesNotExist_ReturnFalse()
        {
            // Arrange
            int mealNumber = 2;
            string ingredient = "buns";

            // Act
            bool success = _repo.RemoveIngredientFromMealNumber(mealNumber, ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveIngredientFromMealNumber_IngredientIsNull_ReturnFalse()
        {
            // Arrange
            int mealNumber = 1;
            string ingredient = null;

            // Act
            bool success = _repo.RemoveIngredientFromMealNumber(mealNumber, ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveIngredientFromMealNumber_IngredientDoesNotExist_ReturnFalse()
        {
            // Arrange
            int mealNumber = 1;
            string ingredient = "fork";

            // Act
            bool success = _repo.RemoveIngredientFromMealNumber(mealNumber, ingredient);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void RemoveIngredientFromMealNumber_IngredientRemoved_ReturnTrue()
        {
            // Arrange
            int mealNumber = 1;
            string ingredient = "buns";

            // Act
            bool success = _repo.RemoveIngredientFromMealNumber(mealNumber, ingredient);

            // Assert
            Assert.IsTrue(success);
        }


        // Delete
        [TestMethod]
        public void DeleteMenuItemForMealNumber_ItemDoesNotExist_ReturnFalse()
        {
            // Arrange
            int mealNumber = 2;

            // Act
            bool success = _repo.DeleteMenuItemForMealNumber(mealNumber);

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void DeleteMenuItemForMealNumber_ItemExists_ReturnTrue()
        {
            // Arrange
            int mealNumber = 1;

            // Act
            bool success = _repo.DeleteMenuItemForMealNumber(mealNumber);

            // Assert
            Assert.IsTrue(success);
        }
    }
}
