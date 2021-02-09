using System;
using System.Xml;
using MessageAssistant.Model;
using MessageAssistant.Util;
using MessageAssistant.Constant;
using System.Collections.Generic;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class FieldModelService : FieldModelServiceBase
    {
        public static int GetNeedLengthAccordDataType(String type)
        {
            int len = 0;
            switch (type)
            {
                case MessageXmlConst.TYPE_BYTE:
                    len = 1;
                    break;
                case MessageXmlConst.TYPE_SHORT:
                    len = 2;
                    break;
                case MessageXmlConst.TYPE_USHORT:
                    len = 2;
                    break; 
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
                default:
                    break;
            }
            return len;
        }

        protected override void _CollectValueField(List<FieldModelBase> fields, FieldModelBase field)
        {
            fields.Add(field);
        }

        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            if(!(field is FieldModel))
            {
                throw new ArgumentException("argument is not ");
            }

            FieldModel fieldModel = (FieldModel)field;
            if (String.IsNullOrEmpty(fieldModel.Endian))
            {
                fieldModel.Endian = model.Endian;
            }
            int len = GetNeedLengthAccordDataType(fieldModel.DataType);
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
            switch (fieldModel.DataType)
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
            fieldModel.OriginalContent = StringConverter.byteToHexStr(bts);
        }

        protected override FieldModelBase _Read(String strDir, XmlElement e)
        {
            FieldModel model = new FieldModel();
            base._Read(e, model);
            String str = e.GetAttributeEx(MessageXmlConst.ENDIAN, null);
            model.Endian = str;
            model.Length = e.GetAttributeInt(MessageXmlConst.LENGTH);
            model.DataType = e.GetAttributeEx(MessageXmlConst.TYPE);
            model.Rate = e.GetAttributeDouble(MessageXmlConst.RATE, 1);
            model.Offset = e.GetAttributeDouble(MessageXmlConst.OFFSET, 0);
            model.Skip = e.GetAttributeEx(MessageXmlConst.SKIP, MessageXmlConst.SKIP_FALSE);
            return model;
        }
    }
}
