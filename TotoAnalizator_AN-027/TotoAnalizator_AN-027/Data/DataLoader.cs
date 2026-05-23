// ===============================================
// Copyright by Aleksandar Novoselski
// Student from South West University
// Faculty Number: 23251421027
// ===============================================

using TotoAnalyzer.Helpers;
using TotoAnalyzer.Models;
using TotoAnalyzer.Parsers;

namespace TotoAnalyzer.Data;

/// <summary>
/// Тото сайта има anti scraping механизъм, който не позволява автоматично извличане на данни от страницата или поне не позволява лесно.
/// За щастие, те предоставят възможност за сваляне на данни за всеки тираж в текстов или Word документ. Този клас отговаря за извличането на
/// наличните файлове, показването им на потребителя и свалянето на избрания от него файл. След това, в зависимост от формата на файла, се използва 
/// съответният парсер за извличане на данните от него.
/// </summary>
public class DataLoader
{
    private const string BaseUrl =
        "https://info.toto.bg/statistika/6x49";

    private const string Domain =
        "https://info.toto.bg";

    // Http client for downloading html and files
    private readonly HttpClient _client = new();

    // Main method that loads selected year data
    public async Task<IEnumerable<Draw>> LoadAsync()
    {
        List<Draw> draws = new();

        Console.WriteLine(
            "Зареждане на години...");

        // Extract available files from website
        List<YearFile> availableYears =
            await LoadAvailableYearsAsync();

        // Display all available years
        Console.WriteLine();

        Console.WriteLine(
            "Налични години:");

        foreach (var year in availableYears)
        {
            Console.WriteLine(
                $"{year.Year} ({year.Extension})");
        }

        Console.WriteLine();

        // Read user-selected year
        int selectedYear =
            ConsoleHelper.ReadIntSafe(
                "Избери година: ");

        // Find matching file
        YearFile? selectedFile =
            availableYears
                .FirstOrDefault(
                    y => y.Year == selectedYear);

        if (selectedFile == null)
        {
            Console.ForegroundColor =
                ConsoleColor.Red;

            Console.WriteLine(
                "Грешна година!");

            Console.ResetColor();

            return draws;
        }

        Console.WriteLine();

        Console.WriteLine(
            $"Сваляне на данни за година: {selectedYear}...");

        // Download selected file
        byte[] data =
            await _client.GetByteArrayAsync(
                selectedFile.Url);

        // Create temporary file
        string tempFile =
            Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid() +
                selectedFile.Extension);

        // Save downloaded file locally
        await File.WriteAllBytesAsync(
            tempFile,
            data);

        // Automatically choose parser
        if (selectedFile.Extension == ".txt")
        {
            draws.AddRange(
                TxtParser.Parse(tempFile));
        }
        else if (selectedFile.Extension == ".docx")
        {
            draws.AddRange(
                DocxParser.Parse(tempFile));
        }

        Console.WriteLine(
            $"Заредени данни: {draws.Count}");

        return draws;
    }

    private Task<List<YearFile>>LoadAvailableYearsAsync()
    {
        List<YearFile> years = new()
        {
            new()
            {
                Year = 2021,
                Url = "https://info.toto.bg/content/files/2022/01/02/b72d0cbe449bcc17ec8ecb19ee82233a.docx",
                Extension = ".docx"
            },

            new()
            {
                Year = 2022,
                Url = "https://info.toto.bg/content/files/2023/01/11/5f8be78ee5e2ceb7839cefe22b7d2f1b.docx",
                Extension = ".docx"
            },

            new()
            {
                Year = 2023,
                Url = "https://info.toto.bg/content/files/2024/01/08/c6283cfbdeb917bb3ba894cc38b24728.docx",
                Extension = ".docx"
            },

            new()
            {
                Year = 2024,
                Url = "https://info.toto.bg/content/files/2025/01/06/ea7643fc1635991fe4548cf57b3cf994.docx",
                Extension = ".docx"
            },

            new()
            {
                Year = 2025,
                Url = "https://info.toto.bg/content/files/2026/01/07/5026e066d4883844db5c8ab602e38858.docx",
                Extension = ".docx"
            }
        };

        // Generate TXT years automatically
        for (int year = 1958; year <= 2020; year++)
        {
            string fileName;

            if (year <= 2004)
            {
                fileName =
                    $"649_{year % 100:D2}.txt";
            }
            else
            {
                fileName =
                    $"649_{year}.txt";
            }

            years.Add(new YearFile
            {
                Year = year,

                Url =
                    $"https://info.toto.bg/content/files/stats-tiraji/{fileName}",

                Extension = ".txt"
            });
        }

        return Task.FromResult(
            years
                .OrderBy(y => y.Year)
                .ToList());
    }
}