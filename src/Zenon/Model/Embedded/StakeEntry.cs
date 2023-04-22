using Zenon.Model.Embedded.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class StakeEntry : IJsonConvertible<JStakeEntry>
    {
        public StakeEntry(JStakeEntry json)
        {
            Amount = json.amount;
            WeightedAmount = json.weightedAmount;
            StartTimestamp = json.startTimestamp;
            ExpirationTimestamp = json.expirationTimestamp;
            Address = Address.Parse(json.address);
            Id = Hash.Parse(json.id);
        }

        public StakeEntry(long amount, long weightedAmount, long startTimestamp, long expirationTimestamp, Address address, Hash id)
        {
            Amount = amount;
            WeightedAmount = weightedAmount;
            StartTimestamp = startTimestamp;
            ExpirationTimestamp = expirationTimestamp;
            Address = address;
            Id = id;
        }

        public long Amount { get; }
        public long WeightedAmount { get; }
        public long StartTimestamp { get; }
        public long ExpirationTimestamp { get; }
        public Address Address { get; }
        public Hash Id { get; }

        public virtual JStakeEntry ToJson()
        {
            return new JStakeEntry()
            {
                amount = Amount,
                weightedAmount = WeightedAmount,
                startTimestamp = StartTimestamp,
                expirationTimestamp = ExpirationTimestamp,
                address = Address.ToString(),
                id = Id.ToString()
            };
        }
    }
}
