using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public class InMemoryStudent : StudentBase
    {
        public InMemoryStudent(string forname, string surname) : base(forname, surname)
        {
            grades = new List<double>();
        }
        public override event LessThenTreeDelegate SendMessageLessThenThree;
        public override void AddGrade(double grade)
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
            Double.TryParse(gradeName, out var grade);

            if (!Double.IsNaN(grade) && grade != 0)
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
            var gradeDouble = double.Parse(gradeEmpty);

            if (grade.Length == 2 && char.IsDigit(grade[0]) && grade[0] >= '1' && grade[0] <= '6' && (grade.Contains("+") || grade.Contains("-")))
            {
                switch (grade[1])
                {
                    case '+':
                        gradeDouble += 0.5;
                        AddGrade(gradeDouble);
                        Console.WriteLine($"Dodano ocenę : " + gradeDouble);
                        if (SendMessageLessThenThree != null && gradeDouble < 3)
                        {
                            SendMessageLessThenThree(this, new EventArgs());
                        }
                        break;

                    case '-':
                        gradeDouble -= 0.25;
                        AddGrade(gradeDouble);
                        Console.WriteLine($"Dodano ocenę " + gradeDouble);
                        if (SendMessageLessThenThree != null && gradeDouble < 3)
                        {
                            SendMessageLessThenThree(this, new EventArgs());
                        }
                        break;
                }
            }
            else if (grade.Length == 1 && char.IsDigit(grade[0]) && gradeDouble >= 1 && gradeDouble <= 6)
            {
                AddGrade(grade);
                if (SendMessageLessThenThree != null && gradeDouble < 3)
                {
                    SendMessageLessThenThree(this, new EventArgs());
                }
            }
            else
            {
                Console.WriteLine("Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem (1 - 6). Spróbuj jeszcze raz.");
            }
        }
        public override void AddGradeToFile(string grade, string fullFileName)
        {
            var gradeToFile = Console.ReadLine();
            var gradeEmpty = gradeToFile.Replace("+", String.Empty).Replace("-", String.Empty);
            var gradeDouble = double.Parse(gradeEmpty);

            if (gradeToFile.Length == 2 && char.IsDigit(gradeToFile[0]) && gradeToFile[0] >= '1' && gradeToFile[0] <= '6' && (gradeToFile.Contains("+") || gradeToFile.Contains("-")))
            {
                switch (gradeToFile[1])
                {
                    case '+':
                        gradeDouble += 0.5;
                        AddToFile(gradeDouble);
                        Console.WriteLine($"Dodano ocenę test " + gradeDouble);
                        if (SendMessageLessThenThree != null && gradeDouble < 3)
                        {
                            SendMessageLessThenThree(this, new EventArgs());
                        }
                        break;

                    case '-':
                        gradeDouble -= 0.25;
                        AddToFile(gradeDouble);
                        Console.WriteLine($"Dodano ocenę " + gradeDouble);
                        if (SendMessageLessThenThree != null && gradeDouble < 3)
                        {
                            SendMessageLessThenThree(this, new EventArgs());
                        }
                        break;
                }
            }
            else if (gradeToFile.Length == 1 && char.IsDigit(gradeToFile[0]) && gradeDouble >= 1 && gradeDouble <= 6)
            {
                AddToFile(gradeDouble);

                if (SendMessageLessThenThree != null && gradeDouble < 3)
                {
                    SendMessageLessThenThree(this, new EventArgs());
                }
            }
            else
            {
                Console.WriteLine("Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem (1 - 6). Spróbuj jeszcze raz.");
            }
        }
        public static void AddToFile(double grade) { }
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            for (var i = 0; i < grades.Count; i++)
            {
                statistics.Add(grades[i]);
            }
            Console.WriteLine($"TOP **************** Statystyki ******************* TOP\n" +
                              $"Na temat ucznia mamy informacje : \n" +
                              $"Średnia ocen jest równa : {statistics.Average}\n" +
                              $"Najniż1sza ocena jest równa : {statistics.Low}\n" +
                              $"Najwyższa ocena jest równa  : {statistics.High}\n" +
                              $"END ***************  Statystyki ******************* END\n\n");
            return statistics;
        }
        public override Statistics GetStatistics(string fullFileName)
        {
            var statistics = new Statistics();
            return statistics;
        }
    }
}