// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SingleObjectMap
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls
{
  internal sealed class SingleObjectMap : FrugalMapBase
  {
    private FrugalMapBase.Entry _loneEntry;

    public SingleObjectMap()
    {
      this._loneEntry.Key = int.MaxValue;
      this._loneEntry.Value = RadProperty.UnsetValue;
    }

    public override void GetKeyValuePair(int index, out int key, out object value)
    {
      if (index == 0)
      {
        value = this._loneEntry.Value;
        key = this._loneEntry.Key;
      }
      else
      {
        value = RadProperty.UnsetValue;
        key = int.MaxValue;
        throw new ArgumentOutOfRangeException(nameof (index));
      }
    }

    public override FrugalMapStoreState InsertEntry(int key, object value)
    {
      if (int.MaxValue != this._loneEntry.Key && key != this._loneEntry.Key)
        return FrugalMapStoreState.ThreeObjectMap;
      this._loneEntry.Key = key;
      this._loneEntry.Value = value;
      return FrugalMapStoreState.Success;
    }

    public override void Iterate(ArrayList list, FrugalMapIterationCallback callback)
    {
      if (this.Count != 1)
        return;
      callback(list, this._loneEntry.Key, this._loneEntry.Value);
    }

    public override void Promote(FrugalMapBase newMap)
    {
      if (newMap.InsertEntry(this._loneEntry.Key, this._loneEntry.Value) != FrugalMapStoreState.Success)
        throw new ArgumentException(string.Format("FrugalMap_TargetMapCannotHoldAllData {0}, {1}, {2}", new object[3]
        {
          (object) this.ToString(),
          (object) newMap.ToString(),
          (object) nameof (newMap)
        }));
    }

    public override void RemoveEntry(int key)
    {
      if (key != this._loneEntry.Key)
        return;
      this._loneEntry.Key = int.MaxValue;
      this._loneEntry.Value = RadProperty.UnsetValue;
    }

    public override object Search(int key)
    {
      if (key == this._loneEntry.Key)
        return this._loneEntry.Value;
      return RadProperty.UnsetValue;
    }

    public override void Sort()
    {
    }

    public override int Count
    {
      get
      {
        return int.MaxValue != this._loneEntry.Key ? 1 : 0;
      }
    }
  }
}
