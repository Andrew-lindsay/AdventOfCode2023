using System.Text.RegularExpressions;
using AdventOfCode2023.Shared;

namespace AdventOfCode2023.DayOne {
    public class Program {
        public static void Main(string[] args) {
            string filePath = FileUtility.GetFilePathOrExit(args);

            using StreamReader file = File.OpenText(filePath);

            string? line = null;
            int count = 0;
            long total = 0;
            while ((line = file.ReadLine()) != null) {
                count++;

                MatchCollection matches = Regex.Matches(line, @"\d");

                if (matches.Count == 0) {
                    Console.WriteLine($"No digits found in line {count}");
                    continue;
                }

                string? firstDigit = matches[0].Value;
                string? secondDigit = matches[^1].Value;

                int lineResult = int.Parse($"{firstDigit}{secondDigit}");
                total += lineResult;
            }

            Console.WriteLine(total);
        }
    }
}