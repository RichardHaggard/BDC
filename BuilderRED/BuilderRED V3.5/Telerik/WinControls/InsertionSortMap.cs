// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.InsertionSortMap
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls
{
  internal struct InsertionSortMap
  {
    internal LargeSortedObjectMap _mapStore;

    public void Sort()
    {
      if (this._mapStore == null)
        return;
      this._mapStore.Sort();
    }

    public void GetKeyValuePair(int index, out int key, out object value)
    {
      if (this._mapStore == null)
        throw new ArgumentOutOfRangeException(nameof (index));
      this._mapStore.GetKeyValuePair(index, out key, out value);
    }

    public void Iterate(ArrayList list, FrugalMapIterationCallback callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof (callback));
      if (list == null)
        throw new ArgumentNullException(nameof (list));
      if (this._mapStore == null)
        return;
      this._mapStore.Iterate(list, callback);
    }

    public int Count
    {
      get
      {
        if (this._mapStore != null)
          return this._mapStore.Count;
        return 0;
      }
    }

    public object this[int key]
    {
      get
      {
        if (this._mapStore != null)
          return this._mapStore.Search(key);
        return RadProperty.UnsetValue;
      }
      set
      {
        if (value != RadProperty.UnsetValue)
        {
          if (this._mapStore == null)
            this._mapStore = new LargeSortedObjectMap();
          FrugalMapStoreState frugalMapStoreState = this._mapStore.InsertEntry(key, value);
          if (frugalMapStoreState == FrugalMapStoreState.Success)
            return;
          if (FrugalMapStoreState.SortedArray != frugalMapStoreState)
            throw new InvalidOperationException("FrugalMap: CannotPromoteBeyondHashtable");
          LargeSortedObjectMap largeSortedObjectMap = new LargeSortedObjectMap();
          this._mapStore.Promote((FrugalMapBase) largeSortedObjectMap);
          this._mapStore = largeSortedObjectMap;
          int num = (int) this._mapStore.InsertEntry(key, value);
        }
        else
        {
          if (this._mapStore == null)
            return;
          this._mapStore.RemoveEntry(key);
          if (this._mapStore.Count != 0)
            return;
          this._mapStore = (LargeSortedObjectMap) null;
        }
      }
    }
  }
}
