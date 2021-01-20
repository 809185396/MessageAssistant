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
    class FieldModelService : FieldModelServiceBase
    {
        protected override void _Decomposite(FieldModel field, ByteBuffer buf)
        {
            int len = 0;
            switch (field.Type)
            {
                case MessageXmlConst.TYPE_BYTE:
                    len = 1;
                    break;
                case MessageXmlConst.TYPE_SHORT:
                    len = 2;
                    return;
                case MessageXmlConst.TYPE_USHORT:
                    len = 2;
                    return;
                case MessageXmlConst.TYPE_INT:
                    len = 4;
                    break;
                case MessageXmlConst.TYPE_UINT:
                    len = 4;
                    break;
                case MessageXmlConst.TYPE_LONG:
                    len = 8;
                    break;
                case MessageXmlConst.TYPE_ULONG:
                    len = 8;
                    break;
                case MessageXmlConst.TYPE_ASTRING:
                    len = field.Length;
                    break;
                default:
                    break;
            }
            // 保证临时空间够装入要读取的字节数量。
            len = len > field.Length ? len : field.Length;
            byte[] bts = new byte[len];
            for (int i = 0; i < bts.Length; ++i)
            {
                bts[i] = 0;
            }
            buf.ReadBytes(bts, 0, field.Length);
            ByteBuffer tmp = ByteBuffer.Allocate(bts);
            double val = 0;
            switch (field.Type)
            {
                case MessageXmlConst.TYPE_BYTE:
                    val = tmp.ReadByte();
                    break;
                case MessageXmlConst.TYPE_SHORT:
                    val = tmp.ReadShort(field.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_USHORT:
                    val = tmp.ReadUshort(field.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_INT:
                    val = tmp.ReadInt(field.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_UINT:
                    val = tmp.ReadUint(field.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_LONG:
                    val = tmp.ReadLong(field.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_ULONG:
                    val = tmp.ReadUlong(field.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_ASTRING:
                    field.Value = System.Text.Encoding.ASCII.GetString(bts);
                    return;
                default:
                    break;
            }

            if (field.Rate != 1 && field.Rate != 0)
            {
                val = val / field.Rate;
            }
            val = val - field.Offset;
            field.Value = val.ToString();
        }

        protected override FieldModelBase _Read(XmlElement e)
        {
            FieldModel model = new FieldModel();
            base._Read(e, model);
            model.Length = e.GetAttributeInt(MessageXmlConst.LENGTH);
            model.Type = e.GetAttributeEx(MessageXmlConst.TYPE);
            model.Rate = e.GetAttributeDouble(MessageXmlConst.RATE, 1);
            model.Offset = e.GetAttributeDouble(MessageXmlConst.OFFSET, 0);
            model.Skip = e.GetAttributeEx(MessageXmlConst.SKIP, MessageXmlConst.SKIP_FALSE);
            model.Unit = e.GetAttributeEx(MessageXmlConst.UNIT, MessageXmlConst.UNIT_BYTE);
            return model;
        }
    }
}
