# LedgerWallet for Zenon .NET SDK

[![nuget](https://img.shields.io/nuget/v/Zenon.Sdk.LedgerWallet)](https://nuget.org/packages/Zenon.Sdk.LedgerWallet) [![build](https://img.shields.io/github/actions/workflow/status/kingGorrin/znn_sdk_csharp/publish.yml?branch=main)](https://github.com/KingGorrin/znn_sdk_csharp/actions/workflows/publish.yml) [![codecov](https://img.shields.io/codecov/c/github/KingGorrin/znn_sdk_csharp?token=FWKGWMWO7U)](https://codecov.io/gh/KingGorrin/znn_sdk_csharp)

The LedgerWallet package offers a cross platform client implementation for the [Zenon Ledger App](https://github.com/HyperCore-One/ledger-app-zenon). Supported platforms are Linux, OSX and Windows.

To use the library please reference the [nuget package](https://www.nuget.org/packages/Zenon.Sdk.LedgerWallet) in your project. Additionally it is required to either ensure that [HIDAPI](https://github.com/libusb/hidapi) is available on the host system or is distributed as part of your application.

## Linux

Note that on Linux you will need to install an udev rule file with your application for unprivileged users to be able to access HID devices with hidapi. 
Refer to the [README](../../udev/) file in the udev directory for an example.

## Installation

Install the Zenon.Sdk.LedgerWallet package from [NuGet](https://www.nuget.org/packages/Zenon.Sdk)

```
dotnet add package Zenon.Sdk.LedgerWallet
```

## Code Example

You can use the `LedgerWallet` class to connect to a Ledger Nano S/X/SP and Stax device:

```csharp
using Zenon;
using Zenon.Wallet;
using Zenon.Wallet.Ledger;

// Use ledger manager
using var walletManager = new LedgerManager();

// Use first wallet available
var walletDefinition =
    (await walletManager.GetWalletDefinitionsAsync()).First();

// Retrieve the wallet
var wallet =
    await walletManager.GetWalletAsync(walletDefinition);

// Get primary wallet account
var walletAccount = await wallet.GetAccountAsync(accountIndex: 0);

// Get address
var address = await walletAccount.GetAddressAsync();
```

## Contributing

Please check [CONTRIBUTING](../../CONTRIBUTING.md) for more details.

## License

The MIT License (MIT). Please check [LICENSE](../../LICENSE) for more information.