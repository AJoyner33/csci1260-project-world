using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    public class Parrot : Animal, ITrainable
    {
        // A concrete animal hanging directly off the root, skipping Mammal.
        private double wingSpanCm;

        public const decimal SeedCostPerKg = 6.00m;

        public double WingSpanCm { get { return wingSpanCm; } }
        public string TrickName { get { return "Word mimic"; } }

        public Parrot(string id, string name, double dailyFeedKg, double wingSpanCm) : base(id, name, dailyFeedKg)
        {
            if (wingSpanCm < 0)
                throw new ArgumentException("Wing span cannot be negative.");

            this.wingSpanCm = wingSpanCm;
        }

        public override string Category()
        {
            return "Bird";
        }

        public override decimal DailyFoodCost()
        {
            return (decimal)DailyFeedKg * SeedCostPerKg;
        }

        public string Perform()
        {
            return String.Format("{0} performs: {1}", Name, TrickName);
        }

        public override string Describe()
        {
            return base.Describe() + String.Format(", {0:N1} cm wingspan", WingSpanCm);
        }
    }
}
