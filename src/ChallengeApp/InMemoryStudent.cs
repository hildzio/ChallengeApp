using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public class InMemoryStudent : StudentBase
    {
        public InMemoryStudent(string forname, string surname) : base(forname, surname)
        {
            grades = new List<float>();
        }
        public override event LessThenTreeDelegate SendMessageLessThenThree;
        public override void AddGrade(float grade)
        {
            if (grade >= 0 && grade <= 100)
            {
                grades.Add(grade);
                if (SendMessageLessThenThree != null)
                {
                    SendMessageLessThenThree(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("Invalid value.");
                throw new ArgumentException($"Invalid argument: {nameof(grade)}");
            }
        }
        public override void AddGrade(string gradeName)
        {
            float.TryParse(gradeName, out var grade);

            if (!float.IsNaN(grade) && grade != 0)
            {
                this.grades.Add(grade);
                Console.WriteLine($"Dodano do listy ocen : " + grade);
                return;
            }
            Console.WriteLine("Błąd ! Nie można przekonwertować zmiennej na docelową ocenę");
        }
        public override void AddGradePlus(string grade)
        {
            var gradeEmpty = grade.Replace("+", String.Empty).Replace("-", String.Empty);
            var gradeFloat = float.Parse(gradeEmpty);

            if (grade.Length == 2 && char.IsDigit(grade[0]) && grade[0] >= '1' && grade[0] <= '6' && (grade.Contains("+") || grade.Contains("-")))
            {
                switch (grade[1])
                {
                    case '+':
                        gradeFloat += 0.50f;
                        AddGrade(gradeFloat);
                        Console.WriteLine($"Dodano ocenę : " + gradeFloat);
                        if (SendMessageLessThenThree != null && gradeFloat < 3)
                        {
                            SendMessageLessThenThree(this, new EventArgs());
                        }
                        break;

                    case '-':
                        gradeFloat -= 0.25f;
                        AddGrade(gradeFloat);
                        Console.WriteLine($"Dodano ocenę " + gradeFloat);
                        if (SendMessageLessThenThree != null && gradeFloat < 3)
                        {
                            SendMessageLessThenThree(this, new EventArgs());
                        }
                        break;
                }
            }
            else if (grade.Length == 1 && char.IsDigit(grade[0]) && gradeFloat >= 1 && gradeFloat <= 6)
            {
                AddGrade(grade);
                if (SendMessageLessThenThree != null && gradeFloat < 3)
                {
                    SendMessageLessThenThree(this, new EventArgs());
                }
            }
            else
            {
                Console.WriteLine("Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem (1 - 6). Spróbuj jeszcze raz.");
            }
        }
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            for (var i = 0; i < grades.Count; i++)
            {
                statistics.Add(grades[i]);
            }
            Console.WriteLine($"TOP **************** Statystyki ******************* TOP\n" +
                              $"Na temat ucznia mamy informacje : \n" +
                              $"Średnia ocen jest równa : {statistics.Average:N2}\n" +
                              $"Najniż1sza ocena jest równa : {statistics.Low}\n" +
                              $"Najwyższa ocena jest równa  : {statistics.High}\n" +
                              $"END ***************  Statystyki ******************* END\n\n");
            return statistics;
        }
        public override void AddGradeToFile(string grade, string fullFileName) { }
        public static void AddToFile(float grade) { }
        public override Statistics GetStatistics(string fullFileName)
        {
            var statistics = new Statistics();
            return statistics;
        }
    }
}