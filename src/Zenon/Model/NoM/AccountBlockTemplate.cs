using Newtonsoft.Json;
using System;
using Zenon.Model.Primitives;

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

    public class AccountBlockTemplate
    {
        public static AccountBlockTemplate Receive(Hash fromBlockHash) =>
            new AccountBlockTemplate(blockType: BlockTypeEnum.UserReceive, fromBlockHash: fromBlockHash);

        public static AccountBlockTemplate Send(Address toAddress, TokenStandard tokenStandard, int amount, byte[] data = null) =>
            new AccountBlockTemplate(blockType: BlockTypeEnum.UserSend, toAddress: toAddress, tokenStandard: tokenStandard, amount: amount, data: data);

        public static AccountBlockTemplate CallContract(Address toAddress, TokenStandard tokenStandard, int amount, byte[] data) =>
            new AccountBlockTemplate(blockType: BlockTypeEnum.UserSend, toAddress: toAddress, tokenStandard: tokenStandard, amount: amount, data: data);

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
            Data = string.IsNullOrEmpty(json.data) ? new byte[0] : Convert.FromBase64String(json.data);
            FusedPlasma = json.fusedPlasma;
            Difficulty = json.difficulty;
            Nonce = json.nonce;
            PublicKey = string.IsNullOrEmpty(json.data) ? new byte[0] : Convert.FromBase64String(json.publicKey);
            Signature = string.IsNullOrEmpty(json.data) ? new byte[0] : Convert.FromBase64String(json.signature);
        }

        public AccountBlockTemplate(BlockTypeEnum blockType,
            Address toAddress = null,
            long? amount = null,
            TokenStandard tokenStandard = null,
            Hash fromBlockHash = null,
            byte[] data = null)
        {
            Version = 1;
            ChainIdentifier = Constants.NetId;
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

        public Hash Hash { get; }
        public Hash PreviousHash { get; }
        public long Height { get; }
        public HashHeight MomentumAcknowledged { get; }

        public Address Address { get; }

        // Send information
        public Address ToAddress { get; }

        public long Amount { get; }
        public TokenStandard TokenStandard { get; }

        // Receive information
        public Hash FromBlockHash { get; }

        public byte[] Data { get; }

        // PoW
        public long FusedPlasma { get; }
        public long Difficulty { get; }

        // Hex representation of 8 byte nonce
        public string Nonce { get; }

        // Verification
        public byte[] PublicKey { get; }
        public byte[] Signature { get; }

        public virtual Json.JAccountBlockTemplate ToJson()
        {
            var data = new Json.JAccountBlockTemplate();
            ToJson(data);
            return data;
        }

        public virtual void ToJson(Json.JAccountBlockTemplate json)
        {
            json.version = Version;
            json.chainIdentifier = ChainIdentifier;
            json.blockType = (int)BlockType;
            json.hash = Hash.ToString();
            json.previousHash = PreviousHash.ToString();
            json.height = Height;
            json.momentumAcknowledged = MomentumAcknowledged.ToJson();
            json.address = Address.ToString();
            json.toAddress = ToAddress.ToString();
            json.amount = Amount;
            json.tokenStandard = TokenStandard.ToString();
            json.fromBlockHash = FromBlockHash.ToString();
            json.data = Convert.ToBase64String(Data);
            json.fusedPlasma = FusedPlasma;
            json.difficulty = Difficulty;
            json.nonce = Nonce;
            json.publicKey = Convert.ToBase64String(PublicKey);
            json.signature = Convert.ToBase64String(Signature);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}
