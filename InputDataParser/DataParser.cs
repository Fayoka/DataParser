using System;
using System.Globalization;

namespace InputDataParser
{
    public static class DataParser
    {
        public static int PromptForInteger(string prompt, bool displayErrorMessages = true)
        {
            while (true)
            {
                string input = ReadLineSafe();

                if (int.TryParse(input, out int result))
                    return result;

                if (displayErrorMessages)
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
            }
        }

        public static double PromptForDouble(string prompt, bool displayErrorMessages = true)
        {
            while (true)
            {
                string input = ReadLineSafe();

                if (double.TryParse(input, out double result))
                    return result;

                if (displayErrorMessages)
                {
                    Console.WriteLine("Please enter a valid double.");
                }
            }
        }

        public static string PromptForNonEmptyString(string prompt, bool displayErrorMessages = true)
        {
            while (true)
            {
                string input = ReadLineSafe();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                if (displayErrorMessages)
                {
                    Console.WriteLine("Input cannot be empty.");
                }
            }
        }

        public static DateTime PromptForFlexibleDate(string prompt, bool displayErrorMessages = true)
        {
            string[] acceptedFormats = {
                "dd/MM/yyyy", "dd-MM-yyyy", "dd.MM.yyyy",
                "ddMMyyyy", "dd MM yyyy",
                "d/M/yy", "d-M-yy", "d.M.yy",
                "dMyy", "d M yy"
            };

            while (true)
            {
                string input = ReadLineSafe();

                if (DateTime.TryParseExact(input, acceptedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    return result;

                if (displayErrorMessages)
                {
                    Console.WriteLine("Please enter a valid date.");
                }
            }
        }

        private static string ReadLineSafe()
        {
            string? input = null;
            while (input == null)
            {
                input = Console.ReadLine();

                if (input != null)
                {
                    input = input.Trim();
                    if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new OperationCanceledException("User requested termination.");
                    }
                }
            }
            return input;
        }
    }
}
