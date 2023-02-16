using ChallengeApp;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChallengeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var quitApp = false;
            while (!quitApp)
            {
                PrintMainMenu();
                var inputMenu = Console.ReadLine().ToLower();
                if (inputMenu == "f" || inputMenu == "m" || inputMenu == "q")
                {
                    switch (inputMenu)
                    {
                        case "f":
                            SubMenuToFile();
                            break;
                        case "m":
                            SubMenuToMemory();
                            break;
                        case "q":
                            quitApp = true;
                            break;
                    }
                }
                WrongMenuChoiceMsg();
            }
        }
        public static void PrintMainMenu()
        {
            Console.WriteLine("Witaj !\n" +
                              "Jest to aplikacja przechowująca oceny wprowadzonego ucznia oraz wylicza jego statystyki. Taki elektroniczny dzienniczek.\n\n" +
                              "Wybierz gdzie chcesz dodać oceny: \n" +
                              "________________________________________\n" +
                              "|                Menu :                |\n" +
                              "| f - file - zapis danych do pliku     |\n" +
                              "| m - memory - zapis danych do pamięci |\n" +
                              "| q - quit - wyjście                   |\n" +
                              "|______________________________________|\n\n");
        }
        public static void SubMenuToFile()
        {
            Console.WriteLine("Podaj imię studenta któremu chcesz wystawić ocenę: ");
            var inputname = Console.ReadLine();
            Console.WriteLine("Teraz podaj jego nazwisko: ");
            var inputSurname = Console.ReadLine();
            var savedStudent = new SavedStudent(inputname, inputSurname);
            var backFromFile = false;
            while (!backFromFile)
            {
                Console.WriteLine("_________________________________________\n" +
                                  "|    Menu dla zapisu ocen do pliku :    |\n" +
                                  "| a - add - dodaj oceny dla ucznia oraz |\n" +
                                  "|     wyświetl jego statystyki          |\n" +
                                  "| b - back - powrót do poprzedniego menu|\n" +
                                  "|_______________________________________|\n\n");

                Console.WriteLine("Wybierz/napisz opcję z menu i naciśnij Enter : ");
                var inputSubMenu = Console.ReadLine().ToLower();
                if (inputSubMenu == "a" || inputSubMenu == "b")
                {
                    switch (inputSubMenu)
                    {
                        case "a":
                            var backToMenu = false;
                            while (!backToMenu)
                            {
                                Grade1to6Msg();
                                var inputToFile = Console.ReadLine();

                                if (inputToFile == "q")
                                {
                                    backToMenu = true;
                                    backFromFile = true;
                                    break;
                                }
                                else if (inputToFile.Length > 0 && inputToFile.Length <= 2 && char.IsDigit(inputToFile[0]))
                                {
                                    savedStudent.AddGrade(inputToFile);
                                    savedStudent.SendMessageLessThenThree += OnSendMessageLessThenThree;
                                    Console.WriteLine($"Dla studenta {savedStudent.Name} {savedStudent.Surname} przypisane są oceny : ");
                                    using (StreamReader sr = File.OpenText(savedStudent.GetFileName()))
                                    {
                                        var line = sr.ReadLine();
                                        string GradesFileList = "";
                                        while (line != null)
                                        {
                                            GradesFileList += ($"; {line}");
                                            line = sr.ReadLine();
                                        }
                                        GradesFileList = GradesFileList.TrimStart(';');
                                        Console.WriteLine($"{GradesFileList}\n\n");
                                    }
                                    savedStudent.GetStatistics();
                                }
                                else
                                {
                                    WrongGradeFormatMsg();
                                }
                            }
                            break;
                        case "b":
                            backFromFile = true;
                            break;
                    }
                }
                WrongMenuChoiceMsg();
            }
        }
        public static void SubMenuToMemory()
        {
            Console.WriteLine("Podaj imię studenta któremu chcesz wystawić ocenę: ");
            var inputname = Console.ReadLine();
            Console.WriteLine("Teraz podaj jego nazwisko: ");
            var inputSurname = Console.ReadLine();
            var inMemoryStudent = new InMemoryStudent(inputname, inputSurname);
            var backFromMemory = false;
            while (!backFromMemory)
            {
                Console.WriteLine("_________________________________________\n" +
                                  "|    Menu dla zapisu ocen w pamięci :   |\n" +
                                  "| a - add - dodaj oceny dla ucznia oraz |\n" +
                                  "|     wyświetl jego statystyki          |\n" +
                                  "| b - back - powrót do poprzedniego menu|\n" +
                                  "|_______________________________________|\n\n");

                Console.WriteLine("Wybierz/napisz opcję z menu i naciśnij Enter : ");
                var inputToMemory = Console.ReadLine().ToLower();
                if (inputToMemory == "a" || inputToMemory == "b")
                {
                    switch (inputToMemory)
                    {
                        case "a":
                            var backToMenu2 = false;
                            while (!backToMenu2)
                            {
                                Grade1to6Msg();
                                string inputGradeToMemory = Console.ReadLine();

                                if (inputGradeToMemory == "q")
                                {
                                    backToMenu2 = true;
                                    backFromMemory = true;
                                    break;
                                }
                                else if (inputGradeToMemory.Length > 0 && inputGradeToMemory.Length <= 2 && char.IsDigit(inputGradeToMemory[0]))
                                {
                                    inMemoryStudent.AddGrade(inputGradeToMemory);
                                    inMemoryStudent.SendMessageLessThenThree += OnSendMessageLessThenThree;
                                    Console.WriteLine($"Dla studenta {inMemoryStudent.Name} {inMemoryStudent.Surname} przypisane są oceny : ");
                                    List<float> inMemoryGrades = new List<float>();
                                    inMemoryGrades = inMemoryStudent.grades;
                                    string GradesMemoryList = "";
                                    for (var i = 0; i < inMemoryGrades.Count; i++)
                                    {
                                        GradesMemoryList += ($"; {inMemoryGrades[i]}");
                                    }
                                    GradesMemoryList = GradesMemoryList.TrimStart(';');
                                    Console.Write($"{GradesMemoryList}\n\n");
                                    inMemoryStudent.GetStatistics();
                                }
                                else
                                {
                                    WrongGradeFormatMsg();
                                }
                            }
                            break;
                        case "b":
                            backFromMemory = true;
                            break;
                    }
                }
                else
                {
                    WrongMenuChoiceMsg();
                }
            }
        }
        static void Grade1to6Msg()
        {
            Console.WriteLine($"Podaj ocenę od 1 do 6 z lub bez +/- lub naciśnij q aby wrócić : ");
        }
        static void WrongGradeFormatMsg()
        {
            Console.WriteLine("Wprowadź poprawny format oceny. Wprowadzona ocena nie składa się z cyfry ; z cyfry z + lub - ; jest poza zakresem (1 - 6) lu nie jest literą 'q'. Spróbuj jeszcze raz.");
        }
        static void WrongMenuChoiceMsg()
        {
            Console.WriteLine("Wprowadź poprawne dane wybrane z menu i naciśnij Enter. Spróbuj jeszcze raz.");
        }
        static void OnSendMessageLessThenThree(object sender, EventArgs args)
        {
            Console.WriteLine($"Otrzymana ocena jest poniżej 3 !!");
        }
    }
}