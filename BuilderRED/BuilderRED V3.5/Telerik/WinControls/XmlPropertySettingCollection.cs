// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlPropertySettingCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class XmlPropertySettingCollection : CollectionBase, ICollection<XmlPropertySetting>, IEnumerable<XmlPropertySetting>, IEnumerable
  {
    public XmlPropertySettingCollection()
    {
    }

    public XmlPropertySettingCollection(XmlPropertySettingCollection value)
    {
      this.AddRange(value);
    }

    public XmlPropertySettingCollection(XmlPropertySetting[] value)
    {
      this.AddRange(value);
    }

    public XmlPropertySetting this[int index]
    {
      get
      {
        return (XmlPropertySetting) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public int Add(XmlPropertySetting value)
    {
      return this.List.Add((object) value);
    }

    void ICollection<XmlPropertySetting>.Add(XmlPropertySetting value)
    {
      this.Add(value);
    }

    bool ICollection<XmlPropertySetting>.Remove(
      XmlPropertySetting value)
    {
      int index = this.IndexOf(value);
      if (index < 0)
        return false;
      this.RemoveAt(index);
      return true;
    }

    public void AddRange(XmlPropertySetting[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    public void AddRange(XmlPropertySettingCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    public bool Contains(XmlPropertySetting value)
    {
      return this.List.Contains((object) value);
    }

    public void CopyTo(XmlPropertySetting[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public int IndexOf(XmlPropertySetting value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Insert(int index, XmlPropertySetting value)
    {
      this.List.Insert(index, (object) value);
    }

    public XmlPropertySettingCollection.XmlPropertySettingEnumerator GetEnumerator()
    {
      return new XmlPropertySettingCollection.XmlPropertySettingEnumerator(this);
    }

    IEnumerator<XmlPropertySetting> IEnumerable<XmlPropertySetting>.GetEnumerator()
    {
      return (IEnumerator<XmlPropertySetting>) this.GetEnumerator();
    }

    public void Remove(XmlPropertySetting value)
    {
      int index = this.List.IndexOf((object) value);
      if (index < 0)
        return;
      this.List.RemoveAt(index);
    }

    bool ICollection<XmlPropertySetting>.IsReadOnly
    {
      get
      {
        return this.InnerList.IsReadOnly;
      }
    }

    public class XmlPropertySettingEnumerator : IEnumerator<XmlPropertySetting>, IDisposable, IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public XmlPropertySettingEnumerator(XmlPropertySettingCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public XmlPropertySetting Current
      {
        get
        {
          return (XmlPropertySetting) this.baseEnumerator.Current;
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

      void IDisposable.Dispose()
      {
      }
    }
  }
}
