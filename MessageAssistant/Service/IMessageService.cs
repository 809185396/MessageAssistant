using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Model;

namespace MessageAssistant.Service
{
    interface IMessageService
    {
        byte[] Composite(MessageModel model);
        MessageModel Decomposite(MessageModel model, byte[] message);
        List<FieldModelBase> CollectValueField(MessageModel m);
    }
}
