// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridFilterComboBoxCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridFilterComboBoxCellElement : GridFilterCellElement, IDataConversionInfoProvider, ITypeDescriptorContext, IServiceProvider
  {
    public GridFilterComboBoxCellElement(GridViewDataColumn dataColumn, GridRowElement row)
      : base(dataColumn, row)
    {
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (GridFilterCellElement);
      }
    }

    protected GridViewComboBoxColumn ComboBoxColumnInfo
    {
      get
      {
        return this.ColumnInfo as GridViewComboBoxColumn;
      }
    }

    Type IDataConversionInfoProvider.DataType
    {
      get
      {
        return this.ComboBoxColumnInfo.FilteringMemberDataType;
      }
      set
      {
        this.DataColumnInfo.DataType = value;
      }
    }

    TypeConverter IDataConversionInfoProvider.DataTypeConverter
    {
      get
      {
        TypeConverter typeConverter = this.DataColumnInfo.DataTypeConverter;
        GridViewComboBoxColumn comboBoxColumnInfo = this.ComboBoxColumnInfo;
        if (comboBoxColumnInfo.FilteringMode == GridViewFilteringMode.DisplayMember)
          typeConverter = TypeDescriptor.GetConverter(comboBoxColumnInfo.DisplayMemberDataType);
        return typeConverter;
      }
      set
      {
        this.DataColumnInfo.DataTypeConverter = value;
      }
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      if (data is GridViewComboBoxColumn)
        return context is GridFilterRowElement;
      return false;
    }

    protected override void SetContentCore(object value)
    {
      if (this.ComboBoxColumnInfo == null)
        return;
      GridViewComboBoxColumn comboBoxColumnInfo = this.ComboBoxColumnInfo;
      if (comboBoxColumnInfo.HasLookupValue && comboBoxColumnInfo.FilteringMode != GridViewFilteringMode.DisplayMember)
        value = comboBoxColumnInfo.GetLookupValue(value);
      base.SetContentCore(value);
    }

    protected override RadDropDownMenu CreateFilterMenu(Type dataType)
    {
      dataType = this.ComboBoxColumnInfo.FilteringMemberDataType;
      return base.CreateFilterMenu(dataType);
    }

    IContainer ITypeDescriptorContext.Container
    {
      get
      {
        if (this.ElementTree == null)
          return (IContainer) null;
        IComponent control = (IComponent) this.ElementTree.Control;
        if (control != null)
        {
          ISite site = control.Site;
          if (site != null)
            return site.Container;
        }
        return (IContainer) null;
      }
    }

    object ITypeDescriptorContext.Instance
    {
      get
      {
        return (object) this;
      }
    }

    void ITypeDescriptorContext.OnComponentChanged()
    {
    }

    bool ITypeDescriptorContext.OnComponentChanging()
    {
      return true;
    }

    PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
    {
      get
      {
        return (PropertyDescriptor) null;
      }
    }

    object IServiceProvider.GetService(Type serviceType)
    {
      if ((object) serviceType == (object) typeof (GridFilterComboBoxCellElement))
        return (object) this;
      return (object) null;
    }
  }
}
