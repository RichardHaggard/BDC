// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Telerik.Data.Expressions;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemComparer : IComparer<PropertyGridItem>
  {
    private List<PropertyGridItemComparer.PropertyGridItemDescriptor> context = new List<PropertyGridItemComparer.PropertyGridItemDescriptor>();
    private string propertyNameCache = string.Empty;
    private PropertyGridTableElement propertyGridElement;

    public PropertyGridItemComparer(PropertyGridTableElement propertyGridElement)
    {
      this.propertyGridElement = propertyGridElement;
      this.Update();
    }

    public void Update()
    {
      this.context.Clear();
      this.propertyNameCache = this.propertyGridElement.SortDescriptors.Count <= 0 ? string.Empty : this.propertyGridElement.SortDescriptors[0].PropertyName;
      if (!this.propertyGridElement.ListSource.IsDataBound)
        return;
      for (int index = 0; index < this.propertyGridElement.SortDescriptors.Count; ++index)
      {
        SortDescriptor sortDescriptor = this.propertyGridElement.SortDescriptors[index];
        PropertyDescriptor propertyDescriptor = this.propertyGridElement.ListSource.BoundProperties.Find(sortDescriptor.PropertyName, true);
        if (propertyDescriptor != null)
          this.context.Add(new PropertyGridItemComparer.PropertyGridItemDescriptor()
          {
            Descriptor = propertyDescriptor,
            Index = this.propertyGridElement.ListSource.BoundProperties.IndexOf(propertyDescriptor),
            Descending = sortDescriptor.Direction == ListSortDirection.Descending
          });
      }
    }

    public int Compare(PropertyGridItem x, PropertyGridItem y)
    {
      object xValue = (object) null;
      object yValue = (object) null;
      if (this.propertyNameCache == "Label")
      {
        xValue = (object) x.Label;
        yValue = (object) y.Label;
      }
      else if (string.IsNullOrEmpty(this.propertyNameCache) || this.propertyNameCache == "SortOrder")
      {
        xValue = (object) x.SortOrder;
        yValue = (object) y.SortOrder;
      }
      else if (!string.IsNullOrEmpty(this.propertyNameCache))
      {
        PropertyInfo property = x.GetType().GetProperty(this.propertyNameCache, BindingFlags.Instance | BindingFlags.Public);
        xValue = property.GetValue((object) x, (object[]) null);
        yValue = property.GetValue((object) y, (object[]) null);
      }
      IComparable comparable = xValue as IComparable;
      int num = comparable == null || yValue == null || (object) yValue.GetType() != (object) xValue.GetType() ? DataUtils.CompareNulls(xValue, yValue) : comparable.CompareTo(yValue);
      if (num != 0 && this.propertyGridElement.SortOrder == SortOrder.Descending)
        return -num;
      return num;
    }

    private class PropertyGridItemDescriptor
    {
      public PropertyDescriptor Descriptor;
      public int Index;
      public bool Descending;
    }
  }
}
