using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public class InMemoryStudent : StudentBase
    {
        private const string constFileName = "adam_kowalski.txt";
        private static string GetFileName()
        {
            return constFileName;
        }
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
        public override void AddGradeToFile(string grade)
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
        public void AddToFile(double grade)
        {
            using (var writer = File.AppendText($"{GetFileName()}"))
            {
                writer.WriteLine(grade);
                Console.WriteLine($"Dopisano {grade} do pliku {GetFileName()} ");
            }
            using (var writer1 = File.AppendText("audit.txt"))
            {
                writer1.WriteLine($"{grade}        {DateTime.UtcNow}");
                Console.WriteLine($"Dopisano {grade} do pliku audit.txt z datą {DateTime.UtcNow} ");
            }
        }

        /*public override void AddNameGradeToFile(string forname, string surname, double grade)
        {
            using (var writer = File.AppendText($"{forname}_{surname}.txt"))
            {
                writer.WriteLine(grade);
                if (SendMessageLessThenThree != null)
                {
                    SendMessageLessThenThree(this, new EventArgs());
                }
            }
        }*/
        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            for (var i = 0; i < grades.Count; i += 1)
            {
                result.Add(grades[i]);
            }
            return result;
        }
    }
}