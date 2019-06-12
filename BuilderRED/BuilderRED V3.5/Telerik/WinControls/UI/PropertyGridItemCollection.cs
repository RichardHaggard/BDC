// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemCollection : ReadOnlyCollection<PropertyGridItem>
  {
    public PropertyGridItemCollection(IList<PropertyGridItem> items)
      : base(items)
    {
    }

    public PropertyGridItem this[string propertyName]
    {
      get
      {
        foreach (PropertyGridItem propertyGridItem in (ReadOnlyCollection<PropertyGridItem>) this)
        {
          if (propertyGridItem.Name == propertyName)
            return propertyGridItem;
        }
        return (PropertyGridItem) null;
      }
    }

    internal void AddProperty(PropertyGridItem item)
    {
      this.Items.Add(item);
    }

    public virtual void Sort(IComparer<PropertyGridItem> comparer)
    {
      ((List<PropertyGridItem>) this.Items).Sort(comparer);
    }
  }
}
