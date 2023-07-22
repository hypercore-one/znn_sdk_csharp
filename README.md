# Zenon Sdk for .NET

[![nuget](https://img.shields.io/nuget/v/Zenon.Sdk)](https://nuget.org/packages/Zenon.Sdk) [![build](https://img.shields.io/github/actions/workflow/status/kingGorrin/znn_sdk_csharp/publish.yml?branch=main)](https://github.com/KingGorrin/znn_sdk_csharp/actions/workflows/publish.yml) [![codecov](https://img.shields.io/codecov/c/github/KingGorrin/znn_sdk_csharp?token=FWKGWMWO7U)](https://codecov.io/gh/KingGorrin/znn_sdk_csharp)

Reference implementation for the Zenon SDK for .NET. Compatible with the Zenon Alphanet - Network of Momentum Phase 1. 
It provides a simple integration with any .NET based projects.

## Installation

Install the Zenon.Sdk package from [NuGet](https://www.nuget.org/packages/Zenon.Sdk)

```
dotnet add package Zenon.Sdk
```

## Usage

### Connect node

```csharp
using Zenon;

var nodeUrl = new Uri("wss://my.hc1node.com:35998");

// Use default mainnet instance
var mainnet = Znn.Mainnet;

// Connect to node
await mainnet.Client.Value.StartAsync(nodeUrl);
...
// Disconnect from node
await mainnet.Client.Value.StopAsync();
```

### Generate wallet

```csharp
using Zenon;
using Zenon.Wallet;

var wallet = "name";
var passphrase = "secret";

// Use the key store manager
var walletManager = new KeyStoreManager();

// Create wallet
var walletDefinition = walletManager
    .CreateNew(passphrase, wallet);
```

### Generate wallet from mnemonic

```csharp
using Zenon;
using Zenon.Wallet;

var wallet = "name";
var passphrase = "secret";
var mnemonic =
        "route become dream access impulse price inform obtain engage ski believe awful absent pig thing vibrant possible exotic flee pepper marble rural fire fancy";

// Use the key store manager
var walletManager = new KeyStoreManager();

// Create wallet
var walletDefinition = walletManager
    .CreateFromMnemonic(mnemonic, passphrase, wallet);
```

### Sending a transaction

```csharp
using Zenon;
using Zenon.Wallet;

// Use the key store manager
var walletManager = new KeyStoreManager();

// Use the first wallet available
var walletDefinition =
    (await walletManager.GetWalletDefinitionsAsync()).First();

// Options for retrieving the wallet
var options = new KeyStoreOptions()
{
    DecryptionPassword = "secret"
};

// Retrieve the wallet
var wallet =
    await walletManager.GetWalletAsync(walletDefinition, options);

// Use default mainnet instance
var mainnet = Znn.Mainnet;

// Attach primary wallet account
mainnet.DefaultWalletAccount =
    await wallet.GetAccountAsync();

// Connect to node
await mainnet.Client.Value.StartAsync(new Uri("wss://my.hc1node.com:35998"));

// Send tx
await mainnet.SendAsync(mainnet.Embedded.Pillar.CollectReward());

// Disconnect from node
await mainnet.Client.Value.StopAsync();
```

### Receive a transaction

```csharp
using Zenon;
using Zenon.Wallet;
using Zenon.Model.NoM;

// Use the key store manager
var walletManager = new KeyStoreManager();

// Use the first wallet available
var walletDefinition =
    (await walletManager.GetWalletDefinitionsAsync()).First();

// Options for retrieving the wallet
var options = new KeyStoreOptions()
{
    DecryptionPassword = "secret"
};

// Retrieve the wallet
var wallet =
    await walletManager.GetWalletAsync(walletDefinition, options);

// Use default mainnet instance
var mainnet = Znn.Mainnet;

// Attach primary wallet account
mainnet.DefaultWalletAccount =
    await wallet.GetAccountAsync();

// Get account address
var address = await mainnet.DefaultWalletAccount.GetAddressAsync();

// Connect to node
await mainnet.Client.Value.StartAsync(new Uri("wss://my.hc1node.com:35998"));

// Get all unreceived tx's
var result = await mainnet.Ledger
    .GetUnreceivedBlocksByAddress(address);

if (result.Count != 0)
{
    foreach (var item in result.List)
    {
        // Send tx
        await mainnet.SendAsync(AccountBlockTemplate.Receive(item.Hash));
    }
}

// Disconnect from node
await mainnet.Client.Value.StopAsync();
```

## Contributing

Please check [CONTRIBUTING](./CONTRIBUTING.md) for more details.

## License

The MIT License (MIT). Please check [LICENSE](./LICENSE) for more information.