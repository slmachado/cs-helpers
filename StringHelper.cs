using System.Text.RegularExpressions;

namespace Helpers;

public static class StringHelper
{

    //Function To test for Alphabets. 
    public static bool IsAlpha(string strToCheck)
    {
        Regex objAlphaPattern=new Regex("[^a-zA-Z]");
        return !objAlphaPattern.IsMatch(strToCheck);
    }
  

    //Function to Check for AlphaNumeric.
    public static bool IsAlphaNumeric(string strToCheck)
    {
        Regex objAlphaNumericPattern=new Regex("[^a-zA-Z0-9]");
        return !objAlphaNumericPattern.IsMatch(strToCheck);
    }


    /// <summary>Convert the string to double if the value is a numeric. If not a numeric, return NULL</summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double? ToDoubleNullable(this string value)
    {
        if (double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out var number))
        {
            return number;
        }

        return null;
    }


    /// <summary>Fills an IEnumerable with each line of a string</summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static IEnumerable<string> ToEnumerable(this string text)
    {
        if (!text.HasData()) return Enumerable.Empty<string>();
        var lines = text.Split(new [] { '\n' }, StringSplitOptions.None);
        return lines;
    }


    /// <summary>Extracts a substring right after a given string</summary>
    /// <param name="value"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static string? TextAfter(this string value, string search)
    {
        if (value.HasData() && search.HasData())
        {
            return value.Substring(value.IndexOf(search, StringComparison.OrdinalIgnoreCase) + search.Length).Trim();
        }
        return null;
    }


    /// <summary>Get a text from a specific line in a string</summary>
    /// <param name="text"></param>
    /// <param name="lineNo"></param>
    /// <returns></returns>
    public static string? GetTextFromLine(this string text, int lineNo)
    {
        if (!text.HasData() || lineNo <= 0) return null;
        string?[] lines = text.Replace("\r", "").Split('\n');
        return lines.Length >= lineNo ? lines[lineNo - 1] : null;
    }


    /// <summary>Gets the line position of a substring in a given text</summary>
    /// <param name="text"></param>
    /// <param name="lineToFind"></param>
    /// <returns></returns>
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
}