using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    class CompositeFieldFileModel : FieldModelBase
    {
        public String FileName { get; set; }

        public List<FieldModelBase> Children { get; private set; } = new List<FieldModelBase>();

        public override int GetBitLength()
        {
            return Children.Sum(r => r.GetBitLength());
        }
    }
}
