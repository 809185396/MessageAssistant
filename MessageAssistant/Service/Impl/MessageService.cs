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
        public byte[] Composite(List<FieldModelBase> fieldList)
        {
            throw new NotImplementedException();
        }

        public List<FieldModelBase> Decomposite(List<FieldModelBase> fieldList, byte[] message)
        {
            Assert.NotNullOrEmpty(message, "message can not empty");
            Assert.NotNullOrEmpty(fieldList, "fiedList can not empty");
            ByteBuffer buf = ByteBuffer.Allocate(message);
            fieldList.ForEach(r =>
            {
                if (r is FieldModel)
                {
                    FieldModelServiceBase.Decomposite((FieldModel)r, buf);
                }
            });
            return fieldList;
        }
    }
}
