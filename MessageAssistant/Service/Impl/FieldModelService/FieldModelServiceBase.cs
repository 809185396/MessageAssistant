using MessageAssistant.Constant;
using MessageAssistant.Model;
using MessageAssistant.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;

namespace MessageAssistant.Service.Impl.FieldModelService
{
    abstract class FieldModelServiceBase
    {
        public static FieldModelBase Read(XmlElement e)
        {
            FieldModelServiceBase srv = _getFieldModelBaseService(e.Name);
            if(srv != null)
            {
                return srv._Read(e);
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

        protected abstract FieldModelBase _Read(XmlElement e);

        protected abstract void _Decomposite(MessageModel model, FieldModelBase field, ByteBuffer buf);

        protected void _Read(XmlElement e, FieldModelBase model)
        {
            String strEle = e.OuterXml;
            String str = e.GetAttributeEx(MessageXmlConst.FIELD);
            Assert.NotNullOrEmpty(str, strEle + " 名称不可以为空");
            model.Name = str;

            model.Description = e.GetAttributeEx(MessageXmlConst.DESCRIPTION, "");
            str = e.GetAttributeEx(MessageXmlConst.ENDIAN, null);
            model.Endian = str;
            return;
        }        
 
        public static List<FieldModelBase> ReadChildren(XmlElement e)
        {
            List<FieldModelBase> childrenList = new List<FieldModelBase>();
            var children = e.ChildNodes;
            for (int i = 0; i < children.Count; ++i)
            {
                var child = children[i] as XmlElement;
                if (child != null)
                {
                    childrenList.Add(Read(child));
                }
                var field = Read(child);
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
                default:
                    return null;
            }
        }

        public static string PreProcessExpression(MessageModel model, String expression)
        {
            Regex reg = new Regex(@"\$\{(\w+)\}");
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
                else if (field is BitChildModel)
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
