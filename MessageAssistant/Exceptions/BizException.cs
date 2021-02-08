using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Exceptions
{
    class BizException:Exception
    {
        public BizException(string message):
            base(message)
        {
        }

        public BizException(string message, Exception innerException):
            base(message, innerException)
        {

        }
    }
}
