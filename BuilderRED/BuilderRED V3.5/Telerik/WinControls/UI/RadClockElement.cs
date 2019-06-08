// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadClockElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadClockElement : LightVisualElement
  {
    private bool showSystemTime = true;
    private TimeSpan offset = TimeSpan.Zero;
    private DateTime? value;
    private Timer timer;
    private ArrowElement hoursArrow;
    private ArrowElement minutesArrow;
    private ArrowElement secondsArrow;
    private LightVisualElement centralDoth;
    private float angleHour;
    private float angleMinutes;
    private float angleSeconds;

    public TimeSpan Offset
    {
      get
      {
        return this.offset;
      }
      set
      {
        this.offset = value;
      }
    }

    [DefaultValue(true)]
    public virtual bool ShowSystemTime
    {
      get
      {
        return this.showSystemTime;
      }
      set
      {
        this.showSystemTime = value;
      }
    }

    public virtual DateTime? Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
        if (value.HasValue)
          this.SetClock(value.Value);
        else
          this.SetClock(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
      }
    }

    public virtual void SetClock(DateTime value)
    {
      this.angleSeconds = (float) (value.Second * 6);
      this.angleMinutes = (float) (value.Minute * 6);
      this.angleHour = (float) (value.Hour * 30) + (float) value.Minute * 0.5f;
      new PropertySetting(RadElement.AngleTransformProperty, (object) this.angleSeconds).ApplyValue((RadObject) this.secondsArrow);
      new PropertySetting(RadElement.AngleTransformProperty, (object) this.angleMinutes).ApplyValue((RadObject) this.minutesArrow);
      PropertySetting propertySetting = new PropertySetting(RadElement.AngleTransformProperty, (object) this.angleHour);
      if ((double) this.angleHour % 360.0 == (double) this.hoursArrow.AngleTransform % 360.0)
        return;
      propertySetting.ApplyValue((RadObject) this.hoursArrow);
    }

    public ArrowElement SecondsArrow
    {
      get
      {
        return this.secondsArrow;
      }
      set
      {
        this.secondsArrow = value;
      }
    }

    public ArrowElement MinutesArrow
    {
      get
      {
        return this.minutesArrow;
      }
      set
      {
        this.minutesArrow = value;
      }
    }

    public ArrowElement HoursArrow
    {
      get
      {
        return this.hoursArrow;
      }
      set
      {
        this.hoursArrow = value;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ShouldPaint = true;
      this.MinSize = this.MaxSize = new Size(134, 135);
      this.ImageLayout = ImageLayout.None;
      this.ImageAlignment = ContentAlignment.TopLeft;
      this.hoursArrow = new ArrowElement();
      this.hoursArrow.Class = "HoursArrow";
      this.hoursArrow.ImageAlignment = ContentAlignment.MiddleRight;
      this.hoursArrow.MinSize = this.hoursArrow.MaxSize = new Size(134, 135);
      this.Children.Add((RadElement) this.hoursArrow);
      this.minutesArrow = new ArrowElement();
      this.minutesArrow.Class = "MinutesArrow";
      this.minutesArrow.ImageAlignment = ContentAlignment.MiddleRight;
      this.minutesArrow.MinSize = this.minutesArrow.MaxSize = new Size(134, 135);
      this.Children.Add((RadElement) this.minutesArrow);
      this.secondsArrow = new ArrowElement();
      this.secondsArrow.Class = "SecondsArrow";
      this.secondsArrow.ImageAlignment = ContentAlignment.MiddleRight;
      this.secondsArrow.MinSize = this.secondsArrow.MaxSize = new Size(134, 135);
      this.Children.Add((RadElement) this.secondsArrow);
      this.centralDoth = new LightVisualElement();
      this.centralDoth.Class = "CentralDot";
      this.centralDoth.MinSize = this.centralDoth.MaxSize = new Size(134, 135);
      this.Children.Add((RadElement) this.centralDoth);
      this.timer = new Timer();
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.timer.Interval = 1000;
      this.timer.Start();
    }

    protected override void DisposeManagedResources()
    {
      this.timer.Stop();
      this.timer.Dispose();
      base.DisposeManagedResources();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (!this.showSystemTime || !this.IsInValidState(false) || this.IsDisposed)
        return;
      this.SetClock(DateTime.Now + this.offset);
    }
  }
}
