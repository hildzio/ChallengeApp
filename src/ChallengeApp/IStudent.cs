using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public interface IStudent
    {
        string Forname { get; set; }
        string Surname { get; set; }
        public delegate void LessThenTreeDelegate(object sender, EventArgs args);
        event LessThenTreeDelegate SendMessageLessThenThree;
        void AddGrade(float grade);
        void AddGrade(string gradeName);
        void AddGradePlus(string grade);
        Statistics GetStatistics();
        Statistics GetStatistics(string fullFileName);
    }
}