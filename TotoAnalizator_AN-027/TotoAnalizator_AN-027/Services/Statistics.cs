// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

using TotoAnalyzer.Models;

namespace TotoAnalyzer.Services;

public class Statistics
{
    private readonly IEnumerable<Draw> _draws;

    public Statistics(IEnumerable<Draw> draws)
    {
        _draws = draws;
    }

    public Dictionary<int, int> TopNumbers(int n)
    {
        return _draws
            .SelectMany(x => x.Numbers)
            .GroupBy(x => x)
            .OrderByDescending(g => g.Count())
            .Take(n)
            .ToDictionary(
                g => g.Key,
                g => g.Count());
    }

    public IEnumerable<(int, int, int)> HotPairs(int n)
    {
        return _draws
            .SelectMany(draw =>
                draw.Numbers.SelectMany(
                    (x, i) =>
                        draw.Numbers
                            .Skip(i + 1)
                            .Select(y => (x, y))))
            .GroupBy(p => p)
            .Select(g =>
                (g.Key.x, g.Key.y, g.Count()))
            .OrderByDescending(x => x.Item3)
            .Take(n);
    }

    public Dictionary<string, int> Distribution()
    {
        return _draws
            .SelectMany(x => x.Numbers)
            .GroupBy(x =>
            {
                if (x <= 10) return "1-10";
                if (x <= 20) return "11-20";
                if (x <= 30) return "21-30";
                if (x <= 40) return "31-40";

                return "41-49";
            })
            .ToDictionary(
                g => g.Key,
                g => g.Count());
    }

    public Dictionary<int, int> AllFrequencies()
    {
        return Enumerable.Range(1, 49)
            .ToDictionary(
                n => n,
                n => _draws.Count(
                    d => d.Numbers.Contains(n)));
    }
}