using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public class Statistics
    {
        public double High;
        public double Low;
        public double Total;
        public int Count;
        public List<double> grades { get; set; }
        public Statistics()
        {
            Count = 0;
            Total = 0.0;
            High = double.MinValue;
            Low = double.MaxValue;
        }
        public double Average
        {
            get
            {
                return Total / Count;
            }
        }
        public void Add(double number)
        {
            Total += number;
            Count += 1;
            Low = Math.Min(number, Low);
            High = Math.Max(number, High);
        }
    }
}