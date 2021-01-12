using MessageAssistant.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageAssistant
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void tsmiDecompse_Click(object sender, EventArgs e)
        {
            openMessageDetailFrm();
        }

        private void tsmiCompose_Click(object sender, EventArgs e)
        {
            openMessageDetailFrm();
        }

        private void openMessageDetailFrm()
        {
            MessageDetailFrm frm = new MessageDetailFrm();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
            frm.Dock = DockStyle.Fill;            
        }

        private void tsmiQuit_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            if (e.Item.Text.Length == 0) //隐藏子窗体图标
                //|| e.Item.Text == "最小化(&N)" //隐藏最小化按钮
                //|| e.Item.Text == "还原(&R)" //隐藏还原按钮
                //|| e.Item.Text == "关闭(&C)") //隐藏最关闭按钮
            {
                e.Item.Visible = false;
            }
        }
    }
}
