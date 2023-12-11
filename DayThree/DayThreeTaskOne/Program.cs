using System.Text;
using AdventOfCode2023.Shared;

namespace DayThreeTaskOne {
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

            int total = CalculateSchematicTotal(schematicRows);

            Console.WriteLine(total);
        }

        private static int CalculateSchematicTotal(List<SchematicRow> schematicRows) {
            int total = 0;

            for (int i = 0; i < schematicRows.Count; i++) {
                SchematicRow schematicRow = schematicRows[i];

                foreach (var schematicNumber in schematicRow.Numbers) {
                    int rowBefore = i - 1;
                    int rowAfter = i + 1;

                    if (schematicRow.SymbolIndexes.Contains(schematicNumber.StartIndex - 1) ||
                        schematicRow.SymbolIndexes.Contains(schematicNumber.EndIndex + 1)) {
                        total += schematicNumber.Number;
                        continue;
                    }

                    if (rowBefore >= 0) {
                        var schematicRowBefore = schematicRows[rowBefore];

                        bool isInRange = schematicRowBefore.IsSymbolInRange(schematicNumber.StartIndex, schematicNumber.EndIndex);
                        if (isInRange) {
                            total += schematicNumber.Number;
                            continue;
                        }
                    }

                    if (rowAfter < schematicRows.Count) {
                        var schematicRowAfter = schematicRows[rowAfter];
                        bool isInRange = schematicRowAfter.IsSymbolInRange(schematicNumber.StartIndex, schematicNumber.EndIndex);

                        if (isInRange) {
                            total += schematicNumber.Number;
                            continue;
                        }
                    }
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
                if (!char.IsDigit(currentCharacter) && currentCharacter != '.') {
                    row.SymbolIndexes.Add(i);
                }
            }

            return row;
        }
    }
}
