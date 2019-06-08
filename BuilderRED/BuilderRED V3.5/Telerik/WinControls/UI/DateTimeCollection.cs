// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DateTimeCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class DateTimeCollection : IEnumerable<DateTime>, IList, ICollection, IEnumerable, ICloneable
  {
    private List<DateTime> children = new List<DateTime>();
    private RadCalendar calendar;
    private bool updating;

    public DateTimeCollection(RadCalendar calendar)
    {
      this.calendar = calendar;
    }

    public DateTimeCollection()
    {
    }

    public virtual int Count
    {
      get
      {
        return this.children.Count;
      }
    }

    public virtual DateTime this[int index]
    {
      get
      {
        return this.children[index];
      }
      set
      {
        if (this.calendar != null && !this.updating)
        {
          if (this.calendar.CallOnSelectionChanging(this, new List<DateTime>(1) { value }).Cancel)
            return;
        }
        if (!this.calendar.AllowMultipleSelect)
        {
          this.children.Clear();
          this.children[0] = value;
        }
        else
          this.children[index] = value;
        if (this.calendar == null || this.updating)
          return;
        this.calendar.CallOnSelectionChanged();
      }
    }

    internal List<DateTime> Dates
    {
      get
      {
        return this.children;
      }
      set
      {
        this.children = value;
      }
    }

    public void BeginUpdate()
    {
      this.updating = true;
    }

    public void EndUpdate()
    {
      this.updating = false;
    }

    public virtual int IndexOf(DateTime date)
    {
      return this.children.IndexOf(date);
    }

    public virtual DateTime Add(DateTime date)
    {
      if (!this.CanAdd(date))
        return DateTime.MinValue;
      if (this.calendar != null && !this.updating)
      {
        if (this.calendar.CallOnSelectionChanging(this, new List<DateTime>(1) { date }).Cancel)
          return DateTime.MinValue;
      }
      if (!this.calendar.AllowMultipleSelect)
        this.children.Clear();
      this.children.Add(date);
      if (this.calendar != null && !this.updating)
        this.calendar.CallOnSelectionChanged();
      return date;
    }

    public virtual IEnumerator<DateTime> GetEnumerator()
    {
      return (IEnumerator<DateTime>) this.children.GetEnumerator();
    }

    public virtual void Insert(int index, DateTime date)
    {
      if (!this.CanAdd(date))
        return;
      if (this.calendar != null && !this.updating)
      {
        if (this.calendar.CallOnSelectionChanging(this, new List<DateTime>(1) { date }).Cancel)
          return;
      }
      if (this.calendar != null && !this.calendar.AllowMultipleSelect)
        this.children.Clear();
      this.children.Insert(index, date);
      if (this.calendar == null || this.updating)
        return;
      this.calendar.CallOnSelectionChanged();
    }

    public virtual bool CanAdd(DateTime date)
    {
      return !this.children.Contains(date) && (this.calendar == null || !(date < this.calendar.RangeMinDate) && !(date > this.calendar.RangeMaxDate) && (this.calendar.AllowMultipleSelect || this.children.Count <= 0));
    }

    public virtual void Remove(DateTime date)
    {
      if (!this.children.Contains(date) || this.calendar != null && !this.updating && this.calendar.CallOnSelectionChanging(this, new List<DateTime>()).Cancel)
        return;
      this.children.Remove(date);
      if (this.calendar == null || this.updating)
        return;
      this.calendar.CallOnSelectionChanged();
    }

    public void Clear()
    {
      if (this.calendar != null && !this.updating && this.calendar.CallOnSelectionChanging(this, new List<DateTime>()).Cancel)
        return;
      this.children.Clear();
      if (this.calendar != null && this.calendar.AllowMultipleSelect && !this.updating)
        this.calendar.CallOnSelectionChanged();
      this.UpdateOwnerVisuals();
    }

    public virtual void RemoveRange(int index, int count)
    {
      if (count == 0)
        return;
      for (int index1 = index; index1 < count && (this.calendar == null || this.updating || !this.calendar.CallOnSelectionChanging(this, new List<DateTime>()).Cancel); ++index1)
      {
        this.children.RemoveAt(index1);
        if (this.calendar != null && !this.updating)
          this.calendar.CallOnSelectionChanged();
      }
    }

    public virtual void RemoveRange(DateTime[] dates)
    {
      if (this.calendar == null || !this.updating && this.calendar.CallOnSelectionChanging(this, new List<DateTime>()).Cancel)
        return;
      foreach (DateTime date in dates)
      {
        if (this.children.Contains(date))
          this.children.Remove(date);
      }
      if (this.updating)
        return;
      this.calendar.CallOnSelectionChanged();
    }

    public virtual void AddRange(DateTime[] inputItems)
    {
      if (inputItems == null)
        throw new ArgumentNullException();
      if (inputItems.Length == 0)
        return;
      if (!this.calendar.AllowMultipleSelect)
      {
        this.Add(inputItems[0]);
      }
      else
      {
        if (!this.updating && this.calendar.CallOnSelectionChanging(this, new List<DateTime>((IEnumerable<DateTime>) inputItems)).Cancel)
          return;
        for (int index = 0; index < inputItems.Length; ++index)
        {
          if (this.CanAdd(inputItems[index]))
            this.children.Add(inputItems[index]);
        }
        if (this.updating)
          return;
        this.calendar.CallOnSelectionChanged();
      }
    }

    public virtual bool Contains(DateTime value)
    {
      return this.children.Contains(value);
    }

    public virtual DateTime[] ToArray()
    {
      return this.children.ToArray();
    }

    protected void UpdateOwnerVisuals()
    {
      if (this.calendar == null)
        return;
      this.calendar.CalendarElement.RefreshVisuals(true);
      this.calendar.CalendarElement.Invalidate();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.children.CopyTo(array as DateTime[], index);
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
      return this.children.IndexOf(this.Add((DateTime) value));
    }

    void IList.Clear()
    {
      this.Clear();
    }

    bool IList.Contains(object value)
    {
      return this.children.Contains((DateTime) value);
    }

    int IList.IndexOf(object value)
    {
      return this.children.IndexOf((DateTime) value);
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(index, (DateTime) value);
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
      this.Remove((DateTime) value);
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
        this[index] = (DateTime) value;
      }
    }

    public virtual DateTimeCollection Clone()
    {
      DateTimeCollection dateTimeCollection = new DateTimeCollection(this.calendar);
      for (int index = 0; index < this.Dates.Count; ++index)
        dateTimeCollection.Dates.Add(this.Dates[index]);
      return dateTimeCollection;
    }

    object ICloneable.Clone()
    {
      return (object) this.Clone();
    }
  }
}
