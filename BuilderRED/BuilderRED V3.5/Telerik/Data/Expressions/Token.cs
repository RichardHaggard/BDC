// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.Token
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Data.Expressions
{
  internal enum Token
  {
    None,
    Name,
    Numeric,
    Decimal,
    Float,
    NumericHex,
    StringConst,
    Date,
    ListSeparator,
    LeftParen,
    RightParen,
    ZeroOp,
    UnaryOp,
    BinaryOp,
    TernaryOp,
    Child,
    Parent,
    Dot,
    Parameter,
    Unknown,
    EOF,
  }
}
