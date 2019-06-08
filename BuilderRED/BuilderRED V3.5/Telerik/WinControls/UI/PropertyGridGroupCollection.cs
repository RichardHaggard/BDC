// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class PropertyGridGroupCollection : GroupCollection<PropertyGridItem>, IReadOnlyCollection<PropertyGridItemBase>, IEnumerable<PropertyGridItemBase>, IEnumerable
  {
    public static PropertyGridGroupCollection Empty = new PropertyGridGroupCollection((IList<Group<PropertyGridItem>>) new List<Group<PropertyGridItem>>());

    public PropertyGridGroupCollection(IList<Group<PropertyGridItem>> list)
      : base(list)
    {
    }

    public PropertyGridGroup this[int index]
    {
      get
      {
        return (PropertyGridGroup) base[index];
      }
    }

    PropertyGridItemBase IReadOnlyCollection<PropertyGridItemBase>.this[
      int index]
    {
      get
      {
        return (PropertyGridItemBase) this[index].GroupItem;
      }
    }

    bool IReadOnlyCollection<PropertyGridItemBase>.Contains(
      PropertyGridItemBase value)
    {
      PropertyGridGroupItem propertyGridGroupItem = value as PropertyGridGroupItem;
      if (propertyGridGroupItem == null)
        return false;
      return this.IndexOf((Group<PropertyGridItem>) propertyGridGroupItem.Group) >= 0;
    }

    void IReadOnlyCollection<PropertyGridItemBase>.CopyTo(
      PropertyGridItemBase[] array,
      int index)
    {
      for (int index1 = index; index1 < this.Count; ++index1)
        array[index1] = (PropertyGridItemBase) this[index1].GroupItem;
    }

    int IReadOnlyCollection<PropertyGridItemBase>.IndexOf(
      PropertyGridItemBase value)
    {
      PropertyGridGroupItem propertyGridGroupItem = value as PropertyGridGroupItem;
      if (propertyGridGroupItem == null)
        return -1;
      return this.IndexOf((Group<PropertyGridItem>) propertyGridGroupItem.Group);
    }

    IEnumerator<PropertyGridItemBase> IEnumerable<PropertyGridItemBase>.GetEnumerator()
    {
      for (int i = 0; i < this.Count; ++i)
        yield return (PropertyGridItemBase) this[i].GroupItem;
    }
  }
}
