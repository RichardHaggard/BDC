// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ItemsTraverser`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class ItemsTraverser<T> : ITraverser<T>, IEnumerator<T>, IDisposable, IEnumerator, IEnumerable
    where T : class
  {
    private IList<T> collection;
    private T current;
    private int position;
    protected ItemsTraverser<T> enumerator;

    public ItemsTraverser(IList<T> collection)
    {
      this.collection = collection;
      this.position = -1;
    }

    public IList<T> Collection
    {
      get
      {
        return this.collection;
      }
      set
      {
        this.collection = value;
        this.position = -1;
        if (this.enumerator == null)
          return;
        this.enumerator.ItemsNavigating -= new ItemsNavigatingEventHandler<T>(this.Enumerator_ItemsNavigating);
        this.enumerator = (ItemsTraverser<T>) null;
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.current;
      }
    }

    public virtual bool MoveNext()
    {
      while (this.MoveNextCore())
      {
        if (!this.OnItemsNavigating(this.current))
          return true;
      }
      return false;
    }

    public void Reset()
    {
      this.position = -1;
      this.current = default (T);
    }

    public T Current
    {
      get
      {
        return this.current;
      }
    }

    public object Position
    {
      get
      {
        return (object) this.position;
      }
      set
      {
        int num = (int) value;
        if (num >= 0 && num < this.collection.Count)
        {
          this.position = num;
          this.current = this.collection[this.position];
        }
        else
        {
          this.position = -1;
          this.current = default (T);
        }
      }
    }

    protected T InternalCurrent
    {
      get
      {
        return this.current;
      }
      set
      {
        this.current = value;
      }
    }

    protected int InternalPosition
    {
      get
      {
        return this.position;
      }
      set
      {
        this.position = value;
      }
    }

    public bool MovePrevious()
    {
      while (this.MovePreviousCore())
      {
        if (!this.OnItemsNavigating(this.current))
          return true;
      }
      return false;
    }

    public bool MoveToEnd()
    {
      if (this.position >= this.collection.Count - 1)
        return false;
      this.position = this.collection.Count - 1;
      this.current = this.collection[this.position];
      return true;
    }

    public virtual IEnumerator GetEnumerator()
    {
      if (this.enumerator == null)
      {
        this.enumerator = new ItemsTraverser<T>(this.collection);
        this.enumerator.ItemsNavigating += new ItemsNavigatingEventHandler<T>(this.Enumerator_ItemsNavigating);
      }
      this.enumerator.Position = (object) this.position;
      return (IEnumerator) this.enumerator;
    }

    protected void Enumerator_ItemsNavigating(object sender, ItemsNavigatingEventArgs<T> e)
    {
      e.SkipItem = this.OnItemsNavigating(e.Item);
    }

    public void Dispose()
    {
    }

    public event ItemsNavigatingEventHandler<T> ItemsNavigating;

    protected virtual bool OnItemsNavigating(T current)
    {
      if (this.ItemsNavigating == null)
        return false;
      ItemsNavigatingEventArgs<T> e = new ItemsNavigatingEventArgs<T>(current);
      this.ItemsNavigating((object) this, e);
      return e.SkipItem;
    }

    protected virtual bool MoveNextCore()
    {
      if (this.position >= this.collection.Count - 1)
        return false;
      ++this.position;
      this.current = this.collection[this.position];
      return true;
    }

    protected virtual bool MovePreviousCore()
    {
      if (this.position > 0)
      {
        --this.position;
        this.current = this.collection[this.position];
        return true;
      }
      this.position = -1;
      this.current = default (T);
      return false;
    }
  }
}
