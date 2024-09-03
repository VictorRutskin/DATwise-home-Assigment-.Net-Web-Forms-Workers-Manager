using System;
using System.Text.RegularExpressions;

namespace Common.ValidationHandler
{
    public static class ValidationHandler
    {
        private const int MaxLength = 50;

        // General validation
        private static bool ValidateString(string input, string pattern, int maxLength)
        {
            return !string.IsNullOrWhiteSpace(input)
                   && input.Length <= maxLength
                   && Regex.IsMatch(input, pattern);
        }

        // Validates a name (first or last) between 1 and MaxLength characters, only English letters
        public static bool ValidateName(string name) => ValidateString(name, RegexPatterns.EnglishLettersOnly, MaxLength);

        // Validates email with length constraint
        public static bool ValidateEmail(string email) => ValidateString(email, RegexPatterns.Email, MaxLength);

        // Validates Israeli phone number with length constraint
        public static bool ValidatePhone(string phone) => ValidateString(phone, RegexPatterns.IsraeliPhone, MaxLength);

        // Validates hire date not before 1900 and not after today
        public static bool ValidateHireDate(string hireDate)
        {
            return DateTime.TryParse(hireDate, out var parsedDate)
                   && parsedDate >= new DateTime(1900, 1, 1)
                   && parsedDate <= DateTime.Today;
        }
    }

    // Class for regex patterns
    public static class RegexPatterns
    {
        public const string Email = @"^[^\s@]+@[^\s@]+\.[^\s@]{2,}$";
        public const string IsraeliPhone = @"^05\d{8}$";
        public const string EnglishLettersOnly = @"^[a-zA-Z]+$";
    }
}
