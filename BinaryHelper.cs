namespace Helpers;

/// <summary>
/// helper to get binary value from Word
/// </summary>
public static class BinaryHelper
{
    #region Convert to binary string
    /// <summary>
    /// Get binary string on 64 bits from long
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToBinary(this long value)
    {
        return Convert.ToString(value, 2).PadLeft(64, '0');
    }
    #endregion

    #region Get Value from binary string
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

        int startIndex = (binaryWord.Length-1) - position;
        string binaryValue = binaryWord.Substring(startIndex, 1);

        return binaryValue.Equals("1");
    }

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
    /// Get bool from word
    /// </summary>
    /// <param name="value">word</param>
    /// <param name="position">Bit position</param>
    /// <returns></returns>
    public static bool GetBoolFromWord(this long value, int position)
    {
        string binaryWord = value.ToBinary();
        return GetBoolFromBinaryWord(binaryWord, position);
    }


    /// <summary>
    /// Get binary value from binary word
    /// </summary>
    /// <param name="value"></param>
    /// <param name="startBitPosition"></param>
    /// <param name="endBitPosition"></param>
    /// <returns></returns>
    public static string GetBinaryValueFromBinaryWord(this long value, int startBitPosition, int endBitPosition)
    {
        string binaryWord = value.ToBinary();
        return GetBinaryValueFromBinaryWord(binaryWord, startBitPosition, endBitPosition);
    }

    public static int? GetValueFromWordBinary(this double valueDouble, int startBitPosition, int endBitPosition,  bool isInverted = false)
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