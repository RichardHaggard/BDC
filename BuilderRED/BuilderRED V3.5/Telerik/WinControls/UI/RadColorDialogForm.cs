// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorDialogForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadColorDialogForm : RadForm, IRadColorDialog
  {
    private IColorSelector colorSelector;
    private RadThemeComponentBase.ThemeContext context;
    private IContainer components;

    public RadColorDialogForm()
    {
      this.colorSelector = RadColorEditor.CreateColorSelectorInstance();
      this.EnableAnalytics = false;
      this.InitializeComponent();
      this.context = new RadThemeComponentBase.ThemeContext((Control) this);
      this.colorSelector.SelectedColor = Color.Red;
      this.colorSelector.OkButtonClicked += (ColorChangedEventHandler) ((sender, args) =>
      {
        this.DialogResult = DialogResult.OK;
        this.Close();
      });
      this.colorSelector.CancelButtonClicked += (ColorChangedEventHandler) ((sender, args) =>
      {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
      });
      (this.colorSelector as Telerik.WinControls.UI.RadColorSelector).ColorChanged += new ColorChangedEventHandler(this.RadColorDialogForm_ColorChanged);
      this.ThemeNameChanged += new ThemeNameChangedEventHandler(this.RadColorDialogForm_ThemeNameChanged);
    }

    public UserControl RadColorSelector
    {
      get
      {
        return this.colorSelector as UserControl;
      }
    }

    public Color SelectedColor
    {
      get
      {
        return this.colorSelector.SelectedColor;
      }
      set
      {
        this.colorSelector.SelectedColor = value;
      }
    }

    public HslColor SelectedHslColor
    {
      get
      {
        return this.colorSelector.SelectedHslColor;
      }
      set
      {
        this.colorSelector.SelectedHslColor = value;
      }
    }

    public Color OldColor
    {
      get
      {
        return this.colorSelector.OldColor;
      }
      set
      {
        this.colorSelector.OldColor = value;
      }
    }

    public ColorPickerActiveMode ActiveMode
    {
      get
      {
        return this.colorSelector.ActiveMode;
      }
      set
      {
        this.colorSelector.ActiveMode = value;
      }
    }

    public bool ShowBasicColors
    {
      get
      {
        return this.colorSelector.ShowBasicColors;
      }
      set
      {
        this.colorSelector.ShowBasicColors = value;
      }
    }

    public bool ShowSystemColors
    {
      get
      {
        return this.colorSelector.ShowSystemColors;
      }
      set
      {
        this.colorSelector.ShowSystemColors = value;
      }
    }

    public bool ShowWebColors
    {
      get
      {
        return this.colorSelector.ShowWebColors;
      }
      set
      {
        this.colorSelector.ShowWebColors = value;
      }
    }

    public bool ShowProfessionalColors
    {
      get
      {
        return this.colorSelector.ShowProfessionalColors;
      }
      set
      {
        this.colorSelector.ShowProfessionalColors = value;
      }
    }

    public bool ShowCustomColors
    {
      get
      {
        return this.colorSelector.ShowCustomColors;
      }
      set
      {
        this.colorSelector.ShowCustomColors = value;
      }
    }

    public bool ShowHEXColorValue
    {
      get
      {
        return this.colorSelector.ShowHEXColorValue;
      }
      set
      {
        this.colorSelector.ShowHEXColorValue = value;
      }
    }

    public bool AllowEditHEXValue
    {
      get
      {
        return this.colorSelector.AllowEditHEXValue;
      }
      set
      {
        this.colorSelector.AllowEditHEXValue = value;
      }
    }

    public bool AllowColorPickFromScreen
    {
      get
      {
        return this.colorSelector.AllowColorPickFromScreen;
      }
      set
      {
        this.colorSelector.AllowColorPickFromScreen = value;
      }
    }

    public bool AllowColorSaving
    {
      get
      {
        return this.colorSelector.AllowColorSaving;
      }
      set
      {
        this.colorSelector.AllowColorSaving = value;
      }
    }

    public Color[] CustomColors
    {
      get
      {
        return this.colorSelector.CustomColors;
      }
    }

    public string BasicTabHeading
    {
      get
      {
        return this.colorSelector.BasicTabHeading;
      }
      set
      {
        this.colorSelector.BasicTabHeading = value;
      }
    }

    public string SystemTabHeading
    {
      get
      {
        return this.colorSelector.SystemTabHeading;
      }
      set
      {
        this.colorSelector.SystemTabHeading = value;
      }
    }

    public string WebTabHeading
    {
      get
      {
        return this.colorSelector.WebTabHeading;
      }
      set
      {
        this.colorSelector.WebTabHeading = value;
      }
    }

    public string ProfessionalTabHeading
    {
      get
      {
        return this.colorSelector.ProfessionalTabHeading;
      }
      set
      {
        this.colorSelector.ProfessionalTabHeading = value;
      }
    }

    public string SelectedColorLabelHeading
    {
      get
      {
        return this.colorSelector.SelectedColorLabelHeading;
      }
      set
      {
        this.colorSelector.SelectedColorLabelHeading = value;
      }
    }

    public string OldColorLabelHeading
    {
      get
      {
        return this.colorSelector.OldColorLabelHeading;
      }
      set
      {
        this.colorSelector.OldColorLabelHeading = value;
      }
    }

    public event ColorChangedEventHandler ColorChanged;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
        this.context.CorrectPositions();
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      this.SetupControlsForMaterialTheme();
    }

    private void SetupControlsForMaterialTheme()
    {
      if (this.ClientSize.Width == 605 && this.ClientSize.Height == 483)
        return;
      this.context.CorrectPositions();
      Telerik.WinControls.UI.RadColorSelector radColorSelector = this.RadColorSelector as Telerik.WinControls.UI.RadColorSelector;
      if (radColorSelector == null)
        return;
      this.ClientSize = new Size(605, 483);
      Control control1 = radColorSelector.Controls["btnAddNewColor"];
      Control control2 = radColorSelector.Controls["radButton1"];
      Control control3 = radColorSelector.Controls["radButton3"];
      int num1 = 100;
      int num2 = 6;
      int right = control3.Right;
      control1.Width = 160;
      radColorSelector.ControlsHolderPageView.Height = 402;
      Control control4 = radColorSelector.Controls["customColors"];
      control4.Top = radColorSelector.ControlsHolderPageView.Bottom + 1;
      control1.Top = control3.Top = control2.Top = control4.Bottom + 1;
      control3.Width = num1;
      control3.Left = right - num1;
      control2.Width = num1;
      control2.Left = control3.Left - num2 - num1;
      foreach (RowStyle rowStyle in (IEnumerable) (radColorSelector.ControlsHolderPageView.Pages["radPageViewPage4"].Controls["professionalColorsControl"].Controls["tableLayoutPanel1"] as TableLayoutPanel).RowStyles)
      {
        if (rowStyle.SizeType == SizeType.Absolute)
          rowStyle.Height = 40f;
      }
    }

    private void RadColorDialogForm_ThemeNameChanged(object source, ThemeNameChangedEventArgs args)
    {
      ThemeResolutionService.ApplyThemeToControlTree((Control) this, args.newThemeName);
    }

    protected virtual void OnColorChanged(ColorChangedEventArgs e)
    {
      ColorChangedEventHandler colorChanged = this.ColorChanged;
      if (colorChanged != null)
        colorChanged((object) this, e);
      if (!this.EnableAnalytics || string.IsNullOrEmpty(this.Name) || !this.IsHandleCreated)
        return;
      string name = this.Name;
      Color selectedColor = e.SelectedColor;
      string str = e.SelectedColor.ToString();
      ControlTraceMonitor.TrackAtomicFeature(name, "SelectionChanged", (object) str);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Return)
        this.DialogResult = DialogResult.OK;
      return base.ProcessCmdKey(ref msg, keyData);
    }

    private void RadColorDialog_Load(object sender, EventArgs e)
    {
      this.KeyDown += new KeyEventHandler(this.RadColorDialog_KeyDown);
      LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.ColorDialogLocalizationProvider_CurrentProviderChanged);
      this.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogCaption");
    }

    private void ColorDialogLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString("ColorDialogCaption");
    }

    private void RadColorDialogForm_Disposed(object sender, EventArgs e)
    {
      LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.ColorDialogLocalizationProvider_CurrentProviderChanged);
    }

    private void RadColorDialog_KeyDown(object sender, KeyEventArgs e)
    {
      int num = (int) MessageBox.Show(e.KeyCode.ToString());
    }

    private void RadColorDialogForm_ColorChanged(object sender, ColorChangedEventArgs args)
    {
      this.OnColorChanged(args);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.BeginInit();
      this.SuspendLayout();
      UserControl colorSelector = this.colorSelector as UserControl;
      colorSelector.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      colorSelector.Location = new Point(0, 0);
      colorSelector.Name = "RadColorSelector";
      this.colorSelector.OldColor = Color.LightGray;
      this.colorSelector.SelectedColor = Color.Empty;
      this.colorSelector.SelectedHslColor = HslColor.Empty;
      colorSelector.Size = new Size(547, 411);
      colorSelector.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(558, 416);
      this.Controls.Add((Control) colorSelector);
      this.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (RadColorDialogForm);
      this.RootElement.ApplyShapeToControl = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Color dialog";
      this.Load += new EventHandler(this.RadColorDialog_Load);
      this.Disposed += new EventHandler(this.RadColorDialogForm_Disposed);
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
