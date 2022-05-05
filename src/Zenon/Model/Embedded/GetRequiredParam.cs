using Newtonsoft.Json;
using System;
using Zenon.Model.Embedded.Json;
using Zenon.Model.NoM;
using Zenon.Model.Primitives;

namespace Zenon.Model.Embedded
{
    public class GetRequiredParam
    {
        public GetRequiredParam(JGetRequiredParam json)
        {
            Address = Address.Parse(json.address);
            BlockType = (BlockTypeEnum)json.blockType;
            ToAddress = json.address != null ? Address.Parse(json.address) : null;
            Data = Convert.FromBase64String(json.data);
        }

        public GetRequiredParam(Address address, BlockTypeEnum blockType, Address toAddress, byte[] data)
        {
            Address = address;
            BlockType = blockType;
            Data = data;

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
                data = Convert.ToBase64String(Data)
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson());
        }
    }
}
