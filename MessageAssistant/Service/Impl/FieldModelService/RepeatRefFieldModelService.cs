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
    class RepeatRefFieldModelService : FieldModelServiceBase
    {
        protected override void _Decomposite(FieldModelBase field, ByteBuffer buf)
        {
            
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
