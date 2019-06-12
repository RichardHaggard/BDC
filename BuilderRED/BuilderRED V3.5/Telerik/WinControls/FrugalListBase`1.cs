// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.FrugalListBase`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  internal abstract class FrugalListBase<T>
  {
    protected int _count;

    public abstract FrugalListStoreState Add(T value);

    public abstract void Clear();

    public abstract object Clone();

    public abstract bool Contains(T value);

    public abstract void CopyTo(T[] array, int index);

    public abstract T EntryAt(int index);

    public abstract int IndexOf(T value);

    public abstract void Insert(int index, T value);

    public abstract void Promote(FrugalListBase<T> newList);

    public abstract bool Remove(T value);

    public abstract void RemoveAt(int index);

    public abstract void SetAt(int index, T value);

    public abstract void Sort();

    public abstract T[] ToArray();

    public abstract int Capacity { get; }

    public int Count
    {
      get
      {
        return this._count;
      }
    }
  }
}
