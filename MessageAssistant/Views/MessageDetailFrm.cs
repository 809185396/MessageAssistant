using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageAssistant.Model;
using MessageAssistant.Service;
using MessageAssistant.Service.Impl;
using MessageAssistant.Util;

namespace MessageAssistant.Views
{
    public partial class MessageDetailFrm : Form
    {
        List<String> xmlFiles = new List<string>();
        public MessageDetailFrm()
        {
            InitializeComponent();
        }

        private void MessageDetailFrm_Load(object sender, EventArgs e)
        {
            String str = Application.StartupPath;
            str = str.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            str = str + Path.DirectorySeparatorChar + "message-cfg";
            var files = Directory.GetFiles(str, "*.xml");
            xmlFiles.Clear();
            xmlFiles.AddRange(files);
            foreach(var file in files)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi = new ComboBoxItem();
                cbi.Text = Path.GetFileName(file);
                cbi.Value = file;
                cmbProtocol.Items.Add(cbi);
            }
        }

        private void btnDecompose_Click(object sender, EventArgs e)
        {
            String strMsg = this.tbMessage.Text.Trim();
            if (String.IsNullOrWhiteSpace(strMsg))
            {
                WrapMessageBox.Info("请输入报文");
                return;
            }

            var obj = cmbProtocol.SelectedValue;
            if(obj == null)
            {
                WrapMessageBox.Info("请选择消息配置文件");
                return;
            }
            String strFile = obj.ToString();

            try
            {
                IMessageModelService modelService = new MessageModelServiceImpl();
                MessageModel model = modelService.Read(strFile);
                String strMsg2 = strMsg.Replace(" ", "");
                byte[] btMsg = MessageAssistant.Util.StringConverter.hexStrToToByte(strMsg2);
                MessageService service = new MessageService();
                model = service.Decomposite(model, btMsg);
            }
            catch (Exception ex)
            {
                WrapMessageBox.Error(ex.Message);
            }
        }

        private void btnCompose_Click(object sender, EventArgs e)
        {

        }
    }

    class ComboBoxItem
    {
        string _text;
        string _value;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public override String ToString()
        {
            return this.Value;
        }

    }
}
