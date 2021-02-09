using System;
using System.Collections.Generic;
using System.IO;
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
            dataGridView1.Columns[0].DataPropertyName = "Name";
            dataGridView1.Columns[1].DataPropertyName = "DataType";
            dataGridView1.Columns[2].DataPropertyName = "Description";
            dataGridView1.Columns[3].DataPropertyName = "OriginalContent";
            dataGridView1.Columns[4].DataPropertyName = "DefaultValue";
            dataGridView1.Columns[5].DataPropertyName = "Value";
        }

        private void MessageDetailFrm_Load(object sender, EventArgs e)
        {
            RefreshProtocol();
        }

        private void RefreshProtocol()
        {
            String str = Application.StartupPath;
            str = str.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            str = str + Path.DirectorySeparatorChar + "message-cfg";
            var files = Directory.GetFiles(str, "*.xml");
            xmlFiles.Clear();
            xmlFiles.AddRange(files);
            foreach (var file in files)
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

            var obj = cmbProtocol.SelectedItem;
            if(obj == null)
            {
                WrapMessageBox.Info("请选择消息配置文件");
                return;
            }
            String strFile = (obj as ComboBoxItem).Value;

            try
            {
                IMessageModelService modelService = new MessageModelServiceImpl();
                MessageModel model = modelService.Read(strFile);
                String strMsg2 = strMsg.Replace(" ", "");
                byte[] btMsg = MessageAssistant.Util.StringConverter.hexStrToToByte(strMsg2);
                MessageService service = new MessageService();
                model = service.Decomposite(model, btMsg);
                BindMessageModel(model);
            }
            catch (Exception ex)
            {
                WrapMessageBox.Error(ex.Message);
            }
        }

        private void btnCompose_Click(object sender, EventArgs e)
        {
            
        }

        private void BindMessageModel(MessageModel model)
        {
            dataGridView1.Rows.Clear();
            model.Fields.ForEach(r => AddField(model, r));
            
        }

        private void AddField(MessageModel model,FieldModelBase field)
        {
            if(field is BitFieldModel)
            {
                AddField(model, field as BitFieldModel);
            } else if( field is BitChildModel)
            {
                AddField(model, field as BitChildModel);
            } else if(field is FieldModel)
            {
                AddField(model, field as FieldModel);
            } else if(field is IfFieldModel)
            {
                AddField(model, field as IfFieldModel);
            } else if(field is RepeatFieldModel)
            {
                AddField(model, field as RepeatFieldModel);
            } else if (field is FileFieldModel)
            {
                AddField(model, field as FileFieldModel);
            }
        }

        private void AddField(MessageModel model, BitFieldModel field)
        {
            field.Children.ForEach(r => AddField(model, r));
        }

        private void AddField(MessageModel model, BitChildModel field)
        {
            AddField(model, (FieldModel)field);
        }

        private void AddField(MessageModel model, FieldModel field)
        {
            int index = dataGridView1.Rows.Add();
            var row = dataGridView1.Rows[index];
            row.Tag = field;
            row.Cells[0].Value = field.Name;
            row.Cells[1].Value = field.DataType;
            row.Cells[2].Value = field.Description;
            row.Cells[3].Value = field.OriginalContent;
            row.Cells[4].Value = field.DefaultValue;
            row.Cells[5].Value = field.Value;
        }

        private void AddField(MessageModel model, IfFieldModel field)
        {
            if (field.Value)
            {
                field.Children.ForEach(r => AddField(model, r));
            }
        }

        private void AddField(MessageModel model, RepeatFieldModel field)
        {
            field.Children.ForEach(r =>
            {
                r.ForEach(r1 => AddField(model, r1));
            });
        }

        private void AddField(MessageModel model, FileFieldModel field)
        {
            field.Children.ForEach(r => AddField(model, r));
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
            return this.Text;
        }

    }
}
