using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Repository
{
    public class Booth
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }


        // Constructors
        public Booth()
        {
            Products = new List<Product>();
        }
        public Booth(string name)
        {
            Name = name;
            Products = new List<Product>();
        }


        // Methods
        public double TotalCost()
        {
            double cost = 0.0d;

            foreach (Product p in Products)
            {
                if (!(p is null))
                {
                    cost += p.TotalCost();
                }
            }
            return cost;
        }

        public int TicketsExchanged()
        {
            int tickets = 0;

            foreach (Product p in Products)
            {
                if (!(p is null))
                {
                    tickets += p.TicketsExchanged();
                }
            }

            return tickets;
        }

        public bool AddProduct(Product newProduct)
        {
            // Check to see if the product already exists
            if (newProduct is null || Products is null || Products.Count == 0)
            {
                return false;
            }

            foreach (Product oldProduct in Products)
            {
                if (oldProduct.Name == newProduct.Name)
                {
                    return false;
                }
            }

            return true;
        }

        public bool RemoveProduct(Product productToDelete)
        {
            // Check to see if the product actually exists
            if (productToDelete is null || Products is null || Products.Count == 0)
            {
                return false;
            }

            foreach (Product oldProduct in Products)
            {
                if (oldProduct.Name == productToDelete.Name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
