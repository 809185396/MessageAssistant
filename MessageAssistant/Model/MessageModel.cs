using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Util;

namespace MessageAssistant.Model
{
    class MessageModel
    {
        public String Name { get; set; } = "";
        public int Cmd { get; set; } = 0;
        public String Description { get; set; } = "";
        public String Endian { get; set; } = MessageXmlConst.ENDIAN_BIG;
        public List<FieldModelBase> Fields { get; private set; } = new List<FieldModelBase>();

        public String GetValue(String[] path)
        {
            Assert.NotNullOrEmpty(path, "");
            FieldModelBase field = Fields.First(r => r.Name == path[0]);
            if(field == null)
            {
                throw new ArgumentException("");
            }
            return field.GetValue(path.Skip(1));
        }
    }
}
