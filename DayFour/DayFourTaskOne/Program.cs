using AdventOfCode2023.Shared;

namespace DayFourTaskOne {
    public class Program {
        public static void Main(string[] args) {
            string filePath = FileUtility.GetFilePathOrExit(args);

            using StreamReader stream = File.OpenText(filePath);

            string? line = null;

            long total = 0;
            while ((line = stream.ReadLine()) != null) {
                total += CheckScratchCard(line);
            }

            Console.WriteLine(total);
        }

        private static long CheckScratchCard(string line) {
            string? lineWithOutHeader = line
                .Split(':')[1];

            if (lineWithOutHeader == null) {
                return 0;
            }

            string[] card = lineWithOutHeader.Split('|', StringSplitOptions.RemoveEmptyEntries);

            var winningNumbers = card[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => Convert.ToInt32(i))
                .ToHashSet();

            var game = card[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => Convert.ToInt32(i))
                .ToHashSet();

            var matchingNumbers = winningNumbers.Intersect(game);

            if (!matchingNumbers.Any()) {
                return 0;
            }

            return Convert.ToInt64(Math.Pow(2, matchingNumbers.Count() - 1));
        }
    }
}
