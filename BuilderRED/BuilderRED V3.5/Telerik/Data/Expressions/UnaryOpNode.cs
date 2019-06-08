// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.UnaryOpNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.Data.Expressions
{
  internal class UnaryOpNode : ExpressionNode
  {
    private Operator op;
    private ExpressionNode right;

    public ExpressionNode Right
    {
      get
      {
        return this.right;
      }
      set
      {
        this.right = value;
      }
    }

    public override bool IsConst
    {
      get
      {
        return this.right.IsConst;
      }
    }

    public UnaryOpNode(Operator op, ExpressionNode right)
    {
      this.op = op;
      this.right = right;
    }

    public override object Eval(object row, object context)
    {
      return ((Operator.UnaryFunc) this.op.Func)(new Operand(this.right, row, context), new OperatorContext(row, (IFormatProvider) this.Culture, context));
    }

    public override string ToString()
    {
      return string.Format("UnaryOp: {0} ({1})", (object) this.op.Name, (object) this.right);
    }

    public override IEnumerable<ExpressionNode> GetChildNodes()
    {
      if (this.right != null)
        yield return this.right;
    }

    public Operator Op
    {
      get
      {
        return this.op;
      }
    }
  }
}
