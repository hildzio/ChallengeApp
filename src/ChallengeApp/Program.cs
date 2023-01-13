using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChallengeApp
{
    class Program
    {
        private const string constFileName = "grades.txt";
        private static string GetFileName()
        {
            return constFileName;
        }
        static void Main(string[] args)
        {
            var quitApp = false;
            var backFromFile = false;
            var backFromMemory = false;

            Console.WriteLine("Witaj !\n" +
                              "Jest to aplikacja przechowująca oceny wprowadzonego ucznia oraz wylicza jego statystyki. Taki elektroniczny dzienniczek.\n\n" +
                              "Podaj imię studenta któremu chcesz wystawić ocenę: ");
            var inputForname = Console.ReadLine();

            Console.WriteLine("Teraz podaj jego nazwisko: ");
            var inputSurname = Console.ReadLine();

            var inMemoryStudent = new InMemoryStudent(inputForname, inputSurname);
            var fullName = $"{inputForname} {inputSurname}";
            var fullFileName = $"{inputForname}_{inputSurname}_{GetFileName()}";

            while (!quitApp)
            {
                Console.WriteLine($"Wybierz gdzie chcesz dodać oceny, a następnie wyświetlić statystyki ucznia {inputForname} {inputSurname}");
                Console.WriteLine("________________________________________\n" +
                                  "|                Menu :                |\n" +
                                  "| file - zapis danych do pliku         |\n" +
                                  "| memory - zapis danych do pamięci     |\n" +
                                  "| quit - wyjście                       |\n" +
                                  "|______________________________________|\n\n");
                var inputMenu = Console.ReadLine().ToLower();
                if (inputMenu == "file" || inputMenu == "memory" || inputMenu == "quit")
                {
                    switch (inputMenu)
                    {
                        case "file":
                            while (!backFromFile)
                            {
                                var savedStudent = new SavedStudent(inputForname, inputSurname);
                                Console.WriteLine("________________________________________\n" +
                                                  "|    Menu dla zapisu ocen do pliku :   |\n" +
                                                  "| add - dodaj oceny                    |\n" +
                                                  "| stats - statystyki                   |\n" +
                                                  "| grades - lista ocen                  |\n" +
                                                  "| back - powrót do poprzedniego menu   |\n" +
                                                  "|______________________________________|\n\n");

                                Console.WriteLine("Wybierz/napisz opcję z menu i naciśnij Enter : ");
                                var inputSubMenu = Console.ReadLine().ToLower();
                                if (inputSubMenu == "add" || inputSubMenu == "stats" || inputSubMenu == "grades" || inputSubMenu == "back")
                                {
                                    switch (inputSubMenu)
                                    {
                                        case "add":
                                            var backToMenu = false;
                                            while (!backToMenu)
                                            {
                                                Console.WriteLine($"Podaj ocenę od 1 do 6 z lub bez +/- dla {fullName} \n" +
                                                                                       " lub naciśnij q aby wrócić : ");
                                                string inputToFile = Console.ReadLine();
                                                if (inputToFile == "q")
                                                {
                                                    backToMenu = true;
                                                    break;
                                                }
                                                else if (inputToFile.Length > 0 && inputToFile.Length <= 2 && char.IsDigit(inputToFile[0]))
                                                {
                                                    savedStudent.AddGradeToFile(inputToFile);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem (1 - 6) lu nie jest literą 'q'. Spróbuj jeszcze raz.");
                                                }
                                            }
                                            break;

                                        case "stats":
                                            var stats = new Statistics();
                                            stats.GetStatistics();
                                            break;

                                        case "grades":
                                            Console.WriteLine($"Dla studenta {savedStudent.Forname} {savedStudent.Surname} przypisane są oceny : ");
                                            using (StreamReader sr = File.OpenText("adam_kowalski.txt"/*fullFileName*/))
                                            {
                                                var line = sr.ReadLine();
                                                StringBuilder GradesList = new StringBuilder();

                                                while (line != null)
                                                {
                                                    GradesList.Append($"{line}, ");
                                                    line = sr.ReadLine();
                                                }
                                                Console.WriteLine($"{GradesList}");
                                            }
                                            break;

                                        case "back":
                                            backFromFile = true;
                                            break;
                                    }
                                }
                                Console.WriteLine("Wprowadź poprawne dane wybrane z menu i naciśnij Enter. Spróbuj jeszcze raz.");
                            }
                            break;

                        case "memory":
                            while (!backFromMemory)
                            {
                                Console.WriteLine("________________________________________\n" +
                                                  "|    Menu dla zapisu ocen w pamięci :  |\n" +
                                                  "| add - dodaj oceny                    |\n" +
                                                  "| stats - statystyki                   |\n" +
                                                  "| grades - lista ocen                  |\n" +
                                                  "| back - powrót do poprzedniego menu   |\n" +
                                                  "|______________________________________|\n\n");

                                Console.WriteLine("Wybierz/napisz opcję z menu i naciśnij Enter : ");
                                var inputToMemory = Console.ReadLine().ToLower();
                                if (inputToMemory == "add" || inputToMemory == "stats" || inputToMemory == "grades" || inputToMemory == "back")
                                {
                                    switch (inputToMemory)
                                    {
                                        case "add":
                                            var backToMenu2 = false;
                                            while (!backToMenu2)
                                            {
                                                Console.WriteLine($"Podaj ocenę od 1 do 6 z lub bez +/- dla {fullName} \n" +
                                                                                       " lub naciśnij q aby wrócić : ");
                                                string inputGradeToMemory = Console.ReadLine();

                                                if (inputGradeToMemory == "q")
                                                {
                                                    backToMenu2 = true;
                                                    break;
                                                }
                                                else if (inputGradeToMemory.Length > 0 && inputGradeToMemory.Length <= 2 && char.IsDigit(inputGradeToMemory[0]))
                                                {
                                                    inMemoryStudent.AddGradePlus(inputGradeToMemory);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem (1 - 6) lu nie jest literą 'q'. Spróbuj jeszcze raz.");
                                                }
                                            }
                                            break;

                                        case "stats":
                                            var stats = new Statistics();
                                            stats.GetStatistics();
                                            break;

                                        case "grades":
                                            Console.WriteLine($"Dla studenta {inMemoryStudent.Forname} {inMemoryStudent.Surname} przypisane są oceny : ");
                                            List<double> inMemoryGrades = new List<double>();
                                            inMemoryGrades = inMemoryStudent.grades;
                                            string GradesMemoryList = "";
                                            for (var i = 0; i < inMemoryGrades.Count; i++)
                                            {
                                                GradesMemoryList += ($"{inMemoryGrades[i]}; ");
                                            }
                                            GradesMemoryList = GradesMemoryList.TrimEnd();
                                            GradesMemoryList = GradesMemoryList.TrimEnd(';');
                                            Console.Write($"{GradesMemoryList}\n");
                                            break;

                                        case "back":
                                            backFromMemory = true;
                                            break;
                                    }
                                }
                                Console.WriteLine("Wprowadź poprawne dane wybrane z menu i naciśnij Enter. Spróbuj jeszcze raz.");
                            }
                            break;

                        case "quit":
                            quitApp = true;
                            break;
                    }
                }
                Console.WriteLine("Wprowadź poprawne dane wybrane z menu i naciśnij Enter. Spróbuj jeszcze raz.");
            }
        }
    }
}