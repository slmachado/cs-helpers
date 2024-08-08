using System.Globalization;
using System.Text.RegularExpressions;

namespace Helpers
{
    public static class RegExValidation
    {
        /// <summary>
        /// Validates if the password meets the following criteria:
        /// - Contains at least one digit (0-9)
        /// - Contains at least one lowercase character (a-z)
        /// - Contains at least one uppercase character (A-Z)
        /// - Contains at least one special character
        /// - Length is between 8 and 30 characters
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the password meets the criteria; otherwise, false.</returns>
        public static bool CheckPassword(string password)
        {
            var regex = new Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,30}");
            var match = regex.Match(password);
            return match.Success;
        }

        /// <summary>
        /// Regular expression for validating e-mail format.
        /// </summary>
        public static Regex EmailValidation => new(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

        /// <summary>
        /// Checks if the e-mail has a valid format before processing.
        /// </summary>
        /// <param name="email">The e-mail to validate.</param>
        /// <returns>True if the e-mail has a valid format; otherwise, false.</returns>
        public static bool IsValidEmail(string email)
        {
            Regex rgx = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return rgx.IsMatch(email);
        }

        /// <summary>
        /// Validates if the phone number is in a valid format.
        /// Supports formats with country code and different separators.
        /// </summary>
        /// <param name="phoneNumber">The phone number to validate.</param>
        /// <returns>True if the phone number is valid; otherwise, false.</returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex rgx = new(@"^\+?[1-9]\d{1,14}$");
            return rgx.IsMatch(phoneNumber);
        }

        /// <summary>
        /// Validates if the URL is in a valid format.
        /// </summary>
        /// <param name="url">The URL to validate.</param>
        /// <returns>True if the URL is valid; otherwise, false.</returns>
        public static bool IsValidUrl(string url)
        {
            Regex rgx = new(@"^(http|https)://[^\s/$.?#].[^\s]*$");
            return rgx.IsMatch(url);
        }

        /// <summary>
        /// Validates if the postal code is in a valid format.
        /// </summary>
        /// <param name="postalCode">The postal code to validate.</param>
        /// <returns>True if the postal code is valid; otherwise, false.</returns>
        public static bool IsValidPostalCode(string postalCode)
        {
            Regex rgx = new(@"^\d{4,5}(-\d{4})?$");
            return rgx.IsMatch(postalCode);
        }

        /// <summary>
        /// Validates if the credit card number is in a valid format.
        /// Uses Luhn algorithm for validation.
        /// </summary>
        /// <param name="creditCardNumber">The credit card number to validate.</param>
        /// <returns>True if the credit card number is valid; otherwise, false.</returns>
        public static bool IsValidCreditCard(string creditCardNumber)
        {
            Regex rgx = new(@"^\d{16}$");
            if (!rgx.IsMatch(creditCardNumber)) return false;

            int sum = 0;
            bool alternate = false;
            for (int i = creditCardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(creditCardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                    {
                        n -= 9;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }

        /// <summary>
        /// Validates if the IP address is in a valid IPv4 format.
        /// </summary>
        /// <param name="ipAddress">The IP address to validate.</param>
        /// <returns>True if the IP address is valid IPv4; otherwise, false.</returns>
        public static bool IsValidIPv4(string ipAddress)
        {
            Regex rgx = new(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            return rgx.IsMatch(ipAddress);
        }

        /// <summary>
        /// Validates if the IP address is in a valid IPv6 format.
        /// </summary>
        /// <param name="ipAddress">The IP address to validate.</param>
        /// <returns>True if the IP address is valid IPv6; otherwise, false.</returns>
        public static bool IsValidIPv6(string ipAddress)
        {
            Regex rgx = new(@"^(([0-9a-fA-F]{1,4}:){7}([0-9a-fA-F]{1,4}|:)|(([0-9a-fA-F]{1,4}:){1,6}:)|(([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2})|(([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3})|(([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4})|(([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5})|(([0-9a-fA-F]{1,4}:){1,1}(:[0-9a-fA-F]{1,4}){1,6})|(:((:[0-9a-fA-F]{1,4}){1,7}|:))|(((?<=::)|(?<=^))(([0-9a-fA-F]{1,4}:){1,6}|:)|((([0-9a-fA-F]{1,4}:){1,7}|:):)([0-9a-fA-F]{1,4}:){0,5}[0-9a-fA-F]{1,4}))$");
            return rgx.IsMatch(ipAddress);
        }

        /// <summary>
        /// Validates if the SSN (Social Security Number) is in a valid format.
        /// </summary>
        /// <param name="ssn">The SSN to validate.</param>
        /// <returns>True if the SSN is valid; otherwise, false.</returns>
        public static bool IsValidSSN(string ssn)
        {
            Regex rgx = new(@"^\d{3}-\d{2}-\d{4}$");
            return rgx.IsMatch(ssn);
        }

        /// <summary>
        /// Validates if the username is in a valid format.
        /// Username must be alphanumeric and between 3 and 16 characters.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <returns>True if the username is valid; otherwise, false.</returns>
        public static bool IsValidUsername(string username)
        {
            Regex rgx = new(@"^[a-zA-Z0-9]{3,16}$");
            return rgx.IsMatch(username);
        }

        /// <summary>
        /// Validates if the date is in a valid format.
        /// Supports multiple date formats.
        /// </summary>
        /// <param name="date">The date to validate.</param>
        /// <param name="dateFormats">An array of date formats to validate against.</param>
        /// <returns>True if the date is valid; otherwise, false.</returns>
        public static bool IsValidDate(string date, params string[] dateFormats)
        {
            foreach (var format in dateFormats)
            {
                if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
