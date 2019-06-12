// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadWaitingBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadWaitingBarDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(true)]
  [Description("The component indicates that a long-running operation is underway")]
  [DefaultProperty("WaitingSpeed")]
  [DefaultEvent("WaitingStarted")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadWaitingBar : RadControl
  {
    private readonly IList<string> notSupportedAssociatedControlTypes = (IList<string>) new List<string>() { "RadPageView", "RadDock", "RadCollapsiblePanel", "RadSplitContainer", "RadDataEntry", "RadDataLayout" };
    private RadWaitingBarElement waitingBarElement;
    private Control associatedControl;
    private RadPanel associatedControlCover;
    private Control oldParent;

    protected virtual RadWaitingBarElement CreateWaitingBarElement()
    {
      return new RadWaitingBarElement();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.waitingBarElement = this.CreateWaitingBarElement();
      this.RootElement.Children.Add((RadElement) this.waitingBarElement);
      this.WireEvents();
    }

    protected override void Dispose(bool disposing)
    {
      if (this.IsDisposed)
        return;
      this.AssociatedControl = (Control) null;
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [Browsable(false)]
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

    [Description("Gets a collection of BaseWaitingBarIndicatorElement elements which contains all waiting indicators of RadWaitingBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public WaitingBarIndicatorCollection WaitingIndicators
    {
      get
      {
        return this.WaitingBarElement.WaitingIndicators;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    [Description("Gets the instance of RadWaitingBarElement wrapped by this control")]
    public RadWaitingBarElement WaitingBarElement
    {
      get
      {
        return this.waitingBarElement;
      }
    }

    public bool ShouldSerializeWaitingBarElement()
    {
      return this.WaitingStyle == WaitingBarStyles.Dash;
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(130, 24));
      }
    }

    [Browsable(true)]
    [DefaultValue(null)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets and sets the image property of the indicator")]
    public Image Image
    {
      get
      {
        return this.WaitingBarElement.IndicatorImage;
      }
      set
      {
        this.WaitingBarElement.IndicatorImage = value;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Browsable(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets and sets the image index property of the indicator")]
    [DefaultValue(-1)]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public int ImageIndex
    {
      get
      {
        return this.WaitingBarElement.IndicatorImageIndex;
      }
      set
      {
        this.WaitingBarElement.IndicatorImageIndex = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Localizable(true)]
    [RelatedImageList("ImageList")]
    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets and sets the image key property of the indicator")]
    [DefaultValue("")]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public string ImageKey
    {
      get
      {
        return this.WaitingBarElement.IndicatorImageKey;
      }
      set
      {
        this.WaitingBarElement.IndicatorImageKey = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Indicates whether the control is currently waiting")]
    public bool IsWaiting
    {
      get
      {
        return this.waitingBarElement.IsWaiting;
      }
    }

    [Browsable(true)]
    [DefaultValue(Orientation.Horizontal)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Indicates the orientation of the RadWaitingBar")]
    public Orientation Orientation
    {
      get
      {
        return this.WaitingBarElement.WaitingBarOrientation;
      }
      set
      {
        if (this.WaitingBarElement.WaitingBarOrientation == value)
          return;
        this.WaitingBarElement.WaitingBarOrientation = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("WaitingDirection"));
      }
    }

    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Indicates whether the indicators are stretched horizontally")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool StretchIndicatorsHorizontally
    {
      get
      {
        return this.WaitingBarElement.StretchIndicatorsHorizontally;
      }
      set
      {
        this.WaitingBarElement.StretchIndicatorsHorizontally = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Indicates whether the indicators are stretched vertically")]
    public bool StretchIndicatorsVertically
    {
      get
      {
        return this.WaitingBarElement.StretchIndicatorsVertically;
      }
      set
      {
        this.WaitingBarElement.StretchIndicatorsVertically = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Sets the style of RadWaitingBar")]
    [DefaultValue(WaitingBarStyles.Indeterminate)]
    public WaitingBarStyles WaitingStyle
    {
      get
      {
        return this.WaitingBarElement.WaitingStyle;
      }
      set
      {
        if (this.WaitingBarElement.WaitingStyle == value)
          return;
        this.WaitingBarElement.WaitingStyle = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (WaitingStyle)));
      }
    }

    [Category("Appearance")]
    [Description("Gets and sets the text of the control's textElement")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
      get
      {
        return this.waitingBarElement.TextElement.Text;
      }
      set
      {
        this.waitingBarElement.TextElement.Text = value;
      }
    }

    [DefaultValue(ProgressOrientation.Right)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets and sets the WaitingDirection of the RadWaitingBarElement")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ProgressOrientation WaitingDirection
    {
      get
      {
        return this.WaitingBarElement.WaitingDirection;
      }
      set
      {
        if (this.WaitingBarElement.WaitingDirection == value)
          return;
        this.WaitingBarElement.WaitingDirection = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (WaitingDirection)));
      }
    }

    [Description("Gets and sets the size of the indicator in pixels")]
    [Browsable(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof (Size), "50, 14")]
    public Size WaitingIndicatorSize
    {
      get
      {
        return this.waitingBarElement.WaitingIndicatorSize;
      }
      set
      {
        this.waitingBarElement.WaitingIndicatorSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets and sets the speed of the animation. Higher value moves the indicator more quickly across the bar")]
    [DefaultValue(90)]
    public int WaitingSpeed
    {
      get
      {
        return this.waitingBarElement.WaitingSpeed;
      }
      set
      {
        if (this.WaitingSpeed == value)
          return;
        this.waitingBarElement.WaitingSpeed = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (WaitingSpeed)));
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets and sets the number of pixels the indicator moves each step")]
    [DefaultValue(1)]
    public int WaitingStep
    {
      get
      {
        return this.waitingBarElement.WaitingStep;
      }
      set
      {
        if (this.WaitingStep == value)
          return;
        this.waitingBarElement.WaitingStep = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (WaitingStep)));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [Description("Shows text in RadWaitingBar.")]
    [DefaultValue(false)]
    public bool ShowText
    {
      get
      {
        return this.WaitingBarElement.ShowText;
      }
      set
      {
        this.WaitingBarElement.ShowText = value;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.WaitingBarAssociatedControlEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DefaultValue(null)]
    public Control AssociatedControl
    {
      get
      {
        return this.associatedControl;
      }
      set
      {
        if (value != null)
        {
          string name = value.GetType().Name;
          if (this.notSupportedAssociatedControlTypes.Contains(name))
            throw new NotSupportedException(string.Format("{0} cannot be set as the AssociatedControl of RadWaitingBar.\nInstead add the {0} to a Panel and set the latter as the AssociatedControl.", (object) name));
        }
        this.SetAssociatedControl(value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadPanel AssociatedControlCoverPanel
    {
      get
      {
        return this.associatedControlCover;
      }
    }

    [Description("The method starts control's waiting animation")]
    public void StartWaiting()
    {
      this.WaitingBarElement.StartWaiting();
      this.ShowCover();
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, nameof (StartWaiting));
    }

    [Description("The method stops control's waiting animation")]
    public void StopWaiting()
    {
      this.WaitingBarElement.StopWaiting();
      this.HideCover();
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "EndWaiting");
    }

    [Description("The method sets the indicator to its initial position")]
    public void ResetWaiting()
    {
      this.WaitingBarElement.ResetWaiting();
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.SetAssociatedControl(this.AssociatedControl);
    }

    protected override void OnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      base.OnToolTipTextNeeded((object) this.WaitingBarElement, e);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.WaitingBarElement.SuspendApplyOfThemeSettings();
      this.WaitingBarElement.ContentElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.WaitingBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.WaitingBarElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.WaitingBarElement.SetThemeValueOverride(LightVisualElement.GradientStyleProperty, (object) GradientStyles.Solid, state);
        this.WaitingBarElement.ContentElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.WaitingBarElement.ContentElement.SetThemeValueOverride(LightVisualElement.GradientStyleProperty, (object) GradientStyles.Solid, state);
      }
      this.WaitingBarElement.ResumeApplyOfThemeSettings();
      this.WaitingBarElement.ContentElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.WaitingBarElement.SuspendApplyOfThemeSettings();
      this.WaitingBarElement.ContentElement.SuspendApplyOfThemeSettings();
      this.WaitingBarElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.WaitingBarElement.ResetThemeValueOverride(LightVisualElement.GradientStyleProperty);
      this.WaitingBarElement.ContentElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.WaitingBarElement.ContentElement.ResetThemeValueOverride(LightVisualElement.GradientStyleProperty);
      this.WaitingBarElement.ElementTree.ApplyThemeToElementTree();
      this.WaitingBarElement.ResumeApplyOfThemeSettings();
      this.WaitingBarElement.ContentElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.WaitingBarElement.SuspendApplyOfThemeSettings();
      this.WaitingBarElement.TextElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.WaitingBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.WaitingBarElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.WaitingBarElement.TextElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      }
      this.WaitingBarElement.ResumeApplyOfThemeSettings();
      this.WaitingBarElement.TextElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.WaitingBarElement.SuspendApplyOfThemeSettings();
      this.WaitingBarElement.TextElement.SuspendApplyOfThemeSettings();
      this.WaitingBarElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.WaitingBarElement.TextElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num = (int) this.WaitingBarElement.TextElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.WaitingBarElement.ElementTree.ApplyThemeToElementTree();
      this.WaitingBarElement.ResumeApplyOfThemeSettings();
      this.WaitingBarElement.TextElement.ResumeApplyOfThemeSettings();
    }

    private void WireEvents()
    {
      this.WaitingBarElement.TextElement.TextChanged += new EventHandler(this.TextElement_TextChanged);
    }

    private void UnwireEvents()
    {
      this.WaitingBarElement.TextElement.TextChanged -= new EventHandler(this.TextElement_TextChanged);
    }

    private void SetAssociatedControl(Control value)
    {
      if (value == null)
      {
        this.ClearAssociatedControl();
        this.associatedControl = value;
      }
      else
      {
        this.associatedControl = value;
        if (this.Site != null)
          return;
        this.Visible = false;
        this.SetAssociatedControlCore();
      }
    }

    private void ClearAssociatedControl()
    {
      if (this.associatedControlCover != null)
      {
        this.associatedControlCover.Layout -= new LayoutEventHandler(this.associatedControlCover_Layout);
        this.associatedControlCover.BackColorChanged -= new EventHandler(this.associatedControlCover_BackColorChanged);
        if (this.associatedControlCover.Controls.Contains((Control) this))
          this.associatedControlCover.Controls.Remove((Control) this);
      }
      if (this.associatedControl != null && this.associatedControl.Controls.Contains((Control) this.associatedControlCover))
        this.associatedControl.Controls.Remove((Control) this.associatedControlCover);
      if (this.oldParent != null)
        this.Parent = this.oldParent;
      this.associatedControlCover = (RadPanel) null;
    }

    private void SetAssociatedControlCore()
    {
      if (this.associatedControl == null)
        return;
      this.ClearAssociatedControl();
      if (this.Site == null)
      {
        this.CreateAssociatedCoverPanel();
        if (this.RootElement.GetValueSource(VisualElement.BackColorProperty) < ValueSource.Local)
          this.BackColor = Color.Transparent;
      }
      if (!this.IsWaiting)
        return;
      this.ShowCover();
    }

    protected virtual void CreateAssociatedCoverPanel()
    {
      this.associatedControlCover = new RadPanel();
      this.associatedControlCover.BackColor = Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.associatedControlCover.PanelElement.PanelBorder.ForeColor = Color.FromArgb(238, 238, 238);
      this.associatedControlCover.PanelElement.Text = string.Empty;
    }

    private void ShowCover()
    {
      if (this.associatedControl == null)
        return;
      if (this.associatedControlCover == null)
        this.CreateAssociatedCoverPanel();
      this.associatedControlCover.Layout += new LayoutEventHandler(this.associatedControlCover_Layout);
      this.associatedControlCover.BackColorChanged += new EventHandler(this.associatedControlCover_BackColorChanged);
      int num = 0;
      bool flag = false;
      foreach (Control control in (ArrangedElementCollection) this.AssociatedControl.Controls)
      {
        if (control.Visible && control != this.associatedControlCover)
          ++num;
        if (control == this.associatedControlCover)
          flag = true;
      }
      if (num > 0)
        this.AddBackgroundImageToAssociatedControlCoverPanel();
      this.Visible = true;
      this.associatedControlCover.Visible = true;
      if (!flag)
        this.associatedControl.Controls.Add((Control) this.associatedControlCover);
      if (!this.associatedControlCover.Controls.Contains((Control) this))
      {
        this.oldParent = this.Parent;
        this.associatedControlCover.Controls.Add((Control) this);
      }
      this.associatedControl.Controls.SetChildIndex((Control) this.associatedControlCover, 0);
      this.AssociatedControlCoverPanel.BringToFront();
      this.UpdateAssociatedControlLocations();
    }

    protected virtual void AddBackgroundImageToAssociatedControlCoverPanel()
    {
      Size size = this.associatedControl.Size;
      if (size.Width <= 0 || size.Height <= 0)
        return;
      Bitmap bitmap = new Bitmap(size.Width, size.Height);
      this.associatedControl.DrawToBitmap(bitmap, new Rectangle(Point.Empty, size));
      SolidBrush solidBrush = new SolidBrush(this.associatedControlCover.BackColor);
      Graphics.FromImage((Image) bitmap).FillRectangle((Brush) solidBrush, new Rectangle(Point.Empty, bitmap.Size));
      this.AssociatedControlCoverPanel.BackgroundImage = (Image) bitmap;
    }

    private void HideCover()
    {
      if (this.associatedControl == null || this.associatedControlCover == null)
        return;
      this.associatedControlCover.Visible = false;
      this.associatedControlCover.Layout -= new LayoutEventHandler(this.associatedControlCover_Layout);
      this.associatedControlCover.BackColorChanged -= new EventHandler(this.associatedControlCover_BackColorChanged);
    }

    private void UpdateAssociatedControlLocations()
    {
      if (this.associatedControl == null || !this.IsWaiting)
        return;
      if (this.associatedControlCover != null)
      {
        this.associatedControlCover.Location = Point.Empty;
        this.associatedControlCover.Size = this.AssociatedControl.Size;
        this.associatedControlCover.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        if (this.associatedControlCover.BackgroundImage != null)
        {
          this.associatedControlCover.Visible = false;
          this.AddBackgroundImageToAssociatedControlCoverPanel();
          this.associatedControlCover.Visible = true;
        }
      }
      this.Location = new Point((this.associatedControl.Width - this.Width) / 2, (this.associatedControl.Height - this.Height) / 2);
    }

    public event EventHandler WaitingStarted
    {
      add
      {
        this.WaitingBarElement.WaitingStarted += value;
      }
      remove
      {
        this.WaitingBarElement.WaitingStarted -= value;
      }
    }

    public event EventHandler WaitingStopped
    {
      add
      {
        this.WaitingBarElement.WaitingStopped += value;
      }
      remove
      {
        this.WaitingBarElement.WaitingStopped -= value;
      }
    }

    private void associatedControlCover_Layout(object sender, LayoutEventArgs e)
    {
      this.UpdateAssociatedControlLocations();
    }

    private void associatedControlCover_BackColorChanged(object sender, EventArgs e)
    {
      this.UpdateAssociatedControlLocations();
    }

    private void TextElement_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }
  }
}
