using System.Globalization;
using System.Text.RegularExpressions;

namespace InputDataParser
{
    public static class DataParser
    {
        public static int PromptForInteger(string prompt, string errorMessage = "Please enter a valid integer.", bool displayErrorMessages = true)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = ReadLineSafe();

                if (int.TryParse(input, out int result))
                    return result;

                if (displayErrorMessages)
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        public static double PromptForDouble(string prompt, string errorMessage = "Please enter a valid double.", bool displayErrorMessages = true)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = ReadLineSafe();

                if (double.TryParse(input, out double result))
                    return result;

                if (displayErrorMessages)
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        public static string PromptForNonEmptyString(string prompt, string errorMessage = "Input cannot be empty.", bool displayErrorMessages = true)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = ReadLineSafe();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                if (displayErrorMessages)
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        public static DateTime PromptForFlexibleDate(string prompt, string[] acceptedFormats = null, bool displayErrorMessages = true)
        {
            acceptedFormats ??= new string[] {
                "dd/MM/yyyy", "dd-MM-yyyy", "dd.MM.yyyy",
                "ddMMyyyy", "dd MM yyyy",
                "d/M/yy", "d-M-yy", "d.M.yy",
                "dMyy", "d M yy"
            };

            while (true)
            {
                Console.WriteLine(prompt);
                string input = ReadLineSafe();

                if (DateTime.TryParseExact(input, acceptedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    return result;

                if (displayErrorMessages)
                {
                    Console.WriteLine($"Invalid date. Please enter a date in one of the following formats: {string.Join(", ", acceptedFormats)}");
                }
            }
        }
        
        public static DateTime PromptForFlexibleDateOrDefaultDate(string prompt, string[] acceptedFormats = null, bool displayErrorMessages = true)
        {
            acceptedFormats ??= new string[] {
                "dd/MM/yyyy", "dd-MM-yyyy", "dd.MM.yyyy",
                "ddMMyyyy", "dd MM yyyy",
                "d/M/yy", "d-M-yy", "d.M.yy",
                "dMyy", "d M yy"
            };

            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                if (input == "")
                    return DateTime.Today;
                
                if (DateTime.TryParseExact(input, acceptedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    return result;

                if (displayErrorMessages)
                {
                    Console.WriteLine($"Invalid date. Please enter a date in one of the following formats: {string.Join(", ", acceptedFormats)}");
                }
            }
        }
        

        public static string PromptForStringWithTimeout(string prompt, int timeoutSeconds = 30, string timeoutMessage = "Input timed out.")
        {
            DateTime startTime = DateTime.Now;
            string? input = null;

            Console.WriteLine(prompt);

            while ((DateTime.Now - startTime).TotalSeconds < timeoutSeconds)
            {
                if (Console.KeyAvailable)
                {
                    input = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrWhiteSpace(input))
                        return input;
                }
            }

            Console.WriteLine(timeoutMessage);
            return string.Empty;
        }
        public static string PromptForEmail(string prompt, string errorMessage = "Please enter a valid email address.", 
            bool displayErrorMessages = true)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = ReadLineSafe();

                if (IsValidEmail(input))
                    return input;

                if (displayErrorMessages)
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return emailRegex.IsMatch(email);
        }

        public static string ReadLineSafe()
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
