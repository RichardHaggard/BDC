// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseGanttViewTimelineBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class BaseGanttViewTimelineBehavior
  {
    private GanttViewGraphicalViewElement graphicalViewElement;
    private string timelineUpperItemFormat;
    private string timelineLowerItemFormat;
    private Dictionary<TimeRange, int> rangesMinWidths;

    public GanttViewGraphicalViewElement GraphicalViewElement
    {
      get
      {
        return this.graphicalViewElement;
      }
      internal set
      {
        this.graphicalViewElement = value;
      }
    }

    [DefaultValue(null)]
    public virtual string TimelineUpperItemFormat
    {
      get
      {
        return this.timelineUpperItemFormat;
      }
      set
      {
        this.timelineUpperItemFormat = value;
        this.FillRangesMinWidths();
      }
    }

    [DefaultValue(null)]
    public virtual string TimelineLowerItemFormat
    {
      get
      {
        return this.timelineLowerItemFormat;
      }
      set
      {
        this.timelineLowerItemFormat = value;
        this.FillRangesMinWidths();
      }
    }

    public virtual DateTime AdjustedTimelineEnd
    {
      get
      {
        DateTime timelineEnd = this.GraphicalViewElement.TimelineEnd;
        switch (this.GraphicalViewElement.TimelineRange)
        {
          case TimeRange.Week:
            return timelineEnd.Date.AddDays(1.0);
          case TimeRange.Month:
            return timelineEnd.Date.AddDays(1.0);
          case TimeRange.Year:
            DateTime dateTime1 = timelineEnd.AddMonths(1);
            return new DateTime(dateTime1.Year, dateTime1.Month, 1);
          case TimeRange.YearHalves:
            int num1 = (DateTime.IsLeapYear(timelineEnd.Year) ? 366 : 365) / 2;
            if (timelineEnd.DayOfYear < num1)
              return new DateTime(timelineEnd.Year, 1, 1).AddDays((double) (num1 + 1));
            return new DateTime(timelineEnd.Year, 1, 1).AddYears(1);
          case TimeRange.YearQuarters:
            float num2 = (DateTime.IsLeapYear(timelineEnd.Year) ? 366f : 365f) / 4f;
            float num3 = num2 * 2f;
            float num4 = num2 + num3;
            if ((double) timelineEnd.DayOfYear > (double) num4)
              return new DateTime(timelineEnd.Year, 1, 1).AddYears(1);
            if ((double) timelineEnd.DayOfYear > (double) num3)
              return new DateTime(timelineEnd.Year, 1, 1).AddDays((double) num4);
            if ((double) timelineEnd.DayOfYear > (double) num2)
              return new DateTime(timelineEnd.Year, 1, 1).AddDays((double) num3);
            return new DateTime(timelineEnd.Year, 1, 1).AddDays((double) num2);
          case TimeRange.Day:
            DateTime dateTime2 = timelineEnd.AddHours(1.0);
            return new DateTime(dateTime2.Year, dateTime2.Month, dateTime2.Day, dateTime2.Hour, 0, 0);
          case TimeRange.DayHalfHours:
            if (timelineEnd.Minute <= 30)
              return new DateTime(timelineEnd.Year, timelineEnd.Month, timelineEnd.Day, timelineEnd.Hour, 30, 0);
            DateTime dateTime3 = timelineEnd.AddHours(1.0);
            return new DateTime(dateTime3.Year, dateTime3.Month, dateTime3.Day, dateTime3.Hour, 0, 0);
          case TimeRange.DayQuarterHours:
            if (timelineEnd.Minute > 45)
            {
              DateTime dateTime4 = timelineEnd.AddHours(1.0);
              return new DateTime(dateTime4.Year, dateTime4.Month, dateTime4.Day, dateTime4.Hour, 0, 0);
            }
            if (timelineEnd.Minute > 30)
              return new DateTime(timelineEnd.Year, timelineEnd.Month, timelineEnd.Day, timelineEnd.Hour, 45, 0);
            if (timelineEnd.Minute > 15)
              return new DateTime(timelineEnd.Year, timelineEnd.Month, timelineEnd.Day, timelineEnd.Hour, 30, 0);
            return new DateTime(timelineEnd.Year, timelineEnd.Month, timelineEnd.Day, timelineEnd.Hour, 15, 0);
          case TimeRange.Hour:
            DateTime dateTime5 = timelineEnd.AddMinutes(1.0);
            return new DateTime(dateTime5.Year, dateTime5.Month, dateTime5.Day, dateTime5.Hour, dateTime5.Minute + 1, 0);
          default:
            return timelineEnd;
        }
      }
    }

    public virtual DateTime AdjustedTimelineStart
    {
      get
      {
        DateTime timelineStart = this.GraphicalViewElement.TimelineStart;
        switch (this.GraphicalViewElement.TimelineRange)
        {
          case TimeRange.Week:
            return timelineStart.Date;
          case TimeRange.Month:
            return timelineStart.Date;
          case TimeRange.Year:
            return new DateTime(timelineStart.Year, timelineStart.Month, 1);
          case TimeRange.YearHalves:
            int num1 = (DateTime.IsLeapYear(timelineStart.Year) ? 366 : 365) / 2;
            if (timelineStart.DayOfYear < num1)
              return new DateTime(timelineStart.Year, 1, 1);
            return new DateTime(timelineStart.Year, 1, 1).AddDays((double) num1);
          case TimeRange.YearQuarters:
            float num2 = (DateTime.IsLeapYear(timelineStart.Year) ? 366f : 365f) / 4f;
            float num3 = num2 * 2f;
            float num4 = num2 + num3;
            if ((double) timelineStart.DayOfYear < (double) num2)
              return new DateTime(timelineStart.Year, 1, 1);
            if ((double) timelineStart.DayOfYear < (double) num3)
              return new DateTime(timelineStart.Year, 1, 1).AddDays((double) num2 + 1.0);
            if ((double) timelineStart.DayOfYear < (double) num4)
              return new DateTime(timelineStart.Year, 1, 1).AddDays((double) num3 + 1.0);
            return new DateTime(timelineStart.Year, 1, 1).AddDays((double) num4 + 1.0);
          case TimeRange.Day:
            return new DateTime(timelineStart.Year, timelineStart.Month, timelineStart.Day, timelineStart.Hour, 0, 0);
          case TimeRange.DayHalfHours:
            return new DateTime(timelineStart.Year, timelineStart.Month, timelineStart.Day, timelineStart.Hour, timelineStart.Minute >= 30 ? 30 : 0, 0);
          case TimeRange.DayQuarterHours:
            if (timelineStart.Minute < 15)
              return new DateTime(timelineStart.Year, timelineStart.Month, timelineStart.Day, timelineStart.Hour, 0, 0);
            if (timelineStart.Minute < 30)
              return new DateTime(timelineStart.Year, timelineStart.Month, timelineStart.Day, timelineStart.Hour, 15, 0);
            if (timelineStart.Minute < 45)
              return new DateTime(timelineStart.Year, timelineStart.Month, timelineStart.Day, timelineStart.Hour, 30, 0);
            return new DateTime(timelineStart.Year, timelineStart.Month, timelineStart.Day, timelineStart.Hour, 45, 0);
          case TimeRange.Hour:
            return new DateTime(timelineStart.Year, timelineStart.Month, timelineStart.Day, timelineStart.Hour, timelineStart.Minute, 0);
          default:
            return timelineStart;
        }
      }
    }

    protected Dictionary<TimeRange, int> RangesMinWidths
    {
      get
      {
        if (this.rangesMinWidths == null)
        {
          this.rangesMinWidths = new Dictionary<TimeRange, int>();
          this.FillRangesMinWidths();
        }
        return this.rangesMinWidths;
      }
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItems(
      TimeRange range)
    {
      switch (range)
      {
        case TimeRange.Week:
          return this.BuildTimelineDataItemsForWeekRange();
        case TimeRange.Month:
          return this.BuildTimelineDataItemsForMonthRange();
        case TimeRange.Year:
          return this.BuildTimelineDataItemsForYearRange();
        case TimeRange.YearHalves:
          return this.BuildTimelineDataItemsForYearHalvesRange();
        case TimeRange.YearQuarters:
          return this.BuildTimelineDataItemsForYearQuartersRange();
        case TimeRange.Day:
          return this.BuildTimelineDataItemsForDayRange();
        case TimeRange.DayHalfHours:
          return this.BuildTimelineDataItemsForDayHalfHoursRange();
        case TimeRange.DayQuarterHours:
          return this.BuildTimelineDataItemsForDayQuarterHoursRange();
        case TimeRange.Hour:
          return this.BuildTimelineDataItemsForHourRange();
        default:
          return (IList<GanttViewTimelineDataItem>) new List<GanttViewTimelineDataItem>();
      }
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForWeekRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      DateTime dateTime = adjustedTimelineStart.Date;
      int num1 = this.WeekNumber(adjustedTimelineStart);
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(adjustedTimelineStart.Date, adjustedTimelineStart.AddDays(1.0), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddDays(1.0);
        dateTime = dateTime.AddDays(1.0);
        int num2 = this.WeekNumber(dateTime);
        if (num2 != num1 && dateTime.AddDays(1.0) <= adjustedTimelineEnd)
        {
          num1 = num2;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForMonthRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      DateTime dateTime = adjustedTimelineStart.Date;
      int num = adjustedTimelineStart.Month;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(adjustedTimelineStart.Date, adjustedTimelineStart.AddDays(1.0), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddDays(1.0);
        dateTime = dateTime.AddDays(1.0);
        int month = dateTime.Month;
        if (month != num && dateTime.AddDays(1.0) <= adjustedTimelineEnd)
        {
          num = month;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForYearRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      DateTime dateTime = adjustedTimelineStart.Date;
      int num = dateTime.Year;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(dateTime.Date, dateTime.AddMonths(1), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddMonths(1);
        dateTime = dateTime.AddMonths(1);
        int year = dateTime.Year;
        if (year != num && dateTime <= adjustedTimelineEnd)
        {
          num = year;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForYearHalvesRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      double num1 = Math.Ceiling((DateTime.IsLeapYear(adjustedTimelineStart.Year) ? 366.0 : 365.0) / 2.0);
      DateTime dateTime = adjustedTimelineStart.Date;
      int num2 = adjustedTimelineStart.Year;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(adjustedTimelineStart.Date, adjustedTimelineStart.AddDays(num1), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddDays(num1);
        dateTime = dateTime.AddDays(num1);
        int year = dateTime.Year;
        if (year != num2 && dateTime.AddDays(1.0) <= adjustedTimelineEnd)
        {
          num1 = (DateTime.IsLeapYear(year) ? 366.0 : 365.0) / 2.0;
          num2 = year;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForYearQuartersRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      double num1 = (DateTime.IsLeapYear(adjustedTimelineStart.Year) ? 366.0 : 365.0) / 4.0;
      DateTime dateTime = adjustedTimelineStart.Date;
      int num2 = adjustedTimelineStart.Year;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(adjustedTimelineStart.Date, adjustedTimelineStart.Date.AddDays((double) (int) num1), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddDays(num1);
        dateTime = dateTime.AddDays(num1);
        int year = dateTime.Year;
        if (year != num2 && dateTime <= adjustedTimelineEnd)
        {
          num1 = (DateTime.IsLeapYear(year) ? 366.0 : 365.0) / 4.0;
          num2 = year;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForDayRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      DateTime dateTime = adjustedTimelineStart;
      int num = adjustedTimelineStart.Day;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(adjustedTimelineStart, adjustedTimelineStart.AddHours(1.0), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddHours(1.0);
        dateTime = dateTime.AddHours(1.0);
        int day = dateTime.Day;
        if (day != num && dateTime.AddHours(1.0) <= adjustedTimelineEnd)
        {
          num = day;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForDayHalfHoursRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      DateTime dateTime = adjustedTimelineStart;
      int num = adjustedTimelineStart.Day;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(adjustedTimelineStart, adjustedTimelineStart.AddMinutes(30.0), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddMinutes(30.0);
        dateTime = dateTime.AddMinutes(30.0);
        int day = dateTime.Day;
        if (day != num && dateTime.AddMinutes(30.0) <= adjustedTimelineEnd)
        {
          num = day;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForDayQuarterHoursRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      DateTime dateTime = adjustedTimelineStart;
      int num = adjustedTimelineStart.Day;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(adjustedTimelineStart, adjustedTimelineStart.AddMinutes(15.0), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddMinutes(15.0);
        dateTime = dateTime.AddMinutes(15.0);
        int day = dateTime.Day;
        if (day != num && dateTime.AddMinutes(15.0) <= adjustedTimelineEnd)
        {
          num = day;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual IList<GanttViewTimelineDataItem> BuildTimelineDataItemsForHourRange()
    {
      List<GanttViewTimelineDataItem> timelineDataItemList = new List<GanttViewTimelineDataItem>();
      DateTime adjustedTimelineStart = this.AdjustedTimelineStart;
      DateTime adjustedTimelineEnd = this.AdjustedTimelineEnd;
      DateTime dateTime = adjustedTimelineStart;
      int num = adjustedTimelineStart.Hour;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(adjustedTimelineStart, adjustedTimelineStart.AddMinutes(1.0), this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
      timelineDataItemList.Add(timelineDataItem);
      while (dateTime < adjustedTimelineEnd)
      {
        timelineDataItem.End = dateTime.AddMinutes(1.0);
        dateTime = dateTime.AddMinutes(1.0);
        int hour = dateTime.Hour;
        if (hour != num && dateTime.AddMinutes(1.0) <= adjustedTimelineEnd)
        {
          num = hour;
          timelineDataItem = new GanttViewTimelineDataItem(dateTime, dateTime, this.GraphicalViewElement.TimelineRange, this.GraphicalViewElement.OnePixelTime);
          timelineDataItemList.Add(timelineDataItem);
        }
      }
      return (IList<GanttViewTimelineDataItem>) timelineDataItemList;
    }

    public virtual int WeekNumber(DateTime date)
    {
      DateTime dateTime1 = new DateTime(date.Year, 1, 1);
      DateTime dateTime2 = new DateTime(date.Year, 12, 31);
      int[] numArray = new int[7]{ 6, 7, 8, 9, 10, 4, 5 };
      int num = (date.Subtract(dateTime1).Days + numArray[(int) dateTime1.DayOfWeek]) / 7;
      switch (num)
      {
        case 0:
          return this.WeekNumber(dateTime1.AddDays(-1.0));
        case 53:
          if (dateTime2.DayOfWeek < DayOfWeek.Thursday)
            return 1;
          return num;
        default:
          return num;
      }
    }

    public virtual string GetTimelineTopElementText(GanttViewTimelineDataItem item)
    {
      switch (item.Range)
      {
        case TimeRange.Week:
          string localizedString = LocalizationProvider<GanttViewLocalizationProvider>.CurrentProvider.GetLocalizedString("TimelineWeek");
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentUICulture, this.TimelineUpperItemFormat ?? "{0}#{1}, {2:yyyy}", (object) localizedString, (object) this.WeekNumber(item.Start), (object) item.Start);
        case TimeRange.Month:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentUICulture, this.TimelineUpperItemFormat ?? "{0:MMMM, yyyy}", (object) item.Start);
        case TimeRange.Year:
        case TimeRange.YearHalves:
        case TimeRange.YearQuarters:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentUICulture, this.TimelineUpperItemFormat ?? "{0:yyyy}", (object) item.Start);
        case TimeRange.Day:
        case TimeRange.DayHalfHours:
        case TimeRange.DayQuarterHours:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentUICulture, this.TimelineUpperItemFormat ?? "{0:dd, MMMM}", (object) item.Start);
        case TimeRange.Hour:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentUICulture, this.TimelineUpperItemFormat ?? "{0:HH:mm}", (object) item.Start);
        default:
          return string.Empty;
      }
    }

    public virtual string GetTimelineBottomElementText(GanttViewTimelineDataItem item, int index)
    {
      switch (item.Range)
      {
        case TimeRange.Week:
        case TimeRange.Month:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, this.TimelineLowerItemFormat ?? "{0:dd}", (object) item.Start.AddDays((double) index));
        case TimeRange.Year:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, this.TimelineLowerItemFormat ?? "{0:MMMM}", (object) item.Start.AddMonths(index));
        case TimeRange.YearHalves:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, this.TimelineLowerItemFormat ?? "H{0}", (object) (index + 1));
        case TimeRange.YearQuarters:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, this.TimelineLowerItemFormat ?? "Q{0}", (object) (index + 1));
        case TimeRange.Day:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, this.TimelineLowerItemFormat ?? "{0:HH:mm}", (object) item.Start.AddHours((double) index));
        case TimeRange.DayHalfHours:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, this.TimelineLowerItemFormat ?? "{0:HH:mm}", (object) item.Start.AddMinutes((double) (30 * index)));
        case TimeRange.DayQuarterHours:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, this.TimelineLowerItemFormat ?? "{0:HH:mm}", (object) item.Start.AddMinutes((double) (15 * index)));
        case TimeRange.Hour:
          return string.Format((IFormatProvider) Thread.CurrentThread.CurrentCulture, this.TimelineLowerItemFormat ?? "{0:mm}", (object) item.Start.AddMinutes((double) index));
        default:
          return string.Empty;
      }
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForItem(
      GanttViewTimelineDataItem item,
      TimeRange timeRange)
    {
      switch (timeRange)
      {
        case TimeRange.Week:
          return this.GetTimelineCellInfoForWeekRange(item);
        case TimeRange.Month:
          return this.GetTimelineCellInfoForMonthRange(item);
        case TimeRange.Year:
          return this.GetTimelineCellInfoForYearRange(item);
        case TimeRange.YearHalves:
          return this.GetTimelineCellInfoForYearHalvesRange(item);
        case TimeRange.YearQuarters:
          return this.GetTimelineCellInfoForYearQuartersRange(item);
        case TimeRange.Day:
          return this.GetTimelineCellInfoForDayRange(item);
        case TimeRange.DayHalfHours:
          return this.GetTimelineCellInfoForDayHalfHoursRange(item);
        case TimeRange.DayQuarterHours:
          return this.GetTimelineCellInfoForDayQuarterHoursRange(item);
        case TimeRange.Hour:
          return this.GetTimelineCellInfoForHourRange(item);
        default:
          return new GanttTimelineCellsInfo();
      }
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForWeekRange(
      GanttViewTimelineDataItem item)
    {
      return new GanttTimelineCellsInfo((int) Math.Ceiling((item.End - item.Start).TotalDays));
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForMonthRange(
      GanttViewTimelineDataItem item)
    {
      int num = DateTime.DaysInMonth(item.Start.Year, item.Start.Month);
      int numberOfCells = num;
      if (item.Start <= this.AdjustedTimelineStart && item.Start.Day > 1)
        numberOfCells -= item.Start.Day - 1;
      if (item.End >= this.AdjustedTimelineEnd && item.End.AddDays(-1.0).Day < num)
        numberOfCells -= num - item.End.AddDays(-1.0).Day;
      return new GanttTimelineCellsInfo(numberOfCells);
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForYearRange(
      GanttViewTimelineDataItem item)
    {
      int num = 12;
      int numberOfCells = 12;
      if (item.Start == this.AdjustedTimelineStart && item.Start.Month > 1)
        numberOfCells -= item.Start.Month - 1;
      if (item.End == this.AdjustedTimelineEnd && item.End.AddDays(-1.0).Month < num)
        numberOfCells -= num - item.End.AddDays(-1.0).Month;
      return new GanttTimelineCellsInfo(numberOfCells);
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForYearHalvesRange(
      GanttViewTimelineDataItem item)
    {
      int numberOfCells = 2;
      int startIndex = 0;
      double num = (DateTime.IsLeapYear(item.Start.Year) ? 366.0 : 365.0) / 2.0;
      if ((double) item.Start.DayOfYear > num)
      {
        numberOfCells = 1;
        startIndex = 1;
      }
      if ((double) item.End.AddDays(-1.0).DayOfYear <= num)
        numberOfCells = 1;
      return new GanttTimelineCellsInfo(numberOfCells, startIndex);
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForYearQuartersRange(
      GanttViewTimelineDataItem item)
    {
      int numberOfCells = 4;
      int startIndex = 0;
      float num1 = (DateTime.IsLeapYear(item.Start.Year) ? 366f : 365f) / 4f;
      float num2 = num1 * 2f;
      float num3 = num1 + num2;
      float dayOfYear1 = (float) item.Start.DayOfYear;
      float dayOfYear2 = (float) item.End.AddDays(-1.0).DayOfYear;
      if ((double) dayOfYear1 >= (double) num1)
      {
        --numberOfCells;
        ++startIndex;
        if ((double) dayOfYear1 >= (double) num2)
        {
          --numberOfCells;
          ++startIndex;
          if ((double) dayOfYear1 >= (double) num3)
          {
            --numberOfCells;
            ++startIndex;
          }
        }
      }
      if (numberOfCells > 1 && (double) dayOfYear2 <= (double) num3)
      {
        --numberOfCells;
        if (numberOfCells > 1 && (double) dayOfYear2 <= (double) num2)
        {
          --numberOfCells;
          if (numberOfCells > 1 && (double) dayOfYear2 <= (double) num1)
            --numberOfCells;
        }
      }
      return new GanttTimelineCellsInfo(numberOfCells, startIndex);
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForDayRange(
      GanttViewTimelineDataItem item)
    {
      int num = 24;
      int numberOfCells = num;
      if (item.Start <= this.AdjustedTimelineStart && item.Start.Hour > 0)
        numberOfCells -= item.Start.Hour;
      if (item.End >= this.AdjustedTimelineEnd && item.End.Hour < num)
        numberOfCells -= num - item.End.Hour;
      return new GanttTimelineCellsInfo(numberOfCells);
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForDayHalfHoursRange(
      GanttViewTimelineDataItem item)
    {
      int num1 = 48;
      int numberOfCells = num1;
      int num2 = item.End.Hour == 0 ? 24 : item.End.Hour;
      if (item.Start <= this.AdjustedTimelineStart)
      {
        if (item.Start.Hour > 0)
          numberOfCells -= item.Start.Hour * 2;
        if (item.Start.Minute >= 30)
          --numberOfCells;
      }
      if (item.End >= this.AdjustedTimelineEnd)
      {
        if (num2 * 2 < num1)
          numberOfCells -= num1 - num2 * 2;
        if (item.End.Minute > 0)
          ++numberOfCells;
      }
      return new GanttTimelineCellsInfo(numberOfCells);
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForDayQuarterHoursRange(
      GanttViewTimelineDataItem item)
    {
      int num1 = 96;
      int numberOfCells = num1;
      int num2 = item.End.Hour == 0 ? 24 : item.End.Hour;
      if (item.Start <= this.AdjustedTimelineStart)
      {
        if (item.Start.Hour > 0)
          numberOfCells -= item.Start.Hour * 4;
        if (item.Start.Minute >= 15)
        {
          --numberOfCells;
          if (item.Start.Minute >= 30)
          {
            --numberOfCells;
            if (item.Start.Minute >= 45)
              --numberOfCells;
          }
        }
      }
      if (item.End >= this.AdjustedTimelineEnd)
      {
        if (num2 * 4 < num1)
          numberOfCells -= num1 - num2 * 4;
        if (item.End.Minute >= 15)
        {
          ++numberOfCells;
          if (item.End.Minute >= 30)
          {
            ++numberOfCells;
            if (item.End.Minute >= 45)
              ++numberOfCells;
          }
        }
      }
      return new GanttTimelineCellsInfo(numberOfCells);
    }

    public virtual GanttTimelineCellsInfo GetTimelineCellInfoForHourRange(
      GanttViewTimelineDataItem item)
    {
      int num = 60;
      int numberOfCells = num;
      if (item.Start <= this.AdjustedTimelineStart && item.Start.Minute > 0)
        numberOfCells -= item.Start.Minute;
      if (item.End >= this.AdjustedTimelineEnd && item.End.Hour < num)
        numberOfCells -= num - item.End.Minute;
      return new GanttTimelineCellsInfo(numberOfCells);
    }

    public virtual GanttViewTimelineCellElement CreateElement()
    {
      return new GanttViewTimelineCellElement(this.GraphicalViewElement);
    }

    public virtual void FillRangesMinWidths()
    {
      if (this.GraphicalViewElement.TimelineContainer.Children == null || this.GraphicalViewElement.TimelineContainer.Children.Count == 0)
        return;
      GanttViewTimelineItemElement child = this.GraphicalViewElement.TimelineContainer.Children[0] as GanttViewTimelineItemElement;
      if (child.BottomElement.Children == null || child.BottomElement.Children.Count == 0)
        return;
      Font font = ((VisualElement) child.BottomElement.Children[0]).Font;
      this.RangesMinWidths.Clear();
      Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
      float num1 = 0.0f;
      float num2 = 0.0f;
      GanttViewTimelineDataItem timelineDataItem = new GanttViewTimelineDataItem(DateTime.Now, DateTime.Now, TimeRange.YearHalves, this.GraphicalViewElement.OnePixelTime);
      for (int index = 0; index < 2; ++index)
      {
        timelineDataItem.Start = new DateTime(DateTime.Now.Year, index * 7 + 1, 1);
        timelineDataItem.End = timelineDataItem.Start.AddDays(1.0);
        float width = graphics.MeasureString(this.GetTimelineBottomElementText(timelineDataItem, index), font).Width;
        if ((double) width > (double) num2)
          num2 = width + 6f;
      }
      this.RangesMinWidths.Add(TimeRange.YearHalves, (int) ((double) num2 * 2.0));
      timelineDataItem.Range = TimeRange.YearQuarters;
      num1 = 0.0f;
      float num3 = 0.0f;
      for (int index = 0; index < 4; ++index)
      {
        timelineDataItem.Start = new DateTime(DateTime.Now.Year, index * 3 + 2, 1);
        timelineDataItem.End = timelineDataItem.Start.AddDays(1.0);
        float width = graphics.MeasureString(this.GetTimelineBottomElementText(timelineDataItem, index + 1), font).Width;
        if ((double) width > (double) num3)
          num3 = width + 6f;
      }
      this.RangesMinWidths.Add(TimeRange.YearQuarters, (int) ((double) num3 * 4.0));
      timelineDataItem.Range = TimeRange.Year;
      num1 = 0.0f;
      float num4 = 0.0f;
      for (int index = 0; index < 12; ++index)
      {
        timelineDataItem.Start = new DateTime(DateTime.Now.Year, index + 1, 1);
        timelineDataItem.End = timelineDataItem.Start.AddMonths(1);
        float width = graphics.MeasureString(this.GetTimelineBottomElementText(timelineDataItem, 0), font).Width;
        if ((double) width > (double) num4)
          num4 = width + 6f;
      }
      this.RangesMinWidths.Add(TimeRange.Year, (int) ((double) num4 * 12.0));
      timelineDataItem.Range = TimeRange.Month;
      num1 = 0.0f;
      float num5 = 0.0f;
      for (int index = 0; index < 31; ++index)
      {
        timelineDataItem.Start = new DateTime(DateTime.Now.Year, 1, index + 1);
        timelineDataItem.End = timelineDataItem.Start.AddDays(1.0);
        float width = graphics.MeasureString(this.GetTimelineBottomElementText(timelineDataItem, 0), font).Width;
        if ((double) width > (double) num5)
          num5 = width + 6f;
      }
      this.RangesMinWidths.Add(TimeRange.Month, (int) ((double) num5 * 31.0));
      timelineDataItem.Range = TimeRange.Day;
      num1 = 0.0f;
      float num6 = 0.0f;
      for (int index = 0; index < 24; ++index)
      {
        timelineDataItem.Start = new DateTime(DateTime.Now.Year, 1, 1).AddHours((double) index);
        timelineDataItem.End = timelineDataItem.Start.AddHours(1.0);
        float width = graphics.MeasureString(this.GetTimelineBottomElementText(timelineDataItem, 0), font).Width;
        if ((double) width > (double) num6)
          num6 = width + 6f;
      }
      this.RangesMinWidths.Add(TimeRange.Day, (int) ((double) num6 * 24.0));
      timelineDataItem.Range = TimeRange.DayHalfHours;
      num1 = 0.0f;
      float num7 = 0.0f;
      for (int index = 0; index < 48; ++index)
      {
        timelineDataItem.Start = new DateTime(DateTime.Now.Year, 1, 1).AddMinutes((double) (30 * index));
        timelineDataItem.End = timelineDataItem.Start.AddMinutes(30.0);
        float width = graphics.MeasureString(this.GetTimelineBottomElementText(timelineDataItem, 0), font).Width;
        if ((double) width > (double) num7)
          num7 = width + 6f;
      }
      this.RangesMinWidths.Add(TimeRange.DayHalfHours, (int) ((double) num7 * 48.0));
      timelineDataItem.Range = TimeRange.Hour;
      num1 = 0.0f;
      float num8 = 0.0f;
      for (int index = 0; index < 60; ++index)
      {
        timelineDataItem.Start = new DateTime(DateTime.Now.Year, 1, 1).AddMinutes((double) index);
        timelineDataItem.End = timelineDataItem.Start.AddMinutes(1.0);
        float width = graphics.MeasureString(this.GetTimelineBottomElementText(timelineDataItem, 0), font).Width;
        if ((double) width > (double) num8)
          num8 = width + 6f;
      }
      this.RangesMinWidths.Add(TimeRange.Hour, (int) ((double) num8 * 60.0));
    }

    public virtual TimeRange GetAutoTimeRange(TimeRange currentRange, bool zoomIn)
    {
      switch (currentRange)
      {
        case TimeRange.Week:
        case TimeRange.Month:
          float num1 = (float) (new TimeSpan(31, 0, 0, 0).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds);
          if (zoomIn)
          {
            if ((double) num1 > (double) this.RangesMinWidths[TimeRange.Day])
              return TimeRange.Day;
            break;
          }
          if ((double) num1 < (double) this.RangesMinWidths[currentRange])
            return TimeRange.Year;
          break;
        case TimeRange.Year:
          if (zoomIn)
          {
            if (new TimeSpan(31, 0, 0, 0).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds > (double) this.RangesMinWidths[TimeRange.Month])
              return TimeRange.Month;
            break;
          }
          if (new TimeSpan(365, 0, 0, 0).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds < (double) this.RangesMinWidths[currentRange])
            return TimeRange.YearQuarters;
          break;
        case TimeRange.YearHalves:
          float num2 = (float) (new TimeSpan(365, 0, 0, 0).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds);
          if (zoomIn)
          {
            if ((double) num2 > (double) this.RangesMinWidths[TimeRange.YearQuarters])
              return TimeRange.YearQuarters;
            break;
          }
          double rangesMinWidth = (double) this.RangesMinWidths[currentRange];
          break;
        case TimeRange.YearQuarters:
          float num3 = (float) (new TimeSpan(365, 0, 0, 0).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds);
          if (zoomIn)
          {
            if ((double) num3 > (double) this.RangesMinWidths[TimeRange.Year])
              return TimeRange.Year;
            break;
          }
          if ((double) num3 < (double) this.RangesMinWidths[currentRange])
            return TimeRange.YearHalves;
          break;
        case TimeRange.Day:
          float num4 = (float) (new TimeSpan(1, 0, 0, 0).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds);
          if (zoomIn)
          {
            if ((double) num4 > (double) this.RangesMinWidths[TimeRange.DayHalfHours])
              return TimeRange.DayHalfHours;
            break;
          }
          if ((double) num4 < (double) this.RangesMinWidths[currentRange])
            return TimeRange.Month;
          break;
        case TimeRange.DayHalfHours:
        case TimeRange.DayQuarterHours:
          float num5 = (float) (new TimeSpan(1, 0, 0, 0).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds);
          if (zoomIn)
          {
            if ((double) num5 > (double) this.RangesMinWidths[TimeRange.DayQuarterHours])
              return TimeRange.Hour;
            break;
          }
          if ((double) num5 < (double) this.RangesMinWidths[currentRange])
            return TimeRange.Day;
          break;
        case TimeRange.Hour:
          float num6 = (float) (new TimeSpan(0, 1, 0, 0).TotalSeconds / this.GraphicalViewElement.OnePixelTime.TotalSeconds);
          if (!zoomIn && (double) num6 < (double) this.RangesMinWidths[currentRange])
            return TimeRange.DayHalfHours;
          break;
      }
      return currentRange;
    }
  }
}
