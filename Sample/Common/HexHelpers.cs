using System.Globalization;

namespace Sample
{
    public static class HexHelpers
    {
        public static string ToHex(this int value)
        {
            return String.Format("0x{0:X}", value);
        }

        public static decimal ToDecimal(this string value)
        {
            //// strip the leading 0x
            //if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            //{
            //    value = value.Substring(2);
            //}
            ////return decimal.Parse(value, NumberStyles.AllowHexSpecifier);
            //return int.Parse(value, NumberStyles.HexNumber);

            return Convert.ToInt64(value, 16);
        }
    }
}
