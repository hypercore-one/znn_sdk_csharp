using System.Globalization;
using System.Numerics;

namespace Zenon.Utils
{
    public static class AmountUtils
    {
        private const char NumberDecimalSeparator = '.';

        public static BigInteger ParseAmount(string value)
        {
            return string.IsNullOrEmpty(value) 
                ? BigInteger.Zero
                : BigInteger.Parse(value, CultureInfo.InvariantCulture);
        }

        public static BigInteger ExtractDecimals(string amount, int decimals)
        {
            if (!amount.Contains(NumberDecimalSeparator))
            {
                return ParseAmount(amount + new string('0', decimals));
            }
            var parts = amount.Split(NumberDecimalSeparator);

            return ParseAmount(parts[0] +
                (parts[1].Length > decimals
                    ? parts[1].Substring(0, decimals)
                    : parts[1].PadRight(decimals, '0')));
        }

        public static string AddDecimals(BigInteger value, int decimals)
        {
            return CreateAndStripZerosForScale(value, decimals, 0);
        }

        private static string CreateAndStripZerosForScale(
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

            var strVal = intVal.ToString().PadLeft(scale, '0');
            return scale > 0
                ? strVal.Insert(strVal.Length - scale, NumberDecimalSeparator.ToString())
                : strVal;
        }
    }
}