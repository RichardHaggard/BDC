// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterPredicate
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Data
{
  public class FilterPredicate : NotifyPropertyBase
  {
    private GridKnownFunction function = GridKnownFunction.EqualTo;
    private ArrayList values = new ArrayList(2);
    private const int maxParameters = 2;
    private FilterExpression.BinaryOperation binaryOperator;
    private FilterFunction filterFunction;

    public FilterPredicate()
    {
      this.filterFunction = FilterFunction.GetFilterFunction(this.function);
    }

    public FilterPredicate(
      FilterExpression.BinaryOperation binaryOperator,
      GridKnownFunction function)
    {
      this.binaryOperator = binaryOperator;
      this.function = function;
      this.filterFunction = FilterFunction.GetFilterFunction(this.function);
    }

    public FilterPredicate(
      FilterExpression.BinaryOperation binaryOperator,
      GridKnownFunction function,
      params object[] values)
      : this(binaryOperator, function)
    {
      if (values == null)
        return;
      for (int index = 0; index < Math.Min(2, values.Length); ++index)
        this.values.Add(values[index]);
    }

    [DefaultValue(GridKnownFunction.EqualTo)]
    public GridKnownFunction Function
    {
      get
      {
        return this.function;
      }
      set
      {
        this.SetProperty<GridKnownFunction>(nameof (Function), ref this.function, value);
      }
    }

    internal FilterFunction FilterFunction
    {
      get
      {
        return this.filterFunction;
      }
    }

    [Browsable(true)]
    [DefaultValue(FilterExpression.BinaryOperation.AND)]
    [Description("Gets or sets a value that indicates which binary operator will be used when concatenating multiple filter expressions.")]
    public FilterExpression.BinaryOperation BinaryOperator
    {
      get
      {
        return this.binaryOperator;
      }
      set
      {
        this.SetProperty<FilterExpression.BinaryOperation>(nameof (BinaryOperator), ref this.binaryOperator, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public ArrayList Values
    {
      get
      {
        return this.values;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsValid
    {
      get
      {
        if (this.function != GridKnownFunction.NoFilter)
          return this.values[0] != null;
        return false;
      }
    }

    public override bool Equals(object obj)
    {
      FilterPredicate filterPredicate = obj as FilterPredicate;
      if (filterPredicate == null || this.function != filterPredicate.function || (this.binaryOperator != filterPredicate.BinaryOperator || this.values.Count != filterPredicate.values.Count))
        return false;
      for (int index = 0; index < this.values.Count; ++index)
      {
        if (this.values[index] != filterPredicate.values[index])
          return false;
      }
      return true;
    }

    public override int GetHashCode()
    {
      return -1;
    }

    protected override void ProcessPropertyChanged(PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "Function":
          this.filterFunction = FilterFunction.GetFilterFunction(this.function);
          break;
      }
    }
  }
}
