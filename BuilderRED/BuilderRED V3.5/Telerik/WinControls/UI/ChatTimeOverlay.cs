// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ChatTimeOverlay
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ChatTimeOverlay : BaseChatItemOverlay
  {
    private RadTimePickerContent timePicker;

    public ChatTimeOverlay(string title, DateTime selectedTime)
      : base(title)
    {
      this.timePicker.ValueChanged += new EventHandler(this.TimePicker_ValueChanged);
      this.timePicker.Value = (object) selectedTime;
    }

    public RadTimePickerContent timePickerContent
    {
      get
      {
        return this.timePicker;
      }
    }

    private void TimePicker_ValueChanged(object sender, EventArgs e)
    {
      this.CurrentValue = (object) ((DateTime) this.timePicker.Value).ToLongTimeString();
    }

    protected override RadElement CreateMainElement()
    {
      this.timePicker = new RadTimePickerContent();
      this.timePicker.TimePickerElement.TimeTables = TimeTables.HoursAndMinutesInOneTable;
      this.timePicker.TimePickerElement.Step = 30;
      this.timePicker.TimePickerElement.ColumnsCount = 6;
      return (RadElement) new RadHostItem((Control) this.timePicker);
    }

    protected override void DisposeManagedResources()
    {
      this.timePicker.ValueChanged -= new EventHandler(this.TimePicker_ValueChanged);
      base.DisposeManagedResources();
    }

    public override void PrepareForOverlayDisplay()
    {
      base.PrepareForOverlayDisplay();
      this.timePicker.TimePickerElement.ColumnsCount = 4;
    }

    public override void PrepareForPopupDisplay()
    {
      base.PrepareForPopupDisplay();
      this.timePicker.TimePickerElement.ColumnsCount = 8;
    }
  }
}
