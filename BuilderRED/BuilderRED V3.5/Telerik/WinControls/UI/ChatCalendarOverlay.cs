// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatCalendarOverlay
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ChatCalendarOverlay : BaseChatItemOverlay
  {
    private RadCalendar calendar;

    public ChatCalendarOverlay(string title)
      : this(title, DateTime.Now)
    {
    }

    public ChatCalendarOverlay(string title, DateTime selectedDate)
      : base(title)
    {
      this.calendar.SelectionChanged += new EventHandler(this.Calendar_SelectionChanged);
      this.calendar.SelectedDate = selectedDate;
    }

    public RadCalendar Calendar
    {
      get
      {
        return this.calendar;
      }
    }

    private void Calendar_SelectionChanged(object sender, EventArgs e)
    {
      if (this.Calendar.AllowMultipleSelect)
        this.CurrentValue = (object) this.GetDateRangeString(this.Calendar.SelectedDates);
      else
        this.CurrentValue = (object) this.calendar.SelectedDate.ToString("D", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    protected override RadElement CreateMainElement()
    {
      this.calendar = new RadCalendar();
      return (RadElement) new RadHostItem((Control) this.calendar);
    }

    protected override void DisposeManagedResources()
    {
      this.calendar.SelectionChanged -= new EventHandler(this.Calendar_SelectionChanged);
      base.DisposeManagedResources();
    }

    protected override string GetDisplayString(object value)
    {
      if (this.Calendar.AllowMultipleSelect)
        return this.GetDateRangeString(this.Calendar.SelectedDates);
      return this.calendar.SelectedDate.ToString("D", (IFormatProvider) CultureInfo.InvariantCulture);
    }

    protected virtual string GetDateRangeString(DateTimeCollection dateRange)
    {
      DateTime[] array = dateRange.ToArray();
      Array.Sort<DateTime>(array);
      if (array.Length == 1)
        return "From " + array[0].ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
      if (array.Length <= 1)
        return "From " + Environment.NewLine + "to ";
      return "From " + array[0].ToString("d", (IFormatProvider) CultureInfo.InvariantCulture) + Environment.NewLine + "to " + array[array.Length - 1].ToString("d", (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
