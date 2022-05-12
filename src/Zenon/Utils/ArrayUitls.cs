using System;
using System.Linq;

namespace Zenon.Utils
{
    internal static class ArrayUtils
    {
        public static byte[] Concat(params byte[][] arrs)
        {
            var len = arrs.Sum(a => a.Length);
            var ret = new byte[len];
            var pos = 0;
            foreach (var a in arrs)
            {
                Buffer.BlockCopy(a, 0, ret, pos, a.Length);
                pos += a.Length;
            }
            return ret;
        }
    }
}
