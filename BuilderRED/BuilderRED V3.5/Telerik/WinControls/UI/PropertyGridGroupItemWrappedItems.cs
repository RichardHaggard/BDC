// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupItemWrappedItems
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  internal class PropertyGridGroupItemWrappedItems : IList<PropertyGridItem>, ICollection<PropertyGridItem>, IEnumerable<PropertyGridItem>, IEnumerable
  {
    private IList<PropertyGridItem> items;

    public PropertyGridGroupItemWrappedItems(IList<PropertyGridItem> items)
    {
      this.items = items;
    }

    public int IndexOf(PropertyGridItem item)
    {
      return this.items.IndexOf(item);
    }

    public void Insert(int index, PropertyGridItem item)
    {
      this.items.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      this.items.RemoveAt(index);
    }

    public PropertyGridItem this[int index]
    {
      get
      {
        return this.items[index];
      }
      set
      {
        this.items[index] = value;
      }
    }

    public void Add(PropertyGridItem item)
    {
      this.items.Add(item);
    }

    public void Clear()
    {
      this.items.Clear();
    }

    public bool Contains(PropertyGridItem item)
    {
      return this.items.Contains(item);
    }

    public void CopyTo(PropertyGridItem[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get
      {
        return this.items.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.items.IsReadOnly;
      }
    }

    public bool Remove(PropertyGridItem item)
    {
      return this.items.Remove(item);
    }

    public IEnumerator<PropertyGridItem> GetEnumerator()
    {
      return (IEnumerator<PropertyGridItem>) new PropertyGridGroupItemWrappedItems.MyEnumerator((IEnumerator) this.items.GetEnumerator());
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.items.GetEnumerator();
    }

    private class MyEnumerator : IEnumerator<PropertyGridItem>, IDisposable, IEnumerator
    {
      private IEnumerator enumerator;

      public MyEnumerator(IEnumerator enumerator)
      {
        this.enumerator = enumerator;
      }

      public PropertyGridItem Current
      {
        get
        {
          return this.enumerator.Current as PropertyGridItem;
        }
      }

      public void Dispose()
      {
        (this.enumerator as IDisposable)?.Dispose();
      }

      object IEnumerator.Current
      {
        get
        {
          return this.enumerator.Current;
        }
      }

      public bool MoveNext()
      {
        return this.enumerator.MoveNext();
      }

      public void Reset()
      {
        this.enumerator.Reset();
      }
    }
  }
}
