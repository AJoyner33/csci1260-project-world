using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    internal class Elephant : Mammal, ITrainable
    {
        private double tuskLengthCm;
        public const decimal HayCostPerKg = 0.80m;

        public double TuskLengthCm { get { return tuskLengthCm; } }
        public string TrickName { get { return "Balance on two feet"; } }

        public Elephant(string id, string name, double dailyFeedKg, int lifespanYears, double tuskLengthCm) : base(id, name, dailyFeedKg, lifespanYears)
        {
            if (tuskLengthCm < 0)
                throw new ArgumentException("Tusk length cannot be negative.");

            this.tuskLengthCm = tuskLengthCm;
        }

        public override string Category()
        {
            return "Grazer";
        }

        public override decimal DailyFoodCost()
        {
            return (decimal)DailyFeedKg * HayCostPerKg;
        }

        public string Perform()
        {
            return String.Format("{0} performs: {1}", Name, TrickName);
        }

        public override string Describe()
        {
            return base.Describe() + String.Format(", {0:N1} cm tusks", TuskLengthCm);
        }


    }
}
