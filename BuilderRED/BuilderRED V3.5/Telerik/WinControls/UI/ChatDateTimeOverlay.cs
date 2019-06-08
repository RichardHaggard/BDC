// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatDateTimeOverlay
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ChatDateTimeOverlay : BaseChatItemOverlay
  {
    private RadPageView pageView;
    private RadCalendar calendar;
    private RadTimePickerContent timePicker;

    public ChatDateTimeOverlay(string title, DateTime selectedDateTime)
      : base(title)
    {
      this.calendar.SelectionChanged += new EventHandler(this.Calendar_SelectionChanged);
      this.calendar.SelectedDate = selectedDateTime;
      this.timePicker.ValueChanged += new EventHandler(this.TimePicker_ValueChanged);
      this.timePicker.Value = (object) selectedDateTime;
    }

    public RadPageView PageView
    {
      get
      {
        return this.pageView;
      }
    }

    public RadCalendar Calendar
    {
      get
      {
        return this.calendar;
      }
    }

    public RadTimePickerContent TimePicker
    {
      get
      {
        return this.timePicker;
      }
    }

    private void SetValue()
    {
      DateTime selectedDate = this.calendar.SelectedDate;
      DateTime dateTime = (DateTime) this.timePicker.Value;
      this.CurrentValue = (object) new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, dateTime.Hour, dateTime.Minute, dateTime.Second).ToString();
    }

    private void Calendar_SelectionChanged(object sender, EventArgs e)
    {
      this.SetValue();
    }

    private void TimePicker_ValueChanged(object sender, EventArgs e)
    {
      this.SetValue();
    }

    protected override RadElement CreateMainElement()
    {
      this.calendar = new RadCalendar();
      this.timePicker = new RadTimePickerContent();
      this.timePicker.TimePickerElement.TimeTables = TimeTables.HoursAndMinutesInOneTable;
      this.timePicker.TimePickerElement.Step = 30;
      this.timePicker.TimePickerElement.ColumnsCount = 6;
      this.pageView = new RadPageView();
      ((RadPageViewStripElement) this.pageView.ViewElement).ItemFitMode = StripViewItemFitMode.Fill;
      ((RadPageViewStripElement) this.pageView.ViewElement).StripButtons = StripViewButtons.None;
      this.pageView.Pages.Add(new RadPageViewPage("Date"));
      this.pageView.Pages.Add(new RadPageViewPage("Time"));
      this.pageView.Pages[0].Item.TextAlignment = ContentAlignment.MiddleCenter;
      this.pageView.Pages[1].Item.TextAlignment = ContentAlignment.MiddleCenter;
      this.pageView.Pages[0].Controls.Add((Control) this.calendar);
      this.pageView.Pages[1].Controls.Add((Control) this.timePicker);
      this.calendar.Dock = DockStyle.Fill;
      this.timePicker.Dock = DockStyle.Fill;
      return (RadElement) new RadHostItem((Control) this.pageView);
    }

    protected override void DisposeManagedResources()
    {
      this.calendar.SelectionChanged -= new EventHandler(this.Calendar_SelectionChanged);
      this.timePicker.ValueChanged -= new EventHandler(this.TimePicker_ValueChanged);
      base.DisposeManagedResources();
    }

    public override void PrepareForOverlayDisplay()
    {
      base.PrepareForOverlayDisplay();
      this.timePicker.TimePickerElement.ColumnsCount = 6;
      this.PageView.Pages[1].Visible = true;
      this.PageView.Pages[1].Visible = false;
    }

    public override void PrepareForPopupDisplay()
    {
      base.PrepareForPopupDisplay();
      this.timePicker.TimePickerElement.ColumnsCount = 8;
      this.PageView.Pages[1].Visible = true;
      this.PageView.Pages[1].Visible = false;
    }
  }
}
