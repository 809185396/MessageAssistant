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
        byte[] Composite(List<FieldModelBase> fieldList);

        List<FieldModelBase> Decomposite(List<FieldModelBase> fieldList, byte[] message);
    }
}
