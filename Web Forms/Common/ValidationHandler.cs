using System;
using System.Text.RegularExpressions;

namespace Common.ValidationHandler
{
    public class ValidationHandler
    {
        // Between 1 and 30 characters long, and only English letters
        public static bool ValidateFirstName(string firstName)
        {
            return !string.IsNullOrWhiteSpace(firstName)
                   && firstName.Length >= 1
                   && firstName.Length <= 30
                   && Regex.IsMatch(firstName, RegexPatterns.EnglishLettersOnly);
        }

        // Between 1 and 30 characters long, and only English letters
        public static bool ValidateLastName(string lastName)
        {
            return !string.IsNullOrWhiteSpace(lastName)
                   && lastName.Length >= 1
                   && lastName.Length <= 30
                   && Regex.IsMatch(lastName, RegexPatterns.EnglishLettersOnly);
        }

        // Email regex validation
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, RegexPatterns.Email);
        }

        // IsraeliPhone regex validation
        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Regex.IsMatch(phone, RegexPatterns.IsraeliPhone);
        }

        // Not before 1900 and not after today
        public static bool ValidateHireDate(string hireDate)
        {
            if (DateTime.TryParse(hireDate, out DateTime parsedDate))
            {
                DateTime today = DateTime.Today;
                DateTime minDate = new DateTime(1900, 1, 1);

                return parsedDate >= minDate && parsedDate <= today;
            }

            return false;
        }
    }

    // Struct for regex validations
    public struct RegexPatterns
    {
        public const string Email = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
        public const string IsraeliPhone = @"^05\d{8}$";
        public const string EnglishLettersOnly = @"^[a-zA-Z]+$"; 
    }
}
