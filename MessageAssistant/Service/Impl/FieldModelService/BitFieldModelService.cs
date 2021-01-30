using System;
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
    class BitFieldModelService : FieldModelServiceBase
    {
        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            var f = field as BitFieldModel;
            byte[] bts = new byte[f.Length];
            buf.ReadBytes(bts, 0, f.Length);
            int loc = 0;
            BitChildModelService childService = new BitChildModelService();
            foreach (var child in f.Children)
            {
                int chileLen = child.GetLength();
                byte[] btsChild = new byte[chileLen];
                BitUtil.GetBits(bts, btsChild, loc, chileLen);
                ByteBuffer childBuf = ByteBuffer.Allocate(btsChild);
                Decomposite(model, child, childBuf);
            }
        }

        protected override FieldModelBase _Read(String strDir, XmlElement e)
        {
            BitFieldModel model = new BitFieldModel();
            _Read(e, model);
            model.Length = e.GetAttributeInt(MessageXmlConst.LENGTH);
            var children = ReadChildren(strDir, e);
            for(int i = 0; i < children.Count; ++i)
            {
                if(children[i] is BitFieldModel)
                {
                    model.Children.Add(children[i] as BitFieldModel);
                }
                else
                {
                    throw new ArgumentException("");
                }
            }
            return model;
        }
    }
}
