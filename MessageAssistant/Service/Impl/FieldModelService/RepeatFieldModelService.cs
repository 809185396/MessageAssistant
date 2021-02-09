using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MessageAssistant.Constant;
using MessageAssistant.Exceptions;
using MessageAssistant.Model;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class RepeatFieldModelService : FieldModelServiceBase
    {
        protected override void _CollectValueField(List<FieldModelBase> fields, FieldModelBase field)
        {
            RepeatFieldModel f = field as RepeatFieldModel;
            f.Children.ForEach(r =>
            {
                r.ForEach(r1 => CollectValueField(fields, r1));
            });
        }

        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            RepeatFieldModel f = field as RepeatFieldModel;

            var expr = PreProcessExpression(model, f.Expression);
            Object obj = ExpressionUtil.ComplierCode(expr);
            int result = 0;
            if (!int.TryParse(obj.ToString(), out result))
            {
                throw new BizException($"{f.Expression} is invalid");
            }
            f.Value = result;
            List<FieldModelBase> next = null;
            List<FieldModelBase> cur = null;
            for (int i = 0; i < result; ++i)
            {
                next = null;
                cur = f.Children[i];
                if(i < result - 1)
                {
                    next = new List<FieldModelBase>();
                    f.Children.Add(next);
                }
                foreach (var child in cur)
                {
                    if(next != null)
                    {
                        next.Add((FieldModelBase)child.Clone());
                    }
                    Decomposite(model, child, buf);
                }
            }
        }

        protected override FieldModelBase _Read(String strDir, XmlElement e)
        {
            RepeatFieldModel model = new RepeatFieldModel();
            _Read(e, model);
            model.Expression = e.GetAttributeEx(MessageXmlConst.EXPRESSION);
            model.Children[0].AddRange(ReadChildren(strDir, e));
            if (null != model.Children[0].FirstOrDefault(r => r is BitChildModel))
            {
                throw new BizException("repeat-field元素不能包含bit-child元素");
            }
            return model;
        }
    }
}
