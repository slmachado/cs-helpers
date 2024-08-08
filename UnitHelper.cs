namespace Helpers
{
    public static class UnitHelper
    {
        /// <summary>
        /// Converts degrees Celsius to Kelvin.
        /// </summary>
        /// <param name="value">Temperature in degrees Celsius.</param>
        /// <returns>Temperature in Kelvin.</returns>
        public static double ConvertDegreeToKelvin(double value)
        {
            return value + 273.15;
        }

        /// <summary>
        /// Converts Kelvin to degrees Celsius.
        /// </summary>
        /// <param name="value">Temperature in Kelvin.</param>
        /// <returns>Temperature in degrees Celsius.</returns>
        public static double ConvertKelvinToDegree(double value)
        {
            return value - 273.15;
        }

        /// <summary>
        /// Converts degrees Celsius to degrees Fahrenheit.
        /// </summary>
        /// <param name="value">Temperature in degrees Celsius.</param>
        /// <returns>Temperature in degrees Fahrenheit.</returns>
        public static double ConvertCelsiusToFahrenheit(double value)
        {
            return (value * 9 / 5) + 32;
        }

        /// <summary>
        /// Converts degrees Fahrenheit to degrees Celsius.
        /// </summary>
        /// <param name="value">Temperature in degrees Fahrenheit.</param>
        /// <returns>Temperature in degrees Celsius.</returns>
        public static double ConvertFahrenheitToCelsius(double value)
        {
            return (value - 32) * 5 / 9;
        }

        /// <summary>
        /// Converts miles to kilometers.
        /// </summary>
        /// <param name="value">Distance in miles.</param>
        /// <returns>Distance in kilometers.</returns>
        public static double ConvertMilesToKilometers(double value)
        {
            return value * 1.60934;
        }

        /// <summary>
        /// Converts kilometers to miles.
        /// </summary>
        /// <param name="value">Distance in kilometers.</param>
        /// <returns>Distance in miles.</returns>
        public static double ConvertKilometersToMiles(double value)
        {
            return value / 1.60934;
        }

        /// <summary>
        /// Converts pounds to kilograms.
        /// </summary>
        /// <param name="value">Weight in pounds.</param>
        /// <returns>Weight in kilograms.</returns>
        public static double ConvertPoundsToKilograms(double value)
        {
            return value * 0.453592;
        }

        /// <summary>
        /// Converts kilograms to pounds.
        /// </summary>
        /// <param name="value">Weight in kilograms.</param>
        /// <returns>Weight in pounds.</returns>
        public static double ConvertKilogramsToPounds(double value)
        {
            return value / 0.453592;
        }

        /// <summary>
        /// Converts liters to gallons.
        /// </summary>
        /// <param name="value">Volume in liters.</param>
        /// <returns>Volume in gallons.</returns>
        public static double ConvertLitersToGallons(double value)
        {
            return value * 0.264172;
        }

        /// <summary>
        /// Converts gallons to liters.
        /// </summary>
        /// <param name="value">Volume in gallons.</param>
        /// <returns>Volume in liters.</returns>
        public static double ConvertGallonsToLiters(double value)
        {
            return value / 0.264172;
        }

        /// <summary>
        /// Converts inches to centimeters.
        /// </summary>
        /// <param name="value">Length in inches.</param>
        /// <returns>Length in centimeters.</returns>
        public static double ConvertInchesToCentimeters(double value)
        {
            return value * 2.54;
        }

        /// <summary>
        /// Converts centimeters to inches.
        /// </summary>
        /// <param name="value">Length in centimeters.</param>
        /// <returns>Length in inches.</returns>
        public static double ConvertCentimetersToInches(double value)
        {
            return value / 2.54;
        }
    }
}
