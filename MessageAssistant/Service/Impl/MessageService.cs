using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Constant;
using MessageAssistant.Model;
using MessageAssistant.Util;
using MessageAssistant.Service.Impl.FieldModelService;
using System.Xml;

namespace MessageAssistant.Service.Impl
{
    class MessageService : IMessageService
    {
        public List<FieldModelBase> CollectValueField(MessageModel model)
        {
            List<FieldModelBase> fields = new List<FieldModelBase>();
            model.Fields.ForEach(r =>            {
                FieldModelServiceBase.CollectValueField(fields, r);
            });
            return fields;
        }

        public byte[] Composite(MessageModel model)
        {
            throw new NotImplementedException();
        }

        public byte[] Composite(List<FieldModelBase> fieldList)
        {
            throw new NotImplementedException();
        }

        public MessageModel Decomposite(MessageModel model, byte[] message)
        {
            Assert.NotNullOrEmpty(message, "message can not empty");
            Assert.NotNull(model, "fiedList can not empty");
            ByteBuffer buf = ByteBuffer.Allocate(message);
            model.Fields.ForEach(r =>
            {
                FieldModelServiceBase.Decomposite(model, r, buf);
            });
            return model; 
        }

        private void LoadRefFile(String file)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(file);
                var xn = doc.SelectSingleNode(MessageXmlConst.FILE_BLOCK);
                var strDir = System.IO.Path.GetDirectoryName(file);
                if (xn != null)
                {
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


    }
}
