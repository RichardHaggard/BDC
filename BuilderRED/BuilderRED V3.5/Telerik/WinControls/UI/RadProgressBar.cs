// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadProgressBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [Description("Displays a bar that fills to display progress information during a long-running operation.")]
  [DefaultProperty("Value")]
  [DefaultEvent("Click")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadProgressBar : RadControl
  {
    private RadProgressBarElement progressBarElement;

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.progressBarElement = this.CreateProgressBarElement();
      parent.Children.Add((RadElement) this.progressBarElement);
    }

    protected virtual RadProgressBarElement CreateProgressBarElement()
    {
      return new RadProgressBarElement();
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(130, 24));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Category("Layout")]
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

    [Localizable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the displayed text.")]
    [RadDefaultValue("Text", typeof (RadProgressBarElement))]
    [Bindable(true)]
    public override string Text
    {
      get
      {
        return this.progressBarElement.TextElement.Text;
      }
      set
      {
        this.progressBarElement.Text = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.TextChanged));
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets the background image of the RadProgressBar.")]
    [Category("Appearance")]
    public override Image BackgroundImage
    {
      get
      {
        return this.progressBarElement.BackgroundImage;
      }
      set
      {
        this.progressBarElement.BackgroundImage = value;
      }
    }

    [Description("Gets or sets the layout of the background image of the RadProgressBar.")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(ImageLayout.Center)]
    [Category("Appearance")]
    public override ImageLayout BackgroundImageLayout
    {
      get
      {
        return this.progressBarElement.BackgroundImageLayout;
      }
      set
      {
        this.progressBarElement.BackgroundImageLayout = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadProgressBarElement ProgressBarElement
    {
      get
      {
        return this.progressBarElement;
      }
    }

    [Category("Appearance")]
    [Description("Current Value of the progress in the range between Minimum and Maximum.")]
    [DefaultValue(0)]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("Value1", typeof (RadProgressBarElement))]
    public int Value1
    {
      get
      {
        return this.progressBarElement.Value1;
      }
      set
      {
        this.progressBarElement.Value1 = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.ValueChanged));
      }
    }

    [Description("Current Value of the progress in the range between Minimum and Maximum.")]
    [DefaultValue(0)]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("Value2", typeof (RadProgressBarElement))]
    public int Value2
    {
      get
      {
        return this.progressBarElement.Value2;
      }
      set
      {
        this.progressBarElement.Value2 = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.ValueChanged));
      }
    }

    [DefaultValue(0)]
    [Description("The lower bound of the range this ProgressBar is working with.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("Minimum", typeof (RadProgressBarElement))]
    public int Minimum
    {
      get
      {
        return this.progressBarElement.Minimum;
      }
      set
      {
        this.progressBarElement.Minimum = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.MinimumChanged));
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Category("Appearance")]
    [Description("The upper bound of the range this ProgressBar is working with")]
    [RadPropertyDefaultValue("Maximum", typeof (RadProgressBarElement))]
    [DefaultValue(100)]
    public int Maximum
    {
      get
      {
        return this.progressBarElement.Maximum;
      }
      set
      {
        this.progressBarElement.Maximum = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.MaximumChanged));
      }
    }

    [Description("The amount to increment the current value.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("Step", typeof (RadProgressBarElement))]
    [DefaultValue(10)]
    public int Step
    {
      get
      {
        return this.progressBarElement.Step;
      }
      set
      {
        this.progressBarElement.Step = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.StepChanged));
      }
    }

    [RadPropertyDefaultValue("StepWidth", typeof (RadProgressBarElement))]
    [Description("Gets or sets the StepWidth between different separators.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(14)]
    public int StepWidth
    {
      get
      {
        return this.progressBarElement.StepWidth;
      }
      set
      {
        this.progressBarElement.StepWidth = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.StepWidthChanged));
      }
    }

    [Description("Gets or Sets the style of the ProgressBar to dash.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("Dash", typeof (RadProgressBarElement))]
    [DefaultValue(false)]
    public bool Dash
    {
      get
      {
        return this.progressBarElement.Dash;
      }
      set
      {
        this.progressBarElement.Dash = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.DashChanged));
      }
    }

    [Category("Appearance")]
    [Description("Gets or Sets the style of the ProgressBar to hatch.")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("Dash", typeof (RadProgressBarElement))]
    [DefaultValue(false)]
    public bool Hatch
    {
      get
      {
        return this.progressBarElement.Hatch;
      }
      set
      {
        this.progressBarElement.Hatch = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.HatchChanged));
      }
    }

    [Category("Appearance")]
    [Description("When style is dash indicates if the progress indicators will progress on steps or smoothly.")]
    [DefaultValue(false)]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("IntegralDash", typeof (RadProgressBarElement))]
    public bool IntegralDash
    {
      get
      {
        return this.progressBarElement.IntegralDash;
      }
      set
      {
        this.progressBarElement.IntegralDash = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.IntegralDashChanged));
      }
    }

    [RadPropertyDefaultValue("SeparatorColor1", typeof (SeparatorsElement))]
    [Description("Gets or Sets the first gradient color for separators")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    public Color SeparatorColor1
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SeparatorColor1;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.SeparatorColor1 = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorColorChanged));
      }
    }

    [Description("Gets or Sets the second gradient color for separators.")]
    [RadPropertyDefaultValue("SeparatorColor2", typeof (SeparatorsElement))]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    public Color SeparatorColor2
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SeparatorColor2;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.SeparatorColor2 = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorColorChanged));
      }
    }

    [Category("Appearance")]
    [Description("Gets or Sets the third gradient color for separators.")]
    [RadPropertyDefaultValue("SeparatorColor3", typeof (SeparatorsElement))]
    [RefreshProperties(RefreshProperties.All)]
    public Color SeparatorColor3
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SeparatorColor3;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.SeparatorColor3 = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorColorChanged));
      }
    }

    [Description("Gets or Sets the fourth gradient color for separators.")]
    [RadPropertyDefaultValue("SeparatorColor4", typeof (SeparatorsElement))]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    public Color SeparatorColor4
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SeparatorColor4;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.SeparatorColor4 = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorColorChanged));
      }
    }

    [RadPropertyDefaultValue("SeparatorGradientAngle", typeof (RadProgressBarElement))]
    [Description("Gets or Sets the angle of the separators gradient.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(0)]
    public int SeparatorGradientAngle
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SeparatorGradientAngle;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.SeparatorGradientAngle = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorGradientAngleChanged));
      }
    }

    [DefaultValue(0.4f)]
    [Description("Gets or sets the first color stop in the separator gradient.")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("SeparatorGradientPercentage1", typeof (RadProgressBarElement))]
    public float SeparatorGradientPercentage1
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SeparatorGradientPercentage1;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.SeparatorGradientPercentage1 = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorColorStopChanged));
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the second color stop in the separator gradient.")]
    [DefaultValue(0.6f)]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("SeparatorGradientPercentage2", typeof (RadProgressBarElement))]
    public float SeparatorGradientPercentage2
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SeparatorGradientPercentage2;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.SeparatorGradientPercentage2 = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorColorStopChanged));
      }
    }

    [Description("Gets or sets the number of colors used in the separator gradient.")]
    [DefaultValue(2)]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("SeparatorGradientPercentage2", typeof (RadProgressBarElement))]
    public int SeparatorNumberOfColors
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.NumberOfColors;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.NumberOfColors = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorNumberOfColorChanged));
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or Sets the width of separators")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("SeparatorsWidth", typeof (RadProgressBarElement))]
    [DefaultValue(3)]
    public int SeparatorWidth
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SeparatorWidth;
      }
      set
      {
        this.progressBarElement.SeparatorsElement.SeparatorWidth = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.SeparatorWidthChanged));
      }
    }

    [RadDescription("Image", typeof (RadProgressBarElement))]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    public Image Image
    {
      get
      {
        return this.progressBarElement.Image;
      }
      set
      {
        this.progressBarElement.Image = value;
      }
    }

    private bool ShouldSerializeImage()
    {
      if (this.Image != null && this.ImageList == null)
        return this.progressBarElement.GetValueSource(LightVisualElement.ImageProperty) != ValueSource.Style;
      return false;
    }

    [RadDefaultValue("BackgroundImageLayout", typeof (LightVisualElement))]
    [Category("Appearance")]
    [RadDescription("ImageLayout", typeof (RadProgressBarElement))]
    [RefreshProperties(RefreshProperties.All)]
    public ImageLayout ImageLayout
    {
      get
      {
        return this.progressBarElement.IndicatorElement1.BackgroundImageLayout;
      }
      set
      {
        this.progressBarElement.IndicatorElement1.BackgroundImageLayout = value;
      }
    }

    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RadDefaultValue("ImageIndex", typeof (LightVisualElement))]
    [Category("Appearance")]
    [RadDescription("ImageIndex", typeof (RadProgressBarElement))]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public int ImageIndex
    {
      get
      {
        return this.progressBarElement.ImageIndex;
      }
      set
      {
        this.progressBarElement.ImageIndex = value;
      }
    }

    [RelatedImageList("ImageList")]
    [RadDefaultValue("ImageKey", typeof (LightVisualElement))]
    [Category("Appearance")]
    [RadDescription("ImageKey", typeof (RadProgressBarElement))]
    [RefreshProperties(RefreshProperties.All)]
    [Localizable(true)]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public string ImageKey
    {
      get
      {
        return this.progressBarElement.ImageKey;
      }
      set
      {
        this.progressBarElement.ImageKey = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Description("Gets or sets the alignment of the image of the progress line.")]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.progressBarElement.ImageAlignment;
      }
      set
      {
        this.progressBarElement.ImageAlignment = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the text orientation in the progress bar.")]
    [RadPropertyDefaultValue("TextOrientation", typeof (RadProgressBarElement))]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(Orientation.Horizontal)]
    public Orientation TextOrientation
    {
      get
      {
        return this.progressBarElement.TextOrientation;
      }
      set
      {
        this.progressBarElement.TextOrientation = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.TextOrientationChanged));
      }
    }

    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Category("Appearance")]
    [Description("Gets or sets the alignment of text content on the drawing surface.")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.progressBarElement.TextAlignment;
      }
      set
      {
        this.progressBarElement.TextAlignment = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.TextAlignmentChanged));
      }
    }

    [Description("Gets or Sets the progress orientation of progress bar")]
    [DefaultValue(ProgressOrientation.Left)]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("ProgressOrientation", typeof (RadProgressBarElement))]
    public ProgressOrientation ProgressOrientation
    {
      get
      {
        return this.progressBarElement.ProgressOrientation;
      }
      set
      {
        this.progressBarElement.ProgressOrientation = value;
        this.progressBarElement.SeparatorsElement.ProgressOrientation = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.ProgressOrientationChanged));
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets whether progress should show percents")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("ShowProgressIndicator", typeof (RadProgressBarElement))]
    public bool ShowProgressIndicators
    {
      get
      {
        return this.progressBarElement.ShowProgressIndicators;
      }
      set
      {
        this.progressBarElement.ShowProgressIndicators = value;
        this.OnPropertyChanged(new ProgressBarEventArgs(ProgressBarSenderEvent.ShowProgressIndicatorsChanged));
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Category("Appearance")]
    [Description("Gets or sets the angle at which the dash or hatch lines are tilted.")]
    [RadPropertyDefaultValue("SweepAngle", typeof (RadProgressBarElement))]
    [DefaultValue(90)]
    public int SweepAngle
    {
      get
      {
        return this.progressBarElement.SeparatorsElement.SweepAngle;
      }
      set
      {
        if (value % 180 == 0)
          throw new ArgumentException("Sweep angle cannot be equal to kπ, where k=(0,1,2...).");
        this.progressBarElement.SeparatorsElement.SweepAngle = value;
      }
    }

    public event RadProgressBar.ProgressBarHandler ValueChanged;

    public event RadProgressBar.ProgressBarHandler StepChanged;

    public event RadProgressBar.ProgressBarHandler StepWidthChanged;

    public event RadProgressBar.ProgressBarHandler SeparatorWidthChanged;

    public event RadProgressBar.ProgressBarHandler MinimumChanged;

    public event RadProgressBar.ProgressBarHandler MaximumChanged;

    public event RadProgressBar.ProgressBarHandler DashChanged;

    public event RadProgressBar.ProgressBarHandler HatchChanged;

    public event RadProgressBar.ProgressBarHandler IntegralDashChanged;

    public event RadProgressBar.ProgressBarHandler TextOrientationChanged;

    public event RadProgressBar.ProgressBarHandler TextAlignmentChanged;

    public event RadProgressBar.ProgressBarHandler ProgressOrientationChanged;

    public event RadProgressBar.ProgressBarHandler ShowProgressIndicatorsChanged;

    public event RadProgressBar.ProgressBarHandler SeparatorColorChanged;

    protected virtual void OnPropertyChanged(ProgressBarEventArgs e)
    {
      switch (e.SenderEvent)
      {
        case ProgressBarSenderEvent.ValueChanged:
          if (this.ValueChanged == null)
            break;
          this.ValueChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.MinimumChanged:
          if (this.MinimumChanged == null)
            break;
          this.MinimumChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.MaximumChanged:
          if (this.MaximumChanged == null)
            break;
          this.MaximumChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.StepChanged:
          if (this.StepChanged == null)
            break;
          this.StepChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.StepWidthChanged:
          if (this.StepWidthChanged == null)
            break;
          this.StepWidthChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.DashChanged:
          if (this.DashChanged == null)
            break;
          this.DashChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.HatchChanged:
          if (this.HatchChanged == null)
            break;
          this.HatchChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.IntegralDashChanged:
          if (this.IntegralDashChanged == null)
            break;
          this.IntegralDashChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.TextChanged:
          this.OnTextChanged((EventArgs) e);
          break;
        case ProgressBarSenderEvent.SeparatorWidthChanged:
          if (this.SeparatorWidthChanged == null)
            break;
          this.SeparatorWidthChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.TextOrientationChanged:
          if (this.TextOrientationChanged == null)
            break;
          this.TextOrientationChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.TextAlignmentChanged:
          if (this.TextAlignmentChanged == null)
            break;
          this.TextAlignmentChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.ProgressOrientationChanged:
          if (this.ProgressOrientationChanged == null)
            break;
          this.ProgressOrientationChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.ShowProgressIndicatorsChanged:
          if (this.ShowProgressIndicatorsChanged == null)
            break;
          this.ShowProgressIndicatorsChanged((object) this, e);
          break;
        case ProgressBarSenderEvent.SeparatorColorChanged:
          if (this.SeparatorColorChanged == null)
            break;
          this.SeparatorColorChanged((object) this, e);
          break;
      }
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.ProgressBarElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ProgressBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.ProgressBarElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.ProgressBarElement.SetThemeValueOverride(LightVisualElement.GradientStyleProperty, (object) GradientStyles.Solid, state);
      }
      this.ProgressBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.ProgressBarElement.SuspendApplyOfThemeSettings();
      this.ProgressBarElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.ProgressBarElement.ResetThemeValueOverride(LightVisualElement.GradientStyleProperty);
      this.ProgressBarElement.ElementTree.ApplyThemeToElementTree();
      this.ProgressBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.ProgressBarElement.SuspendApplyOfThemeSettings();
      this.ProgressBarElement.TextElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ProgressBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.ProgressBarElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.ProgressBarElement.TextElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      }
      this.ProgressBarElement.ResumeApplyOfThemeSettings();
      this.ProgressBarElement.TextElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.ProgressBarElement.SuspendApplyOfThemeSettings();
      this.ProgressBarElement.TextElement.SuspendApplyOfThemeSettings();
      this.ProgressBarElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.ProgressBarElement.TextElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num = (int) this.ProgressBarElement.TextElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.ProgressBarElement.ElementTree.ApplyThemeToElementTree();
      this.ProgressBarElement.ResumeApplyOfThemeSettings();
      this.ProgressBarElement.TextElement.ResumeApplyOfThemeSettings();
    }

    public delegate void ProgressBarHandler(object sender, ProgressBarEventArgs e);
  }
}
