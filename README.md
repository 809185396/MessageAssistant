软件名称是报文助手，主要想实现从报文数据流解析出其中的各个域，同时也可以根据各个域中的值来组建报文数据流。目的减轻我们分析和拼接报文的负担，提高工作效率。改善解析和拼接报文这种枯燥乏味的工作，希望它能完成这个初衷。

消息格式用xml文件来描述，xml文件的结构描述如下：
1 文件根节点是<message>
1.1 文件根节点必须有name, cmd属性
1.2 文件根节点可以有description, endian属性
1.3 message节点下可以有field, if-field,repeat-field, repeat-ref-field,file-field,composite-field等字段节点
2 字段节点是

