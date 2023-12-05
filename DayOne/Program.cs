using System.Reflection;
using System.Reflection.PortableExecutable;

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
            List<int> results = new List<int>();
            while ((line = file.ReadLine()) != null) {
                count++;
                char? firstDigit = null;
                char? secondDigit = null;

                int leftIndex = 0;
                for (leftIndex = 0; leftIndex < line.Length; leftIndex++) {
                    char c = line[leftIndex];

                    if (!Char.IsDigit(c)) {
                        continue;
                    }

                    firstDigit = c;
                    break;
                }

                for (int rightIndex = line.Length - 1; rightIndex >= leftIndex; rightIndex--) {
                    char c = line[rightIndex];

                    if (!Char.IsDigit(c)) {
                        continue;
                    }

                    secondDigit = c;
                    break;
                }

                if (firstDigit == null && secondDigit == null) {
                    Console.WriteLine($"No digits found in line {count}");
                    continue;
                }

                if (firstDigit != null && secondDigit == null) {
                    secondDigit = firstDigit;
                }

                int lineResult = int.Parse($"{firstDigit}{secondDigit}");
                results.Add(lineResult);
            }

            Console.WriteLine($"Total: {results.Sum()}");
        }
    }
}