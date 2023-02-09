using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public interface IStudent
    {
        string Name { get; set; }
        string Surname { get; set; }
        public delegate void LessThenTreeDelegate(object sender, EventArgs args);
        event LessThenTreeDelegate SendMessageLessThenThree;
        Statistics GetStatistics();
        Statistics GetStatistics(string fullFileName);
    }
}