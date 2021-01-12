using System;
namespace MessageAssistant.Util
{
    class Cp56Time2aUtil
    {
        public static DateTime ToDt(byte[] bts)
        {
            int year = (bts[6] & 0x7F) + 2000;
            int month = bts[5] & 0x0F;
            int day = bts[4] & 0x1F;
            // int week = (b[4] & 0xE0) / 32;
            int hour = bts[3] & 0x1F;
            int minute = bts[2] & 0x3F;
            int milsec = (bts[1] << 8) + bts[0];
            DateTime dt = new DateTime(year, month, day, hour, minute, milsec / 1000, milsec % 1000);
            return dt;
        }

        public static byte[] ToBytes(DateTime dt)
        {
            byte[] bts = new byte[7];
            bts[6] = (byte)(dt.Year - 2000);
            bts[5] = (byte)dt.Month;
            bts[4] = (byte)dt.Day;
            bts[3] = (byte)dt.Hour;
            bts[2] = (byte)dt.Minute;
            int milSec = dt.Millisecond;
            bts[1] = (byte)(milSec >> 8);
            bts[0] = (byte)milSec;
            return bts;
        }
    }
}
