using FluentAssertions;
using Moq;
using System;
using Xunit;
using Zenon.Api;
using Zenon.Api.Embedded;
using Zenon.Client;
using Zenon.Model.Primitives;

namespace Zenon.Tests
{
    public partial class ContractTests
    {
        public class EmbeddedApiFixture
        {
            public EmbeddedApiFixture()
            {
                this.Api = new EmbeddedApi(new Lazy<IClient>(() => new Mock<TestClient>().Object));
            }

            public EmbeddedApi Api { get; }
        }

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
