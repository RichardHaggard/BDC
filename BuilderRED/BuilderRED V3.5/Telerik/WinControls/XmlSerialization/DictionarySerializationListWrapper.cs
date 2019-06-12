// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.DictionarySerializationListWrapper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls.XmlSerialization
{
  public class DictionarySerializationListWrapper : IList, ICollection, IEnumerable
  {
    private IDictionary wrapped;

    public DictionarySerializationListWrapper(IDictionary toWrap)
    {
      this.wrapped = toWrap;
    }

    public int Add(object value)
    {
      if (value != null && typeof (DictionaryEntry).IsAssignableFrom(value.GetType()))
      {
        DictionaryEntry dictionaryEntry = (DictionaryEntry) value;
        this.wrapped[dictionaryEntry.Key] = dictionaryEntry.Value;
      }
      return this.wrapped.Count;
    }

    public void Clear()
    {
      this.wrapped.Clear();
    }

    public bool Contains(object value)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public int IndexOf(object value)
    {
      int num = 0;
      foreach (object obj in this.wrapped)
      {
        if (obj == value)
          return num;
        ++num;
      }
      return -1;
    }

    public void Insert(int index, object value)
    {
      this.Add(value);
    }

    public bool IsFixedSize
    {
      get
      {
        return this.wrapped.IsFixedSize;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.wrapped.IsReadOnly;
      }
    }

    public void Remove(object value)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public void RemoveAt(int index)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public object this[int index]
    {
      get
      {
        int num = 0;
        foreach (object obj in this.wrapped)
        {
          if (num == index)
            return obj;
          ++num;
        }
        throw new ArgumentOutOfRangeException(nameof (index));
      }
      set
      {
        DictionaryEntry dictionaryEntry = (DictionaryEntry) value;
        this.wrapped[dictionaryEntry.Key] = dictionaryEntry.Value;
      }
    }

    public void CopyTo(Array array, int index)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public int Count
    {
      get
      {
        return this.wrapped.Count;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return this.wrapped.IsSynchronized;
      }
    }

    public object SyncRoot
    {
      get
      {
        return this.wrapped.SyncRoot;
      }
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) this.wrapped.GetEnumerator();
    }
  }
}
