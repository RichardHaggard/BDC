// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterDefaultSourceControlProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterDefaultSourceControlProvider : IDataFilterProvider
  {
    private PropertyDescriptorCollection boundProperties = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
    private object sourceControl;

    public DataFilterDefaultSourceControlProvider(object sourceControl)
    {
      this.SourceControl = sourceControl;
    }

    public object SourceControl
    {
      get
      {
        return this.sourceControl;
      }
      set
      {
        this.sourceControl = value;
        this.UpdateBoundProperties();
      }
    }

    public void UpdateBoundProperties()
    {
      object sourceControl = this.SourceControl;
      this.boundProperties = ListBindingHelper.GetListItemProperties(this.SourceControl);
      PropertyDescriptor[] propertyDescriptorArray = new PropertyDescriptor[this.boundProperties.Count];
      this.boundProperties.CopyTo((Array) propertyDescriptorArray, 0);
      foreach (PropertyDescriptor propertyDescriptor in propertyDescriptorArray)
      {
        if ((object) propertyDescriptor.PropertyType == (object) typeof (IBindingList))
          this.boundProperties.Remove(propertyDescriptor);
      }
    }

    public IEnumerable<string> GetFieldNames()
    {
      List<string> stringList = new List<string>();
      foreach (PropertyDescriptor boundProperty in this.boundProperties)
      {
        if ((object) boundProperty.PropertyType != (object) typeof (IBindingList))
          stringList.Add(boundProperty.Name);
      }
      return (IEnumerable<string>) stringList;
    }

    public void ApplyFilter(string expression)
    {
      if (this.sourceControl == null)
        throw new NullReferenceException("The DataSource of the control is not set, which is required for the ApplyFilter method to execute.");
      if (this.SourceControl is IBindingListView)
      {
        IBindingListView sourceControl = this.SourceControl as IBindingListView;
        if (string.IsNullOrEmpty(expression))
          sourceControl.RemoveFilter();
        else
          sourceControl.Filter = expression;
      }
      else if (this.SourceControl is DataView)
      {
        (this.SourceControl as DataView).RowFilter = expression;
      }
      else
      {
        if (!(this.SourceControl is DataTable))
          throw new NotSupportedException(this.SourceControl.GetType().ToString() + " does not implement IBindingListView, which is required to apply the filter expression. The supported types are implementers of System.ComponentModel.IBindingListView, System.Data.DataTable and System.Data.DataView");
        (this.SourceControl as DataTable).DefaultView.RowFilter = expression;
      }
    }

    public System.Type GetFieldType(string fieldName)
    {
      return this.GetPropertyDescriptor(fieldName)?.PropertyType;
    }

    public PropertyDescriptor GetPropertyDescriptor(string propertyName)
    {
      foreach (PropertyDescriptor boundProperty in this.boundProperties)
      {
        if (boundProperty.Name == propertyName)
          return boundProperty;
      }
      return (PropertyDescriptor) null;
    }

    public PropertyDescriptorCollection PropertyDescriptors
    {
      get
      {
        return this.boundProperties;
      }
      set
      {
        this.boundProperties = value;
      }
    }
  }
}
