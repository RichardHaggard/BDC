// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class ListGroup : DataItemGroup<RadListDataItem>
  {
    private RadListElement owner;
    private bool collapsed;
    private bool collapsible;

    public bool Collapsed
    {
      get
      {
        if (!this.Collapsible)
          return false;
        return this.collapsed;
      }
      set
      {
        this.collapsed = value;
      }
    }

    public bool Collapsible
    {
      get
      {
        return this.collapsible;
      }
      set
      {
        this.collapsible = value;
      }
    }

    public ListGroup(object key, RadListElement owner)
      : base(key)
    {
      this.owner = owner;
    }

    public void AddItem(RadListDataItem item)
    {
      if (this.Items.Contains(item))
        return;
      if (item.Group != null)
        item.Group.RemoveItem(item);
      this.Items.Add(item);
      item.Group = this;
    }

    public void AddRange(IEnumerable<RadListDataItem> items)
    {
      this.owner.SuspendGroupRefresh();
      foreach (RadListDataItem radListDataItem in items)
        this.AddItem(radListDataItem);
      this.owner.ResumeGroupRefresh(true);
    }

    public void RemoveItem(RadListDataItem item)
    {
      this.Items.Remove(item);
      item.Group = (ListGroup) null;
    }

    public void ClearItems()
    {
      foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) this.Items)
        radListDataItem.Group = (ListGroup) null;
      this.Items.Clear();
    }
  }
}
