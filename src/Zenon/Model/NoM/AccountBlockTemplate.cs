using Newtonsoft.Json;
using System;
using Zenon.Model.NoM.Json;
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

    public class AccountBlockTemplate : IJsonConvertible<JAccountBlockTemplate>
    {
        public static AccountBlockTemplate Receive(Hash fromBlockHash) =>
            new AccountBlockTemplate(blockType: BlockTypeEnum.UserReceive, fromBlockHash: fromBlockHash);

        public static AccountBlockTemplate Send(Address toAddress, TokenStandard tokenStandard, long amount, byte[] data = null) =>
            new AccountBlockTemplate(blockType: BlockTypeEnum.UserSend, toAddress: toAddress, tokenStandard: tokenStandard, amount: amount, data: data);

        public static AccountBlockTemplate CallContract(Address toAddress, TokenStandard tokenStandard, long amount, byte[] data) =>
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
            PublicKey = string.IsNullOrEmpty(json.publicKey) ? new byte[0] : Convert.FromBase64String(json.publicKey);
            Signature = string.IsNullOrEmpty(json.signature) ? new byte[0] : Convert.FromBase64String(json.signature);
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
            json.data = Convert.ToBase64String(this.Data);
            json.fusedPlasma = this.FusedPlasma;
            json.difficulty = this.Difficulty;
            json.nonce = this.Nonce;
            json.publicKey = Convert.ToBase64String(this.PublicKey);
            json.signature = Convert.ToBase64String(this.Signature);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}
