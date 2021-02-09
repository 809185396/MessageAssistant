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
    class IfFieldModelService : FieldModelServiceBase
    {
        protected override void _CollectValueField(List<FieldModelBase> fields, FieldModelBase field)
        {
            IfFieldModel f = field as IfFieldModel;
            f.Children.ForEach(r => CollectValueField(fields, r));
        }

        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            IfFieldModel f = field as IfFieldModel;
            var expr = PreProcessExpression(model, f.Expression);
            Object obj = ExpressionUtil.ComplierCode(expr);
            bool result = false;
            if(!bool.TryParse(obj.ToString(), out result))
            {
                throw new BizException($"{f.Expression} is invalid");
            }
            f.Value = result;
            if (!result)
            {
                return;
            }
            foreach (var child in f.Children)
            {
                Decomposite(model, child, buf);
            }
        }

        protected override FieldModelBase _Read(String strDir, XmlElement e)
        {
            IfFieldModel model = new IfFieldModel();
            _Read(e, model);
            model.Expression = e.GetAttributeEx(MessageXmlConst.EXPRESSION);
            model.Children.AddRange(ReadChildren(strDir, e));
            if(null != model.Children.FirstOrDefault(r=>r is BitChildModel))
            {
                throw new BizException("if-field元素不能包含bit-child元素");
            }
            return model;
        }
    }
}
