// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.EditorSupportedType
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  [Flags]
  public enum EditorSupportedType
  {
    Object = 0,
    DBNull = 1,
    Null = 2,
    Bool = 4,
    Char = 8,
    Sbyte = 16, // 0x00000010
    Byte = 32, // 0x00000020
    Short = 64, // 0x00000040
    UShort = 128, // 0x00000080
    Int = 256, // 0x00000100
    UInt = 512, // 0x00000200
    Long = 1024, // 0x00000400
    Ulong = 2048, // 0x00000800
    Float = 4096, // 0x00001000
    Double = 8192, // 0x00002000
    Decimal = 16384, // 0x00004000
    DateTime = 32768, // 0x00008000
    TimeSpan = 65536, // 0x00010000
    String = 131072, // 0x00020000
    Guid = 262144, // 0x00040000
    Type = 524288, // 0x00080000
    Uri = 1048576, // 0x00100000
    SqlBinary = 2097152, // 0x00200000
    SqlBoolean = 4194304, // 0x00400000
    SqlByte = 8388608, // 0x00800000
    SqlBytes = 16777216, // 0x01000000
    SqlChars = 33554432, // 0x02000000
    SqlDateTime = 67108864, // 0x04000000
    SqlDecimal = 134217728, // 0x08000000
    SqlDouble = 268435456, // 0x10000000
    SqlGuid = 536870912, // 0x20000000
    SqlInt16 = 1073741824, // 0x40000000
    SqlInt32 = -2147483648, // -0x80000000
    SqlInt64 = 0,
    SqlMoney = 0,
    SqlSingle = 0,
    SqlString = 0,
    Numeric = SqlInt32 | SqlInt16 | SqlGuid | SqlDouble | SqlDecimal | SqlByte | Decimal | Double | Float | Ulong | Long | UInt | Int | UShort | Short | Byte | Sbyte, // -0x077F8010
    Alpha = SqlChars | Uri | String | Char, // 0x02120008
    AlphaNumeric = Alpha | Numeric, // -0x056D8008
  }
}
