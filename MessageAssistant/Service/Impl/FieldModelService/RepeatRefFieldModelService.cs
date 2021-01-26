using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MessageAssistant.Model;
using MessageAssistant.Constant;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class RepeatRefFieldModelService : RepeatFieldModelService
    {
        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            RepeatRefFieldModel f = field as RepeatRefFieldModel;
            String strRef = f.RepeatRef;
            FieldModelBase refField = model.GetFieldModelBase(strRef);
            String strValue = null;
            if (refField is FieldModel)
            {
                strValue = ((FieldModel)refField).Value.ToString();
            } else if(refField is BitChildModel)
            {
                strValue = ((BitChildModel)refField).Value.ToString();
            }
            if (String.IsNullOrEmpty(strValue))
            {
                // TODO:
            }
            int repeat;
            if(!int.TryParse(strValue, out repeat))
            {
                // TODO:
            }
            f.Repeat = repeat;
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
            RepeatRefFieldModel model = new RepeatRefFieldModel();
            _Read(e, model);
            model.RepeatRef = e.GetAttributeEx(MessageXmlConst.REPEAT_REF);
            model.Children.AddRange(ReadChildren(e));
            return model;
        }
    }
}
