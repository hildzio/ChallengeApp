using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChallengeApp
{
    public class SavedStudent : StudentBase
    {
        private const string constFileName = "grades.txt";
        private string fullFileName = string.Empty;
        public string GetFileName()
        {
            return fullFileName;
        }
        public SavedStudent(string name, string surname)
            : base(name, surname)
        {
            fullFileName = $"{name}_{surname}_{constFileName}";
        }
        public override event LessThenTreeDelegate SendMessageLessThenThree;
        public override void AddGrade(float grade)
        {
            try
            {
                if (grade >= 0 && grade <= 7)
                {
                    AddGradeFloatWithMsgEvent(grade);
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"Wprowadź poprawny format oceny.Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem(1 - 6).Spróbuj jeszcze raz.");
            }
        }
        public override void AddGrade(string grade)
        {
            var gradeToFile = grade;
            var gradeEmpty = gradeToFile.Replace("+", String.Empty).Replace("-", String.Empty);
            var gradeFloat = float.Parse(gradeEmpty);
            try
            {
                if (gradeToFile.Length == 2 && char.IsDigit(gradeToFile[0]) && gradeToFile[0] >= '1' && gradeToFile[0] <= '6' && (gradeToFile.Contains("+") || gradeToFile.Contains("-")))
                {
                    switch (gradeToFile[1])
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
                else if (gradeToFile.Length == 1 && char.IsDigit(gradeToFile[0]) && gradeFloat >= 1 && gradeFloat <= 6)
                {
                    AddGradeFloatWithMsgEvent(gradeFloat);
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem(1 - 6).Spróbuj jeszcze raz.");
            }
        }
        public void AddToFile(float grade)
        {
            using (var writer = File.AppendText($"{fullFileName}"))
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
        public override void AddGradeFloatWithMsgEvent(float gradeFloat)
        {
            AddToFile(gradeFloat);
            Console.WriteLine($"Dodano ocenę : {gradeFloat}\n");
            if (SendMessageLessThenThree != null && gradeFloat < 3)
            {
                SendMessageLessThenThree(this, new EventArgs());
            }
        }
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            using (StreamReader sr = File.OpenText($"{fullFileName}"))
            {
                string gradeInString;
                while ((gradeInString = sr.ReadLine()) != null)
                {
                    var gradeInFloat = float.Parse(gradeInString);
                    statistics.Add(gradeInFloat);
                }
            }
            Console.WriteLine($"Na temat ucznia mamy informacje : \n" +
                              $"Średnia ocen jest równa : {statistics.Average:N2}\n" +
                              $"Najniż1sza ocena jest równa : {statistics.Low}\n" +
                              $"Najwyższa ocena jest równa  : {statistics.High}\n\n");
            return statistics;
        }
    }
}