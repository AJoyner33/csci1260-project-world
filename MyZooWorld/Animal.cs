using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    public abstract class Animal
    {
        private string id;
        private string name;
        private double dailyFeedKg;
        private List <FeedingRecord> history; 
        private int nextSeq;

        public string Id { get { return id; } }
        public string Name { get { return name; } }
        public double DailyFeedKg { get { return dailyFeedKg; } }
        public int FeedCount { get { return history.Count; } }

        protected Animal(string id, string name, double dailyFeedKg)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Animal id cannot be blank.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Animal name cannot be blank.");
            if (dailyFeedKg < 0)
                throw new ArgumentException("Daily feed cannot be negative.");

            this.id = id;
            this.name = name;
            this.dailyFeedKg = dailyFeedKg;
            history = new List<FeedingRecord>();
            nextSeq = 1;
        }

        public abstract string Category();
        public abstract decimal DailyFoodCost();

        public decimal MonthlyFoodCost()
        {
            return DailyFoodCost() * 30;
        }

        public bool Feed(double kg)
        {
            if (kg <= 0)
                return false;

            history.Add(new FeedingRecord(nextSeq, kg));
            nextSeq++;
            return true;
        }

        public string FeedingLines()
        {
            string text = "";
            for (int i = 0; i < history.Count; i++)
            {
                if (i > 0)
                    text += "\n";
                text += history[i].Describe();
            }
            return text;
        }

        public virtual string Describe()
        {
            return String.Format("{0} {1} ({2})", Id, Name, Category());
        }

        public override string ToString()
        {
            return Describe();  
        }
    }

}
