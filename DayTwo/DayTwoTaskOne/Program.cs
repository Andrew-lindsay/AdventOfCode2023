namespace DayTwoTaskOne {
    internal class Program {
        static void Main(string[] args) {
            string filePath = @"Inputs\input";

            if (!File.Exists(filePath)) {
                Console.WriteLine("File Not Found");
                Environment.Exit(-1);
            }

            // only 12 red cubes, 13 green cubes, and 14 blue cubes
            var maxColourValues = new Dictionary<string, int>() {
                {"red", 12 },
                {"green", 13 },
                {"blue", 14},
            };

            using StreamReader file = File.OpenText(filePath);

            string? line = null;
            long total = 0;
            while ((line = file.ReadLine()) != null) {
                string[] gameLine = line.Split(':');
                int gameId = Convert.ToInt32(gameLine[0].Split(' ')[^1]);

                string[] rounds = gameLine[1].Split(';');
                bool impossible = false;
                foreach (string round in rounds) {

                    foreach(string grab in round.Split(',')) {
                        var grabSplit = grab.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        string count = grabSplit[0];
                        string colour = grabSplit[1];

                        if (int.Parse(count) > maxColourValues[colour]) {
                            impossible = true;
                            break;
                        }
                    }

                    if (impossible){
                        break;
                    }
                }

                if (impossible) {
                    continue;
                }

                total += gameId;
            }

            Console.WriteLine(total);
        }
    }
}
