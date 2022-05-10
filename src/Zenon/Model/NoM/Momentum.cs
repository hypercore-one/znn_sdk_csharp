using System;
using System.Linq;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class Momentum : IJsonConvertible<JMomentum>
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
                version = this.Version,
                chainIdentifier = this.ChainIdentifier,
                hash = this.Hash.ToString(),
                previousHash = this.PreviousHash.ToString(),
                height = this.Height,
                timestamp = this.Timestamp,
                data = this.Data != null && this.Data.Length != 0 ? Convert.ToBase64String(Data) : string.Empty,
                content = this.Content.Select(x => x.ToJson()).ToArray(),
                changesHash = this.ChangesHash.ToString(),
                publicKey = this.PublicKey,
                signature = this.Signature,
                producer = this.Producer.ToString()
            };
        }
    }
}
