using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Knila.BAL.Implementations
{
    public static class ValidationClass
    {

        public static bool PhoneNumberValidation(this string? phoneNumber)
        {
            if (phoneNumber.IsNullOrEmpty())
            {
                return false;
            }
            bool isNumber = int.TryParse(phoneNumber, out int number);
            var digits = phoneNumber.Length;
            if (isNumber && digits == 10)
            {
                return true;
            }
            return false;
        }

        public static bool EmailValidation(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
