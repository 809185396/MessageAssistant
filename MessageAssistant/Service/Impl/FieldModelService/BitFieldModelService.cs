using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MessageAssistant.Model;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class BitFieldModelService : FieldModelServiceBase
    {
        protected override void _Decomposite(FieldModelBase field, ByteBuffer buf)
        {
            throw new NotImplementedException();
        }

        protected override FieldModelBase _Read(XmlElement e)
        {
            BitFieldModel model = new BitFieldModel();
            _Read(e, model);

            BitChildModelService srv = new BitChildModelService();
            var children = e.ChildNodes;
            for (int i = 0; i < children.Count; ++i)
            {
                var childXml = children[i] as XmlElement;
                if (childXml == null)
                {
                    continue;
                }
                var child = srv.Read(childXml);
                if (child != null)
                {
                    model.Children.Add(child);
                }
            }
            return model;
        }
    }
}
