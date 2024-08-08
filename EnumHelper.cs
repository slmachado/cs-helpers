using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Gets a list of all values in the enum.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <returns>An IEnumerable of the enum values, or null if T is not an enum.</returns>
        public static IEnumerable<T>? GetList<T>()
        {
            var type = typeof(T);
            return !type.IsEnum ? null : Enum.GetValues(type).Cast<T>();
        }

        /// <summary>
        /// Parses an integer value to the corresponding enum value.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The integer value to parse.</param>
        /// <returns>The corresponding enum value, or the default value if parsing fails.</returns>
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
        /// Gets the attribute of a specified type associated with an enum value.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="enumVal">The enum value.</param>
        /// <returns>The attribute of the specified type, or null if not found.</returns>
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

        /// <summary>
        /// Gets the description attribute of an enum value.
        /// </summary>
        /// <param name="enumVal">The enum value.</param>
        /// <returns>The description attribute if present, otherwise the enum value as a string.</returns>
        public static string GetDescription(this Enum enumVal)
        {
            var attribute = enumVal.GetAttributeOfType<System.ComponentModel.DescriptionAttribute>();
            return attribute == null ? enumVal.ToString() : attribute.Description;
        }

        /// <summary>
        /// Converts the enum value to a list of its name and value pairs.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <returns>A list of KeyValuePairs where the key is the name and the value is the integer value of the enum.</returns>
        public static List<KeyValuePair<string, int>> ToList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                .Select(e => new KeyValuePair<string, int>(e.ToString(), Convert.ToInt32(e)))
                .ToList();
        }

        /// <summary>
        /// Parses a string value to the corresponding enum value.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The string value to parse.</param>
        /// <returns>The corresponding enum value, or the default value if parsing fails.</returns>
        public static T Parse<T>(string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            if (Enum.TryParse(value, true, out T enumValue))
                if (Enum.IsDefined(typeof(T), enumValue) || enumValue.ToString().Contains(","))
                    return enumValue;

            return default;
        }

        /// <summary>
        /// Checks if an integer value is defined in the specified enum.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The integer value to check.</param>
        /// <returns>True if the value is defined in the enum, otherwise false.</returns>
        public static bool IsDefined<T>(int value) where T : Enum
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>
        /// Checks if a string value is defined in the specified enum.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The string value to check.</param>
        /// <returns>True if the value is defined in the enum, otherwise false.</returns>
        public static bool IsDefined<T>(string value) where T : Enum
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>
        /// Gets the names of the enum values as a list of strings.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <returns>A list of the names of the enum values.</returns>
        public static List<string> GetNames<T>() where T : Enum
        {
            return Enum.GetNames(typeof(T)).ToList();
        }

        /// <summary>
        /// Gets the values of the enum as a list of integers.
        /// </summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <returns>A list of the values of the enum.</returns>
        public static List<int> GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<int>().ToList();
        }
    }
}
