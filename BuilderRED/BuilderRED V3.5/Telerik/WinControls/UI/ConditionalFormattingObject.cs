// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ConditionalFormattingObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  [Browsable(false)]
  public class ConditionalFormattingObject : BaseFormattingObject
  {
    private ConditionTypes conditionType = ConditionTypes.Equal;
    private string tValue1 = "";
    private string tValue2 = "";
    private bool caseSensitive;
    private TypeConverter colorConverter;

    public ConditionalFormattingObject()
      : this("NewCondition", ConditionTypes.Equal, string.Empty, string.Empty, false)
    {
    }

    public ConditionalFormattingObject(
      string name,
      ConditionTypes type,
      string tvalue1,
      string tvalue2,
      bool applyToRow)
      : base(name, applyToRow)
    {
      this.conditionType = type;
      this.colorConverter = TypeDescriptor.GetConverter(typeof (Color));
      this.tValue1 = tvalue1;
      this.tValue2 = tvalue2;
    }

    [DefaultValue(ConditionTypes.Equal)]
    public ConditionTypes ConditionType
    {
      get
      {
        return this.conditionType;
      }
      set
      {
        if (this.conditionType == value)
          return;
        this.conditionType = value;
        this.OnPropertyChanged(nameof (ConditionType));
      }
    }

    [DefaultValue("")]
    public string TValue1
    {
      get
      {
        return this.tValue1;
      }
      set
      {
        if (!(this.tValue1 != value))
          return;
        this.tValue1 = value;
        this.OnPropertyChanged(nameof (TValue1));
      }
    }

    [DefaultValue("")]
    public string TValue2
    {
      get
      {
        return this.tValue2;
      }
      set
      {
        if (!(this.tValue2 != value))
          return;
        this.tValue2 = value;
        this.OnPropertyChanged(nameof (TValue2));
      }
    }

    [DefaultValue(false)]
    public bool CaseSensitive
    {
      get
      {
        return this.caseSensitive;
      }
      set
      {
        if (this.caseSensitive == value)
          return;
        this.caseSensitive = value;
        this.OnPropertyChanged(nameof (CaseSensitive));
      }
    }

    public override bool Evaluate(GridViewRowInfo row, GridViewColumn column)
    {
      if (!this.Enabled || row.Cells.Count == 0)
        return false;
      object cellValue = row.Cells[column.Name].Value;
      if (cellValue != null)
      {
        if ((object) cellValue.GetType() == (object) typeof (DateTime))
        {
          switch (this.conditionType)
          {
            case ConditionTypes.StartsWith:
              return ((DateTime) cellValue).ToString().StartsWith(this.tValue1);
            case ConditionTypes.EndsWith:
              return ((DateTime) cellValue).ToString().EndsWith(this.tValue1);
            case ConditionTypes.Contains:
              return ((DateTime) cellValue).ToString().Contains(this.tValue1);
            case ConditionTypes.DoesNotContain:
              return !((DateTime) cellValue).ToString().Contains(this.tValue1);
          }
        }
        else if ((object) cellValue.GetType() == (object) typeof (Color))
          cellValue = (object) this.colorConverter.ConvertToString(cellValue);
      }
      switch (this.conditionType)
      {
        case ConditionTypes.None:
          return false;
        case ConditionTypes.Equal:
          return this.CompareValues(cellValue, this.tValue1) == 0;
        case ConditionTypes.NotEqual:
          return this.CompareValues(cellValue, this.tValue1) != 0;
        case ConditionTypes.StartsWith:
        case ConditionTypes.EndsWith:
        case ConditionTypes.Contains:
        case ConditionTypes.DoesNotContain:
          return this.StringCompare(cellValue, this.tValue1, this.conditionType);
        case ConditionTypes.Greater:
          return this.CompareValues(cellValue, this.tValue1) > 0;
        case ConditionTypes.GreaterOrEqual:
          return this.CompareValues(cellValue, this.tValue1) >= 0;
        case ConditionTypes.Less:
          return this.CompareValues(cellValue, this.tValue1) < 0;
        case ConditionTypes.LessOrEqual:
          return this.CompareValues(cellValue, this.tValue1) <= 0;
        case ConditionTypes.Between:
          if (this.CompareValues(cellValue, this.tValue1) > 0)
            return this.CompareValues(cellValue, this.tValue2) < 0;
          return false;
        case ConditionTypes.NotBetween:
          if (this.CompareValues(cellValue, this.tValue1) >= 0)
            return this.CompareValues(cellValue, this.tValue2) > 0;
          return true;
        default:
          return false;
      }
    }

    public override object Clone()
    {
      return (object) ReflectionHelper.Clone<ConditionalFormattingObject>(this);
    }

    public override void Copy(BaseFormattingObject source)
    {
      ConditionalFormattingObject source1 = source as ConditionalFormattingObject;
      if (source1 != null)
      {
        ReflectionHelper.CopyFields<ConditionalFormattingObject>(this, source1);
        this.OnPropertyChanged("CellBackColor");
      }
      else
        base.Copy(source);
    }

    private bool StringCompare(object cellValue, string expression, ConditionTypes condition)
    {
      if (cellValue is DBNull || cellValue == null)
        return expression == null;
      if (expression == null)
        return cellValue == null;
      string lower = cellValue.ToString();
      string str = expression;
      if (!this.caseSensitive)
      {
        lower = lower.ToLower();
        str = str.ToLower();
      }
      bool flag = false;
      switch (condition)
      {
        case ConditionTypes.StartsWith:
          flag = lower.StartsWith(str);
          break;
        case ConditionTypes.EndsWith:
          flag = lower.EndsWith(str);
          break;
        case ConditionTypes.Contains:
          flag = lower.Contains(str);
          break;
        case ConditionTypes.DoesNotContain:
          flag = !lower.Contains(str);
          break;
      }
      return flag;
    }

    private int CompareValues(object cellValue, string expression)
    {
      if (cellValue is DBNull || cellValue == null)
        return expression != null ? -1 : 0;
      if (expression == null)
        return cellValue != null ? -1 : 0;
      if (cellValue is string)
        return string.Compare((string) cellValue, expression, !this.caseSensitive);
      Type type = cellValue.GetType();
      bool parsed;
      object obj = new ParsableValueType(expression, type).GetValue(out parsed);
      if (parsed && cellValue is IComparable)
        return ((IComparable) cellValue).CompareTo(obj);
      return !cellValue.Equals(obj) ? -1 : 0;
    }
  }
}
