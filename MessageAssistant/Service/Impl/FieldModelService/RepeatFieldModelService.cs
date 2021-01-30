using System;
using System.Xml;
using MessageAssistant.Model;
using MessageAssistant.Util;
using MessageAssistant.Constant;
using System.Collections.Generic;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class RepeatFieldModelService : FieldModelServiceBase
    {
        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            RepeatFieldModel f = field as RepeatFieldModel;

            var expr = PreProcessExpression(model, f.Expression);
            Object obj = ExpressionUtil.ComplierCode(expr);
            int result = 0;
            if (!int.TryParse(obj.ToString(), out result))
            {
                // TODO: 表达式错误
            }
            List<FieldModelBase> next = null;
            List<FieldModelBase> cur = null;
            for (int i = 0; i < result; ++i)
            {
                next = null;
                cur = f.Children[i];
                if(i < result - 1 && f.Children.Count == i)
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
            return model;
        }
    }
}
