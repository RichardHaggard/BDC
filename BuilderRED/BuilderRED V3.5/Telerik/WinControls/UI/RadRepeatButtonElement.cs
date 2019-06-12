// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRepeatButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class RadRepeatButtonElement : RadButtonElement
  {
    public static readonly RadProperty DelayProperty = RadProperty.Register(nameof (Delay), typeof (int), typeof (RadRepeatButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadRepeatButtonElement.GetKeyboardDelay(), ElementPropertyOptions.None), new ValidateValueCallback(RadRepeatButtonElement.IsDelayValid));
    public static readonly RadProperty IntervalProperty = RadProperty.Register(nameof (Interval), typeof (int), typeof (RadRepeatButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadRepeatButtonElement.GetKeyboardSpeed(), ElementPropertyOptions.None), new ValidateValueCallback(RadRepeatButtonElement.IsIntervalValid));
    private Timer timer;

    static RadRepeatButtonElement()
    {
      RadElement.ClickModeProperty.OverrideMetadata(typeof (RadRepeatButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ClickMode.Press, ElementPropertyOptions.None));
    }

    public RadRepeatButtonElement()
    {
    }

    public RadRepeatButtonElement(string text)
      : base(text)
    {
    }

    protected override void DisposeManagedResources()
    {
      if (this.timer != null)
        this.timer.Dispose();
      base.DisposeManagedResources();
    }

    [Description("Gets or sets the amount of time, in milliseconds, the Repeat button element waits while it is pressed before it starts repeating. The value must be non-negative.")]
    [Category("Behavior")]
    [RadPropertyDefaultValue("Delay", typeof (RadRepeatButtonElement))]
    [Bindable(true)]
    public int Delay
    {
      get
      {
        return (int) this.GetValue(RadRepeatButtonElement.DelayProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadRepeatButtonElement.DelayProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Interval", typeof (RadRepeatButtonElement))]
    [Description("Gets or sets the amount of time, in milliseconds, between repeats once repeating starts. The value must be non-negative.")]
    [Category("Behavior")]
    [Bindable(true)]
    public int Interval
    {
      get
      {
        return (int) this.GetValue(RadRepeatButtonElement.IntervalProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadRepeatButtonElement.IntervalProperty, (object) value);
      }
    }

    protected virtual void OnTimeout(object sender, EventArgs e)
    {
      if (this.timer.Interval != this.Interval)
        this.timer.Interval = this.Interval;
      if (!this.IsPressed || this.ClickMode != ClickMode.Hover && MouseButtons.Left != (MouseButtons.Left & Control.MouseButtons))
        return;
      this.OnClick(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.HandleIsMouseOverChanged();
      if (!this.IsPressed)
        return;
      this.StartTimer();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.HandleIsMouseOverChanged();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left || this.ClickMode == ClickMode.Hover)
        return;
      this.StartTimer();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (e.Button != MouseButtons.Left || this.ClickMode == ClickMode.Hover)
        return;
      this.StopTimer();
    }

    protected override void OnEnabledChanged(RadPropertyChangedEventArgs e)
    {
      base.OnEnabledChanged(e);
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
      this.StopTimer();
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      (this.ElementTree.Control as RadRepeatButton)?.PerformButtonClick();
    }

    internal static int GetKeyboardDelay()
    {
      int num = SystemInformation.KeyboardDelay;
      switch (num)
      {
        case 0:
        case 1:
        case 2:
        case 3:
          return (num + 1) * 250;
        default:
          num = 0;
          goto case 0;
      }
    }

    internal static int GetKeyboardSpeed()
    {
      int num = SystemInformation.KeyboardSpeed;
      if (num < 0 || num > 31)
        num = 31;
      return (31 - num) * 367 / 31 + 33;
    }

    private static bool IsDelayValid(object value, RadObject obj)
    {
      return (int) value >= 0;
    }

    private static bool IsIntervalValid(object value, RadObject obj)
    {
      return (int) value > 0;
    }

    private void StartTimer()
    {
      if (this.timer == null)
      {
        this.timer = new Timer();
        this.timer.Tick += new EventHandler(this.OnTimeout);
      }
      else if (this.timer.Enabled)
        return;
      this.timer.Interval = this.Delay;
      this.timer.Start();
    }

    private void StopTimer()
    {
      if (this.timer == null)
        return;
      this.timer.Stop();
    }

    private bool HandleIsMouseOverChanged()
    {
      if (this.ClickMode != ClickMode.Hover)
        return false;
      if (this.IsMouseOver)
        this.StartTimer();
      else
        this.StopTimer();
      return true;
    }
  }
}
