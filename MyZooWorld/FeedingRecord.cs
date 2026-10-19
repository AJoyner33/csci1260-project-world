using System;
using System.Collections.Generic;
using System.Text;

namespace MyZooWorld
{
    public class FeedingRecord
    {
        private int seq;
        private double kg;

        public int Seq {  get { return seq; } }
        public double Kg {  get { return kg; } }

        public FeedingRecord(int seq, double kg)
        {
            this.seq = seq;
            this.kg = kg;
        }

        public string Describe()
        {
            return String.Format("    feeding {0}: {1:N1} kg", Seq, Kg);
        }   
    }
}
