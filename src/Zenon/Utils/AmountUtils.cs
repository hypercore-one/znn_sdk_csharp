using System;

namespace Zenon.Utils
{
    public static class AmountUtils
    {
        public static long ExtractDecimals(double num, long decimals) =>
            (long)(num * Math.Pow(10, decimals));

        public static double AddDecimals(long num, long decimals)
        {
            var numberWithDecimals = num / Math.Pow(10, decimals);
            if (numberWithDecimals == (long)numberWithDecimals)
            {
                return (long)numberWithDecimals;
            }
            return numberWithDecimals;
        }
    }
}
