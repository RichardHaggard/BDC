// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class PropertyGridGroupFactory : IGroupFactory<PropertyGridItem>
  {
    private PropertyGridTableElement propertyGridElement;

    public PropertyGridGroupFactory(PropertyGridTableElement propertyGridElement)
    {
      this.propertyGridElement = propertyGridElement;
    }

    public Group<PropertyGridItem> CreateGroup(
      object key,
      Group<PropertyGridItem> parent,
      params object[] metaData)
    {
      return (Group<PropertyGridItem>) new PropertyGridGroup(key, parent, this.propertyGridElement);
    }

    public GroupCollection<PropertyGridItem> CreateCollection(
      IList<Group<PropertyGridItem>> list)
    {
      return (GroupCollection<PropertyGridItem>) new PropertyGridGroupCollection(list);
    }
  }
}
