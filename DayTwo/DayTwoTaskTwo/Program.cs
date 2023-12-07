using AdventOfCode2023.Shared;

namespace AdventOfCode2023.DayTwoTaskTwo {
    internal class Program {
        public static void Main(string[] args) {
            string filePath = FileUtility.GetFilePathOrExit(args);

            using StreamReader file = File.OpenText(filePath);

            string? line = null;
            long total = 0;
            while ((line = file.ReadLine()) != null) {
                string[] gameLine = line.Split(':');

                Dictionary<string, int> maxColourValues = GetMaxColourValues(gameLine);

                total += maxColourValues.Values.Aggregate((i, j) => i * j);
            }

            Console.WriteLine(total);
        }

        private static Dictionary<string, int> GetMaxColourValues(string[] gameLine) {
            string[] rounds = gameLine[1].Split(';');
            var maxColourValues = new Dictionary<string, int>();
            foreach (string round in rounds) {

                foreach (string grab in round.Split(',')) {
                    var grabSplit = grab.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    int count = int.Parse(grabSplit[0]);
                    string colour = grabSplit[1];

                    if (count > maxColourValues.GetValueOrDefault(colour)) {
                        maxColourValues[colour] = count;
                    }
                }
            }

            return maxColourValues;
        }
    }
}
