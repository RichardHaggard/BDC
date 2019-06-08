// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListDataItemSelectedCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RadListDataItemSelectedCollection : ObservableCollection<RadListDataItem>, IReadOnlyCollection<RadListDataItem>, IEnumerable<RadListDataItem>, IEnumerable
  {
    private RadListElement owner;
    private bool removing;
    private bool inserting;
    private bool clearing;

    public RadListDataItemSelectedCollection(RadListElement owner)
    {
      if (owner == null)
        throw new ArgumentException("This collection can not be created without an owner. The owner argument can not be null.");
      this.owner = owner;
    }

    protected override void InsertItem(int index, RadListDataItem item)
    {
      if (this.Inserting)
        return;
      this.Inserting = true;
      if (this.owner.SelectionMode == SelectionMode.None)
        throw new InvalidOperationException("Items can not be selected when SelectionMode is None.");
      base.InsertItem(index, item);
      if (!this.Contains(item))
      {
        this.inserting = false;
      }
      else
      {
        int num = (int) item.SetValue(RadListDataItem.SelectedProperty, (object) true);
        this.inserting = false;
      }
    }

    protected override void RemoveItem(int index)
    {
      if (this.Removing || this.Clearing)
        return;
      this.Removing = true;
      if (this[index].Owner != null)
      {
        int num = (int) this[index].SetValue(RadListDataItem.SelectedProperty, (object) false);
      }
      base.RemoveItem(index);
      this.Removing = false;
    }

    protected override void SetItem(int index, RadListDataItem item)
    {
      throw new InvalidOperationException("Set is not a valid operation on this collection.");
    }

    protected override void ClearItems()
    {
      if (this.Clearing || this.Removing)
        return;
      this.Clearing = true;
      foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) this.Items)
      {
        if (radListDataItem.Owner != null)
        {
          int num = (int) radListDataItem.SetValue(RadListDataItem.SelectedProperty, (object) false);
        }
      }
      this.Clearing = false;
      base.ClearItems();
    }

    public bool Inserting
    {
      get
      {
        return this.inserting;
      }
      private set
      {
        this.inserting = value;
      }
    }

    public bool Removing
    {
      get
      {
        return this.removing;
      }
      private set
      {
        this.removing = value;
      }
    }

    public bool Clearing
    {
      get
      {
        return this.clearing;
      }
      private set
      {
        this.clearing = value;
      }
    }
  }
}
