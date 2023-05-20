using System.Numerics;
using Zenon.Model.NoM.Json;
using Zenon.Utils;

namespace Zenon.Model.NoM
{
    public class BalanceInfoListItem : IJsonConvertible<JBalanceInfoListItem>
    {
        public BalanceInfoListItem(JBalanceInfoListItem json)
        {
            Token = json.token != null ? new Token(json.token) : null;
            Balance = BigInteger.Parse(json.balance);
        }

        public BalanceInfoListItem(Token token, BigInteger? balance)
        {
            Token = token;
            Balance = balance;
        }

        public Token Token { get; }
        public BigInteger? Balance { get; }

        public virtual JBalanceInfoListItem ToJson()
        {
            var data = new JBalanceInfoListItem();
            if (this.Token != null)
            {
                data.token = this.Token!.ToJson();
            }
            data.balance = Balance.ToString();
            return data;
        }
    }
}
