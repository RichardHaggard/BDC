// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.TernaryOpNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Data.Expressions
{
  internal class TernaryOpNode : ExpressionNode
  {
    private Operator @operator;
    private ExpressionNode[] operands;

    public TernaryOpNode(
      Operator @operator,
      ExpressionNode op1,
      ExpressionNode op2,
      ExpressionNode op3)
    {
      this.@operator = @operator;
      this.operands = new ExpressionNode[3]{ op1, op2, op3 };
    }

    public override object Eval(object row, object context)
    {
      return ((Operator.TernaryFunc) this.@operator.Func)(new Operand(this.operands[0], row, context), new Operand(this.operands[1], row, context), new Operand(this.operands[2], row, context), new OperatorContext(row, (IFormatProvider) this.Culture, context));
    }
  }
}
