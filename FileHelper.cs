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
            /// <summary>
        /// Reads all lines from a text file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>An array of lines from the file.</returns>
        public static string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        /// <summary>
        /// Writes all lines to a text file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="lines">The lines to write to the file.</param>
        public static void WriteAllLines(string filePath, string[] lines)
        {
            File.WriteAllLines(filePath, lines);
        }

        /// <summary>
        /// Reads all text from a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>The text from the file.</returns>
        public static string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Writes text to a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="text">The text to write to the file.</param>
        public static void WriteAllText(string filePath, string text)
        {
            File.WriteAllText(filePath, text);
        }

        /// <summary>
        /// Appends text to a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="text">The text to append to the file.</param>
        public static void AppendAllText(string filePath, string text)
        {
            File.AppendAllText(filePath, text);
        }

        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <returns>True if the file exists; otherwise, false.</returns>
        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        public static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        /// <summary>
        /// Copies a file to a new location.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="destFilePath">The destination file path.</param>
        /// <param name="overwrite">True to overwrite the destination file if it exists; otherwise, false.</param>
        public static void CopyFile(string sourceFilePath, string destFilePath, bool overwrite = false)
        {
            File.Copy(sourceFilePath, destFilePath, overwrite);
        }

        /// <summary>
        /// Moves a file to a new location.
        /// </summary>
        /// <param name="sourceFilePath">The source file path.</param>
        /// <param name="destFilePath">The destination file path.</param>
        public static void MoveFile(string sourceFilePath, string destFilePath)
        {
            File.Move(sourceFilePath, destFilePath);
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

    private const string FileSizeFormat = "fs";
    private const decimal OneKiloByte   = 1024M;
    private const decimal OneMegaByte   = OneKiloByte * 1024M;
    private const decimal OneGigaByte   = OneMegaByte * 1024M;

    public string? Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        if (format == null || !format.StartsWith(FileSizeFormat))
        {
            if (arg != null)
                if (formatProvider != null)
                    return DefaultFormat(format, arg, formatProvider);
        }

        if (arg is string)
        {
            if (formatProvider != null) return DefaultFormat(format, arg, formatProvider);
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
                    return DefaultFormat(format, arg, formatProvider);
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

        var precision = format?.Substring(2);
        if (string.IsNullOrEmpty(precision)) precision = "2";
        return string.Format("{0:N" + precision + "}}{1}", size, suffix);

    }

    private static string? DefaultFormat(string? format, object arg, IFormatProvider formatProvider)
    {
        return arg is IFormattable formattableArg ? formattableArg.ToString(format, formatProvider) : arg.ToString();
    }

}