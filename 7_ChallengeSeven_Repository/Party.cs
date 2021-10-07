using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Repository
{
    public class Party
    {
        public int PartyID { get; set; }
        public string Purpose { get; set; }
        public DateTime Date { get; set; }
        public List<Booth> Booths { get; set; }


        // Constructors
        public Party()
        {
            Booths = new List<Booth>();
        }

        public Party(string purpose, DateTime date)
        {
            Purpose = purpose;
            Date = date;
            Booths = new List<Booth>();
        }


        // Methods
        public double TotalCost()
        {
            double cost = 0.0d;

            foreach (Booth b in Booths)
            {
                if (!(b is null))
                {
                    cost += b.TotalCost();
                }
            }
            return cost;
        }

        public int TicketsExchanged()
        {
            int tickets = 0;

            foreach (Booth b in Booths)
            {
                if (!(b is null))
                {
                    tickets += b.TicketsExchanged();
                }
            }

            return tickets;
        }

        public bool AddBooth(Booth newBooth)
        {
            // Check to see if the booth already exists
            if (newBooth is null || Booths is null)
            {
                return false;
            }

            foreach (Booth oldBooth in Booths)
            {
                if (oldBooth.Name == newBooth.Name)
                {
                    return false;
                }
            }

            int before = Booths.Count;
            Booths.Add(newBooth);
            int after = Booths.Count;

            if(after > before)
            {
                return true;
            }

            return false;
        }

        public bool RemoveBooth(Booth boothToDelete)
        {
            // Check to see if the booth actually exists
            if (boothToDelete is null || Booths is null || Booths.Count == 0)
            {
                return false;
            }

            foreach (Booth oldBooth in Booths)
            {
                if (oldBooth.Name == boothToDelete.Name)
                {

                    int before = Booths.Count;
                    Booths.Remove(boothToDelete);
                    int after = Booths.Count;

                    if (after < before)
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }

        public bool UpdateBoothAtIndex(int boothIndex, Booth newBooth)
        {
            // Check to see if the booth already exists
            if (newBooth is null || Booths is null || Booths.Count == 0 || boothIndex < 0)
            {
                return false;
            }

            try
            {
                Booth oldBooth = Booths[boothIndex];
                oldBooth.Name = newBooth.Name;
                oldBooth.Products = newBooth.Products;
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
