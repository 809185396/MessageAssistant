﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageAssistant.Model
{
    [Serializable]
    /// <summary>
    /// 消息中的字段元素
    /// </summary>
    abstract class FieldModelBase : ICloneable
    {
        public String Name { get; set; } = "";
        public String Description { get; set; } = "";        
        public abstract int GetLength();
        public abstract String GetFieldTypeName();
        public abstract FieldModelBase GetFieldModelBase(String[] paths);
        public abstract object Clone();
    }
}
