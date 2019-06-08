// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Telerik.Data.Expressions;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.Data
{
  public class FilterDescriptor : INotifyPropertyChanged, INotifyPropertyChangingEx, ICloneable
  {
    private FilterOperator filterOperator = FilterOperator.Contains;
    private string propertyName;
    private bool isFilterEditor;
    private object value;

    public FilterDescriptor()
    {
    }

    public FilterDescriptor(string propertyName, FilterOperator filterOperator, object value)
    {
      this.propertyName = propertyName;
      this.filterOperator = filterOperator;
      this.value = value;
    }

    [DefaultValue(null)]
    [Browsable(true)]
    public virtual string PropertyName
    {
      get
      {
        return this.propertyName;
      }
      set
      {
        if (!(this.propertyName != value) || !this.OnPropertyChanging(nameof (PropertyName), (object) this.propertyName, (object) value))
          return;
        this.propertyName = value;
        this.OnPropertyChanged(nameof (PropertyName));
      }
    }

    [Browsable(true)]
    [DefaultValue(FilterOperator.Contains)]
    public virtual FilterOperator Operator
    {
      get
      {
        return this.filterOperator;
      }
      set
      {
        if (this.filterOperator == value)
          return;
        PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(nameof (Operator), this.value, (object) value);
        this.OnPropertyChanging(e);
        if (e.Cancel)
          return;
        this.filterOperator = (FilterOperator) e.NewValue;
        this.OnPropertyChanged(nameof (Operator));
      }
    }

    [DefaultValue(null)]
    [TypeConverter(typeof (FilterValueStringConverter))]
    public virtual object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        if (object.Equals(this.value, value))
          return;
        PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(nameof (Value), this.value, value);
        this.OnPropertyChanging(e);
        if (e.Cancel)
          return;
        this.value = e.NewValue;
        this.OnPropertyChanged(nameof (Value));
      }
    }

    [Browsable(true)]
    public virtual string Expression
    {
      get
      {
        if (string.IsNullOrEmpty(this.PropertyName))
          return string.Empty;
        return FilterDescriptor.GetExpression(this);
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    public virtual bool IsFilterEditor
    {
      get
      {
        return this.isFilterEditor;
      }
      set
      {
        if (value == this.isFilterEditor || !this.OnPropertyChanging(nameof (IsFilterEditor), (object) this.isFilterEditor, (object) value))
          return;
        this.isFilterEditor = value;
        this.OnPropertyChanged(nameof (IsFilterEditor));
      }
    }

    private static object GetExpressionValue(
      FilterDescriptor filterDescriptor,
      Function<FilterDescriptor, object> formatValue)
    {
      object obj1 = filterDescriptor.Value;
      if (!(obj1 is string) && obj1 is IEnumerable)
      {
        IEnumerable enumerable = obj1 as IEnumerable;
        List<object> objectList = new List<object>();
        foreach (object obj2 in enumerable)
        {
          FilterDescriptor filterDescriptor1 = filterDescriptor.Clone() as FilterDescriptor;
          filterDescriptor1.Value = obj2;
          object expressionValue = FilterDescriptor.GetExpressionValue(filterDescriptor1, formatValue);
          objectList.Add(expressionValue);
        }
        return (object) objectList.ToArray();
      }
      object obj3 = formatValue != null ? formatValue(filterDescriptor) : filterDescriptor.Value;
      object obj4;
      if (obj3 is string || obj3 is char || (obj3 is Guid || obj3 is TimeSpan))
        obj4 = (object) ("'" + DataStorageHelper.EscapeValue(Convert.ToString(obj3)) + "'");
      else if (obj3 is Enum)
        obj4 = (object) Convert.ToInt32(obj3);
      else if (obj3 is DateTime)
        obj4 = (object) string.Format((IFormatProvider) CultureInfo.InvariantCulture, "#{0}#", obj3);
      else
        obj4 = !(obj3 is double) ? (!(obj3 is Color) ? (obj3 is short || obj3 is int || (obj3 is long || obj3 is Decimal) || (obj3 is float || obj3 is double || obj3 is bool) ? (object) Convert.ToString(filterDescriptor.Value, (IFormatProvider) CultureInfo.InvariantCulture) : (object) ("'" + DataStorageHelper.EscapeValue(Convert.ToString(filterDescriptor.Value, (IFormatProvider) CultureInfo.InvariantCulture)) + "'")) : (object) ((Color) obj3).ToArgb()) : (object) ((double) obj3).ToString("G17", (IFormatProvider) CultureInfo.InvariantCulture);
      return obj4;
    }

    public static string GetExpression(
      FilterDescriptor filterDescriptor,
      Function<FilterDescriptor, object> formatValue)
    {
      if (string.IsNullOrEmpty(filterDescriptor.PropertyName) || filterDescriptor.filterOperator == FilterOperator.None || filterDescriptor.Operator != FilterOperator.IsNotNull && filterDescriptor.Operator != FilterOperator.IsNull && (filterDescriptor.Operator != FilterOperator.IsEqualTo && filterDescriptor.Operator != FilterOperator.IsNotEqualTo) && filterDescriptor.value == null)
        return string.Empty;
      FilterOperator filterOperator = filterDescriptor.Operator;
      object obj = FilterDescriptor.GetExpressionValue(filterDescriptor, formatValue);
      if (filterDescriptor.value == null || filterDescriptor.value == DBNull.Value)
      {
        if (filterOperator == FilterOperator.IsEqualTo)
          filterOperator = FilterOperator.IsNull;
        if (filterOperator == FilterOperator.IsNotEqualTo)
          filterOperator = FilterOperator.IsNotNull;
      }
      if (filterOperator == FilterOperator.IsContainedIn || filterOperator == FilterOperator.IsNotContainedIn)
      {
        IEnumerable enumebrable = obj as IEnumerable;
        if (enumebrable != null && obj is string)
        {
          enumebrable = (IEnumerable) obj.ToString().Trim('\'').Split(new char[2]
          {
            ',',
            ' '
          }, StringSplitOptions.RemoveEmptyEntries);
          obj = (object) DataStorageHelper.ParseEnumerbale(enumebrable);
        }
        if (!(obj is string) && enumebrable != null)
          obj = (object) DataStorageHelper.ParseEnumerbale(enumebrable);
      }
      string empty = string.Empty;
      string str1 = DataStorageHelper.EscapeName(filterDescriptor.PropertyName);
      switch (filterOperator)
      {
        case FilterOperator.IsLike:
          return string.Format("{0} LIKE {1}", (object) str1, obj);
        case FilterOperator.IsNotLike:
          return string.Format("{0} NOT LIKE {1}", (object) str1, obj);
        case FilterOperator.IsLessThan:
          return string.Format("{0} < {1}", (object) str1, obj);
        case FilterOperator.IsLessThanOrEqualTo:
          return string.Format("{0} <= {1}", (object) str1, obj);
        case FilterOperator.IsEqualTo:
          return string.Format("{0} = {1}", (object) str1, obj);
        case FilterOperator.IsNotEqualTo:
          return string.Format("{0} <> {1}", (object) str1, obj);
        case FilterOperator.IsGreaterThanOrEqualTo:
          return string.Format("{0} >= {1}", (object) str1, obj);
        case FilterOperator.IsGreaterThan:
          return string.Format("{0} > {1}", (object) str1, obj);
        case FilterOperator.StartsWith:
          string str2 = DataStorageHelper.EscapeLikeValue(Convert.ToString(filterDescriptor.Value, (IFormatProvider) CultureInfo.InvariantCulture));
          return string.Format("{0} LIKE '{1}%'", (object) str1, (object) str2);
        case FilterOperator.EndsWith:
          string str3 = DataStorageHelper.EscapeLikeValue(Convert.ToString(filterDescriptor.Value, (IFormatProvider) CultureInfo.InvariantCulture));
          return string.Format("{0} LIKE '%{1}'", (object) str1, (object) str3);
        case FilterOperator.Contains:
          string str4 = DataStorageHelper.EscapeLikeValue(Convert.ToString(filterDescriptor.Value, (IFormatProvider) CultureInfo.InvariantCulture));
          return string.Format("{0} LIKE '%{1}%'", (object) str1, (object) str4);
        case FilterOperator.NotContains:
          string str5 = DataStorageHelper.EscapeLikeValue(Convert.ToString(filterDescriptor.Value, (IFormatProvider) CultureInfo.InvariantCulture));
          return string.Format("{0} NOT LIKE '%{1}%'", (object) str1, (object) str5);
        case FilterOperator.IsNull:
          return string.Format("{0} IS NULL", (object) str1);
        case FilterOperator.IsNotNull:
          return string.Format("NOT ({0} IS NULL)", (object) str1);
        case FilterOperator.IsContainedIn:
          return string.Format("{0} IN ({1})", (object) str1, obj);
        case FilterOperator.IsNotContainedIn:
          return string.Format("{0} NOT IN ({1})", (object) str1, obj);
        default:
          return string.Empty;
      }
    }

    public static string GetExpression(FilterDescriptor filterDescriptor)
    {
      return FilterDescriptor.GetExpression(filterDescriptor, (Function<FilterDescriptor, object>) null);
    }

    public override string ToString()
    {
      return this.Expression;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    public event PropertyChangingEventHandlerEx PropertyChanging;

    protected bool OnPropertyChanging(string propertyName, object oldValue, object newValue)
    {
      PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(propertyName, oldValue, newValue);
      this.OnPropertyChanging(e);
      return !e.Cancel;
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgsEx e)
    {
      PropertyChangingEventHandlerEx propertyChanging = this.PropertyChanging;
      if (propertyChanging == null)
        return;
      propertyChanging((object) this, e);
    }

    public virtual object Clone()
    {
      return (object) new FilterDescriptor(this.propertyName, this.filterOperator, this.value);
    }
  }
}
