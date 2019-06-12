// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BoundAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Data;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class BoundAccessor : Accessor
  {
    private bool raisingException;

    public BoundAccessor(GridViewColumn column)
      : base(column)
    {
      if (this.Template == null)
        return;
      this.subPropertyMode = GridViewHelper.ContainsInnerDescriptor(this.Template.ListSource.BoundProperties, column.FieldName);
    }

    public override object this[GridViewRowInfo row]
    {
      get
      {
        try
        {
          if (row.DataBoundItem == null)
            return (object) null;
          if (this.subPropertyMode)
            return this.GetSubPropertyValue(this.Column.FieldName, row.DataBoundItem);
          if (string.IsNullOrEmpty(this.Column.FieldName))
            return (object) null;
          return this.Template.ListSource.GetBoundValue(row.DataBoundItem, this.Column.FieldName);
        }
        catch (RowNotInTableException ex)
        {
          return (object) null;
        }
        catch (ArgumentException ex)
        {
          return (object) null;
        }
        catch (Exception ex)
        {
          this.RaiseError(ex, GridViewDataErrorContexts.Display);
          return (object) null;
        }
      }
      set
      {
        try
        {
          if (row.DataBoundItem == null)
            return;
          if (this.subPropertyMode)
            this.Template.ListSource.SetBoundValue((IDataItem) row, this.Column.Name, value, this.Column.FieldName);
          else
            this.Template.ListSource.SetBoundValue((IDataItem) row, this.Column.FieldName, this.Column.Name, value, (string) null);
        }
        catch (Exception ex)
        {
          this.Template.SetError(new GridViewCellCancelEventArgs(row, this.Column, (IInputEditor) null), ex);
        }
      }
    }

    private void RaiseError(Exception ex, GridViewDataErrorContexts context)
    {
      if (this.raisingException)
        return;
      this.raisingException = true;
      if (this.Template == null)
        throw new NullReferenceException("The column is not attached to a template! Set the OwnerTemplate property to a valid template.");
      int rowIndex = -1;
      if (this.Template.MasterViewInfo.CurrentRow != null)
        rowIndex = this.Template.MasterViewInfo.CurrentRow.Index;
      this.Template.EventDispatcher.RaiseEvent<GridViewDataErrorEventArgs>(EventDispatcher.DataError, (object) this.Template, new GridViewDataErrorEventArgs(ex, this.Column.Index, rowIndex, context));
      this.raisingException = false;
    }

    private void GetSubPropertyByPath(
      string propertyPath,
      object dataObject,
      out PropertyDescriptor innerDescriptor,
      out object innerObject)
    {
      string[] strArray = propertyPath.Split('.');
      innerDescriptor = this.Template.ListSource.BoundProperties[strArray[0]];
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
