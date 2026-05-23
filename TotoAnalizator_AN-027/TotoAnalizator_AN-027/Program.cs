// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

using TotoAnalyzer.Data;
using TotoAnalyzer.Services;
using TotoAnalyzer.UI;

Console.OutputEncoding =
    System.Text.Encoding.UTF8;

while (true)
{
    Console.Clear();

    Console.WriteLine(
        "ТОТО АНАЛИЗАТОР");

    Console.WriteLine();

    DataLoader loader = new();

    var draws =
        await loader.LoadAsync();

    if (!draws.Any())
    {
        Console.ForegroundColor =
            ConsoleColor.Red;

        Console.WriteLine(
            "Няма намерени данни!");

        Console.ResetColor();

        Console.WriteLine(
            "Натисни накой бутон...");

        Console.ReadKey();

        continue;
    }

    Statistics statistics =
        new(draws);

    Menu menu =
        new(statistics, draws);

    menu.Start();

    // Ask user if another year should be loaded
    Console.Clear();

    Console.WriteLine(
        "Зареждане на друга година?");

    Console.WriteLine();

    Console.WriteLine(
        "[1] Да");

    Console.WriteLine(
        "[0] Не");

    Console.WriteLine();

    string? choice =
        Console.ReadLine();

    if (choice == "0")
    {
        break;
    }
}