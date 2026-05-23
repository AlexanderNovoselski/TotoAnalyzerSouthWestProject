// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

using DocumentFormat.OpenXml.Packaging;
using System.Text.RegularExpressions;
using TotoAnalyzer.Models;

namespace TotoAnalyzer.Parsers;

public static class DocxParser
{
    public static IEnumerable<Draw> Parse(string path)
    {
        // Collection that stores parsed draws
        List<Draw> draws = new();

        // Open DOCX document in read-only mode
        using var document =
            WordprocessingDocument.Open(path, false);

        // Extract all text from the document body
        string text =
            document.MainDocumentPart!
                    .Document
                    .Body!
                    .InnerText;

        // Regular expression used to locate draw lines
        // Тираж 1/2025, Теглене 1: 3 16 23 36 41 49
        var matches = Regex.Matches(
            text,
            @"Тираж\s+\d+/\d+.*?:\s*((\d+\s+){5}\d+)");

        foreach (Match match in matches)
        {
            string numbersPart =
                match.Groups[1].Value;

            int[] numbers =
                numbersPart
                    .Split(' ',
                        StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

            // Validate draw length
            // Valid Toto draw must contain exactly 6 numbers
            if (numbers.Length == 6)
            {
                draws.Add(new Draw
                {
                    // Temporary date value
                    // Can later be replaced with parsed draw date
                    Date = DateTime.Now,

                    // Store extracted numbers
                    Numbers = numbers
                });
            }
        }

        return draws;
    }
}