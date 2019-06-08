// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.WeakReferenceList`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class WeakReferenceList<T> : IEnumerable<T>, IEnumerable where T : class
  {
    private System.Collections.Generic.List<WeakReference> list;
    private bool autoCleanNonAlive;
    private bool trackResurrection;

    public WeakReferenceList()
    {
      this.list = new System.Collections.Generic.List<WeakReference>();
      this.autoCleanNonAlive = false;
      this.trackResurrection = false;
    }

    public WeakReferenceList(bool cleanNonAlive)
      : this()
    {
      this.autoCleanNonAlive = cleanNonAlive;
    }

    public WeakReferenceList(bool cleanNonAlive, bool trackResurrection)
      : this(cleanNonAlive)
    {
      this.trackResurrection = trackResurrection;
    }

    public void Add(T value)
    {
      this.InsertCore(this.Count, value);
    }

    public void Insert(int index, T value)
    {
      WeakReference weakReference = new WeakReference((object) value, this.trackResurrection);
      this.InsertCore(index, value);
    }

    protected virtual void InsertCore(int index, T value)
    {
      WeakReference weakReference = new WeakReference((object) value, this.trackResurrection);
      this.list.Insert(index, weakReference);
    }

    public int IndexOf(T value)
    {
      for (int index = this.list.Count - 1; index >= 0; --index)
      {
        WeakReference weakReference = this.list[index];
        if (weakReference.IsAlive)
        {
          if (weakReference.Target == (object) value)
            return index;
        }
        else if (this.autoCleanNonAlive)
          this.list.RemoveAt(index);
      }
      return -1;
    }

    public void Remove(T value)
    {
      int index = this.IndexOf(value);
      if (index < 0)
        return;
      this.list.RemoveAt(index);
    }

    public void Clear()
    {
      this.list.Clear();
    }

    public void RemoveAt(int index)
    {
      if (index < 0 || index >= this.list.Count)
        throw new IndexOutOfRangeException();
      this.list.RemoveAt(index);
    }

    public void CleanNonAlive()
    {
      for (int index = this.list.Count - 1; index >= 0; --index)
      {
        if (!this.list[index].IsAlive)
          this.list.RemoveAt(index);
      }
    }

    public T this[int index]
    {
      get
      {
        if (index < 0 || index >= this.list.Count)
          throw new IndexOutOfRangeException();
        return (T) this.list[index].Target;
      }
      set
      {
        if (index < 0 || index >= this.list.Count)
          throw new IndexOutOfRangeException();
        this.list[index] = new WeakReference((object) value, this.trackResurrection);
      }
    }

    public IEnumerator<T> GetEnumerator()
    {
      for (int i = 0; i < this.list.Count; ++i)
      {
        WeakReference reference = this.list[i];
        if (!reference.IsAlive)
        {
          if (this.autoCleanNonAlive)
            this.list.RemoveAt(i--);
        }
        else
          yield return (T) reference.Target;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      for (int i = 0; i < this.list.Count; ++i)
      {
        WeakReference reference = this.list[i];
        if (!reference.IsAlive)
        {
          if (this.autoCleanNonAlive)
            this.list.RemoveAt(i--);
        }
        else
          yield return (object) (T) reference.Target;
      }
    }

    public bool AutoCleanNonAlive
    {
      get
      {
        return this.autoCleanNonAlive;
      }
      set
      {
        this.autoCleanNonAlive = value;
      }
    }

    public bool TrackRessurection
    {
      get
      {
        return this.trackResurrection;
      }
      set
      {
        this.trackResurrection = value;
      }
    }

    public int Count
    {
      get
      {
        return this.list.Count;
      }
    }

    protected System.Collections.Generic.List<WeakReference> List
    {
      get
      {
        return this.list;
      }
    }
  }
}
