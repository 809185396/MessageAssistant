using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using MessageAssistant.Constant;
using MessageAssistant.Model;
using MessageAssistant.Util;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    abstract class FieldModelServiceBase
    {
        public static FieldModelBase Read(String strDir, XmlElement e)
        {
            FieldModelServiceBase srv = _getFieldModelBaseService(e.Name);
            if(srv != null)
            {
                return srv._Read(strDir, e);
            }
            return null;
        }

        public static void Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf)
        {
            FieldModelServiceBase srv = _getFieldModelBaseService(field);
            if (srv != null)
            {
                srv._Decomposite(model, field, buf);
            }
        }

        public static void CollectValueField(List<FieldModelBase> fields, FieldModelBase field)
        {
            FieldModelServiceBase srv = _getFieldModelBaseService(field);
            if (srv != null)
            {
                srv._CollectValueField(fields, field);
            }
        }
        protected abstract void _CollectValueField(List<FieldModelBase> fields, FieldModelBase field);

        protected abstract FieldModelBase _Read(String strDir, XmlElement e);

        protected abstract void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf);

        protected void _Read(XmlElement e, FieldModelBase model)
        {
            String strEle = e.OuterXml;
            String str = e.GetAttributeEx(MessageXmlConst.NAME);
            Assert.NotNullOrEmpty(str, strEle + " 名称不可以为空");
            model.Name = str;

            model.Description = e.GetAttributeEx(MessageXmlConst.DESCRIPTION, "");            
            return;
        }        
 
        public static List<FieldModelBase> ReadChildren(String strDir, XmlElement e)
        {
            List<FieldModelBase> childrenList = new List<FieldModelBase>();
            var children = e.ChildNodes;
            for (int i = 0; i < children.Count; ++i)
            {
                var child = children[i] as XmlElement;
                if (child == null)
                {
                    continue;
                }
                var field = Read(strDir,child);
                if(field != null)
                {
                    childrenList.Add(field);
                }
            }
            return childrenList;
        }

        public static FieldModelServiceBase _getFieldModelBaseService(FieldModelBase field)
        {
              return _getFieldModelBaseService(field.GetFieldTypeName());
        }

        public static FieldModelServiceBase _getFieldModelBaseService(String fieldTypeName)
        {
            switch (fieldTypeName)
            {
                case MessageXmlConst.FIELD:
                    return new FieldModelService();
                case MessageXmlConst.IFFIELD:
                    return new IfFieldModelService();
                case MessageXmlConst.REPEAT_FIELD:
                    return new RepeatFieldModelService();
                case MessageXmlConst.BIT_FIELD:
                    return new BitFieldModelService();
                case MessageXmlConst.BIT_CHILD:
                    return new BitChildModelService();
                case MessageXmlConst.FILE_FIELD:
                    return new FileFieldModelService();
                default:
                    return null;
            }
        }

        public static string PreProcessExpression(MessageModel model, String expression)
        {
            Regex reg = new Regex(@"\$\{\s*([\w|\-|.]+)\s*\}");
            int loc = 0;
            Match m = reg.Match(expression, loc);
            while (m.Success)
            {
                loc += 1;
                var strReplace = m.Groups[0].Value;
                var strFieldName = m.Groups[1].Value;
                var field = model.GetFieldModelBase(strFieldName);
                String strValue = null;
                if (field is FieldModel)
                {
                    strValue = ((FieldModel)field).Value;
                }
                else if (field is BitFieldModel)
                {
                    strValue = ((BitChildModel)field).Value;
                }
                if (String.IsNullOrEmpty(strValue))
                {
                    // TODO:
                }
                expression = expression.Replace(strReplace, strValue);
                m = reg.Match(expression, loc);
            }

            return expression;
        }
    }
}
