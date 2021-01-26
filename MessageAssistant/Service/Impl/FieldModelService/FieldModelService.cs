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
        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            if(!(field is FieldModel))
            {
                throw new ArgumentException("argument is not ");
            }

            FieldModel fieldModel = (FieldModel)field;
            int len = 0;
            switch (fieldModel.Type)
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
                    len = fieldModel.Length;
                    break;
                default:
                    break;
            }
            // 保证临时空间够装入要读取的字节数量。
            len = len > fieldModel.Length ? len : fieldModel.Length;
            byte[] bts = new byte[len];
            for (int i = 0; i < bts.Length; ++i)
            {
                bts[i] = 0;
            }
            buf.ReadBytes(bts, 0, fieldModel.Length);
            ByteBuffer tmp = ByteBuffer.Allocate(bts);
            double val = 0;
            switch (fieldModel.Type)
            {
                case MessageXmlConst.TYPE_BYTE:
                    val = tmp.ReadByte();
                    break;
                case MessageXmlConst.TYPE_SHORT:
                    val = tmp.ReadShort(fieldModel.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_USHORT:
                    val = tmp.ReadUshort(fieldModel.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_INT:
                    val = tmp.ReadInt(fieldModel.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_UINT:
                    val = tmp.ReadUint(fieldModel.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_LONG:
                    val = tmp.ReadLong(fieldModel.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_ULONG:
                    val = tmp.ReadUlong(fieldModel.IsLittleEndian);
                    break;
                case MessageXmlConst.TYPE_ASTRING:
                    fieldModel.Value = System.Text.Encoding.ASCII.GetString(bts);
                    return;
                default:
                    break;
            }

            if (fieldModel.Rate != 1 && fieldModel.Rate != 0)
            {
                val = val / fieldModel.Rate;
            }
            val = val - fieldModel.Offset;
            fieldModel.Value = val.ToString();
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
            return model;
        }
    }
}
