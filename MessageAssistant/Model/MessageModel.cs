using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    class MessageModel
    {
        public String Name { get; set; } = "";
        public int Cmd { get; set; } = 0;
        public String Description { get; set; } = "";
        public String Endian { get; set; } = MessageXmlConst.ENDIAN_BIG;
        public List<FieldModelBase> Fields { get; private set; } = new List<FieldModelBase>();
    }
}
