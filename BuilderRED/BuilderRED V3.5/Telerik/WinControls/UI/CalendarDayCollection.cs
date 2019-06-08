// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarDayCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class CalendarDayCollection : IEnumerable<RadCalendarDay>, IList, ICollection, IEnumerable, ICloneable
  {
    private List<RadCalendarDay> children = new List<RadCalendarDay>();
    private RadCalendar calendar;

    public CalendarDayCollection()
    {
    }

    public CalendarDayCollection(RadCalendar owner, CalendarDayCollection days)
      : this(owner, days, 10)
    {
    }

    internal CalendarDayCollection(CalendarDayCollection days)
      : this((RadCalendar) null, days, 10)
    {
    }

    internal CalendarDayCollection(RadCalendar calnedar)
      : this(calnedar, (CalendarDayCollection) null, 10)
    {
    }

    internal CalendarDayCollection(RadCalendar owner, CalendarDayCollection days, int capacity)
    {
      this.children.Capacity = capacity;
      this.calendar = owner;
      if (days == null)
        return;
      this.AddRange(days);
    }

    protected internal virtual void UpdateOwnerShip(RadCalendarDay day)
    {
      if (this == day.Owner || day == null)
        return;
      day.Owner = this;
    }

    protected internal virtual void UpdateVisuals()
    {
      if (this.calendar == null)
        return;
      this.calendar.CalendarElement.RefreshVisuals(true);
    }

    public RadCalendarDay[] Find(DateTime key)
    {
      ArrayList arrayList = this.FindInternal(key, this);
      RadCalendarDay[] radCalendarDayArray = new RadCalendarDay[arrayList.Count];
      arrayList.CopyTo((Array) radCalendarDayArray, 0);
      return radCalendarDayArray;
    }

    private ArrayList FindInternal(
      DateTime key,
      CalendarDayCollection viewsCollectionToLookIn)
    {
      ArrayList arrayList = new ArrayList();
      if (viewsCollectionToLookIn == null || arrayList == null)
        return (ArrayList) null;
      for (int index = 0; index < viewsCollectionToLookIn.Count; ++index)
      {
        if (viewsCollectionToLookIn[index] != null && DateTime.Equals(key, viewsCollectionToLookIn[index].Date))
          arrayList.Add((object) viewsCollectionToLookIn[index]);
      }
      return arrayList;
    }

    public virtual int IndexOf(RadCalendarDay day)
    {
      return this.children.IndexOf(day);
    }

    public virtual CalendarDayCollection AddRange(CalendarDayCollection days)
    {
      int count = this.children.Count;
      this.children.AddRange((IEnumerable<RadCalendarDay>) days);
      for (int index = count; index < this.children.Count; ++index)
        this.UpdateOwnerShip(this.children[index]);
      this.UpdateVisuals();
      return days;
    }

    public virtual RadCalendarDay Add(RadCalendarDay day)
    {
      if (this.children.Contains(day))
        return (RadCalendarDay) null;
      this.UpdateOwnerShip(day);
      this.children.Add(day);
      this.UpdateVisuals();
      return day;
    }

    public virtual RadCalendarDay Add(DateTime day)
    {
      return this.Add(new RadCalendarDay(day));
    }

    public virtual CalendarDayCollection AddRange(IEnumerable<DateTime> days)
    {
      CalendarDayCollection days1 = new CalendarDayCollection();
      foreach (DateTime day in days)
        days1.Add(day);
      return this.AddRange(days1);
    }

    public virtual IEnumerator<RadCalendarDay> GetEnumerator()
    {
      return (IEnumerator<RadCalendarDay>) this.children.GetEnumerator();
    }

    public virtual void Insert(int index, RadCalendarDay day)
    {
      if (this.children.Contains(day))
        return;
      this.UpdateOwnerShip(day);
      this.children.Insert(index, day);
      this.UpdateVisuals();
    }

    public virtual void Remove(RadCalendarDay day)
    {
      if (!this.children.Contains(day))
        return;
      day.Owner = (CalendarDayCollection) null;
      this.children.Remove(day);
      this.UpdateVisuals();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public void Clear()
    {
      this.children.CopyTo(new RadCalendarDay[this.children.Count]);
      this.children.Clear();
      for (int index = 0; index < this.children.Count; ++index)
        this.children[index].Owner = (CalendarDayCollection) null;
      this.UpdateVisuals();
    }

    public virtual RadCalendarDay[] ToArray()
    {
      return this.children.ToArray();
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.children.CopyTo(array as RadCalendarDay[], index);
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
      return this.children.IndexOf(this.Add(value as RadCalendarDay));
    }

    void IList.Clear()
    {
      this.Clear();
    }

    bool IList.Contains(object value)
    {
      return this.children.Contains(value as RadCalendarDay);
    }

    int IList.IndexOf(object value)
    {
      if (value is RadCalendarDay)
        return this.children.IndexOf(value as RadCalendarDay);
      if (value is string)
      {
        DateTime dateTime = DateTime.Parse((string) value);
        for (int index = 0; index < this.children.Count; ++index)
        {
          if (this.children[index].Date == dateTime)
            return index;
        }
        return -1;
      }
      if (!(value is DateTime))
        throw new ArgumentException("You may use only a RadCalendarDay object, date string or DateTime structure as index in this " + this.GetType().ToString() + " type collection.");
      for (int index = 0; index < this.children.Count; ++index)
      {
        if (this.children[index].Date == (DateTime) value)
          return index;
      }
      return -1;
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(index, value as RadCalendarDay);
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
      this.Remove(value as RadCalendarDay);
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
        this.children[index] = value as RadCalendarDay;
      }
    }

    public virtual int Count
    {
      get
      {
        return this.children.Count;
      }
    }

    public virtual RadCalendarDay this[int index]
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
        this.UpdateVisuals();
      }
    }

    public virtual RadCalendarDay this[object obj]
    {
      get
      {
        if (obj == null || obj is string && (string) obj == string.Empty)
          return (RadCalendarDay) null;
        int index = ((IList) this).IndexOf(obj);
        if (index < 0)
          return (RadCalendarDay) null;
        return this.children[index];
      }
      set
      {
        int index = ((IList) this).IndexOf(obj);
        if (this.children[index] == value)
          return;
        this.UpdateOwnerShip(value);
        this.children[index] = value;
        this.UpdateVisuals();
      }
    }

    public RadCalendar Calendar
    {
      get
      {
        return this.calendar;
      }
      set
      {
        this.calendar = value;
      }
    }

    public virtual CalendarDayCollection Clone()
    {
      return new CalendarDayCollection(this);
    }

    object ICloneable.Clone()
    {
      return (object) this.Clone();
    }
  }
}
