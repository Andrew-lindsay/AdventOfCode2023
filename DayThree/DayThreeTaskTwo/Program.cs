using System.Text;
using AdventOfCode2023.Shared;
using DayThreeTaskOne;

namespace DayThreeTaskTwo {
    public class Program {
        public static void Main(string[] args) {
            string filePath = FileUtility.GetFilePathOrExit(args);

            using StreamReader stream = File.OpenText(filePath);

            var schematicRows = new List<SchematicRow>();
            string? line = null;

            while ((line = stream.ReadLine()) != null) {
                SchematicRow row = CreateSchematicRow(line);
                schematicRows.Add(row);
            }

            int total = CalculateGearRatio(schematicRows);

            Console.WriteLine(total);
        }

        private static int CalculateGearRatio(List<SchematicRow> schematicRows) {
            int total = 0;
            for (int i = 1; i < schematicRows.Count - 1; i++) {
                var row = schematicRows[i];
                if (!row.SymbolIndexes.Any()) {
                    continue;
                }

                var rowBefore = schematicRows[i - 1];
                var rowAfter = schematicRows[i + 1];

                foreach (int symbolIndex in row.SymbolIndexes) {
                    var beforeNumbers = rowBefore.NumbersInRange(symbolIndex);
                    var afterNumbers = rowAfter.NumbersInRange(symbolIndex);

                    var inlineNumebrs = row.Numbers
                        .Where(i => i.StartIndex - 1 == symbolIndex || i.EndIndex + 1 == symbolIndex)
                        .ToList();

                    var numbers = beforeNumbers
                        .Concat(afterNumbers)
                        .Concat(inlineNumebrs)
                        .Select(i => i.Number)
                        .ToList();

                    if (numbers.Count != 2) {
                        continue;
                    }

                    total += numbers.Aggregate((k, l) => k * l);
                }
            }

            return total;
        }

        private static SchematicRow CreateSchematicRow(string line) {
            var row = new SchematicRow();

            for (int i = 0; i < line.Length; i++) {
                char currentCharacter = line[i];

                // Skip periods
                if (currentCharacter == '.') {
                    continue;
                }

                // Parse Numbers
                if (char.IsDigit(currentCharacter)) {
                    int startIndex = i;
                    var numberBuilder = new StringBuilder();

                    while (i < line.Length) {
                        if (char.IsDigit(line[i])) {
                            numberBuilder.Append(line[i]);
                            i++;
                        } else {
                            break;
                        }
                    }

                    var schematicNumber = new SchematicNumber(Convert.ToInt32(numberBuilder.ToString()), startIndex, i - 1);
                    row.Numbers.Add(schematicNumber);

                    i--;
                    continue;
                }

                // get sybmol indexes
                if (currentCharacter == '*') {
                    row.SymbolIndexes.Add(i);
                }
            }

            return row;
        }
    }
}
