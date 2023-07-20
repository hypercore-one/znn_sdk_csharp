namespace Zenon.LedgerWallet
{
    internal static class Constants
    {
        public const byte CLA = 0xE0;

        public const byte P1_START = 0x00;
        public const byte P2_MORE = 0x80;
        public const byte P2_LAST = 0x00;

        public const int DEFAULT_CHANNEL = 0x0101;
        public const int LEDGER_HID_PACKET_SIZE = 64;
        public const int LEDGER_MAX_DATA_SIZE = 0xFF;
        public const int TAG_APDU = 0x05;

        public const byte ZNN_INS_GET_VERSION = 0x03;
        public const byte ZNN_INS_GET_APP_NAME = 0x04;
        public const byte ZNN_INS_GET_PUBLIC_KEY = 0x05;
        public const byte ZNN_INS_SIGN_TX = 0x06;

        public const int SuccessStatusCode = 0x9000;
        public const int DenyStatusCode = 0x6985;
        public const int WrongP1P2StatusCode = 0x6A86;
        public const int WrongDataLengthStatusCode = 0x6A87;
        public const int InstructionCodeNotSupportedStatusCode = 0x6D00;
        public const int InstructionClassNotSupportedStatusCode = 0x6E00;
        public const int WrongResponseLengthStatusCode = 0xB000;
        public const int DisplayBIP32PathFailStatusCode = 0xB001;
        public const int DisplayAddressFailStatusCode = 0xB002;
        public const int DisplayAmountFailStatusCode = 0xB003;
        public const int WrongTxLengthStatusCode = 0xB004;
        public const int TxParsingFailStatusCode = 0xB005;
        public const int TxHashFailStatusCode = 0xB006;
        public const int BadStateStatusCode = 0xB007;
        public const int SignatureFailStatusCode = 0xB008;
    }
}
