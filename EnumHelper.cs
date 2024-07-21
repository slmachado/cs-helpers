namespace Helpers;

public static class EnumHelper
{
    public static IEnumerable<T>? GetList<T>()
    {
        var type = typeof(T);
        return !type.IsEnum ? null : Enum.GetValues(type).Cast<T>();
    }

    public static T Parse<T>(int? value) where T : struct, IConvertible
    {
        if (value.HasValue == false)
        {
            return default;
        }

        if (!typeof(T).IsEnum) throw new ArgumentException("Error in AssetConvert class: T must be an enumerated type");

        if (!Enum.TryParse(value.ToString(), true, out T enumValue)) return default;
        if (Enum.IsDefined(typeof(T), enumValue))
            return enumValue;

        return default;
    }

    /// <summary>
    /// Gets the type of the attribute of.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumVal">The enum value.</param>
    /// <returns></returns>
    public static T? GetAttributeOfType<T>(this Enum? enumVal) where T : System.Attribute
    {
        if (enumVal == null)
        {
            return null;
        }
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }
}