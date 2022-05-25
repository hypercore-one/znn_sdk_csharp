# Zenon Sdk for .NET

[![nuget](https://img.shields.io/nuget/vpre/Zenon.Sdk)](https://nuget.org/packages/Zenon.Sdk) [![build](https://img.shields.io/github/workflow/status/kingGorrin/znn_sdk_csharp/Zenon.Sdk.NET)](https://github.com/KingGorrin/znn_sdk_csharp/actions/workflows/publish.yml) [![codecov](https://img.shields.io/codecov/c/github/KingGorrin/znn_sdk_csharp?token=FWKGWMWO7U)](https://codecov.io/gh/KingGorrin/znn_sdk_csharp)

Reference implementation for the Zenon SDK for .NET. Compatible with the Zenon Alphanet - Network of Momentum Phase 0. 
It provides a simple integration with any .NET based projects.

## Installation

Install the Zenon.Sdk package from [NuGet](https://www.nuget.org/packages/Zenon.Sdk)

```
dotnet add package Zenon.Sdk --prerelease
```

## Usage

### Connect node

```csharp
using Zenon;
await Znn.Instance.Client.Value.StartAsync(new Uri("ws://nodes.zenon.place:35998"));
...
await Znn.Instance.Client.Value.StopAsync();
```

### Generate wallet

```csharp
using Zenon;
Znn.Instance.DefaultKeyStore = Znn.Instance.KeyStoreManager.CreateNew("secret", "name");
```

### Generate wallet from mnemonic

```csharp
using Zenon;
Znn.Instance.DefaultKeyStore = Znn.Instance.KeyStoreManager.CreateFromMnemonic("mnemonic", "secret", "name");
```

### Sending a transaction

```csharp
using Zenon;

var passphrase = "secret";
var keyStorePath = Path.Combine(Constants.ZnnDefaultWalletDirectory, "name");

Znn.Instance.DefaultKeyStore = Znn.Instance.KeyStoreManager.ReadKeyStore(passphrase, keyStorePath);
Znn.Instance.DefaultKeyPair = Znn.Instance.DefaultKeyStore.GetKeyPair();

await Znn.Instance.Client.Value.StartAsync(new Uri("ws://nodes.zenon.place:35998"));

var tx = Znn.Instance.Embedded.Pillar.CollectReward();

await Znn.Instance.Send(tx);
```

### Receive a transaction

```csharp
using Zenon;
using Zenon.Model.NoM;

var passphrase = "secret";
var keyStorePath = Path.Combine(Constants.ZnnDefaultWalletDirectory, "name");

Znn.Instance.DefaultKeyStore = Znn.Instance.KeyStoreManager.ReadKeyStore(passphrase, keyStorePath);
Znn.Instance.DefaultKeyPair = Znn.Instance.DefaultKeyStore.GetKeyPair();

await Znn.Instance.Client.Value.StartAsync(new Uri("ws://nodes.zenon.place:35998"));

var address = Address.Parse("address");

await Znn.Instance.Subscribe.ToUnreceivedAccountBlocksByAddress(address, result =>
{
    var hash = result[0].Value<string>("hash");
});
```

## Contributing

Please check [CONTRIBUTING](./CONTRIBUTING.md) for more details.

## License

The MIT License (MIT). Please check [LICENSE](./LICENSE) for more information.