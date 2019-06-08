// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.BinaryOpNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Data.Expressions
{
  internal class BinaryOpNode : ExpressionNode
  {
    protected ExpressionNode left;
    protected ExpressionNode right;
    protected Operator op;
    protected CompareOptions compareOptions;
    private Operand leftOperand;
    private Operand rightOperand;
    private uint leftOpVersion;
    private uint rightOpVersion;

    public ExpressionNode Left
    {
      get
      {
        return this.left;
      }
      set
      {
        this.left = value;
      }
    }

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

    public Operator Op
    {
      get
      {
        return this.op;
      }
    }

    public override bool IsConst
    {
      get
      {
        if (this.left.IsConst)
          return this.right.IsConst;
        return false;
      }
    }

    public BinaryOpNode(Operator op, ExpressionNode left, ExpressionNode right)
    {
      this.op = op;
      this.left = left;
      this.right = right;
    }

    public BinaryOpNode(
      Operator op,
      ExpressionNode left,
      ExpressionNode right,
      CompareOptions compareOptions)
    {
      this.op = op;
      this.left = left;
      this.right = right;
      this.compareOptions = compareOptions;
    }

    public Operand GetLeftOperand(object row, object context)
    {
      ExpressionContext expressionContext = context as ExpressionContext;
      if (this.leftOperand == null || this.leftOperand.Row != row || this.leftOperand.ExpressionContext != context || expressionContext != null && (int) expressionContext.Version != (int) this.leftOpVersion)
      {
        this.leftOperand = new Operand(this.left, row, context);
        this.leftOpVersion = expressionContext != null ? expressionContext.Version : 0U;
      }
      return this.leftOperand;
    }

    public Operand GetRightOperand(object row, object context)
    {
      ExpressionContext expressionContext = context as ExpressionContext;
      if (this.rightOperand == null || this.rightOperand.Row != row || this.rightOperand.ExpressionContext != context || expressionContext != null && (int) expressionContext.Version != (int) this.rightOpVersion)
      {
        this.rightOperand = new Operand(this.right, row, context);
        this.rightOpVersion = expressionContext != null ? expressionContext.Version : 0U;
      }
      return this.rightOperand;
    }

    public override object Eval(object row, object context)
    {
      return ((Operator.BinaryFunc) this.op.Func)(this.GetLeftOperand(row, context), this.GetRightOperand(row, context), new OperatorContext(row, (IFormatProvider) this.Culture, context, this.compareOptions));
    }

    public override string ToString()
    {
      return string.Format("BinaryOp {0} ({1}, {2})", (object) this.op.Name, (object) this.left, (object) this.right);
    }

    public override IEnumerable<ExpressionNode> GetChildNodes()
    {
      if (this.left != null)
        yield return this.left;
      if (this.right != null)
        yield return this.right;
    }
  }
}
