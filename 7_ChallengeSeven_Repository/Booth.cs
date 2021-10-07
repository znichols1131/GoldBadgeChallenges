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


        // Cloning for testing
        public Booth Clone()
        {
            Booth newBooth = new Booth(Name);
            newBooth.Products.Clear();

            foreach(Product p in Products)
            {
                newBooth.AddProduct(p.Clone());
            }

            return newBooth;
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
            if (newProduct is null || Products is null)
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

            int before = Products.Count;
            Products.Add(newProduct);
            int after = Products.Count;

            if (after > before)
            {
                return true;
            }

            return false;
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
                    int before = Products.Count;
                    Products.Remove(productToDelete);
                    int after = Products.Count;

                    if (after < before)
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }

        public bool UpdateProductAtIndex(int productIndex, Product newProduct)
        {
            // Check to see if the booth already exists
            if (newProduct is null || Products is null || Products.Count == 0 || productIndex < 0)
            {
                return false;
            }

            try
            {
                Product oldProduct = Products[productIndex];
                oldProduct.Name = newProduct.Name;
                oldProduct.Ingredients = newProduct.Ingredients;
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
