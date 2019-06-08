// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadItemOwnerCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class RadItemOwnerCollection : RadItemCollection
  {
    private RadElement owner;

    public RadItemOwnerCollection(RadElement owner)
    {
      this.owner = owner;
    }

    public RadItemOwnerCollection()
    {
    }

    public RadItemOwnerCollection(RadItemOwnerCollection value)
      : base((RadItemCollection) value)
    {
    }

    public RadItemOwnerCollection(RadItem[] value)
      : base(value)
    {
    }

    public RadElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        if (this.owner == value)
          return;
        if (this.owner != null)
          this.RemoveAllFromOwner();
        this.owner = value;
        if (this.owner == null)
          return;
        this.SynchronizeAllWithOwner();
      }
    }

    private void SynchronizeAllWithOwner()
    {
      foreach (RadItem radItem in (RadItemCollection) this)
      {
        if (radItem.Parent != this.owner)
          this.owner.Children.Add((RadElement) radItem);
      }
    }

    private void RemoveAllFromOwner()
    {
      for (int index1 = this.Count - 1; index1 >= 0; --index1)
      {
        int index2 = this.owner.Children.IndexOf((RadElement) this[index1]);
        if (index2 >= 0)
          this.owner.Children.RemoveAt(index2);
      }
    }

    protected override void OnItemsChanged(RadItem target, ItemsChangeOperation operation)
    {
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
        case ItemsChangeOperation.Set:
          target.SetOwnerCollection(this);
          break;
        case ItemsChangeOperation.Removed:
          target.SetOwnerCollection((RadItemOwnerCollection) null);
          break;
        case ItemsChangeOperation.Cleared:
          using (RadItemCollection.RadItemEnumerator enumerator = this.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.SetOwnerCollection((RadItemOwnerCollection) null);
            break;
          }
      }
      if (this.owner != null && (this.owner.ElementState == ElementState.Disposing || this.owner.ElementState == ElementState.Disposed))
        return;
      base.OnItemsChanged(target, operation);
    }

    protected override void OnSetComplete(int index, object oldValue, object newValue)
    {
      if (this.owner != null && this.owner.IsInValidState(false))
      {
        RadItem radItem = newValue as RadItem;
        int index1 = this.owner.Children.IndexOf((RadElement) radItem);
        if (index1 >= 0)
          this.owner.Children[index1] = (RadElement) radItem;
        else
          this.owner.Children.Add((RadElement) radItem);
      }
      base.OnSetComplete(index, oldValue, newValue);
    }

    protected override void OnClear()
    {
      base.OnClear();
      if (this.owner == null || !this.owner.IsInValidState(false))
        return;
      this.RemoveAllFromOwner();
    }

    protected override void OnInsertComplete(int index, object value)
    {
      if (this.owner != null)
      {
        RadItem radItem = value as RadItem;
        if (radItem.Parent != this.owner)
        {
          int index1 = index;
          if (index1 > this.owner.Children.Count)
            index1 = this.owner.Children.Count;
          this.owner.Children.Insert(index1, (RadElement) radItem);
        }
      }
      base.OnInsertComplete(index, value);
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      if (this.owner != null && this.owner.IsInValidState(false))
      {
        RadItem radItem = value as RadItem;
        if (radItem.Parent == this.owner)
          this.owner.Children.Remove((RadElement) radItem);
      }
      base.OnRemoveComplete(index, value);
    }

    protected override void OnSortComplete()
    {
      if (this.owner != null)
        this.owner.ChangeCollection((RadElement) null, ItemsChangeOperation.Sorted);
      base.OnSortComplete();
    }
  }
}
