using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class UncollectedReward
    {
        public UncollectedReward(JUncollectedReward json)
        {
            Address = Address.Parse(json.address);
            ZnnAmount = json.znnAmount;
            QsrAmount = json.qsrAmount;
        }

        public Address Address { get; }
        public long ZnnAmount { get; }
        public long QsrAmount { get; }

        public virtual JUncollectedReward ToJson()
        {
            return new JUncollectedReward()
            {
                address = Address.ToString(),
                znnAmount = ZnnAmount,
                qsrAmount = QsrAmount
            };
        }
    }
}
