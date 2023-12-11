namespace Zenon.Wallet.Ledger
{
    public static class StatusCode
    {
        public const int Success = 0x9000;
        public const int Deny = 0x6985;
        public const int WrongP1P2 = 0x6A86;
        public const int WrongDataLength = 0x6A87;
        public const int InactiveDevice = 0x6b0c;
        public const int NotAllowed = 0x6c66;
        public const int InstructionCodeNotSupported = 0x6D00;
        public const int InstructionClassNotSupported = 0x6E00;
        public const int AppIsNotOpen = 0x6511;
        public const int WrongResponseLength = 0xB000;
        public const int DisplayBIP32PathFail = 0xB001;
        public const int DisplayAddressFail = 0xB002;
        public const int DisplayAmountFail = 0xB003;
        public const int WrongTxLength = 0xB004;
        public const int TxParsingFail = 0xB005;
        public const int TxHashFail = 0xB006;
        public const int BadState = 0xB007;
        public const int SignatureFail = 0xB008;
    }
}
