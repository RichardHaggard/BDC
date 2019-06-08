// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlPropertySettingGroupCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls
{
  public class XmlPropertySettingGroupCollection : CollectionBase
  {
    public XmlPropertySettingGroupCollection()
    {
    }

    public XmlPropertySettingGroupCollection(int capacity)
      : base(capacity)
    {
    }

    public XmlPropertySettingGroupCollection(XmlPropertySettingGroupCollection value)
    {
      this.AddRange(value);
    }

    public XmlPropertySettingGroupCollection(XmlPropertySettingGroup[] value)
    {
      this.AddRange(value);
    }

    public XmlPropertySettingGroup this[int index]
    {
      get
      {
        return (XmlPropertySettingGroup) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(XmlPropertySettingGroup value)
    {
      return this.List.Add((object) value);
    }

    public void AddRange(XmlPropertySettingGroup[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    public void AddRange(XmlPropertySettingGroupCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    public bool Contains(XmlPropertySettingGroup value)
    {
      return this.List.Contains((object) value);
    }

    public void CopyTo(XmlPropertySettingGroup[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public int IndexOf(XmlPropertySettingGroup value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Insert(int index, XmlPropertySettingGroup value)
    {
      this.List.Insert(index, (object) value);
    }

    public XmlPropertySettingGroupCollection.XmlPropertySettingGroupEnumerator GetEnumerator()
    {
      return new XmlPropertySettingGroupCollection.XmlPropertySettingGroupEnumerator(this);
    }

    public void Remove(XmlPropertySettingGroup value)
    {
      int index = this.List.IndexOf((object) value);
      if (index < 0)
        return;
      this.List.RemoveAt(index);
    }

    public class XmlPropertySettingGroupEnumerator : IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public XmlPropertySettingGroupEnumerator(XmlPropertySettingGroupCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public XmlPropertySettingGroup Current
      {
        get
        {
          return (XmlPropertySettingGroup) this.baseEnumerator.Current;
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
