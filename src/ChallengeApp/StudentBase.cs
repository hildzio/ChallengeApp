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
        public abstract Statistics GetStatistics();
        public abstract void AddGrade(float grade);
        public abstract void AddGrade(string grade);
        public abstract void AddGradeFloatWithMsgEvent(float gradeFloat);
    }
}