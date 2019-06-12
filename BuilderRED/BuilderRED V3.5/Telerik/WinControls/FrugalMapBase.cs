// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.FrugalMapBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;

namespace Telerik.WinControls
{
  internal abstract class FrugalMapBase
  {
    public const int INVALIDKEY = 2147483647;

    public abstract void GetKeyValuePair(int index, out int key, out object value);

    public abstract FrugalMapStoreState InsertEntry(int key, object value);

    public abstract void Iterate(ArrayList list, FrugalMapIterationCallback callback);

    public abstract void Promote(FrugalMapBase newMap);

    public abstract void RemoveEntry(int key);

    public abstract object Search(int key);

    public abstract void Sort();

    public abstract int Count { get; }

    internal struct Entry
    {
      public int Key;
      public object Value;
    }
  }
}
