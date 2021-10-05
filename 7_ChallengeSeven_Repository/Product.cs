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

        // Methods
        public void ExchangeTicket()
        {
            _ticketsExchanged += 1;
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
            if(newIngredient is null || Ingredients is null || Ingredients.Count == 0)
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

            return true;
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
                    return true;
                }
            }

            return false;
        }
    }
}
