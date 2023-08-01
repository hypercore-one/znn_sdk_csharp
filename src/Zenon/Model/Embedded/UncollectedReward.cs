using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class UncollectedReward : IJsonConvertible<JUncollectedReward>
    {
        public UncollectedReward(JUncollectedReward json)
        {
            Address = Address.Parse(json.address);
            ZnnAmount = AmountUtils.ParseAmount(json.znnAmount);
            QsrAmount = AmountUtils.ParseAmount(json.qsrAmount);
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
