using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Constant;
using MessageAssistant.Util;

namespace MessageAssistant.Model
{
    [Serializable]
    class MessageModel
    {
        public String Name { get; set; } = "";
        public int Cmd { get; set; } = 0;
        public String Description { get; set; } = "";
        public String Endian { get; set; } = MessageXmlConst.ENDIAN_BIG;
        public List<FieldModelBase> Fields { get; private set; } = new List<FieldModelBase>();

        public FieldModelBase GetFieldModelBase(String path)
        {
            Assert.NotNullOrEmpty(path, "");
            String[] paths = path.Split(new char[] { '.', '[', ']' });
            FieldModelBase field = Fields.First(r => r.Name == paths[0]);
            if(field == null)
            {
                throw new ArgumentException("");
            }
            return field.GetFieldModelBase(paths.Skip(1).ToArray());
        }
    }
}
