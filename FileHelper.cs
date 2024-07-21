namespace Helpers;

using System.IO;
using System.Text.RegularExpressions;

public static class FileHelper
{
    public static string RemoveIllegalFileNameChars(string input, string replacement = "")
    {
        var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
        var r = new Regex($"[{Regex.Escape(regexSearch)}]");
        return r.Replace(input, replacement);
    }
}

public static class FileExtensionMethods
{
    public static string ToFileSize(this long l)
    {
        return string.Format(new FileSizeFormatProvider(), "{0:fs}", l);
    }
}

public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
{
    public object? GetFormat(Type? formatType)
    {
        return formatType == typeof(ICustomFormatter) ? this : null;
    }

    private const string fileSizeFormat = "fs";
    private const decimal OneKiloByte   = 1024M;
    private const decimal OneMegaByte   = OneKiloByte * 1024M;
    private const decimal OneGigaByte   = OneMegaByte * 1024M;

    public string? Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        if (format == null || !format.StartsWith(fileSizeFormat))
        {
            if (arg != null)
                if (formatProvider != null)
                    return defaultFormat(format, arg, formatProvider);
        }

        if (arg is string)
        {
            if (formatProvider != null) return defaultFormat(format, arg, formatProvider);
        }

        decimal size = 0;

        try
        {
            size = Convert.ToDecimal(arg);
        }
        catch (InvalidCastException)
        {
            if (arg != null)
                if (formatProvider != null)
                    return defaultFormat(format, arg, formatProvider);
        }

        string suffix;
        switch (size)
        {
            case > OneGigaByte:
                size /= OneGigaByte;
                suffix = "GB";
                break;
            case > OneMegaByte:
                size /= OneMegaByte;
                suffix = "MB";
                break;
            case > OneKiloByte:
                size /= OneKiloByte;
                suffix = "kB";
                break;
            default:
                suffix = " B";
                break;
        }

        var precision = format.Substring(2);
        if (string.IsNullOrEmpty(precision)) precision = "2";
        return string.Format("{0:N" + precision + "}}{1}", size, suffix);

    }

    private static string? defaultFormat(string? format, object arg, IFormatProvider formatProvider)
    {
        return arg is IFormattable formattableArg ? formattableArg.ToString(format, formatProvider) : arg.ToString();
    }

}