using System.Numerics;
using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class FusionEntry : IJsonConvertible<JFusionEntry>
    {
        public FusionEntry(JFusionEntry json)
        {
            QsrAmount = BigInteger.Parse(json.qsrAmount);
            Beneficiary = Address.Parse(json.beneficiary);
            ExpirationHeight = json.expirationHeight;
            Id = Hash.Parse(json.id);
        }

        public FusionEntry(Address beneficiary, long expirationHeight, Hash id, BigInteger qsrAmount)
        {
            Beneficiary = beneficiary;
            ExpirationHeight = expirationHeight;
            Id = id;
            QsrAmount = qsrAmount;
        }

        public BigInteger QsrAmount { get; }
        public Address Beneficiary { get; }
        public long ExpirationHeight { get; }
        public Hash Id { get; }
        public bool? IsRevocable { get; }

        public virtual JFusionEntry ToJson()
        {
            return new JFusionEntry()
            {
                qsrAmount = QsrAmount.ToString(),
                beneficiary = Beneficiary.ToString(),
                expirationHeight = ExpirationHeight,
                id = Id.ToString()
            };
        }
    }
}
