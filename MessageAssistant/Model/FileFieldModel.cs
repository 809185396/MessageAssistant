using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    class FileFieldModel : FieldModelBase
    {
        public String FileName { get; set; }

        public List<FieldModelBase> Children { get; private set; } = new List<FieldModelBase>();

        public override int GetLength()
        {
            return Children.Sum(r => r.GetLength());
        }

        public override string GetFieldTypeName()
        {
            return MessageXmlConst.FILE_FIELD;
        }
    }
}
