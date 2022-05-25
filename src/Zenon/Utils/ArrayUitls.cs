using System;
using System.Linq;

namespace Zenon.Utils
{
    internal static class ArrayUtils
    {
        /// <summary>
        /// Concatenates an abritary set of one-dimensional arrays of the same type.
        /// </summary>
        /// <typeparam name="T">The type of the one-dimensional arrays.</typeparam>
        /// <param name="arrs">The set of one-dimensional arrays.</param>
        /// <returns></returns>
        public static T[] Concat<T>(params T[][] arrs)
        {
            var result = new T[arrs.Sum(a => a.Length)];
            var offset = 0;
            for (int i = 0; i < arrs.Length; i++)
            {
                arrs[i].CopyTo(result, offset);
                offset += arrs[i].Length;
            }
            return result;
        }
    }
}
