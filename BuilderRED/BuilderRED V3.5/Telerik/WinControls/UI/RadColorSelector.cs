// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Themes.ColorDialog;
using Telerik.WinControls.UI.Properties;
using Telerik.WinControls.UI.RadColorPicker;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Description("Enables users to select colors from presets and the continuous RgbValue or HSL color spaces using a highly customizable interface")]
  [DefaultEvent("ColorChanged")]
  [DefaultProperty("SelectedColor")]
  public class RadColorSelector : UserControl, IColorSelector, IAnalyticsProvider
  {
    private bool enableAnalytics = true;
    private string analyticsName = "";
    private ColorPickerActiveMode colorPickerActiveMode;
    private HslColor selectedHslColor;
    private Color selectedColor;
    private bool supressTextBoxColorChange;
    private IContainer components;
    private DiscreteColorHexagon discreteColorHexagon;
    private TransparentColorBox labelColor;
    private RadLabel newLabel;
    private RadLabel labelOldColor;
    private Panel panel1;
    private RadLabel hexHeadingLabel;
    private Telerik.WinControls.UI.RadColorPicker.ProfessionalColors professionalColorsControl;
    private RadButton radButton1;
    private RadButton btnScreenColorPick;
    private RadButton radButton3;
    private RadPageView radPageView1;
    private RadPageViewPage radPageViewPage1;
    private RadPageViewPage radPageViewPage2;
    private RadPageViewPage radPageViewPage3;
    private RadPageViewPage radPageViewPage4;
    private ColorListBox listBox2;
    private ColorListBox listBox1;
    private RadButton btnAddNewColor;
    private RadLabel currentLabel;
    private RadTextBox textBoxColor;
    private Telerik.WinControls.UI.RadColorPicker.CustomColors customColors;
    private CaptureBox captureBox;

    public event CustomColorsEventHandler CustomColorsConfigLocationNeeded;

    public event ColorChangedEventHandler ColorChanged;

    public event ColorChangedEventHandler OkButtonClicked;

    public event ColorChangedEventHandler CancelButtonClicked;

    public RadColorSelector()
    {
      this.InitializeComponent();
      this.captureBox.ColorChanged += new ColorChangedEventHandler(this.colorDialog_ColorChanged);
      this.customColors.ColorChanged += new ColorChangedEventHandler(this.colorDialog_ColorChanged);
      this.customColors.CustomColorsConfigLocationNeeded += new CustomColorsEventHandler(this.customColors_CustomColorsConfigLocationNeeded);
      this.professionalColorsControl.ColorChanged += new ColorChangedEventHandler(this.colorDialog_ColorChanged);
      this.professionalColorsControl.MouseDoubleClick += new MouseEventHandler(this.professionalColorsControl_MouseDoubleClick);
      LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.ColorDialogLocalizationProvider_CurrentProviderChanged);
      this.radPageView1.EnableAnalytics = false;
      ((RadPageViewStripElement) this.radPageView1.ViewElement).StripButtons = StripViewButtons.None;
      this.radPageView1.ViewElement.DrawFill = false;
      this.radPageView1.ViewElement.DrawBorder = false;
      this.LocalizeStrings();
      this.listBox1.DataSource = (object) ColorProvider.SystemColors;
      this.listBox1.ColorChanged += new ColorChangedEventHandler(this.colorDialog_ColorChanged);
      this.listBox2.DataSource = (object) ColorProvider.NamedColors;
      this.listBox2.ColorChanged += new ColorChangedEventHandler(this.colorDialog_ColorChanged);
      this.listBox2.MouseDoubleClick += new MouseEventHandler(this.listBox2_MouseDoubleClick);
      this.listBox1.MouseDoubleClick += new MouseEventHandler(this.listBox1_MouseDoubleClick);
      this.listBox1.KeyDown += new KeyEventHandler(this.listBox1_KeyDown);
      this.listBox2.KeyDown += new KeyEventHandler(this.listBox2_KeyDown);
      this.listBox1.EnableAnalytics = this.listBox2.EnableAnalytics = false;
      this.discreteColorHexagon.ColorChanged += new ColorChangedEventHandler(this.colorDialog_ColorChanged);
      this.discreteColorHexagon.MouseDoubleClick += new MouseEventHandler(this.colorPalette1_MouseDoubleClick);
      this.discreteColorHexagon.KeyDown += new KeyEventHandler(this.colorPalette1_KeyDown);
      this.KeyDown += new KeyEventHandler(this.RadColorDialog_KeyDown);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Description("Gets or sets a value indicating whether the Analytics functionality is enable or disable for this control.")]
    public virtual bool EnableAnalytics
    {
      get
      {
        return this.enableAnalytics;
      }
      set
      {
        this.enableAnalytics = value;
      }
    }

    public RadPageView ControlsHolderPageView
    {
      get
      {
        return this.radPageView1;
      }
    }

    [Category("Layout")]
    [Description("Gets or sets the new color.")]
    public Color SelectedColor
    {
      get
      {
        return this.selectedColor;
      }
      set
      {
        if (!(this.selectedColor != value))
          return;
        Color backColor = this.labelColor.BackColor;
        this.professionalColorsControl.ColorRgb = value;
        this.labelOldColor.BackColor = backColor;
        this.labelColor.BackColor = value;
        this.selectedHslColor = HslColor.FromColor(value);
        this.selectedColor = value;
      }
    }

    [Description("Gets or sets the new RGB color.")]
    [Category("Layout")]
    public Color SelectedRgbColor
    {
      get
      {
        return this.selectedHslColor.RgbValue;
      }
      set
      {
        if (!(this.selectedHslColor.RgbValue != value))
          return;
        this.SelectedColor = value;
      }
    }

    [Description("Gets or sets selected HSL color.")]
    [Category("Layout")]
    public HslColor SelectedHslColor
    {
      get
      {
        return this.selectedHslColor;
      }
      set
      {
        if (!(this.selectedHslColor != value))
          return;
        this.SelectedColor = value.RgbValue;
      }
    }

    private void SetSelectedColor(Color color)
    {
      this.selectedHslColor = HslColor.FromColor(color);
      this.selectedColor = color;
      this.labelColor.BackColor = color;
    }

    [Category("Layout")]
    [Description("Gets or sets the old color.")]
    public Color OldColor
    {
      get
      {
        return this.labelOldColor.BackColor;
      }
      set
      {
        this.labelOldColor.BackColor = value;
      }
    }

    public Color[] CustomColors
    {
      get
      {
        return this.customColors.Colors;
      }
    }

    [Category("Layout")]
    [DefaultValue(true)]
    [Description("Shows or hides the web colors tab.")]
    public bool ShowWebColors
    {
      get
      {
        return this.radPageView1.Pages[2].Item.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (this.radPageView1.Pages[0].Item.Visibility != ElementVisibility.Visible && this.radPageView1.Pages[1].Item.Visibility != ElementVisibility.Visible && this.radPageView1.Pages[3].Item.Visibility != ElementVisibility.Visible)
          return;
        if (value)
        {
          this.radPageView1.Pages[2].Item.Visibility = ElementVisibility.Visible;
          this.radPageView1.SelectedPage = this.radPageView1.Pages[2];
        }
        else
        {
          bool flag = object.ReferenceEquals((object) this.radPageView1.Pages[2], (object) this.radPageView1.SelectedPage);
          this.radPageView1.Pages[2].Item.Visibility = ElementVisibility.Collapsed;
          if (!flag)
            return;
          if (this.radPageView1.Pages[3].Item.Visibility == ElementVisibility.Visible)
            this.radPageView1.SelectedPage = this.radPageView1.Pages[3];
          else if (this.radPageView1.Pages[1].Item.Visibility == ElementVisibility.Visible)
          {
            this.radPageView1.SelectedPage = this.radPageView1.Pages[1];
          }
          else
          {
            if (this.radPageView1.Pages[0].Item.Visibility != ElementVisibility.Visible)
              return;
            this.radPageView1.SelectedPage = this.radPageView1.Pages[0];
          }
        }
      }
    }

    [DefaultValue(true)]
    [Category("Layout")]
    [Description("Shows or hides the basic colors tab.")]
    public bool ShowBasicColors
    {
      get
      {
        return this.radPageView1.Pages[0].Item.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (this.radPageView1.Pages[1].Item.Visibility != ElementVisibility.Visible && this.radPageView1.Pages[2].Item.Visibility != ElementVisibility.Visible && this.radPageView1.Pages[3].Item.Visibility != ElementVisibility.Visible)
          return;
        if (value)
        {
          this.radPageView1.Pages[0].Item.Visibility = ElementVisibility.Visible;
          this.radPageView1.SelectedPage = this.radPageView1.Pages[0];
        }
        else
        {
          bool flag = object.ReferenceEquals((object) this.radPageView1.Pages[0], (object) this.radPageView1.SelectedPage);
          this.radPageView1.Pages[0].Item.Visibility = ElementVisibility.Collapsed;
          if (!flag)
            return;
          if (this.radPageView1.Pages[1].Item.Visibility == ElementVisibility.Visible)
            this.radPageView1.SelectedPage = this.radPageView1.Pages[1];
          else if (this.radPageView1.Pages[2].Item.Visibility == ElementVisibility.Visible)
          {
            this.radPageView1.SelectedPage = this.radPageView1.Pages[2];
          }
          else
          {
            if (this.radPageView1.Pages[3].Item.Visibility != ElementVisibility.Visible)
              return;
            this.radPageView1.SelectedPage = this.radPageView1.Pages[3];
          }
        }
      }
    }

    [Category("Layout")]
    [DefaultValue(true)]
    [Description("Shows or hides the system colors tab.")]
    public bool ShowSystemColors
    {
      get
      {
        return this.radPageView1.Pages[1].Item.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (this.radPageView1.Pages[0].Item.Visibility != ElementVisibility.Visible && this.radPageView1.Pages[2].Item.Visibility != ElementVisibility.Visible && this.radPageView1.Pages[3].Item.Visibility != ElementVisibility.Visible)
          return;
        if (value)
        {
          this.radPageView1.Pages[1].Item.Visibility = ElementVisibility.Visible;
          this.radPageView1.SelectedPage = this.radPageView1.Pages[1];
        }
        else
        {
          bool flag = object.ReferenceEquals((object) this.radPageView1.Pages[1], (object) this.radPageView1.SelectedPage);
          this.radPageView1.Pages[1].Item.Visibility = ElementVisibility.Collapsed;
          if (!flag)
            return;
          if (this.radPageView1.Pages[2].Item.Visibility == ElementVisibility.Visible)
            this.radPageView1.SelectedPage = this.radPageView1.Pages[2];
          else if (this.radPageView1.Pages[0].Item.Visibility == ElementVisibility.Visible)
          {
            this.radPageView1.SelectedPage = this.radPageView1.Pages[0];
          }
          else
          {
            if (this.radPageView1.Pages[3].Item.Visibility != ElementVisibility.Visible)
              return;
            this.radPageView1.SelectedPage = this.radPageView1.Pages[3];
          }
        }
      }
    }

    [Category("Layout")]
    [DefaultValue(true)]
    [Description("Shows or hides the professional colors tab.")]
    public bool ShowProfessionalColors
    {
      get
      {
        return this.radPageView1.Pages[3].Item.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (this.radPageView1.Pages[0].Item.Visibility != ElementVisibility.Visible && this.radPageView1.Pages[1].Item.Visibility != ElementVisibility.Visible && this.radPageView1.Pages[2].Item.Visibility != ElementVisibility.Visible)
          return;
        if (value)
        {
          this.radPageView1.Pages[3].Item.Visibility = ElementVisibility.Visible;
          this.radPageView1.SelectedPage = this.radPageView1.Pages[3];
        }
        else
        {
          bool flag = object.ReferenceEquals((object) this.radPageView1.Pages[3], (object) this.radPageView1.SelectedPage);
          this.radPageView1.Pages[3].Item.Visibility = ElementVisibility.Collapsed;
          if (!flag)
            return;
          if (this.radPageView1.Pages[0].Item.Visibility == ElementVisibility.Visible)
            this.radPageView1.SelectedPage = this.radPageView1.Pages[0];
          else if (this.radPageView1.Pages[1].Item.Visibility == ElementVisibility.Visible)
          {
            this.radPageView1.SelectedPage = this.radPageView1.Pages[1];
          }
          else
          {
            if (this.radPageView1.Pages[2].Item.Visibility != ElementVisibility.Visible)
              return;
            this.radPageView1.SelectedPage = this.radPageView1.Pages[2];
          }
        }
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Shows or hides the custom colors panel.")]
    public bool ShowCustomColors
    {
      get
      {
        return this.customColors.Visible;
      }
      set
      {
        this.customColors.Visible = value;
        this.btnAddNewColor.Visible = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Shows or hides the HEX color box.")]
    public bool ShowHEXColorValue
    {
      get
      {
        return this.textBoxColor.Visible;
      }
      set
      {
        this.hexHeadingLabel.Visible = value;
        this.textBoxColor.Visible = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Allows or disallows editing the HEX value.")]
    public bool AllowEditHEXValue
    {
      get
      {
        return !this.textBoxColor.ReadOnly;
      }
      set
      {
        this.textBoxColor.ReadOnly = !value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Allows or disallows picking a color from screen.")]
    public bool AllowColorPickFromScreen
    {
      get
      {
        return this.btnScreenColorPick.Visible;
      }
      set
      {
        this.btnScreenColorPick.Visible = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Allows or disallows saving custom colors.")]
    public bool AllowColorSaving
    {
      get
      {
        return this.btnAddNewColor.Visible;
      }
      set
      {
        this.btnAddNewColor.Visible = value;
      }
    }

    [Category("Appearance")]
    [Localizable(true)]
    [Description("Gets or sets the text of the add new color button.")]
    public string AddNewColorButtonText
    {
      get
      {
        return this.btnAddNewColor.Text;
      }
      set
      {
        this.btnAddNewColor.Text = value;
      }
    }

    [DefaultValue(ColorPickerActiveMode.Basic)]
    [Category("Behavior")]
    [Description("Gets or sets the mode in which the color picker operates.")]
    public ColorPickerActiveMode ActiveMode
    {
      get
      {
        return this.colorPickerActiveMode;
      }
      set
      {
        this.colorPickerActiveMode = value;
        switch (this.colorPickerActiveMode)
        {
          case ColorPickerActiveMode.Basic:
            this.radPageView1.SelectedPage = this.radPageView1.Pages[0];
            break;
          case ColorPickerActiveMode.System:
            this.radPageView1.SelectedPage = this.radPageView1.Pages[1];
            break;
          case ColorPickerActiveMode.Web:
            this.radPageView1.SelectedPage = this.radPageView1.Pages[2];
            break;
          case ColorPickerActiveMode.Professional:
            this.radPageView1.SelectedPage = this.radPageView1.Pages[3];
            break;
        }
        if (!this.enableAnalytics || string.IsNullOrEmpty(this.Name) || !this.IsHandleCreated)
          return;
        ControlTraceMonitor.TrackAtomicFeature(this.Name, "ColorModeChanged", (object) this.colorPickerActiveMode.ToString());
      }
    }

    [Category("Behavior")]
    [Localizable(true)]
    [Description("Gets or sets the heading of the basic colors tab.")]
    public string BasicTabHeading
    {
      get
      {
        return this.radPageView1.Pages[0].Text;
      }
      set
      {
        this.radPageView1.Pages[0].Text = value;
      }
    }

    [Category("Behavior")]
    [Localizable(true)]
    [Description("Gets or sets the heading of the system colors tab.")]
    public string SystemTabHeading
    {
      get
      {
        return this.radPageView1.Pages[1].Text;
      }
      set
      {
        this.radPageView1.Pages[1].Text = value;
      }
    }

    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the heading of the web colors tab.")]
    public string WebTabHeading
    {
      get
      {
        return this.radPageView1.Pages[2].Text;
      }
      set
      {
        this.radPageView1.Pages[2].Text = value;
      }
    }

    [Category("Behavior")]
    [Localizable(true)]
    [Description("Gets or sets the heading of the professional colors tab.")]
    public string ProfessionalTabHeading
    {
      get
      {
        return this.radPageView1.Pages[3].Text;
      }
      set
      {
        this.radPageView1.Pages[3].Text = value;
      }
    }

    [Category("Behavior")]
    [Localizable(true)]
    [Description("Gets or sets the heading of the selected color label.")]
    public string SelectedColorLabelHeading
    {
      get
      {
        return this.newLabel.Text;
      }
      set
      {
        this.newLabel.Text = value;
      }
    }

    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the heading of the old color label.")]
    public string OldColorLabelHeading
    {
      get
      {
        return this.currentLabel.Text;
      }
      set
      {
        this.currentLabel.Text = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DiscreteColorHexagon DiscreteColorHexagon
    {
      get
      {
        return this.discreteColorHexagon;
      }
    }

    public bool SaveCustomColors
    {
      get
      {
        return this.customColors.SaveCustomColors;
      }
      set
      {
        this.customColors.SaveCustomColors = value;
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Return:
          if (this.OkButtonClicked != null)
          {
            this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
            break;
          }
          break;
        case Keys.Escape:
          if (this.CancelButtonClicked != null)
          {
            this.CancelButtonClicked((object) this, new ColorChangedEventArgs(this.OldColor));
            break;
          }
          break;
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      this.professionalColorsControl.RightToLeft = RightToLeft.No;
      this.professionalColorsControl.RightToLeft = RightToLeft.Yes;
      this.professionalColorsControl.RightToLeft = this.RightToLeft;
      this.listBox1.RightToLeft = RightToLeft.No;
      this.listBox1.RightToLeft = RightToLeft.Yes;
      this.listBox1.RightToLeft = this.RightToLeft;
      this.listBox2.RightToLeft = RightToLeft.No;
      this.listBox2.RightToLeft = RightToLeft.Yes;
      this.listBox2.RightToLeft = this.RightToLeft;
    }

    private void listBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return || this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void listBox2_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return || this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void colorPalette1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return || this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void RadColorDialog_KeyDown(object sender, KeyEventArgs e)
    {
      int num = (int) MessageBox.Show(e.KeyCode.ToString());
    }

    private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (!(this.listBox2.RootElement.ElementTree.GetElementAtPoint((RadElement) this.listBox2.RootElement, e.Location, (List<RadElement>) null) is RadListVisualItem) || this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (!(this.listBox1.RootElement.ElementTree.GetElementAtPoint((RadElement) this.listBox1.RootElement, e.Location, (List<RadElement>) null) is RadListVisualItem) || this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void radarColors1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void professionalColorsControl_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void colorPalette1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void buttonColorPick_Click(object sender, EventArgs e)
    {
      this.captureBox.Start();
    }

    private void colorDialog_ColorChanged(object sender, ColorChangedEventArgs args)
    {
      if (this.selectedHslColor.IsEmpty)
        this.labelOldColor.BackColor = args.SelectedHslColor.RgbValue;
      if (sender != this.professionalColorsControl)
        this.professionalColorsControl.SetColorSilently(args.SelectedHslColor);
      this.supressTextBoxColorChange = true;
      this.textBoxColor.Text = ColorProvider.ColorToHex(args.SelectedColor);
      this.supressTextBoxColorChange = false;
      this.SetSelectedColor(args.SelectedColor);
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, args);
    }

    private void customColors_CustomColorsConfigLocationNeeded(
      object sender,
      CustomColorsEventArgs args)
    {
      if (this.CustomColorsConfigLocationNeeded == null)
        return;
      this.CustomColorsConfigLocationNeeded(sender, args);
    }

    private void textBoxColor_TextChanged(object sender, EventArgs e)
    {
      if (this.supressTextBoxColorChange)
        return;
      Color color = ColorProvider.HexToColor(this.textBoxColor.Text);
      if (color == Color.Empty)
        return;
      this.SetSelectedColor(color);
      this.professionalColorsControl.SetColorSilently(HslColor.FromColor(color));
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, new ColorChangedEventArgs(color));
    }

    private void radButton4_Click(object sender, EventArgs e)
    {
      this.customColors.SaveColor(this.labelColor.BackColor);
    }

    private void radButton1_Click(object sender, EventArgs e)
    {
      if (this.OkButtonClicked == null)
        return;
      this.OkButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void radButton3_Click(object sender, EventArgs e)
    {
      if (this.CancelButtonClicked == null)
        return;
      this.CancelButtonClicked((object) this, new ColorChangedEventArgs(this.SelectedColor));
    }

    private void ColorDialogLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.LocalizeStrings();
    }

    private void LocalizeStrings()
    {
      this.newLabel.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogNewColorLabel");
      this.currentLabel.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogCurrentColorLabel");
      this.btnAddNewColor.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogAddCustomColorButton");
      this.radPageViewPage1.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogBasicTab");
      this.radPageViewPage2.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogSystemTab");
      this.radPageViewPage3.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogWebTab");
      this.radPageViewPage4.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogProfessionalTab");
      this.radButton3.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogCancelButton");
      this.radButton1.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogOKButton");
    }

    [Description("Gets or sets the Analytics Name associated with this control.")]
    [DefaultValue("")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string AnalyticsName
    {
      get
      {
        return this.analyticsName;
      }
      set
      {
        this.analyticsName = value;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.ColorDialogLocalizationProvider_CurrentProviderChanged);
        if (this.components != null)
          this.components.Dispose();
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.newLabel = new RadLabel();
      this.labelOldColor = new RadLabel();
      this.panel1 = new Panel();
      this.labelColor = new TransparentColorBox();
      this.hexHeadingLabel = new RadLabel();
      this.currentLabel = new RadLabel();
      this.customColors = new Telerik.WinControls.UI.RadColorPicker.CustomColors();
      this.textBoxColor = new RadTextBox();
      this.btnAddNewColor = new RadButton();
      this.radPageView1 = new RadPageView();
      this.radPageViewPage1 = new RadPageViewPage();
      this.discreteColorHexagon = new DiscreteColorHexagon();
      this.radPageViewPage2 = new RadPageViewPage();
      this.listBox1 = new ColorListBox();
      this.radPageViewPage3 = new RadPageViewPage();
      this.listBox2 = new ColorListBox();
      this.radPageViewPage4 = new RadPageViewPage();
      this.professionalColorsControl = new Telerik.WinControls.UI.RadColorPicker.ProfessionalColors();
      this.radButton3 = new RadButton();
      this.btnScreenColorPick = new RadButton();
      this.radButton1 = new RadButton();
      this.captureBox = new CaptureBox();
      this.newLabel.BeginInit();
      this.labelOldColor.BeginInit();
      this.panel1.SuspendLayout();
      this.labelColor.BeginInit();
      this.hexHeadingLabel.BeginInit();
      this.currentLabel.BeginInit();
      this.textBoxColor.BeginInit();
      this.btnAddNewColor.BeginInit();
      this.radPageView1.BeginInit();
      this.radPageView1.SuspendLayout();
      this.radPageViewPage1.SuspendLayout();
      this.radPageViewPage2.SuspendLayout();
      this.listBox1.BeginInit();
      this.radPageViewPage3.SuspendLayout();
      this.listBox2.BeginInit();
      this.radPageViewPage4.SuspendLayout();
      this.radButton3.BeginInit();
      this.btnScreenColorPick.BeginInit();
      this.radButton1.BeginInit();
      this.SuspendLayout();
      this.newLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.newLabel.Location = new Point(445, 45);
      this.newLabel.Name = "newLabel";
      this.newLabel.Size = new Size(29, 18);
      this.newLabel.TabIndex = 4;
      this.newLabel.Text = "New";
      this.labelOldColor.Anchor = AnchorStyles.None;
      this.labelOldColor.BackColor = Color.LightGray;
      this.labelOldColor.Location = new Point(-6, 25);
      this.labelOldColor.Name = "labelOldColor";
      this.labelOldColor.Size = new Size(2, 2);
      this.labelOldColor.TabIndex = 5;
      this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.panel1.BorderStyle = BorderStyle.FixedSingle;
      this.panel1.Controls.Add((Control) this.labelOldColor);
      this.panel1.Controls.Add((Control) this.labelColor);
      this.panel1.Location = new Point(448, 62);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(57, 50);
      this.panel1.TabIndex = 7;
      this.labelColor.Anchor = AnchorStyles.None;
      this.labelColor.Location = new Point(-6, 0);
      this.labelColor.Name = "labelColor";
      this.labelColor.Size = new Size(67, 25);
      this.labelColor.TabIndex = 0;
      this.hexHeadingLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.hexHeadingLabel.Location = new Point(437, 142);
      this.hexHeadingLabel.Name = "hexHeadingLabel";
      this.hexHeadingLabel.Size = new Size(13, 18);
      this.hexHeadingLabel.TabIndex = 19;
      this.hexHeadingLabel.Text = "#";
      this.currentLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.currentLabel.Location = new Point(442, 122);
      this.currentLabel.Name = "currentLabel";
      this.currentLabel.Size = new Size(44, 18);
      this.currentLabel.TabIndex = 27;
      this.currentLabel.Text = "Current";
      this.customColors.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.customColors.BackColor = Color.Transparent;
      this.customColors.Location = new Point(3, 301);
      this.customColors.Name = "customColors";
      this.customColors.Padding = new Padding(5);
      this.customColors.SaveCustomColors = true;
      this.customColors.SelectedColorIndex = -1;
      this.customColors.Size = new Size(290, 32);
      this.customColors.TabIndex = 29;
      this.textBoxColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.textBoxColor.Location = new Point(448, 138);
      this.textBoxColor.Name = "textBoxColor";
      this.textBoxColor.Padding = new Padding(2, 2, 2, 3);
      this.textBoxColor.RootElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.textBoxColor.Size = new Size(57, 22);
      this.textBoxColor.TabIndex = 28;
      this.textBoxColor.TabStop = false;
      this.textBoxColor.TextChanged += new EventHandler(this.textBoxColor_TextChanged);
      this.btnAddNewColor.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.btnAddNewColor.Location = new Point(3, 333);
      this.btnAddNewColor.Name = "btnAddNewColor";
      this.btnAddNewColor.Size = new Size(111, 23);
      this.btnAddNewColor.TabIndex = 26;
      this.btnAddNewColor.Text = "Add Custom Color";
      this.btnAddNewColor.Click += new EventHandler(this.radButton4_Click);
      this.radPageView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.radPageView1.Controls.Add((Control) this.radPageViewPage1);
      this.radPageView1.Controls.Add((Control) this.radPageViewPage2);
      this.radPageView1.Controls.Add((Control) this.radPageViewPage3);
      this.radPageView1.Controls.Add((Control) this.radPageViewPage4);
      this.radPageView1.Location = new Point(0, 0);
      this.radPageView1.Name = "radPageView1";
      this.radPageView1.SelectedPage = this.radPageViewPage4;
      this.radPageView1.Size = new Size(433, 300);
      this.radPageView1.TabIndex = 24;
      this.radPageViewPage1.Controls.Add((Control) this.discreteColorHexagon);
      this.radPageViewPage1.ItemSize = new SizeF(41f, 28f);
      this.radPageViewPage1.Location = new Point(10, 37);
      this.radPageViewPage1.Name = "radPageViewPage1";
      this.radPageViewPage1.Size = new Size(412, 252);
      this.radPageViewPage1.Text = "Basic";
      this.discreteColorHexagon.Dock = DockStyle.Fill;
      this.discreteColorHexagon.Location = new Point(0, 0);
      this.discreteColorHexagon.Name = "discreteColorHexagon";
      this.discreteColorHexagon.Size = new Size(412, 252);
      this.discreteColorHexagon.TabIndex = 0;
      this.radPageViewPage2.Controls.Add((Control) this.listBox1);
      this.radPageViewPage2.ItemSize = new SizeF(52f, 28f);
      this.radPageViewPage2.Location = new Point(10, 37);
      this.radPageViewPage2.Name = "radPageViewPage2";
      this.radPageViewPage2.Size = new Size(412, 252);
      this.radPageViewPage2.Text = "System";
      this.listBox1.Dock = DockStyle.Fill;
      this.listBox1.EnableAnalytics = false;
      this.listBox1.ItemHeight = 24;
      this.listBox1.Location = new Point(0, 0);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new Size(412, 252);
      this.listBox1.TabIndex = 0;
      this.radPageViewPage3.Controls.Add((Control) this.listBox2);
      this.radPageViewPage3.ItemSize = new SizeF(39f, 28f);
      this.radPageViewPage3.Location = new Point(10, 37);
      this.radPageViewPage3.Name = "radPageViewPage3";
      this.radPageViewPage3.Size = new Size(412, 252);
      this.radPageViewPage3.Text = "Web";
      this.listBox2.Dock = DockStyle.Fill;
      this.listBox2.EnableAnalytics = false;
      this.listBox2.ItemHeight = 24;
      this.listBox2.Location = new Point(0, 0);
      this.listBox2.Name = "listBox2";
      this.listBox2.Size = new Size(412, 252);
      this.listBox2.TabIndex = 1;
      this.radPageViewPage4.Controls.Add((Control) this.professionalColorsControl);
      this.radPageViewPage4.ItemSize = new SizeF(76f, 28f);
      this.radPageViewPage4.Location = new Point(10, 37);
      this.radPageViewPage4.Name = "radPageViewPage4";
      this.radPageViewPage4.Size = new Size(412, 252);
      this.radPageViewPage4.Text = "Professional";
      this.professionalColorsControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.professionalColorsControl.ColorHsl = HslColor.FromAhsl(0.0, 1.0, 1.0);
      this.professionalColorsControl.ColorRgb = Color.FromArgb((int) byte.MaxValue, 0, 0);
      this.professionalColorsControl.Location = new Point(0, 0);
      this.professionalColorsControl.Name = "professionalColorsControl";
      this.professionalColorsControl.Size = new Size(415, 251);
      this.professionalColorsControl.TabIndex = 0;
      this.radButton3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButton3.EnableAnalytics = false;
      this.radButton3.Location = new Point(431, 333);
      this.radButton3.Name = "radButton3";
      this.radButton3.Size = new Size(75, 23);
      this.radButton3.TabIndex = 23;
      this.radButton3.Text = "Cancel";
      this.radButton3.Click += new EventHandler(this.radButton3_Click);
      this.btnScreenColorPick.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.btnScreenColorPick.DisplayStyle = DisplayStyle.Image;
      this.btnScreenColorPick.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.eyedropper;
      this.btnScreenColorPick.ImageAlignment = ContentAlignment.MiddleCenter;
      this.btnScreenColorPick.Location = new Point(457, 166);
      this.btnScreenColorPick.Name = "btnScreenColorPick";
      this.btnScreenColorPick.Size = new Size(38, 32);
      this.btnScreenColorPick.TabIndex = 22;
      this.btnScreenColorPick.Click += new EventHandler(this.buttonColorPick_Click);
      this.radButton1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.radButton1.EnableAnalytics = false;
      this.radButton1.Location = new Point(350, 333);
      this.radButton1.Name = "radButton1";
      this.radButton1.Size = new Size(75, 23);
      this.radButton1.TabIndex = 21;
      this.radButton1.Text = "OK";
      this.radButton1.Click += new EventHandler(this.radButton1_Click);
      this.captureBox.CapturedColor = Color.Empty;
      this.captureBox.Location = new Point(0, 0);
      this.captureBox.Name = "captureBox";
      this.captureBox.Size = new Size(166, 131);
      this.captureBox.TabIndex = 30;
      this.Controls.Add((Control) this.textBoxColor);
      this.Controls.Add((Control) this.currentLabel);
      this.Controls.Add((Control) this.btnAddNewColor);
      this.Controls.Add((Control) this.radPageView1);
      this.Controls.Add((Control) this.radButton3);
      this.Controls.Add((Control) this.btnScreenColorPick);
      this.Controls.Add((Control) this.radButton1);
      this.Controls.Add((Control) this.hexHeadingLabel);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.newLabel);
      this.Controls.Add((Control) this.captureBox);
      this.Controls.Add((Control) this.customColors);
      this.Name = nameof (RadColorSelector);
      this.Size = new Size(509, 361);
      this.newLabel.EndInit();
      this.labelOldColor.EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.labelColor.EndInit();
      this.hexHeadingLabel.EndInit();
      this.currentLabel.EndInit();
      this.textBoxColor.EndInit();
      this.btnAddNewColor.EndInit();
      this.radPageView1.EndInit();
      this.radPageView1.ResumeLayout(false);
      this.radPageViewPage1.ResumeLayout(false);
      this.radPageViewPage2.ResumeLayout(false);
      this.listBox1.EndInit();
      this.radPageViewPage3.ResumeLayout(false);
      this.listBox2.EndInit();
      this.radPageViewPage4.ResumeLayout(false);
      this.radButton3.EndInit();
      this.btnScreenColorPick.EndInit();
      this.radButton1.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
