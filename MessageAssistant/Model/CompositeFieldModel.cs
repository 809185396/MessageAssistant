using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    class CompositeFieldModel : FieldModelBase
    {
        public int Length { get; set; }

        public override string GetFieldTypeName()
        {
            return MessageXmlConst.COMPOSITE_FIELD;
        }

        public override int GetLength()
        {
            return Length;
        }

        public override string GetValue(string[] path)
        {
            throw new NotImplementedException();
        }

        public List<CompositeChildModel> Children { get; private set; } = new List<CompositeChildModel>();
    }
}
