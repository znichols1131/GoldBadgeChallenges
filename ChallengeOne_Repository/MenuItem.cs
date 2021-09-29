using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOne_Repository
{
    public class MenuItem
    {
        public int MealNumber { get; set; }
        public string MealName { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public double Price { get; set; }

        public MenuItem() 
        {
            Ingredients = new List<string>();
        }


        // Constructors
        public MenuItem(string mealName)
        {
            MealName = mealName;
            Ingredients = new List<string>();
        }
        public MenuItem(string mealName, double price)
        {
            MealName = mealName;
            Ingredients = new List<string>();
            Price = price;
        }
        public MenuItem(string mealName, string description, List<string>ingredients, double price)
        {
            MealName = mealName;
            Description = description;
            Ingredients = ingredients;
            Price = price;
        }

        
        // Other methods

        public bool AddIngredient(string newIngredient)
        {
            // Make sure that the ingredient doesn't already exist

            if(newIngredient is null || newIngredient == "")
            {
                return false;
            }

            foreach(string ingredient in Ingredients)
            {
                if(ingredient.ToLower().Equals(newIngredient.ToLower()))
                {
                    return false;
                }
            }

            int before = Ingredients.Count();
            Ingredients.Add(newIngredient.ToLower());
            int after = Ingredients.Count();

            if(before < after)
            {
                return true;
            }

            return false;
        }

    }
}
