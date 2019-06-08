// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSeparator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  public class RadSeparator : RadControl
  {
    private SeparatorElement separatorElement;

    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DefaultValue(true)]
    public bool ShowShadow
    {
      get
      {
        return this.SeparatorElement.ShowShadow;
      }
      set
      {
        this.SeparatorElement.ShowShadow = value;
      }
    }

    [DefaultValue(typeof (Point), "0,0")]
    public Point ShadowOffset
    {
      get
      {
        return this.SeparatorElement.ShadowOffset;
      }
      set
      {
        this.SeparatorElement.ShadowOffset = value;
      }
    }

    [DefaultValue(Orientation.Horizontal)]
    public Orientation Orientation
    {
      get
      {
        return this.SeparatorElement.Orientation;
      }
      set
      {
        this.SeparatorElement.Orientation = value;
      }
    }

    public SeparatorElement SeparatorElement
    {
      get
      {
        return this.separatorElement;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(200, 4));
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    public RadSeparator()
    {
      this.MinimumSize = new Size(0, 0);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.separatorElement = new SeparatorElement();
      this.separatorElement.StretchHorizontally = true;
      this.separatorElement.StretchVertically = true;
      parent.Children.Add((RadElement) this.separatorElement);
    }
  }
}
