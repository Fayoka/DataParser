using System;
using System.Globalization;

namespace InputDataParser
{
    public static class DataParser
    {
        public static int PromptForInteger(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = ReadLineSafe();

                if (int.TryParse(input, out int result))
                    return result;

                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        public static double PromptForDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = ReadLineSafe();

                if (double.TryParse(input, out double result))
                    return result;

                Console.WriteLine("Invalid input. Please enter a valid floating-point number.");
            }
        }

        public static string PromptForNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = ReadLineSafe();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                Console.WriteLine("Invalid input. Please enter a non-empty string.");
            }
        }

        public static DateTime PromptForFlexibleDate(string prompt)
        {
            string[] acceptedFormats = {
                "dd/MM/yyyy", "dd-MM-yyyy", "dd.MM.yyyy",
                "ddMMyyyy", "dd MM yyyy",
                "d/M/yy", "d-M-yy", "d.M.yy",
                "dMyy", "d M yy"
            };

            while (true)
            {
                Console.Write(prompt);
                string input = ReadLineSafe();

                if (DateTime.TryParseExact(input, acceptedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    return result;

                Console.WriteLine("Invalid input. Please enter a date in a recognized format (e.g., dd/MM/yyyy, dd-MM-yy, or ddMMyyyy).");
            }
        }

        private static string ReadLineSafe()
        {
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("Unexpected end of input. Terminating program.");
                Environment.Exit(1); // Exit with error code
            }

            input = input.Trim();

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                throw new OperationCanceledException("User requested termination.");
            }

            return input;
        }
    }
}
