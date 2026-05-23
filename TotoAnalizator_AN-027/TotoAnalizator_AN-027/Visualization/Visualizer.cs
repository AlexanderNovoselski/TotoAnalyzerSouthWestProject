// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

namespace TotoAnalyzer.Visualization;

public static class Visualizer
{
    public static void BarChart(
        Dictionary<int, int> data)
    {
        // Prevent crash on empty collections
        if (data == null || !data.Any())
        {
            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                "Няма намерени данни.");

            Console.ResetColor();

            return;
        }

        int max = data.Values.Max();

        foreach (var item in data)
        {
            int width =
                (int)((item.Value / (double)max) * 40);

            Console.WriteLine(
                $"{item.Key,2} | {new string('#', width)} {item.Value}");
        }
    }

    public static void HeatMap(
        Dictionary<int, int> frequencies)
    {
        int max = frequencies.Values.Max();

        for (int i = 1; i <= 49; i++)
        {
            double ratio =
                frequencies[i] / (double)max;

            if (ratio >= 0.7)
                Console.ForegroundColor =
                    ConsoleColor.Red;

            else if (ratio >= 0.3)
                Console.ForegroundColor =
                    ConsoleColor.Yellow;

            else
                Console.ForegroundColor =
                    ConsoleColor.Cyan;

            Console.Write($"{i,3}");

            Console.ResetColor();

            if (i % 7 == 0)
                Console.WriteLine();
        }
    }
}