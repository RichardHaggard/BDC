// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.Operand
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Data.Expressions
{
  public sealed class Operand
  {
    private ExpressionNode node;
    private object row;
    private object value;
    private object expressionContext;
    private bool hasValue;

    public object ExpressionContext
    {
      get
      {
        return this.expressionContext;
      }
    }

    public object Row
    {
      get
      {
        return this.row;
      }
    }

    public object Value
    {
      get
      {
        if (!this.hasValue)
        {
          this.value = this.node.Eval(this.row, this.expressionContext);
          this.hasValue = true;
        }
        return this.value;
      }
    }

    public object ForceEvaluate()
    {
      if (!this.hasValue)
      {
        this.value = this.node.Eval(this.row, this.expressionContext);
        this.hasValue = true;
      }
      return this.value;
    }

    public bool HasValue
    {
      get
      {
        return this.hasValue;
      }
    }

    public bool IsConst
    {
      get
      {
        return this.node.IsConst;
      }
    }

    public ExpressionNode Node
    {
      get
      {
        return this.node;
      }
    }

    public Operand(ExpressionNode node, object row, object expressionContext)
    {
      this.node = node;
      this.row = row;
      this.value = (object) null;
      this.expressionContext = expressionContext;
    }
  }
}
