using System;
using System.Text.RegularExpressions;

namespace Common.ValidationHandler
{
    public class ValidationHandler
    {
        public static bool ValidateFirstName(string firstName)
        {
            return !string.IsNullOrWhiteSpace(firstName) && firstName.Length >= 1 && firstName.Length <= 30;
        }

        public static bool ValidateLastName(string lastName)
        {
            return !string.IsNullOrWhiteSpace(lastName) && lastName.Length >= 1 && lastName.Length <= 30;
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
            DateTime parsedDate;
            return DateTime.TryParse(hireDate, out parsedDate);
        }
    }

    public static class RegexPatterns
    {
        public const string Email = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
        public const string IsraeliPhone = @"^05\d{8}$";
    }
}
