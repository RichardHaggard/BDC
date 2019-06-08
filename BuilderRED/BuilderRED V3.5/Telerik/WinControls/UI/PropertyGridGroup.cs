// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class PropertyGridGroup : DataItemGroup<PropertyGridItem>
  {
    private PropertyGridGroupItem groupData;
    private List<PropertyGridItem> items;

    public PropertyGridGroup(
      object key,
      Group<PropertyGridItem> parent,
      PropertyGridTableElement propertyGridElement)
      : base(key, parent)
    {
      this.groupData = new PropertyGridGroupItem(propertyGridElement, this);
      this.groupData.SuspendPropertyNotifications();
      this.groupData.Expanded = propertyGridElement.AutoExpandGroups;
      this.groupData.ResumePropertyNotifications();
    }

    public void Expand()
    {
      this.groupData.Expanded = true;
    }

    public void Collapse()
    {
      this.groupData.Expanded = false;
    }

    public PropertyGridGroupItem GroupItem
    {
      get
      {
        return this.groupData;
      }
    }

    public bool IsExpanded
    {
      get
      {
        return this.groupData.Expanded;
      }
    }

    protected internal override IList<PropertyGridItem> Items
    {
      get
      {
        if (this.items == null)
          this.items = new List<PropertyGridItem>();
        return (IList<PropertyGridItem>) this.items;
      }
    }
  }
}
