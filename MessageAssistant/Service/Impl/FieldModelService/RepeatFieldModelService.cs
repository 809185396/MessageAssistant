using System;
using System.Xml;
using MessageAssistant.Model;
using MessageAssistant.Util;
using MessageAssistant.Constant;

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
            for (int i = 0; i < result; ++i)
            {
                foreach (var child in f.Children)
                {
                    Decomposite(model, child, buf);
                }
            }
        }

        protected override FieldModelBase _Read(String strDir, XmlElement e)
        {
            RepeatFieldModel model = new RepeatFieldModel();
            _Read(e, model);
            model.Expression = e.GetAttributeEx(MessageXmlConst.EXPRESSION);
            model.Children.AddRange(ReadChildren(strDir, e));
            return model;
        }
    }
}
