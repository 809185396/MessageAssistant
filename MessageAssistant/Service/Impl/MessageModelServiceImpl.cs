using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Model;
using System.Xml;
using MessageAssistant.Constant;
using MessageAssistant.Util;
using MessageAssistant.Service.Impl.FieldModelService;

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
            String str = e.GetAttributeEx(MessageXmlConst.NAME);
            Assert.NotNullOrEmpty(str, strEle + " 名称不可以为空");
            model.Name = str;

            model.Description = e.GetAttributeEx(MessageXmlConst.DESCRIPTION, "");
            str = e.GetAttributeEx(MessageXmlConst.ENDIAN);
            if (String.Compare(str, MessageXmlConst.ENDIAN_LITTLE) == 0)
            {
                model.Endian = str;
            }
            else
            {
                model.Endian = MessageXmlConst.ENDIAN_BIG;
            }
                    
            model.Cmd = e.GetAttributeInt(MessageXmlConst.CMD);
            model.Fields.AddRange(FieldModelServiceBase.ReadChildren(e));            
            return model;
        }

        public String GetFieldValue(MessageModel model, String name)
        {
            String[] path = name.Split('.');

            return String.Empty;
        }       
    }
}
