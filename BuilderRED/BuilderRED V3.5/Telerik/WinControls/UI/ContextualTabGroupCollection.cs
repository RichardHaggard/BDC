// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ContextualTabGroupCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls.UI
{
  [Serializable]
  public class ContextualTabGroupCollection : CollectionBase
  {
    private RadElement owner;

    public RadElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    public ContextualTabGroupCollection(RadElement owner)
    {
      this.owner = owner;
    }

    public ContextualTabGroupCollection()
    {
    }

    public ContextualTabGroupCollection(ContextualTabGroupCollection value)
    {
      this.AddRange(value);
    }

    public ContextualTabGroupCollection(ContextualTabGroup[] value)
    {
      this.AddRange(value);
    }

    public ContextualTabGroup this[int index]
    {
      get
      {
        return (ContextualTabGroup) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(ContextualTabGroup value)
    {
      return this.List.Add((object) value);
    }

    public void AddRange(ContextualTabGroup[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    public void AddRange(ContextualTabGroupCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    public bool Contains(ContextualTabGroup value)
    {
      return this.List.Contains((object) value);
    }

    public void CopyTo(ContextualTabGroup[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public int IndexOf(ContextualTabGroup value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Insert(int index, ContextualTabGroup value)
    {
      this.List.Insert(index, (object) value);
    }

    public ContextualTabGroupCollection.ContextualTabGroupEnumerator GetEnumerator()
    {
      return new ContextualTabGroupCollection.ContextualTabGroupEnumerator(this);
    }

    public void Remove(ContextualTabGroup value)
    {
      this.List.Remove((object) value);
    }

    public class ContextualTabGroupEnumerator : IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public ContextualTabGroupEnumerator(ContextualTabGroupCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public ContextualTabGroup Current
      {
        get
        {
          return (ContextualTabGroup) this.baseEnumerator.Current;
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
    }
  }
}
