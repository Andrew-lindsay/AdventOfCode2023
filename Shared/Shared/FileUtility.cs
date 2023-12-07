namespace AdventOfCode2023.Shared {
    public class FileUtility {
        public static string GetFilePathOrExit(string[] args) {
            if (args.Length == 0) {
                Console.WriteLine("Please supply a file path");
                Environment.Exit(-1);
            }

            string filePath = args[0];

            if (!File.Exists(filePath)) {
                Console.WriteLine("File Not Found");
                Environment.Exit(-1);
            }

            return filePath;
        }
    }
}
