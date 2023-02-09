using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChallengeApp
{
    public class SavedStudent : StudentBase
    {
        public SavedStudent(string forname, string surname) : base(forname, surname) { }

        public override event LessThenTreeDelegate SendMessageLessThenThree;
        public void AddGradeToFile(string grade, string fullFileName)
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
                            AddToFile(gradeFloat, fullFileName);
                            IfWithMsgEvent(gradeFloat);
                            break;

                        case '-':
                            gradeFloat -= 0.25f;
                            AddToFile(gradeFloat, fullFileName);
                            IfWithMsgEvent(gradeFloat);
                            break;
                    }
                }
                else if (gradeToFile.Length == 1 && char.IsDigit(gradeToFile[0]) && gradeFloat >= 1 && gradeFloat <= 6)
                {
                    AddToFile(gradeFloat, fullFileName);
                    IfWithMsgEvent(gradeFloat);
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem(1 - 6).Spróbuj jeszcze raz.");
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
            return statistics;
        }
    }
}