using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Constant;

namespace MessageAssistant.Model
{
    class BitChildModel: FieldModelBase
    {
        public int Length { get; set; }
        public String Type { get; set; }
        public double Rate { get; set; } = 1.0;
        public double Offset { get; set; }
        public String Skip { get; set; } = "false";
        public String DefaultValue { get; set; }
        public String Value { get; set; }
        public bool IsLittleEndian { get { return Endian == MessageXmlConst.ENDIAN_LITTLE; } }

        public override int GetLength()
        {
            return (Length - 1) / 8 + 1;
        }

        public override string GetFieldTypeName()
        {
            return Type;
        }

        public override FieldModelBase GetFieldModelBase(string[] paths)
        {
            if(paths.Length != 0)
            {
                throw new ArgumentException("");
            }
            return this;
        }
    }
}
