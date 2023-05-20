using System.Linq;
using System.Numerics;
using Zenon.Model.Embedded.Json;

namespace Zenon.Model.Embedded
{
    public class FusionEntryList : IJsonConvertible<JFusionEntryList>
    {
        public FusionEntryList(JFusionEntryList json)
        {
            QsrAmount = json.qsrAmount != null ? BigInteger.Parse(json.qsrAmount) : BigInteger.Zero;
            Count = json.count;
            List = json.list != null
                ? json.list.Select(x => new FusionEntry(x)).ToArray()
                : new FusionEntry[0];
        }

        public FusionEntryList(BigInteger qsrAmount, long count, FusionEntry[] list)
        {
            QsrAmount = qsrAmount;
            Count = count;
            List = list;
        }

        public BigInteger QsrAmount { get; }
        public long Count { get; }
        public FusionEntry[] List { get; }

        public virtual JFusionEntryList ToJson()
        {
            return new JFusionEntryList()
            {
                qsrAmount = QsrAmount.ToString(),
                count = Count,
                list = List.Select(x => x.ToJson()).ToArray()
            };
        }
    }
}
