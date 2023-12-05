using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.DayOne {
    public class Program {
        static void Main(string[] args) {

            string filePath = @"Inputs\input";

            if (!File.Exists(filePath)) {
                Console.WriteLine("File Not Found");
                Environment.Exit(-1);
            }

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