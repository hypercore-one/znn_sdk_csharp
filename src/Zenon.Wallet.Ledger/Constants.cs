namespace Zenon.Wallet.Ledger
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
    }
}
