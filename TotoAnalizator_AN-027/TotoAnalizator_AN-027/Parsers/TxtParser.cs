// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

using TotoAnalyzer.Models;

namespace TotoAnalyzer.Parsers;

public static class TxtParser
{
    public static IEnumerable<Draw> Parse(string path)
    {
        List<Draw> draws = new();

        var lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            var groups = line.Split(
                [' ', '\t'],
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var group in groups)
            {
                var numbers = group
                    .Split(',')
                    .Where(x => int.TryParse(x, out _))
                    .Select(int.Parse)
                    .ToList();

                // трябва да са:
                // тираж + 6 числа = 7 елемента
                if (numbers.Count == 7)
                {
                    draws.Add(new Draw
                    {
                        Date = DateTime.Now,

                        Numbers = numbers
                            .Skip(1)
                            .Take(6)
                            .ToArray()
                    });
                }
            }
        }

        return draws;
    }
}