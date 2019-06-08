// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSelectorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls
{
  public class XmlSelectorCollection : CollectionBase
  {
    public XmlSelectorCollection()
      : base(1)
    {
    }

    public XmlSelectorCollection(int capacity)
      : base(capacity)
    {
    }

    public XmlSelectorCollection(XmlSelectorCollection value)
    {
      this.AddRange(value);
    }

    public XmlSelectorCollection(XmlElementSelector[] value)
    {
      this.AddRange(value);
    }

    public XmlElementSelector this[int index]
    {
      get
      {
        return (XmlElementSelector) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(XmlElementSelector value)
    {
      return this.List.Add((object) value);
    }

    public void AddRange(XmlElementSelector[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    public void AddRange(XmlSelectorCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    public bool Contains(XmlElementSelector value)
    {
      return this.List.Contains((object) value);
    }

    public void CopyTo(XmlElementSelector[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public int IndexOf(XmlElementSelector value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Insert(int index, XmlElementSelector value)
    {
      this.List.Insert(index, (object) value);
    }

    public XmlSelectorCollection.XmlSelectorEnumerator GetEnumerator()
    {
      return new XmlSelectorCollection.XmlSelectorEnumerator(this);
    }

    public void Remove(XmlElementSelector value)
    {
      this.List.Remove((object) value);
    }

    public class XmlSelectorEnumerator : IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public XmlSelectorEnumerator(XmlSelectorCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public XmlElementSelector Current
      {
        get
        {
          return (XmlElementSelector) this.baseEnumerator.Current;
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
