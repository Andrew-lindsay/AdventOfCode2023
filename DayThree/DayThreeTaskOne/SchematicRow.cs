
namespace DayThreeTaskOne {
    public class SchematicRow {
        public List<SchematicNumber> Numbers { get; set; } = new();

        public HashSet<int> SymbolIndexes { get; set; } = new();

        public bool IsSymbolInRange(int startIndex, int endIndex) {
            foreach (var symbolIndex in this.SymbolIndexes) {
                if ((startIndex - 1) <= symbolIndex && symbolIndex <= (endIndex + 1)) {
                    return true;
                }
            }

            return false;
        }
    }
}
