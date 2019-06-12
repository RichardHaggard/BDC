// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadClock
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  public class RadClock : RadControl
  {
    private RadClockElement timePickerElement;

    public RadClock()
    {
      this.AutoSize = true;
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.FixedHeight, true);
      this.SetStyle(ControlStyles.FixedWidth, true);
      this.Size = this.DefaultSize;
      if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        return;
      LicenseManager.Validate(this.GetType());
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.timePickerElement = new RadClockElement();
      parent.Children.Add((RadElement) this.timePickerElement);
    }

    protected override void InitializeRootElement(RootRadElement rootElement)
    {
      base.InitializeRootElement(rootElement);
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(134, 135));
      }
    }

    [Browsable(false)]
    public override RightToLeft RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
      }
    }

    [Description("Determines whether the Clock will show the system time.")]
    [DefaultValue(true)]
    public bool ShowSystemTime
    {
      get
      {
        return this.ClockElement.ShowSystemTime;
      }
      set
      {
        this.ClockElement.ShowSystemTime = value;
      }
    }

    [DefaultValue(typeof (TimeSpan), "00:00:00.00")]
    [Description("Determines the Offset from the system time.")]
    public TimeSpan Offset
    {
      get
      {
        return this.ClockElement.Offset;
      }
      set
      {
        this.ClockElement.Offset = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    [Description("Determines whether control's height will be determines automatically, depending on the current Font.")]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [Browsable(false)]
    public RadClockElement ClockElement
    {
      get
      {
        return this.timePickerElement;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    public DateTime? Value
    {
      get
      {
        return this.ClockElement.Value;
      }
      set
      {
        this.ClockElement.Value = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }
  }
}
