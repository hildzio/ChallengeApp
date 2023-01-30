using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public abstract class StudentBase : Person, IStudent
    {
        public List<float> grades { get; set; }
        public StudentBase(string forname, string surname) : base(forname, surname)
        {
            grades = new List<float>();
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
        public abstract void AddGrade(float grade);
        public abstract void AddGrade(string gradeName);
        public abstract void AddGradePlus(string grade);
        public abstract void AddGradeToFile(string grade, string fullFileName);
        public abstract Statistics GetStatistics(string fullFileName);
        public abstract Statistics GetStatistics();
    }
}