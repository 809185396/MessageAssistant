using System;
using System.Xml;
using MessageAssistant.Constant;
using MessageAssistant.Exceptions;
using MessageAssistant.Model;
using MessageAssistant.Service.Impl.FieldModelService;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl
{
    class MessageModelServiceImpl : IMessageModelService
    {
        public MessageModel Read(string file)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(file);
                var xn = doc.SelectSingleNode(MessageXmlConst.MESSAGE);
                var strDir = System.IO.Path.GetDirectoryName(file);
                if (xn != null)
                {
                    return Read(strDir, (XmlElement)xn);
                }
                throw new BizException($"{file} 没有消息根节点");
            }
            catch(BizException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new BizException($"读取{file} 文件失败", ex);
            }
        }

        public void Write(MessageModel model, string file)
        {
            throw new NotImplementedException();
        }

        private MessageModel Read(String strDir, XmlElement e)
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
            model.Fields.AddRange(FieldModelServiceBase.ReadChildren(strDir, e));            
            return model;
        }       
    }
}
