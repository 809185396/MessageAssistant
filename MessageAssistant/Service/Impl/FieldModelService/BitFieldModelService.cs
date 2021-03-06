﻿using System;
using System.Collections.Generic;
using System.Xml;
using MessageAssistant.Constant;
using MessageAssistant.Exceptions;
using MessageAssistant.Model;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class BitFieldModelService : FieldModelServiceBase
    {
        protected override void _CollectValueField(List<FieldModelBase> fields, FieldModelBase field)
        {
            var f = field as BitFieldModel;
            f.Children.ForEach(r => CollectValueField(fields, r));
        }

        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            var f = field as BitFieldModel;
            byte[] bts = new byte[f.Length];
            buf.ReadBytes(bts, 0, f.Length);
            int loc = 0;
            BitChildModelService childService = new BitChildModelService();
            foreach (var child in f.Children)
            {
                int len = FieldModelService.GetNeedLengthAccordDataType(child.DataType);
                int childByteLen = child.GetLength();
                len = len > childByteLen ? len : childByteLen;
                byte[] btsChild = new byte[len];

                BitUtil.GetBits(bts, btsChild, loc, child.Length);
                loc += child.Length;
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
                if(children[i] is BitChildModel)
                {
                    model.Children.Add(children[i] as BitChildModel);
                }
                else
                {
                    throw new BizException("bit-field 下包含了非bit-child元素");
                }
            }
            return model;
        }
    }
}
