﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterDescriptorItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (DataFilterPropertyDescriptorItemTypeConverter))]
  [Serializable]
  public class DataFilterDescriptorItem : RadItem
  {
    private string descriptorName;
    private Type descriptorType;
    private FilterOperator defaultFilterOperator;
    private IList<FilterOperator> filterOperators;
    private IList<FilterOperator> defaultFilterOperators;
    private object defaultValue;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public DataFilterDescriptorItem()
    {
    }

    public DataFilterDescriptorItem(string descriptorName, Type descriptorType)
    {
      this.DescriptorName = descriptorName;
      this.DescriptorType = descriptorType;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.descriptorName = "DefaultName";
      this.descriptorType = typeof (string);
      this.defaultFilterOperator = FilterOperator.None;
      this.filterOperators = DataFilterOperatorContext.GetFilterOperators(this.descriptorType);
      this.defaultFilterOperators = (IList<FilterOperator>) new List<FilterOperator>((IEnumerable<FilterOperator>) this.filterOperators);
      this.defaultValue = (object) string.Empty;
    }

    [System.ComponentModel.DefaultValue("DefaultName")]
    public string DescriptorName
    {
      get
      {
        return this.descriptorName;
      }
      set
      {
        if (!(this.descriptorName != value))
          return;
        this.descriptorName = value;
        this.OnNotifyPropertyChanged(nameof (DescriptorName));
      }
    }

    [Editor(typeof (DataFilterDescriptorTypeEditor), typeof (UITypeEditor))]
    [System.ComponentModel.DefaultValue(typeof (Type), "System.String")]
    public Type DescriptorType
    {
      get
      {
        return this.descriptorType;
      }
      set
      {
        if ((object) this.descriptorType == (object) value)
          return;
        this.descriptorType = value;
        this.OnDescriptorTypeChanged();
        this.OnNotifyPropertyChanged(nameof (DescriptorType));
      }
    }

    [System.ComponentModel.DefaultValue(false)]
    public bool IsAutoGenerated { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual FilterOperator DefaultFilterOperator
    {
      get
      {
        return this.ValidateDefaultOperator();
      }
      set
      {
        if (this.defaultFilterOperator == value)
          return;
        if (!this.defaultFilterOperators.Contains(value))
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (FilterOperator defaultFilterOperator in (IEnumerable<FilterOperator>) this.defaultFilterOperators)
            stringBuilder.Append(((int) defaultFilterOperator).ToString() + ", ");
          stringBuilder.Length -= 2;
          throw new ArgumentException(string.Format("DefaultFilterOperator does not match current descriptor type. Please use one of the following: {0}", (object) stringBuilder));
        }
        this.defaultFilterOperator = value;
        this.OnNotifyPropertyChanged(nameof (DefaultFilterOperator));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object DefaultValue
    {
      get
      {
        return this.defaultValue;
      }
      set
      {
        if (this.defaultValue == value)
          return;
        this.defaultValue = value;
        this.OnNotifyPropertyChanged(nameof (DefaultValue));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IList<FilterOperator> FilterOperators
    {
      get
      {
        return this.filterOperators;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal IList<DataFilterOperatorContext> FilterOperationContext
    {
      get
      {
        return DataFilterOperatorContext.GetContextFromOperators(this.filterOperators);
      }
    }

    public override string ToString()
    {
      return string.Format("{0}: {1}, {2}.", (object) this.GetType().Name, (object) this.DescriptorName, (object) this.DescriptorType);
    }

    protected virtual object GetDefaultDescriptorValue()
    {
      if ((object) this.DescriptorType == null)
        return (object) null;
      if (this.DescriptorType.IsEnum)
      {
        Array values = Enum.GetValues(this.DescriptorType);
        if (values.Length > 0)
          return values.GetValue(0);
      }
      else
      {
        if (TelerikHelper.IsNumericType(this.DescriptorType))
          return (object) 0;
        if ((object) this.DescriptorType == (object) typeof (string))
          return (object) string.Empty;
        if ((object) this.DescriptorType == (object) typeof (DateTime) || (object) this.DescriptorType == (object) typeof (DateTime?))
          return (object) DateTime.Now;
        if ((object) this.DescriptorType == (object) typeof (Color))
          return (object) Color.Empty;
        if ((object) this.DescriptorType == (object) typeof (bool))
          return (object) false;
      }
      return (object) null;
    }

    private void OnDescriptorTypeChanged()
    {
      this.filterOperators = DataFilterOperatorContext.GetFilterOperators(this.DescriptorType);
      this.defaultFilterOperators = (IList<FilterOperator>) new List<FilterOperator>((IEnumerable<FilterOperator>) this.filterOperators);
      this.defaultFilterOperator = this.GetDefaultFilterOperator(this.DescriptorType);
      this.defaultValue = this.GetDefaultDescriptorValue();
    }

    private FilterOperator ValidateDefaultOperator()
    {
      if (!this.filterOperators.Contains(this.defaultFilterOperator))
        return this.filterOperators[0];
      return this.defaultFilterOperator;
    }

    protected virtual FilterOperator GetDefaultFilterOperator(Type dataType)
    {
      return FilterOperator.None;
    }

    internal FilterOperator GetFilterOperatorByText(string filterOperatorText)
    {
      foreach (DataFilterOperatorContext filterOperatorContext in (IEnumerable<DataFilterOperatorContext>) this.FilterOperationContext)
      {
        if (filterOperatorContext.Name == filterOperatorText)
          return filterOperatorContext.Operator;
      }
      return this.DefaultFilterOperator;
    }
  }
}