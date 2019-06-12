// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadarColors
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RadarColors : UserControl
  {
    private Color colorRGB = Color.Empty;
    private IContainer components;
    private ProfessionalColorsSlider adobeColorsSlider1;
    private ColorWheel colorWheel1;

    public RadarColors()
    {
      this.InitializeComponent();
      this.adobeColorsSlider1.ColorHSL = HslColor.FromColor(Color.Green);
      this.adobeColorsSlider1.ColorMode = ColorModes.Luminance;
      this.adobeColorsSlider1.ColorChanged += new ColorChangedEventHandler(this.adobeColorsSlider1_ColorChanged);
      this.colorWheel1.ColorChanged += new ColorChangedEventHandler(this.colorWheel1_ColorChanged);
    }

    public Color ColorRGB
    {
      get
      {
        return this.colorRGB;
      }
      set
      {
        this.colorRGB = value;
        this.adobeColorsSlider1.ColorHSL = HslColor.FromColor(value);
        this.colorWheel1.ColorRGB = value;
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    private void colorWheel1_ColorChanged(object sender, ColorChangedEventArgs args)
    {
      HslColor colorHsl = this.adobeColorsSlider1.ColorHSL;
      HslColor hslColor = HslColor.FromColor(args.SelectedColor);
      hslColor.L = colorHsl.L;
      this.adobeColorsSlider1.ColorHSL = hslColor;
      this.colorRGB = hslColor.RgbValue;
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(this.colorRGB));
    }

    private void adobeColorsSlider1_ColorChanged(object sender, ColorChangedEventArgs args)
    {
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, args);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.adobeColorsSlider1 = new ProfessionalColorsSlider();
      this.colorWheel1 = new ColorWheel();
      this.SuspendLayout();
      this.adobeColorsSlider1.ColorHSL = HslColor.FromAhsl(0.0, 1.0, 1.0);
      this.adobeColorsSlider1.ColorMode = ColorModes.Red;
      this.adobeColorsSlider1.ColorRGB = Color.FromArgb((int) byte.MaxValue, 0, 0);
      this.adobeColorsSlider1.Location = new Point(196, 15);
      this.adobeColorsSlider1.Name = "adobeColorsSlider1";
      this.adobeColorsSlider1.Position = 0;
      this.adobeColorsSlider1.Size = new Size(42, 172);
      this.adobeColorsSlider1.TabIndex = 3;
      this.colorWheel1.Location = new Point(18, 15);
      this.colorWheel1.Name = "colorWheel1";
      this.colorWheel1.Size = new Size(172, 172);
      this.colorWheel1.TabIndex = 2;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.adobeColorsSlider1);
      this.Controls.Add((Control) this.colorWheel1);
      this.Name = nameof (RadarColors);
      this.Size = new Size(257, 202);
      this.ResumeLayout(false);
    }
  }
}
