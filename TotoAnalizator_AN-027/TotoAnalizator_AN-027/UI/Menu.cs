// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

using TotoAnalyzer.Helpers;
using TotoAnalyzer.Models;
using TotoAnalyzer.Services;
using TotoAnalyzer.Visualization;

namespace TotoAnalyzer.UI;

public class Menu
{
    private readonly Statistics _statistics;

    private readonly IEnumerable<Draw> _draws;

    public Menu(
        Statistics statistics,
        IEnumerable<Draw> draws)
    {
        _statistics = statistics;
        _draws = draws;
    }

    public void Start()
    {
        while (true)
        {
            ConsoleHelper.Header(
                "ТОТО АНАЛИЗАТОР");

            Console.WriteLine("[1] Топ числа");
            Console.WriteLine("[2] Горещи двойки");
            Console.WriteLine("[3] Разпределение");
            Console.WriteLine("[4] Heat Map");
            Console.WriteLine("[0] Изход");

            int choice =
                ConsoleHelper.ReadIntSafe(
                    "Избор: ");

            switch (choice)
            {
                case 1:
                    ShowTopNumbers();
                    break;

                case 2:
                    ShowPairs();
                    break;

                case 3:
                    ShowDistribution();
                    break;

                case 4:
                    ShowHeatMap();
                    break;

                case 0:
                    return;
            }
        }
    }

    private void ShowTopNumbers()
    {
        ConsoleHelper.Header(
            "ТОП ЧИСЛА");

        int n;

        // Validate positive number input
        do
        {
            n = ConsoleHelper.ReadIntSafe(
                "N = ");

            if (n <= 0)
            {
                Console.ForegroundColor =
                    ConsoleColor.Red;

                Console.WriteLine(
                    "N трябва да е по голямо от 0!");

                Console.ResetColor();
            }

        } while (n <= 0);

        var result =
            _statistics.TopNumbers(n);

        // Additional protection against empty collections
        if (!result.Any())
        {
            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                "No data available.");

            Console.ResetColor();

            ConsoleHelper.Pause();

            return;
        }

        Visualizer.BarChart(result);

        ConsoleHelper.Pause();
    }

    private void ShowPairs()
    {
        ConsoleHelper.Header(
            "ГОРЕЩИ ДВОЙКИ");

        var pairs =
            _statistics.HotPairs(10);

        foreach (var p in pairs)
        {
            Console.WriteLine(
                $"{p.Item1} + {p.Item2} => {p.Item3}");
        }

        ConsoleHelper.Pause();
    }

    private void ShowDistribution()
    {
        ConsoleHelper.Header(
            "РАЗПРЕДЕЛЕНИЕ");

        var result =
            _statistics.Distribution();

        foreach (var item in result)
        {
            Console.WriteLine(
                $"{item.Key} => {item.Value}");
        }

        ConsoleHelper.Pause();
    }

    private void ShowHeatMap()
    {
        ConsoleHelper.Header(
            "HEAT MAP");

        var result =
            _statistics.AllFrequencies();

        Visualizer.HeatMap(result);

        ConsoleHelper.Pause();
    }
}