using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    class BitFieldModel : FieldModelBase
    {
        public int Length { get; set; }

        public override string GetFieldTypeName()
        {
            return MessageXmlConst.BIT_FIELD;
        }

        public override int GetLength()
        {
            return Length;
        }

        public List<BitChildModel> Children { get; private set; } = new List<BitChildModel>();
    }
}
