using System;

namespace Zenon.Utils
{
    public static class AmountUtils
    {
        public static long ExtractDecimals(double num, long decimals) =>
            Convert.ToInt64(num * Math.Pow(10, decimals));

        public static double AddDecimals(long num, long decimals)
        {
            var numberWithDecimals = num / Math.Pow(10, decimals);
            if (numberWithDecimals == Convert.ToInt64(numberWithDecimals))
            {
                return Convert.ToInt64(numberWithDecimals);
            }
            return numberWithDecimals;
        }
    }
}
