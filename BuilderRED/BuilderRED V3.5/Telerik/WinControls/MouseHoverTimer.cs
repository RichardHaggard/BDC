// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.MouseHoverTimer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class MouseHoverTimer : IDisposable
  {
    private static int mouseOverTimerInterval = 400;
    private const int SPI_GETMOUSEHOVERTIME_WIN9X = 400;
    private RadElement currentElement;
    private Timer mouseHoverTimer;

    public static int MouseOverTimerInterval
    {
      get
      {
        return MouseHoverTimer.mouseOverTimerInterval;
      }
    }

    public MouseHoverTimer()
    {
      this.mouseHoverTimer = new Timer();
      int num = SystemInformation.MouseHoverTime;
      if (num == 0)
        num = MouseHoverTimer.MouseOverTimerInterval;
      this.mouseHoverTimer.Interval = num;
      MouseHoverTimer.mouseOverTimerInterval = this.mouseHoverTimer.Interval;
      this.mouseHoverTimer.Tick += new EventHandler(this.OnTick);
    }

    public void Cancel()
    {
      if (this.mouseHoverTimer == null)
        return;
      this.mouseHoverTimer.Enabled = false;
      this.currentElement = (RadElement) null;
    }

    public void Cancel(RadElement element)
    {
      if (element != this.currentElement)
        return;
      this.Cancel();
    }

    public void Dispose()
    {
      if (this.mouseHoverTimer == null)
        return;
      this.Cancel();
      this.mouseHoverTimer.Tick -= new EventHandler(this.OnTick);
      this.mouseHoverTimer.Stop();
      this.mouseHoverTimer.Dispose();
      this.mouseHoverTimer = (Timer) null;
    }

    private void OnTick(object sender, EventArgs e)
    {
      this.mouseHoverTimer.Enabled = false;
      if (this.currentElement != null && this.currentElement.ElementState == ElementState.Loaded)
        this.currentElement.CallDoMouseHover(EventArgs.Empty);
      this.currentElement = (RadElement) null;
    }

    public void Start(RadElement element)
    {
      if (element != this.currentElement)
        this.Cancel(this.currentElement);
      this.currentElement = element;
      if (this.currentElement == null)
        return;
      this.mouseHoverTimer.Enabled = true;
    }
  }
}
