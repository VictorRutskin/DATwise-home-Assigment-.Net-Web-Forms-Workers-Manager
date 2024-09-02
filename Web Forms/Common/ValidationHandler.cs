using System;
using System.Text.RegularExpressions;

namespace Common.ValidationHandler
{
    public class ValidationHandler
    {
        public static bool ValidateFirstName(string firstName)
        {
            // Validate that first name is not empty, is between 1 and 30 characters long, and contains only English letters
            return !string.IsNullOrWhiteSpace(firstName)
                   && firstName.Length >= 1
                   && firstName.Length <= 30
                   && Regex.IsMatch(firstName, RegexPatterns.EnglishLettersOnly);
        }

        public static bool ValidateLastName(string lastName)
        {
            // Validate that last name is not empty, is between 1 and 30 characters long, and contains only English letters
            return !string.IsNullOrWhiteSpace(lastName)
                   && lastName.Length >= 1
                   && lastName.Length <= 30
                   && Regex.IsMatch(lastName, RegexPatterns.EnglishLettersOnly);
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email, RegexPatterns.Email);
        }

        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Regex.IsMatch(phone, RegexPatterns.IsraeliPhone);
        }

        public static bool ValidateHireDate(string hireDate)
        {
            if (DateTime.TryParse(hireDate, out DateTime parsedDate))
            {
                DateTime today = DateTime.Today;
                DateTime minDate = new DateTime(1900, 1, 1);

                // Check that the date is not before 1900 and not after today's date
                return parsedDate >= minDate && parsedDate <= today;
            }

            return false;
        }
    }

    public static class RegexPatterns
    {
        public const string Email = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
        public const string IsraeliPhone = @"^05\d{8}$";
        public const string EnglishLettersOnly = @"^[a-zA-Z]+$"; 
    }
}
