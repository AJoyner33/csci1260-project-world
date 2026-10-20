using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    // A concrete mammal that eats meat. Does not sign ITrainable.
    public class Lion : Mammal
    {
        private int prideSize;
        public const decimal MeatCostPerKg = 9.00m;
        public int PrideSize { get { return prideSize; } }

        public Lion(string id, string name, double dailyFeedKg, int gestationDays, int prideSize) : base(id, name, dailyFeedKg, gestationDays)
        {
            if (prideSize < 0)
                throw new ArgumentException("Pride size cannot be negative.");

            this.prideSize = prideSize;
        }

        public override string Category()
        {
            return "Predator";
        }

        public override decimal DailyFoodCost()
        {
            return (decimal)DailyFeedKg * MeatCostPerKg;
        }

        public override string Describe()
        {
            return base.Describe() + String.Format(", pride of {0}", PrideSize);
        }
    }
}
