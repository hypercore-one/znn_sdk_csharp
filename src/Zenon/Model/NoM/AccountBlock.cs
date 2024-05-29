using System.Linq;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class AccountBlock : AccountBlockTemplate, IJsonConvertible<JAccountBlock>
    {
        public AccountBlock(JAccountBlock json)
            : base(json)
        {
            this.DescendantBlocks = json.descendantBlocks.Select(x => new AccountBlock(x)).ToArray();
            this.BasePlasma = json.basePlasma;
            this.UsedPlasma = json.usedPlasma;
            this.ChangesHash = Hash.Parse(json.changesHash);
            this.Token = json.token != null ? new Token(json.token) : null;
            this.ConfirmationDetail = json.confirmationDetail != null ? new AccountBlockConfirmationDetail(json.confirmationDetail) : null;
            this.PairedAccountBlock = json.pairedAccountBlock != null ? new AccountBlock(json.pairedAccountBlock) : null;
        }

        public AccountBlock[] DescendantBlocks { get; }
        public ulong BasePlasma { get; }
        public ulong UsedPlasma { get; }
        public Hash ChangesHash { get; }
        public Token Token { get; }
        // Available if account-block is confirmed, null otherwise
        public AccountBlockConfirmationDetail ConfirmationDetail { get; }
        public AccountBlock PairedAccountBlock { get; }

        public bool IsCompleted => ConfirmationDetail != null;

        public override JAccountBlock ToJson()
        {
            var data = new JAccountBlock();
            ToJson(data);
            return data;
        }

        public virtual void ToJson(JAccountBlock json)
        {
            base.ToJson(json);

            json.descendantBlocks = this.DescendantBlocks.Select(x => x.ToJson()).ToArray();
            json.usedPlasma = this.UsedPlasma;
            json.basePlasma = this.BasePlasma;
            json.changesHash = this.ChangesHash.ToString();
            json.token = Token != null ? Token.ToJson() : null;
            json.confirmationDetail = this.ConfirmationDetail != null ? this.ConfirmationDetail.ToJson() : null;
            json.pairedAccountBlock = this.PairedAccountBlock != null ? this.PairedAccountBlock.ToJson() : null;
        }
    }
}
