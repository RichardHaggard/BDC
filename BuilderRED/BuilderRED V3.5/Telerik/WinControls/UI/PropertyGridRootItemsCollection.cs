// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridRootItemsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class PropertyGridRootItemsCollection : IList<PropertyGridItem>, ICollection<PropertyGridItem>, IEnumerable<PropertyGridItem>, IEnumerable
  {
    private PropertyGridTableElement tableElement;

    public PropertyGridRootItemsCollection(PropertyGridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    public int IndexOf(PropertyGridItem item)
    {
      return this.tableElement.CollectionView.IndexOf(item);
    }

    public void Insert(int index, PropertyGridItem item)
    {
      throw new NotImplementedException();
    }

    PropertyGridItem IList<PropertyGridItem>.this[int index]
    {
      get
      {
        return this.tableElement.CollectionView[index];
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public void RemoveAt(int index)
    {
      throw new NotImplementedException();
    }

    public int Count
    {
      get
      {
        return this.tableElement.CollectionView.Count;
      }
    }

    public void Add(PropertyGridItem item)
    {
      throw new NotImplementedException();
    }

    public bool Contains(PropertyGridItem item)
    {
      return this.tableElement.CollectionView.Contains(item);
    }

    public void CopyTo(PropertyGridItem[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public bool Remove(PropertyGridItem item)
    {
      throw new NotImplementedException();
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    IEnumerator<PropertyGridItem> IEnumerable<PropertyGridItem>.GetEnumerator()
    {
      return (IEnumerator<PropertyGridItem>) new PropertyGridRootItemsCollection.PropertyGridRootItemsCollectionEnumerator(this);
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new PropertyGridRootItemsCollection.PropertyGridRootItemsCollectionEnumerator(this);
    }

    private class PropertyGridRootItemsCollectionEnumerator : IEnumerator<PropertyGridItem>, IDisposable, IEnumerator
    {
      private int index = -1;
      private PropertyGridRootItemsCollection list;

      public PropertyGridRootItemsCollectionEnumerator(PropertyGridRootItemsCollection list)
      {
        this.list = list;
      }

      public PropertyGridItem Current
      {
        get
        {
          return ((IList<PropertyGridItem>) this.list)[this.index];
        }
      }

      public void Dispose()
      {
        this.list = (PropertyGridRootItemsCollection) null;
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) ((IList<PropertyGridItem>) this.list)[this.index];
        }
      }

      public bool MoveNext()
      {
        if (this.index >= this.list.Count - 1)
          return false;
        ++this.index;
        return true;
      }

      public void Reset()
      {
        this.index = -1;
      }
    }
  }
}
