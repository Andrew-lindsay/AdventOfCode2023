namespace DayThreeTaskOne {
    public class SchematicNumber {
        public SchematicNumber(int number, int startIndex, int endIndex) {
            this.Number = number;
            this.StartIndex = startIndex;
            this.EndIndex = endIndex;
        }

        public int Number { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }
    }
}
