using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    class CompositeChildModel
    {
        public String Name { get; set; } = "";
        public String Description { get; set; } = "";
        public String Endian { get; set; }
        public int Length { get; set; }
        public String Type { get; set; }
        public double Rate { get; set; } = 1.0;
        public double Offset { get; set; }
        public String Skip { get; set; } = "false";
        public String DefaultValue { get; set; }
        public String Value { get; set; }
    }
}
