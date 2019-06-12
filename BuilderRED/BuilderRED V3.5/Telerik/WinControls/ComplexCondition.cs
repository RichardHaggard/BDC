// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ComplexCondition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class ComplexCondition : Condition
  {
    private Condition condition1;
    private Condition condition2;
    private BinaryOperator binaryOperator;

    public Condition Condition1
    {
      get
      {
        return this.condition1;
      }
      set
      {
        this.condition1 = value;
      }
    }

    public BinaryOperator BinaryOperator
    {
      get
      {
        return this.binaryOperator;
      }
      set
      {
        this.binaryOperator = value;
      }
    }

    public Condition Condition2
    {
      get
      {
        return this.condition2;
      }
      set
      {
        this.condition2 = value;
      }
    }

    public ComplexCondition()
    {
    }

    public ComplexCondition(
      Condition condition1,
      BinaryOperator binaryOperator,
      Condition condition2)
    {
      this.condition1 = condition1;
      this.binaryOperator = binaryOperator;
      this.condition2 = condition2;
    }

    public override bool Evaluate(RadObject target)
    {
      if (target == null)
        return false;
      switch (this.BinaryOperator)
      {
        case BinaryOperator.AndOperator:
          if (this.Condition1.Evaluate(target))
            return this.Condition2.Evaluate(target);
          return false;
        case BinaryOperator.OrOperator:
          if (!this.Condition1.Evaluate(target))
            return this.Condition2.Evaluate(target);
          return true;
        case BinaryOperator.XorOperator:
          return this.Condition1.Evaluate(target) != this.Condition2.Evaluate(target);
        default:
          return false;
      }
    }

    protected override void FillAffectedProperties(List<RadProperty> inList)
    {
      inList.AddRange((IEnumerable<RadProperty>) this.Condition1.AffectedProperties);
      inList.AddRange((IEnumerable<RadProperty>) this.Condition2.AffectedProperties);
    }

    public override string ToString()
    {
      if (this.Condition1 == null || this.Condition2 == null)
        return base.ToString();
      return "(" + this.Condition1.ToString() + " " + this.BinaryOperator.ToString() + " " + this.Condition2.ToString() + ")";
    }
  }
}
