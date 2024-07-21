namespace Helpers
{
    /// <summary>
    /// Helper to get binary value from Word
    /// </summary>
    public static class BinaryHelper
    {
        #region Convert to binary string

        /// <summary>
        /// Get binary string on 64 bits from long
        /// </summary>
        /// <param name="value">The long value to convert to a binary string.</param>
        /// <returns>A 64-bit binary string representation of the specified long value.</returns>
        public static string ToBinary(this long value)
        {
            return Convert.ToString(value, 2).PadLeft(64, '0');
        }

        #endregion

        #region Get Value from binary string

        /// <summary>
        /// Get bool value from a specific position in a binary string.
        /// </summary>
        /// <param name="binaryWord">The binary string to evaluate.</param>
        /// <param name="position">The position of the bit to evaluate.</param>
        /// <returns>True if the bit at the specified position is 1, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when binaryWord is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when position is out of range.</exception>
        public static bool GetBoolFromBinaryWord(string binaryWord, int position)
        {
            if (String.IsNullOrEmpty(binaryWord))
            {
                throw new ArgumentNullException(nameof(binaryWord));
            }

            if (position < 0 || position >= binaryWord.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            int startIndex = (binaryWord.Length - 1) - position;
            string binaryValue = binaryWord.Substring(startIndex, 1);

            return binaryValue.Equals("1");
        }

        /// <summary>
        /// Get a substring of the binary string between the specified start and end bit positions.
        /// </summary>
        /// <param name="binaryWord">The binary string to evaluate.</param>
        /// <param name="startBitPosition">The start position of the bit range.</param>
        /// <param name="endBitPosition">The end position of the bit range.</param>
        /// <returns>A binary substring from the specified range.</returns>
        /// <exception cref="ArgumentNullException">Thrown when binaryWord is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when startBitPosition or endBitPosition are out of range.</exception>
        /// <exception cref="ArgumentException">Thrown when endBitPosition is not greater than startBitPosition.</exception>
        public static string GetBinaryValueFromBinaryWord(string binaryWord, int startBitPosition, int endBitPosition)
        {
            if (String.IsNullOrEmpty(binaryWord))
            {
                throw new ArgumentNullException(nameof(binaryWord));
            }

            if (startBitPosition < 0 || startBitPosition >= binaryWord.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startBitPosition));
            }

            if (endBitPosition < 0 || endBitPosition > binaryWord.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(endBitPosition));
            }

            if (startBitPosition >= endBitPosition)
            {
                throw new ArgumentException("EndBitPosition must be superior to StartBitPosition");
            }

            int startIndex = (binaryWord.Length) - endBitPosition;
            int length = endBitPosition - startBitPosition;
            return binaryWord.Substring(startIndex, length);
        }

        #endregion

        #region Get Value from word

        /// <summary>
        /// Get bool value from a specific bit position in a long word.
        /// </summary>
        /// <param name="value">The long value to evaluate.</param>
        /// <param name="position">The position of the bit to evaluate.</param>
        /// <returns>True if the bit at the specified position is 1, otherwise false.</returns>
        public static bool GetBoolFromWord(this long value, int position)
        {
            string binaryWord = value.ToBinary();
            return GetBoolFromBinaryWord(binaryWord, position);
        }

        /// <summary>
        /// Get a binary substring from a specific bit range in a long word.
        /// </summary>
        /// <param name="value">The long value to evaluate.</param>
        /// <param name="startBitPosition">The start position of the bit range.</param>
        /// <param name="endBitPosition">The end position of the bit range.</param>
        /// <returns>A binary substring from the specified range.</returns>
        public static string GetBinaryValueFromBinaryWord(this long value, int startBitPosition, int endBitPosition)
        {
            string binaryWord = value.ToBinary();
            return GetBinaryValueFromBinaryWord(binaryWord, startBitPosition, endBitPosition);
        }

        /// <summary>
        /// Get integer value from a specific bit range in a double word, with an option to invert the boolean value.
        /// </summary>
        /// <param name="valueDouble">The double value to evaluate.</param>
        /// <param name="startBitPosition">The start position of the bit range.</param>
        /// <param name="endBitPosition">The end position of the bit range.</param>
        /// <param name="isInverted">Whether to invert the boolean value.</param>
        /// <returns>An integer value from the specified range, or null if the range is invalid.</returns>
        public static int? GetValueFromWordBinary(this double valueDouble, int startBitPosition, int endBitPosition, bool isInverted = false)
        {
            long value = Convert.ToInt64(valueDouble);
            int? result = null;
            if (startBitPosition == endBitPosition || (startBitPosition + 1) == endBitPosition)
            {
                // Boolean
                bool bValue = value.GetBoolFromWord(startBitPosition);
                if (isInverted)
                {
                    bValue = !bValue;
                }
                result = bValue ? 1 : 0;
            }
            else
            {
                // Enum
                string strValue = value.GetBinaryValueFromBinaryWord(startBitPosition,
                    endBitPosition);
                if (String.IsNullOrEmpty(strValue) == false)
                {
                    result = Convert.ToInt32(strValue, 2);
                }
            }

            return result;
        }

        #endregion
    }
}
