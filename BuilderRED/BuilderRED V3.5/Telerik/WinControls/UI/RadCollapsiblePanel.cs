// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCollapsiblePanel
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

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Containers")]
  [DefaultEvent("Expanded")]
  [Designer("Telerik.WinControls.UI.Design.RadCollapsiblePanelDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("A control that displays a header that has a collapsible window that displays other controls.")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  public class RadCollapsiblePanel : RadControl
  {
    private RadCollapsiblePanelElement collapsiblePanelElement;
    private CollapsiblePanelControlsContainer controlsContainer;

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(150, 200));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the controls container.")]
    public CollapsiblePanelControlsContainer ControlsContainer
    {
      get
      {
        return this.controlsContainer;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the panel container which contains the controls of the RadCollapsiblePanel.")]
    public CollapsiblePanelPanelContainer PanelContainer
    {
      get
      {
        return this.controlsContainer.PanelContainer as CollapsiblePanelPanelContainer;
      }
    }

    [Description("Gets the collapsible panel element.")]
    public RadCollapsiblePanelElement CollapsiblePanelElement
    {
      get
      {
        return this.collapsiblePanelElement;
      }
    }

    [DefaultValue(RadDirection.Down)]
    [Description("Gets or sets the expand direction.")]
    public RadDirection ExpandDirection
    {
      get
      {
        return this.collapsiblePanelElement.ExpandDirection;
      }
      set
      {
        this.collapsiblePanelElement.ExpandDirection = value;
      }
    }

    [Description("Gets or sets a value indicating whether to use animation to expand or collapse the control.")]
    [DefaultValue(true)]
    public bool EnableAnimation
    {
      get
      {
        return this.CollapsiblePanelElement.EnableAnimation;
      }
      set
      {
        this.CollapsiblePanelElement.EnableAnimation = value;
      }
    }

    [Description("Gets or sets the content sizing mode.")]
    [DefaultValue(CollapsiblePanelContentSizingMode.None)]
    public CollapsiblePanelContentSizingMode ContentSizingMode
    {
      get
      {
        return this.CollapsiblePanelElement.ContentSizingMode;
      }
      set
      {
        this.CollapsiblePanelElement.ContentSizingMode = value;
      }
    }

    [Description("Gets a value indicating whether the control is expanded.")]
    [DefaultValue(true)]
    public bool IsExpanded
    {
      get
      {
        return this.CollapsiblePanelElement.IsExpanded;
      }
      set
      {
        this.CollapsiblePanelElement.IsExpanded = value;
      }
    }

    [Browsable(false)]
    [Description("Gets a value indicating whether the control is currently animating.")]
    public bool IsAnimating
    {
      get
      {
        return this.CollapsiblePanelElement.IsAnimating;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to show a line primitive in the header.")]
    public bool ShowHeaderLine
    {
      get
      {
        return this.CollapsiblePanelElement.ShowHeaderLine;
      }
      set
      {
        this.CollapsiblePanelElement.ShowHeaderLine = value;
      }
    }

    [DefaultValue(RadHorizontalAlignment.Left)]
    public RadHorizontalAlignment HorizontalHeaderAlignment
    {
      get
      {
        return this.CollapsiblePanelElement.HorizontalHeaderAlignment;
      }
      set
      {
        this.CollapsiblePanelElement.HorizontalHeaderAlignment = value;
      }
    }

    [DefaultValue(RadVerticalAlignment.Top)]
    public RadVerticalAlignment VerticalHeaderAlignment
    {
      get
      {
        return this.CollapsiblePanelElement.VerticalHeaderAlignment;
      }
      set
      {
        this.CollapsiblePanelElement.VerticalHeaderAlignment = value;
      }
    }

    [DefaultValue("")]
    [Localizable(true)]
    public string HeaderText
    {
      get
      {
        return this.CollapsiblePanelElement.HeaderText;
      }
      set
      {
        this.CollapsiblePanelElement.HeaderText = value;
      }
    }

    [Description("This value is set when the control is about to be collapsed and is used to restore the control's size when expanding. It should only be set by the control itself.")]
    [DefaultValue(typeof (Rectangle), "0, 0, 0, 0")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Rectangle OwnerBoundsCache
    {
      get
      {
        return this.CollapsiblePanelElement.OwnerBoundsCache;
      }
      set
      {
        this.CollapsiblePanelElement.OwnerBoundsCache = value;
      }
    }

    [Description("Gets or sets the animation interval.")]
    [DefaultValue(30)]
    public int AnimationInterval
    {
      get
      {
        return this.collapsiblePanelElement.AnimationInterval;
      }
      set
      {
        this.collapsiblePanelElement.AnimationInterval = value;
      }
    }

    [DefaultValue(15)]
    [Description("Gets or sets the animation frames.")]
    public int AnimationFrames
    {
      get
      {
        return this.collapsiblePanelElement.AnimationFrames;
      }
      set
      {
        this.collapsiblePanelElement.AnimationFrames = value;
      }
    }

    [DefaultValue(RadEasingType.Default)]
    [Description("Gets or sets the easing type to be applied to the animations")]
    public RadEasingType AnimationEasingType
    {
      get
      {
        return this.collapsiblePanelElement.AnimationEasingType;
      }
      set
      {
        this.collapsiblePanelElement.AnimationEasingType = value;
      }
    }

    [DefaultValue(CollapsiblePanelAnimationType.Reveal)]
    [Description("Gets or sets the type of the expand or collapse animation.")]
    public CollapsiblePanelAnimationType AnimationType
    {
      get
      {
        return this.collapsiblePanelElement.AnimationType;
      }
      set
      {
        this.collapsiblePanelElement.AnimationType = value;
      }
    }

    [Description("Gets or sets the BackColor of the control. This is actually the BackColor property of the root element.")]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    public RadCollapsiblePanel()
    {
      this.InitializeInternalContainer();
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.LocationChanged += new EventHandler(this.RadCollapsiblePanelLocationChanged);
      this.SizeChanged += new EventHandler(this.RadCollapsiblePanelSizeChanged);
      if (this.IsExpanded)
        return;
      this.ControlsContainer.Size = Size.Empty;
    }

    protected virtual void InitializeInternalContainer()
    {
      this.SuspendLayout();
      this.controlsContainer = this.CreateControlsContainer();
      this.controlsContainer.Name = "CollapsiblePanelControlsContainer";
      this.controlsContainer.RootElement.StretchHorizontally = true;
      this.controlsContainer.RootElement.StretchVertically = true;
      this.controlsContainer.RootElement.EnableElementShadow = false;
      (this.Controls as RadCollapsiblePanelControlsCollection).AddInternal((Control) this.controlsContainer);
      this.ResumeLayout(true);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.collapsiblePanelElement = this.CreateCollapsiblePanelElement();
      parent.Children.Add((RadElement) this.collapsiblePanelElement);
    }

    protected virtual CollapsiblePanelControlsContainer CreateControlsContainer()
    {
      return new CollapsiblePanelControlsContainer();
    }

    protected virtual RadCollapsiblePanelElement CreateCollapsiblePanelElement()
    {
      return new RadCollapsiblePanelElement(this);
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new RadCollapsiblePanelControlsCollection(this);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.LocationChanged -= new EventHandler(this.RadCollapsiblePanelLocationChanged);
        this.SizeChanged -= new EventHandler(this.RadCollapsiblePanelSizeChanged);
      }
      base.Dispose(disposing);
    }

    [Description("Expands the control. Will not have effect if the control is already expanded or is animating. Can be canceled from the Expanding event.")]
    public void Expand()
    {
      this.CollapsiblePanelElement.Expand();
    }

    [Description("Collapses the control. Will not have effect if the control is already collapsed or is animating. Can be canceled from the Collapsing event.")]
    public void Collapse()
    {
      this.CollapsiblePanelElement.Collapse();
    }

    [Description("If the Control is expanded it will be collapsed and vice-versa.")]
    public void ToggleExpandCollapse()
    {
      this.CollapsiblePanelElement.ToggleExpandCollapse();
    }

    [Description("If the Control is expanded it will be collapsed and vice-versa.")]
    protected void ToggleExpandCollapse(bool ignoreIsExpanded, bool stopAnimations)
    {
      this.CollapsiblePanelElement.ToggleExpandCollapse(ignoreIsExpanded, stopAnimations);
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      if (this.collapsiblePanelElement != null)
        this.collapsiblePanelElement.RightToLeft = this.RightToLeft == RightToLeft.Yes;
      if (this.controlsContainer == null)
        return;
      this.controlsContainer.RightToLeft = this.RightToLeft;
    }

    protected override void OnDockChanged(EventArgs e)
    {
      base.OnDockChanged(e);
      this.OwnerBoundsCache = this.Bounds;
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      if (!(this.ControlsContainer.ThemeName != this.ThemeName))
        return;
      this.ControlsContainer.ThemeName = this.ThemeName;
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.CollapsiblePanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.ControlsContainer.BackColor = this.BackColor;
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.CollapsiblePanelElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.CollapsiblePanelElement.ElementTree.ApplyThemeToElementTree();
      this.ControlsContainer.BackColor = Color.Empty;
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.CollapsiblePanelElement.HeaderElement.HeaderTextElement.SuspendApplyOfThemeSettings();
      this.CollapsiblePanelElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
      List<string> availableVisualStates = this.CollapsiblePanelElement.HeaderElement.HeaderTextElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.CollapsiblePanelElement.HeaderElement.HeaderTextElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.CollapsiblePanelElement.HeaderElement.HeaderTextElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.CollapsiblePanelElement.SuspendApplyOfThemeSettings();
      this.CollapsiblePanelElement.HeaderElement.HeaderTextElement.SuspendApplyOfThemeSettings();
      this.CollapsiblePanelElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num1 = (int) this.CollapsiblePanelElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.CollapsiblePanelElement.HeaderElement.HeaderTextElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num2 = (int) this.CollapsiblePanelElement.HeaderElement.HeaderTextElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.CollapsiblePanelElement.ElementTree.ApplyThemeToElementTree();
      this.CollapsiblePanelElement.ResumeApplyOfThemeSettings();
      this.CollapsiblePanelElement.HeaderElement.HeaderTextElement.ResumeApplyOfThemeSettings();
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      if ((double) factor.Width == 1.0 && (double) factor.Height == 1.0)
        return;
      base.ScaleControl(factor, specified);
      this.ToggleExpandCollapse(true, true);
      this.ToggleExpandCollapse(true, true);
    }

    [Description("Occurs after the control is expanded.")]
    public event EventHandler Expanded
    {
      add
      {
        this.CollapsiblePanelElement.Expanded += value;
      }
      remove
      {
        this.CollapsiblePanelElement.Expanded -= value;
      }
    }

    [Description("Occurs after the control is collapsed.")]
    public event EventHandler Collapsed
    {
      add
      {
        this.CollapsiblePanelElement.Collapsed += value;
      }
      remove
      {
        this.CollapsiblePanelElement.Collapsed -= value;
      }
    }

    [Description("Occurs before the control is expanded.")]
    public event CancelEventHandler Expanding
    {
      add
      {
        this.CollapsiblePanelElement.Expanding += value;
      }
      remove
      {
        this.CollapsiblePanelElement.Expanding -= value;
      }
    }

    [Description("Occurs before the control is collapsed.")]
    public event CancelEventHandler Collapsing
    {
      add
      {
        this.CollapsiblePanelElement.Collapsing += value;
      }
      remove
      {
        this.CollapsiblePanelElement.Collapsing -= value;
      }
    }

    private void RadCollapsiblePanelLocationChanged(object sender, EventArgs e)
    {
      if (this.IsExpanded)
        return;
      this.OwnerBoundsCache = new Rectangle(this.Location, this.OwnerBoundsCache.Size);
    }

    private void RadCollapsiblePanelSizeChanged(object sender, EventArgs e)
    {
      if (this.IsExpanded)
        return;
      if (this.ExpandDirection == RadDirection.Up || this.ExpandDirection == RadDirection.Down)
        this.OwnerBoundsCache = new Rectangle(this.Location, new Size(this.Bounds.Width, this.OwnerBoundsCache.Height));
      else
        this.OwnerBoundsCache = new Rectangle(this.Location, new Size(this.OwnerBoundsCache.Width, this.Bounds.Height));
    }
  }
}
