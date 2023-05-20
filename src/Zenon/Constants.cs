using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Zenon
{
    public static class Constants
    {
        // Global constants
        public const string ZnnSdkVersion = "0.0.5";
        public const string ZnnRootDirectory = "znn";

        // https://github.com/zenon-network/go-zenon/blob/b2e6a98fa154d763571bb7af6b1c685d0d82497d/zenon/zenon.go#L41
        public const int NetId = 1; // Alphanet network identifier
        public const int ChainId = 1; // Alphanet chain identifier

        public static string ZnnDefaultDirectory = ZnnPaths.Default.Main;
        public static string ZnnDefaultWalletDirectory = ZnnPaths.Default.Wallet;
        public static string ZnnDefaultCacheDirectory = ZnnPaths.Default.Cache;

        // Client constants
        public const int DefaultHttpPort = 35997;
        public const int DefaultWsPort = 35998;
        public const int NumRetries = 10;
        public const int RpcMaxPageSize = 1024;
        public const int MemoryPoolPageSize = 50;

        // NoM constants
        public const int ZnnDecimals = 8;
        public const int QsrDecimals = 8;
        public const long OneZnn = 1 * 100000000;
        public const long OneQsr = 1 * 100000000;
        public static readonly TimeSpan IntervalBetweenMomentums = TimeSpan.FromSeconds(10);

        // Embedded constants
        public const long GenesisTimestamp = 1637755200;

        // Plasma
        public static readonly BigInteger FuseMinQsrAmount = new BigInteger(10 * OneQsr);
        public static readonly BigInteger MinPlasmaAmount = new BigInteger(21000);

        // Pillar
        public static readonly BigInteger PillarRegisterZnnAmount = new BigInteger(15000 * OneZnn);
        public static readonly BigInteger PillarRegisterQsrAmount = new BigInteger(150000 * OneQsr);
        public const long PillarNameMaxLength = 40;
        public static readonly Regex PillarNameRegExp = new Regex("^([a-zA-Z0-9]+[-._]?)*[a-zA-Z0-9]$");

        // Sentinel
        public static readonly BigInteger SentinelRegisterZnnAmount = new BigInteger(5000 * OneZnn);
        public static readonly BigInteger SentinelRegisterQsrAmount = new BigInteger(50000 * OneQsr);

        // Staking
        public static readonly BigInteger StakeMinZnnAmount = new BigInteger(1 * OneZnn);
        public const long StakeTimeUnitSec = 30 * 24 * 60 * 60;
        public const long StakeTimeMaxSec = 12 * StakeTimeUnitSec;
        public const string StakeUnitDurationName = "month";

        // Token
        public static readonly BigInteger TokenZtsIssueFeeInZnn = new BigInteger(1 * OneZnn);
        public const long TokenNameMaxLength = 40;
        public const long TokenSymbolMaxLength = 10;
        public static readonly string[] TokenSymbolExceptions = new string[] { "ZNN", "QSR" };

        public static readonly Regex TokenNameRegExp = new Regex("^([a-zA-Z0-9]+[-._]?)*[a-zA-Z0-9]$");
        public static readonly Regex TokenSymbolRegExp = new Regex("^[A-Z0-9]+$");
        public static readonly Regex TokenDomainRegExp = new Regex("^([A-Za-z0-9][A-Za-z0-9-]{0,61}[A-Za-z0-9].)+[A-Za-z]{2,}$");

        // Accelerator
        public static readonly BigInteger ProjectCreationFeeInZnn = new BigInteger(1 * OneZnn);
        public const int ProjectDescriptionMaxLength = 240;
        public const int ProjectNameMaxLength = 30;
        public const int ProjectVotingStatus = 0;
        public const int ProjectActiveStatus = 1;
        public const int ProjectPaidStatus = 2;
        public const int ProjectClosedStatus = 3;
        public static readonly Regex ProjectUrlRegExp = new Regex("^[a-zA-Z0-9]{2,60}.[a-zA-Z]{1,6}([a-zA-Z0-9()@:%_\\+.~#?&/=-]{0,100})$");

        // Spork
        public const int SporkNameMinLength = 5;
        public const int SporkNameMaxLength = 40;
        public const int SporkDescriptionMaxLength = 400;

        // Swap
        public const int SwapAssetDecayTimestampStart = 1645531200;
        public const int SwapAssetDecayEpochsOffset = 30 * 3;
        public const int SwapAssetDecayTickEpochs = 30;
        public const int SwapAssetDecayTickValuePercentage = 10;

        // Htlc
        public const int HtlcPreimageMinLength = 1;
        public const int HtlcPreimageMaxLength = 255;
        public const int HtlcPreimageDefaultLength = 32;
        public const int HtlcTimelockUnitSec = 60 * 60; // 1 hour
        public const int HtlcTimelockMinSec = HtlcTimelockUnitSec * 1; // 1 hour
        public const int HtlcTimelockMaxSec = HtlcTimelockUnitSec * 24 * 30 * 12; // ~1 year
    }
}