using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageAssistant.Util;

namespace MessageAssistant
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Test();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFrm());
        }

        static void Test()
        {
            String str = "f34578";
            // 连个字符作为一个0xff
            byte[] bts1 = StringConverter.hexStrToToByte(str);            
            Console.WriteLine(bts1.ToArray());

            // 单个字符转
            byte[] bts2 = System.Text.Encoding.ASCII.GetBytes(str);
            Console.WriteLine(bts2.ToArray());

            Console.WriteLine(BitConverter.IsLittleEndian);
            ByteBuffer buf = ByteBuffer.Allocate(bts1);
            var s1 = buf.GetShort(BitConverter.IsLittleEndian);
            Console.WriteLine(s1);
            var s2 = buf.GetShort(!BitConverter.IsLittleEndian);
            Console.WriteLine(s2);
        }
    }
}
