using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOne_Repository
{
    public class MenuItemRepository
    {
        private List<MenuItem> _listOfMenuItems = new List<MenuItem>();
        private int _nextMealNumber = 0;

        // Create
        public bool CreateMenuItem(MenuItem item)
        {
            if(item is null)
            {
                return false;
            }

            // Keep meal numbers unique
            item.MealNumber = _nextMealNumber;
            _nextMealNumber++;

            int before = _listOfMenuItems.Count();
            _listOfMenuItems.Add(item);
            int after = _listOfMenuItems.Count();

            if (before < after)
            {
                return true;
            }

            return false;
        }

        // Read
        public List<MenuItem> GetAllMenuItems()
        {
            return _listOfMenuItems;
        }

        public MenuItem GetMenuItemForMealNumber(int mealNumber)
        {
            if(mealNumber < 0 || _listOfMenuItems.Count == 0)
            {
                return null;
            }

            foreach(MenuItem item in _listOfMenuItems)
            {
                if(item.MealNumber == mealNumber)
                {
                    return item;
                }
            }

            return null;
        }

        // Update
        public bool UpdateMenuItemForMealNumber(int mealNumber, MenuItem newItem)
        {
            if(mealNumber < 0 || newItem is null || _listOfMenuItems.Count == 0)
            {
                return false;
            }

            MenuItem oldItem = GetMenuItemForMealNumber(mealNumber);
            if(oldItem is null)
            {
                return false;
            }

            oldItem.MealNumber = newItem.MealNumber;
            oldItem.MealName = newItem.MealName;
            oldItem.Description = newItem.Description;
            oldItem.Ingredients = newItem.Ingredients;
            oldItem.Price = newItem.Price;

            return true;
        }

        public bool AddIngredientToMealNumber(int mealNumber, string newIngredient)
        {
            // Use this method to add a new ingredient to an existing MenuItem.
            // Using this triggers the MenuItem's method that checks for duplicate ingredients.

            if(mealNumber < 0 || newIngredient is null || newIngredient == "")
            {
                return false;
            }

            MenuItem oldItem = GetMenuItemForMealNumber(mealNumber);
            if (oldItem is null)
            {
                return false;
            }

            bool success = oldItem.AddIngredient(newIngredient);

            if(success)
            {
                return true;
            }

            return false;
        }

        // Delete
        public bool DeleteMenuItem(MenuItem item)
        {
            if(item is null || _listOfMenuItems.Count == 0)
            {
                return false;
            }

            int before = _listOfMenuItems.Count();
            _listOfMenuItems.Remove(item);
            int after = _listOfMenuItems.Count();

            if (before > after)
            {
                return true;
            }

            return false;
        }

        // Helper methods (if any)
    }
}
