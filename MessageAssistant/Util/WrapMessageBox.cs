using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageAssistant.Util
{
    class WrapMessageBox
    {
        public static void Info(String text, String cap = "提示")
        {
            MessageBox.Show(text, cap, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Warn(String text, String cap = "警告")
        {
            MessageBox.Show(text, cap, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(String text, String cap = "错误")
        {
            MessageBox.Show(text, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
