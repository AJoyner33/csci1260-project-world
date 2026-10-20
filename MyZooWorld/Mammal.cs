using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    public abstract class Mammal : Animal
    {
        private int lifespanYears;
        public int LifespanYears { get { return lifespanYears; } }

        protected Mammal(string id, string name, double dailyFeedKg, int lifespanYears) : base(id, name, dailyFeedKg)
        {
            if (lifespanYears < 0)
                throw new ArgumentException("Lifespan cannot be negative.");

            this.lifespanYears = lifespanYears;
        }

        public override string Describe()
        {
            return base.Describe() + String.Format(", {0} yr lifespan", LifespanYears);
        }
    
    }
}
