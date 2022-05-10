using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class FusionEntry : IJsonConvertible<JFusionEntry>
    {
        public FusionEntry(JFusionEntry json)
        {
            QsrAmount = json.qsrAmount;
            Beneficiary = Address.Parse(json.beneficiary);
            ExpirationHeight = json.expirationHeight;
            Id = Hash.Parse(json.id);
        }

        public FusionEntry(Address beneficiary, long expirationHeight, Hash id, long qsrAmount)
        {
            Beneficiary = beneficiary;
            ExpirationHeight = expirationHeight;
            Id = Id;
            QsrAmount = qsrAmount;
        }

        public long QsrAmount { get; }
        public Address Beneficiary { get; }
        public long ExpirationHeight { get; }
        public Hash Id { get; }
        public bool? IsRevocable { get; }

        public virtual JFusionEntry ToJson()
        {
            return new JFusionEntry()
            {
                qsrAmount = QsrAmount,
                beneficiary = Beneficiary.ToString(),
                expirationHeight = ExpirationHeight,
                id = Id.ToString()
            };
        }
    }
}
