using Newtonsoft.Json;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Model.NoM
{
    public enum BlockTypeEnum
    {
        Unknown,
        GenesisReceive,
        UserSend,
        UserReceive,
        ContractSend,
        ContractReceive
    }

    public class AccountBlockTemplate : IJsonConvertible<JAccountBlockTemplate>
    {
        public static AccountBlockTemplate Receive(int protocolVersion, int chainIdentifier, Hash fromBlockHash) =>
            new AccountBlockTemplate(protocolVersion, chainIdentifier, BlockTypeEnum.UserReceive, fromBlockHash: fromBlockHash);

        public static AccountBlockTemplate Send(int protocolVersion, int chainIdentifier, Address toAddress, TokenStandard tokenStandard, long amount, byte[] data = null) =>
            new AccountBlockTemplate(protocolVersion, chainIdentifier, BlockTypeEnum.UserSend, toAddress, amount, tokenStandard, data: data);

        public static AccountBlockTemplate CallContract(int protocolVersion, int chainIdentifier, Address toAddress, TokenStandard tokenStandard, long amount, byte[] data) =>
            new AccountBlockTemplate(protocolVersion, chainIdentifier, BlockTypeEnum.UserSend, toAddress, amount, tokenStandard, data: data);

        public AccountBlockTemplate(Json.JAccountBlockTemplate json)
        {
            Version = json.version;
            ChainIdentifier = json.chainIdentifier;
            BlockType = (BlockTypeEnum)json.blockType;
            Hash = Hash.Parse(json.hash);
            PreviousHash = Hash.Parse(json.previousHash);
            Height = json.height;
            MomentumAcknowledged = new HashHeight(json.momentumAcknowledged);
            Address = Address.Parse(json.address);
            ToAddress = Address.Parse(json.toAddress);
            Amount = json.amount;
            TokenStandard = TokenStandard.Parse(json.tokenStandard);
            FromBlockHash = Hash.Parse(json.fromBlockHash);
            Data = string.IsNullOrEmpty(json.data) ? new byte[0] : BytesUtils.FromBase64String(json.data);
            FusedPlasma = json.fusedPlasma;
            Difficulty = json.difficulty;
            Nonce = json.nonce;
            PublicKey = string.IsNullOrEmpty(json.publicKey) ? new byte[0] : BytesUtils.FromBase64String(json.publicKey);
            Signature = string.IsNullOrEmpty(json.signature) ? new byte[0] : BytesUtils.FromBase64String(json.signature);
        }

        public AccountBlockTemplate(
            int protocolVersion,
            int chainIdentifier,
            BlockTypeEnum blockType,
            Address toAddress = null,
            long? amount = null,
            TokenStandard tokenStandard = null,
            Hash fromBlockHash = null,
            byte[] data = null)
        {
            Version = protocolVersion;
            ChainIdentifier = chainIdentifier;
            BlockType = blockType;
            Hash = Hash.Empty;
            PreviousHash = Hash.Empty;
            Height = 0;
            MomentumAcknowledged = HashHeight.Empty;
            Address = Address.EmptyAddress;
            ToAddress = toAddress ?? Address.EmptyAddress;
            Amount = amount ?? 0;
            TokenStandard = tokenStandard ?? TokenStandard.EmptyZts;
            FromBlockHash = fromBlockHash ?? Hash.Empty;
            Data = data ?? new byte[0];
            FusedPlasma = 0;
            Difficulty = 0;
            Nonce = string.Empty;
            PublicKey = new byte[0];
            Signature = new byte[0];
        }

        public int Version { get; }
        public int ChainIdentifier { get; }
        public BlockTypeEnum BlockType { get; }

        public Hash Hash { get; internal set; }
        public Hash PreviousHash { get; internal set; }
        public long Height { get; internal set; }
        public HashHeight MomentumAcknowledged { get; internal set; }

        public Address Address { get; set; }

        // Send information
        public Address ToAddress { get; }

        public long Amount { get; }
        public TokenStandard TokenStandard { get; }

        // Receive information
        public Hash FromBlockHash { get; }

        public byte[] Data { get; }

        // PoW
        public long FusedPlasma { get; internal set; }
        public long Difficulty { get; internal set; }

        // Hex representation of 8 byte nonce
        public string Nonce { get; internal set; }

        // Verification
        public byte[] PublicKey { get; internal set; }
        public byte[] Signature { get; internal set; }

        public virtual JAccountBlockTemplate ToJson()
        {
            var data = new JAccountBlockTemplate();
            ToJson(data);
            return data;
        }

        public virtual void ToJson(JAccountBlockTemplate json)
        {
            json.version = this.Version;
            json.chainIdentifier = this.ChainIdentifier;
            json.blockType = (int)this.BlockType;
            json.hash = this.Hash.ToString();
            json.previousHash = this.PreviousHash.ToString();
            json.height = this.Height;
            json.momentumAcknowledged = this.MomentumAcknowledged.ToJson();
            json.address = this.Address.ToString();
            json.toAddress = this.ToAddress.ToString();
            json.amount = this.Amount;
            json.tokenStandard = this.TokenStandard.ToString();
            json.fromBlockHash = this.FromBlockHash.ToString();
            json.data = BytesUtils.ToBase64String(this.Data);
            json.fusedPlasma = this.FusedPlasma;
            json.difficulty = this.Difficulty;
            json.nonce = this.Nonce;
            json.publicKey = BytesUtils.ToBase64String(this.PublicKey);
            json.signature = BytesUtils.ToBase64String(this.Signature);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}
