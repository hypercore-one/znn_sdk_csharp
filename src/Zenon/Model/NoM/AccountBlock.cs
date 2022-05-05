using System.Linq;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class AccountBlock : AccountBlockTemplate
    {
        public AccountBlock(JAccountBlock json)
            : base(json)
        {
            DescendantBlocks = json.descendantBlocks.Select(x => new AccountBlock(x)).ToArray();
            BasePlasma = json.basePlasma;
            UsedPlasma = json.usedPlasma;
            ChangesHash = Hash.Parse(json.changesHash);
            Token = json.token != null ? new Token(json.token) : null;
            ConfirmationDetail = json.confirmationDetail != null ? new AccountBlockConfirmationDetail(json.confirmationDetail) : null;
            PairedAccountBlock = json.pairedAccountBlock != null ? new AccountBlock(json.pairedAccountBlock) : null;
        }

        public AccountBlock[] DescendantBlocks { get; }
        public long BasePlasma { get; }
        public long UsedPlasma { get; }
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

            json.descendantBlocks = DescendantBlocks.Select(x => x.ToJson()).ToArray();
            json.usedPlasma = UsedPlasma;
            json.basePlasma = BasePlasma;
            json.changesHash = ChangesHash.ToString();
            json.token = Token != null ? Token.ToJson() : null;
            json.confirmationDetail = ConfirmationDetail != null ? ConfirmationDetail.ToJson() : null;
            json.pairedAccountBlock = PairedAccountBlock != null ? PairedAccountBlock.ToJson() : null;
        }
    }
}
