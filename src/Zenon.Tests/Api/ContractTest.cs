using FluentAssertions;
using Moq;
using System;
using System.Text;
using Xunit;
using Zenon.Api.Embedded;
using Zenon.Client;
using Zenon.Model.Primitives;
using Zenon.Utils;

namespace Zenon.Api
{
    public partial class ContractTest
    {
        public class EmbeddedApiFixture
        {
            public EmbeddedApiFixture()
            {
                this.Api = new EmbeddedApi(new Lazy<IClient>(() => new Mock<TestClient>().Object));
            }

            public EmbeddedApi Api { get; }
        }

        #region Bridge
        public class Bridge : IClassFixture<EmbeddedApiFixture>
        {
            public BridgeApi Api { get; }

            public Bridge(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Bridge;
            }

            [Fact]
            public void When_WrapToken_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1qanamzukd2v0pp8j2wzx6m", 5000,
                    "YdIkvAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHsAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAqMHhiNzk0ZjVlYTBiYTM5NDk0Y2U4Mzk2MTNmZmZiYTc0Mjc5NTc5MjY4AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.WrapToken(2, 123, "0xb794f5ea0ba39494ce839613fffba74279579268", 5000, TokenStandard.Parse("zts1qanamzukd2v0pp8j2wzx6m"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_UpdateWrapRequest_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "1LsRwLOgI9dRaBtq7VB/f3xPqKWdvufuEbPT45wpT8B42be5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWGVDS3ViV2h3bnVxcVg5dmpHbDlsdHF4Q053RTJWOVhpNGJPMXE0MDRKWU5DVWxKMGMzaDVDcTU1OHBMeHRpbXJTNzNoUFN0anR6MjgxK0djZk5QVHlBRT0AAAAAAAAAAA==");

                // Execute
                var block = this.Api.UpdateWrapRequest(Hash.Parse("b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9"), "eCKubWhwnuqqX9vjGl9ltqxCNwE2V9Xi4bO1q404JYNCUlJ0c3h5Cq558pLxtimrS73hPStjtz281+GcfNPTyAE=");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_UnwrapToken_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "tgaUAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHsRrHbkDMI2dDAPaMqH9evrchD8Mn/UPzUIG3WoOcnGMgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADIAAAAAAAAAAAAAAAAAOocmnNPcmAAR5wxKWIdkSl2gTYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKjB4NWFhYWEyMzE1Njc4YWZlY2IzNjdmMDMyZDkzZjY0MmY2NDE4MGFhMwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWGVDS3ViV2h3bnVxcVg5dmpHbDlsdHF4Q053RTJWOVhpNGJPMXE0MDRKWU5DVWxKMGMzaDVDcTU1OHBMeHRpbXJTNzNoUFN0anR6MjgxK0djZk5QVHlBRT0AAAAAAAAAAA==");

                // Execute
                var block = this.Api.UnwrapToken(2,
                    123,
                    "0x5aaaa2315678afecb367f032d93f642f64180aa3",
                    Hash.Parse("11ac76e40cc23674300f68ca87f5ebeb7210fc327fd43f35081b75a839c9c632"),
                    200,
                    800,
                    Address.Parse("z1qr4pexnnfaexqqz8nscjjcsajy5hdqfkgadvwx"),
                    "eCKubWhwnuqqX9vjGl9ltqxCNwE2V9Xi4bO1q404JYNCUlJ0c3h5Cq558pLxtimrS73hPStjtz281+GcfNPTyAE=");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Redeem_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "1OBseRGsduQMwjZ0MA9oyof16+tyEPwyf9Q/NQgbdag5ycYyAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMg=");

                // Execute
                var block = this.Api.Redeem(
                    Hash.Parse("11ac76e40cc23674300f68ca87f5ebeb7210fc327fd43f35081b75a839c9c632"),
                    200);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Halt_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "cjNNIQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFhlQ0t1Yldod251cXFYOXZqR2w5bHRxeENOd0UyVjlYaTRiTzFxNDA0SllOQ1VsSjBjM2g1Q3E1NThwTHh0aW1yUzczaFBTdGp0ejI4MStHY2ZOUFR5QUU9AAAAAAAAAAA=");

                // Execute
                var block = this.Api.Halt("eCKubWhwnuqqX9vjGl9ltqxCNwE2V9Xi4bO1q404JYNCUlJ0c3h5Cq558pLxtimrS73hPStjtz281+GcfNPTyAE=");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Unhalt_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "OhbyDg==");

                // Execute
                var block = this.Api.Unhalt();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetAllowKeyGen_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "S5s+ywAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB");

                // Execute
                var block = this.Api.SetAllowKeyGen(true);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_ChangeTssECDSAPubKey_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "FaDGQQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAOAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABYZUNLdWJXaHdudXFxWDl2akdsOWx0cXhDTndFMlY5WGk0Yk8xcTQwNEpZTkNVbEowYzNoNUNxNTU4cEx4dGltclM3M2hQU3RqdHoyODErR2NmTlBUeUFFPQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFhlQ0t1Yldod251cXFYOXZqR2w5bHRxeENOd0UyVjlYaTRiTzFxNDA0SllOQ1VsSjBjM2g1Q3E1NThwTHh0aW1yUzczaFBTdGp0ejI4MStHY2ZOUFR5QUU9AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWFlYRmVBSDJCSUtuYkhtN3FkMXpYdDZTU1JlSTlvdmhZZzVqVlBHdW1OT2tqM0J3Nk9mVXdTWlNDS1NNUEN3NE5SWkdEWFQ2bTdBeGU1VUl3QW5vNHhRRT0AAAAAAAAAAA==");

                // Execute
                var block = this.Api.ChangeTssECDSAPubKey(
                    "eCKubWhwnuqqX9vjGl9ltqxCNwE2V9Xi4bO1q404JYNCUlJ0c3h5Cq558pLxtimrS73hPStjtz281+GcfNPTyAE=",
                    "eCKubWhwnuqqX9vjGl9ltqxCNwE2V9Xi4bO1q404JYNCUlJ0c3h5Cq558pLxtimrS73hPStjtz281+GcfNPTyAE=",
                    "YXFeAH2BIKnbHm7qd1zXt6SSReI9ovhYg5jVPGumNOkj3Bw6OfUwSZSCKSMPCw4NRZGDXT6m7Axe5UIwAno4xQE=");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_ChangeAdministrator_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "T2vvfAAAAAAAAAAAAAAAAAA7BzFvzHGKQ375EWILpR91HAz5");

                // Execute
                var block = this.Api.ChangeAdministrator(Address.Parse("z1qqaswvt0e3cc5sm7lygkyza9ra63cr8e6zre09"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetNetwork_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "5PDGOQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHsAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACEV0aGVyZXVtAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACoweDMyM2I1ZDRjMzIzNDVjZWQ3NzM5M2IzNTMwYjFlZWQwZjM0NjQyOWQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.SetNetwork(2, 123, "Ethereum", "0x323b5d4c32345ced77393b3530b1eed0f346429d", "");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_RemoveNetwork_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "PTaqwQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHs=");

                // Execute
                var block = this.Api.RemoveNetwork(2, 123);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetTokenPair_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "1SkkdgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHsAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFOZjGMYxjGMYxgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAcAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKjB4NWZiZGIyMzE1Njc4YWZlY2IzNjdmMDMyZDkzZjY0MmY2NDE4MGFhMwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

                // Execute
                var block = this.Api.SetTokenPair(2, 123, TokenStandard.ZnnZts, "0x5fbdb2315678afecb367f032d93f642f64180aa3", true, true, false, 100, 15, 20, "");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_RemoveTokenPair_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "tJe/OQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHsAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFOZjGMYxjGMYxgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACoweDVmYmRiMjMxNTY3OGFmZWNiMzY3ZjAzMmQ5M2Y2NDJmNjQxODBhYTMAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

                // Execute
                var block = this.Api.RemoveTokenPair(2, 123, TokenStandard.ZnnZts, "0x5fbdb2315678afecb367f032d93f642f64180aa3");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetNetworkMetadata_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "6+pEAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHsAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAheyJBUFIiOiAxNSwgIkxvY2tpbmdQZXJpb2QiOiAxMDB9AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.SetNetworkMetadata(2, 123, "{\"APR\": 15, \"LockingPeriod\": 100}");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetOrchestratorInfo_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "7taYVgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABh");

                // Execute
                var block = this.Api.SetOrchestratorInfo(10, 10, 15, 97);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_NominateGuardians_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "aIrGCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.NominateGuardians(new Address[] { Address.Parse("z1qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqsggv2f") });

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetBridgeMetadata_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "lr4p4wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJ7fQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.SetBridgeMetadata("{}");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_ProposeAdministrator_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "HKMTvQAAAAAAAAAAAAAAAAA7BzFvzHGKQ375EWILpR91HAz5");

                // Execute
                var block = this.Api.ProposeAdministrator(Address.Parse("z1qqaswvt0e3cc5sm7lygkyza9ra63cr8e6zre09"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Emergency_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "+kuhXw==");

                // Execute
                var block = this.Api.Emergency();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetRedeemDelay_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "/SQR7AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACcQ");

                // Execute
                var block = this.Api.SetRedeemDelay(10000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_RevokeUnwrapRequest_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxdrydgexxxxxxxxxxxxxxxmqgr0d", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "+nx/PbOgI9dRaBtq7VB/f3xPqKWdvufuEbPT45wpT8B42be5AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMg=");

                // Execute
                var block = this.Api.RevokeUnwrapRequest(Hash.Parse("b3a023d751681b6aed507f7f7c4fa8a59dbee7ee11b3d3e39c294fc078d9b7b9"), 200);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Liquidity
        public class Liquidity : IClassFixture<EmbeddedApiFixture>
        {
            public LiquidityApi Api { get; }

            public Liquidity(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Liquidity;
            }

            [Fact]
            public void When_LiquidityStake_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 1000000000,
                    "Bx+hFgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAVGA");

                // Execute
                var block = this.Api.LiquidityStake(86400L, 1000000000, TokenStandard.ZnnZts);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_CancelLiquidityStake_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "uO/DfFnkOgCVs2M3kRjJ4oOEQtwrHqWWJLGK6txc1mFWB5Qe");

                // Execute
                var block = this.Api.CancelLiquidityStake(Hash.Parse("59e43a0095b363379118c9e2838442dc2b1ea59624b18aeadc5cd6615607941e"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_UnlockLiquidityStakeEntries_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "YWZDyg==");

                // Execute
                var block = this.Api.UnlockLiquidityStakeEntries(TokenStandard.ZnnZts);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_BurnZnn_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "CWt1pAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJUC+QA");

                // Execute
                var block = this.Api.BurnZnn(10000000000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Fund_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "kS88PwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJUC+QAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC6Q7dAA=");

                // Execute
                var block = this.Api.Fund(10000000000, 50000000000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Donate_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "y3+LKg==");

                // Execute
                var block = this.Api.Donate();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Update_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "IAk+pg==");

                // Execute
                var block = this.Api.Update();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Emergency_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "+kuhXw==");

                // Execute
                var block = this.Api.Emergency();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_NominateGuardians_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "aIrGCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.NominateGuardians(new Address[] { Address.Parse("z1qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqsggv2f") });

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_ChangeAdministrator_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "T2vvfAAAAAAAAAAAAAAAAAA7BzFvzHGKQ375EWILpR91HAz5");

                // Execute
                var block = this.Api.ChangeAdministrator(Address.Parse("z1qqaswvt0e3cc5sm7lygkyza9ra63cr8e6zre09"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_ProposeAdministrator_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "HKMTvQAAAAAAAAAAAAAAAAA7BzFvzHGKQ375EWILpR91HAz5");

                // Execute
                var block = this.Api.ProposeAdministrator(Address.Parse("z1qqaswvt0e3cc5sm7lygkyza9ra63cr8e6zre09"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetAdditionalReward_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "qPv+VgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJUC+QAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC6Q7dAA=");

                // Execute
                var block = this.Api.SetAdditionalReward(10000000000, 50000000000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetIsHalted_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "Rkn+kQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB");

                // Execute
                var block = this.Api.SetIsHalted(true);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_SetTokenTuple_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "8K1o2wAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABp6dHMxOTkybnE0M3huMnVyejN3dHRrbGM4egAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAaenRzMWZyZXM1YzM5YXh3ODA1eHN3dnQ1NWoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABOIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE4gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABOIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE4gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAfQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB9A=");

                // Execute
                var block = this.Api.SetTokenTuple(
                    new string[] { "zts1992nq43xn2urz3wttklc8z", "zts1fres5c39axw805xswvt55j" },
                    new int[] { 5000, 5000 },
                    new int[] { 5000, 5000 },
                    new long[] { 2000, 2000 });

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_CollectReward_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxlyquydytyxxxxxxxxxxxxflaaae", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                    "r0PT8A==");

                // Execute
                var block = this.Api.CollectReward();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Htlc
        public class Htlc : IClassFixture<EmbeddedApiFixture>
        {
            public HtlcApi Api { get; }

            public Htlc(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Htlc;
            }

            [Fact]
            public void When_Create_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxhtlcxxxxxxxxxxxxxxxxxygecvw", "zts1znnxxxxxxxxxxxxx9z4ulx", 10000000000L,
                    "XH5xEAAAAAAAAAAAAAAAAABhJkXCFzgm8ajy065dg9QtXqD7AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGNs2EoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIN5UOmyrjbW9wIbRcguXsPCXRYhBzQJk14k1DjsHWH9b");

                // Execute
                var block = this.Api.Create(
                    TokenStandard.ZnnZts, 10000000000L,
                        Address.Parse("z1qpsjv3wzzuuzdudg7tf6uhvr6sk4ag8me42ua4"), 1668077642L,
                        0, 32, BytesUtils.FromHexString("de543a6cab8db5bdc086d1720b97b0f097458841cd0264d789350e3b07587f5b"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Reclaim_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxhtlcxxxxxxxxxxxxxxxxxygecvw",
                    "zts1znnxxxxxxxxxxxxx9z4ulx", 0, "fgA8jVnkOgCVs2M3kRjJ4oOEQtwrHqWWJLGK6txc1mFWB5Qe");

                // Execute
                var block = this.Api.Reclaim(
                        Hash.Parse("59e43a0095b363379118c9e2838442dc2b1ea59624b18aeadc5cd6615607941e"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Unlock_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxhtlcxxxxxxxxxxxxxxxxxygecvw", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                        "0zeR059uMIjiaH5kL0wPPyn7IZEfpcoaumbcBUP7xu7gFmTtAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGWFsbCB5b3VyIHpubiBiZWxvbmcgdG8gdXMAAAAAAAAA");

                // Execute
                var block = this.Api.Unlock(
                        Hash.Parse("9f6e3088e2687e642f4c0f3f29fb21911fa5ca1aba66dc0543fbc6eee01664ed"),
                        Encoding.UTF8.GetBytes("all your znn belong to us"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_DenyProxyUnlock_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxhtlcxxxxxxxxxxxxxxxxxygecvw", "zts1znnxxxxxxxxxxxxx9z4ulx", 0, "4Xw57Q==");

                // Execute
                var block = this.Api.DenyProxyUnlock();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_AllowProxyUnlock_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxhtlcxxxxxxxxxxxxxxxxxygecvw", "zts1znnxxxxxxxxxxxxx9z4ulx", 0, "V3WPEA==");

                // Execute
                var block = this.Api.AllowProxyUnlock();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Spork
        public class Spork : IClassFixture<EmbeddedApiFixture>
        {
            public SporkApi Api { get; }

            public Spork(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Spork;
            }

            [Fact]
            public void When_CreateSpork_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate("z1qxemdeddedxsp0rkxxxxxxxxxxxxxxxx956u48", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                        "tgLjEQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACVRlc3RTcG9yawAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABRUaGlzIGlzIGEgdGVzdCBzcG9yawAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.CreateSpork(
                        "TestSpork", "This is a test spork");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_ActivateSpork_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                        "z1qxemdeddedxsp0rkxxxxxxxxxxxxxxxx956u48", "zts1znnxxxxxxxxxxxxx9z4ulx", 0,
                        "JcVOllnkOgCVs2M3kRjJ4oOEQtwrHqWWJLGK6txc1mFWB5Qe");

                // Execute
                var block = this.Api.ActivateSpork(
                        Hash.Parse("59e43a0095b363379118c9e2838442dc2b1ea59624b18aeadc5cd6615607941e"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Accelerator
        public partial class Accelerator : IClassFixture<EmbeddedApiFixture>
        {
            public AcceleratorApi Api { get; }

            public Accelerator(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Accelerator;
            }

            [Fact]
            public void When_CreateProject_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(100000000, "d8BEtgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAOAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHRqUogAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEjCc5UAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC1Rlc3RQcm9qZWN0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABdUaGlzIGlzIGEgdGVzdCBwcm9qZWN0LgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASaHR0cDovL2NvbnRvc28uY29tAAAAAAAAAAAAAAAAAAA=");

                // Execute
                var block = this.Api.CreateProject(
                    "TestProject",
                    "This is a test project.",
                    "http://contoso.com",
                    500000000000,
                    5000000000000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_AddPhase_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(0, "x+E93G3mL6f0xYfRv9un3QDR8bphbwGcFLEDZ8t+ePcWW2HwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAdGpSiAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASMJzlQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALVGVzdFByb2plY3QAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAF1RoaXMgaXMgYSB0ZXN0IHByb2plY3QuAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABJodHRwOi8vY29udG9zby5jb20AAAAAAAAAAAAAAAAAAA=="); ;

                // Execute
                var block = this.Api.AddPhase(
                    Hash.Parse("6de62fa7f4c587d1bfdba7dd00d1f1ba616f019c14b10367cb7e78f7165b61f0"),
                    "TestProject",
                    "This is a test project.",
                    "http://contoso.com",
                    500000000000,
                    5000000000000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_UpdatePhase_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(0, "wdfTI23mL6f0xYfRv9un3QDR8bphbwGcFLEDZ8t+ePcWW2HwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAdGpSiAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAASMJzlQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALVGVzdFByb2plY3QAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAF1RoaXMgaXMgYSB0ZXN0IHByb2plY3QuAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABJodHRwOi8vY29udG9zby5jb20AAAAAAAAAAAAAAAAAAA=="); ;

                // Execute
                var block = this.Api.UpdatePhase(
                    Hash.Parse("6de62fa7f4c587d1bfdba7dd00d1f1ba616f019c14b10367cb7e78f7165b61f0"),
                    "TestProject",
                    "This is a test project.",
                    "http://contoso.com",
                    500000000000,
                    5000000000000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Donate_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(50000000000, "y3+LKg==");

                // Execute
                var block = this.Api.Donate(
                    50000000000, TokenStandard.ZnnZts);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_VoteByName_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(0, "XGwQZG3mL6f0xYfRv9un3QDR8bphbwGcFLEDZ8t+ePcWW2HwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHQ29udG9zbwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA="); ;

                // Execute
                var block = this.Api.VoteByName(
                    Hash.Parse("6de62fa7f4c587d1bfdba7dd00d1f1ba616f019c14b10367cb7e78f7165b61f0"), "Contoso", 1);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_VoteByProdAddress_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(0, "kO0AHG3mL6f0xYfRv9un3QDR8bphbwGcFLEDZ8t+ePcWW2HwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE=");

                // Execute
                var block = this.Api.VoteByProdAddress(
                    Hash.Parse("6de62fa7f4c587d1bfdba7dd00d1f1ba616f019c14b10367cb7e78f7165b61f0"), 1);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Pillar
        public partial class Pillar : IClassFixture<EmbeddedApiFixture>
        {
            public PillarApi Api { get; }

            public Pillar(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Pillar;
            }

            [Fact]
            public void When_Register_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    1500000000000,
                    "ZE3pJwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACgAAAAAAAAAAAAAAAAAJKCmPF9nrfyBcVaEn+x1LNaMHIAAAAAAAAAAAAAAAAAkoKY8X2et/IFxVoSf7HUs1owcgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB0NvbnRvc28AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

                // Execute
                var block = this.Api.Register(
                    "Contoso",
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"),
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"),
                    60,
                    60);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_RegisterLegacy_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    1500000000000,
                    "5FiCBwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADgAAAAAAAAAAAAAAAAAJKCmPF9nrfyBcVaEn+x1LNaMHIAAAAAAAAAAAAAAAAAkoKY8X2et/IFxVoSf7HUs1owcgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAdDb250b3NvAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAsT0x1UlVrdzg2bDh1Q2RubTBpRzBKMktwQUxpUVVqaFFFZmx2dXQ1M0hTVT0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABYL2dmYTI2UFA0YXV6c2xScEh2TEZ4cVM0MHF4MG1vM2wrVEx3cWQ4RDB2QlIzOWI0Y2dSLzFGQlJubFFiSVhhQ04zTC8vUE51TVBVRGRienE1cEdFQ1E9PQAAAAAAAAAA");

                // Execute
                var block = this.Api.RegisterLegacy(
                    "Contoso",
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"),
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"),
                    "OLuRUkw86l8uCdnm0iG0J2KpALiQUjhQEflvut53HSU=",
                    "/gfa26PP4auzslRpHvLFxqS40qx0mo3l+TLwqd8D0vBR39b4cgR/1FBRnlQbIXaCN3L//PNuMPUDdbzq5pGECQ==",
                    60,
                    60);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_UpdatePillar_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "3grjSwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACgAAAAAAAAAAAAAAAAAJKCmPF9nrfyBcVaEn+x1LNaMHIAAAAAAAAAAAAAAAAAkoKY8X2et/IFxVoSf7HUs1owcgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB0NvbnRvc28AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

                // Execute
                var block = this.Api.UpdatePillar(
                    "Contoso",
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"),
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"),
                    60,
                    60);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Revoke_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "lWMTBgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAdDb250b3NvAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.Revoke("Contoso");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Delegate_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "fC1dbgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAdDb250b3NvAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.Delegate("Contoso");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Undelegate_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "folSyA==");

                // Execute
                var block = this.Api.Undelegate();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_CollectReward_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "r0PT8A==");

                // Execute
                var block = this.Api.CollectReward();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }


            [Fact]
            public void When_DepositQsr_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1qsrxxxxxxxxxxxxxmrhjll",
                    5000,
                    "1JV39A==");

                // Execute
                var block = this.Api.DepositQsr(5000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_WithdrawQsr_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxpyllarxxxxxxxxxxxxxxxsy3fmg",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "s9ZY/Q==");

                // Execute
                var block = this.Api.WithdrawQsr();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Plasma
        public partial class Plasma : IClassFixture<EmbeddedApiFixture>
        {
            public PlasmaApi Api { get; }

            public Plasma(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Plasma;
            }

            [Fact]
            public void When_Fuse_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxplasmaxxxxxxxxxxxxxxxxsctrp",
                    "zts1qsrxxxxxxxxxxxxxmrhjll",
                    5000,
                    "WslC6AAAAAAAAAAAAAAAAACSgpjxfZ638gXFWhJ/sdSzWjBy");

                // Execute
                var block = this.Api.Fuse(
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"),
                    5000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Cancel_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxplasmaxxxxxxxxxxxxxxxxsctrp",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "+cqdw+RDjuO7nxgX9KH1v0RAB2s4nTCjuz0GTzn6LJlVNtjV");

                // Execute
                var block = this.Api.Cancel(Hash.Parse("e4438ee3bb9f1817f4a1f5bf4440076b389d30a3bb3d064f39fa2c995536d8d5"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Sentinel
        public partial class Sentinel : IClassFixture<EmbeddedApiFixture>
        {
            public SentinelApi Api { get; }

            public Sentinel(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Sentinel;
            }

            [Fact]
            public void When_Register_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    500000000000,
                    "TdI1Fw==");

                // Execute
                var block = this.Api.Register();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Revoke_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "WDY+JA==");

                // Execute
                var block = this.Api.Revoke();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_CollectReward_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "r0PT8A==");

                // Execute
                var block = this.Api.CollectReward();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_DepositQsr_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r",
                    "zts1qsrxxxxxxxxxxxxxmrhjll",
                    5000,
                    "1JV39A==");

                // Execute
                var block = this.Api.DepositQsr(5000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_WithdrawQsr_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxsentynelxxxxxxxxxxxxxwy0r2r",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "s9ZY/Q==");

                // Execute
                var block = this.Api.WithdrawQsr();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Stake
        public partial class Stake : IClassFixture<EmbeddedApiFixture>
        {
            public StakeApi Api { get; }

            public Stake(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Stake;
            }

            [Fact]
            public void When_Stake_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxstakexxxxxxxxxxxxxxxxjv8v62",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    5000,
                    "2AKEWgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJ40A");

                // Execute
                var block = this.Api.Stake(2592000, 5000);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Cancel_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxstakexxxxxxxxxxxxxxxxjv8v62",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "WpL+MuRDjuO7nxgX9KH1v0RAB2s4nTCjuz0GTzn6LJlVNtjV");

                // Execute
                var block = this.Api.Cancel(Hash.Parse("e4438ee3bb9f1817f4a1f5bf4440076b389d30a3bb3d064f39fa2c995536d8d5"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_Undelegate_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxstakexxxxxxxxxxxxxxxxjv8v62",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "r0PT8A==");

                // Execute
                var block = this.Api.CollectReward();

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Swap
        public partial class Swap : IClassFixture<EmbeddedApiFixture>
        {
            public SwapApi Api { get; }

            public Swap(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Swap;
            }

            [Fact]
            public void When_RetrieveAssets_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxswapxxxxxxxxxxxxxxxxxxl4yww",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "R/EsgQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALE9MdVJVa3c4Nmw4dUNkbm0waUcwSjJLcEFMaVFVamhRRWZsdnV0NTNIU1U9AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWC9nZmEyNlBQNGF1enNsUnBIdkxGeHFTNDBxeDBtbzNsK1RMd3FkOEQwdkJSMzliNGNnUi8xRkJSbmxRYklYYUNOM0wvL1BOdU1QVURkYnpxNXBHRUNRPT0AAAAAAAAAAA==");

                // Execute
                var block = this.Api.RetrieveAssets(
                    "OLuRUkw86l8uCdnm0iG0J2KpALiQUjhQEflvut53HSU=",
                    "/gfa26PP4auzslRpHvLFxqS40qx0mo3l+TLwqd8D0vBR39b4cgR/1FBRnlQbIXaCN3L//PNuMPUDdbzq5pGECQ==");

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion

        #region Token
        public partial class Token : IClassFixture<EmbeddedApiFixture>
        {
            public TokenApi Api { get; }

            public Token(EmbeddedApiFixture fixture)
            {
                this.Api = fixture.Api.Token;
            }

            [Fact]
            public void When_IssueToken_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxt0kenxxxxxxxxxxxxxxxxh9amk0",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    100000000,
                    "vEELkQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAWAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH//////////AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAf/////////8AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHQ29udG9zbwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0NUUwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABJodHRwOi8vY29udG9zby5jb20AAAAAAAAAAAAAAAAAAA==");

                // Execute
                var block = this.Api.IssueToken(
                    "Contoso",
                    "CTS",
                    "http://contoso.com",
                    Int64.MaxValue,
                    Int64.MaxValue,
                    8,
                    true,
                    true,
                    false);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_MintToken_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxt0kenxxxxxxxxxxxxxxxxh9amk0",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "zXD5vAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB3xw+baYTJUN1AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAf/////////8AAAAAAAAAAAAAAAAAkoKY8X2et/IFxVoSf7HUs1owcg==");

                // Execute
                var block = this.Api.MintToken(
                    TokenStandard.Parse("zts1q803c0nd5cfj2sm4fv0yga"),
                    9223372036854775807,
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"));

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_BurnToken_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxt0kenxxxxxxxxxxxxxxxxh9amk0",
                    "zts1q803c0nd5cfj2sm4fv0yga",
                    9223372036854775807,
                    "M5WrlA==");

                // Execute
                var block = this.Api.BurnToken(
                    TokenStandard.Parse("zts1q803c0nd5cfj2sm4fv0yga"),
                    9223372036854775807);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }

            [Fact]
            public void When_UpdateToken_ExpectResultToEqual()
            {
                // Setup
                var expectedResult = TestHelper.CreateAccountBlockTemplate(
                    "z1qxemdeddedxt0kenxxxxxxxxxxxxxxxxh9amk0",
                    "zts1znnxxxxxxxxxxxxx9z4ulx",
                    0,
                    "KjzzLAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB3xw+baYTJUN1AAAAAAAAAAAAAAAAAJKCmPF9nrfyBcVaEn+x1LNaMHIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

                // Execute
                var block = this.Api.UpdateToken(
                    TokenStandard.Parse("zts1q803c0nd5cfj2sm4fv0yga"),
                    Address.Parse("z1qzfg9x830k0t0us9c4dpyla36je45vrj8wjlxx"),
                    false,
                    false);

                // Validate
                block.Should().BeEquivalentTo(expectedResult);
                block.ToJson().Should().BeEquivalentTo(expectedResult.ToJson());
            }
        }
        #endregion
    }
}
