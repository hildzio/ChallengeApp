using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public abstract class StudentBase : Person, IStudent
    {
        public StudentBase(string forname, string surname) : base(forname, surname) { } 
        
        event IStudent.LessThenTreeDelegate IStudent.SendMessageLessThenThree
        {
           // ta cz�� jest wygenerowana z automatu VS. Niestety jak tego nie by�o wyrzuca�o b��d. Nie wiem jak inaczej to zrobi�?
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }
        public delegate void LessThenTreeDelegate(object sender, EventArgs args);
        public abstract event LessThenTreeDelegate SendMessageLessThenThree;
        public Statistics GetStatistics(string fullFileName)
        {  
            var statistics = new Statistics();
            using (StreamReader sr = File.OpenText(fullFileName))
            {
                string gradeInString;
                while ((gradeInString = sr.ReadLine()) != null)
                {
                    var gradeInFloat = float.Parse(gradeInString);
                    statistics.Add(gradeInFloat);
                }
            }
            Console.WriteLine($"Na temat ucznia mamy informacje : \n" +
                              $"�rednia ocen jest r�wna : {statistics.Average:N2}\n" +
                              $"Najni�1sza ocena jest r�wna : {statistics.Low}\n" +
                              $"Najwy�sza ocena jest r�wna  : {statistics.High}\n\n");
            return statistics;
        }            
        public abstract Statistics GetStatistics();
    }
}