// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadItemVirtualizationCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  public class RadItemVirtualizationCollection : RadItemCollection, IVirtualizationCollection
  {
    private IVirtualViewport ownerViewport;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IVirtualViewport OwnerViewport
    {
      get
      {
        return this.ownerViewport;
      }
      set
      {
        this.ownerViewport = value;
        this.ownerViewport.SetVirtualItemsCollection((IVirtualizationCollection) this);
      }
    }

    public RadItemVirtualizationCollection(IVirtualViewport ownerViewport)
    {
      this.OwnerViewport = ownerViewport;
    }

    public RadItemVirtualizationCollection()
    {
    }

    protected override void OnInsertComplete(int index, object value)
    {
      if (this.OwnerViewport != null)
        this.OwnerViewport.OnItemDataInserted(index, value);
      base.OnInsertComplete(index, value);
    }

    protected override void OnSetComplete(int index, object oldValue, object newValue)
    {
      if (this.OwnerViewport != null)
        this.OwnerViewport.OnItemDataSet(index, oldValue, newValue);
      base.OnSetComplete(index, oldValue, newValue);
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      base.OnRemoveComplete(index, value);
      if (this.OwnerViewport == null)
        return;
      this.OwnerViewport.OnItemDataRemoved(index, value);
    }

    protected override void OnClear()
    {
      if (this.OwnerViewport != null)
        this.OwnerViewport.OnItemsDataClear();
      base.OnClear();
    }

    protected override void OnClearComplete()
    {
      if (this.OwnerViewport != null)
        this.OwnerViewport.OnItemsDataClearComplete();
      base.OnClearComplete();
    }

    protected override void OnSort()
    {
      if (this.OwnerViewport != null)
        this.OwnerViewport.OnItemsDataSort();
      base.OnSort();
    }

    protected override void OnSortComplete()
    {
      if (this.OwnerViewport != null)
        this.OwnerViewport.OnItemsDataSortComplete();
      base.OnSortComplete();
    }

    public object GetVirtualData(int index)
    {
      if (index < 0 || index >= this.Count)
        return (object) null;
      return (object) this[index];
    }

    public int IndexOf(object data)
    {
      return this.List.IndexOf(data);
    }
  }
}
