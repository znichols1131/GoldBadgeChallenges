using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Repository
{
    public class Product
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        private int _ticketsExchanged { get; set; }

        // Constructors
        public Product() 
        {
            Ingredients = new List<Ingredient>();
            _ticketsExchanged = 0;
        }

        public Product(string name)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            _ticketsExchanged = 0;
        }

        // Cloning for testing
        public Product Clone()
        {
            Product newProduct = new Product(Name);
            newProduct.Ingredients = Ingredients;
            newProduct.ExchangeTickets(_ticketsExchanged);
            return newProduct;
        }

        // Methods
        public void ExchangeTickets(int tickets)
        {
            _ticketsExchanged += tickets;
        }

        public int TicketsExchanged()
        {
            return _ticketsExchanged;
        }

        public double CostPerProduct()
        {
            double cost = 0.0d;

            foreach(Ingredient i in Ingredients)
            {
                if(!(i is null))
                {
                    cost += i.Cost;
                }
            }

            return cost;
        }

        public double TotalCost()
        {
            double costPerProduct = CostPerProduct();
            return costPerProduct * _ticketsExchanged;
        }

        public bool AddIngredient(Ingredient newIngredient)
        {
            // Check to see if the ingredient already exists
            if(newIngredient is null || Ingredients is null)
            {
                return false;
            }

            foreach(Ingredient oldIngredient in Ingredients)
            {
                if(oldIngredient.Name == newIngredient.Name)
                {
                    return false;
                }
            }

            int before = Ingredients.Count;
            Ingredients.Add(newIngredient);
            int after = Ingredients.Count;

            if (after > before)
            {
                return true;
            }

            return false;
        }

        public bool RemoveIngredient(Ingredient ingredientToDelete)
        {
            // Check to see if the ingredient actually exists
            if (ingredientToDelete is null || Ingredients is null || Ingredients.Count == 0)
            {
                return false;
            }

            foreach (Ingredient oldIngredient in Ingredients)
            {
                if (oldIngredient.Name == ingredientToDelete.Name)
                {
                    int before = Ingredients.Count;
                    Ingredients.Remove(ingredientToDelete);
                    int after = Ingredients.Count;

                    if (after < before)
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }

        public bool UpdateIngredientAtIndex(int ingredientIndex, Ingredient newIngredient)
        {
            // Check to see if the booth already exists
            if (newIngredient is null || Ingredients is null || Ingredients.Count == 0 || ingredientIndex < 0)
            {
                return false;
            }

            try
            {
                Ingredient oldIngredient = Ingredients[ingredientIndex];
                oldIngredient.Name = newIngredient.Name;
                oldIngredient.Cost = newIngredient.Cost;
                return true;
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
