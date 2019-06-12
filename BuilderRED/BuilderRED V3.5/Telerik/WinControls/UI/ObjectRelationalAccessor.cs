// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ObjectRelationalAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class ObjectRelationalAccessor : Accessor
  {
    private Dictionary<Type, PropertyDescriptorCollection> propertyDescriptors;

    public ObjectRelationalAccessor(GridViewColumn column)
      : base(column)
    {
    }

    public override object this[GridViewRowInfo row]
    {
      get
      {
        return row.Cache[this.Column];
      }
      set
      {
        row.Cache[this.Column] = value;
        if (row.DataBoundItem != null && this.Template.AutoUpdateObjectRelationalSource)
          this.SetBoundValue((IDataItem) row, this.Column.FieldName, value);
        row.ViewTemplate.RefreshAggregates(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) row, row.Index));
      }
    }

    private void SetBoundValue(IDataItem dataItem, string propertyName, object value)
    {
      object dataBoundItem = dataItem.DataBoundItem;
      if (dataBoundItem == null)
        return;
      Type type = dataBoundItem.GetType();
      if (this.propertyDescriptors == null)
        this.propertyDescriptors = new Dictionary<Type, PropertyDescriptorCollection>();
      if (!this.propertyDescriptors.ContainsKey(type))
        this.propertyDescriptors.Add(type, TypeDescriptor.GetProperties(dataBoundItem));
      PropertyDescriptor propertyDescriptor = this.propertyDescriptors[type].Find(propertyName, !this.Template.ListSource.UseCaseSensitiveFieldNames);
      if (propertyDescriptor == null)
      {
        this.propertyDescriptors[type] = TypeDescriptor.GetProperties(dataBoundItem);
        propertyDescriptor = this.propertyDescriptors[type].Find(propertyName, !this.Template.ListSource.UseCaseSensitiveFieldNames);
      }
      if (propertyDescriptor == null)
        return;
      GridViewObjectRelationalDataProvider hierarchyDataProvider = this.Template.HierarchyDataProvider as GridViewObjectRelationalDataProvider;
      hierarchyDataProvider?.SuspendNotifications();
      if (value == null)
      {
        try
        {
          propertyDescriptor.SetValue(dataBoundItem, value);
        }
        catch
        {
          propertyDescriptor.SetValue(dataBoundItem, (object) DBNull.Value);
        }
      }
      else
      {
        Type underlyingType = Nullable.GetUnderlyingType(propertyDescriptor.PropertyType);
        if (propertyDescriptor.Converter != null && (object) underlyingType != null && underlyingType.IsGenericType)
          value = propertyDescriptor.Converter.ConvertFromInvariantString(value.ToString());
        propertyDescriptor.SetValue(dataBoundItem, value);
      }
      hierarchyDataProvider?.ResumeNotifications();
    }
  }
}
