// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupItemCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class PropertyGridGroupItemCollection : IList<PropertyGridGroupItem>, ICollection<PropertyGridGroupItem>, IEnumerable<PropertyGridGroupItem>, IEnumerable
  {
    private PropertyGridTableElement tableElement;

    public PropertyGridGroupItemCollection(PropertyGridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    public int IndexOf(PropertyGridGroupItem item)
    {
      return this.tableElement.CollectionView.Groups.IndexOf((Group<PropertyGridItem>) item.Group);
    }

    public void Insert(int index, PropertyGridGroupItem item)
    {
      throw new NotImplementedException();
    }

    public PropertyGridGroupItem this[int index]
    {
      get
      {
        return ((PropertyGridGroup) this.tableElement.CollectionView.Groups[index]).GroupItem;
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public PropertyGridGroupItem this[string name]
    {
      get
      {
        int count = this.Count;
        for (int index = 0; index < count; ++index)
        {
          PropertyGridGroupItem propertyGridGroupItem = this[index];
          if (propertyGridGroupItem.Name == name)
            return propertyGridGroupItem;
        }
        return (PropertyGridGroupItem) null;
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
        return this.tableElement.CollectionView.Groups.Count;
      }
    }

    public void Add(PropertyGridGroupItem item)
    {
      throw new NotImplementedException();
    }

    public bool Contains(PropertyGridGroupItem item)
    {
      return this.tableElement.CollectionView.Groups.Contains((Group<PropertyGridItem>) item.Group);
    }

    public void CopyTo(PropertyGridGroupItem[] array, int arrayIndex)
    {
      throw new NotImplementedException();
    }

    public bool Remove(PropertyGridGroupItem item)
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
        return true;
      }
    }

    IEnumerator<PropertyGridGroupItem> IEnumerable<PropertyGridGroupItem>.GetEnumerator()
    {
      return (IEnumerator<PropertyGridGroupItem>) new PropertyGridGroupItemCollection.PropertyGridGroupItemCollectionEnumerator(this);
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new PropertyGridGroupItemCollection.PropertyGridGroupItemCollectionEnumerator(this);
    }

    private class PropertyGridGroupItemCollectionEnumerator : IEnumerator<PropertyGridGroupItem>, IDisposable, IEnumerator
    {
      private int index = -1;
      private PropertyGridGroupItemCollection list;

      public PropertyGridGroupItemCollectionEnumerator(PropertyGridGroupItemCollection list)
      {
        this.list = list;
      }

      public PropertyGridGroupItem Current
      {
        get
        {
          return this.list[this.index];
        }
      }

      public void Dispose()
      {
        this.list = (PropertyGridGroupItemCollection) null;
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.list[this.index];
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
