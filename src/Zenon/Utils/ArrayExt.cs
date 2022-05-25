using System;
using System.Linq;

namespace Zenon.Utils
{
    public static class ArrayExt
    {
        /// <summary>
        /// Returns a new array containing the elements between start and length of the array.
        /// </summary>
        /// <typeparam name="T">The type of the array elements</typeparam>
        /// <param name="array">The array containing the elements</param>
        /// <param name="start">The zero-based index of the first element in the range.</param>
        /// <returns>A new array containing the specified range of the elements</returns>
        public static T[] Sublist<T>(this T[] array, int start)
        {
            return Sublist<T>(array, start, array.Length);
        }

        /// <summary>
        /// Returns a new array containing the elements between start and end.
        /// </summary>
        /// <typeparam name="T">The type of the array elements</typeparam>
        /// <param name="array"></param>
        /// <param name="start">The zero-based index of the first element in the range.</param>
        /// <param name="end">The zero-based index of the last element in the range.</param>
        /// <returns>A new array containing the specified range of the elements</returns>
        /// <remarks>
        /// The <paramref name="start"/> and <paramref name="end"/> must satisfy 
        /// the relations 0 ≤ <paramref name="start"/> ≤ <paramref name="end"/> ≤ length. 
        /// If <paramref name="end"/> is equal to <paramref name="start"/>, then the returned array is empty.
        /// </remarks>
        public static T[] Sublist<T>(this T[] array, int start, int end)
        {
            return start != end
                ? new ArraySegment<T>(array, start, end - start).ToArray()
                : Array.Empty<T>();
        }
    }
}
