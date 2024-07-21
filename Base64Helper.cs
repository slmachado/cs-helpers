namespace Helpers;

public static class Base64Helper
{
    /// <summary>
    /// Decode base64 string
    /// Check base64 length and add characters to reach base64 length
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
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