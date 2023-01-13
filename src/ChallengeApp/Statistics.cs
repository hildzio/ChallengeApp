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
        public Statistics GetStatistics()
        {
            var result = new Statistics();
            for (var i = 0; i < grades.Count; i ++)
            {
                result.Add(grades[i]);
            }
            return result;   
/*
            Console.WriteLine("TOP **************** Statystyki ******************* TOP");
            Console.WriteLine("Na temat ucznia mamy informacje : ");
            Console.WriteLine($"Średnia ocen jest równa : {result.Average}");
            Console.WriteLine($"Najniż1sza ocena jest równa : {result.Low}");
            Console.WriteLine($"Najwyższa ocena jest równa  : {result.High}");
            Console.WriteLine("END ***************  Statystyki ******************* END\n");    */    
        }
    }
}