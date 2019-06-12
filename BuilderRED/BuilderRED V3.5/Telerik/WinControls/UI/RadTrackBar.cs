// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTrackBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Editors")]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadTrackBarDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("A slider control that enables the user to select a value on a bar by moving a slider")]
  [DefaultProperty("Value")]
  [DefaultEvent("ValueChanged")]
  public class RadTrackBar : RadEditorControl
  {
    private Dictionary<string, object> initValues = new Dictionary<string, object>();
    private RadTrackBarElement trackBarElement;

    public RadTrackBar()
    {
      this.AutoSize = true;
      this.WireEvents();
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected override void InitializeRootElement(RootRadElement rootElement)
    {
      this.RootElement.StretchVertically = true;
      base.InitializeRootElement(rootElement);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.trackBarElement = this.CreateTrackBarElement();
      this.RootElement.Children.Add((RadElement) this.trackBarElement);
      base.CreateChildItems((RadElement) this.trackBarElement);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.TrackBarElement.Focus();
    }

    protected virtual RadTrackBarElement CreateTrackBarElement()
    {
      return new RadTrackBarElement() { TickStyle = TickStyles.Both };
    }

    protected virtual void WireEvents()
    {
      this.Initialized += new EventHandler(this.RadTrackBar_Initialized);
      if (this.trackBarElement == null)
        return;
      this.trackBarElement.Scroll += new ScrollEventHandler(this.trackBarElement_Scroll);
      this.trackBarElement.TickFormatting += new TickFormattingEventHandler(this.trackBarElement_TickFormatting);
      this.trackBarElement.LabelFormatting += new LabelFormattingEventHandler(this.trackBarElement_LabelFormatting);
      this.trackBarElement.ValueChanged += new EventHandler(this.trackBarElement_ValueChanged);
    }

    protected virtual void UnwireEvents()
    {
      if (this.trackBarElement == null)
        return;
      this.trackBarElement.Scroll -= new ScrollEventHandler(this.trackBarElement_Scroll);
      this.trackBarElement.TickFormatting -= new TickFormattingEventHandler(this.trackBarElement_TickFormatting);
      this.trackBarElement.LabelFormatting -= new LabelFormattingEventHandler(this.trackBarElement_LabelFormatting);
      this.trackBarElement.ValueChanged -= new EventHandler(this.trackBarElement_ValueChanged);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
    [Description("Gets or sets whether the edit control is auto-sized")]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
        this.trackBarElement.AutoSizeCore = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(150, 40));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadTrackBarElement TrackBarElement
    {
      get
      {
        return this.trackBarElement;
      }
    }

    [Description("Gets or sets a minimum value for the trackbar position")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(0.0f)]
    public float Minimum
    {
      get
      {
        return this.trackBarElement.Minimum;
      }
      set
      {
        this.trackBarElement.Minimum = value;
        if (!this.IsInitializing || !this.initValues.ContainsKey("Maximum"))
          return;
        this.trackBarElement.Maximum = (float) this.initValues["Maximum"];
        this.initValues.Remove("Maximum");
      }
    }

    [DefaultValue(20f)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a maximum value for the trackbar position")]
    public float Maximum
    {
      get
      {
        return this.trackBarElement.Maximum;
      }
      set
      {
        if (this.IsInitializing)
          this.initValues[nameof (Maximum)] = (object) value;
        else
          this.trackBarElement.Maximum = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(0.0f)]
    [Description("Gets or Sets TrackBar's value")]
    public float Value
    {
      get
      {
        return this.trackBarElement.Value;
      }
      set
      {
        if (this.IsInitializing)
        {
          this.initValues[nameof (Value)] = (object) value;
        }
        else
        {
          this.trackBarElement.Value = value;
          this.OnNotifyPropertyChanged(nameof (Value));
        }
      }
    }

    [Browsable(true)]
    [DefaultValue(TickStyles.Both)]
    [Category("Behavior")]
    [Description("Gets or Sets whether the TrackBar's ticks should be drawn")]
    public TickStyles TickStyle
    {
      get
      {
        return this.trackBarElement.TickStyle;
      }
      set
      {
        this.trackBarElement.TickStyle = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(Orientation.Horizontal)]
    [Browsable(true)]
    [Description("Gets or Sets the orientation of the TrackBar")]
    public Orientation Orientation
    {
      get
      {
        return this.trackBarElement.Orientation;
      }
      set
      {
        if (value == this.trackBarElement.Orientation)
          return;
        if (!this.AutoSize || (double) this.trackBarElement.DesiredSize.Width == 0.0 || (double) this.trackBarElement.DesiredSize.Height == 0.0)
        {
          this.trackBarElement.Orientation = value;
          int width = this.Width;
          this.Width = this.Height;
          this.Height = width;
          this.SetRootElementStretchStyle(this.AutoSize);
        }
        else
        {
          this.trackBarElement.SuspendLayout();
          this.trackBarElement.Orientation = value;
          int width = (int) this.trackBarElement.DesiredSize.Width;
          int height = (int) this.trackBarElement.DesiredSize.Height;
          if (this.RootElement.StretchHorizontally)
            width = this.Width;
          if (this.RootElement.StretchVertically)
            height = this.Height;
          this.SetRootElementStretchStyle(this.AutoSize);
          this.Width = height;
          this.Height = width;
          this.trackBarElement.ResumeLayout(true);
        }
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (Orientation)));
      }
    }

    [DefaultValue(0)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or Sets the number of positions that the trackbar moves in response to mouse clicks.")]
    public int LargeChange
    {
      get
      {
        return this.trackBarElement.LargeChange;
      }
      set
      {
        this.trackBarElement.LargeChange = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(1)]
    [Description("Gets or Sets the number of positions that the trackbar moves in response to keyboard arrow keys and the trackbar buttons.")]
    public int SmallChange
    {
      get
      {
        return this.trackBarElement.SmallChange;
      }
      set
      {
        this.trackBarElement.SmallChange = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(Orientation.Horizontal)]
    [Category("Behavior")]
    [Description("Gets or Sets the orientation of the text associated with TrackBar. Whether it should appear horizontal or vertical.")]
    public Orientation TextOrientation
    {
      get
      {
        return this.trackBarElement.TextOrientation;
      }
      set
      {
        this.trackBarElement.TextOrientation = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(TrackBarLabelStyle.None)]
    [Category("Behavior")]
    [Description("Gets or Sets whether the TrackBar's labels should be drawn")]
    public TrackBarLabelStyle LabelStyle
    {
      get
      {
        return this.trackBarElement.LabelStyle;
      }
      set
      {
        this.trackBarElement.LabelStyle = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or Sets whether the TrackBar's handles should be drawn")]
    public bool ShowButtons
    {
      get
      {
        return this.trackBarElement.ShowButtons;
      }
      set
      {
        this.trackBarElement.ShowButtons = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(5)]
    [Category("Behavior")]
    [Description("Gets or Sets the number of positions between large tick marks")]
    public int LargeTickFrequency
    {
      get
      {
        return this.trackBarElement.LargeTickFrequency;
      }
      set
      {
        this.trackBarElement.LargeTickFrequency = value;
      }
    }

    [Browsable(true)]
    [Description(" Gets or Sets the number of positions between small tick marks")]
    [Category("Behavior")]
    [DefaultValue(1)]
    public int SmallTickFrequency
    {
      get
      {
        return this.trackBarElement.SmallTickFrequency;
      }
      set
      {
        this.trackBarElement.SmallTickFrequency = value;
      }
    }

    [Description("Gets or Sets the Mode of the TrackBa")]
    [Browsable(true)]
    [DefaultValue(TrackBarRangeMode.SingleThumb)]
    public TrackBarRangeMode TrackBarMode
    {
      get
      {
        return this.trackBarElement.TrackBarMode;
      }
      set
      {
        this.trackBarElement.TrackBarMode = value;
      }
    }

    [Description(" Gets the Range collection. ")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public TrackBarRangeCollection Ranges
    {
      get
      {
        return this.trackBarElement.Ranges;
      }
    }

    [Browsable(true)]
    [Description(" Gets or Sets the Snap mode of the TrackBar")]
    [DefaultValue(TrackBarSnapModes.SnapToTicks)]
    public TrackBarSnapModes SnapMode
    {
      get
      {
        return this.TrackBarElement.SnapMode;
      }
      set
      {
        this.TrackBarElement.SnapMode = value;
      }
    }

    [Description("Gets or Sets TrackBar's Size")]
    [Browsable(true)]
    [DefaultValue(typeof (Size), "10,14")]
    public Size ThumbSize
    {
      get
      {
        return this.trackBarElement.ThumbSize;
      }
      set
      {
        this.trackBarElement.ThumbSize = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or Sets whether the SlideArea should be visible")]
    [Category("Appearance")]
    [DefaultValue(true)]
    public bool ShowSlideArea
    {
      get
      {
        return this.trackBarElement.ShowSlideArea;
      }
      set
      {
        this.trackBarElement.ShowSlideArea = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Gets or Sets whether the selected thumb should move on arrow key press.")]
    public bool AllowKeyNavigation
    {
      get
      {
        return this.trackBarElement.AllowKeyNavigation;
      }
      set
      {
        this.trackBarElement.AllowKeyNavigation = value;
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

    [Category("Behavior")]
    [Description("Occurs when the value of the controls changes.")]
    public event EventHandler ValueChanged;

    [Category("Behavior")]
    [Description("Occurs when a Label needs to be formatted.")]
    public event LabelFormattingEventHandler LabelFormatting;

    [Description("Occurs when a Tick needs to be formatted.")]
    [Category("Behavior")]
    public event TickFormattingEventHandler TickFormatting;

    protected override void ProcessAutoSizeChanged(bool value)
    {
      this.SetRootElementStretchStyle(value);
      this.RootElement.SaveCurrentStretchModeAsDefault();
    }

    protected virtual void OnTickFormatting(TickFormattingEventArgs e)
    {
      TickFormattingEventHandler tickFormatting = this.TickFormatting;
      if (tickFormatting == null)
        return;
      tickFormatting((object) this, e);
    }

    protected virtual void OnLabelFormatting(LabelFormattingEventArgs e)
    {
      LabelFormattingEventHandler labelFormatting = this.LabelFormatting;
      if (labelFormatting == null)
        return;
      labelFormatting((object) this, e);
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
      EventHandler valueChanged = this.ValueChanged;
      if (valueChanged == null)
        return;
      valueChanged((object) this, e);
    }

    private void RadTrackBar_Initialized(object sender, EventArgs e)
    {
      this.Initialized -= new EventHandler(this.RadTrackBar_Initialized);
      if (this.initValues.ContainsKey("Minimum"))
        this.Minimum = (float) this.initValues["Minimum"];
      if (this.initValues.ContainsKey("Maximum"))
        this.Maximum = (float) this.initValues["Maximum"];
      if (this.initValues.ContainsKey("Value"))
        this.Value = (float) this.initValues["Value"];
      this.initValues.Clear();
    }

    private void trackBarElement_Scroll(object sender, ScrollEventArgs e)
    {
      this.OnScroll(e);
    }

    private void trackBarElement_TickFormatting(object sender, TickFormattingEventArgs e)
    {
      this.OnTickFormatting(e);
    }

    private void trackBarElement_LabelFormatting(object sender, LabelFormattingEventArgs e)
    {
      this.OnLabelFormatting(e);
    }

    private void trackBarElement_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged(e);
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (this.AllowKeyNavigation && this.ContainsFocus && (keyData == Keys.End || keyData == Keys.Prior || (keyData == Keys.Next || keyData == Keys.Home) || (keyData == Keys.Left || keyData == Keys.Right || (keyData == Keys.Up || keyData == Keys.Down))))
        return false;
      return base.ProcessDialogKey(keyData);
    }

    private void SetRootElementStretchStyle(bool autoSize)
    {
      bool flag = this.Orientation == Orientation.Horizontal;
      if (autoSize)
      {
        this.RootElement.StretchHorizontally = flag;
        this.RootElement.StretchVertically = !flag;
      }
      else
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = true;
      }
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.TrackBarElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TrackBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.TrackBarElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.TrackBarElement.SetThemeValueOverride(LightVisualElement.GradientStyleProperty, (object) GradientStyles.Solid, state);
      }
      this.TrackBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.TrackBarElement.SuspendApplyOfThemeSettings();
      this.TrackBarElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.TrackBarElement.ResetThemeValueOverride(LightVisualElement.GradientStyleProperty);
      this.TrackBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.TrackBarElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TrackBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.TrackBarElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.TrackBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.TrackBarElement.SuspendApplyOfThemeSettings();
      this.TrackBarElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.TrackBarElement.ElementTree.ApplyThemeToElementTree();
      this.TrackBarElement.ResumeApplyOfThemeSettings();
    }
  }
}
