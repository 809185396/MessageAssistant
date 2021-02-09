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
    /// 消息中重复组合字段，子元素重复次数是固定值
    /// </summary>
    class RepeatFieldModel :FieldModelBase
    {
        public String Expression { get; set; }

        public int Value { get; set; } = 1;

        public List<List<FieldModelBase>> Children { get; private set; } = new List<List<FieldModelBase>>();

        public RepeatFieldModel()
        {
            Children.Add(new List<FieldModelBase>());
        }
        public override int GetLength()
        {
            return Children[0].Sum(r => r.GetLength());
        }

        public override string GetFieldTypeName()
        {
            return MessageXmlConst.REPEAT_FIELD;
        }

        public override FieldModelBase GetFieldModelBase(string[] paths)
        {
            if (paths.Length == 0 || paths.Length ==1)
            {
                return this;
            }
            int index = 0;
            if(!int.TryParse(paths[0], out index))
            {
                // TODO:
                return null;
            }

            FieldModelBase field = Children[index].First(r => r.Name == paths[1]);
            if (field == null)
            {
                throw new ArgumentException("");
            }
            return field.GetFieldModelBase(paths.Skip(2).ToArray());
        }

        public override Object Clone()
        {
            return ObjectUtil.Copy<RepeatFieldModel>(this);
        }
    }
}
