using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Constant;

namespace MessageAssistant.Model
{
    class FieldChildModel
    {
        public String Endian { get; set; } = MessageXmlConst.ENDIAN_BIG;
        public int Length { get; set; }
        public String Unit { get; set; } = MessageXmlConst.UNIT_BIT;
        public String Type { get; set; }
        public double Rate { get; set; } = 1.0;
        public double Offset { get; set; }
        public String Skip { get; set; } = "false";
        public String DefaultValue { get; set; }
        public String Value { get; set; }

        public bool IsLittleEndian { get { return Endian == MessageXmlConst.ENDIAN_LITTLE; } }
    }
}
