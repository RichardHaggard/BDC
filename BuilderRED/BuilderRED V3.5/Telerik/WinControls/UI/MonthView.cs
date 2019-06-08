// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MonthView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class MonthView : CalendarView
  {
    private DateTime viewInMonthDate = DateTime.MinValue;
    private int monthDays;

    public MonthView(RadCalendar parent)
      : base(parent)
    {
    }

    public MonthView(RadCalendar parent, DateTime inMonthDate)
      : this(parent, inMonthDate, (CalendarView) null)
    {
    }

    public MonthView(RadCalendar parent, DateTime inMonthDate, CalendarView parentView)
      : base(parent, parentView)
    {
      this.viewInMonthDate = inMonthDate;
      this.Initialize();
    }

    public override bool IsMultipleView
    {
      get
      {
        return false;
      }
    }

    protected internal virtual int MonthsInView
    {
      get
      {
        return 1;
      }
    }

    protected internal override CalendarView CreateView()
    {
      return (CalendarView) new MonthView(this.Calendar);
    }

    internal override void Initialize()
    {
      base.Initialize();
      this.SetDateRange();
      this.TitleFormat = "MMMM";
    }

    protected internal override void EnsureRenderSettings()
    {
      int num = 42;
      string empty = string.Empty;
      if (num % this.Columns != 0 || num % this.Rows != 0)
        empty += "The product of (MonthColumns x MonthRows) differs from 42 which is the correct value.";
      if (this.Columns < 7 && this.ShowColumnHeaders && (this.AllowColumnHeaderSelectors && this.Orientation == Orientation.Horizontal))
        empty += "The current combination of the properties: \n[ShowColumnHeaders, UseColumnHeadersAsSelectors, Orientation] and MonthColumns < 7 does not allow proper rendering of Telerik RadCalendar. Please correct.";
      if (this.Rows < 7 && this.ShowRowHeaders && (this.AllowRowHeaderSelectors && this.Orientation == Orientation.Vertical))
        empty += "The current combination of the properties: \n[ShowRowHeaders, UseRowHeadersAsSelectors, Orientation] and MonthRows < 7 does not allow proper rendering of Telerik RadCalendar. Please correct.";
      if (empty != string.Empty)
        throw new ArgumentException(empty);
    }

    internal override string GetTitleContent()
    {
      if (this.Calendar == null)
        return string.Empty;
      if (this.IsRootView)
        return this.EffectiveVisibleDate().ToString(this.Calendar.TitleFormat, (IFormatProvider) this.Calendar.Culture);
      return this.EffectiveVisibleDate().ToString(this.TitleFormat, (IFormatProvider) this.Calendar.DateTimeFormat);
    }

    protected internal virtual string GetDayHeaderString(int weekDay)
    {
      DateTimeFormatInfo dateTimeFormat = this.Calendar.DateTimeFormat;
      DayNameFormat dayNameFormat = this.Calendar.DayNameFormat;
      string empty = string.Empty;
      string str;
      switch (dayNameFormat)
      {
        case DayNameFormat.Full:
          str = dateTimeFormat.GetDayName((DayOfWeek) weekDay);
          break;
        case DayNameFormat.Short:
          str = dateTimeFormat.GetAbbreviatedDayName((DayOfWeek) weekDay);
          break;
        case DayNameFormat.FirstLetter:
          TextElementEnumerator elementEnumerator1 = StringInfo.GetTextElementEnumerator(dateTimeFormat.ShortestDayNames[weekDay]);
          elementEnumerator1.MoveNext();
          str = elementEnumerator1.Current.ToString();
          break;
        case DayNameFormat.FirstTwoLetters:
          TextElementEnumerator elementEnumerator2 = StringInfo.GetTextElementEnumerator(dateTimeFormat.ShortestDayNames[weekDay]);
          elementEnumerator2.MoveNext();
          StringBuilder stringBuilder = new StringBuilder(elementEnumerator2.Current.ToString());
          if (elementEnumerator2.MoveNext())
            stringBuilder.Append(elementEnumerator2.Current.ToString());
          str = stringBuilder.ToString();
          break;
        default:
          str = dateTimeFormat.ShortestDayNames[weekDay];
          break;
      }
      return str;
    }

    protected internal virtual string GetToolTip(RadCalendarDay calendarDay)
    {
      if (calendarDay.ToolTip != string.Empty)
        return calendarDay.ToolTip;
      return this.GetToolTip(calendarDay.Date);
    }

    protected internal virtual string GetToolTip(DateTime processedDate)
    {
      if (this.CellToolTipFormat != string.Empty)
        return processedDate.ToString(this.CellToolTipFormat, (IFormatProvider) this.Calendar.Culture);
      return processedDate.ToString();
    }

    protected internal virtual RadCalendarDay GetSpecialDay(DateTime processedDate)
    {
      if (this.Calendar == null)
        return (RadCalendarDay) null;
      RadCalendarDay specialDay = this.Calendar.SpecialDays[(object) processedDate];
      if (specialDay == null)
      {
        for (int index = 0; index < this.Calendar.SpecialDays.Count; ++index)
        {
          if (this.Calendar.SpecialDays[index].IsRecurring(processedDate, this.Calendar.CurrentCalendar) != RecurringEvents.None)
          {
            specialDay = this.Calendar.SpecialDays[index];
            break;
          }
        }
      }
      return specialDay;
    }

    protected override DateTime AddViewPeriods(DateTime startDate, int periods)
    {
      return this.Calendar.CurrentCalendar.AddMonths(startDate, periods * this.MonthsInView);
    }

    protected internal override CalendarView CreateView(DateTime date)
    {
      if (date > this.Calendar.CurrentCalendar.MaxSupportedDateTime)
        date = this.Calendar.CurrentCalendar.AddMonths(this.Calendar.CurrentCalendar.MaxSupportedDateTime, -1);
      if (date < this.Calendar.CurrentCalendar.MinSupportedDateTime)
        date = this.Calendar.CurrentCalendar.MinSupportedDateTime;
      MonthView monthView = new MonthView(this.Calendar, date);
      monthView.Initialize((CalendarView) this);
      return (CalendarView) monthView;
    }

    internal override CalendarView GetPreviousView(int steps)
    {
      if (this.ViewStartDate.Year * 12 + -(steps * this.MonthsInView) <= 0 || (this.ViewStartDate.Year - 1) * 12 + this.ViewStartDate.Month - steps * this.MonthsInView <= 0)
        return this.CreateView(this.ViewStartDate);
      DateTime date = this.ViewStartDate.AddMonths(-(steps * this.MonthsInView));
      if (date <= this.Calendar.RangeMinDate)
        return this.CreateView(this.Calendar.RangeMinDate.AddDays(1.0));
      return this.CreateView(date);
    }

    internal override CalendarView GetNextView(int steps)
    {
      DateTime date = this.ViewStartDate.AddMonths(steps * this.MonthsInView);
      if (date > this.Calendar.RangeMaxDate)
        return this.CreateView(this.Calendar.RangeMaxDate);
      return this.CreateView(date);
    }

    protected override void SetDateRange()
    {
      this.ViewStartDate = this.EffectiveVisibleDate();
      if (this.CurrentCalendar.MaxSupportedDateTime > this.ViewStartDate && this.CurrentCalendar.MinSupportedDateTime < this.ViewStartDate)
      {
        string s1 = this.ViewStartDate.ToString("yyyy", (IFormatProvider) this.Calendar.Culture.DateTimeFormat);
        string s2 = this.ViewStartDate.ToString("MM", (IFormatProvider) this.Calendar.Culture.DateTimeFormat);
        int result1 = this.ViewStartDate.Year;
        int.TryParse(s1, out result1);
        int result2 = this.ViewStartDate.Month;
        int.TryParse(s2, out result2);
        this.monthDays = this.CurrentCalendar.GetDaysInMonth(result1, result2);
      }
      if (this.CurrentCalendar.MinSupportedDateTime == this.ViewStartDate)
        this.ViewEndDate = this.CurrentCalendar.AddDays(this.ViewStartDate, this.monthDays);
      else
        this.ViewEndDate = this.CurrentCalendar.AddDays(this.ViewStartDate, this.monthDays - 1);
      this.ViewRenderStartDate = this.FirstCalendarDay(this.ViewStartDate);
      this.ViewRenderEndDate = this.CurrentCalendar.MaxSupportedDateTime.Ticks > new TimeSpan(this.Rows * this.Columns - 1, 0, 0, 0).Ticks + this.ViewStartDate.Ticks ? this.CurrentCalendar.AddDays(this.ViewStartDate, this.Rows * this.Columns - 1) : this.ViewRenderEndDate;
    }

    protected internal override DateTime EffectiveVisibleDate()
    {
      DateTime time = this.viewInMonthDate != DateTime.MinValue ? this.viewInMonthDate : base.EffectiveVisibleDate();
      return this.CurrentCalendar.AddDays(time, -(this.CurrentCalendar.GetDayOfMonth(time) - 1));
    }

    internal virtual DateTime FirstCalendarDay(DateTime visibleDate)
    {
      DateTime time = visibleDate;
      int num = (int) (this.Calendar.CurrentCalendar.GetDayOfWeek(time) - this.NumericFirstDayOfWeek());
      if (num <= 0)
        num += 7;
      if (time == this.CurrentCalendar.MinSupportedDateTime)
        return this.CurrentCalendar.MinSupportedDateTime;
      return this.Calendar.CurrentCalendar.AddDays(time, -num);
    }

    private string GetMonthName(int m, bool bFull)
    {
      if (bFull)
        return this.Calendar.DateTimeFormat.GetMonthName(m);
      return this.Calendar.DateTimeFormat.GetAbbreviatedMonthName(m);
    }

    private int NumericFirstDayOfWeek()
    {
      if (this.Calendar.FirstDayOfWeek != FirstDayOfWeek.Default)
        return (int) this.Calendar.FirstDayOfWeek;
      return (int) this.Calendar.DateTimeFormat.FirstDayOfWeek;
    }

    protected override void HandlePageDownKey(Keys keys)
    {
      this.Calendar.FocusedDate = this.CurrentCalendar.AddMonths(this.Calendar.FocusedDate, 1);
    }

    protected override void HandlePageUpKey(Keys keys)
    {
      this.Calendar.FocusedDate = this.CurrentCalendar.AddMonths(this.Calendar.FocusedDate, -1);
    }

    protected override void HandleEndKey(Keys keys)
    {
      this.Calendar.FocusedDate = this.ViewEndDate;
    }

    protected override void HandleHomeKey(Keys keys)
    {
      this.Calendar.FocusedDate = this.ViewStartDate;
    }

    protected override void HandleDownKey(Keys keys)
    {
      if (this.Calendar.FocusedDate == DateTime.MaxValue)
        return;
      if (this.Orientation == Orientation.Vertical)
        this.Calendar.FocusedDate = this.CurrentCalendar.AddDays(this.Calendar.FocusedDate, 1);
      else
        this.Calendar.FocusedDate = this.CurrentCalendar.AddWeeks(this.Calendar.FocusedDate, 1);
    }

    protected override void HandleUpKey(Keys keys)
    {
      if (this.Calendar.FocusedDate == DateTime.MinValue)
        return;
      if (this.Orientation == Orientation.Vertical)
        this.Calendar.FocusedDate = this.CurrentCalendar.AddDays(this.Calendar.FocusedDate, -1);
      else
        this.Calendar.FocusedDate = this.CurrentCalendar.AddWeeks(this.Calendar.FocusedDate, -1);
    }

    protected override void HandleLeftKey(Keys keys)
    {
      if ((keys & Keys.Control) == Keys.Control)
      {
        this.HandlePageUpKey(keys);
      }
      else
      {
        if (this.Calendar.FocusedDate == DateTime.MinValue)
          return;
        if (this.Orientation == Orientation.Vertical)
          this.Calendar.FocusedDate = this.CurrentCalendar.AddWeeks(this.Calendar.FocusedDate, -1);
        else
          this.Calendar.FocusedDate = this.CurrentCalendar.AddDays(this.Calendar.FocusedDate, -1);
      }
    }

    protected override void HandleRightKey(Keys keys)
    {
      if ((keys & Keys.Control) == Keys.Control)
        this.HandlePageDownKey(keys);
      else if (this.Orientation == Orientation.Vertical)
        this.Calendar.FocusedDate = this.CurrentCalendar.AddWeeks(this.Calendar.FocusedDate, 1);
      else
        this.Calendar.FocusedDate = this.CurrentCalendar.AddDays(this.Calendar.FocusedDate, 1);
    }

    protected override void ToggleSelection(Keys keys)
    {
      if (this.Calendar.ReadOnly)
        return;
      bool flag = this.Calendar.SelectedDates.Contains(this.Calendar.FocusedDate);
      List<DateTime> newDates = new List<DateTime>(1);
      if (!flag)
        newDates.Add(this.Calendar.FocusedDate);
      if (this.Calendar.CallOnSelectionChanging(this.Calendar.SelectedDates, newDates).Cancel)
        return;
      this.Calendar.SelectedDates.BeginUpdate();
      if (!this.Calendar.AllowMultipleSelect)
        this.Calendar.SelectedDates.Clear();
      if (flag)
        this.Calendar.SelectedDates.Remove(this.Calendar.FocusedDate);
      else
        this.Calendar.SelectedDates.Insert(0, this.Calendar.FocusedDate);
      this.Calendar.SelectedDates.EndUpdate();
      this.Calendar.CallOnSelectionChanged();
    }
  }
}
