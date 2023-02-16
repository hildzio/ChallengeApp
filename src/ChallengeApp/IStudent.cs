using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public interface IStudent
    {
        string Name { get; set; }
        string Surname { get; set; }
        delegate void LessThenTreeDelegate(object sender, EventArgs args);
        event LessThenTreeDelegate SendMessageLessThenThree;
        void AddGrade(float grade);
        void AddGrade(string grade);
        void AddGradeFloatWithMsgEvent(float gradeFloat);
        Statistics GetStatistics();
    }
}