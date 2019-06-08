// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SixObjectMap
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls
{
  internal sealed class SixObjectMap : FrugalMapBase
  {
    private const int SIZE = 6;
    private ushort _count;
    private FrugalMapBase.Entry _entry0;
    private FrugalMapBase.Entry _entry1;
    private FrugalMapBase.Entry _entry2;
    private FrugalMapBase.Entry _entry3;
    private FrugalMapBase.Entry _entry4;
    private FrugalMapBase.Entry _entry5;
    private bool _sorted;

    public override void GetKeyValuePair(int index, out int key, out object value)
    {
      if (index < (int) this._count)
      {
        switch (index)
        {
          case 0:
            key = this._entry0.Key;
            value = this._entry0.Value;
            break;
          case 1:
            key = this._entry1.Key;
            value = this._entry1.Value;
            break;
          case 2:
            key = this._entry2.Key;
            value = this._entry2.Value;
            break;
          case 3:
            key = this._entry3.Key;
            value = this._entry3.Value;
            break;
          case 4:
            key = this._entry4.Key;
            value = this._entry4.Value;
            break;
          case 5:
            key = this._entry5.Key;
            value = this._entry5.Value;
            break;
          default:
            key = int.MaxValue;
            value = RadProperty.UnsetValue;
            break;
        }
      }
      else
      {
        key = int.MaxValue;
        value = RadProperty.UnsetValue;
        throw new ArgumentOutOfRangeException(nameof (index));
      }
    }

    public override FrugalMapStoreState InsertEntry(int key, object value)
    {
      if (this._count > (ushort) 0)
      {
        if (this._entry0.Key == key)
        {
          this._entry0.Value = value;
          return FrugalMapStoreState.Success;
        }
        if (this._count > (ushort) 1)
        {
          if (this._entry1.Key == key)
          {
            this._entry1.Value = value;
            return FrugalMapStoreState.Success;
          }
          if (this._count > (ushort) 2)
          {
            if (this._entry2.Key == key)
            {
              this._entry2.Value = value;
              return FrugalMapStoreState.Success;
            }
            if (this._count > (ushort) 3)
            {
              if (this._entry3.Key == key)
              {
                this._entry3.Value = value;
                return FrugalMapStoreState.Success;
              }
              if (this._count > (ushort) 4)
              {
                if (this._entry4.Key == key)
                {
                  this._entry4.Value = value;
                  return FrugalMapStoreState.Success;
                }
                if (this._count > (ushort) 5 && this._entry5.Key == key)
                {
                  this._entry5.Value = value;
                  return FrugalMapStoreState.Success;
                }
              }
            }
          }
        }
      }
      if ((ushort) 6 <= this._count)
        return FrugalMapStoreState.Array;
      this._sorted = false;
      switch (this._count)
      {
        case 0:
          this._entry0.Key = key;
          this._entry0.Value = value;
          this._sorted = true;
          break;
        case 1:
          this._entry1.Key = key;
          this._entry1.Value = value;
          break;
        case 2:
          this._entry2.Key = key;
          this._entry2.Value = value;
          break;
        case 3:
          this._entry3.Key = key;
          this._entry3.Value = value;
          break;
        case 4:
          this._entry4.Key = key;
          this._entry4.Value = value;
          break;
        case 5:
          this._entry5.Key = key;
          this._entry5.Value = value;
          break;
      }
      ++this._count;
      return FrugalMapStoreState.Success;
    }

    public override void Iterate(ArrayList list, FrugalMapIterationCallback callback)
    {
      if (this._count <= (ushort) 0)
        return;
      if (this._count >= (ushort) 1)
        callback(list, this._entry0.Key, this._entry0.Value);
      if (this._count >= (ushort) 2)
        callback(list, this._entry1.Key, this._entry1.Value);
      if (this._count >= (ushort) 3)
        callback(list, this._entry2.Key, this._entry2.Value);
      if (this._count >= (ushort) 4)
        callback(list, this._entry3.Key, this._entry3.Value);
      if (this._count >= (ushort) 5)
        callback(list, this._entry4.Key, this._entry4.Value);
      if (this._count != (ushort) 6)
        return;
      callback(list, this._entry5.Key, this._entry5.Value);
    }

    public override void Promote(FrugalMapBase newMap)
    {
      if (newMap.InsertEntry(this._entry0.Key, this._entry0.Value) != FrugalMapStoreState.Success)
        throw new ArgumentException(string.Format("FrugalMap_TargetMapCannotHoldAllData: {0}, {1}, {2}", new object[3]
        {
          (object) this.ToString(),
          (object) newMap.ToString(),
          (object) nameof (newMap)
        }));
      if (newMap.InsertEntry(this._entry1.Key, this._entry1.Value) != FrugalMapStoreState.Success)
        throw new ArgumentException(string.Format("FrugalMap_TargetMapCannotHoldAllData: {0}, {1}, {2}", new object[3]
        {
          (object) this.ToString(),
          (object) newMap.ToString(),
          (object) nameof (newMap)
        }));
      if (newMap.InsertEntry(this._entry2.Key, this._entry2.Value) != FrugalMapStoreState.Success)
        throw new ArgumentException(string.Format("FrugalMap_TargetMapCannotHoldAllData: {0}, {1}, {2}", new object[3]
        {
          (object) this.ToString(),
          (object) newMap.ToString(),
          (object) nameof (newMap)
        }));
      if (newMap.InsertEntry(this._entry3.Key, this._entry3.Value) != FrugalMapStoreState.Success)
        throw new ArgumentException(string.Format("FrugalMap_TargetMapCannotHoldAllData: {0}, {1}, {2}", new object[3]
        {
          (object) this.ToString(),
          (object) newMap.ToString(),
          (object) nameof (newMap)
        }));
      if (newMap.InsertEntry(this._entry4.Key, this._entry4.Value) != FrugalMapStoreState.Success)
        throw new ArgumentException(string.Format("FrugalMap_TargetMapCannotHoldAllData: {0}, {1}, {2}", new object[3]
        {
          (object) this.ToString(),
          (object) newMap.ToString(),
          (object) nameof (newMap)
        }));
      if (newMap.InsertEntry(this._entry5.Key, this._entry5.Value) != FrugalMapStoreState.Success)
        throw new ArgumentException(string.Format("FrugalMap_TargetMapCannotHoldAllData: {0}, {1}, {2}", new object[3]
        {
          (object) this.ToString(),
          (object) newMap.ToString(),
          (object) nameof (newMap)
        }));
    }

    public override void RemoveEntry(int key)
    {
      switch (this._count)
      {
        case 1:
          if (this._entry0.Key != key)
            break;
          this._entry0.Key = int.MaxValue;
          this._entry0.Value = RadProperty.UnsetValue;
          --this._count;
          break;
        case 2:
          if (this._entry0.Key != key)
          {
            if (this._entry1.Key != key)
              break;
            this._entry1.Key = int.MaxValue;
            this._entry1.Value = RadProperty.UnsetValue;
            --this._count;
            break;
          }
          this._entry0 = this._entry1;
          this._entry1.Key = int.MaxValue;
          this._entry1.Value = RadProperty.UnsetValue;
          --this._count;
          break;
        case 3:
          if (this._entry0.Key != key)
          {
            if (this._entry1.Key == key)
            {
              this._entry1 = this._entry2;
              this._entry2.Key = int.MaxValue;
              this._entry2.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry2.Key != key)
              break;
            this._entry2.Key = int.MaxValue;
            this._entry2.Value = RadProperty.UnsetValue;
            --this._count;
            break;
          }
          this._entry0 = this._entry1;
          this._entry1 = this._entry2;
          this._entry2.Key = int.MaxValue;
          this._entry2.Value = RadProperty.UnsetValue;
          --this._count;
          break;
        case 4:
          if (this._entry0.Key != key)
          {
            if (this._entry1.Key == key)
            {
              this._entry1 = this._entry2;
              this._entry2 = this._entry3;
              this._entry3.Key = int.MaxValue;
              this._entry3.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry2.Key == key)
            {
              this._entry2 = this._entry3;
              this._entry3.Key = int.MaxValue;
              this._entry3.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry3.Key != key)
              break;
            this._entry3.Key = int.MaxValue;
            this._entry3.Value = RadProperty.UnsetValue;
            --this._count;
            break;
          }
          this._entry0 = this._entry1;
          this._entry1 = this._entry2;
          this._entry2 = this._entry3;
          this._entry3.Key = int.MaxValue;
          this._entry3.Value = RadProperty.UnsetValue;
          --this._count;
          break;
        case 5:
          if (this._entry0.Key != key)
          {
            if (this._entry1.Key == key)
            {
              this._entry1 = this._entry2;
              this._entry2 = this._entry3;
              this._entry3 = this._entry4;
              this._entry4.Key = int.MaxValue;
              this._entry4.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry2.Key == key)
            {
              this._entry2 = this._entry3;
              this._entry3 = this._entry4;
              this._entry4.Key = int.MaxValue;
              this._entry4.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry3.Key == key)
            {
              this._entry3 = this._entry4;
              this._entry4.Key = int.MaxValue;
              this._entry4.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry4.Key != key)
              break;
            this._entry4.Key = int.MaxValue;
            this._entry4.Value = RadProperty.UnsetValue;
            --this._count;
            break;
          }
          this._entry0 = this._entry1;
          this._entry1 = this._entry2;
          this._entry2 = this._entry3;
          this._entry3 = this._entry4;
          this._entry4.Key = int.MaxValue;
          this._entry4.Value = RadProperty.UnsetValue;
          --this._count;
          break;
        case 6:
          if (this._entry0.Key != key)
          {
            if (this._entry1.Key == key)
            {
              this._entry1 = this._entry2;
              this._entry2 = this._entry3;
              this._entry3 = this._entry4;
              this._entry4 = this._entry5;
              this._entry5.Key = int.MaxValue;
              this._entry5.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry2.Key == key)
            {
              this._entry2 = this._entry3;
              this._entry3 = this._entry4;
              this._entry4 = this._entry5;
              this._entry5.Key = int.MaxValue;
              this._entry5.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry3.Key == key)
            {
              this._entry3 = this._entry4;
              this._entry4 = this._entry5;
              this._entry5.Key = int.MaxValue;
              this._entry5.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry4.Key == key)
            {
              this._entry4 = this._entry5;
              this._entry5.Key = int.MaxValue;
              this._entry5.Value = RadProperty.UnsetValue;
              --this._count;
              break;
            }
            if (this._entry5.Key != key)
              break;
            this._entry5.Key = int.MaxValue;
            this._entry5.Value = RadProperty.UnsetValue;
            --this._count;
            break;
          }
          this._entry0 = this._entry1;
          this._entry1 = this._entry2;
          this._entry2 = this._entry3;
          this._entry3 = this._entry4;
          this._entry4 = this._entry5;
          this._entry5.Key = int.MaxValue;
          this._entry5.Value = RadProperty.UnsetValue;
          --this._count;
          break;
      }
    }

    public override object Search(int key)
    {
      if (this._count > (ushort) 0)
      {
        if (this._entry0.Key == key)
          return this._entry0.Value;
        if (this._count > (ushort) 1)
        {
          if (this._entry1.Key == key)
            return this._entry1.Value;
          if (this._count > (ushort) 2)
          {
            if (this._entry2.Key == key)
              return this._entry2.Value;
            if (this._count > (ushort) 3)
            {
              if (this._entry3.Key == key)
                return this._entry3.Value;
              if (this._count > (ushort) 4)
              {
                if (this._entry4.Key == key)
                  return this._entry4.Value;
                if (this._count > (ushort) 5 && this._entry5.Key == key)
                  return this._entry5.Value;
              }
            }
          }
        }
      }
      return RadProperty.UnsetValue;
    }

    public override void Sort()
    {
      if (this._sorted || this._count <= (ushort) 1)
        return;
      bool flag;
      do
      {
        flag = false;
        if (this._entry0.Key > this._entry1.Key)
        {
          FrugalMapBase.Entry entry0 = this._entry0;
          this._entry0 = this._entry1;
          this._entry1 = entry0;
          flag = true;
        }
        if (this._count > (ushort) 2)
        {
          if (this._entry1.Key > this._entry2.Key)
          {
            FrugalMapBase.Entry entry1 = this._entry1;
            this._entry1 = this._entry2;
            this._entry2 = entry1;
            flag = true;
          }
          if (this._count > (ushort) 3)
          {
            if (this._entry2.Key > this._entry3.Key)
            {
              FrugalMapBase.Entry entry2 = this._entry2;
              this._entry2 = this._entry3;
              this._entry3 = entry2;
              flag = true;
            }
            if (this._count > (ushort) 4)
            {
              if (this._entry3.Key > this._entry4.Key)
              {
                FrugalMapBase.Entry entry3 = this._entry3;
                this._entry3 = this._entry4;
                this._entry4 = entry3;
                flag = true;
              }
              if (this._count > (ushort) 5 && this._entry4.Key > this._entry5.Key)
              {
                FrugalMapBase.Entry entry4 = this._entry4;
                this._entry4 = this._entry5;
                this._entry5 = entry4;
                flag = true;
              }
            }
          }
        }
      }
      while (flag);
      this._sorted = true;
    }

    public override int Count
    {
      get
      {
        return (int) this._count;
      }
    }
  }
}
