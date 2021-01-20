using MessageAssistant.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    class RepeatRefFieldModel : RepeatFieldModel
    {
        public String RepeatRef { get; set; }


        public override string GetFieldTypeName()
        {
            return MessageXmlConst.REPEAT_REF_FIELD;
        }
    }
}
