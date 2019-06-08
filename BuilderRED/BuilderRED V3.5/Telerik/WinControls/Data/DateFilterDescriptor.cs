// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.DateFilterDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Globalization;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  public class DateFilterDescriptor : FilterDescriptor
  {
    private bool ignoreTimePart;

    public DateTime? Value
    {
      get
      {
        return (DateTime?) base.Value;
      }
      set
      {
        this.Value = (object) value;
      }
    }

    public override string Expression
    {
      get
      {
        if (string.IsNullOrEmpty(this.PropertyName))
          return string.Empty;
        return DateFilterDescriptor.GetExpression(this);
      }
    }

    public bool IgnoreTimePart
    {
      get
      {
        return this.ignoreTimePart;
      }
      set
      {
        this.ignoreTimePart = value;
      }
    }

    public DateFilterDescriptor()
    {
    }

    public DateFilterDescriptor(
      string propertyName,
      FilterOperator filterOperator,
      DateTime? value,
      bool ignoreTimePart)
    {
      this.PropertyName = propertyName;
      this.Operator = filterOperator;
      this.Value = (object) value;
      this.ignoreTimePart = ignoreTimePart;
    }

    public DateFilterDescriptor(
      string propertyName,
      FilterOperator filterOperator,
      DateTime? value)
      : this(propertyName, filterOperator, value, false)
    {
    }

    public static string GetExpression(DateFilterDescriptor dateTimeFilterDescriptor)
    {
      if (!dateTimeFilterDescriptor.IgnoreTimePart)
        return FilterDescriptor.GetExpression((FilterDescriptor) dateTimeFilterDescriptor);
      if (string.IsNullOrEmpty(dateTimeFilterDescriptor.PropertyName) || dateTimeFilterDescriptor.Operator == FilterOperator.None || dateTimeFilterDescriptor.Operator != FilterOperator.IsNotNull && dateTimeFilterDescriptor.Operator != FilterOperator.IsNull && !dateTimeFilterDescriptor.Value.HasValue)
        return string.Empty;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = DataStorageHelper.EscapeName(dateTimeFilterDescriptor.PropertyName);
      if (dateTimeFilterDescriptor.Value.HasValue)
      {
        str1 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "#{0}#", (object) dateTimeFilterDescriptor.Value.Value.Date);
        str2 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "#{0}#", (object) dateTimeFilterDescriptor.Value.Value.Date.AddDays(1.0));
      }
      switch (dateTimeFilterDescriptor.Operator)
      {
        case FilterOperator.None:
          return string.Empty;
        case FilterOperator.IsLike:
        case FilterOperator.IsEqualTo:
          return string.Format("{0} >= {1} AND {0} < {2}", (object) str3, (object) str1, (object) str2);
        case FilterOperator.IsNotLike:
        case FilterOperator.IsNotEqualTo:
          return string.Format("{0} < {1} OR {0} >= {2}", (object) str3, (object) str1, (object) str2);
        case FilterOperator.IsLessThan:
          return string.Format("{0} < {1}", (object) str3, (object) str1);
        case FilterOperator.IsLessThanOrEqualTo:
          return string.Format("{0} < {1}", (object) str3, (object) str2);
        case FilterOperator.IsGreaterThanOrEqualTo:
          return string.Format("{0} >= {1}", (object) str3, (object) str1);
        case FilterOperator.IsGreaterThan:
          return string.Format("{0} >= {1}", (object) str3, (object) str2);
        case FilterOperator.IsNull:
          return string.Format("{0} IS NULL", (object) str3);
        case FilterOperator.IsNotNull:
          return string.Format("NOT ({0} IS NULL)", (object) str3);
        default:
          return string.Empty;
      }
    }

    public override string ToString()
    {
      return DateFilterDescriptor.GetExpression(this);
    }

    public override object Clone()
    {
      return (object) new DateFilterDescriptor(this.PropertyName, this.Operator, this.Value, this.IgnoreTimePart);
    }
  }
}
