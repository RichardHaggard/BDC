// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MultiMonthView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class MultiMonthView : MonthView
  {
    private int previousViews;
    private int nextViews;

    public MultiMonthView(RadCalendar parent)
      : base(parent)
    {
    }

    public MultiMonthView(RadCalendar parent, DateTime inMonthDate)
      : this(parent, inMonthDate, (CalendarView) null)
    {
    }

    public MultiMonthView(RadCalendar parent, DateTime inMonthDate, CalendarView parentView)
      : base(parent, inMonthDate, parentView)
    {
    }

    public override bool IsMultipleView
    {
      get
      {
        return true;
      }
    }

    protected internal override int MonthsInView
    {
      get
      {
        this.EnsureChildViews();
        return this.Children.Count;
      }
    }

    protected internal override CalendarView CreateView()
    {
      return (CalendarView) new MultiMonthView(this.Calendar);
    }

    internal override void Initialize()
    {
      this.EnsureChildViews();
      this.Children.Clear();
      base.Initialize();
    }

    protected internal override void EnsureRenderSettings()
    {
      if (!this.Equals((object) this.Calendar.DefaultView))
        throw new FormatException("Multiview mode is allowed only for top calendar views (not for their descendants).");
    }

    internal override string GetTitleContent()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string str1 = "-";
      string str2;
      string str3;
      if (this.IsRootView)
      {
        str2 = this.ViewStartDate.ToString(this.Calendar.TitleFormat, (IFormatProvider) this.Calendar.Culture);
        str3 = this.ViewEndDate.ToString(this.Calendar.TitleFormat, (IFormatProvider) this.Calendar.Culture);
      }
      else
      {
        str2 = this.ViewStartDate.ToString(this.TitleFormat, (IFormatProvider) this.Calendar.DateTimeFormat);
        str3 = this.ViewEndDate.ToString(this.TitleFormat, (IFormatProvider) this.Calendar.DateTimeFormat);
      }
      if (this.Calendar != null)
        str1 = this.Calendar.DateRangeSeparator;
      return str2 + str1 + str3;
    }

    protected internal override CalendarView CreateView(DateTime date)
    {
      if (date > this.Calendar.CurrentCalendar.MaxSupportedDateTime)
        date = this.Calendar.CurrentCalendar.AddMonths(this.Calendar.CurrentCalendar.MaxSupportedDateTime, this.MultiViewColumns * this.MultiViewRows);
      if (date < this.Calendar.CurrentCalendar.MinSupportedDateTime)
        date = this.Calendar.CurrentCalendar.MinSupportedDateTime;
      MultiMonthView multiMonthView = new MultiMonthView(this.Calendar, date);
      multiMonthView.Initialize((CalendarView) this);
      return (CalendarView) multiMonthView;
    }

    protected override void SetDateRange()
    {
      this.EnsureChildViews();
      this.InitializeMultiViewData();
      if (this.Children.Count <= 0)
        return;
      this.ViewStartDate = this.Children[0].ViewStartDate;
      this.ViewRenderStartDate = this.Children[0].ViewRenderStartDate;
      this.ViewEndDate = this.Children[this.Children.Count - 1].ViewEndDate;
      this.ViewRenderEndDate = this.Children[this.Children.Count - 1].ViewRenderEndDate;
    }

    protected virtual void InitializeFocusedViewPosition()
    {
      this.previousViews = this.Calendar.CurrentViewColumn + this.Calendar.CurrentViewRow * this.Calendar.MultiViewColumns;
      this.nextViews = this.Calendar.MultiViewColumns - (this.Calendar.CurrentViewColumn + 1) + (this.Calendar.MultiViewRows - (this.Calendar.CurrentViewRow + 1)) * this.Calendar.MultiViewColumns;
    }

    internal void InitializeMultiViewData()
    {
      this.InitializeFocusedViewPosition();
      if (this.previousViews < 0 || this.nextViews < 0)
        return;
      this.EnsureChildViews();
      this.Children.Clear();
      MonthView[] monthViewArray1 = new MonthView[this.previousViews];
      MonthView[] monthViewArray2 = new MonthView[this.nextViews];
      DateTime dateTime = this.EffectiveVisibleDate();
      MonthView monthView = new MonthView(this.Calendar, dateTime, (CalendarView) this);
      for (int index = 0; index < this.nextViews; ++index)
      {
        DateTime inMonthDate = this.Calendar.CurrentCalendar.AddMonths(dateTime, index + 1);
        monthViewArray2[index] = new MonthView(this.Calendar, inMonthDate, (CalendarView) this);
      }
      int num = 1;
      for (int index = this.previousViews - 1; 0 <= index; --index)
      {
        DateTime inMonthDate = this.Calendar.CurrentCalendar.AddMonths(dateTime, -num);
        monthViewArray1[index] = new MonthView(this.Calendar, inMonthDate, (CalendarView) this);
        ++num;
      }
      for (int index = 0; index < this.previousViews; ++index)
        this.Children.Add((CalendarView) monthViewArray1[index]);
      monthView.Initialize();
      this.Children.Add((CalendarView) monthView);
      for (int index = 0; index < this.nextViews; ++index)
        this.Children.Add((CalendarView) monthViewArray2[index]);
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
      if (this.Orientation == Orientation.Vertical)
        this.Calendar.FocusedDate = this.CurrentCalendar.AddDays(this.Calendar.FocusedDate, 1);
      else
        this.Calendar.FocusedDate = this.CurrentCalendar.AddWeeks(this.Calendar.FocusedDate, 1);
    }

    protected override void HandleUpKey(Keys keys)
    {
      if (this.Orientation == Orientation.Vertical)
        this.Calendar.FocusedDate = this.CurrentCalendar.AddDays(this.Calendar.FocusedDate, -1);
      else
        this.Calendar.FocusedDate = this.CurrentCalendar.AddWeeks(this.Calendar.FocusedDate, -1);
    }

    protected override void HandleLeftKey(Keys keys)
    {
      if ((keys & Keys.Control) == Keys.Control)
        this.HandlePageDownKey(keys);
      if (this.Orientation == Orientation.Vertical)
        this.Calendar.FocusedDate = this.CurrentCalendar.AddWeeks(this.Calendar.FocusedDate, -1);
      else
        this.Calendar.FocusedDate = this.CurrentCalendar.AddDays(this.Calendar.FocusedDate, -1);
    }

    protected override void HandleRightKey(Keys keys)
    {
      if ((keys & Keys.Control) == Keys.Control)
        this.HandlePageUpKey(keys);
      if (this.Orientation == Orientation.Vertical)
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
