using System.Collections;
using System.Collections.Generic;

namespace Zenon.Tests.TestData
{
    internal class HashTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                "b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9"
            };
            yield return new object[]
            {
                "6a50df02d365fe8034881d0bac17be58d5af4f5efec37c4b965a7ba05a557df0"
            };
            yield return new object[]
            {
                "9ef6873791c43a3f380671970c58672e9604c617b529fca282b07e36576f8743"
            };
            yield return new object[]
            {
                "29e36ceb9e12c8dd5c7f42b7fc7e0236fe3df2ac558bd60d1d27e329f75e1514"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
