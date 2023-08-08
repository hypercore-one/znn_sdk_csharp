using System;
using System.Numerics;
using System.Threading.Tasks;
using Zenon.Client;
using Zenon.Embedded;
using Zenon.Model.NoM;
using Zenon.Model.NoM.Json;
using Zenon.Model.Primitives;

namespace Zenon.Api.Embedded
{
    public class TokenApi
    {
        public TokenApi(IClient client)
        {
            Client = client;
        }

        public IClient Client { get; }

        public async Task<TokenList> GetAll(int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JTokenList>("embedded.token.getAll", pageIndex, pageSize);
            return new TokenList(response);
        }

        public async Task<TokenList> GetByOwner(Address address, int pageIndex = 0, int pageSize = Constants.RpcMaxPageSize)
        {
            var response = await Client.SendRequestAsync<JTokenList>("embedded.token.getByOwner", address.ToString(), pageIndex, pageSize);
            return new TokenList(response);
        }

        public async Task<Token> GetByZts(TokenStandard tokenStandard)
        {
            var response = await Client.SendRequestAsync<JToken>("embedded.token.getByZts", tokenStandard.ToString());
            return response != null ? new Token(response) : null;
        }

        // Contract methods
        public AccountBlockTemplate IssueToken(string tokenName, string tokenSymbol, string tokenDomain,
            long totalSupply, long maxSupply, int decimals,
            bool mintable, bool burnable, bool utility)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.TokenAddress, TokenStandard.ZnnZts, Constants.TokenZtsIssueFeeInZnn,
                Definitions.Token.EncodeFunction("IssueToken",
                    tokenName,
                    tokenSymbol,
                    tokenDomain,
                    totalSupply,
                    maxSupply,
                    decimals,
                    mintable,
                    burnable,
                    utility));
        }

        public AccountBlockTemplate MintToken(TokenStandard tokenStandard, BigInteger amount, Address receiveAddress)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.TokenAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Token.EncodeFunction("Mint", tokenStandard, amount, receiveAddress));
        }

        public AccountBlockTemplate BurnToken(TokenStandard tokenStandard, BigInteger amount)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.TokenAddress, tokenStandard, amount,
                Definitions.Token.EncodeFunction("Burn"));
        }

        public AccountBlockTemplate UpdateToken(TokenStandard tokenStandard, Address owner, bool isMintable, bool isBurnable)
        {
            return AccountBlockTemplate.CallContract(Client.ProtocolVersion, Client.ChainIdentifier, Address.TokenAddress, TokenStandard.ZnnZts, BigInteger.Zero,
                Definitions.Token.EncodeFunction("UpdateToken", tokenStandard, owner, isMintable, isBurnable));
        }
    }
}
