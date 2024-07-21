using System.Text.RegularExpressions;

namespace Helpers;

public static class RegExValidation
{

    /// <summary>
    ///(?=.*\d)      #   must contains one digit from 0-9
    ///  (?=.*[a-z])       #   must contains one lowercase characters
    ///  (?=.*[A-Z])       #   must contains one uppercase characters
    ///  (?=.*[\W])        #   must contains at least one special character
    ///                    #   match anything with previous condition checking {8,30}
    ///                    #   length at least 8 characters and maximum of 30  
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static bool CheckPassword(string password)
    {
        var regex = new Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,30}");
        var match = regex.Match(password);
        return match.Success;
    }


    /// <summary>
    /// Check if e-mail has a valid format before processing
    /// </summary>
    public static Regex EmailValidation => new(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);


    /// <summary>
    /// Check if e-mail has a valid format before processing
    /// </summary>
    public static bool IsValidEMAIL(string email)
    {
        Regex Rgx = new (@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        return Rgx.IsMatch(email);
    }
}