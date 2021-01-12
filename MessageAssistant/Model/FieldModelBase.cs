using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    /// <summary>
    /// 消息中的字段元素
    /// </summary>
    abstract class FieldModelBase
    {
        public String Name { get; set; } = "";
        public String Description { get; set; } = "";
        public String Endian { get; set; }
        public abstract int GetBitLength();
    }
}
