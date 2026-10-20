using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    // Abstract middle of the family tree. Adds what only mammals have: a lifespan.
    // Still cannot answer Category() or DailyFoodCost(), so it stays abstract.
    public abstract class Mammal : Animal
    {
        private int gestationDays;
        public int GestationDays { get { return gestationDays; } }

        protected Mammal(string id, string name, double dailyFeedKg, int gestationDays) : base(id, name, dailyFeedKg)
        {
            if (gestationDays < 0)
                throw new ArgumentException("Gestation days cannot be negative.");

            this.gestationDays = gestationDays;
        }

        public override string Describe()
        {
            return base.Describe() + String.Format(", {0} day gestation", GestationDays);
        }
    
    }
}
