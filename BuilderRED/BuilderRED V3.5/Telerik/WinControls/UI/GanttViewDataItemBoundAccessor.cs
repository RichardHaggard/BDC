// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDataItemBoundAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class GanttViewDataItemBoundAccessor : GanttViewDataItemAccessor
  {
    public GanttViewDataItemBoundAccessor(GanttViewTextViewColumn column)
      : base(column)
    {
    }

    public override object this[GanttViewDataItem item]
    {
      get
      {
        if (((IDataItem) item).DataBoundItem == null)
          return (object) null;
        if (this.IsSubPropertyMode)
          return this.GetSubPropertyValue(this.Column.FieldName, ((IDataItem) item).DataBoundItem);
        if (string.IsNullOrEmpty(this.Column.FieldName))
          return (object) null;
        return this.GanttViewElement.BindingProvider.GetBoundValue(((IDataItem) item).DataBoundItem, this.Column.FieldName);
      }
      set
      {
        if (((IDataItem) item).DataBoundItem == null)
          return;
        if (this.IsSubPropertyMode)
          this.GanttViewElement.BindingProvider.SetBoundValue((IDataItem) item, this.Column.FieldName, value, this.Column.FieldName);
        else
          this.GanttViewElement.BindingProvider.SetBoundValue((IDataItem) item, this.Column.FieldName, this.Column.Name, value, (string) null);
      }
    }

    private void GetSubPropertyByPath(
      string propertyPath,
      object dataObject,
      out PropertyDescriptor innerDescriptor,
      out object innerObject)
    {
      string[] strArray = propertyPath.Split('.');
      innerDescriptor = this.GanttViewElement.BindingProvider.BoundProperties[strArray[0]];
      innerObject = innerDescriptor.GetValue(dataObject);
      for (int index = 1; index < strArray.Length && innerDescriptor != null; ++index)
      {
        innerDescriptor = innerDescriptor.GetChildProperties()[strArray[index]];
        if (index + 1 != strArray.Length)
          innerObject = innerDescriptor.GetValue(innerObject);
      }
    }

    private object GetSubPropertyValue(string propertyPath, object dataObject)
    {
      PropertyDescriptor innerDescriptor = (PropertyDescriptor) null;
      object innerObject = (object) null;
      this.GetSubPropertyByPath(propertyPath, dataObject, out innerDescriptor, out innerObject);
      if (innerObject == null || innerObject == DBNull.Value)
        return (object) null;
      return innerDescriptor?.GetValue(innerObject);
    }

    private void SetSubPropertyValue(string propertyPath, object dataObject, object value)
    {
      PropertyDescriptor innerDescriptor = (PropertyDescriptor) null;
      object innerObject = (object) null;
      this.GetSubPropertyByPath(propertyPath, dataObject, out innerDescriptor, out innerObject);
      if (innerObject == null || innerObject == DBNull.Value || innerDescriptor == null)
        return;
      innerDescriptor.SetValue(innerObject, value);
    }
  }
}
