// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

namespace TotoAnalyzer.Models;

public class Draw
{
    public DateTime Date { get; set; }

    public int[] Numbers { get; set; } = Array.Empty<int>();

    public int Year => Date.Year;
}