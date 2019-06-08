// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalendarDay
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Telerik.WinControls.UI
{
  public class RadCalendarDay : INotifyPropertyChanged
  {
    private string toolTip = string.Empty;
    private RecurringEvents recurring = RecurringEvents.None;
    private DateTime date = DateTime.MinValue;
    private bool isWeekend;
    private bool isToday;
    private bool disabled;
    private bool selected;
    private bool selectable;
    private bool dirtyLayout;
    private bool dirtyPaint;
    private CalendarDayCollection owner;
    private RadHostItem templateItem;
    private Image image;
    private bool isTemplateSet;

    public RadCalendarDay()
    {
    }

    public RadCalendarDay(DateTime date)
      : this(date, (CalendarDayCollection) null)
    {
      this.date = date;
    }

    public RadCalendarDay(CalendarDayCollection owner)
      : this(DateTime.MinValue, owner)
    {
    }

    public RadCalendarDay(DateTime date, CalendarDayCollection owner)
    {
      this.owner = owner;
      this.date = date;
    }

    internal virtual bool IsTemplateSet
    {
      get
      {
        return this.isTemplateSet;
      }
      set
      {
        this.isTemplateSet = value;
        this.OnNotifyPropertyChanged(nameof (IsTemplateSet));
      }
    }

    [Browsable(true)]
    [NotifyParentProperty(true)]
    [DefaultValue(null)]
    public virtual Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
        this.OnNotifyPropertyChanged(nameof (Image));
      }
    }

    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    [Browsable(true)]
    public virtual RadHostItem TemplateItem
    {
      get
      {
        return this.templateItem;
      }
      set
      {
        this.templateItem = value;
        this.OnNotifyPropertyChanged(nameof (TemplateItem));
        if (this.owner == null || this.owner.Calendar == null)
          return;
        this.owner.Calendar.CalendarElement.CalendarVisualElement.RefreshVisuals();
      }
    }

    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    public virtual DateTime Date
    {
      get
      {
        return this.date;
      }
      set
      {
        this.date = value;
        this.OnNotifyPropertyChanged(nameof (Date));
      }
    }

    [DefaultValue(true)]
    [NotifyParentProperty(true)]
    public bool Selectable
    {
      get
      {
        return this.selectable;
      }
      set
      {
        if (this.selectable == value)
          return;
        this.selectable = value;
        this.OnNotifyPropertyChanged(nameof (Selectable));
        this.DirtyPaint = true;
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue(false)]
    public bool Selected
    {
      get
      {
        return this.selected;
      }
      set
      {
        if (this.selected == value)
          return;
        this.selected = value;
        this.OnNotifyPropertyChanged(nameof (Selected));
        this.DirtyPaint = true;
      }
    }

    [DefaultValue(false)]
    [NotifyParentProperty(true)]
    public bool Disabled
    {
      get
      {
        return this.disabled;
      }
      set
      {
        if (this.disabled == value)
          return;
        this.disabled = value;
        this.OnNotifyPropertyChanged(nameof (Disabled));
        this.DirtyPaint = true;
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue(false)]
    public bool IsToday
    {
      get
      {
        return this.isToday;
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue(RecurringEvents.None)]
    public RecurringEvents Recurring
    {
      get
      {
        return this.recurring;
      }
      set
      {
        if (this.recurring == value)
          return;
        this.recurring = value;
        this.OnNotifyPropertyChanged(nameof (Recurring));
        this.DirtyPaint = true;
      }
    }

    [DefaultValue(false)]
    [NotifyParentProperty(true)]
    public bool IsWeekend
    {
      get
      {
        return this.isWeekend;
      }
    }

    [DefaultValue("")]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public string ToolTip
    {
      get
      {
        return this.toolTip;
      }
      set
      {
        if (!(this.toolTip != value))
          return;
        this.toolTip = value;
        this.OnNotifyPropertyChanged(nameof (ToolTip));
      }
    }

    [DefaultValue(null)]
    protected internal CalendarDayCollection Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        if (this.owner == value)
          return;
        this.owner = value;
        this.OnNotifyPropertyChanged(nameof (Owner));
      }
    }

    protected internal void SetToday(bool value)
    {
      if (this.isToday == value)
        return;
      this.isToday = value;
      this.DirtyPaint = true;
    }

    protected internal void SetWeekend(bool value)
    {
      if (this.isWeekend == value)
        return;
      this.isWeekend = value;
      this.DirtyPaint = true;
    }

    protected internal virtual RecurringEvents IsRecurring(
      DateTime compareTime,
      Calendar processCalendar)
    {
      if (this.Recurring != RecurringEvents.None)
      {
        switch (this.Recurring)
        {
          case RecurringEvents.DayInMonth:
            if (processCalendar.GetDayOfMonth(compareTime).Equals(processCalendar.GetDayOfMonth(this.Date)))
              return this.Recurring;
            break;
          case RecurringEvents.DayAndMonth:
            int dayOfMonth1 = processCalendar.GetDayOfMonth(compareTime);
            int dayOfMonth2 = processCalendar.GetDayOfMonth(this.Date);
            int month1 = processCalendar.GetMonth(compareTime);
            int month2 = processCalendar.GetMonth(this.Date);
            if (dayOfMonth1.Equals(dayOfMonth2) && month1.Equals(month2))
              return this.Recurring;
            break;
          case RecurringEvents.Week:
            if (processCalendar.GetDayOfWeek(compareTime).Equals((object) processCalendar.GetDayOfWeek(this.Date)))
              return this.Recurring;
            break;
          case RecurringEvents.WeekAndMonth:
            DayOfWeek dayOfWeek1 = processCalendar.GetDayOfWeek(compareTime);
            DayOfWeek dayOfWeek2 = processCalendar.GetDayOfWeek(this.Date);
            int month3 = processCalendar.GetMonth(compareTime);
            int month4 = processCalendar.GetMonth(this.Date);
            if (dayOfWeek1.Equals((object) dayOfWeek2) && month3.Equals(month4))
              return this.Recurring;
            break;
          case RecurringEvents.Today:
            if (compareTime.Equals(DateTime.Today))
              return this.Recurring;
            break;
        }
      }
      return RecurringEvents.None;
    }

    public static DateTime TruncateTimeComponent(DateTime value)
    {
      return value.Subtract(value.TimeOfDay);
    }

    public static RadCalendarDay CreateDay(DateTime date)
    {
      return new RadCalendarDay(date);
    }

    protected internal static RadCalendarDay CreateDay(
      DateTime date,
      CalendarDayCollection owner)
    {
      return new RadCalendarDay(date) { Owner = owner };
    }

    [Browsable(false)]
    internal bool DirtyLayout
    {
      get
      {
        return this.dirtyLayout;
      }
      set
      {
        if (this.dirtyLayout == value)
          return;
        this.dirtyLayout = value;
        this.OnNotifyPropertyChanged(nameof (DirtyLayout));
      }
    }

    [Browsable(false)]
    internal bool DirtyPaint
    {
      get
      {
        return this.dirtyPaint;
      }
      set
      {
        if (this.dirtyPaint == value)
          return;
        this.dirtyPaint = value;
        this.OnNotifyPropertyChanged(nameof (DirtyPaint));
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }
  }
}
