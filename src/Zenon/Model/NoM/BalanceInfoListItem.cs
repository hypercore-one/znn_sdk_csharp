using Zenon.Model.NoM.Json;
using Zenon.Utils;

namespace Zenon.Model.NoM
{
    public class BalanceInfoListItem
    {
        public BalanceInfoListItem(JBalanceInfoListItem json)
        {
            Token = json.token != null ? new Token(json.token) : null;
            Balance = json.balance;
        }

        public BalanceInfoListItem(Token token, long? balance)
        {
            Token = token;
            Balance = balance;
            BalanceWithDecimals = AmountUtils.AddDecimals(balance.Value, token!.Decimals);
            BalanceFormatted = $"{BalanceWithDecimals} {Token!.Symbol}";
        }

        public Token Token { get; }
        public long? Balance { get; }
        public double? BalanceWithDecimals { get; }
        public string BalanceFormatted { get; }

        public virtual JBalanceInfoListItem ToJson()
        {
            var data = new JBalanceInfoListItem();
            if (Token != null)
            {
                data.token = Token!.ToJson();
            }
            data.balance = Balance;
            return data;
        }
    }
}
