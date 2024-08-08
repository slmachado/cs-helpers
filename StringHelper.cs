using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Checks if the string contains only alphabetic characters.
        /// </summary>
        /// <param name="strToCheck">The string to check.</param>
        /// <returns>True if the string contains only alphabetic characters; otherwise, false.</returns>
        public static bool IsAlpha(string strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Checks if the string contains only alphanumeric characters.
        /// </summary>
        /// <param name="strToCheck">The string to check.</param>
        /// <returns>True if the string contains only alphanumeric characters; otherwise, false.</returns>
        public static bool IsAlphaNumeric(string strToCheck)
        {
            Regex objAlphaNumericPattern = new Regex("[^a-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Converts the string to a double if the value is numeric. If not numeric, returns NULL.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>A nullable double if the conversion is successful; otherwise, null.</returns>
        public static double? ToDoubleNullable(this string value)
        {
            if (double.TryParse(value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out var number))
            {
                return number;
            }
            return null;
        }

        /// <summary>
        /// Fills an IEnumerable with each line of a string.
        /// </summary>
        /// <param name="text">The string to split into lines.</param>
        /// <returns>An IEnumerable containing each line of the string.</returns>
        public static IEnumerable<string> ToEnumerable(this string text)
        {
            if (!text.HasData()) return Enumerable.Empty<string>();
            var lines = text.Split(new[] { '\n' }, StringSplitOptions.None);
            return lines;
        }

        /// <summary>
        /// Extracts a substring right after a given string.
        /// </summary>
        /// <param name="value">The original string.</param>
        /// <param name="search">The string to search for.</param>
        /// <returns>The substring right after the search string if found; otherwise, null.</returns>
        public static string? TextAfter(this string value, string search)
        {
            if (value.HasData() && search.HasData())
            {
                return value.Substring(value.IndexOf(search, StringComparison.OrdinalIgnoreCase) + search.Length).Trim();
            }
            return null;
        }

        /// <summary>
        /// Gets a text from a specific line in a string.
        /// </summary>
        /// <param name="text">The string containing lines of text.</param>
        /// <param name="lineNo">The line number to retrieve.</param>
        /// <returns>The text from the specified line if it exists; otherwise, null.</returns>
        public static string? GetTextFromLine(this string text, int lineNo)
        {
            if (!text.HasData() || lineNo <= 0) return null;
            string?[] lines = text.Replace("\r", "").Split('\n');
            return lines.Length >= lineNo ? lines[lineNo - 1] : null;
        }

        /// <summary>
        /// Gets the line position of a substring in a given text.
        /// </summary>
        /// <param name="text">The string containing lines of text.</param>
        /// <param name="lineToFind">The substring to find.</param>
        /// <returns>The line number containing the substring if found; otherwise, -1.</returns>
        public static int GetLineNumber(this string text, string lineToFind)
        {
            int lineNum = 0;
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNum++;
                    if (line.Contains(lineToFind))
                    {
                        return lineNum;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Checks if the string contains only numeric characters.
        /// </summary>
        /// <param name="strToCheck">The string to check.</param>
        /// <returns>True if the string contains only numeric characters; otherwise, false.</returns>
        public static bool IsNumeric(string strToCheck)
        {
            Regex objNumericPattern = new Regex("[^0-9]");
            return !objNumericPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Converts the string to title case (capitalizes the first letter of each word).
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The string in title case.</returns>
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Reverses the characters in the string.
        /// </summary>
        /// <param name="str">The string to reverse.</param>
        /// <returns>The reversed string.</returns>
        public static string ReverseString(this string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Checks if the string is a valid integer.
        /// </summary>
        /// <param name="strToCheck">The string to check.</param>
        /// <returns>True if the string is a valid integer; otherwise, false.</returns>
        public static bool IsInteger(string strToCheck)
        {
            return int.TryParse(strToCheck, out _);
        }

        /// <summary>
        /// Checks if the string is a valid date.
        /// </summary>
        /// <param name="strToCheck">The string to check.</param>
        /// <param name="dateFormat">The date format to validate against. Default is "dd/MM/yyyy".</param>
        /// <returns>True if the string is a valid date; otherwise, false.</returns>
        public static bool IsValidDate(string strToCheck, string dateFormat = "dd/MM/yyyy")
        {
            return DateTime.TryParseExact(strToCheck, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        /// <summary>
        /// Extension method to check if a string is not null or empty.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>True if the string has data; otherwise, false.</returns>
        public static bool HasData(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Removes extra white spaces from the string.
        /// </summary>
        /// <param name="str">The string to process.</param>
        /// <returns>The string without extra white spaces.</returns>
        public static string RemoveExtraSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", " ").Trim();
        }

        /// <summary>
        /// Checks if the string is a valid email address.
        /// </summary>
        /// <param name="email">The string to check.</param>
        /// <returns>True if the string is a valid email address; otherwise, false.</returns>
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if the string is a valid URL.
        /// </summary>
        /// <param name="url">The string to check.</param>
        /// <returns>True if the string is a valid URL; otherwise, false.</returns>
        public static bool IsValidUrl(this string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Truncates the string to the specified length.
        /// </summary>
        /// <param name="str">The string to truncate.</param>
        /// <param name="maxLength">The maximum length of the truncated string.</param>
        /// <returns>The truncated string.</returns>
        public static string Truncate(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Length <= maxLength ? str : str.Substring(0, maxLength);
        }

        /// <summary>
        /// Replaces multiple spaces in the string with a single space.
        /// </summary>
        /// <param name="str">The string to process.</param>
        /// <returns>The string with multiple spaces replaced by a single space.</returns>
        public static string ReplaceMultipleSpaces(this string str)
        {
            return Regex.Replace(str, @"\s{2,}", " ");
        }

        /// <summary>
        /// Removes non-alphanumeric characters from the string.
        /// </summary>
        /// <param name="str">The string to process.</param>
        /// <returns>The string without non-alphanumeric characters.</returns>
        public static string RemoveNonAlphanumeric(this string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9]", "");
        }

        /// <summary>
        /// Converts the string to camelCase.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The string in camelCase.</returns>
        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str.ToLower();
            return char.ToLower(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Converts the string to snake_case.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>The string in snake_case.</returns>
        public static string ToSnakeCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            var startUnderscores = Regex.Match(str, @"^_+");
            return startUnderscores + Regex.Replace(str, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
