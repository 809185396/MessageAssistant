using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using MessageAssistant.Util;

namespace MessageAssistant.Model
{
    [Serializable]
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

        public override FieldModelBase GetFieldModelBase(string[] paths)
        {
            if(paths.Length == 0)
            {
                return this;
            }
            FieldModelBase field = Children.First(r => r.Name == paths[0]);
            if (field == null)
            {
                throw new ArgumentException("");
            }
            return field.GetFieldModelBase(paths.Skip(1).ToArray());
        }

        public List<BitChildModel> Children { get; private set; } = new List<BitChildModel>();

        public override Object Clone()
        {
            return ObjectUtil.Copy<BitFieldModel>(this);
        }
    }
}
