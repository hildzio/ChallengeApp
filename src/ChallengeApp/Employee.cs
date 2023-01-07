using System.Collections.Generic;
using System;

namespace ChallengeApp
{
    public class Employee
    {
        public string name;
        public List<double> grades = new List<double>();
        private List<string> names = new List<string>();
        private string[] users = new string[] { "Adam", "Stefan", "Antoni", "Tomek", "Krystian", "Leszek", "Marek", "Paweł", "Piotr", "Szymon" };
        private int[] age = new int[] { 23, 34, 32, 22, 26, 60, 53, 49, 36, 70 };

        public void PairLists()
        {
            var i = 0;
            for (i = 0; i < users.Length; i++)
            {
                Console.WriteLine(users[i] + " " + age[i]);
            }
        }
        public Employee(string name)
        {
            this.name = name;
            this.grades = new List<double>();
        }
        public void AddGrade(double grade)
        {
            if (grade >= 0 && grade <= 100)
            {
                this.grades.Add(grade);
            }
            else
            {
                throw new Exception("Invalid value.");
                throw new ArgumentException($"Invalid argument: {nameof(grade)}");
            }
        }
        public void AddGrade(char grade)
        {
            switch (grade)
            {
                case 'A':
                    this.AddGrade(100);
                    break;
                case 'B':
                    this.AddGrade(80);
                    break;
                case 'C':
                    this.AddGrade(60);
                    break;
                case 'D':
                    this.AddGrade(40);
                    break;
                case 'E':
                    this.AddGrade(20);
                    break;
                default:
                    this.AddGrade(0);
                    break;
            }
        }

        public void AddGrade(string input)
        {
            if (double.TryParse(input, out var grade))
            {
                this.grades.Add(grade);
                Console.WriteLine($"Dodano do listy ocen : " + grade);
            }
            else
            {
                Console.WriteLine("Błąd ! Nie można przekonwertować zmiennej na docelową ocenę");
            }
        }

        public void AddGradePlus(string input)
        {
            var clearedInput = input.Replace("+", string.Empty).Replace("-", string.Empty);
            var grade = double.Parse(clearedInput);

            char sign;

            if (input.Contains('+'))
            {
                sign = '+';
            }
            else if (input.Contains('-'))
            {
                sign = '-';
            }
            else
            {
                sign = new char();
            }

            switch (sign)
            {
                case '+':
                    grade += 0.5;
                    Console.WriteLine($"Dodano ocenę " + input);
                    break;
                case '-':
                    grade -= 0.25;

                    Console.WriteLine($"Dodano ocenę " + input);
                    break;
            }

            if (0 < grade && grade <= 6)
            {
                AddGrade(grade);
            }
            else
            {
                Console.WriteLine("Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem (1 - 6). Spróbuj jeszcze raz.");
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Average = 0.0;
            result.High = double.MinValue;
            result.Low = double.MaxValue;

            for (var index = 0; index < grades.Count; index++)
            {
                result.Low = Math.Min(grades[index], result.Low);
                result.High = Math.Max(grades[index], result.High);
                result.Average += grades[index];
            };
            result.Average /= grades.Count;
            return result;
        }
    }
}