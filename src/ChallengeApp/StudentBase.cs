using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public abstract class StudentBase : Person, IStudent
    {
        private const string constFileName = "adam_kowalski.txt";
        private static string GetFileName()
        {
            return constFileName;
        }
        public List<double> grades { get; set; }
        public StudentBase(string forname, string surname) : base(forname, surname)
        {
            grades = new List<double>();
        }
        event IStudent.LessThenTreeDelegate IStudent.SendMessageLessThenThree
        {
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
        public abstract void AddGrade(double grade);
        public abstract void AddGrade(string gradeName);
        public abstract void AddGradePlus(string grade);
        public abstract void AddGradeToFile(string grade);
        public abstract Statistics GetStatistics();

    }
}