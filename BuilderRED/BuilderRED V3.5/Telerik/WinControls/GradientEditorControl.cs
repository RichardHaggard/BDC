// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.GradientEditorControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Themes.GradientDialog;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class GradientEditorControl : UserControl
  {
    private const int offsetValue = 50;
    private bool isLoading;
    private IContainer components;
    private GroupBox groupBox1;
    private GradientBox gradientBox1;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private ColorBox colorBoxSolid;
    private System.Windows.Forms.Label label1;
    private TabPage tabPage2;
    private TabPage tabPage3;
    private GradientSlider gradientSlider1;
    private System.Windows.Forms.Label label9;
    private NumericUpDown numericLinearAngle;
    private System.Windows.Forms.Label label3;
    private ColorBox colorBoxSurround3;
    private ColorBox colorBoxSurround2;
    private ColorBox colorBoxSurround1;
    private ColorBox colorBoxRadialStart;
    private TabPage tabPage4;
    private NumericUpDown numericGelPercentage;
    private System.Windows.Forms.Label label14;
    private ColorBox colorBoxGelColor2;
    private System.Windows.Forms.Label label12;
    private ColorBox colorBoxGelColor1;
    private System.Windows.Forms.Label label10;
    private TabPage tabPage5;
    private NumericUpDown numericGlassPercentage;
    private System.Windows.Forms.Label label22;
    private ColorBox colorBoxGlass4;
    private System.Windows.Forms.Label label19;
    private ColorBox colorBoxGlass3;
    private System.Windows.Forms.Label label21;
    private ColorBox colorBoxGlass2;
    private System.Windows.Forms.Label label15;
    private ColorBox colorBoxGlass1;
    private System.Windows.Forms.Label label17;
    private TabPage tabPage6;
    private NumericUpDown numericOfficePercentage2;
    private System.Windows.Forms.Label label32;
    private NumericUpDown numericOfficePercentage1;
    private System.Windows.Forms.Label label23;
    private ColorBox colorBoxOfficeGlass4;
    private System.Windows.Forms.Label label25;
    private ColorBox colorBoxOfficeGlass3;
    private System.Windows.Forms.Label label27;
    private ColorBox colorBoxOfficeGlass2;
    private System.Windows.Forms.Label label29;
    private ColorBox colorBoxOfficeGlass1;
    private System.Windows.Forms.Label label31;
    private TabPage tabPage7;
    private NumericUpDown numericVistaPercentage2;
    private System.Windows.Forms.Label label33;
    private NumericUpDown numericVistaPercentage1;
    private System.Windows.Forms.Label label34;
    private ColorBox colorBoxVista4;
    private System.Windows.Forms.Label label36;
    private ColorBox colorBoxVista3;
    private System.Windows.Forms.Label label38;
    private ColorBox colorBoxVista2;
    private System.Windows.Forms.Label label40;
    private ColorBox colorBoxVista1;
    private System.Windows.Forms.Label label42;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.ComboBox comboBox3;
    private System.Windows.Forms.ComboBox comboBox7;
    private System.Windows.Forms.ComboBox comboBox5;
    private System.Windows.Forms.ComboBox comboBox4;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.ComboBox comboBox6;
    private System.Windows.Forms.Label label16;
    private GradientAngleControl gradientAngleControl1;
    private NumericUpDown numericRadial1;
    private System.Windows.Forms.Label label26;
    private NumericUpDown numericRadial2;
    private System.Windows.Forms.Label label13;
    private TabPage tabPage8;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.ComboBox comboBox8;
    private NumericUpDown numericOfficeRectPercentage2;
    private System.Windows.Forms.Label label28;
    private NumericUpDown numericOfficeRectPercentage1;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.Label label35;
    private System.Windows.Forms.Label label37;
    private System.Windows.Forms.Label label39;
    private System.Windows.Forms.Label label41;
    private ColorBox colorBoxOfficeGlassRect4;
    private ColorBox colorBoxOfficeGlassRect3;
    private ColorBox colorBoxOfficeGlassRect2;
    private ColorBox colorBoxOfficeGlassRect1;
    private TabPage tabPage9;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsLoading
    {
      get
      {
        return this.isLoading;
      }
      set
      {
        this.isLoading = value;
      }
    }

    public GradientEditorControl()
    {
      this.InitializeComponent();
      this.colorBoxSolid.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxRadialStart.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxSurround1.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxSurround2.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxSurround3.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxGelColor1.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxGelColor2.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxGlass1.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxGlass2.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxGlass3.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxGlass4.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxOfficeGlass1.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxOfficeGlass2.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxOfficeGlass3.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxOfficeGlass4.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxOfficeGlassRect1.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxOfficeGlassRect2.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxOfficeGlassRect3.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxOfficeGlassRect4.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxVista1.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxVista2.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxVista3.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.colorBoxVista4.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.gradientSlider1.ColorChanged += new ColorChangedEventHandler(this.colorBox_ColorChanged);
      this.gradientBox1.Paint += new PaintEventHandler(this.gradientBox1_Paint);
      this.Initialize();
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.gradientBox1.Fill;
      }
    }

    public void Initialize()
    {
      this.IsLoading = true;
      this.colorBoxSolid.BackColor = this.gradientBox1.Fill.BackColor;
      this.colorBoxRadialStart.BackColor = this.gradientBox1.Fill.BackColor;
      this.colorBoxGelColor1.BackColor = this.gradientBox1.Fill.BackColor;
      this.colorBoxGlass1.BackColor = this.gradientBox1.Fill.BackColor;
      this.colorBoxOfficeGlass1.BackColor = this.gradientBox1.Fill.BackColor;
      this.colorBoxOfficeGlassRect1.BackColor = this.gradientBox1.Fill.BackColor;
      this.colorBoxVista1.BackColor = this.gradientBox1.Fill.BackColor;
      this.colorBoxSurround1.BackColor = this.gradientBox1.Fill.BackColor2;
      this.colorBoxGelColor2.BackColor = this.gradientBox1.Fill.BackColor2;
      this.colorBoxGlass2.BackColor = this.gradientBox1.Fill.BackColor2;
      this.colorBoxOfficeGlass2.BackColor = this.gradientBox1.Fill.BackColor2;
      this.colorBoxOfficeGlassRect2.BackColor = this.gradientBox1.Fill.BackColor2;
      this.colorBoxVista2.BackColor = this.gradientBox1.Fill.BackColor2;
      this.colorBoxSurround2.BackColor = this.gradientBox1.Fill.BackColor3;
      this.colorBoxGlass3.BackColor = this.gradientBox1.Fill.BackColor3;
      this.colorBoxOfficeGlass3.BackColor = this.gradientBox1.Fill.BackColor3;
      this.colorBoxOfficeGlassRect3.BackColor = this.gradientBox1.Fill.BackColor3;
      this.colorBoxVista3.BackColor = this.gradientBox1.Fill.BackColor3;
      this.colorBoxSurround3.BackColor = this.gradientBox1.Fill.BackColor4;
      this.colorBoxGlass4.BackColor = this.gradientBox1.Fill.BackColor4;
      this.colorBoxOfficeGlass4.BackColor = this.gradientBox1.Fill.BackColor4;
      this.colorBoxOfficeGlassRect4.BackColor = this.gradientBox1.Fill.BackColor4;
      this.colorBoxVista4.BackColor = this.gradientBox1.Fill.BackColor4;
      this.numericLinearAngle.Value = (Decimal) this.gradientBox1.Fill.GradientAngle;
      this.numericGelPercentage.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage;
      this.numericGlassPercentage.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage;
      this.numericOfficePercentage1.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage;
      this.numericOfficePercentage2.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage2;
      this.numericOfficeRectPercentage1.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage;
      this.numericOfficeRectPercentage2.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage2;
      this.numericVistaPercentage1.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage;
      this.numericVistaPercentage2.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage2;
      this.numericRadial1.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage;
      this.numericRadial2.Value = (Decimal) this.gradientBox1.Fill.GradientPercentage2;
      this.SetupGradientSlider();
      this.SetSelectedTab();
      this.gradientSlider1.Invalidate();
      this.gradientBox1.Invalidate();
      this.IsLoading = false;
    }

    private void SetSelectedTab()
    {
      if (this.Fill.BackColor == Color.Transparent && this.Fill.BackColor2 == Color.Transparent && (this.Fill.BackColor3 == Color.Transparent && this.Fill.BackColor4 == Color.Transparent))
        this.tabControl1.SelectedIndex = 0;
      else
        this.tabControl1.SelectedIndex = (int) (this.gradientBox1.Fill.GradientStyle + 1);
    }

    private void gradientBox1_Paint(object sender, PaintEventArgs e)
    {
      if (this.tabControl1.SelectedTab != this.tabPage9)
        return;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      e.Graphics.DrawString("No Preview", new Font(FontFamily.GenericSansSerif, 25f), Brushes.Gray, new PointF(199f, 45f));
    }

    private void SetupGradientSlider()
    {
      this.gradientSlider1.Values.Clear();
      this.gradientSlider1.Values.Add(new GradientColorValue(this.gradientBox1.Fill.BackColor, 0.0f));
      if (this.gradientBox1.Fill.NumberOfColors > 1)
        this.gradientSlider1.Values.Add(new GradientColorValue(this.gradientBox1.Fill.BackColor2, this.gradientBox1.Fill.NumberOfColors == 2 ? 1f : this.gradientBox1.Fill.GradientPercentage));
      if (this.gradientBox1.Fill.NumberOfColors > 2)
        this.gradientSlider1.Values.Add(new GradientColorValue(this.gradientBox1.Fill.BackColor3, this.gradientBox1.Fill.NumberOfColors == 3 ? 1f : this.gradientBox1.Fill.GradientPercentage2));
      if (this.gradientBox1.Fill.NumberOfColors <= 3)
        return;
      this.gradientSlider1.Values.Add(new GradientColorValue(this.gradientBox1.Fill.BackColor4, 1f));
    }

    private void colorBox_ColorChanged(object sender, ColorChangedEventArgs args)
    {
      if (this.IsLoading)
        return;
      if (sender == this.colorBoxSolid || sender == this.colorBoxRadialStart || (sender == this.colorBoxGelColor1 || sender == this.colorBoxGlass1) || (sender == this.colorBoxOfficeGlass1 || sender == this.colorBoxOfficeGlassRect1 || sender == this.colorBoxVista1))
        this.gradientBox1.Fill.BackColor = args.SelectedColor;
      else if (sender == this.colorBoxSurround1 || sender == this.colorBoxGelColor2 || (sender == this.colorBoxGlass2 || sender == this.colorBoxOfficeGlass2) || (sender == this.colorBoxOfficeGlassRect2 || sender == this.colorBoxVista2))
        this.gradientBox1.Fill.BackColor2 = args.SelectedColor;
      else if (sender == this.colorBoxSurround2 || sender == this.colorBoxGlass3 || (sender == this.colorBoxOfficeGlass3 || sender == this.colorBoxOfficeGlassRect3) || sender == this.colorBoxVista3)
        this.gradientBox1.Fill.BackColor3 = args.SelectedColor;
      else if (sender == this.colorBoxSurround3 || sender == this.colorBoxGlass4 || (sender == this.colorBoxOfficeGlass4 || sender == this.colorBoxOfficeGlassRect4) || sender == this.colorBoxVista4)
      {
        this.gradientBox1.Fill.BackColor4 = args.SelectedColor;
      }
      else
      {
        if (sender != this.gradientSlider1)
          return;
        this.gradientBox1.Fill.BackColor = this.gradientSlider1.Values[0].ColorValue;
        if (this.gradientSlider1.Values.Count > 1)
        {
          this.gradientBox1.Fill.BackColor2 = this.gradientSlider1.Values[1].ColorValue;
          this.gradientBox1.Fill.GradientPercentage = this.gradientSlider1.Values[1].ColorPosition;
        }
        if (this.gradientSlider1.Values.Count > 2)
        {
          this.gradientBox1.Fill.BackColor3 = this.gradientSlider1.Values[2].ColorValue;
          this.gradientBox1.Fill.GradientPercentage2 = this.gradientSlider1.Values[2].ColorPosition;
        }
        if (this.gradientSlider1.Values.Count > 3)
          this.gradientBox1.Fill.BackColor4 = this.gradientSlider1.Values[3].ColorValue;
        this.gradientBox1.Fill.NumberOfColors = this.gradientSlider1.Values.Count;
      }
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      switch (this.tabControl1.SelectedIndex)
      {
        case 0:
          this.gradientBox1.Fill.BackColor = Color.Transparent;
          this.gradientBox1.Fill.BackColor2 = Color.Transparent;
          this.gradientBox1.Fill.BackColor3 = Color.Transparent;
          this.gradientBox1.Fill.BackColor4 = Color.Transparent;
          break;
        case 1:
          this.gradientBox1.Fill.GradientStyle = GradientStyles.Solid;
          this.gradientBox1.Fill.BackColor = this.colorBoxSolid.BackColor;
          break;
        case 2:
          this.gradientBox1.Fill.GradientStyle = GradientStyles.Linear;
          this.gradientBox1.Fill.BackColor = this.gradientSlider1.Values[0].ColorValue;
          if (this.gradientSlider1.Values.Count == 2)
          {
            this.gradientBox1.Fill.BackColor2 = this.gradientSlider1.Values[1].ColorValue;
            break;
          }
          if (this.gradientSlider1.Values.Count == 3)
          {
            this.gradientBox1.Fill.BackColor2 = this.gradientSlider1.Values[1].ColorValue;
            this.gradientBox1.Fill.GradientPercentage = this.gradientSlider1.Values[1].ColorPosition;
            this.gradientBox1.Fill.BackColor3 = this.gradientSlider1.Values[2].ColorValue;
            break;
          }
          if (this.gradientSlider1.Values.Count == 4)
          {
            this.gradientBox1.Fill.BackColor2 = this.gradientSlider1.Values[1].ColorValue;
            this.gradientBox1.Fill.GradientPercentage = this.gradientSlider1.Values[1].ColorPosition;
            this.gradientBox1.Fill.BackColor3 = this.gradientSlider1.Values[2].ColorValue;
            this.gradientBox1.Fill.GradientPercentage2 = this.gradientSlider1.Values[2].ColorPosition;
            this.gradientBox1.Fill.BackColor4 = this.gradientSlider1.Values[3].ColorValue;
            break;
          }
          break;
        case 3:
          this.gradientBox1.Fill.GradientStyle = GradientStyles.Radial;
          this.gradientBox1.Fill.BackColor = this.colorBoxRadialStart.BackColor;
          this.gradientBox1.Fill.BackColor2 = this.colorBoxSurround1.BackColor;
          this.gradientBox1.Fill.BackColor3 = this.colorBoxSurround2.BackColor;
          this.gradientBox1.Fill.BackColor4 = this.colorBoxSurround3.BackColor;
          this.gradientBox1.Fill.NumberOfColors = 4;
          this.gradientBox1.Fill.GradientPercentage = (float) this.numericRadial1.Value;
          this.gradientBox1.Fill.GradientPercentage2 = (float) this.numericRadial2.Value;
          break;
        case 4:
          this.gradientBox1.Fill.GradientStyle = GradientStyles.Glass;
          this.gradientBox1.Fill.BackColor = this.colorBoxGlass1.BackColor;
          this.gradientBox1.Fill.BackColor2 = this.colorBoxGlass2.BackColor;
          this.gradientBox1.Fill.BackColor3 = this.colorBoxGlass3.BackColor;
          this.gradientBox1.Fill.BackColor4 = this.colorBoxGlass4.BackColor;
          this.gradientBox1.Fill.GradientPercentage = (float) this.numericGlassPercentage.Value;
          break;
        case 5:
          this.gradientBox1.Fill.GradientStyle = GradientStyles.OfficeGlass;
          this.gradientBox1.Fill.BackColor = this.colorBoxOfficeGlass1.BackColor;
          this.gradientBox1.Fill.BackColor2 = this.colorBoxOfficeGlass2.BackColor;
          this.gradientBox1.Fill.BackColor3 = this.colorBoxOfficeGlass3.BackColor;
          this.gradientBox1.Fill.BackColor4 = this.colorBoxOfficeGlass4.BackColor;
          this.gradientBox1.Fill.GradientPercentage = (float) this.numericOfficePercentage1.Value;
          this.gradientBox1.Fill.GradientPercentage2 = (float) this.numericOfficePercentage2.Value;
          break;
        case 6:
          this.gradientBox1.Fill.GradientStyle = GradientStyles.OfficeGlassRect;
          this.gradientBox1.Fill.BackColor = this.colorBoxOfficeGlassRect1.BackColor;
          this.gradientBox1.Fill.BackColor2 = this.colorBoxOfficeGlassRect2.BackColor;
          this.gradientBox1.Fill.BackColor3 = this.colorBoxOfficeGlassRect3.BackColor;
          this.gradientBox1.Fill.BackColor4 = this.colorBoxOfficeGlassRect4.BackColor;
          this.gradientBox1.Fill.GradientPercentage = (float) this.numericOfficeRectPercentage1.Value;
          this.gradientBox1.Fill.GradientPercentage2 = (float) this.numericOfficeRectPercentage2.Value;
          break;
        case 7:
          this.gradientBox1.Fill.GradientStyle = GradientStyles.Gel;
          this.gradientBox1.Fill.BackColor = this.colorBoxGelColor1.BackColor;
          this.gradientBox1.Fill.BackColor2 = this.colorBoxGelColor2.BackColor;
          this.gradientBox1.Fill.GradientPercentage = (float) this.numericGelPercentage.Value;
          break;
        case 8:
          this.gradientBox1.Fill.GradientStyle = GradientStyles.Vista;
          this.gradientBox1.Fill.BackColor = this.colorBoxVista1.BackColor;
          this.gradientBox1.Fill.BackColor2 = this.colorBoxVista2.BackColor;
          this.gradientBox1.Fill.BackColor3 = this.colorBoxVista3.BackColor;
          this.gradientBox1.Fill.BackColor4 = this.colorBoxVista4.BackColor;
          this.gradientBox1.Fill.GradientPercentage = (float) this.numericVistaPercentage1.Value;
          this.gradientBox1.Fill.GradientPercentage2 = (float) this.numericVistaPercentage2.Value;
          break;
      }
      this.gradientSlider1.Invalidate();
    }

    private void gradientAngle_ValueChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      this.gradientBox1.Fill.GradientAngle = (float) this.numericLinearAngle.Value;
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
      this.gradientAngleControl1.GradientAngle = (double) (float) this.numericLinearAngle.Value;
    }

    private void gradientPercentage_ValueChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      this.gradientBox1.Fill.GradientPercentage = (float) this.numericGelPercentage.Value;
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void gradientPercentage2_ValueChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      this.gradientBox1.Fill.GradientPercentage2 = (float) this.numericGelPercentage.Value;
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      switch (this.comboBox1.SelectedIndex)
      {
        case 0:
          this.colorBoxOfficeGlass4.BackColor = Color.Red;
          this.colorBoxOfficeGlass2.BackColor = Color.FromArgb((int) Color.Red.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxOfficeGlass3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass2.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxOfficeGlass1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass3.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          break;
        case 1:
          this.colorBoxOfficeGlass4.BackColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
          this.colorBoxOfficeGlass2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass4.BackColor.R, (int) this.colorBoxOfficeGlass4.BackColor.G - 50, (int) this.colorBoxOfficeGlass4.BackColor.B);
          this.colorBoxOfficeGlass3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass2.BackColor.R, (int) this.colorBoxOfficeGlass2.BackColor.G - 50, (int) this.colorBoxOfficeGlass2.BackColor.B);
          this.colorBoxOfficeGlass1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass3.BackColor.R, (int) this.colorBoxOfficeGlass3.BackColor.G - 50, (int) this.colorBoxOfficeGlass3.BackColor.B);
          break;
        case 2:
          this.colorBoxOfficeGlass4.BackColor = Color.Blue;
          this.colorBoxOfficeGlass2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass4.BackColor.R, (int) this.colorBoxOfficeGlass4.BackColor.G, (int) this.colorBoxOfficeGlass4.BackColor.B - 50);
          this.colorBoxOfficeGlass3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass2.BackColor.R, (int) this.colorBoxOfficeGlass2.BackColor.G, (int) this.colorBoxOfficeGlass2.BackColor.B - 50);
          this.colorBoxOfficeGlass1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass3.BackColor.R, (int) this.colorBoxOfficeGlass3.BackColor.G, (int) this.colorBoxOfficeGlass3.BackColor.B - 50);
          break;
        case 3:
          this.colorBoxOfficeGlass4.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
          this.colorBoxOfficeGlass2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass4.BackColor.R - 50, (int) this.colorBoxOfficeGlass4.BackColor.G - 50, (int) this.colorBoxOfficeGlass4.BackColor.B);
          this.colorBoxOfficeGlass3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass2.BackColor.R - 50, (int) this.colorBoxOfficeGlass2.BackColor.G - 50, (int) this.colorBoxOfficeGlass2.BackColor.B);
          this.colorBoxOfficeGlass1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass3.BackColor.R - 50, (int) this.colorBoxOfficeGlass3.BackColor.G - 50, (int) this.colorBoxOfficeGlass3.BackColor.B);
          break;
        case 4:
          this.colorBoxOfficeGlass4.BackColor = Color.FromArgb(0, 0, 0);
          this.colorBoxOfficeGlass2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass4.BackColor.R + 50, (int) this.colorBoxOfficeGlass4.BackColor.G + 50, (int) this.colorBoxOfficeGlass4.BackColor.B + 50);
          this.colorBoxOfficeGlass3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass2.BackColor.R + 50, (int) this.colorBoxOfficeGlass2.BackColor.G + 50, (int) this.colorBoxOfficeGlass2.BackColor.B + 50);
          this.colorBoxOfficeGlass1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass3.BackColor.R + 50, (int) this.colorBoxOfficeGlass3.BackColor.G + 50, (int) this.colorBoxOfficeGlass3.BackColor.B + 50);
          break;
        case 5:
          this.colorBoxOfficeGlass4.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          this.colorBoxOfficeGlass2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass4.BackColor.R - 50, (int) this.colorBoxOfficeGlass4.BackColor.G - 50, (int) this.colorBoxOfficeGlass4.BackColor.B - 50);
          this.colorBoxOfficeGlass3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass2.BackColor.R - 50, (int) this.colorBoxOfficeGlass2.BackColor.G - 50, (int) this.colorBoxOfficeGlass2.BackColor.B - 50);
          this.colorBoxOfficeGlass1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass3.BackColor.R - 50, (int) this.colorBoxOfficeGlass3.BackColor.G - 50, (int) this.colorBoxOfficeGlass3.BackColor.B - 50);
          break;
        case 6:
          this.colorBoxOfficeGlass4.BackColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
          this.colorBoxOfficeGlass2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass4.BackColor.R - 50, (int) this.colorBoxOfficeGlass4.BackColor.G, (int) this.colorBoxOfficeGlass4.BackColor.B);
          this.colorBoxOfficeGlass3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass2.BackColor.R - 50, (int) this.colorBoxOfficeGlass2.BackColor.G, (int) this.colorBoxOfficeGlass2.BackColor.B);
          this.colorBoxOfficeGlass1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass3.BackColor.R - 50, (int) this.colorBoxOfficeGlass3.BackColor.G - 50, (int) this.colorBoxOfficeGlass3.BackColor.B);
          break;
        case 7:
          this.colorBoxOfficeGlass4.BackColor = Color.FromArgb(128, 0, 128);
          this.colorBoxOfficeGlass2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlass4.BackColor.R + 50 - 30, (int) this.colorBoxOfficeGlass4.BackColor.G, (int) this.colorBoxOfficeGlass4.BackColor.B + 50 - 30);
          ColorBox colorBoxOfficeGlass3 = this.colorBoxOfficeGlass3;
          int red1 = (int) this.colorBoxOfficeGlass2.BackColor.R + 50 - 30;
          Color backColor = this.colorBoxOfficeGlass2.BackColor;
          int g1 = (int) backColor.G;
          backColor = this.colorBoxOfficeGlass2.BackColor;
          int blue1 = (int) backColor.B + 50 - 30;
          Color color1 = Color.FromArgb(red1, g1, blue1);
          colorBoxOfficeGlass3.BackColor = color1;
          ColorBox colorBoxOfficeGlass1 = this.colorBoxOfficeGlass1;
          backColor = this.colorBoxOfficeGlass3.BackColor;
          int red2 = (int) backColor.R + 50 - 30;
          backColor = this.colorBoxOfficeGlass3.BackColor;
          int g2 = (int) backColor.G;
          backColor = this.colorBoxOfficeGlass3.BackColor;
          int blue2 = (int) backColor.B + 50 - 30;
          Color color2 = Color.FromArgb(red2, g2, blue2);
          colorBoxOfficeGlass1.BackColor = color2;
          break;
      }
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      switch (this.comboBox8.SelectedIndex)
      {
        case 0:
          this.colorBoxOfficeGlassRect4.BackColor = Color.Red;
          this.colorBoxOfficeGlassRect2.BackColor = Color.FromArgb((int) Color.Red.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxOfficeGlassRect3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect2.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxOfficeGlassRect1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect3.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          break;
        case 1:
          this.colorBoxOfficeGlassRect4.BackColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
          this.colorBoxOfficeGlassRect2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect4.BackColor.R, (int) this.colorBoxOfficeGlassRect4.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect4.BackColor.B);
          this.colorBoxOfficeGlassRect3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect2.BackColor.R, (int) this.colorBoxOfficeGlassRect2.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect2.BackColor.B);
          this.colorBoxOfficeGlassRect1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect3.BackColor.R, (int) this.colorBoxOfficeGlassRect3.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect3.BackColor.B);
          break;
        case 2:
          this.colorBoxOfficeGlassRect4.BackColor = Color.Blue;
          this.colorBoxOfficeGlassRect2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect4.BackColor.R, (int) this.colorBoxOfficeGlassRect4.BackColor.G, (int) this.colorBoxOfficeGlassRect4.BackColor.B - 50);
          this.colorBoxOfficeGlassRect3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect2.BackColor.R, (int) this.colorBoxOfficeGlassRect2.BackColor.G, (int) this.colorBoxOfficeGlassRect2.BackColor.B - 50);
          this.colorBoxOfficeGlassRect1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect3.BackColor.R, (int) this.colorBoxOfficeGlassRect3.BackColor.G, (int) this.colorBoxOfficeGlassRect3.BackColor.B - 50);
          break;
        case 3:
          this.colorBoxOfficeGlassRect4.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
          this.colorBoxOfficeGlassRect2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect4.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect4.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect4.BackColor.B);
          this.colorBoxOfficeGlassRect3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect2.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect2.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect2.BackColor.B);
          this.colorBoxOfficeGlassRect1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect3.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect3.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect3.BackColor.B);
          break;
        case 4:
          this.colorBoxOfficeGlassRect4.BackColor = Color.FromArgb(0, 0, 0);
          this.colorBoxOfficeGlassRect2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect4.BackColor.R + 50, (int) this.colorBoxOfficeGlassRect4.BackColor.G + 50, (int) this.colorBoxOfficeGlassRect4.BackColor.B + 50);
          this.colorBoxOfficeGlassRect3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect2.BackColor.R + 50, (int) this.colorBoxOfficeGlassRect2.BackColor.G + 50, (int) this.colorBoxOfficeGlassRect2.BackColor.B + 50);
          this.colorBoxOfficeGlassRect1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect3.BackColor.R + 50, (int) this.colorBoxOfficeGlassRect3.BackColor.G + 50, (int) this.colorBoxOfficeGlassRect3.BackColor.B + 50);
          break;
        case 5:
          this.colorBoxOfficeGlassRect4.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          this.colorBoxOfficeGlassRect2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect4.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect4.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect4.BackColor.B - 50);
          this.colorBoxOfficeGlassRect3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect2.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect2.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect2.BackColor.B - 50);
          this.colorBoxOfficeGlassRect1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect3.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect3.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect3.BackColor.B - 50);
          break;
        case 6:
          this.colorBoxOfficeGlassRect4.BackColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
          this.colorBoxOfficeGlassRect2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect4.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect4.BackColor.G, (int) this.colorBoxOfficeGlassRect4.BackColor.B);
          this.colorBoxOfficeGlassRect3.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect2.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect2.BackColor.G, (int) this.colorBoxOfficeGlassRect2.BackColor.B);
          this.colorBoxOfficeGlassRect1.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect3.BackColor.R - 50, (int) this.colorBoxOfficeGlassRect3.BackColor.G - 50, (int) this.colorBoxOfficeGlassRect3.BackColor.B);
          break;
        case 7:
          this.colorBoxOfficeGlassRect4.BackColor = Color.FromArgb(128, 0, 128);
          this.colorBoxOfficeGlassRect2.BackColor = Color.FromArgb((int) this.colorBoxOfficeGlassRect4.BackColor.R + 50 - 30, (int) this.colorBoxOfficeGlassRect4.BackColor.G, (int) this.colorBoxOfficeGlassRect4.BackColor.B + 50 - 30);
          ColorBox officeGlassRect3 = this.colorBoxOfficeGlassRect3;
          int red1 = (int) this.colorBoxOfficeGlassRect2.BackColor.R + 50 - 30;
          Color backColor = this.colorBoxOfficeGlassRect2.BackColor;
          int g1 = (int) backColor.G;
          backColor = this.colorBoxOfficeGlassRect2.BackColor;
          int blue1 = (int) backColor.B + 50 - 30;
          Color color1 = Color.FromArgb(red1, g1, blue1);
          officeGlassRect3.BackColor = color1;
          ColorBox officeGlassRect1 = this.colorBoxOfficeGlassRect1;
          backColor = this.colorBoxOfficeGlassRect3.BackColor;
          int red2 = (int) backColor.R + 50 - 30;
          backColor = this.colorBoxOfficeGlassRect3.BackColor;
          int g2 = (int) backColor.G;
          backColor = this.colorBoxOfficeGlassRect3.BackColor;
          int blue2 = (int) backColor.B + 50 - 30;
          Color color2 = Color.FromArgb(red2, g2, blue2);
          officeGlassRect1.BackColor = color2;
          break;
      }
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      switch (this.comboBox2.SelectedIndex)
      {
        case 0:
          this.colorBoxGelColor1.BackColor = Color.Red;
          this.colorBoxGelColor2.BackColor = Color.FromArgb((int) Color.Red.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          break;
        case 1:
          this.colorBoxGelColor1.BackColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
          this.colorBoxGelColor2.BackColor = Color.FromArgb((int) this.colorBoxGelColor1.BackColor.R, (int) this.colorBoxGelColor1.BackColor.G - 50, (int) this.colorBoxGelColor1.BackColor.B);
          break;
        case 2:
          this.colorBoxGelColor1.BackColor = Color.Blue;
          this.colorBoxGelColor2.BackColor = Color.FromArgb((int) this.colorBoxGelColor1.BackColor.R, (int) this.colorBoxGelColor1.BackColor.G, (int) this.colorBoxGelColor1.BackColor.B - 50);
          break;
        case 3:
          this.colorBoxGelColor1.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
          this.colorBoxGelColor2.BackColor = Color.FromArgb((int) this.colorBoxGelColor1.BackColor.R - 50, (int) this.colorBoxGelColor1.BackColor.G - 50, (int) this.colorBoxGelColor1.BackColor.B);
          break;
        case 4:
          this.colorBoxGelColor1.BackColor = Color.FromArgb(0, 0, 0);
          this.colorBoxGelColor2.BackColor = Color.FromArgb((int) this.colorBoxGelColor1.BackColor.R + 50, (int) this.colorBoxGelColor1.BackColor.G + 50, (int) this.colorBoxGelColor1.BackColor.B + 50);
          break;
        case 5:
          this.colorBoxGelColor1.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          this.colorBoxGelColor2.BackColor = Color.FromArgb((int) this.colorBoxGelColor1.BackColor.R - 50, (int) this.colorBoxGelColor1.BackColor.G - 50, (int) this.colorBoxGelColor1.BackColor.B - 50);
          break;
        case 6:
          this.colorBoxGelColor1.BackColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
          this.colorBoxGelColor2.BackColor = Color.FromArgb((int) this.colorBoxGelColor1.BackColor.R - 50, (int) this.colorBoxGelColor1.BackColor.G, (int) this.colorBoxGelColor1.BackColor.B);
          break;
        case 7:
          this.colorBoxGelColor1.BackColor = Color.FromArgb(128, 0, 128);
          this.colorBoxGelColor2.BackColor = Color.FromArgb((int) this.colorBoxGelColor1.BackColor.R + 50 - 30, (int) this.colorBoxGelColor1.BackColor.G, (int) this.colorBoxGelColor1.BackColor.B + 50 - 30);
          break;
      }
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      switch (this.comboBox3.SelectedIndex)
      {
        case 0:
          this.colorBoxVista4.BackColor = Color.Red;
          this.colorBoxVista2.BackColor = Color.FromArgb((int) Color.Red.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxVista3.BackColor = Color.FromArgb((int) this.colorBoxVista2.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxVista1.BackColor = Color.FromArgb((int) this.colorBoxVista3.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          break;
        case 1:
          this.colorBoxVista4.BackColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
          this.colorBoxVista2.BackColor = Color.FromArgb((int) this.colorBoxVista4.BackColor.R, (int) this.colorBoxVista4.BackColor.G - 50, (int) this.colorBoxVista4.BackColor.B);
          this.colorBoxVista3.BackColor = Color.FromArgb((int) this.colorBoxVista2.BackColor.R, (int) this.colorBoxVista2.BackColor.G - 50, (int) this.colorBoxVista2.BackColor.B);
          this.colorBoxVista1.BackColor = Color.FromArgb((int) this.colorBoxVista3.BackColor.R, (int) this.colorBoxVista3.BackColor.G - 50, (int) this.colorBoxVista3.BackColor.B);
          break;
        case 2:
          this.colorBoxVista4.BackColor = Color.Blue;
          this.colorBoxVista2.BackColor = Color.FromArgb((int) this.colorBoxVista4.BackColor.R, (int) this.colorBoxVista4.BackColor.G, (int) this.colorBoxVista4.BackColor.B - 50);
          this.colorBoxVista3.BackColor = Color.FromArgb((int) this.colorBoxVista2.BackColor.R, (int) this.colorBoxVista2.BackColor.G, (int) this.colorBoxVista2.BackColor.B - 50);
          this.colorBoxVista1.BackColor = Color.FromArgb((int) this.colorBoxVista3.BackColor.R, (int) this.colorBoxVista3.BackColor.G, (int) this.colorBoxVista3.BackColor.B - 50);
          break;
        case 3:
          this.colorBoxVista4.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
          this.colorBoxVista2.BackColor = Color.FromArgb((int) this.colorBoxVista4.BackColor.R - 50, (int) this.colorBoxVista4.BackColor.G - 50, (int) this.colorBoxVista4.BackColor.B);
          this.colorBoxVista3.BackColor = Color.FromArgb((int) this.colorBoxVista2.BackColor.R - 50, (int) this.colorBoxVista2.BackColor.G - 50, (int) this.colorBoxVista2.BackColor.B);
          this.colorBoxVista1.BackColor = Color.FromArgb((int) this.colorBoxVista3.BackColor.R - 50, (int) this.colorBoxVista3.BackColor.G - 50, (int) this.colorBoxVista3.BackColor.B);
          break;
        case 4:
          this.colorBoxVista4.BackColor = Color.FromArgb(0, 0, 0);
          this.colorBoxVista2.BackColor = Color.FromArgb((int) this.colorBoxVista4.BackColor.R + 50, (int) this.colorBoxVista4.BackColor.G + 50, (int) this.colorBoxVista4.BackColor.B + 50);
          this.colorBoxVista3.BackColor = Color.FromArgb((int) this.colorBoxVista2.BackColor.R + 50, (int) this.colorBoxVista2.BackColor.G + 50, (int) this.colorBoxVista2.BackColor.B + 50);
          this.colorBoxVista1.BackColor = Color.FromArgb((int) this.colorBoxVista3.BackColor.R + 50, (int) this.colorBoxVista3.BackColor.G + 50, (int) this.colorBoxVista3.BackColor.B + 50);
          break;
        case 5:
          this.colorBoxVista4.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          this.colorBoxVista2.BackColor = Color.FromArgb((int) this.colorBoxVista4.BackColor.R - 50, (int) this.colorBoxVista4.BackColor.G - 50, (int) this.colorBoxVista4.BackColor.B - 50);
          this.colorBoxVista3.BackColor = Color.FromArgb((int) this.colorBoxVista2.BackColor.R - 50, (int) this.colorBoxVista2.BackColor.G - 50, (int) this.colorBoxVista2.BackColor.B - 50);
          this.colorBoxVista1.BackColor = Color.FromArgb((int) this.colorBoxVista3.BackColor.R - 50, (int) this.colorBoxVista3.BackColor.G - 50, (int) this.colorBoxVista3.BackColor.B - 50);
          break;
        case 6:
          this.colorBoxVista4.BackColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
          this.colorBoxVista2.BackColor = Color.FromArgb((int) this.colorBoxVista4.BackColor.R - 50, (int) this.colorBoxVista4.BackColor.G, (int) this.colorBoxVista4.BackColor.B);
          this.colorBoxVista3.BackColor = Color.FromArgb((int) this.colorBoxVista2.BackColor.R - 50, (int) this.colorBoxVista2.BackColor.G, (int) this.colorBoxVista2.BackColor.B);
          this.colorBoxVista1.BackColor = Color.FromArgb((int) this.colorBoxVista3.BackColor.R - 50, (int) this.colorBoxVista3.BackColor.G - 50, (int) this.colorBoxVista3.BackColor.B);
          break;
        case 7:
          this.colorBoxVista4.BackColor = Color.FromArgb(128, 0, 128);
          this.colorBoxVista2.BackColor = Color.FromArgb((int) this.colorBoxVista4.BackColor.R + 50 - 30, (int) this.colorBoxVista4.BackColor.G, (int) this.colorBoxVista4.BackColor.B + 50 - 30);
          ColorBox colorBoxVista3 = this.colorBoxVista3;
          int red1 = (int) this.colorBoxVista2.BackColor.R + 50 - 30;
          Color backColor = this.colorBoxVista2.BackColor;
          int g1 = (int) backColor.G;
          backColor = this.colorBoxVista2.BackColor;
          int blue1 = (int) backColor.B + 50 - 30;
          Color color1 = Color.FromArgb(red1, g1, blue1);
          colorBoxVista3.BackColor = color1;
          ColorBox colorBoxVista1 = this.colorBoxVista1;
          backColor = this.colorBoxVista3.BackColor;
          int red2 = (int) backColor.R + 50 - 30;
          backColor = this.colorBoxVista3.BackColor;
          int g2 = (int) backColor.G;
          backColor = this.colorBoxVista3.BackColor;
          int blue2 = (int) backColor.B + 50 - 30;
          Color color2 = Color.FromArgb(red2, g2, blue2);
          colorBoxVista1.BackColor = color2;
          break;
      }
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      switch (this.comboBox7.SelectedIndex)
      {
        case 0:
          this.colorBoxSolid.BackColor = Color.Red;
          break;
        case 1:
          this.colorBoxSolid.BackColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
          break;
        case 2:
          this.colorBoxSolid.BackColor = Color.Blue;
          break;
        case 3:
          this.colorBoxSolid.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
          break;
        case 4:
          this.colorBoxSolid.BackColor = Color.FromArgb(0, 0, 0);
          break;
        case 5:
          this.colorBoxSolid.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          break;
        case 6:
          this.colorBoxSolid.BackColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
          break;
        case 7:
          this.colorBoxSolid.BackColor = Color.FromArgb(128, 0, 128);
          break;
      }
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      switch (this.comboBox5.SelectedIndex)
      {
        case 0:
          this.colorBoxRadialStart.BackColor = Color.Red;
          this.colorBoxSurround1.BackColor = Color.FromArgb((int) Color.Red.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxSurround2.BackColor = Color.FromArgb((int) this.colorBoxSurround1.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxSurround3.BackColor = Color.FromArgb((int) this.colorBoxSurround2.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          break;
        case 1:
          this.colorBoxRadialStart.BackColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
          this.colorBoxSurround1.BackColor = Color.FromArgb((int) this.colorBoxRadialStart.BackColor.R, (int) this.colorBoxRadialStart.BackColor.G - 50, (int) this.colorBoxRadialStart.BackColor.B);
          this.colorBoxSurround2.BackColor = Color.FromArgb((int) this.colorBoxSurround1.BackColor.R, (int) this.colorBoxSurround1.BackColor.G - 50, (int) this.colorBoxSurround1.BackColor.B);
          this.colorBoxSurround3.BackColor = Color.FromArgb((int) this.colorBoxSurround2.BackColor.R, (int) this.colorBoxSurround2.BackColor.G - 50, (int) this.colorBoxSurround2.BackColor.B);
          break;
        case 2:
          this.colorBoxRadialStart.BackColor = Color.Blue;
          this.colorBoxSurround1.BackColor = Color.FromArgb((int) this.colorBoxRadialStart.BackColor.R, (int) this.colorBoxRadialStart.BackColor.G, (int) this.colorBoxRadialStart.BackColor.B - 50);
          this.colorBoxSurround2.BackColor = Color.FromArgb((int) this.colorBoxSurround1.BackColor.R, (int) this.colorBoxSurround1.BackColor.G, (int) this.colorBoxSurround1.BackColor.B - 50);
          this.colorBoxSurround3.BackColor = Color.FromArgb((int) this.colorBoxSurround2.BackColor.R, (int) this.colorBoxSurround2.BackColor.G, (int) this.colorBoxSurround2.BackColor.B - 50);
          break;
        case 3:
          this.colorBoxRadialStart.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
          this.colorBoxSurround1.BackColor = Color.FromArgb((int) this.colorBoxRadialStart.BackColor.R - 50, (int) this.colorBoxRadialStart.BackColor.G - 50, (int) this.colorBoxRadialStart.BackColor.B);
          this.colorBoxSurround2.BackColor = Color.FromArgb((int) this.colorBoxSurround1.BackColor.R - 50, (int) this.colorBoxSurround1.BackColor.G - 50, (int) this.colorBoxSurround1.BackColor.B);
          this.colorBoxSurround3.BackColor = Color.FromArgb((int) this.colorBoxSurround2.BackColor.R - 50, (int) this.colorBoxSurround2.BackColor.G - 50, (int) this.colorBoxSurround2.BackColor.B);
          break;
        case 4:
          this.colorBoxRadialStart.BackColor = Color.FromArgb(0, 0, 0);
          this.colorBoxSurround1.BackColor = Color.FromArgb((int) this.colorBoxRadialStart.BackColor.R + 50, (int) this.colorBoxRadialStart.BackColor.G + 50, (int) this.colorBoxRadialStart.BackColor.B + 50);
          this.colorBoxSurround2.BackColor = Color.FromArgb((int) this.colorBoxSurround1.BackColor.R + 50, (int) this.colorBoxSurround1.BackColor.G + 50, (int) this.colorBoxSurround1.BackColor.B + 50);
          this.colorBoxSurround3.BackColor = Color.FromArgb((int) this.colorBoxSurround2.BackColor.R + 50, (int) this.colorBoxSurround2.BackColor.G + 50, (int) this.colorBoxSurround2.BackColor.B + 50);
          break;
        case 5:
          this.colorBoxRadialStart.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          this.colorBoxSurround1.BackColor = Color.FromArgb((int) this.colorBoxRadialStart.BackColor.R - 50, (int) this.colorBoxRadialStart.BackColor.G - 50, (int) this.colorBoxRadialStart.BackColor.B - 50);
          this.colorBoxSurround2.BackColor = Color.FromArgb((int) this.colorBoxSurround1.BackColor.R - 50, (int) this.colorBoxSurround1.BackColor.G - 50, (int) this.colorBoxSurround1.BackColor.B - 50);
          this.colorBoxSurround3.BackColor = Color.FromArgb((int) this.colorBoxSurround2.BackColor.R - 50, (int) this.colorBoxSurround2.BackColor.G - 50, (int) this.colorBoxSurround2.BackColor.B - 50);
          break;
        case 6:
          this.colorBoxRadialStart.BackColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
          this.colorBoxSurround1.BackColor = Color.FromArgb((int) this.colorBoxRadialStart.BackColor.R - 50, (int) this.colorBoxRadialStart.BackColor.G, (int) this.colorBoxRadialStart.BackColor.B);
          this.colorBoxSurround2.BackColor = Color.FromArgb((int) this.colorBoxSurround1.BackColor.R - 50, (int) this.colorBoxSurround1.BackColor.G, (int) this.colorBoxSurround1.BackColor.B);
          this.colorBoxSurround3.BackColor = Color.FromArgb((int) this.colorBoxSurround2.BackColor.R - 50, (int) this.colorBoxSurround2.BackColor.G - 50, (int) this.colorBoxSurround2.BackColor.B);
          break;
        case 7:
          this.colorBoxRadialStart.BackColor = Color.FromArgb(128, 0, 128);
          this.colorBoxSurround1.BackColor = Color.FromArgb((int) this.colorBoxRadialStart.BackColor.R + 50 - 30, (int) this.colorBoxRadialStart.BackColor.G, (int) this.colorBoxRadialStart.BackColor.B + 50 - 30);
          ColorBox colorBoxSurround2 = this.colorBoxSurround2;
          int red1 = (int) this.colorBoxSurround1.BackColor.R + 50 - 30;
          Color backColor = this.colorBoxSurround1.BackColor;
          int g1 = (int) backColor.G;
          backColor = this.colorBoxSurround1.BackColor;
          int blue1 = (int) backColor.B + 50 - 30;
          Color color1 = Color.FromArgb(red1, g1, blue1);
          colorBoxSurround2.BackColor = color1;
          ColorBox colorBoxSurround3 = this.colorBoxSurround3;
          backColor = this.colorBoxSurround2.BackColor;
          int red2 = (int) backColor.R + 50 - 30;
          backColor = this.colorBoxSurround2.BackColor;
          int g2 = (int) backColor.G;
          backColor = this.colorBoxSurround2.BackColor;
          int blue2 = (int) backColor.B + 50 - 30;
          Color color2 = Color.FromArgb(red2, g2, blue2);
          colorBoxSurround3.BackColor = color2;
          break;
      }
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      switch (this.comboBox4.SelectedIndex)
      {
        case 0:
          this.colorBoxGlass4.BackColor = Color.Red;
          this.colorBoxGlass2.BackColor = Color.FromArgb((int) Color.Red.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxGlass3.BackColor = Color.FromArgb((int) this.colorBoxGlass2.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          this.colorBoxGlass1.BackColor = Color.FromArgb((int) this.colorBoxGlass3.BackColor.R - 50, (int) Color.Red.G, (int) Color.Red.B);
          break;
        case 1:
          this.colorBoxGlass4.BackColor = Color.FromArgb(0, (int) byte.MaxValue, 0);
          this.colorBoxGlass2.BackColor = Color.FromArgb((int) this.colorBoxGlass4.BackColor.R, (int) this.colorBoxGlass4.BackColor.G - 50, (int) this.colorBoxGlass4.BackColor.B);
          this.colorBoxGlass3.BackColor = Color.FromArgb((int) this.colorBoxGlass2.BackColor.R, (int) this.colorBoxGlass2.BackColor.G - 50, (int) this.colorBoxGlass2.BackColor.B);
          this.colorBoxGlass1.BackColor = Color.FromArgb((int) this.colorBoxGlass3.BackColor.R, (int) this.colorBoxGlass3.BackColor.G - 50, (int) this.colorBoxGlass3.BackColor.B);
          break;
        case 2:
          this.colorBoxGlass4.BackColor = Color.Blue;
          this.colorBoxGlass2.BackColor = Color.FromArgb((int) this.colorBoxGlass4.BackColor.R, (int) this.colorBoxGlass4.BackColor.G, (int) this.colorBoxGlass4.BackColor.B - 50);
          this.colorBoxGlass3.BackColor = Color.FromArgb((int) this.colorBoxGlass2.BackColor.R, (int) this.colorBoxGlass2.BackColor.G, (int) this.colorBoxGlass2.BackColor.B - 50);
          this.colorBoxGlass1.BackColor = Color.FromArgb((int) this.colorBoxGlass3.BackColor.R, (int) this.colorBoxGlass3.BackColor.G, (int) this.colorBoxGlass3.BackColor.B - 50);
          break;
        case 3:
          this.colorBoxGlass4.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0);
          this.colorBoxGlass2.BackColor = Color.FromArgb((int) this.colorBoxGlass4.BackColor.R - 50, (int) this.colorBoxGlass4.BackColor.G - 50, (int) this.colorBoxGlass4.BackColor.B);
          this.colorBoxGlass3.BackColor = Color.FromArgb((int) this.colorBoxGlass2.BackColor.R - 50, (int) this.colorBoxGlass2.BackColor.G - 50, (int) this.colorBoxGlass2.BackColor.B);
          this.colorBoxGlass1.BackColor = Color.FromArgb((int) this.colorBoxGlass3.BackColor.R - 50, (int) this.colorBoxGlass3.BackColor.G - 50, (int) this.colorBoxGlass3.BackColor.B);
          break;
        case 4:
          this.colorBoxGlass4.BackColor = Color.FromArgb(0, 0, 0);
          this.colorBoxGlass2.BackColor = Color.FromArgb((int) this.colorBoxGlass4.BackColor.R + 50, (int) this.colorBoxGlass4.BackColor.G + 50, (int) this.colorBoxGlass4.BackColor.B + 50);
          this.colorBoxGlass3.BackColor = Color.FromArgb((int) this.colorBoxGlass2.BackColor.R + 50, (int) this.colorBoxGlass2.BackColor.G + 50, (int) this.colorBoxGlass2.BackColor.B + 50);
          this.colorBoxGlass1.BackColor = Color.FromArgb((int) this.colorBoxGlass3.BackColor.R + 50, (int) this.colorBoxGlass3.BackColor.G + 50, (int) this.colorBoxGlass3.BackColor.B + 50);
          break;
        case 5:
          this.colorBoxGlass4.BackColor = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          this.colorBoxGlass2.BackColor = Color.FromArgb((int) this.colorBoxGlass4.BackColor.R - 50, (int) this.colorBoxGlass4.BackColor.G - 50, (int) this.colorBoxGlass4.BackColor.B - 50);
          this.colorBoxGlass3.BackColor = Color.FromArgb((int) this.colorBoxGlass2.BackColor.R - 50, (int) this.colorBoxGlass2.BackColor.G - 50, (int) this.colorBoxGlass2.BackColor.B - 50);
          this.colorBoxGlass1.BackColor = Color.FromArgb((int) this.colorBoxGlass3.BackColor.R - 50, (int) this.colorBoxGlass3.BackColor.G - 50, (int) this.colorBoxGlass3.BackColor.B - 50);
          break;
        case 6:
          this.colorBoxGlass4.BackColor = Color.FromArgb((int) byte.MaxValue, 128, 0);
          this.colorBoxGlass2.BackColor = Color.FromArgb((int) this.colorBoxGlass4.BackColor.R - 50, (int) this.colorBoxGlass4.BackColor.G, (int) this.colorBoxGlass4.BackColor.B);
          this.colorBoxGlass3.BackColor = Color.FromArgb((int) this.colorBoxGlass2.BackColor.R - 50, (int) this.colorBoxGlass2.BackColor.G, (int) this.colorBoxGlass2.BackColor.B);
          this.colorBoxGlass1.BackColor = Color.FromArgb((int) this.colorBoxGlass3.BackColor.R - 50, (int) this.colorBoxGlass3.BackColor.G - 50, (int) this.colorBoxGlass3.BackColor.B);
          break;
        case 7:
          this.colorBoxGlass4.BackColor = Color.FromArgb(128, 0, 128);
          this.colorBoxGlass2.BackColor = Color.FromArgb((int) this.colorBoxGlass4.BackColor.R + 50 - 30, (int) this.colorBoxGlass4.BackColor.G, (int) this.colorBoxGlass4.BackColor.B + 50 - 30);
          ColorBox colorBoxGlass3 = this.colorBoxGlass3;
          int red1 = (int) this.colorBoxGlass2.BackColor.R + 50 - 30;
          Color backColor = this.colorBoxGlass2.BackColor;
          int g1 = (int) backColor.G;
          backColor = this.colorBoxGlass2.BackColor;
          int blue1 = (int) backColor.B + 50 - 30;
          Color color1 = Color.FromArgb(red1, g1, blue1);
          colorBoxGlass3.BackColor = color1;
          ColorBox colorBoxGlass1 = this.colorBoxGlass1;
          backColor = this.colorBoxGlass3.BackColor;
          int red2 = (int) backColor.R + 50 - 30;
          backColor = this.colorBoxGlass3.BackColor;
          int g2 = (int) backColor.G;
          backColor = this.colorBoxGlass3.BackColor;
          int blue2 = (int) backColor.B + 50 - 30;
          Color color2 = Color.FromArgb(red2, g2, blue2);
          colorBoxGlass1.BackColor = color2;
          break;
      }
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      this.gradientBox1.Fill.NumberOfColors = 4;
      while (this.gradientSlider1.Values.Count < 4)
        this.gradientSlider1.Values.Insert(1, new GradientColorValue());
      switch (this.comboBox6.SelectedIndex)
      {
        case 0:
          this.gradientSlider1.Values[0].ColorValue = Color.FromArgb((int) byte.MaxValue, 238, 156, 156);
          this.gradientSlider1.Values[0].ColorPosition = 0.0f;
          this.gradientSlider1.Values[1].ColorPosition = 0.15f;
          this.gradientSlider1.Values[1].ColorValue = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0, 0);
          this.gradientSlider1.Values[2].ColorPosition = 0.7f;
          this.gradientSlider1.Values[2].ColorValue = Color.FromArgb((int) byte.MaxValue, 178, 34, 34);
          this.gradientSlider1.Values[3].ColorValue = Color.FromArgb((int) byte.MaxValue, 114, 19, 19);
          this.gradientSlider1.Values[3].ColorPosition = 1f;
          break;
        case 1:
          this.gradientSlider1.Values[0].ColorValue = Color.FromArgb((int) byte.MaxValue, 183, 238, 156);
          this.gradientSlider1.Values[0].ColorPosition = 0.0f;
          this.gradientSlider1.Values[1].ColorPosition = 0.15f;
          this.gradientSlider1.Values[1].ColorValue = Color.FromArgb((int) byte.MaxValue, 85, (int) byte.MaxValue, 0);
          this.gradientSlider1.Values[2].ColorPosition = 0.7f;
          this.gradientSlider1.Values[2].ColorValue = Color.FromArgb((int) byte.MaxValue, 82, 178, 34);
          this.gradientSlider1.Values[3].ColorValue = Color.FromArgb((int) byte.MaxValue, 51, 114, 19);
          this.gradientSlider1.Values[3].ColorPosition = 1f;
          break;
        case 2:
          this.gradientSlider1.Values[0].ColorValue = Color.FromArgb((int) byte.MaxValue, 156, 201, 238);
          this.gradientSlider1.Values[0].ColorPosition = 0.0f;
          this.gradientSlider1.Values[1].ColorPosition = 0.15f;
          this.gradientSlider1.Values[1].ColorValue = Color.FromArgb((int) byte.MaxValue, 0, 140, (int) byte.MaxValue);
          this.gradientSlider1.Values[2].ColorPosition = 0.7f;
          this.gradientSlider1.Values[2].ColorValue = Color.FromArgb((int) byte.MaxValue, 34, 113, 178);
          this.gradientSlider1.Values[3].ColorValue = Color.FromArgb((int) byte.MaxValue, 19, 71, 114);
          this.gradientSlider1.Values[3].ColorPosition = 1f;
          break;
        case 3:
          this.gradientSlider1.Values[0].ColorValue = Color.FromArgb((int) byte.MaxValue, 238, 224, 156);
          this.gradientSlider1.Values[0].ColorPosition = 0.0f;
          this.gradientSlider1.Values[1].ColorPosition = 0.15f;
          this.gradientSlider1.Values[1].ColorValue = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 213, 0);
          this.gradientSlider1.Values[2].ColorPosition = 0.7f;
          this.gradientSlider1.Values[2].ColorValue = Color.FromArgb((int) byte.MaxValue, 178, 154, 34);
          this.gradientSlider1.Values[3].ColorValue = Color.FromArgb((int) byte.MaxValue, 114, 98, 19);
          this.gradientSlider1.Values[3].ColorPosition = 1f;
          break;
        case 4:
          this.gradientSlider1.Values[0].ColorValue = Color.FromArgb((int) byte.MaxValue, 0, 0, 0);
          this.gradientSlider1.Values[0].ColorPosition = 0.0f;
          this.gradientSlider1.Values[1].ColorPosition = 0.39f;
          this.gradientSlider1.Values[1].ColorValue = Color.FromArgb((int) byte.MaxValue, 59, 59, 59);
          this.gradientSlider1.Values[2].ColorPosition = 0.6f;
          this.gradientSlider1.Values[2].ColorValue = Color.FromArgb((int) byte.MaxValue, 44, 44, 44);
          this.gradientSlider1.Values[3].ColorValue = Color.FromArgb((int) byte.MaxValue, 0, 0, 0);
          this.gradientSlider1.Values[3].ColorPosition = 1f;
          break;
        case 5:
          this.gradientSlider1.Values[0].ColorValue = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          this.gradientSlider1.Values[0].ColorPosition = 0.0f;
          this.gradientSlider1.Values[1].ColorPosition = 0.3f;
          this.gradientSlider1.Values[1].ColorValue = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
          this.gradientSlider1.Values[2].ColorPosition = 0.5f;
          this.gradientSlider1.Values[2].ColorValue = Color.FromArgb((int) byte.MaxValue, 245, 245, 245);
          this.gradientSlider1.Values[3].ColorValue = Color.FromArgb((int) byte.MaxValue, 202, 202, 202);
          this.gradientSlider1.Values[3].ColorPosition = 1f;
          break;
        case 6:
          this.gradientSlider1.Values[0].ColorValue = Color.FromArgb((int) byte.MaxValue, 238, 204, 156);
          this.gradientSlider1.Values[0].ColorPosition = 0.0f;
          this.gradientSlider1.Values[1].ColorPosition = 0.15f;
          this.gradientSlider1.Values[1].ColorValue = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 149, 0);
          this.gradientSlider1.Values[2].ColorPosition = 0.7f;
          this.gradientSlider1.Values[2].ColorValue = Color.FromArgb((int) byte.MaxValue, 178, 118, 34);
          this.gradientSlider1.Values[3].ColorValue = Color.FromArgb((int) byte.MaxValue, 114, 74, 19);
          this.gradientSlider1.Values[3].ColorPosition = 1f;
          break;
        case 7:
          this.gradientSlider1.Values[0].ColorValue = Color.FromArgb((int) byte.MaxValue, 204, 156, 238);
          this.gradientSlider1.Values[0].ColorPosition = 0.0f;
          this.gradientSlider1.Values[1].ColorPosition = 0.15f;
          this.gradientSlider1.Values[1].ColorValue = Color.FromArgb((int) byte.MaxValue, 149, 0, (int) byte.MaxValue);
          this.gradientSlider1.Values[2].ColorPosition = 0.7f;
          this.gradientSlider1.Values[2].ColorValue = Color.FromArgb((int) byte.MaxValue, 118, 34, 178);
          this.gradientSlider1.Values[3].ColorValue = Color.FromArgb((int) byte.MaxValue, 74, 19, 114);
          this.gradientSlider1.Values[3].ColorPosition = 1f;
          break;
      }
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void numericRadial1_ValueChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      this.gradientBox1.Fill.GradientPercentage = (float) this.numericRadial1.Value;
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    private void numericRadial2_ValueChanged(object sender, EventArgs e)
    {
      if (this.IsLoading)
        return;
      this.gradientBox1.Fill.GradientPercentage = (float) this.numericRadial2.Value;
      this.tabControl1_SelectedIndexChanged((object) this, EventArgs.Empty);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.groupBox1 = new GroupBox();
      this.gradientBox1 = new GradientBox();
      this.tabControl1 = new TabControl();
      this.tabPage9 = new TabPage();
      this.tabPage1 = new TabPage();
      this.label18 = new System.Windows.Forms.Label();
      this.comboBox7 = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.colorBoxSolid = new ColorBox();
      this.tabPage2 = new TabPage();
      this.gradientAngleControl1 = new GradientAngleControl();
      this.label20 = new System.Windows.Forms.Label();
      this.comboBox6 = new System.Windows.Forms.ComboBox();
      this.numericLinearAngle = new NumericUpDown();
      this.label9 = new System.Windows.Forms.Label();
      this.gradientSlider1 = new GradientSlider();
      this.tabPage3 = new TabPage();
      this.numericRadial2 = new NumericUpDown();
      this.label13 = new System.Windows.Forms.Label();
      this.numericRadial1 = new NumericUpDown();
      this.label16 = new System.Windows.Forms.Label();
      this.label26 = new System.Windows.Forms.Label();
      this.comboBox5 = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.colorBoxSurround3 = new ColorBox();
      this.colorBoxSurround2 = new ColorBox();
      this.colorBoxSurround1 = new ColorBox();
      this.colorBoxRadialStart = new ColorBox();
      this.tabPage5 = new TabPage();
      this.label11 = new System.Windows.Forms.Label();
      this.comboBox4 = new System.Windows.Forms.ComboBox();
      this.numericGlassPercentage = new NumericUpDown();
      this.label22 = new System.Windows.Forms.Label();
      this.label19 = new System.Windows.Forms.Label();
      this.label21 = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.label17 = new System.Windows.Forms.Label();
      this.colorBoxGlass4 = new ColorBox();
      this.colorBoxGlass3 = new ColorBox();
      this.colorBoxGlass2 = new ColorBox();
      this.colorBoxGlass1 = new ColorBox();
      this.tabPage6 = new TabPage();
      this.label8 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.numericOfficePercentage2 = new NumericUpDown();
      this.label32 = new System.Windows.Forms.Label();
      this.numericOfficePercentage1 = new NumericUpDown();
      this.label23 = new System.Windows.Forms.Label();
      this.label25 = new System.Windows.Forms.Label();
      this.label27 = new System.Windows.Forms.Label();
      this.label29 = new System.Windows.Forms.Label();
      this.label31 = new System.Windows.Forms.Label();
      this.colorBoxOfficeGlass4 = new ColorBox();
      this.colorBoxOfficeGlass3 = new ColorBox();
      this.colorBoxOfficeGlass2 = new ColorBox();
      this.colorBoxOfficeGlass1 = new ColorBox();
      this.tabPage8 = new TabPage();
      this.label24 = new System.Windows.Forms.Label();
      this.comboBox8 = new System.Windows.Forms.ComboBox();
      this.numericOfficeRectPercentage2 = new NumericUpDown();
      this.label28 = new System.Windows.Forms.Label();
      this.numericOfficeRectPercentage1 = new NumericUpDown();
      this.label30 = new System.Windows.Forms.Label();
      this.label35 = new System.Windows.Forms.Label();
      this.label37 = new System.Windows.Forms.Label();
      this.label39 = new System.Windows.Forms.Label();
      this.label41 = new System.Windows.Forms.Label();
      this.colorBoxOfficeGlassRect4 = new ColorBox();
      this.colorBoxOfficeGlassRect3 = new ColorBox();
      this.colorBoxOfficeGlassRect2 = new ColorBox();
      this.colorBoxOfficeGlassRect1 = new ColorBox();
      this.tabPage4 = new TabPage();
      this.label7 = new System.Windows.Forms.Label();
      this.comboBox2 = new System.Windows.Forms.ComboBox();
      this.numericGelPercentage = new NumericUpDown();
      this.label14 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.colorBoxGelColor2 = new ColorBox();
      this.colorBoxGelColor1 = new ColorBox();
      this.tabPage7 = new TabPage();
      this.label6 = new System.Windows.Forms.Label();
      this.comboBox3 = new System.Windows.Forms.ComboBox();
      this.numericVistaPercentage2 = new NumericUpDown();
      this.label33 = new System.Windows.Forms.Label();
      this.numericVistaPercentage1 = new NumericUpDown();
      this.label34 = new System.Windows.Forms.Label();
      this.label36 = new System.Windows.Forms.Label();
      this.label38 = new System.Windows.Forms.Label();
      this.label40 = new System.Windows.Forms.Label();
      this.label42 = new System.Windows.Forms.Label();
      this.colorBoxVista4 = new ColorBox();
      this.colorBoxVista3 = new ColorBox();
      this.colorBoxVista2 = new ColorBox();
      this.colorBoxVista1 = new ColorBox();
      this.groupBox1.SuspendLayout();
      this.gradientBox1.BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.numericLinearAngle.BeginInit();
      this.tabPage3.SuspendLayout();
      this.numericRadial2.BeginInit();
      this.numericRadial1.BeginInit();
      this.tabPage5.SuspendLayout();
      this.numericGlassPercentage.BeginInit();
      this.tabPage6.SuspendLayout();
      this.numericOfficePercentage2.BeginInit();
      this.numericOfficePercentage1.BeginInit();
      this.tabPage8.SuspendLayout();
      this.numericOfficeRectPercentage2.BeginInit();
      this.numericOfficeRectPercentage1.BeginInit();
      this.tabPage4.SuspendLayout();
      this.numericGelPercentage.BeginInit();
      this.tabPage7.SuspendLayout();
      this.numericVistaPercentage2.BeginInit();
      this.numericVistaPercentage1.BeginInit();
      this.SuspendLayout();
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.gradientBox1);
      this.groupBox1.Location = new Point(12, 138);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(620, 234);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Preview:";
      this.gradientBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.gradientBox1.FillAngle = 90f;
      this.gradientBox1.FillStyle = GradientStyles.Linear;
      this.gradientBox1.Location = new Point(9, 19);
      this.gradientBox1.Name = "gradientBox1";
      this.gradientBox1.Size = new Size(600, 206);
      this.gradientBox1.TabIndex = 0;
      this.gradientBox1.Text = "gradientBox1";
      this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.tabControl1.Controls.Add((Control) this.tabPage9);
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Controls.Add((Control) this.tabPage3);
      this.tabControl1.Controls.Add((Control) this.tabPage5);
      this.tabControl1.Controls.Add((Control) this.tabPage6);
      this.tabControl1.Controls.Add((Control) this.tabPage8);
      this.tabControl1.Controls.Add((Control) this.tabPage4);
      this.tabControl1.Controls.Add((Control) this.tabPage7);
      this.tabControl1.Location = new Point(12, 12);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(620, 125);
      this.tabControl1.TabIndex = 3;
      this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
      this.tabPage9.Location = new Point(4, 22);
      this.tabPage9.Name = "tabPage9";
      this.tabPage9.Size = new Size(612, 99);
      this.tabPage9.TabIndex = 8;
      this.tabPage9.Text = "Transparent";
      this.tabPage9.UseVisualStyleBackColor = true;
      this.tabPage1.Controls.Add((Control) this.label18);
      this.tabPage1.Controls.Add((Control) this.comboBox7);
      this.tabPage1.Controls.Add((Control) this.label1);
      this.tabPage1.Controls.Add((Control) this.colorBoxSolid);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(612, 99);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Solid";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.label18.AutoSize = true;
      this.label18.ForeColor = Color.Black;
      this.label18.Location = new Point(244, 10);
      this.label18.Name = "label18";
      this.label18.Size = new Size(68, 13);
      this.label18.TabIndex = 45;
      this.label18.Text = "QuickTheme";
      this.comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox7.FormattingEnabled = true;
      this.comboBox7.Items.AddRange(new object[8]
      {
        (object) "Red",
        (object) "Green",
        (object) "Blue",
        (object) "Yellow",
        (object) "Black",
        (object) "White",
        (object) "Orange",
        (object) "Purple"
      });
      this.comboBox7.Location = new Point(247, 26);
      this.comboBox7.Name = "comboBox7";
      this.comboBox7.Size = new Size(71, 21);
      this.comboBox7.TabIndex = 32;
      this.comboBox7.SelectedIndexChanged += new EventHandler(this.comboBox7_SelectedIndexChanged);
      this.label1.AutoSize = true;
      this.label1.ForeColor = Color.Black;
      this.label1.Location = new Point(6, 11);
      this.label1.Name = "label1";
      this.label1.Size = new Size(105, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Choose a solid color:";
      this.colorBoxSolid.BackColor = Color.Turquoise;
      this.colorBoxSolid.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxSolid.Location = new Point(126, 9);
      this.colorBoxSolid.Name = "colorBoxSolid";
      this.colorBoxSolid.Size = new Size(41, 23);
      this.colorBoxSolid.TabIndex = 1;
      this.tabPage2.Controls.Add((Control) this.gradientAngleControl1);
      this.tabPage2.Controls.Add((Control) this.label20);
      this.tabPage2.Controls.Add((Control) this.comboBox6);
      this.tabPage2.Controls.Add((Control) this.numericLinearAngle);
      this.tabPage2.Controls.Add((Control) this.label9);
      this.tabPage2.Controls.Add((Control) this.gradientSlider1);
      this.tabPage2.ForeColor = Color.RoyalBlue;
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(612, 99);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Linear";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.gradientAngleControl1.ForeColor = Color.Black;
      this.gradientAngleControl1.GradientAngle = 0.0;
      this.gradientAngleControl1.Location = new Point(151, 8);
      this.gradientAngleControl1.Name = "gradientAngleControl1";
      this.gradientAngleControl1.Size = new Size(27, 20);
      this.gradientAngleControl1.TabIndex = 50;
      this.label20.AutoSize = true;
      this.label20.ForeColor = Color.Black;
      this.label20.Location = new Point(244, 10);
      this.label20.Name = "label20";
      this.label20.Size = new Size(68, 13);
      this.label20.TabIndex = 49;
      this.label20.Text = "QuickTheme";
      this.comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox6.FormattingEnabled = true;
      this.comboBox6.Items.AddRange(new object[8]
      {
        (object) "Red",
        (object) "Green",
        (object) "Blue",
        (object) "Yellow",
        (object) "Black",
        (object) "White",
        (object) "Orange",
        (object) "Purple"
      });
      this.comboBox6.Location = new Point(247, 26);
      this.comboBox6.Name = "comboBox6";
      this.comboBox6.Size = new Size(71, 21);
      this.comboBox6.TabIndex = 47;
      this.comboBox6.SelectedIndexChanged += new EventHandler(this.comboBox6_SelectedIndexChanged);
      this.numericLinearAngle.Location = new Point(92, 8);
      this.numericLinearAngle.Maximum = new Decimal(new int[4]
      {
        360,
        0,
        0,
        0
      });
      this.numericLinearAngle.Name = "numericLinearAngle";
      this.numericLinearAngle.Size = new Size(53, 20);
      this.numericLinearAngle.TabIndex = 12;
      this.numericLinearAngle.ValueChanged += new EventHandler(this.gradientAngle_ValueChanged);
      this.label9.AutoSize = true;
      this.label9.ForeColor = Color.Black;
      this.label9.Location = new Point(6, 11);
      this.label9.Name = "label9";
      this.label9.Size = new Size(80, 13);
      this.label9.TabIndex = 11;
      this.label9.Text = "Gradient Angle:";
      this.gradientSlider1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.gradientSlider1.Location = new Point(5, 54);
      this.gradientSlider1.Name = "gradientSlider1";
      this.gradientSlider1.Size = new Size(564, 42);
      this.gradientSlider1.TabIndex = 0;
      this.tabPage3.Controls.Add((Control) this.numericRadial2);
      this.tabPage3.Controls.Add((Control) this.label13);
      this.tabPage3.Controls.Add((Control) this.numericRadial1);
      this.tabPage3.Controls.Add((Control) this.label16);
      this.tabPage3.Controls.Add((Control) this.label26);
      this.tabPage3.Controls.Add((Control) this.comboBox5);
      this.tabPage3.Controls.Add((Control) this.label5);
      this.tabPage3.Controls.Add((Control) this.label4);
      this.tabPage3.Controls.Add((Control) this.label2);
      this.tabPage3.Controls.Add((Control) this.label3);
      this.tabPage3.Controls.Add((Control) this.colorBoxSurround3);
      this.tabPage3.Controls.Add((Control) this.colorBoxSurround2);
      this.tabPage3.Controls.Add((Control) this.colorBoxSurround1);
      this.tabPage3.Controls.Add((Control) this.colorBoxRadialStart);
      this.tabPage3.ForeColor = Color.Black;
      this.tabPage3.Location = new Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new Padding(3);
      this.tabPage3.Size = new Size(612, 99);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Radial";
      this.tabPage3.UseVisualStyleBackColor = true;
      this.numericRadial2.DecimalPlaces = 2;
      this.numericRadial2.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericRadial2.Location = new Point(86, 67);
      this.numericRadial2.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericRadial2.Name = "numericRadial2";
      this.numericRadial2.Size = new Size(41, 20);
      this.numericRadial2.TabIndex = 48;
      this.numericRadial2.ValueChanged += new EventHandler(this.numericRadial2_ValueChanged);
      this.label13.AutoSize = true;
      this.label13.ForeColor = Color.Black;
      this.label13.Location = new Point(6, 69);
      this.label13.Name = "label13";
      this.label13.Size = new Size(74, 13);
      this.label13.TabIndex = 47;
      this.label13.Text = "Percentage 2:";
      this.numericRadial1.DecimalPlaces = 2;
      this.numericRadial1.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericRadial1.Location = new Point(87, 37);
      this.numericRadial1.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericRadial1.Name = "numericRadial1";
      this.numericRadial1.Size = new Size(41, 20);
      this.numericRadial1.TabIndex = 29;
      this.numericRadial1.ValueChanged += new EventHandler(this.numericRadial1_ValueChanged);
      this.label16.AutoSize = true;
      this.label16.ForeColor = Color.Black;
      this.label16.Location = new Point(244, 10);
      this.label16.Name = "label16";
      this.label16.Size = new Size(68, 13);
      this.label16.TabIndex = 46;
      this.label16.Text = "QuickTheme";
      this.label26.AutoSize = true;
      this.label26.ForeColor = Color.Black;
      this.label26.Location = new Point(6, 39);
      this.label26.Name = "label26";
      this.label26.Size = new Size(74, 13);
      this.label26.TabIndex = 28;
      this.label26.Text = "Percentage 1:";
      this.comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox5.FormattingEnabled = true;
      this.comboBox5.Items.AddRange(new object[8]
      {
        (object) "Red",
        (object) "Green",
        (object) "Blue",
        (object) "Yellow",
        (object) "Black",
        (object) "White",
        (object) "Orange",
        (object) "Purple"
      });
      this.comboBox5.Location = new Point(247, 26);
      this.comboBox5.Name = "comboBox5";
      this.comboBox5.Size = new Size(71, 21);
      this.comboBox5.TabIndex = 32;
      this.comboBox5.SelectedIndexChanged += new EventHandler(this.comboBox5_SelectedIndexChanged);
      this.label5.AutoSize = true;
      this.label5.ForeColor = Color.Black;
      this.label5.Location = new Point(134, 63);
      this.label5.Name = "label5";
      this.label5.Size = new Size(43, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Color 4:";
      this.label4.AutoSize = true;
      this.label4.ForeColor = Color.Black;
      this.label4.Location = new Point(134, 37);
      this.label4.Name = "label4";
      this.label4.Size = new Size(43, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "Color 3:";
      this.label2.AutoSize = true;
      this.label2.ForeColor = Color.Black;
      this.label2.Location = new Point(134, 10);
      this.label2.Name = "label2";
      this.label2.Size = new Size(43, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Color 2:";
      this.label3.AutoSize = true;
      this.label3.ForeColor = Color.Black;
      this.label3.Location = new Point(6, 11);
      this.label3.Name = "label3";
      this.label3.Size = new Size(68, 13);
      this.label3.TabIndex = 0;
      this.label3.Text = "Center Color:";
      this.colorBoxSurround3.BackColor = Color.Turquoise;
      this.colorBoxSurround3.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxSurround3.Location = new Point(183, 59);
      this.colorBoxSurround3.Name = "colorBoxSurround3";
      this.colorBoxSurround3.Size = new Size(41, 23);
      this.colorBoxSurround3.TabIndex = 7;
      this.colorBoxSurround2.BackColor = Color.Turquoise;
      this.colorBoxSurround2.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxSurround2.Location = new Point(183, 32);
      this.colorBoxSurround2.Name = "colorBoxSurround2";
      this.colorBoxSurround2.Size = new Size(41, 23);
      this.colorBoxSurround2.TabIndex = 6;
      this.colorBoxSurround1.BackColor = Color.Turquoise;
      this.colorBoxSurround1.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxSurround1.Location = new Point(183, 5);
      this.colorBoxSurround1.Name = "colorBoxSurround1";
      this.colorBoxSurround1.Size = new Size(41, 23);
      this.colorBoxSurround1.TabIndex = 5;
      this.colorBoxRadialStart.BackColor = Color.Turquoise;
      this.colorBoxRadialStart.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxRadialStart.Location = new Point(87, 5);
      this.colorBoxRadialStart.Name = "colorBoxRadialStart";
      this.colorBoxRadialStart.Size = new Size(41, 23);
      this.colorBoxRadialStart.TabIndex = 4;
      this.tabPage5.Controls.Add((Control) this.label11);
      this.tabPage5.Controls.Add((Control) this.comboBox4);
      this.tabPage5.Controls.Add((Control) this.numericGlassPercentage);
      this.tabPage5.Controls.Add((Control) this.label22);
      this.tabPage5.Controls.Add((Control) this.label19);
      this.tabPage5.Controls.Add((Control) this.label21);
      this.tabPage5.Controls.Add((Control) this.label15);
      this.tabPage5.Controls.Add((Control) this.label17);
      this.tabPage5.Controls.Add((Control) this.colorBoxGlass4);
      this.tabPage5.Controls.Add((Control) this.colorBoxGlass3);
      this.tabPage5.Controls.Add((Control) this.colorBoxGlass2);
      this.tabPage5.Controls.Add((Control) this.colorBoxGlass1);
      this.tabPage5.ForeColor = Color.Black;
      this.tabPage5.Location = new Point(4, 22);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new Padding(3);
      this.tabPage5.Size = new Size(612, 99);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "Glass";
      this.tabPage5.UseVisualStyleBackColor = true;
      this.label11.AutoSize = true;
      this.label11.ForeColor = Color.Black;
      this.label11.Location = new Point(244, 10);
      this.label11.Name = "label11";
      this.label11.Size = new Size(68, 13);
      this.label11.TabIndex = 45;
      this.label11.Text = "QuickTheme";
      this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox4.FormattingEnabled = true;
      this.comboBox4.Items.AddRange(new object[8]
      {
        (object) "Red",
        (object) "Green",
        (object) "Blue",
        (object) "Yellow",
        (object) "Black",
        (object) "White",
        (object) "Orange",
        (object) "Purple"
      });
      this.comboBox4.Location = new Point(247, 26);
      this.comboBox4.Name = "comboBox4";
      this.comboBox4.Size = new Size(71, 21);
      this.comboBox4.TabIndex = 32;
      this.comboBox4.SelectedIndexChanged += new EventHandler(this.comboBox4_SelectedIndexChanged);
      this.numericGlassPercentage.DecimalPlaces = 2;
      this.numericGlassPercentage.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericGlassPercentage.Location = new Point(77, 67);
      this.numericGlassPercentage.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericGlassPercentage.Name = "numericGlassPercentage";
      this.numericGlassPercentage.Size = new Size(53, 20);
      this.numericGlassPercentage.TabIndex = 18;
      this.numericGlassPercentage.ValueChanged += new EventHandler(this.gradientPercentage_ValueChanged);
      this.label22.AutoSize = true;
      this.label22.ForeColor = Color.Black;
      this.label22.Location = new Point(6, 69);
      this.label22.Name = "label22";
      this.label22.Size = new Size(65, 13);
      this.label22.TabIndex = 17;
      this.label22.Text = "Percentage:";
      this.label19.AutoSize = true;
      this.label19.ForeColor = Color.Black;
      this.label19.Location = new Point(102, 39);
      this.label19.Name = "label19";
      this.label19.Size = new Size(43, 13);
      this.label19.TabIndex = 15;
      this.label19.Text = "Color 4:";
      this.label21.AutoSize = true;
      this.label21.ForeColor = Color.Black;
      this.label21.Location = new Point(102, 11);
      this.label21.Name = "label21";
      this.label21.Size = new Size(43, 13);
      this.label21.TabIndex = 13;
      this.label21.Text = "Color 3:";
      this.label15.AutoSize = true;
      this.label15.ForeColor = Color.Black;
      this.label15.Location = new Point(6, 39);
      this.label15.Name = "label15";
      this.label15.Size = new Size(43, 13);
      this.label15.TabIndex = 11;
      this.label15.Text = "Color 2:";
      this.label17.AutoSize = true;
      this.label17.ForeColor = Color.Black;
      this.label17.Location = new Point(6, 11);
      this.label17.Name = "label17";
      this.label17.Size = new Size(43, 13);
      this.label17.TabIndex = 9;
      this.label17.Text = "Color 1:";
      this.colorBoxGlass4.BackColor = Color.Turquoise;
      this.colorBoxGlass4.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxGlass4.Location = new Point(151, 37);
      this.colorBoxGlass4.Name = "colorBoxGlass4";
      this.colorBoxGlass4.Size = new Size(41, 23);
      this.colorBoxGlass4.TabIndex = 16;
      this.colorBoxGlass3.BackColor = Color.Turquoise;
      this.colorBoxGlass3.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxGlass3.Location = new Point(151, 9);
      this.colorBoxGlass3.Name = "colorBoxGlass3";
      this.colorBoxGlass3.Size = new Size(41, 23);
      this.colorBoxGlass3.TabIndex = 14;
      this.colorBoxGlass2.BackColor = Color.Turquoise;
      this.colorBoxGlass2.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxGlass2.Location = new Point(55, 37);
      this.colorBoxGlass2.Name = "colorBoxGlass2";
      this.colorBoxGlass2.Size = new Size(41, 23);
      this.colorBoxGlass2.TabIndex = 12;
      this.colorBoxGlass1.BackColor = Color.Turquoise;
      this.colorBoxGlass1.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxGlass1.Location = new Point(55, 9);
      this.colorBoxGlass1.Name = "colorBoxGlass1";
      this.colorBoxGlass1.Size = new Size(41, 23);
      this.colorBoxGlass1.TabIndex = 10;
      this.tabPage6.Controls.Add((Control) this.label8);
      this.tabPage6.Controls.Add((Control) this.comboBox1);
      this.tabPage6.Controls.Add((Control) this.numericOfficePercentage2);
      this.tabPage6.Controls.Add((Control) this.label32);
      this.tabPage6.Controls.Add((Control) this.numericOfficePercentage1);
      this.tabPage6.Controls.Add((Control) this.label23);
      this.tabPage6.Controls.Add((Control) this.label25);
      this.tabPage6.Controls.Add((Control) this.label27);
      this.tabPage6.Controls.Add((Control) this.label29);
      this.tabPage6.Controls.Add((Control) this.label31);
      this.tabPage6.Controls.Add((Control) this.colorBoxOfficeGlass4);
      this.tabPage6.Controls.Add((Control) this.colorBoxOfficeGlass3);
      this.tabPage6.Controls.Add((Control) this.colorBoxOfficeGlass2);
      this.tabPage6.Controls.Add((Control) this.colorBoxOfficeGlass1);
      this.tabPage6.ForeColor = Color.Black;
      this.tabPage6.Location = new Point(4, 22);
      this.tabPage6.Name = "tabPage6";
      this.tabPage6.Padding = new Padding(3);
      this.tabPage6.Size = new Size(612, 99);
      this.tabPage6.TabIndex = 5;
      this.tabPage6.Text = "Office Glass";
      this.tabPage6.UseVisualStyleBackColor = true;
      this.label8.AutoSize = true;
      this.label8.ForeColor = Color.Black;
      this.label8.Location = new Point(244, 10);
      this.label8.Name = "label8";
      this.label8.Size = new Size(68, 13);
      this.label8.TabIndex = 45;
      this.label8.Text = "QuickTheme";
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[8]
      {
        (object) "Red",
        (object) "Green",
        (object) "Blue",
        (object) "Yellow",
        (object) "Black",
        (object) "White",
        (object) "Orange",
        (object) "Purple"
      });
      this.comboBox1.Location = new Point(247, 26);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(71, 21);
      this.comboBox1.TabIndex = 31;
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      this.numericOfficePercentage2.DecimalPlaces = 2;
      this.numericOfficePercentage2.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericOfficePercentage2.Location = new Point(234, 68);
      this.numericOfficePercentage2.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericOfficePercentage2.Name = "numericOfficePercentage2";
      this.numericOfficePercentage2.Size = new Size(53, 20);
      this.numericOfficePercentage2.TabIndex = 30;
      this.numericOfficePercentage2.ValueChanged += new EventHandler(this.gradientPercentage2_ValueChanged);
      this.label32.AutoSize = true;
      this.label32.ForeColor = Color.Black;
      this.label32.Location = new Point(154, 70);
      this.label32.Name = "label32";
      this.label32.Size = new Size(74, 13);
      this.label32.TabIndex = 29;
      this.label32.Text = "Percentage 2:";
      this.numericOfficePercentage1.DecimalPlaces = 2;
      this.numericOfficePercentage1.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericOfficePercentage1.Location = new Point(86, 68);
      this.numericOfficePercentage1.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericOfficePercentage1.Name = "numericOfficePercentage1";
      this.numericOfficePercentage1.Size = new Size(53, 20);
      this.numericOfficePercentage1.TabIndex = 28;
      this.numericOfficePercentage1.ValueChanged += new EventHandler(this.gradientPercentage_ValueChanged);
      this.label23.AutoSize = true;
      this.label23.ForeColor = Color.Black;
      this.label23.Location = new Point(6, 69);
      this.label23.Name = "label23";
      this.label23.Size = new Size(74, 13);
      this.label23.TabIndex = 27;
      this.label23.Text = "Percentage 1:";
      this.label25.AutoSize = true;
      this.label25.ForeColor = Color.Black;
      this.label25.Location = new Point(102, 39);
      this.label25.Name = "label25";
      this.label25.Size = new Size(43, 13);
      this.label25.TabIndex = 25;
      this.label25.Text = "Color 4:";
      this.label27.AutoSize = true;
      this.label27.ForeColor = Color.Black;
      this.label27.Location = new Point(102, 11);
      this.label27.Name = "label27";
      this.label27.Size = new Size(43, 13);
      this.label27.TabIndex = 23;
      this.label27.Text = "Color 3:";
      this.label29.AutoSize = true;
      this.label29.ForeColor = Color.Black;
      this.label29.Location = new Point(6, 39);
      this.label29.Name = "label29";
      this.label29.Size = new Size(43, 13);
      this.label29.TabIndex = 21;
      this.label29.Text = "Color 2:";
      this.label31.AutoSize = true;
      this.label31.ForeColor = Color.Black;
      this.label31.Location = new Point(6, 11);
      this.label31.Name = "label31";
      this.label31.Size = new Size(43, 13);
      this.label31.TabIndex = 19;
      this.label31.Text = "Color 1:";
      this.colorBoxOfficeGlass4.BackColor = Color.Turquoise;
      this.colorBoxOfficeGlass4.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxOfficeGlass4.Location = new Point(151, 37);
      this.colorBoxOfficeGlass4.Name = "colorBoxOfficeGlass4";
      this.colorBoxOfficeGlass4.Size = new Size(41, 23);
      this.colorBoxOfficeGlass4.TabIndex = 26;
      this.colorBoxOfficeGlass3.BackColor = Color.Turquoise;
      this.colorBoxOfficeGlass3.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxOfficeGlass3.Location = new Point(151, 9);
      this.colorBoxOfficeGlass3.Name = "colorBoxOfficeGlass3";
      this.colorBoxOfficeGlass3.Size = new Size(41, 23);
      this.colorBoxOfficeGlass3.TabIndex = 24;
      this.colorBoxOfficeGlass2.BackColor = Color.Turquoise;
      this.colorBoxOfficeGlass2.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxOfficeGlass2.Location = new Point(55, 37);
      this.colorBoxOfficeGlass2.Name = "colorBoxOfficeGlass2";
      this.colorBoxOfficeGlass2.Size = new Size(41, 23);
      this.colorBoxOfficeGlass2.TabIndex = 22;
      this.colorBoxOfficeGlass1.BackColor = Color.Turquoise;
      this.colorBoxOfficeGlass1.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxOfficeGlass1.Location = new Point(55, 9);
      this.colorBoxOfficeGlass1.Name = "colorBoxOfficeGlass1";
      this.colorBoxOfficeGlass1.Size = new Size(41, 23);
      this.colorBoxOfficeGlass1.TabIndex = 20;
      this.tabPage8.Controls.Add((Control) this.label24);
      this.tabPage8.Controls.Add((Control) this.comboBox8);
      this.tabPage8.Controls.Add((Control) this.numericOfficeRectPercentage2);
      this.tabPage8.Controls.Add((Control) this.label28);
      this.tabPage8.Controls.Add((Control) this.numericOfficeRectPercentage1);
      this.tabPage8.Controls.Add((Control) this.label30);
      this.tabPage8.Controls.Add((Control) this.label35);
      this.tabPage8.Controls.Add((Control) this.label37);
      this.tabPage8.Controls.Add((Control) this.label39);
      this.tabPage8.Controls.Add((Control) this.label41);
      this.tabPage8.Controls.Add((Control) this.colorBoxOfficeGlassRect4);
      this.tabPage8.Controls.Add((Control) this.colorBoxOfficeGlassRect3);
      this.tabPage8.Controls.Add((Control) this.colorBoxOfficeGlassRect2);
      this.tabPage8.Controls.Add((Control) this.colorBoxOfficeGlassRect1);
      this.tabPage8.ForeColor = Color.Black;
      this.tabPage8.Location = new Point(4, 22);
      this.tabPage8.Name = "tabPage8";
      this.tabPage8.Padding = new Padding(3);
      this.tabPage8.Size = new Size(612, 99);
      this.tabPage8.TabIndex = 7;
      this.tabPage8.Text = "Office Glass Rect";
      this.tabPage8.UseVisualStyleBackColor = true;
      this.label24.AutoSize = true;
      this.label24.ForeColor = Color.Black;
      this.label24.Location = new Point(244, 10);
      this.label24.Name = "label24";
      this.label24.Size = new Size(68, 13);
      this.label24.TabIndex = 59;
      this.label24.Text = "QuickTheme";
      this.comboBox8.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox8.FormattingEnabled = true;
      this.comboBox8.Items.AddRange(new object[8]
      {
        (object) "Red",
        (object) "Green",
        (object) "Blue",
        (object) "Yellow",
        (object) "Black",
        (object) "White",
        (object) "Orange",
        (object) "Purple"
      });
      this.comboBox8.Location = new Point(247, 26);
      this.comboBox8.Name = "comboBox8";
      this.comboBox8.Size = new Size(71, 21);
      this.comboBox8.TabIndex = 58;
      this.comboBox8.SelectedIndexChanged += new EventHandler(this.comboBox8_SelectedIndexChanged);
      this.numericOfficeRectPercentage2.DecimalPlaces = 2;
      this.numericOfficeRectPercentage2.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericOfficeRectPercentage2.Location = new Point(234, 68);
      this.numericOfficeRectPercentage2.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericOfficeRectPercentage2.Name = "numericOfficeRectPercentage2";
      this.numericOfficeRectPercentage2.Size = new Size(53, 20);
      this.numericOfficeRectPercentage2.TabIndex = 57;
      this.numericOfficeRectPercentage2.ValueChanged += new EventHandler(this.gradientPercentage2_ValueChanged);
      this.label28.AutoSize = true;
      this.label28.ForeColor = Color.Black;
      this.label28.Location = new Point(154, 70);
      this.label28.Name = "label28";
      this.label28.Size = new Size(74, 13);
      this.label28.TabIndex = 56;
      this.label28.Text = "Percentage 2:";
      this.numericOfficeRectPercentage1.DecimalPlaces = 2;
      this.numericOfficeRectPercentage1.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericOfficeRectPercentage1.Location = new Point(86, 68);
      this.numericOfficeRectPercentage1.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericOfficeRectPercentage1.Name = "numericOfficeRectPercentage1";
      this.numericOfficeRectPercentage1.Size = new Size(53, 20);
      this.numericOfficeRectPercentage1.TabIndex = 55;
      this.numericOfficeRectPercentage1.ValueChanged += new EventHandler(this.gradientPercentage_ValueChanged);
      this.label30.AutoSize = true;
      this.label30.ForeColor = Color.Black;
      this.label30.Location = new Point(6, 69);
      this.label30.Name = "label30";
      this.label30.Size = new Size(74, 13);
      this.label30.TabIndex = 54;
      this.label30.Text = "Percentage 1:";
      this.label35.AutoSize = true;
      this.label35.ForeColor = Color.Black;
      this.label35.Location = new Point(102, 39);
      this.label35.Name = "label35";
      this.label35.Size = new Size(43, 13);
      this.label35.TabIndex = 52;
      this.label35.Text = "Color 4:";
      this.label37.AutoSize = true;
      this.label37.ForeColor = Color.Black;
      this.label37.Location = new Point(102, 11);
      this.label37.Name = "label37";
      this.label37.Size = new Size(43, 13);
      this.label37.TabIndex = 50;
      this.label37.Text = "Color 3:";
      this.label39.AutoSize = true;
      this.label39.ForeColor = Color.Black;
      this.label39.Location = new Point(6, 39);
      this.label39.Name = "label39";
      this.label39.Size = new Size(43, 13);
      this.label39.TabIndex = 48;
      this.label39.Text = "Color 2:";
      this.label41.AutoSize = true;
      this.label41.ForeColor = Color.Black;
      this.label41.Location = new Point(6, 11);
      this.label41.Name = "label41";
      this.label41.Size = new Size(43, 13);
      this.label41.TabIndex = 46;
      this.label41.Text = "Color 1:";
      this.colorBoxOfficeGlassRect4.BackColor = Color.Turquoise;
      this.colorBoxOfficeGlassRect4.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxOfficeGlassRect4.Location = new Point(151, 37);
      this.colorBoxOfficeGlassRect4.Name = "colorBoxOfficeGlassRect4";
      this.colorBoxOfficeGlassRect4.Size = new Size(41, 23);
      this.colorBoxOfficeGlassRect4.TabIndex = 53;
      this.colorBoxOfficeGlassRect3.BackColor = Color.Turquoise;
      this.colorBoxOfficeGlassRect3.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxOfficeGlassRect3.Location = new Point(151, 9);
      this.colorBoxOfficeGlassRect3.Name = "colorBoxOfficeGlassRect3";
      this.colorBoxOfficeGlassRect3.Size = new Size(41, 23);
      this.colorBoxOfficeGlassRect3.TabIndex = 51;
      this.colorBoxOfficeGlassRect2.BackColor = Color.Turquoise;
      this.colorBoxOfficeGlassRect2.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxOfficeGlassRect2.Location = new Point(55, 37);
      this.colorBoxOfficeGlassRect2.Name = "colorBoxOfficeGlassRect2";
      this.colorBoxOfficeGlassRect2.Size = new Size(41, 23);
      this.colorBoxOfficeGlassRect2.TabIndex = 49;
      this.colorBoxOfficeGlassRect1.BackColor = Color.Turquoise;
      this.colorBoxOfficeGlassRect1.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxOfficeGlassRect1.Location = new Point(55, 9);
      this.colorBoxOfficeGlassRect1.Name = "colorBoxOfficeGlassRect1";
      this.colorBoxOfficeGlassRect1.Size = new Size(41, 23);
      this.colorBoxOfficeGlassRect1.TabIndex = 47;
      this.tabPage4.Controls.Add((Control) this.label7);
      this.tabPage4.Controls.Add((Control) this.comboBox2);
      this.tabPage4.Controls.Add((Control) this.numericGelPercentage);
      this.tabPage4.Controls.Add((Control) this.label14);
      this.tabPage4.Controls.Add((Control) this.label12);
      this.tabPage4.Controls.Add((Control) this.label10);
      this.tabPage4.Controls.Add((Control) this.colorBoxGelColor2);
      this.tabPage4.Controls.Add((Control) this.colorBoxGelColor1);
      this.tabPage4.Location = new Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new Padding(3);
      this.tabPage4.Size = new Size(612, 99);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Gel";
      this.tabPage4.UseVisualStyleBackColor = true;
      this.label7.AutoSize = true;
      this.label7.ForeColor = Color.Black;
      this.label7.Location = new Point(244, 10);
      this.label7.Name = "label7";
      this.label7.Size = new Size(68, 13);
      this.label7.TabIndex = 45;
      this.label7.Text = "QuickTheme";
      this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Items.AddRange(new object[8]
      {
        (object) "Red",
        (object) "Green",
        (object) "Blue",
        (object) "Yellow",
        (object) "Black",
        (object) "White",
        (object) "Orange",
        (object) "Purple"
      });
      this.comboBox2.Location = new Point(247, 26);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new Size(71, 21);
      this.comboBox2.TabIndex = 32;
      this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
      this.numericGelPercentage.DecimalPlaces = 2;
      this.numericGelPercentage.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericGelPercentage.Location = new Point(77, 67);
      this.numericGelPercentage.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericGelPercentage.Name = "numericGelPercentage";
      this.numericGelPercentage.Size = new Size(53, 20);
      this.numericGelPercentage.TabIndex = 14;
      this.numericGelPercentage.ValueChanged += new EventHandler(this.gradientPercentage_ValueChanged);
      this.label14.AutoSize = true;
      this.label14.ForeColor = Color.Black;
      this.label14.Location = new Point(6, 69);
      this.label14.Name = "label14";
      this.label14.Size = new Size(65, 13);
      this.label14.TabIndex = 13;
      this.label14.Text = "Percentage:";
      this.label12.AutoSize = true;
      this.label12.ForeColor = Color.Black;
      this.label12.Location = new Point(6, 39);
      this.label12.Name = "label12";
      this.label12.Size = new Size(43, 13);
      this.label12.TabIndex = 7;
      this.label12.Text = "Color 2:";
      this.label10.AutoSize = true;
      this.label10.ForeColor = Color.Black;
      this.label10.Location = new Point(6, 11);
      this.label10.Name = "label10";
      this.label10.Size = new Size(43, 13);
      this.label10.TabIndex = 5;
      this.label10.Text = "Color 1:";
      this.colorBoxGelColor2.BackColor = Color.Turquoise;
      this.colorBoxGelColor2.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxGelColor2.Location = new Point(55, 37);
      this.colorBoxGelColor2.Name = "colorBoxGelColor2";
      this.colorBoxGelColor2.Size = new Size(41, 23);
      this.colorBoxGelColor2.TabIndex = 8;
      this.colorBoxGelColor1.BackColor = Color.Turquoise;
      this.colorBoxGelColor1.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxGelColor1.Location = new Point(55, 9);
      this.colorBoxGelColor1.Name = "colorBoxGelColor1";
      this.colorBoxGelColor1.Size = new Size(41, 23);
      this.colorBoxGelColor1.TabIndex = 6;
      this.tabPage7.Controls.Add((Control) this.label6);
      this.tabPage7.Controls.Add((Control) this.comboBox3);
      this.tabPage7.Controls.Add((Control) this.numericVistaPercentage2);
      this.tabPage7.Controls.Add((Control) this.label33);
      this.tabPage7.Controls.Add((Control) this.numericVistaPercentage1);
      this.tabPage7.Controls.Add((Control) this.label34);
      this.tabPage7.Controls.Add((Control) this.label36);
      this.tabPage7.Controls.Add((Control) this.label38);
      this.tabPage7.Controls.Add((Control) this.label40);
      this.tabPage7.Controls.Add((Control) this.label42);
      this.tabPage7.Controls.Add((Control) this.colorBoxVista4);
      this.tabPage7.Controls.Add((Control) this.colorBoxVista3);
      this.tabPage7.Controls.Add((Control) this.colorBoxVista2);
      this.tabPage7.Controls.Add((Control) this.colorBoxVista1);
      this.tabPage7.ForeColor = Color.Black;
      this.tabPage7.Location = new Point(4, 22);
      this.tabPage7.Name = "tabPage7";
      this.tabPage7.Padding = new Padding(3);
      this.tabPage7.Size = new Size(612, 99);
      this.tabPage7.TabIndex = 6;
      this.tabPage7.Text = "TelerikMetro";
      this.tabPage7.UseVisualStyleBackColor = true;
      this.label6.AutoSize = true;
      this.label6.ForeColor = Color.Black;
      this.label6.Location = new Point(244, 10);
      this.label6.Name = "label6";
      this.label6.Size = new Size(68, 13);
      this.label6.TabIndex = 44;
      this.label6.Text = "QuickTheme";
      this.comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox3.FormattingEnabled = true;
      this.comboBox3.Items.AddRange(new object[8]
      {
        (object) "Red",
        (object) "Green",
        (object) "Blue",
        (object) "Yellow",
        (object) "Black",
        (object) "White",
        (object) "Orange",
        (object) "Purple"
      });
      this.comboBox3.Location = new Point(247, 26);
      this.comboBox3.Name = "comboBox3";
      this.comboBox3.Size = new Size(71, 21);
      this.comboBox3.TabIndex = 43;
      this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox3_SelectedIndexChanged);
      this.numericVistaPercentage2.DecimalPlaces = 2;
      this.numericVistaPercentage2.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericVistaPercentage2.Location = new Point(234, 68);
      this.numericVistaPercentage2.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericVistaPercentage2.Name = "numericVistaPercentage2";
      this.numericVistaPercentage2.Size = new Size(53, 20);
      this.numericVistaPercentage2.TabIndex = 42;
      this.numericVistaPercentage2.ValueChanged += new EventHandler(this.gradientPercentage2_ValueChanged);
      this.label33.AutoSize = true;
      this.label33.ForeColor = Color.Black;
      this.label33.Location = new Point(154, 70);
      this.label33.Name = "label33";
      this.label33.Size = new Size(74, 13);
      this.label33.TabIndex = 41;
      this.label33.Text = "Percentage 2:";
      this.numericVistaPercentage1.DecimalPlaces = 2;
      this.numericVistaPercentage1.Increment = new Decimal(new int[4]
      {
        1,
        0,
        0,
        65536
      });
      this.numericVistaPercentage1.Location = new Point(86, 68);
      this.numericVistaPercentage1.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numericVistaPercentage1.Name = "numericVistaPercentage1";
      this.numericVistaPercentage1.Size = new Size(53, 20);
      this.numericVistaPercentage1.TabIndex = 40;
      this.numericVistaPercentage1.ValueChanged += new EventHandler(this.gradientPercentage_ValueChanged);
      this.label34.AutoSize = true;
      this.label34.ForeColor = Color.Black;
      this.label34.Location = new Point(6, 69);
      this.label34.Name = "label34";
      this.label34.Size = new Size(74, 13);
      this.label34.TabIndex = 39;
      this.label34.Text = "Percentage 1:";
      this.label36.AutoSize = true;
      this.label36.ForeColor = Color.Black;
      this.label36.Location = new Point(102, 39);
      this.label36.Name = "label36";
      this.label36.Size = new Size(43, 13);
      this.label36.TabIndex = 37;
      this.label36.Text = "Color 4:";
      this.label38.AutoSize = true;
      this.label38.ForeColor = Color.Black;
      this.label38.Location = new Point(102, 11);
      this.label38.Name = "label38";
      this.label38.Size = new Size(43, 13);
      this.label38.TabIndex = 35;
      this.label38.Text = "Color 3:";
      this.label40.AutoSize = true;
      this.label40.ForeColor = Color.Black;
      this.label40.Location = new Point(6, 39);
      this.label40.Name = "label40";
      this.label40.Size = new Size(43, 13);
      this.label40.TabIndex = 33;
      this.label40.Text = "Color 2:";
      this.label42.AutoSize = true;
      this.label42.ForeColor = Color.Black;
      this.label42.Location = new Point(6, 11);
      this.label42.Name = "label42";
      this.label42.Size = new Size(43, 13);
      this.label42.TabIndex = 31;
      this.label42.Text = "Color 1:";
      this.colorBoxVista4.BackColor = Color.Turquoise;
      this.colorBoxVista4.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxVista4.Location = new Point(151, 37);
      this.colorBoxVista4.Name = "colorBoxVista4";
      this.colorBoxVista4.Size = new Size(41, 23);
      this.colorBoxVista4.TabIndex = 38;
      this.colorBoxVista3.BackColor = Color.Turquoise;
      this.colorBoxVista3.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxVista3.Location = new Point(151, 9);
      this.colorBoxVista3.Name = "colorBoxVista3";
      this.colorBoxVista3.Size = new Size(41, 23);
      this.colorBoxVista3.TabIndex = 36;
      this.colorBoxVista2.BackColor = Color.Turquoise;
      this.colorBoxVista2.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxVista2.Location = new Point(55, 37);
      this.colorBoxVista2.Name = "colorBoxVista2";
      this.colorBoxVista2.Size = new Size(41, 23);
      this.colorBoxVista2.TabIndex = 34;
      this.colorBoxVista1.BackColor = Color.Turquoise;
      this.colorBoxVista1.BorderStyle = BorderStyle.Fixed3D;
      this.colorBoxVista1.Location = new Point(55, 9);
      this.colorBoxVista1.Name = "colorBoxVista1";
      this.colorBoxVista1.Size = new Size(41, 23);
      this.colorBoxVista1.TabIndex = 32;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.AutoSize = true;
      this.Controls.Add((Control) this.tabControl1);
      this.Controls.Add((Control) this.groupBox1);
      this.Name = nameof (GradientEditorControl);
      this.Size = new Size(642, 380);
      this.groupBox1.ResumeLayout(false);
      this.gradientBox1.EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.numericLinearAngle.EndInit();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.numericRadial2.EndInit();
      this.numericRadial1.EndInit();
      this.tabPage5.ResumeLayout(false);
      this.tabPage5.PerformLayout();
      this.numericGlassPercentage.EndInit();
      this.tabPage6.ResumeLayout(false);
      this.tabPage6.PerformLayout();
      this.numericOfficePercentage2.EndInit();
      this.numericOfficePercentage1.EndInit();
      this.tabPage8.ResumeLayout(false);
      this.tabPage8.PerformLayout();
      this.numericOfficeRectPercentage2.EndInit();
      this.numericOfficeRectPercentage1.EndInit();
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      this.numericGelPercentage.EndInit();
      this.tabPage7.ResumeLayout(false);
      this.tabPage7.PerformLayout();
      this.numericVistaPercentage2.EndInit();
      this.numericVistaPercentage1.EndInit();
      this.ResumeLayout(false);
    }
  }
}
