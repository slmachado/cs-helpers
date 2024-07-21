namespace Helpers;

public static class UnitHelper
{
    public static double ConvertDegreeToKelvin(double value)
    {
        return value + 273;
    }

    public static double ConverKelvinTotDegree(double value)
    {
        return value - 273;
    }
}