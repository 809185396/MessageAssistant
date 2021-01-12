using MessageAssistant.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Service
{
    interface IMessageModelService
    {
        MessageModel Read(String file);
        void Write(MessageModel model, String file);
    }
}
