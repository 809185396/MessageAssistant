﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MessageAssistant.Constant;
using MessageAssistant.Model;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class IfFieldModelService : FieldModelServiceBase
    {
        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            IfFieldModel f = field as IfFieldModel;
            foreach (var child in f.Children)
            {
                Decomposite(model, child, buf);
            }
        }

        protected override FieldModelBase _Read(XmlElement e)
        {
            IfFieldModel model = new IfFieldModel();
            _Read(e, model);
            model.Ref = e.GetAttributeEx(MessageXmlConst.IF_REF);
            model.Relation = e.GetAttributeEx(MessageXmlConst.IF_RELATION);
            model.Value = e.GetAttributeEx(MessageXmlConst.VALUE);
            model.Children.AddRange(ReadChildren(e));
            return model;
        }
    }
}
