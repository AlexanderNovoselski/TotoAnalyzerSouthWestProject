// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

namespace TotoAnalyzer.Helpers;

public static class ConsoleHelper
{
    public static int ReadIntSafe(string message)
    {
        while (true)
        {
            Console.Write(message);

            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Невалидно число!");
            Console.ResetColor();
        }
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Натисни клавиш....");
        Console.ReadKey();
    }

    // Displays formatted section header
    // and clears previous console output
    public static void Header(string title)
    {
        Console.Clear();

        Console.WriteLine("======================================");
        Console.WriteLine(title);
        Console.WriteLine("======================================");
    }
}