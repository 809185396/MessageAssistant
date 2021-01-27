using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            Test3();
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

            int val = BitConverter.ToInt32(bts1, 2);
            Console.WriteLine(val);
        }

        static void Test2()
        {
            String[] paths = new String[] { "123", "456" };
            paths = paths.Skip(1).ToArray();
            paths = paths.Skip(1).ToArray();
            paths = paths.Skip(1).ToArray();
        }

        static void Test3()
        {
            string line = "ADDR=1234;NAME=ZHANG;PHONE=6789";
            Regex reg1 = new Regex("NAMEk=(.+);");
            Match m2 = reg1.Match(line);
            Console.WriteLine(m2.Value);
            String expr = "${ab_}>${bc}+1";
            // String expr = "1ifds93+";
            Regex reg = new Regex(@"\$\{(\w+)\}");
            var m1 = reg.Match(expr);
            Console.WriteLine(m1.Value);
            var matches = reg.Matches(expr);
            for (int i = 0; i < matches.Count; ++i)
            {
                var match = matches[i];
                Console.WriteLine(match.Groups[0].Value + " -- " + match.Groups[1].Value);
                match.Result("yes");
            }
            Console.WriteLine(expr);
        }
    }
}
