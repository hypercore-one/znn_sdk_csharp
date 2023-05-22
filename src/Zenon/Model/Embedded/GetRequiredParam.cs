using Newtonsoft.Json;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.Embedded
{
    public class GetRequiredParam : IJsonConvertible<JGetRequiredParam>
    {
        public GetRequiredParam(JGetRequiredParam json)
        {
            Address = Address.Parse(json.address);
            BlockType = (BlockTypeEnum)json.blockType;
            ToAddress = json.address != null ? Address.Parse(json.address) : null;
            Data = BytesUtils.FromBase64String(json.data);
        }

        public GetRequiredParam(Address address, BlockTypeEnum blockType, Address toAddress, byte[] data)
        {
            Address = address;
            BlockType = blockType;
            Data = data;
            ToAddress = toAddress;

            if (blockType == BlockTypeEnum.UserReceive)
            {
                ToAddress = address;
            }
        }

        public Address Address { get; }
        public BlockTypeEnum BlockType { get; }
        public Address ToAddress { get; }
        public byte[] Data { get; }

        public virtual JGetRequiredParam ToJson()
        {
            return new JGetRequiredParam()
            {
                address = Address.ToString(),
                blockType = (int)BlockType,
                toAddress = ToAddress != null ? ToAddress.ToString() : null,
                data = BytesUtils.ToBase64String(Data)
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson());
        }
    }
}
