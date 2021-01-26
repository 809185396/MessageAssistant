using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            for (int i = 0; i < f.Repeat; ++i)
            {
                foreach (var child in f.Children)
                {
                    Decomposite(model, child, buf);
                }
            }
        }

        protected override FieldModelBase _Read(XmlElement e)
        {
            RepeatFieldModel model = new RepeatFieldModel();
            _Read(e, model);
            model.Repeat = e.GetAttributeInt(MessageXmlConst.REPEAT);
            model.Children.AddRange(ReadChildren(e));
            return model;
        }
    }
}
