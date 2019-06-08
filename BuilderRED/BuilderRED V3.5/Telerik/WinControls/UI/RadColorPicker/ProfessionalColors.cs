// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.ProfessionalColors
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.RadColorPicker
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class ProfessionalColors : UserControl
  {
    private HslColor colorHsl = HslColor.FromAhsl((int) byte.MaxValue);
    private Color colorRgb = Color.Empty;
    private ColorModes colorMode = ColorModes.Hue;
    private int suppressProfessionalColorsEvents;
    private int suppressSpinEditorEvents;
    private IContainer components;
    private RadSpinEditor numRed;
    private RadSpinEditor numGreen;
    private RadSpinEditor numHue;
    private RadSpinEditor numSaturation;
    private RadSpinEditor numBlue;
    private RadSpinEditor numLuminance;
    private RadRadioButton radioL;
    private RadRadioButton radioH;
    private RadRadioButton radioB;
    private RadRadioButton radioG;
    private RadRadioButton radioS;
    private RadRadioButton radioR;
    private ProfessionalColorsSlider proColorsSlider1;
    private RadSpinEditor numAlpha;
    private RadLabel label1;
    private ProfessionalColors2DBox proColors2DBox1;
    private TableLayoutPanel tableLayoutPanel1;

    public ProfessionalColors()
    {
      this.InitializeComponent();
      this.colorMode = ColorModes.Hue;
      this.proColors2DBox1.ColorMode = this.colorMode;
      this.proColorsSlider1.ColorMode = this.colorMode;
      this.proColors2DBox1.ColorChanged += new ColorChangedEventHandler(this.proColors2DBox1_ColorChanged);
      this.proColorsSlider1.ColorChanged += new ColorChangedEventHandler(this.proColorsSlider1_ColorChanged);
      this.proColorsSlider1.Position = this.proColorsSlider1.Height;
    }

    public Color ColorRgb
    {
      get
      {
        return this.colorRgb;
      }
      set
      {
        this.colorRgb = value;
        this.colorHsl = HslColor.FromColor(value);
        this.UpdateColorComponentsSpinEditors();
        this.UpdateProfessionalColorControls();
        this.OnColorChanged();
      }
    }

    public HslColor ColorHsl
    {
      get
      {
        return this.colorHsl;
      }
      set
      {
        if (!(this.colorHsl != value))
          return;
        this.colorHsl = value;
        this.colorRgb = this.colorHsl.RgbValue;
        this.UpdateColorComponentsSpinEditors();
        this.UpdateProfessionalColorControls();
        this.OnColorChanged();
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      this.label1.Padding = this.RightToLeft == RightToLeft.Yes ? new Padding(0, 0, 15, 0) : new Padding(15, 0, 0, 0);
    }

    private void proColors2DBox1_ColorChanged(object sender, ColorChangedEventArgs args)
    {
      if (this.suppressProfessionalColorsEvents > 0)
        return;
      HslColor colorHsl = this.proColors2DBox1.ColorHSL;
      colorHsl.A = (int) this.numAlpha.Value;
      this.colorHsl = colorHsl;
      this.colorRgb = this.colorHsl.RgbValue;
      if (this.ColorChanged != null)
        this.ColorChanged((object) this, new ColorChangedEventArgs(this.colorHsl));
      ++this.suppressProfessionalColorsEvents;
      this.proColorsSlider1.ColorHSL = this.colorHsl;
      --this.suppressProfessionalColorsEvents;
      this.UpdateColorComponentsSpinEditors();
    }

    private void proColorsSlider1_ColorChanged(object sender, ColorChangedEventArgs args)
    {
      if (this.suppressProfessionalColorsEvents > 0)
        return;
      HslColor colorHsl = this.proColorsSlider1.ColorHSL;
      colorHsl.A = (int) this.numAlpha.Value;
      this.colorHsl = colorHsl;
      this.colorRgb = this.colorHsl.RgbValue;
      if (this.ColorChanged != null)
        this.ColorChanged((object) this, new ColorChangedEventArgs(this.colorHsl));
      ++this.suppressProfessionalColorsEvents;
      this.proColors2DBox1.ColorHSL = this.colorHsl;
      --this.suppressProfessionalColorsEvents;
      this.UpdateColorComponentsSpinEditors();
    }

    private void colorModeChanged(object sender, StateChangedEventArgs args)
    {
      if (args.ToggleState != Telerik.WinControls.Enumerations.ToggleState.On)
        return;
      if (sender == this.radioH)
        this.colorMode = ColorModes.Hue;
      else if (sender == this.radioS)
        this.colorMode = ColorModes.Saturation;
      else if (sender == this.radioL)
        this.colorMode = ColorModes.Luminance;
      else if (sender == this.radioR)
        this.colorMode = ColorModes.Red;
      else if (sender == this.radioG)
        this.colorMode = ColorModes.Green;
      else if (sender == this.radioB)
        this.colorMode = ColorModes.Blue;
      this.proColorsSlider1.ColorMode = this.colorMode;
      this.proColors2DBox1.ColorMode = this.colorMode;
    }

    private void numAlpha_ValueChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      this.UpdateUIFromRgbControlChange(Color.FromArgb((int) this.numAlpha.Value, (int) this.numRed.Value, (int) this.numGreen.Value, (int) this.numBlue.Value));
    }

    private void numRed_ValueChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      this.UpdateUIFromRgbControlChange(Color.FromArgb((int) this.numAlpha.Value, (int) this.numRed.Value, (int) this.numGreen.Value, (int) this.numBlue.Value));
    }

    private void numGreen_ValueChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      this.UpdateUIFromRgbControlChange(Color.FromArgb((int) this.numAlpha.Value, (int) this.numRed.Value, (int) this.numGreen.Value, (int) this.numBlue.Value));
    }

    private void numBlue_ValueChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      this.UpdateUIFromRgbControlChange(Color.FromArgb((int) this.numAlpha.Value, (int) this.numRed.Value, (int) this.numGreen.Value, (int) this.numBlue.Value));
    }

    private void numHue_ValueChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      this.UpdateUIFromHslControlChange(HslColor.FromAhsl(this.colorHsl.A, (double) (int) this.numHue.Value / 360.0, this.colorHsl.S, this.colorHsl.L));
    }

    private void numSaturation_ValueChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      this.UpdateUIFromHslControlChange(HslColor.FromAhsl(this.colorHsl.A, this.colorHsl.H, (double) (int) this.numSaturation.Value / 100.0, this.colorHsl.L));
    }

    private void numLuminance_ValueChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      this.UpdateUIFromHslControlChange(HslColor.FromAhsl(this.colorHsl.A, this.colorHsl.H, this.colorHsl.S, (double) (int) this.numLuminance.Value / 100.0));
    }

    private void numBlue_TextChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      int result = 0;
      if (int.TryParse((sender as RadSpinEditor).Text, out result) && (result < 0 || result > (int) byte.MaxValue))
        result = (int) this.numBlue.Value;
      this.UpdateUIFromRgbControlChange(Color.FromArgb((int) this.numAlpha.Value, (int) this.numRed.Value, (int) this.numGreen.Value, result));
    }

    private void numRed_TextChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      int result = 0;
      if (int.TryParse((sender as RadSpinEditor).Text, out result) && (result < 0 || result > (int) byte.MaxValue))
        result = (int) this.numRed.Value;
      this.UpdateUIFromRgbControlChange(Color.FromArgb((int) this.numAlpha.Value, result, (int) this.numGreen.Value, (int) this.numBlue.Value));
    }

    private void numGreen_TextChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      int result = 0;
      if (int.TryParse((sender as RadSpinEditor).Text, out result) && (result < 0 || result > (int) byte.MaxValue))
        result = (int) this.numGreen.Value;
      this.UpdateUIFromRgbControlChange(Color.FromArgb((int) this.numAlpha.Value, (int) this.numRed.Value, result, (int) this.numBlue.Value));
    }

    private void numAlpha_TextChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      int result = 0;
      if (int.TryParse((sender as RadSpinEditor).Text, out result) && (result < 0 || result > (int) byte.MaxValue))
        result = (int) this.numAlpha.Value;
      this.UpdateUIFromRgbControlChange(Color.FromArgb(result, (int) this.numRed.Value, (int) this.numGreen.Value, (int) this.numBlue.Value));
    }

    private void numLuminance_TextChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      int result = 0;
      if (int.TryParse((sender as RadSpinEditor).Text, out result) && (result < 0 || result > 100))
        result = (int) this.numLuminance.Value;
      this.UpdateUIFromHslControlChange(HslColor.FromAhsl(this.colorHsl.A, this.colorHsl.H, this.colorHsl.S, (double) result / 100.0));
    }

    private void numSaturation_TextChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      int result = 0;
      if (int.TryParse((sender as RadSpinEditor).Text, out result) && (result < 0 || result > 100))
        result = (int) this.numSaturation.Value;
      this.UpdateUIFromHslControlChange(HslColor.FromAhsl(this.colorHsl.A, this.colorHsl.H, (double) result / 100.0, this.colorHsl.L));
    }

    private void numHue_TextChanged(object sender, EventArgs e)
    {
      if (this.suppressSpinEditorEvents > 0)
        return;
      int result = 0;
      if (int.TryParse((sender as RadSpinEditor).Text, out result) && (result < 0 || result > 360))
        result = (int) this.numHue.Value;
      this.UpdateUIFromHslControlChange(HslColor.FromAhsl(this.colorHsl.A, (double) result / 360.0, this.colorHsl.S, this.colorHsl.L));
    }

    internal void SetColorSilently(HslColor color)
    {
      this.colorHsl = color;
      this.colorRgb = color.RgbValue;
      this.UpdateProfessionalColorControls();
      this.UpdateColorComponentsSpinEditors();
    }

    protected virtual void OnColorChanged()
    {
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(this.colorHsl));
    }

    private void UpdateUIFromRgbControlChange(Color newColor)
    {
      this.colorHsl = HslColor.FromColor(newColor);
      this.colorRgb = newColor;
      ++this.suppressSpinEditorEvents;
      this.numHue.Value = (Decimal) (int) ((Decimal) this.colorHsl.H * new Decimal(360));
      this.numSaturation.Value = (Decimal) (int) ((Decimal) this.colorHsl.S * new Decimal(100));
      this.numLuminance.Value = (Decimal) (int) ((Decimal) this.colorHsl.L * new Decimal(100));
      --this.suppressSpinEditorEvents;
      this.UpdateProfessionalColorControls();
      this.OnColorChanged();
    }

    private void UpdateColorComponentsSpinEditors()
    {
      ++this.suppressSpinEditorEvents;
      this.numAlpha.EnableAnalytics = false;
      this.numRed.EnableAnalytics = false;
      this.numGreen.EnableAnalytics = false;
      this.numBlue.EnableAnalytics = false;
      this.numHue.EnableAnalytics = false;
      this.numSaturation.EnableAnalytics = false;
      this.numLuminance.EnableAnalytics = false;
      this.numAlpha.Value = (Decimal) this.colorRgb.A;
      this.numRed.Value = (Decimal) this.colorRgb.R;
      this.numGreen.Value = (Decimal) this.colorRgb.G;
      this.numBlue.Value = (Decimal) this.colorRgb.B;
      this.numHue.Value = (Decimal) (int) ((Decimal) this.colorHsl.H * new Decimal(360));
      this.numSaturation.Value = (Decimal) (int) ((Decimal) this.colorHsl.S * new Decimal(100));
      this.numLuminance.Value = (Decimal) (int) ((Decimal) this.colorHsl.L * new Decimal(100));
      --this.suppressSpinEditorEvents;
    }

    private void UpdateProfessionalColorControls()
    {
      ++this.suppressProfessionalColorsEvents;
      this.proColorsSlider1.ColorHSL = this.colorHsl;
      this.proColors2DBox1.ColorHSL = this.colorHsl;
      --this.suppressProfessionalColorsEvents;
    }

    private void UpdateUIFromHslControlChange(HslColor newColor)
    {
      this.colorHsl = newColor;
      this.colorRgb = newColor.RgbValue;
      ++this.suppressSpinEditorEvents;
      this.numRed.Value = (Decimal) this.colorRgb.R;
      this.numGreen.Value = (Decimal) this.colorRgb.G;
      this.numBlue.Value = (Decimal) this.colorRgb.B;
      this.numAlpha.Value = (Decimal) this.colorRgb.A;
      --this.suppressSpinEditorEvents;
      this.UpdateProfessionalColorControls();
      this.OnColorChanged();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radioL = new RadRadioButton();
      this.radioH = new RadRadioButton();
      this.radioB = new RadRadioButton();
      this.radioG = new RadRadioButton();
      this.radioS = new RadRadioButton();
      this.radioR = new RadRadioButton();
      this.numHue = new RadSpinEditor();
      this.numSaturation = new RadSpinEditor();
      this.numLuminance = new RadSpinEditor();
      this.label1 = new RadLabel();
      this.numAlpha = new RadSpinEditor();
      this.numRed = new RadSpinEditor();
      this.numGreen = new RadSpinEditor();
      this.numBlue = new RadSpinEditor();
      this.proColorsSlider1 = new ProfessionalColorsSlider();
      this.proColors2DBox1 = new ProfessionalColors2DBox();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.radioL.BeginInit();
      this.radioH.BeginInit();
      this.radioB.BeginInit();
      this.radioG.BeginInit();
      this.radioS.BeginInit();
      this.radioR.BeginInit();
      this.numHue.BeginInit();
      this.numSaturation.BeginInit();
      this.numLuminance.BeginInit();
      this.label1.BeginInit();
      this.numAlpha.BeginInit();
      this.numRed.BeginInit();
      this.numGreen.BeginInit();
      this.numBlue.BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      this.radioL.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radioL.AutoSize = false;
      this.radioL.Location = new Point(260, 180);
      this.radioL.Margin = new Padding(0);
      this.radioL.Name = "radioL";
      this.radioL.Size = new Size(40, 30);
      this.radioL.TabIndex = 41;
      this.radioL.Text = "L:";
      this.radioL.ToggleStateChanged += new StateChangedEventHandler(this.colorModeChanged);
      this.radioH.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radioH.AutoSize = false;
      this.radioH.CheckState = CheckState.Checked;
      this.radioH.Location = new Point(260, 120);
      this.radioH.Margin = new Padding(0);
      this.radioH.Name = "radioH";
      this.radioH.Size = new Size(40, 30);
      this.radioH.TabIndex = 39;
      this.radioH.Text = "H:";
      this.radioH.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.radioH.ToggleStateChanged += new StateChangedEventHandler(this.colorModeChanged);
      this.radioB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radioB.AutoSize = false;
      this.radioB.Location = new Point(260, 60);
      this.radioB.Margin = new Padding(0);
      this.radioB.Name = "radioB";
      this.radioB.Size = new Size(40, 30);
      this.radioB.TabIndex = 38;
      this.radioB.Text = "B:";
      this.radioB.ToggleStateChanged += new StateChangedEventHandler(this.colorModeChanged);
      this.radioG.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radioG.AutoSize = false;
      this.radioG.Location = new Point(260, 30);
      this.radioG.Margin = new Padding(0);
      this.radioG.Name = "radioG";
      this.radioG.Size = new Size(40, 30);
      this.radioG.TabIndex = 37;
      this.radioG.Text = "G:";
      this.radioG.ToggleStateChanged += new StateChangedEventHandler(this.colorModeChanged);
      this.radioS.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radioS.AutoSize = false;
      this.radioS.Location = new Point(260, 150);
      this.radioS.Margin = new Padding(0);
      this.radioS.Name = "radioS";
      this.radioS.Size = new Size(40, 30);
      this.radioS.TabIndex = 40;
      this.radioS.Text = "S:";
      this.radioS.ToggleStateChanged += new StateChangedEventHandler(this.colorModeChanged);
      this.radioR.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radioR.AutoSize = false;
      this.radioR.Location = new Point(260, 0);
      this.radioR.Margin = new Padding(0);
      this.radioR.Name = "radioR";
      this.radioR.Size = new Size(40, 30);
      this.radioR.TabIndex = 36;
      this.radioR.Text = "R:";
      this.radioR.ToggleStateChanged += new StateChangedEventHandler(this.colorModeChanged);
      this.numHue.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.numHue.Location = new Point(306, 125);
      this.numHue.Margin = new Padding(6, 0, 6, 0);
      this.numHue.Maximum = new Decimal(new int[4]
      {
        360,
        0,
        0,
        0
      });
      this.numHue.Name = "numHue";
      this.numHue.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.numHue.Size = new Size(58, 20);
      this.numHue.TabIndex = 46;
      this.numHue.TabStop = false;
      this.numHue.ThemeName = "TelerikMetro";
      this.numHue.ValueChanged += new EventHandler(this.numHue_ValueChanged);
      this.numHue.TextChanged += new EventHandler(this.numHue_TextChanged);
      this.numSaturation.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.numSaturation.Location = new Point(306, 155);
      this.numSaturation.Margin = new Padding(6, 0, 6, 0);
      this.numSaturation.Name = "numSaturation";
      this.numSaturation.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.numSaturation.Size = new Size(58, 20);
      this.numSaturation.TabIndex = 47;
      this.numSaturation.TabStop = false;
      this.numSaturation.ThemeName = "TelerikMetro";
      this.numSaturation.ValueChanged += new EventHandler(this.numSaturation_ValueChanged);
      this.numSaturation.TextChanged += new EventHandler(this.numSaturation_TextChanged);
      this.numLuminance.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.numLuminance.Location = new Point(306, 185);
      this.numLuminance.Margin = new Padding(6, 0, 6, 0);
      this.numLuminance.Name = "numLuminance";
      this.numLuminance.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.numLuminance.Size = new Size(58, 20);
      this.numLuminance.TabIndex = 48;
      this.numLuminance.TabStop = false;
      this.numLuminance.ThemeName = "TelerikMetro";
      this.numLuminance.ValueChanged += new EventHandler(this.numLuminance_ValueChanged);
      this.numLuminance.TextChanged += new EventHandler(this.numLuminance_TextChanged);
      this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.label1.AutoSize = false;
      this.label1.Location = new Point(260, 90);
      this.label1.Margin = new Padding(0);
      this.label1.Name = "label1";
      this.label1.Padding = new Padding(15, 0, 0, 0);
      this.label1.Size = new Size(40, 30);
      this.label1.TabIndex = 50;
      this.label1.TabStop = true;
      this.label1.Text = "A:";
      this.numAlpha.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.numAlpha.Location = new Point(306, 95);
      this.numAlpha.Margin = new Padding(6, 0, 6, 0);
      this.numAlpha.Maximum = new Decimal(new int[4]
      {
        (int) byte.MaxValue,
        0,
        0,
        0
      });
      this.numAlpha.Name = "numAlpha";
      this.numAlpha.NullableValue = new Decimal?(new Decimal(new int[4]
      {
        (int) byte.MaxValue,
        0,
        0,
        0
      }));
      this.numAlpha.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.numAlpha.Size = new Size(58, 20);
      this.numAlpha.TabIndex = 45;
      this.numAlpha.TabStop = false;
      this.numAlpha.ThemeName = "TelerikMetro";
      this.numAlpha.Value = new Decimal(new int[4]
      {
        (int) byte.MaxValue,
        0,
        0,
        0
      });
      this.numAlpha.ValueChanged += new EventHandler(this.numAlpha_ValueChanged);
      this.numAlpha.TextChanged += new EventHandler(this.numAlpha_TextChanged);
      this.numRed.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.numRed.Location = new Point(306, 5);
      this.numRed.Margin = new Padding(6, 0, 6, 0);
      this.numRed.Maximum = new Decimal(new int[4]
      {
        (int) byte.MaxValue,
        0,
        0,
        0
      });
      this.numRed.Name = "numRed";
      this.numRed.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.numRed.Size = new Size(58, 20);
      this.numRed.TabIndex = 42;
      this.numRed.TabStop = false;
      this.numRed.ThemeName = "TelerikMetro";
      this.numRed.ValueChanged += new EventHandler(this.numRed_ValueChanged);
      this.numRed.TextChanged += new EventHandler(this.numRed_TextChanged);
      this.numGreen.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.numGreen.Location = new Point(306, 35);
      this.numGreen.Margin = new Padding(6, 0, 6, 0);
      this.numGreen.Maximum = new Decimal(new int[4]
      {
        (int) byte.MaxValue,
        0,
        0,
        0
      });
      this.numGreen.Name = "numGreen";
      this.numGreen.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.numGreen.Size = new Size(58, 20);
      this.numGreen.TabIndex = 43;
      this.numGreen.TabStop = false;
      this.numGreen.ThemeName = "TelerikMetro";
      this.numGreen.ValueChanged += new EventHandler(this.numGreen_ValueChanged);
      this.numGreen.TextChanged += new EventHandler(this.numGreen_TextChanged);
      this.numBlue.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      this.numBlue.Location = new Point(306, 65);
      this.numBlue.Margin = new Padding(6, 0, 6, 0);
      this.numBlue.Maximum = new Decimal(new int[4]
      {
        (int) byte.MaxValue,
        0,
        0,
        0
      });
      this.numBlue.Name = "numBlue";
      this.numBlue.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.numBlue.Size = new Size(58, 20);
      this.numBlue.TabIndex = 44;
      this.numBlue.TabStop = false;
      this.numBlue.ThemeName = "TelerikMetro";
      this.numBlue.ValueChanged += new EventHandler(this.numBlue_ValueChanged);
      this.numBlue.TextChanged += new EventHandler(this.numBlue_TextChanged);
      this.proColorsSlider1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
      this.proColorsSlider1.ColorHSL = HslColor.FromAhsl(0.0, 1.0, 1.0);
      this.proColorsSlider1.ColorMode = ColorModes.Red;
      this.proColorsSlider1.ColorRGB = Color.FromArgb((int) byte.MaxValue, 0, 0);
      this.proColorsSlider1.Location = new Point(214, 3);
      this.proColorsSlider1.Name = "proColorsSlider1";
      this.proColorsSlider1.Position = 0;
      this.tableLayoutPanel1.SetRowSpan((Control) this.proColorsSlider1, 8);
      this.proColorsSlider1.Size = new Size(41, 217);
      this.proColorsSlider1.TabIndex = 51;
      this.proColors2DBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.proColors2DBox1.ColorHSL = HslColor.Empty;
      this.proColors2DBox1.ColorMode = ColorModes.Red;
      this.proColors2DBox1.ColorRGB = Color.Empty;
      this.proColors2DBox1.Location = new Point(6, 6);
      this.proColors2DBox1.Margin = new Padding(6, 6, 0, 6);
      this.proColors2DBox1.Name = "proColors2DBox1";
      this.tableLayoutPanel1.SetRowSpan((Control) this.proColors2DBox1, 8);
      this.proColors2DBox1.Size = new Size(204, 211);
      this.proColors2DBox1.TabIndex = 49;
      this.tableLayoutPanel1.ColumnCount = 4;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70f));
      this.tableLayoutPanel1.Controls.Add((Control) this.proColors2DBox1, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.numRed, 3, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.numHue, 3, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.radioH, 2, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.numBlue, 3, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.numSaturation, 3, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.numGreen, 3, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radioS, 2, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.numAlpha, 3, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.radioL, 2, 6);
      this.tableLayoutPanel1.Controls.Add((Control) this.numLuminance, 3, 6);
      this.tableLayoutPanel1.Controls.Add((Control) this.proColorsSlider1, 1, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radioR, 2, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.radioG, 2, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.radioB, 2, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.label1, 2, 3);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 8;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel1.Size = new Size(370, 223);
      this.tableLayoutPanel1.TabIndex = 52;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.Name = nameof (ProfessionalColors);
      this.Size = new Size(370, 223);
      this.radioL.EndInit();
      this.radioH.EndInit();
      this.radioB.EndInit();
      this.radioG.EndInit();
      this.radioS.EndInit();
      this.radioR.EndInit();
      this.numHue.EndInit();
      this.numSaturation.EndInit();
      this.numLuminance.EndInit();
      this.label1.EndInit();
      this.numAlpha.EndInit();
      this.numRed.EndInit();
      this.numGreen.EndInit();
      this.numBlue.EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
