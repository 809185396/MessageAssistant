﻿using System;

namespace MessageAssistant.Constant
{
    class MessageXmlConst
    {
        // 元素属性常量
        public const String MESSAGE = "message";
        public const String CMD = "cmd";
        public const String FIELD = "field";
        public const String IFFIELD = "if-field";
        public const String REPEAT_FIELD = "repeat-field";
        public const String FILE_FIELD = "file-field";
        public const String BIT_FIELD = "bit-field";
        public const String BIT_CHILD = "bit-child";
        public const String FILE_BLOCK = "file-block";
        public const String NAME = "name";
        public const String DESCRIPTION = "description";
        public const String ENDIAN = "endian";
        public const String LENGTH = "length";
        public const String TYPE = "type";
        public const String RATE = "rate";
        public const String OFFSET = "offset";
        public const String SKIP = "skip";
        public const String DEFAULT = "default";
        public const String VALUE = "value";

        // 组合字段重复次数
        public const String EXPRESSION = "expr";
        public const String FILE_REF = "file"; // 引用的文件

        // 值常量
        // 大小端
        public const String ENDIAN_BIG = "big";
        public const String ENDIAN_LITTLE = "little";
     
        // 是否跳过
        public const String SKIP_TRUE = "true";
        public const String SKIP_FALSE = "false";
        // 字段数据类型
        public const String TYPE_BYTE = "byte";
        public const String TYPE_SHORT = "short";
        public const String TYPE_USHORT = "ushort";
        public const String TYPE_INT = "int";
        public const String TYPE_UINT = "uint";
        public const String TYPE_LONG = "long";
        public const String TYPE_ULONG = "ulong";
        public const String TYPE_ASTRING = "ascii";
        public const String TYPE_CP56TIME = "cp56";

    }
}
