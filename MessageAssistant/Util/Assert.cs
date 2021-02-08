using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageAssistant.Exceptions;

namespace MessageAssistant.Util
{
    class Assert
    {
        public static void NotNull(object param, string tip)
        {
            if (param == null)
            {
                throw new BizException(tip);
            }
        }
        public static void NotNullOrEmpty(string param, string tip)
        {
            if (param == null || param == string.Empty)
            {
                throw new BizException(tip);
            }
        }
        public static void NotNullOrEmpty<T>(IEnumerable<T> param, string tip)
        {
            if (param == null || param.Count() == 0)
            {
                throw new BizException(tip);
            }
        }
    }
}
