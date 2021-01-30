using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Util;

namespace MessageAssistant.Model
{
    [Serializable]
    /// <summary>
    /// 消息中具体字段元素
    /// </summary>
    class FieldModel : FieldModelBase
    {
        public String Endian { get; set; }
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
            return Length;
        }

        public override string GetFieldTypeName()
        {
            return MessageXmlConst.FIELD;
        }

        public override FieldModelBase GetFieldModelBase(string[] paths)
        {
            if (paths.Length != 0)
            {
                throw new ArgumentException("");
            }
            return this;
        }

        public override Object Clone()
        {
            return ObjectUtil.Copy<FieldModel>(this);
        }
    }
}
