// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarCommandTabCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  [Serializable]
  public class RadRibbonBarCommandTabCollection : RadItemOwnerCollection
  {
    private bool shouldSetParentCollection = true;
    private RadPageViewElement pageViewElement;
    private bool suspendPageViewItemRemoving;

    public RadRibbonBarCommandTabCollection()
    {
    }

    public RadRibbonBarCommandTabCollection(RadElement owner)
    {
      RadPageViewStripElement viewStripElement = owner as RadPageViewStripElement;
      if (owner != null)
      {
        this.pageViewElement = (RadPageViewElement) viewStripElement;
        this.Owner = (RadElement) ((RadPageViewStripElement) this.pageViewElement).ItemContainer;
      }
      else
        this.Owner = owner;
    }

    public event CollectionChangedHandler CollectionChanged;

    public bool ShouldSetParentCollection
    {
      get
      {
        return this.shouldSetParentCollection;
      }
      set
      {
        this.shouldSetParentCollection = value;
      }
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      if (this.suspendPageViewItemRemoving)
        return;
      if (this.shouldSetParentCollection)
        ((RibbonTab) value).ParentCollection = (RadRibbonBarCommandTabCollection) null;
      base.OnRemoveComplete(index, value);
      RadPageViewItem radPageViewItem = (RadPageViewItem) value;
      if (this.pageViewElement == null || !this.pageViewElement.Items.Contains(radPageViewItem))
        return;
      this.pageViewElement.RemoveItem(radPageViewItem);
    }

    protected override void OnInsertComplete(int index, object value)
    {
      if (this.suspendPageViewItemRemoving)
        return;
      if (this.shouldSetParentCollection)
        ((RibbonTab) value).ParentCollection = this;
      base.OnInsertComplete(index, value);
      RadPageViewItem newItem = (RadPageViewItem) value;
      if (this.pageViewElement != null && !this.pageViewElement.Items.Contains(newItem))
      {
        this.pageViewElement.InsertItem(index, newItem);
        this.EnsureSingleSelectedItem(newItem);
      }
      if (this.Owner.IsDesignMode)
      {
        this.SuspendNotifications();
        this.suspendPageViewItemRemoving = true;
        this.pageViewElement.UpdateLayout();
        this.Clear();
        foreach (RadItem radItem in (IEnumerable<RadPageViewItem>) this.pageViewElement.Items)
          this.Add(radItem);
        this.suspendPageViewItemRemoving = false;
        this.ResumeNotifications();
      }
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((CollectionBase) this, new CollectionChangedEventArgs(value, index, ItemsChangeOperation.Inserted));
    }

    private void EnsureSingleSelectedItem(RadPageViewItem newItem)
    {
      if (!newItem.IsSelected)
        return;
      foreach (RadPageViewItem radPageViewItem in (IEnumerable<RadPageViewItem>) this.pageViewElement.Items)
      {
        if (radPageViewItem != newItem && radPageViewItem.IsSelected)
        {
          newItem.IsSelected = false;
          this.pageViewElement.selectedItem = radPageViewItem;
          break;
        }
      }
    }

    protected override void OnClear()
    {
      if (this.suspendPageViewItemRemoving)
        return;
      foreach (RibbonTab inner in this.InnerList)
      {
        inner.ParentCollection = (RadRibbonBarCommandTabCollection) null;
        if (this.pageViewElement != null && this.pageViewElement.Items.Contains((RadPageViewItem) inner))
          this.pageViewElement.RemoveItem((RadPageViewItem) inner);
      }
      base.OnClear();
    }
  }
}
