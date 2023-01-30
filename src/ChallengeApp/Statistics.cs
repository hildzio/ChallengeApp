using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public class Statistics
    {
        public float High;
        public float Low;
        public float Total;
        public int Count;
        public List<float> grades { get; set; }
        public Statistics()
        {
            Count = 0;
            Total = 0.0f;
            High = float.MinValue;
            Low = float.MaxValue;
        }
        public float Average
        {
            get
            {
                return Total / Count;
            }
        }
        public void Add(float number)
        {
            Total += number;
            Count += 1;
            Low = Math.Min(number, Low);
            High = Math.Max(number, High);
        }
    }
}