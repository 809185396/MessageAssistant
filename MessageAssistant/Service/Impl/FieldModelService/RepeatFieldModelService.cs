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
        protected override void _Decomposite(FieldModelBase field, ByteBuffer buf)
        {
            for(int i = 0; i < ((RepeatFieldModel)field).Repeat; ++i)
            {

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
