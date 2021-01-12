using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Util
{
    class Assert
    {
        public static void NotNull(object param, string tip)
        {
            if (param == null)
            {
                throw new ArgumentNullException(tip);
            }
        }
        public static void NotNullOrEmpty(string param, string tip)
        {
            if (param == null || param == string.Empty)
            {
                throw new ArgumentException(tip);
            }
        }
        public static void NotNullOrEmpty<T>(IEnumerable<T> param, string tip)
        {
            if (param == null || param.Count() == 0)
            {
                throw new ArgumentException(tip);
            }
        }
    }
}
