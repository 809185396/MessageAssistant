using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    /// <summary>
    /// 消息中重复组合字段，子元素重复次数是固定值
    /// </summary>
    class RepeatFieldModel :FieldModelBase
    {
        public int Repeat { get; set; }
        public List<FieldModelBase> Children { get; private set; } = new List<FieldModelBase>();

        public override int GetLength()
        {
            return Children.Sum(r => r.GetLength()) * Repeat;
        }
    }
}
