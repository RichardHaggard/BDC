// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadShortcutCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Telerik.WinControls
{
  public class RadShortcutCollection : IList<RadShortcut>, ICollection<RadShortcut>, IEnumerable<RadShortcut>, IEnumerable
  {
    public const char ShortcutDelimiter = ',';
    private List<RadShortcut> list;
    private IShortcutProvider owner;

    public RadShortcutCollection(IShortcutProvider owner)
    {
      this.owner = owner;
      this.list = new List<RadShortcut>();
    }

    public IShortcutProvider Owner
    {
      get
      {
        return this.owner;
      }
    }

    public string GetDisplayText()
    {
      int count = this.list.Count;
      if (count == 0)
        return "None";
      StringBuilder stringBuilder = new StringBuilder(20);
      for (int index = 0; index < count; ++index)
      {
        stringBuilder.Append(this.list[index].GetDisplayText());
        stringBuilder.Append(',');
      }
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      return stringBuilder.ToString();
    }

    private void OnCollectionChanged()
    {
      this.owner.OnShortcutsChanged();
    }

    public int IndexOf(RadShortcut item)
    {
      return this.list.IndexOf(item);
    }

    public void Insert(int index, RadShortcut item)
    {
      this.list.Insert(index, item);
      this.OnCollectionChanged();
    }

    public void RemoveAt(int index)
    {
      this.list.RemoveAt(index);
      this.OnCollectionChanged();
    }

    public RadShortcut this[int index]
    {
      get
      {
        return this.list[index];
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Value");
        this.list[index] = value;
        this.OnCollectionChanged();
      }
    }

    public void Add(RadShortcut item)
    {
      this.list.Add(item);
      this.OnCollectionChanged();
    }

    public void Clear()
    {
      this.list.Clear();
      this.OnCollectionChanged();
    }

    public bool Contains(RadShortcut item)
    {
      return this.list.Contains(item);
    }

    public void CopyTo(RadShortcut[] array, int arrayIndex)
    {
      this.list.CopyTo(array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return this.list.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool Remove(RadShortcut item)
    {
      bool flag = this.list.Remove(item);
      if (flag)
        this.OnCollectionChanged();
      return flag;
    }

    public IEnumerator<RadShortcut> GetEnumerator()
    {
      return (IEnumerator<RadShortcut>) this.list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.list.GetEnumerator();
    }
  }
}
