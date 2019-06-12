// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ThemeSourceCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls
{
  [Serializable]
  public class ThemeSourceCollection : CollectionBase
  {
    private RadThemeManager ownerManager;
    public ThemeSourcesChangedDelegate ThemeSourcesChanged;

    public ThemeSourceCollection(RadThemeManager ownerManager)
    {
      this.ownerManager = ownerManager;
    }

    public ThemeSourceCollection(ThemeSourceCollection value, RadThemeManager ownerManager)
    {
      this.ownerManager = ownerManager;
      this.AddRange(value);
    }

    public ThemeSourceCollection(ThemeSource[] value, RadThemeManager ownerManager)
    {
      this.ownerManager = ownerManager;
      this.AddRange(value);
    }

    public ThemeSource this[int index]
    {
      get
      {
        return (ThemeSource) this.List[index];
      }
      set
      {
        this.List[index] = (object) value;
      }
    }

    public RadThemeManager OwnerManager
    {
      get
      {
        return this.ownerManager;
      }
    }

    public int Add(ThemeSource value)
    {
      return this.List.Add((object) value);
    }

    public void AddRange(ThemeSource[] value)
    {
      for (int index = 0; index < value.Length; ++index)
        this.Add(value[index]);
    }

    public void AddRange(ThemeSourceCollection value)
    {
      for (int index = 0; index < value.Count; ++index)
        this.Add(value[index]);
    }

    public bool Contains(ThemeSource value)
    {
      return this.List.Contains((object) value);
    }

    public void CopyTo(ThemeSource[] array, int index)
    {
      this.List.CopyTo((Array) array, index);
    }

    public int IndexOf(ThemeSource value)
    {
      return this.List.IndexOf((object) value);
    }

    public void Insert(int index, ThemeSource value)
    {
      this.List.Insert(index, (object) value);
    }

    public ThemeSourceCollection.ThemeSourceEnumerator GetEnumerator()
    {
      return new ThemeSourceCollection.ThemeSourceEnumerator(this);
    }

    public void Remove(ThemeSource value)
    {
      this.List.Remove((object) value);
    }

    protected override void OnInsertComplete(int index, object value)
    {
      ((ThemeSource) value).OwnerThemeManager = this.ownerManager;
      if (this.ThemeSourcesChanged != null)
        this.ThemeSourcesChanged(this, (ThemeSource) value, index, ThemeSourceChangeOperation.Inserted);
      base.OnInsertComplete(index, value);
    }

    protected override void OnSetComplete(int index, object oldValue, object newValue)
    {
      ((ThemeSource) newValue).OwnerThemeManager = this.ownerManager;
      if (this.ThemeSourcesChanged != null)
        this.ThemeSourcesChanged(this, (ThemeSource) oldValue, index, ThemeSourceChangeOperation.Replaced);
      base.OnSetComplete(index, oldValue, newValue);
    }

    protected override void OnRemoveComplete(int index, object value)
    {
      if (this.ThemeSourcesChanged != null)
        this.ThemeSourcesChanged(this, (ThemeSource) value, index, ThemeSourceChangeOperation.Removed);
      base.OnRemoveComplete(index, value);
    }

    protected override void OnClearComplete()
    {
      if (this.ThemeSourcesChanged != null)
        this.ThemeSourcesChanged(this, (ThemeSource) null, -1, ThemeSourceChangeOperation.Cleared);
      base.OnClearComplete();
    }

    public class ThemeSourceEnumerator : IEnumerator
    {
      private IEnumerator baseEnumerator;
      private IEnumerable temp;

      public ThemeSourceEnumerator(ThemeSourceCollection mappings)
      {
        this.temp = (IEnumerable) mappings;
        this.baseEnumerator = this.temp.GetEnumerator();
      }

      public ThemeSource Current
      {
        get
        {
          return (ThemeSource) this.baseEnumerator.Current;
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
