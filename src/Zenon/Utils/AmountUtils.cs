using System.Globalization;
using System.Numerics;

namespace Zenon.Utils
{
    public static class AmountUtils
    {
        public static BigInteger ParseAmount(string value)
        {
            return string.IsNullOrEmpty(value) ? BigInteger.Zero : BigInteger.Parse(value, CultureInfo.InvariantCulture);
        }

        public static BigInteger ExtractDecimals(double value, int decimals)
        {
            return BigInteger.Parse(value.ToString("0." + new string('0', decimals), CultureInfo.InvariantCulture)
                .Replace(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator, ""));
        }

        public static string AddDecimals(BigInteger value, int decimals)
        {
            return CreateAndStripZerosForScale(value, decimals, 0);
        }

        public static string CreateAndStripZerosForScale(
            BigInteger intVal,
            int scale,
            int preferredScale)
        {
            var ten = new BigInteger(10);

            while (intVal.CompareTo(ten) >= 0 && scale > preferredScale)
            {
                if (!intVal.IsEven)
                {
                    break;
                }
                var remainder = BigInteger.Remainder(intVal, ten);

                if (remainder.Sign != 0)
                {
                    break;
                }
                intVal = BigInteger.Divide(intVal, ten);
                scale += -1;
            }

            var strVal = intVal.ToString();
            return scale > 0
                ? strVal.Insert(strVal.Length - scale, CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)
                : strVal;
        }
    }
}