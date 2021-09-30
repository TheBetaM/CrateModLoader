namespace Twinsanity
{
    public static class BitConv
    {
        public static void ToInt16(byte[] arr, int off, short value)
        {
            arr[off] = (byte)(value & 0xFF);
            arr[off + 1] = (byte)((value >> 8) & 0xFF);
        }

        public static short FlipBytes(short val) => (short)(((val & 0xFF) << 8) | ((val & 0xFF00) >> 8));
        public static ushort FlipBytes(ushort val) => (ushort)(((val & 0xFF) << 8) | ((val & 0xFF00) >> 8));
        public static int FlipBytes(int val) => (int)(((val & 0x000000FFU) << 24) | ((val & 0x0000FF00U) << 8) | ((val & 0x00FF0000U) >> 8) | ((val & 0xFF000000U) >> 24));
        public static uint FlipBytes(uint val) => ((val & 0x000000FFU) << 24) | ((val & 0x0000FF00U) << 8) | ((val & 0x00FF0000U) >> 8) | ((val & 0xFF000000U) >> 24);    }
}
