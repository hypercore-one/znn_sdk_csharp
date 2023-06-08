using System.Numerics;

namespace Zenon.Utils
{
    public static class AmountUtils
    {
        public static BigInteger ParseAmount(string value)
        {
            return string.IsNullOrEmpty(value) ? BigInteger.Zero : BigInteger.Parse(value);
        }
    }
}