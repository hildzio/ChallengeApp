using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChallengeApp
{
    public class SavedStudent : StudentBase
    {
        public SavedStudent(string forname, string surname) : base(forname, surname)
        {
            grades = new List<float>();
        }
        public override event LessThenTreeDelegate SendMessageLessThenThree;
        public override void AddGrade(float grade)
        {
            if (grade > 0 && grade <= 100)
            {
                grades.Add(grade);
            }
            else
            {
                throw new Exception("Invalid value.");
                throw new ArgumentException($"Invalid argument: {nameof(grade)}");
            }
        }
        public override void AddGrade(string gradeName)
        {
            float grade;
            float.TryParse(gradeName, out grade);

            if (!float.IsNaN(grade) && grade != 0)
            {
                this.grades.Add(grade);
                Console.WriteLine($"Dodano do listy ocen : " + grade);
            }
            else
            {
                Console.WriteLine("Błąd ! Nie można przekonwertować zmiennej na docelową ocenę");
            }
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
                        Console.WriteLine($"Dodano ocenę test " + gradeFloat);
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
        public override void AddGradeToFile(string grade, string fullFileName)
        {
            var gradeToFile = grade;
            var gradeEmpty = gradeToFile.Replace("+", String.Empty).Replace("-", String.Empty);
            var gradeFloat = float.Parse(gradeEmpty);

            if (gradeToFile.Length == 2 && char.IsDigit(gradeToFile[0]) && gradeToFile[0] >= '1' && gradeToFile[0] <= '6' && (gradeToFile.Contains("+") || gradeToFile.Contains("-")))
            {
                switch (gradeToFile[1])
                {
                    case '+':
                        gradeFloat += 0.50f;
                        AddToFile(gradeFloat, fullFileName);
                        if (SendMessageLessThenThree != null && gradeFloat < 3)
                        {
                            SendMessageLessThenThree(this, new EventArgs());
                        }
                        break;

                    case '-':
                        gradeFloat -= 0.25f;
                        AddToFile(gradeFloat, fullFileName);
                        if (SendMessageLessThenThree != null && gradeFloat < 3)
                        {
                            SendMessageLessThenThree(this, new EventArgs());
                        }
                        break;
                }
            }
            else if (gradeToFile.Length == 1 && char.IsDigit(gradeToFile[0]) && gradeFloat >= 1 && gradeFloat <= 6)
            {
                AddToFile(gradeFloat, fullFileName);
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
        public void AddToFile(float grade, string fullFileName)
        {
            using (var writer = File.AppendText(fullFileName))
            {
                writer.WriteLine(grade);
                Console.WriteLine($"Dopisano {grade} do pliku {fullFileName}\n ");
            }
            using (var writer1 = File.AppendText("audit.txt"))
            {
                writer1.WriteLine($"{grade}        {DateTime.UtcNow}");
                Console.WriteLine($"Dopisano {grade} do pliku audit.txt z datą {DateTime.UtcNow} \n\n");
            }
        }
        public override Statistics GetStatistics(string fullFileName)
        {
            var statistics = new Statistics();
            using (StreamReader sr = File.OpenText(fullFileName))
            {
                string gradeInString;
                while ((gradeInString = sr.ReadLine()) != null)
                {
                    var gradeInFloat = float.Parse(gradeInString);
                    statistics.Add(gradeInFloat);
                }
            }
            Console.WriteLine($"TOP **************** Statystyki ******************* TOP\n" +
                              $"Na temat ucznia mamy informacje : \n" +
                              $"Średnia ocen jest równa : {statistics.Average:N2}\n" +
                              $"Najniż1sza ocena jest równa : {statistics.Low}\n" +
                              $"Najwyższa ocena jest równa  : {statistics.High}\n" +
                              $"END ***************  Statystyki ******************* END\n\n");
            return statistics;
        }
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            return statistics;
        }
    }
}