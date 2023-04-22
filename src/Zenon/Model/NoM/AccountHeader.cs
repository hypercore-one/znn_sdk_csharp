using Newtonsoft.Json;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class AccountHeader : IJsonConvertible<JAccountHeader>
    {
        public AccountHeader(JAccountHeader json)
        {
            Address = Address.Parse(json.address);
            Hash = Hash.Parse(json.hash);
            Height = json.height;
        }

        public AccountHeader(Hash hash, long? height)
        {
            Hash = hash;
            Height = height;
        }

        /// Added here for simplicity. Is not part of the RPC response.
        public Address Address { get; }
        public Hash Hash { get; }
        public long? Height { get; }

        public virtual JAccountHeader ToJson()
        {
            return new JAccountHeader()
            {
                address = Address.ToString(),
                hash = Hash.ToString(),
                height = Height
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}