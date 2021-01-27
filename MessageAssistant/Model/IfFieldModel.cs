using System;
using System.Collections.Generic;
using System.Linq;
using MessageAssistant.Constant;

namespace MessageAssistant.Model
{
    class IfFieldModel : FieldModelBase
    {
        public String Expression { get; set; }

        public List<FieldModelBase> Children { get; private set; } = new List<FieldModelBase>();
        public override string GetFieldTypeName()
        {
            return MessageXmlConst.IFFIELD;
        }

        public override int GetLength()
        {
            return Children.Sum(r => r.GetLength());
        }

        public override FieldModelBase GetFieldModelBase(string[] paths)
        {
            if (paths.Length == 0)
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
    }
}
