using MessageAssistant.Constant;
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
        public String Expression { get; set; }
        public List<FieldModelBase> Children { get; private set; } = new List<FieldModelBase>();

        public override int GetLength()
        {
            return Children.Sum(r => r.GetLength());
        }

        public override string GetFieldTypeName()
        {
            return MessageXmlConst.REPEAT_FIELD;
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
