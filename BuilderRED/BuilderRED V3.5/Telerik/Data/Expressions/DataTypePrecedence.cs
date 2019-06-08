// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.DataTypePrecedence
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Data.Expressions
{
  internal enum DataTypePrecedence
  {
    SqlBinary = -10, // -0x0000000A
    SqlBytes = -9,
    Char = -8,
    SqlChars = -7,
    SqlXml = -6,
    String = -5,
    SqlString = -4,
    SqlGuid = -3,
    Boolean = -2,
    SqlBoolean = -1,
    Error = 0,
    SByte = 1,
    SqlByte = 2,
    Byte = 3,
    Int16 = 4,
    SqlInt16 = 5,
    UInt16 = 6,
    Int32 = 7,
    SqlInt32 = 8,
    UInt32 = 9,
    Int64 = 10, // 0x0000000A
    SqlInt64 = 11, // 0x0000000B
    UInt64 = 12, // 0x0000000C
    SqlMoney = 13, // 0x0000000D
    Decimal = 14, // 0x0000000E
    SqlDecimal = 15, // 0x0000000F
    Single = 16, // 0x00000010
    SqlSingle = 17, // 0x00000011
    Double = 18, // 0x00000012
    SqlDouble = 19, // 0x00000013
    TimeSpan = 20, // 0x00000014
    DateTime = 23, // 0x00000017
    SqlDateTime = 24, // 0x00000018
  }
}
