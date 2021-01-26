using MessageAssistant.Constant;
using MessageAssistant.Model;
using MessageAssistant.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
                case MessageXmlConst.REPEAT_REF_FIELD:
                    return new RepeatRefFieldModelService();
                case MessageXmlConst.BIT_FIELD:
                    return new BitFieldModelService();
                default:
                    return null;
            }
        }
    }
}
