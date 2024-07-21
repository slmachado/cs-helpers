namespace Helpers;

/// <summary>
/// Provides methods for working with Base64 encoding and decoding.
/// </summary>
public static class Base64Helper
{
    /// <summary>
    /// Safely decodes a Base64-encoded string into a byte array.
    /// <para>
    /// The method replaces URL-safe characters ('-' and '_') with the standard Base64 characters ('+' and '/').
    /// It then ensures that the string length is a multiple of 4 by appending '=' padding characters as necessary.
    /// </para>
    /// <para>
    /// If the input string is null, empty, or consists only of white spaces, the method returns an empty byte array.
    /// In case of any errors during the decoding process (e.g., if the input is not valid Base64), the method also returns an empty byte array.
    /// </para>
    /// </summary>
    /// <param name="input">The Base64-encoded string to decode.</param>
    /// <returns>A byte array representing the decoded data, or an empty byte array if the input is invalid or an error occurs.</returns>
    public static byte[] SafeConvertFromBase64String(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return Array.Empty<byte>();
        try
        {
            var working = input.Replace('-', '+').Replace('_', '/');
            while (working.Length % 4 != 0)
            {
                working += '=';
            }
            return Convert.FromBase64String(working);
        }
        catch (Exception)
        {
            return Array.Empty<byte>();
        }
    }
}