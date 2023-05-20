using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class UncollectedReward : IJsonConvertible<JUncollectedReward>
    {
        public UncollectedReward(JUncollectedReward json)
        {
            Address = Address.Parse(json.address);
            ZnnAmount = BigInteger.Parse(json.znnAmount);
            QsrAmount = BigInteger.Parse(json.qsrAmount);
        }

        public Address Address { get; }
        public BigInteger ZnnAmount { get; }
        public BigInteger QsrAmount { get; }

        public virtual JUncollectedReward ToJson()
        {
            return new JUncollectedReward()
            {
                address = Address.ToString(),
                znnAmount = ZnnAmount.ToString(),
                qsrAmount = QsrAmount.ToString()
            };
        }
    }
}
