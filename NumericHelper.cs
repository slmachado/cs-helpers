using System.Globalization;
using System.Text.RegularExpressions;

namespace Helpers;

public static class NumericHelper
{
    public static double GetDoubleFromString(string value)
    {
        if (Double.TryParse(MakeSafeValue(value), NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
        {
            return doubleValue;
        }

        throw new ArgumentException("The counter value is not a valid double");
    }


    public static bool TryGetDoubleFromString(string value, out double doubleValue)
    {
        if (Double.TryParse(MakeSafeValue(value), NumberStyles.Any, CultureInfo.InvariantCulture, out doubleValue))
        {
            return true;
        }

        return false;
    }


    public static float GetFloatFromString(string value)
    {
        if (float.TryParse(MakeSafeValue(value), NumberStyles.Any, CultureInfo.InvariantCulture, out var floatValue))
        {
            return floatValue;
        }

        throw new ArgumentException("The counter value is not a valid float");
    }


    public static int GetIntFromString(string value)
    {
        if (int.TryParse(MakeSafeValue(value), NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
        {
            return intValue;
        }

        throw new ArgumentException("The counter value is not a valid integer");
    }


    public static bool TryGetIntFromString(string value, out int intValue)
    {
        if (int.TryParse(MakeSafeValue(value), NumberStyles.Any, CultureInfo.InvariantCulture, out intValue))
        {
            return true;
        }

        return false;
    }


    private static string MakeSafeValue(string value)
    {
        return value.Replace(",", ".");
    }


    public static double Truncate(this double value, int precision)
    {
        double step = Math.Pow(10, precision);
        double tmp = Math.Truncate(step * value);
        return tmp / step;
    }


    /// <summary>
    /// Return false if the value is a non-numeric value (NaN, Infinity ...), true otherwise
    /// </summary>
    public static bool IsValidNumericValue(double value)
    {
        return !double.IsNaN(value) && !double.IsInfinity(value);
    }


    //Function to test for Positive Integers.  
    public static bool IsNaturalNumber(string strNumber)
    {
        Regex objNotNaturalPattern=new Regex("[^0-9]");
        Regex objNaturalPattern=new Regex("0*[1-9][0-9]*");
        return  !objNotNaturalPattern.IsMatch(strNumber) &&
                objNaturalPattern.IsMatch(strNumber);
    }  


    //Function to test for Positive Integers with zero inclusive  
    public static bool IsWholeNumber(string strNumber)
    {
        Regex objNotWholePattern=new Regex("[^0-9]");
        return !objNotWholePattern.IsMatch(strNumber);
    }  


    //Function to Test for Integers both Positive & Negative  
    public static bool IsInteger(string strNumber)
    {
        Regex objNotIntPattern=new Regex("[^0-9-]");
        Regex objIntPattern=new Regex("^-[0-9]+$|^[0-9]+$");
        return  !objNotIntPattern.IsMatch(strNumber) &&  objIntPattern.IsMatch(strNumber);
    }


    //Function to Test for Positive Number both Integer & Real 
    public static bool IsPositiveNumber(string strNumber)
    {
        Regex objNotPositivePattern=new Regex("[^0-9.]");
        Regex objPositivePattern=new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
        Regex objTwoDotPattern=new Regex("[0-9]*[.][0-9]*[.][0-9]*");
        return !objNotPositivePattern.IsMatch(strNumber) &&
               objPositivePattern.IsMatch(strNumber)  &&
               !objTwoDotPattern.IsMatch(strNumber);
    }  


    //Function to test whether the string is valid number or not
    public static bool IsNumber(string strNumber)
    {
        Regex objNotNumberPattern=new Regex("[^0-9.-]");
        Regex objTwoDotPattern=new Regex("[0-9]*[.][0-9]*[.][0-9]*");
        Regex objTwoMinusPattern=new Regex("[0-9]*[-][0-9]*[-][0-9]*");
        var strValidRealPattern="^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
        var strValidIntegerPattern="^([-]|[0-9])[0-9]*$";
        Regex objNumberPattern =new Regex("(" + strValidRealPattern +")|(" + strValidIntegerPattern + ")");
        return !objNotNumberPattern.IsMatch(strNumber) &&
               !objTwoDotPattern.IsMatch(strNumber) &&
               !objTwoMinusPattern.IsMatch(strNumber) &&
               objNumberPattern.IsMatch(strNumber);
    }
}