namespace Helpers;

/// <summary> Check floating point equality </summary>
public static class FloatingNumberHelper
{
    private const double Epsilon = 0.000001d;

    /// <inheritdoc cref="NearlyEqual(double,double)" />
    public static bool NearlyEqual(this double value, double compareTo, double epsilon)
    {
        return Math.Abs(value - compareTo) < epsilon;
    }

    /// <summary>
    ///     An equality method for floating number in goal to manage imprecision of the type,
    ///     using default  acceptable imprecision step between two value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="compareTo"></param>
    /// <returns>Return TRUE if both are NULL or have the same float value</returns>
    public static bool NearlyEqual(this double? value, double? compareTo)
    {
        if (!value.HasValue && !compareTo.HasValue)
        {
            return true;
        }

        if (!value.HasValue || !compareTo.HasValue)
        {
            return false;
        }

        return NearlyEqual(value.Value, compareTo.Value);
    }


    /// <summary>
    ///     An equality method for floating number in goal to manage imprecision of the type,
    ///     using default  acceptable imprecision step between two value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="compareTo"></param>
    /// <returns></returns>
    public static bool NearlyEqual(this double value, double compareTo) => NearlyEqual(value, compareTo, Epsilon);

    /// <inheritdoc cref="NearlyZero(double)" />
    public static bool NearlyEqual(this double value, double? compareTo)
    {
        return compareTo.HasValue && NearlyEqual(value, compareTo.Value);
    }

    /// <inheritdoc cref="NearlyZero(double)" />
    public static bool NearlyZero(this double value, double epsilon)
    {
        return Math.Abs(value) < epsilon;
    }

    /// <summary>
    ///     A zero equality method for floating number in goal to manage imprecision of the type,
    ///     using default acceptable imprecision
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool NearlyZero(this double value) => NearlyZero(value, Epsilon);

    /// <inheritdoc cref="NearlyZero(double)" />
    public static bool NearlyZero(this double? value)
    {
        return value.HasValue && NearlyZero(value.Value);
    }

    /// <summary>
    ///     A zero or null equality method for floating number in goal to manage imprecision of the type,
    ///     using default acceptable imprecision
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool NearlyZeroOrNull(this double? value)
    {
        return !value.HasValue || NearlyZero(value.Value);
    }
}