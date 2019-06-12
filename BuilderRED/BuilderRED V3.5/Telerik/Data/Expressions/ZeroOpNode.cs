// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ZeroOpNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Data.Expressions
{
  internal class ZeroOpNode : ExpressionNode
  {
    private Operator op;

    public override bool IsConst
    {
      get
      {
        return true;
      }
    }

    public Operator Operator
    {
      get
      {
        return this.op;
      }
    }

    public ZeroOpNode(Operator op)
    {
      this.op = op;
    }

    public override object Eval(object row, object context)
    {
      return ((Operator.ZeroFunc) this.op.Func)();
    }

    public override string ToString()
    {
      return "ZeroOp: " + (object) this.op;
    }
  }
}
