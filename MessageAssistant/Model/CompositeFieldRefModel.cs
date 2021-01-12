using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    class CompositeFieldRefModel : FieldModelBase
    {
        public String RepeatRef { get; set; }
        public int Repeat { get; set; }
        public List<FieldModelBase> Children { get; private set; } = new List<FieldModelBase>();

        public override int GetBitLength()
        {
            return Children.Sum(r => r.GetBitLength());
        }
    }
}
