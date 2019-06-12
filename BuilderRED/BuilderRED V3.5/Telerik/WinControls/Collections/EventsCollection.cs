// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Collections.EventsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;

namespace Telerik.WinControls.Collections
{
  internal class EventsCollection : IDictionary, ICollection, IEnumerable, ICloneable
  {
    private DictionaryEntry[] entries;

    public EventsCollection()
    {
      this.entries = (DictionaryEntry[]) null;
    }

    public EventsCollection(EventsCollection events)
    {
      if (events == null)
        throw new ArgumentNullException("template");
      DictionaryEntry[] entries = events.entries;
      if (entries == null)
      {
        this.entries = (DictionaryEntry[]) null;
      }
      else
      {
        int length = entries.Length;
        this.entries = new DictionaryEntry[length];
        Array.Copy((Array) entries, 0, (Array) this.entries, 0, length);
      }
    }

    public void AddEventHandler(object eventKey, Delegate handler)
    {
      this[eventKey] = (object) Delegate.Combine((Delegate) this[eventKey], handler);
    }

    public void RemoveEventHandler(object eventKey, Delegate handler)
    {
      this[eventKey] = (object) Delegate.Remove((Delegate) this[eventKey], handler);
    }

    public virtual void RaiseEvent(object eventKey, EventArgs e)
    {
      Delegate @delegate = (Delegate) this[eventKey];
      if ((object) @delegate == null)
        return;
      object[] objArray = new object[2]{ (object) this, (object) e };
      @delegate.DynamicInvoke(objArray);
    }

    public void Add(object key, object value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.Contains(key))
        throw new ArgumentException("An element with the same key already exists in the LightHashtable.", nameof (key));
      if (this.entries == null)
      {
        this.entries = new DictionaryEntry[1];
      }
      else
      {
        DictionaryEntry[] dictionaryEntryArray = new DictionaryEntry[this.entries.Length + 1];
        this.entries.CopyTo((Array) dictionaryEntryArray, 1);
        this.entries = dictionaryEntryArray;
      }
      this.entries[0] = new DictionaryEntry(key, value);
    }

    public void Clear()
    {
      this.entries = (DictionaryEntry[]) null;
    }

    public object Clone()
    {
      return (object) new EventsCollection(this);
    }

    public bool Contains(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.entries != null)
      {
        for (int index = 0; index != this.entries.Length; ++index)
        {
          if (this.entries[index].Key == key)
            return true;
        }
      }
      return false;
    }

    public void CopyTo(Array array, int index)
    {
      if (this.entries == null)
        return;
      this.entries.CopyTo(array, index);
    }

    public IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) new EventsCollection.HashtableEnumerator(this.entries);
    }

    internal int IndexOf(object key)
    {
      if (this.entries != null)
      {
        for (int index = 0; index != this.entries.Length; ++index)
        {
          if (this.entries[index].Key == key)
            return index;
        }
      }
      return -1;
    }

    public void Remove(object key)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      int num = this.IndexOf(key);
      if (num == -1)
        return;
      int length1 = this.entries.Length;
      if (length1 == 1)
      {
        this.entries = (DictionaryEntry[]) null;
      }
      else
      {
        int length2 = length1 - 1;
        DictionaryEntry[] dictionaryEntryArray = new DictionaryEntry[length2];
        if (num == 0)
          Array.Copy((Array) this.entries, 1, (Array) dictionaryEntryArray, 0, length2);
        else if (num == length2)
        {
          Array.Copy((Array) this.entries, 0, (Array) dictionaryEntryArray, 0, length2);
        }
        else
        {
          Array.Copy((Array) this.entries, 0, (Array) dictionaryEntryArray, 0, num);
          Array.Copy((Array) this.entries, num + 1, (Array) dictionaryEntryArray, num, length2 - num);
        }
        this.entries = dictionaryEntryArray;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new EventsCollection.HashtableEnumerator(this.entries);
    }

    public int Count
    {
      get
      {
        if (this.entries != null)
          return this.entries.Length;
        return 0;
      }
    }

    public bool IsFixedSize
    {
      get
      {
        return false;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    public object this[object key]
    {
      get
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        int index = this.IndexOf(key);
        if (index != -1)
          return this.entries[index].Value;
        return (object) null;
      }
      set
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        int index = this.IndexOf(key);
        if (index == -1)
          this.Add(key, value);
        else
          this.entries[index].Value = value;
      }
    }

    public ICollection Keys
    {
      get
      {
        if (this.entries == null)
          return (ICollection) new object[0];
        int length = this.entries.Length;
        object[] objArray = new object[length];
        for (int index = 0; index < length; ++index)
          objArray[index] = this.entries[index].Key;
        return (ICollection) objArray;
      }
    }

    public object SyncRoot
    {
      get
      {
        if (this.entries != null)
          return this.entries.SyncRoot;
        return (object) this;
      }
    }

    public ICollection Values
    {
      get
      {
        if (this.entries == null)
          return (ICollection) new object[0];
        int length = this.entries.Length;
        object[] objArray = new object[length];
        for (int index = 0; index < length; ++index)
          objArray[index] = this.entries[index].Value;
        return (ICollection) objArray;
      }
    }

    private class HashtableEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private int currentIndex;
      private DictionaryEntry[] entries;

      public HashtableEnumerator(DictionaryEntry[] entries)
      {
        if (entries == null)
          throw new ArgumentNullException(nameof (entries));
        this.entries = (DictionaryEntry[]) entries.Clone();
        this.currentIndex = -1;
      }

      public bool MoveNext()
      {
        return ++this.currentIndex < this.entries.Length;
      }

      public void Reset()
      {
        this.currentIndex = -1;
      }

      public object Current
      {
        get
        {
          return (object) this.Entry;
        }
      }

      public DictionaryEntry Entry
      {
        get
        {
          if (this.currentIndex < 0 || this.currentIndex >= this.entries.Length)
            throw new InvalidOperationException("An attempt was made to position the enumerator before the first element of the collection or after the last element.");
          return this.entries[this.currentIndex];
        }
      }

      public object Key
      {
        get
        {
          return this.Entry.Key;
        }
      }

      public object Value
      {
        get
        {
          return this.Entry.Value;
        }
      }
    }
  }
}
