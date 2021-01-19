using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Util
{
    static class BitUtil
    {
        /// <summary>
        /// 把src字节数组中指定位置开始的指定个位复制到dst中去，右边对齐
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="srcStart"></param>
        /// <param name="len"></param>
        static void GetBits(byte[] src, byte[] dst, int srcStart, int len)
        {
            int dstStart = dst.Length * 8 - len;
            TransferBits(src, dst, srcStart, dstStart, len);
        }

        /// <summary>
        /// 把src字节数组中右边指定个位复制到dst指定开始的位置去，右边对齐
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <param name="srcStart"></param>
        /// <param name="len"></param>
        static void SetBits(byte[] src, byte[] dst, int dstStart, int len)
        {
            int srcStart = src.Length * 8 - len;
            TransferBits(src, dst, srcStart, dstStart, len);
        }

        static void ClearBits(byte[] dst)
        {
            for (int i = 0; i < dst.Length; ++i)
            {
                dst[i] = 0;
            }
        }

        static void TransferBits(byte[] src, byte[] dst, int srcStart, int dstStart, int len)
        {
            int rLoc = srcStart + len - 1;
            int wLoc = dstStart + len - 1;
            for (; rLoc >= srcStart;)
            {
                int rIndex = rLoc / 8;
                int rInnerLoc = rLoc % 8;
                int wIndex = wLoc / 8;
                int wInnerLoc = wLoc % 8;

                byte val = src[rIndex];
                val >>= (7 - rInnerLoc);
                val &= 0x1;
                val <<= (7 - wInnerLoc);
                byte val2 = (byte)~val;
                dst[wIndex] &= val2;
                dst[wIndex] |= val;

                --rLoc;
                --wLoc;
            }
        }
    }
}
