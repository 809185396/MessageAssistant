using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Model;
using System.Xml;
using MessageAssistant.Constant;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl
{
    class MessageModelServiceImpl : IMessageModelService
    {
        public MessageModel Read(string file)
        {
            throw new NotImplementedException();
        }

        public void Write(MessageModel model, string file)
        {
            throw new NotImplementedException();
        }

        private MessageModel Read(XmlElement e)
        {
            MessageModel model = new MessageModel();
            String strEle = e.OuterXml;
            String str = e.GetAttributeEx(MessageXmlConst.FIELD);
            Assert.NotNullOrEmpty(str, strEle + " 名称不可以为空");
            model.Name = str;

            model.Description = e.GetAttributeEx(MessageXmlConst.DESCRIPTION, "");
            str = e.GetAttributeEx(MessageXmlConst.ENDIAN);
            if (String.Compare(str, MessageXmlConst.ENDIAN_BIG) == 0
                || String.Compare(str, MessageXmlConst.ENDIAN_LITTLE) == 0)
            {
                model.Endian = str;
            }
                    
            model.Cmd = e.GetAttributeInt(MessageXmlConst.CMD);
            model.Fields.AddRange(ReadChildren(e));
            return model;
        }

        private FieldModelBase ReadFieldBase(XmlElement e)
        {
            switch (e.Name)
            {
                case MessageXmlConst.FIELD:
                    return ReadField(e);
                case MessageXmlConst.COMPOSITE_FIELD:
                    return ReadCompositeField(e);
            }
            return null;
        }

        private FieldModelBase ReadField(XmlElement e)
        {
            FieldModel model = new FieldModel();
            ReadFieldBase(e, model);
            model.Length = e.GetAttributeInt(MessageXmlConst.LENGTH);
            model.Type = e.GetAttributeEx(MessageXmlConst.TYPE);
            model.Rate = e.GetAttributeDouble(MessageXmlConst.RATE, 1);
            model.Offset = e.GetAttributeDouble(MessageXmlConst.OFFSET, 0);
            model.Skip = e.GetAttributeEx(MessageXmlConst.SKIP, MessageXmlConst.SKIP_FALSE);
            model.Unit = e.GetAttributeEx(MessageXmlConst.UNIT, MessageXmlConst.UNIT_BYTE);
            return model;
        }

        private FieldModelBase ReadCompositeField(XmlElement e)
        {
            CompositeFieldModel model = new CompositeFieldModel();
            ReadFieldBase(e, model);
            model.Repeat = e.GetAttributeInt(MessageXmlConst.REPEAT);
            model.Children.AddRange(ReadChildren(e));
            return model;
        }

        private FieldModelBase ReadCompositeFieldRef(XmlElement e)
        {
            CompositeFieldRefModel model = new CompositeFieldRefModel();
            ReadFieldBase(e, model);
            model.RepeatRef = e.GetAttributeEx(MessageXmlConst.REPEAT_REF);
            model.Children.AddRange(ReadChildren(e));
            return model;
        }

        private FieldModelBase ReadCompositeFieldFile(XmlElement e)
        {
            throw new NotImplementedException();
            //CompositeFieldFileModel model = new CompositeFieldFileModel();
            //ReadFieldBase(e, model);
            //model.FileName = e.GetAttributeEx(MessageXmlConst.COMPOSITE_FIELD_FILE);
            //model.Children.AddRange(ReadChildren(e));            
            //return model;
        }

        private void ReadFieldBase(XmlElement e, FieldModelBase model)
        {
            String strEle = e.OuterXml;
            String str = e.GetAttributeEx(MessageXmlConst.FIELD);
            Assert.NotNullOrEmpty(str, strEle + " 名称不可以为空");
            model.Name = str;

            model.Description = e.GetAttributeEx(MessageXmlConst.DESCRIPTION, "");
            str = e.GetAttributeEx(MessageXmlConst.ENDIAN);
            if (string.Compare(str, MessageXmlConst.ENDIAN_BIG) == 0
                || String.Compare(str, MessageXmlConst.ENDIAN_LITTLE) == 0)
            {
                model.Endian = str;
            }
            return;
        }

        private List<FieldModelBase> ReadChildren(XmlElement e)
        {
            List<FieldModelBase> childrenList = new List<FieldModelBase>();
            var children = e.ChildNodes;
            for (int i = 0; i < children.Count; ++i)
            {
                var child = children[i] as XmlElement;
                if (child != null)
                {
                    childrenList.Add(ReadFieldBase(child));
                }
            }
            return childrenList;
        }
    }
}
