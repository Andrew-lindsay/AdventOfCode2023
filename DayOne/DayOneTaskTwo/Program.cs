using System.Text.RegularExpressions;
using AdventOfCode2023.Shared;

namespace DayOneTaskTwo {
    public class Program {
        public static void Main(string[] args) {
            string filePath = FileUtility.GetFilePathOrExit(args);

            string digitPattern = @"(\d|zero|one|two|three|four|five|six|seven|eight|nine)";

            var digitMapping = new Dictionary<string, string>() {
                { "zero", "0" },
                { "one", "1" },
                { "two", "2" },
                { "three", "3" },
                { "four", "4" },
                { "five", "5" },
                { "six", "6" },
                { "seven", "7" },
                { "eight", "8" },
                { "nine", "9" },
            };

            using StreamReader file = File.OpenText(filePath);

            string? line = null;
            int count = 0;
            long total = 0;
            while ((line = file.ReadLine()) != null) {
                count++;

                MatchCollection matches = Regex.Matches(line, digitPattern, RegexOptions.IgnoreCase);

                if (matches.Count == 0) {
                    Console.WriteLine($"No digits found in line {count}");
                    continue;
                }

                string? firstMatch = matches[0].Value;
                string? secondMatch = matches[^1].Value;

                string? firstDigit = char.IsDigit(firstMatch[0]) ? firstMatch : digitMapping[firstMatch.ToLower()];
                string? secondDigit = char.IsDigit(secondMatch[0]) ? secondMatch : digitMapping[secondMatch.ToLower()];

                int lineResult = int.Parse($"{firstDigit}{secondDigit}");
                total += lineResult;
            }

            Console.WriteLine(total);
        }
    }
}