// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarViewCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class CalendarViewCollection : IEnumerable<CalendarView>, IList, ICollection, IEnumerable
  {
    private List<CalendarView> children = new List<CalendarView>();
    private CalendarView owner;
    private RadCalendar calendar;

    internal CalendarViewCollection(CalendarView owner)
      : this(owner.Calendar, owner, 10)
    {
    }

    internal CalendarViewCollection(RadCalendar calnedar)
      : this(calnedar, (CalendarView) null, 10)
    {
    }

    internal CalendarViewCollection(RadCalendar calnedar, CalendarView owner, int capacity)
    {
      this.children.Capacity = capacity;
      this.owner = owner;
      this.calendar = calnedar;
    }

    protected internal virtual void UpdateOwnerShip(CalendarView view)
    {
      if (this.calendar != view.Calendar || this.owner != view.Parent)
        view.Remove();
      view.Parent = this.owner;
      view.Calendar = this.calendar;
    }

    public CalendarView[] Find(string key, bool searchAllChildren)
    {
      ArrayList arrayList = this.FindInternal(key, searchAllChildren, this);
      CalendarView[] calendarViewArray = new CalendarView[arrayList.Count];
      arrayList.CopyTo((Array) calendarViewArray, 0);
      return calendarViewArray;
    }

    private ArrayList FindInternal(
      string key,
      bool searchAllChildren,
      CalendarViewCollection viewsCollectionToLookIn)
    {
      ArrayList arrayList = new ArrayList();
      if (viewsCollectionToLookIn == null || arrayList == null)
        return (ArrayList) null;
      for (int index = 0; index < viewsCollectionToLookIn.Count; ++index)
      {
        if (viewsCollectionToLookIn[index] != null && WindowsFormsUtils.SafeCompareStrings(viewsCollectionToLookIn[index].Name, key, true))
        {
          arrayList.Add((object) viewsCollectionToLookIn[index]);
          if (searchAllChildren && viewsCollectionToLookIn[index].Children.Count > 0)
            arrayList.AddRange((ICollection) this.FindInternal(key, searchAllChildren, viewsCollectionToLookIn[index].Children));
        }
      }
      return arrayList;
    }

    public virtual int IndexOf(CalendarView view)
    {
      return this.children.IndexOf(view);
    }

    public virtual CalendarViewCollection AddRange(
      CalendarViewCollection viewsCollection)
    {
      int count = this.children.Count;
      this.children.AddRange((IEnumerable<CalendarView>) viewsCollection);
      for (int index = count; index < this.children.Count; ++index)
        this.UpdateOwnerShip(this.children[index]);
      return viewsCollection;
    }

    public virtual CalendarView Add(CalendarView view)
    {
      if (this.children.Contains(view))
        return (CalendarView) null;
      this.UpdateOwnerShip(view);
      this.children.Add(view);
      return view;
    }

    public virtual IEnumerator<CalendarView> GetEnumerator()
    {
      return (IEnumerator<CalendarView>) this.children.GetEnumerator();
    }

    public virtual void Insert(int index, CalendarView view)
    {
      if (this.children.Contains(view))
        return;
      this.UpdateOwnerShip(view);
      this.children.Insert(index, view);
    }

    public virtual void Remove(CalendarView view)
    {
      if (!this.children.Contains(view))
        return;
      view.Parent = (CalendarView) null;
      view.Calendar = (RadCalendar) null;
      this.children.Remove(view);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public void Clear()
    {
      CalendarView[] array = new CalendarView[this.children.Count];
      this.children.CopyTo(array);
      this.children.Clear();
      for (int index = 0; index < array.Length; ++index)
      {
        array[index].Parent = (CalendarView) null;
        array[index].Calendar = (RadCalendar) null;
      }
    }

    public RadCalendar Calendar
    {
      get
      {
        if (this.owner != null)
          return this.owner.Calendar;
        return this.calendar;
      }
    }

    public CalendarView Owner
    {
      get
      {
        return this.owner;
      }
    }

    public virtual int Count
    {
      get
      {
        return this.children.Count;
      }
    }

    public virtual CalendarView this[int index]
    {
      get
      {
        return this.children[index];
      }
      set
      {
        if (this.children[index] == value)
          return;
        this.UpdateOwnerShip(value);
        this.children[index] = value;
      }
    }

    public virtual CalendarView this[string str]
    {
      get
      {
        return (CalendarView) null;
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.children.CopyTo(array as CalendarView[], index);
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) null;
      }
    }

    int IList.Add(object value)
    {
      return this.children.IndexOf(this.Add(value as CalendarView));
    }

    void IList.Clear()
    {
      this.Clear();
    }

    bool IList.Contains(object value)
    {
      return this.children.Contains(value as CalendarView);
    }

    int IList.IndexOf(object value)
    {
      return this.children.IndexOf(value as CalendarView);
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(index, value as CalendarView);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    void IList.Remove(object value)
    {
      this.Remove(value as CalendarView);
    }

    void IList.RemoveAt(int index)
    {
      this.Remove(this.children[index]);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this.children[index];
      }
      set
      {
        this.children[index] = value as CalendarView;
      }
    }
  }
}
