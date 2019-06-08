// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollablePanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Containers")]
  [ToolboxItem(true)]
  [Description("This control extends the behavior of RadPanel by enabling the theming of the scrollbars when in AutoScroll mode.")]
  [Docking(DockingBehavior.Ask)]
  [Designer("Telerik.WinControls.UI.Design.RadScrollablePanelDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadScrollablePanel : RadControl
  {
    private RadScrollablePanelElement panelElement;
    private RadScrollablePanelContainer container;
    private RadVScrollBar verticalScrollbar;
    private RadHScrollBar horizontalScrollbar;
    private ScrollState verticalScrollBarState;
    private ScrollState horizontalScrollBarState;
    private bool synchronizing;
    private bool scrollbarValueChanging;

    public RadScrollablePanel()
    {
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.InitializeInternalControls();
      this.container.Layout += new LayoutEventHandler(this.OnContainer_Layout);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.panelElement = this.CreatePanelElement();
      parent.Children.Add((RadElement) this.panelElement);
      this.panelElement.Border.PropertyChanged += new PropertyChangedEventHandler(this.OnPanelElementBorder_PropertyChanged);
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new RadScrollablePanelControlCollection(this);
    }

    protected virtual RadScrollablePanelElement CreatePanelElement()
    {
      return new RadScrollablePanelElement();
    }

    protected virtual RadScrollablePanelContainer CreateScrollablePanelContainer()
    {
      return new RadScrollablePanelContainer(this);
    }

    protected override RootRadElement CreateRootElement()
    {
      return (RootRadElement) new PanelRootElement();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.container.ScrollBarSynchronizationNeeded -= new ScrollbarSynchronizationNeededEventHandler(this.OnContainer_ScrolledToControl);
        this.panelElement.Border.PropertyChanged -= new PropertyChangedEventHandler(this.OnPanelElementBorder_PropertyChanged);
        this.container.Layout -= new LayoutEventHandler(this.OnContainer_Layout);
      }
      base.Dispose(disposing);
    }

    [DefaultValue(true)]
    public bool AllowAutomaticScrollToControl
    {
      get
      {
        return this.container.AllowAutomaticScrollToControl;
      }
      set
      {
        this.container.AllowAutomaticScrollToControl = value;
      }
    }

    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState VerticalScrollBarState
    {
      get
      {
        return this.verticalScrollBarState;
      }
      set
      {
        this.verticalScrollBarState = value;
        this.SynchronizeScrollbarsVisiblity();
      }
    }

    [DefaultValue(ScrollState.AutoHide)]
    public ScrollState HorizontalScrollBarState
    {
      get
      {
        return this.horizontalScrollBarState;
      }
      set
      {
        this.horizontalScrollBarState = value;
        this.SynchronizeScrollbarsVisiblity();
      }
    }

    internal Padding NCMargin
    {
      get
      {
        return this.GetContentMargin();
      }
    }

    public new Point AutoScrollPosition
    {
      get
      {
        return this.container.AutoScrollPosition;
      }
      set
      {
        this.container.AutoScrollPosition = value;
      }
    }

    [DefaultValue(typeof (Padding), "1,1,1,1")]
    public new Padding Padding
    {
      get
      {
        return base.Padding;
      }
      set
      {
        base.Padding = value;
      }
    }

    [DefaultValue(typeof (Size), "0,0")]
    public new Size AutoScrollMargin
    {
      get
      {
        return this.container.AutoScrollMargin;
      }
      set
      {
        this.container.AutoScrollMargin = value;
      }
    }

    [DefaultValue(typeof (Size), "0,0")]
    public new Size AutoScrollMinSize
    {
      get
      {
        return this.container.AutoScrollMinSize;
      }
      set
      {
        this.container.AutoScrollMinSize = value;
      }
    }

    [DefaultValue(true)]
    public override bool AutoScroll
    {
      get
      {
        return this.container.AutoScroll;
      }
      set
      {
        if (this.container.AutoScroll == value)
          return;
        this.container.AutoScroll = value;
        this.OnNotifyPropertyChanged(nameof (AutoScroll));
      }
    }

    public override Point AutoScrollOffset
    {
      get
      {
        return this.container.AutoScrollOffset;
      }
      set
      {
        this.container.AutoScrollOffset = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(200, 100));
      }
    }

    [Browsable(false)]
    public RadScrollablePanelElement PanelElement
    {
      get
      {
        return this.panelElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual Panel PanelContainer
    {
      get
      {
        return (Panel) this.container;
      }
    }

    [Browsable(false)]
    public RadVScrollBar VerticalScrollbar
    {
      get
      {
        return this.verticalScrollbar;
      }
    }

    [Browsable(false)]
    public RadHScrollBar HorizontalScrollbar
    {
      get
      {
        return this.horizontalScrollbar;
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

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      if (this.verticalScrollbar == null || this.horizontalScrollbar == null)
        return;
      this.verticalScrollbar.ThemeName = this.ThemeName;
      this.horizontalScrollbar.ThemeName = this.ThemeName;
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      if (this.RootElement.RightToLeft)
        this.verticalScrollbar.Dock = DockStyle.Left;
      else
        this.verticalScrollbar.Dock = DockStyle.Right;
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (!(propertyName == "AutoScroll"))
        return;
      this.OnAutoScrollChanged();
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.SynchronizeScrollbars(this.container.AutoScrollPosition);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      this.AdjustPaddingForAutoScrollSetting();
    }

    protected virtual void OnAutoScrollChanged()
    {
      this.AdjustPaddingForAutoScrollSetting();
      this.SynchronizeScrollbars(this.container.AutoScrollPosition);
      this.Refresh();
      this.LayoutEngine.Layout((object) this, new LayoutEventArgs((Control) this, "Bounds"));
    }

    private void OnContainer_ScrolledToControl(
      object sender,
      ScrollbarSynchronizationNeededEventArgs args)
    {
      this.SynchronizeScrollbars(args.ScrolledLocation);
    }

    private void OnContainer_Layout(object sender, LayoutEventArgs e)
    {
      if (this.scrollbarValueChanging)
        return;
      this.SynchronizeScrollbars(this.container.AutoScrollPosition);
    }

    private void horizontalScrollbar_ValueChanged(object sender, EventArgs e)
    {
      if (this.synchronizing)
        return;
      if (this.RightToLeft != RightToLeft.Yes)
        this.container.HorizontalScroll.Value = this.horizontalScrollbar.Value;
      else
        this.container.HorizontalScroll.Value = this.container.HorizontalScroll.Maximum - this.container.HorizontalScroll.LargeChange + 1 - this.horizontalScrollbar.Value;
      this.scrollbarValueChanging = true;
      this.container.PerformLayout();
      this.scrollbarValueChanging = false;
    }

    private void verticalScrollbar_ValueChanged(object sender, EventArgs e)
    {
      if (this.synchronizing)
        return;
      this.container.VerticalScroll.Value = this.verticalScrollbar.Value;
      this.container.PerformLayout();
    }

    private void OnPanelElementBorder_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == RadElement.VisibilityProperty.Name && this.panelElement.Border.Visibility != ElementVisibility.Visible)
      {
        this.SuspendLayout();
        this.Padding = Padding.Empty;
        this.ResumeLayout(false);
      }
      else
      {
        if (e.PropertyName == BorderPrimitive.WidthProperty.Name && this.panelElement.Border.BoxStyle != BorderBoxStyle.FourBorders)
        {
          this.SuspendLayout();
          this.Padding = new Padding((int) this.panelElement.Border.Width);
          this.ResumeLayout(false);
        }
        if (!(e.PropertyName == BorderPrimitive.LeftWidthProperty.Name) && !(e.PropertyName == BorderPrimitive.TopWidthProperty.Name) && (!(e.PropertyName == BorderPrimitive.RightWidthProperty.Name) && !(e.PropertyName == BorderPrimitive.BottomWidthProperty.Name)) || this.panelElement.Border.BoxStyle != BorderBoxStyle.FourBorders)
          return;
        this.SuspendLayout();
        this.Padding = new Padding((int) this.panelElement.Border.LeftWidth, (int) this.panelElement.Border.TopWidth, (int) this.panelElement.Border.RightWidth, (int) this.panelElement.Border.BottomWidth);
        this.ResumeLayout(false);
      }
    }

    private void OnInternalContainer_MouseWheel(object sender, MouseEventArgs e)
    {
      this.SynchronizeScrollbars(this.container.AutoScrollPosition);
      if (!this.verticalScrollbar.Visible)
        return;
      this.container.Invalidate();
    }

    protected virtual Color GetSizingGripColor()
    {
      FillPrimitive child = this.horizontalScrollbar.ScrollBarElement.Children[0] as FillPrimitive;
      switch (child.GradientStyle)
      {
        case GradientStyles.Linear:
          return this.GetFillPrimitiveAverageColor(child);
        default:
          return child.BackColor;
      }
    }

    protected virtual void InsertInternalControls()
    {
      this.SuspendLayout();
      (this.Controls as RadScrollablePanelControlCollection).AddControlInternal((Control) this.container);
      (this.Controls as RadScrollablePanelControlCollection).AddControlInternal((Control) this.verticalScrollbar);
      (this.Controls as RadScrollablePanelControlCollection).AddControlInternal((Control) this.horizontalScrollbar);
      this.ResumeLayout(true);
    }

    protected virtual Padding GetContentMargin()
    {
      if (this.panelElement.Border.Visibility != ElementVisibility.Visible)
        return Padding.Empty;
      Padding p2 = this.panelElement.BorderThickness;
      if (this.RootElement.Shape != null && this.RootElement.ApplyShapeToControl && this.RootElement.Shape is RoundRectShape)
      {
        RoundRectShape shape = this.RootElement.Shape as RoundRectShape;
        p2 = Padding.Add(new Padding((int) ((double) shape.Radius - Math.Sqrt((double) (shape.Radius * shape.Radius / 2)))), p2);
      }
      return p2;
    }

    protected virtual void InitializeInternalControls()
    {
      this.container = this.CreateScrollablePanelContainer();
      this.container.ScrollBarSynchronizationNeeded += new ScrollbarSynchronizationNeededEventHandler(this.OnContainer_ScrolledToControl);
      this.container.Dock = DockStyle.Fill;
      this.container.AutoScroll = true;
      this.verticalScrollbar = new RadVScrollBar();
      this.verticalScrollbar.Focusable = false;
      this.verticalScrollbar.Dock = DockStyle.Right;
      this.verticalScrollbar.ValueChanged += new EventHandler(this.verticalScrollbar_ValueChanged);
      this.verticalScrollbar.Visible = false;
      this.horizontalScrollbar = new RadHScrollBar();
      this.horizontalScrollbar.Focusable = false;
      this.horizontalScrollbar.Dock = DockStyle.Bottom;
      this.horizontalScrollbar.ValueChanged += new EventHandler(this.horizontalScrollbar_ValueChanged);
      this.horizontalScrollbar.Visible = false;
      this.container.MouseWheel += new MouseEventHandler(this.OnInternalContainer_MouseWheel);
      this.InsertInternalControls();
    }

    protected virtual void SynchronizeVScrollbar(int value)
    {
      this.SynchronizeScrollbarCore(value, (RadScrollBar) this.VerticalScrollbar, this.VerticalScrollBarState, (ScrollProperties) this.container.VerticalScroll);
    }

    protected virtual void SynchronizeHScrollbar(int value)
    {
      this.SynchronizeScrollbarCore(value, (RadScrollBar) this.HorizontalScrollbar, this.HorizontalScrollBarState, (ScrollProperties) this.container.HorizontalScroll);
    }

    protected virtual void AdjustSizingGrip()
    {
      if (this.VerticalScrollbar.Visible)
      {
        if (!this.ElementTree.RootElement.RightToLeft)
          this.horizontalScrollbar.ScrollBarElement.Margin = new Padding(0, 0, this.verticalScrollbar.ScrollBarElement.ControlBoundingRectangle.Width, 0);
        else
          this.horizontalScrollbar.ScrollBarElement.Margin = new Padding(this.verticalScrollbar.ScrollBarElement.ControlBoundingRectangle.Width, 0, 0, 0);
      }
      else
        this.horizontalScrollbar.ScrollBarElement.Margin = Padding.Empty;
    }

    protected virtual void SynchronizeScrollbars(Point scrollPoint)
    {
      this.SynchronizeScrollbarsVisiblity();
      if (!this.AutoScroll)
        return;
      if (this.VerticalScrollbar.Visible)
        this.SynchronizeVScrollbar(-scrollPoint.Y);
      if (this.HorizontalScrollbar.Visible)
        this.SynchronizeHScrollbar(-scrollPoint.X);
      this.AdjustSizingGrip();
    }

    protected virtual void SynchronizeScrollbarsVisiblity()
    {
      switch (this.VerticalScrollBarState)
      {
        case ScrollState.AutoHide:
          this.VerticalScrollbar.Visible = this.container.VerticalScroll.Visible;
          break;
        case ScrollState.AlwaysShow:
          this.VerticalScrollbar.Visible = true;
          break;
        case ScrollState.AlwaysHide:
          this.VerticalScrollbar.Visible = false;
          break;
      }
      switch (this.HorizontalScrollBarState)
      {
        case ScrollState.AutoHide:
          this.HorizontalScrollbar.Visible = this.container.HorizontalScroll.Visible;
          break;
        case ScrollState.AlwaysShow:
          this.HorizontalScrollbar.Visible = true;
          break;
        case ScrollState.AlwaysHide:
          this.HorizontalScrollbar.Visible = false;
          break;
      }
    }

    private void AdjustPaddingForAutoScrollSetting()
    {
      this.Padding = this.GetContentMargin();
    }

    private Color GetFillPrimitiveAverageColor(FillPrimitive fill)
    {
      Color color = fill.BackColor;
      if (fill.NumberOfColors == 2)
        color = Color.FromArgb((int) (byte) ((int) color.A + (int) fill.BackColor2.A) / 2, (int) (byte) ((int) color.R + (int) fill.BackColor2.R) / 2, (int) (byte) ((int) color.G + (int) fill.BackColor2.G) / 2, (int) (byte) ((int) color.B + (int) fill.BackColor2.B) / 2);
      else if (fill.NumberOfColors == 3)
        color = Color.FromArgb((int) (byte) ((int) color.A + (int) fill.BackColor2.A + (int) fill.BackColor3.A) / 3, (int) (byte) ((int) color.R + (int) fill.BackColor2.R + (int) fill.BackColor3.R) / 3, (int) (byte) ((int) color.G + (int) fill.BackColor2.G + (int) fill.BackColor3.G) / 3, (int) (byte) ((int) color.B + (int) fill.BackColor2.B + (int) fill.BackColor3.B) / 3);
      else if (fill.NumberOfColors == 4)
        color = Color.FromArgb((int) (byte) ((int) color.A + (int) fill.BackColor2.A + (int) fill.BackColor3.A + (int) fill.BackColor3.A) / 4, (int) (byte) ((int) color.R + (int) fill.BackColor2.R + (int) fill.BackColor3.R + (int) fill.BackColor3.R) / 4, (int) (byte) ((int) color.G + (int) fill.BackColor2.G + (int) fill.BackColor3.G + (int) fill.BackColor3.G) / 4, (int) (byte) ((int) color.B + (int) fill.BackColor2.B + (int) fill.BackColor3.B + (int) fill.BackColor3.B) / 4);
      return color;
    }

    private bool IsValueInRange(RadScrollBar scrollBar, int value)
    {
      return value <= scrollBar.Maximum && value >= scrollBar.Minimum;
    }

    private void SynchronizeScrollbarCore(
      int value,
      RadScrollBar scrollbar,
      ScrollState scrollbarState,
      ScrollProperties scrollProperties)
    {
      if (!this.IsValueInRange(scrollbar, value))
        return;
      this.synchronizing = true;
      scrollbar.Minimum = scrollProperties.Minimum;
      scrollbar.Maximum = scrollProperties.Maximum;
      scrollbar.SmallChange = scrollProperties.SmallChange;
      scrollbar.Value = value;
      scrollbar.LargeChange = scrollProperties.Visible || scrollbarState != ScrollState.AlwaysShow ? scrollProperties.LargeChange : scrollProperties.Maximum;
      this.synchronizing = false;
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      this.container.ProcessAutoSizeChanged(value);
    }

    public virtual void FitToChildControls()
    {
      Size empty = Size.Empty;
      foreach (Control control in (ArrangedElementCollection) this.container.Controls)
      {
        empty.Width = Math.Max(empty.Width, control.Right);
        empty.Height = Math.Max(empty.Height, control.Bottom);
      }
      empty.Width += this.Padding.Right + this.PanelElement.BorderThickness.Right;
      empty.Height += this.Padding.Bottom + this.PanelElement.BorderThickness.Bottom;
      empty.Width = Math.Max(this.MinimumSize.Width, empty.Width);
      empty.Height = Math.Max(this.MinimumSize.Height, empty.Height);
      if (this.MaximumSize.Width > 0)
        empty.Width = Math.Min(this.MaximumSize.Width, empty.Width);
      if (this.MaximumSize.Height > 0)
        empty.Height = Math.Min(this.MaximumSize.Height, empty.Height);
      this.ClientSize = empty;
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.PanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.PanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "", "RadScrollablePanelFill");
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.PanelElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.PanelElement.Fill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.PanelElement.ElementTree.ApplyThemeToElementTree();
    }
  }
}
