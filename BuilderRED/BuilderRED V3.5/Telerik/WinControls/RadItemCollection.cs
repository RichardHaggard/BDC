// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [DebuggerDisplay("Count = {Count}")]
  [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  [Serializable]
  public class RadItemCollection : CollectionBase, IEnumerable<RadItem>, IEnumerable
  {
    private System.Type[] itemTypes = new System.Type[1]
    {
      typeof (RadItem)
    };
    private int suspendNotifyCount;
    private System.Type[] excludedTypes;
    private System.Type[] sealedTypes;
    private System.Type defaultType;

    public event ItemChangedDelegate ItemsChanged;

    protected virtual void OnItemsChanged(RadItem target, ItemsChangeOperation operation)
    {
      if (this.suspendNotifyCount > 0 || this.ItemsChanged == null)
        return;
      this.ItemsChanged(this, target, operation);
    }

    public RadItemCollection()
    {
    }

    public RadItemCollection(RadItemCollection value)
    {
      this.AddRange(value);
    }

    public RadItemCollection(RadItem[] value)
    {
      this.AddRange(value);
    }

    protected override void OnValidate(object value)
    {
      if (value is RadItem || value == null)
        return;
      int num = (int) MessageBox.Show("Error", "Error when adding " + value.GetType().FullName + " From " + value.GetType().Assembly.Location + " into " + typeof (RadElement).Assembly.FullName + " " + typeof (RadElement).Assembly.Location);
    }

    public void SuspendNotifications()
    {
      ++this.suspendNotifyCount;
    }

    public void ResumeNotifications()
    {
      if (this.suspendNotifyCount <= 0)
        return;
      --this.suspendNotifyCount;
    }

    public virtual System.Type[] ItemTypes
    {
      get
      {
        return this.itemTypes;
      }
      set
      {
        this.itemTypes = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual System.Type[] ExcludedTypes
    {
      get
      {
        return this.excludedTypes;
      }
      set
      {
        this.excludedTypes = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual System.Type[] SealedTypes
    {
      get
      {
        return this.sealedTypes;
      }
      set
      {
        this.sealedTypes = value;
      }
    }

    public virtual System.Type DefaultType
    {
      get
      {
        return this.defaultType;
      }
      set
      {
        this.defaultType = value;
      }
    }

    IEnumerator<RadItem> IEnumerable<RadItem>.GetEnumerator()
    {
      return (IEnumerator<RadItem>) new RadItemCollection.RadItemEnumerator(this);
    }

    public RadItemCollection.RadItemEnumerator GetEnumerator()
    {
      return new RadItemCollection.RadItemEnumerator(this);
    }

    protected override void OnInsertComplete(int index, object value)
    {
      this.OnItemsChanged((RadItem) value, ItemsChangeOperation.Inserted);
    }

    protected override void OnRemove(int index, object value)
    {
      this.OnItemsChanged((RadItem) value, ItemsChangeOperation.Removing);
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      this.OnItemsChanged((RadItem) value, ItemsChangeOperation.Removed);
    }

    protected override void OnSet(int index, object oldValue, object newValue)
    {
      this.OnItemsChanged((RadItem) newValue, ItemsChangeOperation.Setting);
    }

    protected override void OnSetComplete(int index, object oldValue, object newValue)
    {
      this.OnItemsChanged((RadItem) newValue, ItemsChangeOperation.Set);
    }

    protected override void OnClear()
    {
      this.OnItemsChanged((RadItem) null, ItemsChangeOperation.Clearing);
    }

    protected override void OnClearComplete()
    {
      this.OnItemsChanged((RadItem) null, ItemsChangeOperation.Cleared);
    }

    protected virtual void OnSort()
    {
      this.OnItemsChanged((RadItem) null, ItemsChangeOperation.Sorting);
    }

    protected virtual void OnSortComplete()
    {
      this.OnItemsChanged((RadItem) null, ItemsChangeOperation.Sorted);
    }

    public int Add(RadItem value)
    {
      return this.List.Add((object) value);
    }

    public void AddRange(params RadItem[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    public void AddRange(RadItemCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    public void Insert(int index, RadItem value)
    {
      this.List.Insert(index, (object) value);
    }

    public void Remove(RadItem value)
    {
      int index = this.List.IndexOf((object) value);
      if (index < 0)
        return;
      this.List.RemoveAt(index);
    }

    public void Sort()
    {
      this.OnSort();
      this.InnerList.Sort();
      this.OnSortComplete();
    }

    public void Sort(IComparer comparer)
    {
      this.OnSort();
      this.InnerList.Sort(comparer);
      this.OnSortComplete();
    }

    public void Sort(int index, int count, IComparer comparer)
    {
      this.OnSort();
      this.InnerList.Sort(index, count, comparer);
      this.OnSortComplete();
    }

    public bool Contains(RadItem value)
    {
      return this.List.Contains((object) value);
    }

    public int IndexOf(RadItem value)
    {
      return this.List.IndexOf((object) value);
    }

    public void CopyTo(RadItem[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public RadItem[] ToArray()
    {
      RadItem[] array = new RadItem[this.Count];
      this.CopyTo(array, 0);
      return array;
    }

    public virtual RadItem this[int index]
    {
      get
      {
        return (RadItem) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    [Browsable(false)]
    public RadItem First
    {
      get
      {
        if (this.Count != 0)
          return this[0];
        return (RadItem) null;
      }
    }

    [Browsable(false)]
    public RadItem Last
    {
      get
      {
        if (this.Count != 0)
          return this[this.Count - 1];
        return (RadItem) null;
      }
    }

    public virtual RadItem this[string itemName]
    {
      get
      {
        for (int index = 0; index < this.List.Count; ++index)
        {
          RadItem radItem = (RadItem) this.List[index];
          if (radItem.Name == itemName)
            return radItem;
        }
        return (RadItem) null;
      }
    }

    public class RadItemEnumerator : IEnumerator<RadItem>, IDisposable, IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public RadItemEnumerator(RadItemCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public RadItem Current
      {
        get
        {
          return (RadItem) this.baseEnumerator.Current;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return this.baseEnumerator.Current;
        }
      }

      public bool MoveNext()
      {
        return this.baseEnumerator.MoveNext();
      }

      bool IEnumerator.MoveNext()
      {
        return this.baseEnumerator.MoveNext();
      }

      public void Reset()
      {
        this.baseEnumerator.Reset();
      }

      void IEnumerator.Reset()
      {
        this.baseEnumerator.Reset();
      }

      void IDisposable.Dispose()
      {
      }
    }
  }
}
