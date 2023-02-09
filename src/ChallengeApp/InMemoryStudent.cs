using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ChallengeApp
{
    public class InMemoryStudent : StudentBase
    {
        public List<float> grades { get; set; }
        public InMemoryStudent(string forname, string surname) : base(forname, surname)
        {
            grades = new List<float>();
        }
        public override event LessThenTreeDelegate SendMessageLessThenThree;
        public void AddGrade(float grade)
        {
            try
            {
                if (grade >= 0 && grade <= 7)
                {
                    grades.Add(grade);
                    IfWithMsgEvent(grade);
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Wprowadź poprawny format oceny.Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem(1 - 6).Spróbuj jeszcze raz.");
            }
        }
        public void AddGrade(string grade)
        {
            try
            {
                var gradeEmpty = grade.Replace("+", String.Empty).Replace("-", String.Empty);
                var gradeFloat = float.Parse(gradeEmpty);

                if (grade.Length == 2 && char.IsDigit(grade[0]) && grade[0] >= '1' && grade[0] <= '6' && (grade.Contains("+") || grade.Contains("-")))
                {
                    switch (grade[1])
                    {
                        case '+':
                            gradeFloat += 0.50f;
                            AddGradeFloatWithMsgEvent(gradeFloat);
                            break;

                        case '-':
                            gradeFloat -= 0.25f;
                            AddGradeFloatWithMsgEvent(gradeFloat);
                            break;
                    }
                }
                else if (grade.Length == 1 && char.IsDigit(grade[0]) && gradeFloat >= 1 && gradeFloat <= 6)
                {
                    var parsedGrade = float.Parse(grade);
                    AddGrade(parsedGrade);
                    IfWithMsgEvent(gradeFloat);
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Wprowadź poprawny format oceny.Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem(1 - 6).Spróbuj jeszcze raz.");
            }
        }
        public void AddGradeFloatWithMsgEvent(float gradeFloat)
        {
            AddGrade(gradeFloat);
            Console.WriteLine($"Dodano ocenę : {gradeFloat}\n");
            IfWithMsgEvent(gradeFloat);
        }
        public void IfWithMsgEvent(float gradeFloat)
        {
            if (SendMessageLessThenThree != null && gradeFloat < 3)
            {
                SendMessageLessThenThree(this, new EventArgs());
            }
        }
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            for (var i = 0; i < grades.Count; i++)
            {
                statistics.Add(grades[i]);
            }
            Console.WriteLine($"Na temat ucznia mamy informacje : \n" +
                              $"Średnia ocen jest równa : {statistics.Average:N2}\n" +
                              $"Najniż1sza ocena jest równa : {statistics.Low}\n" +
                              $"Najwyższa ocena jest równa  : {statistics.High}\n\n");
            return statistics;
        }
    }
}