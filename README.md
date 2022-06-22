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

var nodeUrl = new Uri("ws://nodes.zenon.place:35998");

await Znn.Instance.Client.Value.StartAsync(nodeUrl);
...
await Znn.Instance.Client.Value.StopAsync();
```

### Generate wallet

```csharp
using Zenon;

Znn.Instance.DefaultKeyStore = 
    Znn.Instance.KeyStoreManager.CreateNew("secret", "name");
```

### Generate wallet from mnemonic

```csharp
using Zenon;

Znn.Instance.DefaultKeyStore = 
    Znn.Instance.KeyStoreManager.CreateFromMnemonic("mnemonic", "secret", "name");
```

### Sending a transaction

```csharp
using Zenon;

var nodeUrl = new Uri("ws://nodes.zenon.place:35998");
var passphrase = "secret";
var keyStorePath = Path.Combine(Constants.ZnnDefaultWalletDirectory, "name");

Znn.Instance.DefaultKeyStore = 
    Znn.Instance.KeyStoreManager.ReadKeyStore(passphrase, keyStorePath);
Znn.Instance.DefaultKeyPair = 
    Znn.Instance.DefaultKeyStore.GetKeyPair(0); // Use primary address

await Znn.Instance.Client.Value.StartAsync(nodeUrl);

var tx = Znn.Instance.Embedded.Pillar.CollectReward();

await Znn.Instance.Send(tx);
```

### Receive a transaction

```csharp
using Zenon;
using Zenon.Model.NoM;

var nodeUrl = new Uri("ws://nodes.zenon.place:35998");
var passphrase = "secret";
var keyStorePath = Path.Combine(Constants.ZnnDefaultWalletDirectory, "name");

Znn.Instance.DefaultKeyStore = 
    Znn.Instance.KeyStoreManager.ReadKeyStore(passphrase, keyStorePath);
Znn.Instance.DefaultKeyPair = 
    Znn.Instance.DefaultKeyStore.GetKeyPair(0); // Use primary address

await Znn.Instance.Client.Value.StartAsync(nodeUrl);

var result = await Znn.Instance.Ledger
    .GetUnreceivedBlocksByAddress(Znn.Instance.DefaultKeyPair.Address);

if (result.Count != 0)
{
    foreach (var item in result.List)
    {
        var tx = AccountBlockTemplate.Receive(item.Hash);

        await Znn.Instance.Send(tx, true);
    }
}
```

## Contributing

Please check [CONTRIBUTING](./CONTRIBUTING.md) for more details.

## License

The MIT License (MIT). Please check [LICENSE](./LICENSE) for more information.