using System;
using System.Linq;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class Momentum
    {
        public Momentum(JMomentum json)
        {
            Version = json.version;
            ChainIdentifier = json.chainIdentifier;
            Hash = Hash.Parse(json.hash);
            PreviousHash = Hash.Parse(json.previousHash);
            Height = json.height;
            Timestamp = json.timestamp;
            Data = string.IsNullOrEmpty(json.data) ? new byte[0] : Convert.FromBase64String(json.data);
            Content = json.content.Select(x => new AccountHeader(x)).ToArray();
            ChangesHash = Hash.Parse(json.changesHash);
            PublicKey = json.publicKey ?? string.Empty;
            Signature = json.signature ?? string.Empty;
            Producer = Address.Parse(json.producer);
        }

        public int Version { get; }
        public int ChainIdentifier { get; }
        public Hash Hash { get; }
        public Hash PreviousHash { get; }
        public long Height { get; }
        public long Timestamp { get; }
        public byte[] Data { get; }
        public AccountHeader[] Content { get; }
        public Hash ChangesHash { get; }
        public string PublicKey { get; }
        public string Signature { get; }
        public Address Producer { get; }

        public virtual JMomentum ToJson()
        {
            return new JMomentum()
            {
                version = Version,
                chainIdentifier = ChainIdentifier,
                hash = Hash.ToString(),
                previousHash = PreviousHash.ToString(),
                height = Height,
                timestamp = Timestamp,
                data = Data != null && Data.Length != 0 ? Convert.ToBase64String(Data) : string.Empty,
                content = Content.Select(x => x.ToJson()).ToArray(),
                changesHash = ChangesHash.ToString(),
                publicKey = PublicKey,
                signature = Signature,
                producer = Producer.ToString()
            };
        }
    }
}
