using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Model.NoM
{
    public class AccountInfo : IJsonConvertible<JAccountInfo>
    {
        public string Address { get; }
        public ulong? BlockCount { get; }
        public BalanceInfoListItem[] BalanceInfoList { get; }

        public AccountInfo(JAccountInfo json)
        {
            Address = json.address;
            BlockCount = json.accountHeight;
            BalanceInfoList = this.BlockCount > 0
                ? json.balanceInfoMap.Select(x => new BalanceInfoListItem(x.Value)).ToArray()
                : new BalanceInfoListItem[0];
        }

        public AccountInfo(string address, ulong? blockCount, BalanceInfoListItem[] balanceInfoList)
        {
            Address = address;
            BlockCount = blockCount;
            BalanceInfoList = balanceInfoList;
        }

        public BigInteger? Znn => GetBalance(TokenStandard.ZnnZts);

        public BigInteger? Qsr => GetBalance(TokenStandard.QsrZts);

        public BigInteger GetBalance(TokenStandard tokenStandard)
        {
            var info = BalanceInfoList!.FirstOrDefault(
                x => x.Token!.TokenStandard == tokenStandard);
            return info?.Balance ?? BigInteger.Zero;
        }

        public Token FindTokenByTokenStandard(TokenStandard tokenStandard)
        {
            try
            {
                return BalanceInfoList!
                    .First((x) => x.Token!.TokenStandard == tokenStandard)
                    .Token;
            }
            catch
            {
                return null;
            }
        }

        public virtual JAccountInfo ToJson()
        {
            return new JAccountInfo()
            {
                address = this.Address.ToString(),
                accountHeight = this.BlockCount,
                balanceInfoMap = this.BalanceInfoList
                    .ToDictionary(x => x.Token.TokenStandard.ToString(), x => x.ToJson())
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(ToJson(), Formatting.None);
        }
    }
}