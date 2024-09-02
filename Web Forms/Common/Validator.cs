using System;
using System.Text.RegularExpressions;

namespace Common
{
    public class Validator
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

            var emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Israeli phone number regex: Start with 05, followed by 8 digits
            var phonePattern = @"^05\d{8}$";
            return Regex.IsMatch(phone, phonePattern);
        }

        public static bool ValidateHireDate(string hireDate)
        {
            DateTime parsedDate;
            return DateTime.TryParse(hireDate, out parsedDate);
        }
    }
}
