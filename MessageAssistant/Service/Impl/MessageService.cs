using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Constant;
using MessageAssistant.Model;
using MessageAssistant.Util;
using MessageAssistant.Service.Impl.FieldModelService;

namespace MessageAssistant.Service.Impl
{
    class MessageService : IMessageService
    {
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
                if (r is FieldModel)
                {
                    FieldModelServiceBase.Decomposite(model, (FieldModel)r, buf);
                }
            });
            return model; 
        }
    }
}
