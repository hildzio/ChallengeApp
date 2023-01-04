/*Dzień 12: Switch’e i Wyjątki*/
using System;
using System.Collections.Generic;

namespace ChallengeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var employee = new Employee("Adam");
                    Console.WriteLine("________________________________________");
                    Console.WriteLine("|                Menu :                |");
                    Console.WriteLine("|             q - wyjście              |");
                    Console.WriteLine("|______________________________________|");
                    Console.WriteLine("");
                    Console.WriteLine($"Witaj! Podaj ocenę od 1 do 6 z lub bez +/- dla {employee.Name} : ");
                    var input = Console.ReadLine();
                    
                    if (input == "q")
                    {
                        break;
                    }
                    else
                    {
                        employee.AddGradePlus(input);
                        var stats = employee.GetStatistics();
                        Console.WriteLine($"");
                        Console.WriteLine($"TOP **************** Statystyki ******************* TOP");
                        Console.WriteLine($"Na temat pracownika {employee.Name} mamy informacje : ");
                        Console.WriteLine($"Średnia ocena jest równa : {stats.Average}");
                        Console.WriteLine($"Najniż1sza ocena jest równa : {stats.Low}");
                        Console.WriteLine($"Najwyższa ocena jest równa  : {stats.High}");
                        Console.WriteLine($"Przypisana litera to  : {stats.Letter}");
                        Console.WriteLine($"END ***************  Statystyki ******************* END");
                        Console.WriteLine($"");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Niestety wprowadziłeś nieprawidłowy znak. Błąd : " + ex.Message + " Wprowadź ocenę która jest cyfrą lub cyfrą z +/-.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Niestety wprowadziłeś liczbę z poza zakresu. Błąd :" + ex.Message + " Wprowadź ocenę ponownie.");
                }
            }
        }
    }
}