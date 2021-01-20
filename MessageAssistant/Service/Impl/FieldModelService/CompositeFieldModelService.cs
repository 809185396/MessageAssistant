using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MessageAssistant.Model;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class CompositeFieldModelService : FieldModelServiceBase
    {
        protected override FieldModelBase _Read(XmlElement e)
        {
            CompositeFieldModel model = new CompositeFieldModel();
            _Read(e, model);
            model.Children.AddRange(ReadChildren(e));
            return model;
        }
    }
}
