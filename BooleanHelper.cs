namespace Helpers;

public static class BooleanHelper
{
    public static int GetBooleanIntValueFromString(string value)
    {
        if (TryGetBooleanIntValueFromString(value, out var intValue))
        {
            return intValue;
        }

        throw new ArgumentException("The counter value is not a valid boolean");
    }


    public static bool TryGetBooleanIntValueFromString(string value, out int boolValue)
    {
        if (NumericHelper.TryGetIntFromString(value, out boolValue))
        {
            if (boolValue == 0 || boolValue == 1)
            {
                return true;
            }
            boolValue = 0;
        }

        return false;
    }


    public static bool GetBooleanFromString(string value)
    {
        if (NumericHelper.TryGetIntFromString(value, out var intValue))
        {
            switch (intValue)
            {
                case 0:
                    return false;
                case 1:
                    return true;
            }
        }

        if (bool.TryParse(value, out var boolValue))
        {
            return boolValue;
        }
                
        throw new ArgumentException("The counter value is not a valid boolean");
    }


    public static bool TryGetBooleanFromString(string value, out bool boolValue)
    {
        return Boolean.TryParse(value, out boolValue);
    }
}