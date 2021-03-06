﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MessageAssistant.Constant;
using MessageAssistant.Exceptions;
using MessageAssistant.Model;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    class FileFieldModelService : FieldModelServiceBase
    {
        protected override void _CollectValueField(List<FieldModelBase> fields, FieldModelBase field)
        {
            FileFieldModel f = field as FileFieldModel;
            f.Children.ForEach(r => CollectValueField(fields, r));
        }

        protected override void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            FileFieldModel f = field as FileFieldModel;
            foreach (var child in f.Children)
            {
                Decomposite(model, child, buf);
            }
        }

        protected override FieldModelBase _Read(string strDir, XmlElement e)
        {
            FileFieldModel model = new FileFieldModel();
            _Read(e, model);
            model.FileName = e.GetAttributeEx(MessageXmlConst.FILE_REF);
            String filePath = strDir.TrimEnd(new char[] { '/', '\\' }) + Path.DirectorySeparatorChar + model.FileName;
            var children = ReadRefFile(filePath);
            model.Children.AddRange(children);
            return model;
        }

        private List<FieldModelBase> ReadRefFile(String file)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(file);
                var xn = doc.SelectSingleNode(MessageXmlConst.FILE_BLOCK);
                var strDir = System.IO.Path.GetDirectoryName(file);
                if (xn != null)
                {
                    return ReadChildren(strDir, (XmlElement)xn);
                }
            }
            catch (Exception ex)
            {
                throw new BizException($"解析{file}文件失败.");
            }
            return null;
        }
    }
}
