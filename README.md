# dlt645-tool

内置 DLT645 规则解析引擎的协议测试工具

## 配置规则

按照数据类型可分为：

- time
- bcd
- hex
- bit
- string
- block

### time

数据格式为 YY、MM、DD、WW、hh、mm、ss 的自由组合。

```
        {
            "id": "00000000",
            "name": "日期时间",
            "type": "time",
            "data": "YYMMDDWWhhmmss"
        },
```

### bcd

第一种：

```
        {
            "id": "00000000",
            "name": "当前电压（Ａ相）（XXX.X）",
            "type": "bcd",
            "data": "XXX.X",
            "unit": "V"
        },
```

第二种（数据块）：

```
        {
            "id": "00000000",
            "name": "当前电压（整块）（XXX.X）　",
            "type": "bcd",
            "data": [
                "XXX.X",
                "XXX.X",
                "XXX.X"
            ],
            "unit": "V"
        },
```

> 如果数据块中的数据单位不一样，则建议使用 block 类型。

### hex

需要显示原数据的要使用 hex 类型：

```
        {
            "id": "00000000",
            "name": "通信地址",
            "type": "hex",
            "data": "XXXXXXXXXXXX"
        },
```

一些没有数据域的控制指令也可以使用 hex 类型：

```
        {
            "id": "00000000",
            "name": "设备重启",
            "type": "hex",
            "data": ""
        },
```

### bit

位解析配置如下：

- lsb：表示该位段的最低位
- msb：表示该位段的最高位
- XX.：表示该位段的枚举值（00 ~ FF）

```
        {
            "id": "00000000",
            "name": "有功组合方式特征字",
            "type": "bit",
            "data": "XX",
            "bits": [
                {
                    "name": "正向有功",
                    "lsb": 0,
                    "msb": 1,
                    "00": "不加不减",
                    "01": "加",
                    "02": "减",
                    "03": "又加又减"
                },
                {
                    "name": "反向有功",
                    "lsb": 2,
                    "msb": 3,
                    "00": "不加不减",
                    "01": "加",
                    "02": "减",
                    "03": "又加又减"
                }
            ]
        },
```

支持多字节的位解析：

```
        {
            "id": "00000000",
            "name": "主动上报模式字",
            "type": "bit",
            "data": "XXXXXXXXXXXXXXXX",
            "bits": [
                {
                    "name": "bit-14: 分闸",
                    "lsb": 14,
                    "msb": 14,
                    "00": "",
                    "01": "✔"
                },
                {
                    "name": "bit-15: 合闸",
                    "lsb": 15,
                    "msb": 15,
                    "00": "",
                    "01": "✔"
                },
                {
                    "name": "bit-16: 失压",
                    "lsb": 16,
                    "msb": 16,
                    "00": "",
                    "01": "✔"
                },
                {
                    "name": "bit-17: 欠压",
                    "lsb": 17,
                    "msb": 17,
                    "00": "",
                    "01": "✔"
                },
                {
                    "name": "bit-18: 过压",
                    "lsb": 18,
                    "msb": 18,
                    "00": "",
                    "01": "✔"
                }
            ]
        },
```

### string

目前「fillin」「fillat」「endian」暂不支持。

```
        {
            "id": "00000000",
            "name": "资产编码（ASCII）",
            "type": "string",
            "len": 32,
            "fillin": 0,
            "fillat": "front",
            "endian": "LE"
        },
```

### block

block 类型中可以放置上述任意类型，支持在 block 中嵌套 block 类型。

```
        {
            "id": "00000000",
            "name": "电压整定参数块",
            "type": "block",
            "data": [
                {
                    "name": "过压整定值",
                    "type": "bcd",
                    "data": "XXX.X",
                    "unit": "V"
                },
                {
                    "name": "欠压整定值",
                    "type": "bcd",
                    "data": "XXX.X",
                    "unit": "V"
                },
                {
                    "name": "断相整定值",
                    "type": "bcd",
                    "data": "XXX.X",
                    "unit": "V"
                }
            ]
        },
```

```
        {
            "id": "00000000",
            "name": "瞬时冻结（数据块）",
            "type": "block",
            "data": [
                {
                    "name": "冻结时间",
                    "type": "time",
                    "data": "YYMMDDhhmm"
                },
                {
                    "name": "正向有功电能（总）",
                    "type": "bcd",
                    "data": [
                        "XXXXXX.XX"
                    ],
                    "unit": "kWh"
                },
                {
                    "name": "反向有功电能（总）",
                    "type": "bcd",
                    "data": [
                        "XXXXXX.XX"
                    ],
                    "unit": "kWh"
                },
                {
                    "name": "组合无功电能（c1）",
                    "type": "bcd",
                    "data": [
                        "XXXXXX.XX"
                    ],
                    "unit": "kvarh"
                },
                {
                    "name": "组合无功电能（c2）",
                    "type": "bcd",
                    "data": [
                        "XXXXXX.XX"
                    ],
                    "unit": "kvarh"
                }
            ]
        },
```
