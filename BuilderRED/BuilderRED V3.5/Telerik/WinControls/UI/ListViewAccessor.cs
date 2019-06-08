// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  internal class ListViewAccessor : IDisposable
  {
    private ListViewDetailColumn column;
    private bool suspendItemChanged;

    public void SuspendItemNotifications()
    {
      this.suspendItemChanged = true;
    }

    public void ResumeItemNotifications()
    {
      this.suspendItemChanged = false;
    }

    public ListViewAccessor(ListViewDetailColumn column)
    {
      if (column == null)
        throw new ArgumentException("Column argument can not be null.");
      this.column = column;
    }

    public ListViewDetailColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public RadListViewElement Owner
    {
      get
      {
        return this.column.Owner;
      }
    }

    public virtual object this[ListViewDataItem item]
    {
      get
      {
        return item.Cache[this.column];
      }
      set
      {
        this.SetUnboundValue(item, value);
      }
    }

    private void SetUnboundValue(ListViewDataItem item, object value)
    {
      if (item != null && this.Owner != null && !this.suspendItemChanged)
        this.Owner.ListSource.NotifyItemChanging(item);
      item.Cache[this.column] = value;
      if (item == null || this.Owner == null || this.suspendItemChanged)
        return;
      this.Owner.ListSource.NotifyItemChanged(item);
    }

    public override bool Equals(object obj)
    {
      ListViewAccessor listViewAccessor = obj as ListViewAccessor;
      if (obj == null || (object) listViewAccessor.GetType() != (object) this.GetType())
        return false;
      return listViewAccessor.Column == this.Column;
    }

    public override int GetHashCode()
    {
      int num = 0;
      if (this.column != null)
        num = this.column.GetHashCode();
      return base.GetHashCode() ^ num;
    }

    public void Dispose()
    {
      this.column = (ListViewDetailColumn) null;
      GC.SuppressFinalize((object) this);
    }
  }
}
