using AdventOfCode2023.Shared;
using System.Text.RegularExpressions;

namespace DayFourTaskTwo {
    internal class Program {
        public static void Main(string[] args) {
            string filePath = FileUtility.GetFilePathOrExit(args);

            using StreamReader stream = File.OpenText(filePath);

            string? line = null;

            Dictionary<int, int> cardToWins = new Dictionary<int, int>();
            while ((line = stream.ReadLine()) != null) {
                (int CardNumber, int MatchingNumbers) card = NumberOfWins(line);
                cardToWins.Add(card.CardNumber, card.MatchingNumbers);
            }

            long total = cardToWins.Count;

            var cardsToProcess = new Stack<int>(cardToWins.Where(i => i.Value >= 1).Select(i => i.Key));

            while (cardsToProcess.Any()) {
                var card = cardsToProcess.Pop();
                int wins = cardToWins[card];
                total += wins;
                for (int i = 0; i < wins; i++) {
                    cardsToProcess.Push(++card);
                }
            }

            Console.WriteLine(total);
        }

        private static (int CardNumber, int MatchingNumbers) NumberOfWins(string line) {
            var match = Regex.Match(line, @"Card\s*(\d+): (.*)");
            int cardNumber = Convert.ToInt32(match.Groups[1].ToString());
            string lineWithOutHeader = match.Groups[2].ToString();

            string[] card = lineWithOutHeader.Split('|', StringSplitOptions.RemoveEmptyEntries);

            var winningNumbers = card[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => Convert.ToInt32(i))
                .ToHashSet();

            var game = card[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(i => Convert.ToInt32(i))
                .ToHashSet();

            IEnumerable<int> matchingNumbers = winningNumbers.Intersect(game);

            return (cardNumber, matchingNumbers.Count());
        }
    }
}
