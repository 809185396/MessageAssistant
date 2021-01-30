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
    class BitChildModelService:FieldModelServiceBase
    {
        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            if(!(field is BitChildModel))
            {
                return;
            }
            BitChildModel child = (BitChildModel)field;
            double val = 0;
            switch (child.Type)
            {
                case MessageXmlConst.TYPE_BYTE:
                    val = buf.ReadByte();
                    break;
                case MessageXmlConst.TYPE_SHORT:
                    val = buf.ReadShort(child.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_USHORT:
                    val = buf.ReadUshort(child.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_INT:
                    val = buf.ReadInt(child.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_UINT:
                    val = buf.ReadUint(child.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_LONG:
                    val = buf.ReadLong(child.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_ULONG:
                    val = buf.ReadUlong(child.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_ASTRING:
                    child.Value = System.Text.Encoding.ASCII.GetString(buf.ToArray());
                    return;
                default:
                    break;
            }

            if (child.Rate != 1 && child.Rate != 0)
            {
                val = val / child.Rate;
            }
            val = val - child.Offset;
            child.Value = val.ToString();
        }

        protected override FieldModelBase _Read(String strDir, XmlElement e)
        {
            BitChildModel model = new BitChildModel();
            _Read(e, model);
            model.Length = e.GetAttributeInt(MessageXmlConst.LENGTH);
            model.Type= e.GetAttributeEx(MessageXmlConst.TYPE);
            model.Rate = e.GetAttributeDouble(MessageXmlConst.RATE, 1);
            model.Offset = e.GetAttributeDouble(MessageXmlConst.OFFSET, 0);
            model.Skip = e.GetAttributeEx(MessageXmlConst.SKIP, MessageXmlConst.SKIP_FALSE);
            return model;
        }
    }
}