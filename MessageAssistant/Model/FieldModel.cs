using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    /// <summary>
    /// 消息中具体字段元素
    /// </summary>
    class FieldModel : FieldModelBase
    {
        public String Endian { get; set; } = MessageXmlConst.ENDIAN_BIG;
        public int Length { get; set; }
        public String Unit { get; set; } = MessageXmlConst.UNIT_BYTE;
        public String Type { get; set; }
        public double Rate { get; set; } = 1.0;
        public double Offset { get; set; }
        public String Skip { get; set; } = "false";
        public String DefaultValue { get; set; }
        public String Value { get; set; }

        public override int GetBitLength()
        {
            return Length * (MessageXmlConst.UNIT_BYTE == Unit? 8: 1);
        }
    }
}
