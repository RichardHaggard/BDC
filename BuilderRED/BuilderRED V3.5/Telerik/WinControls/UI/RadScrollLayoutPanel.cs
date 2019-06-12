// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadScrollLayoutPanel : LayoutPanel
  {
    public static readonly RadProperty ScrollThicknessProperty = RadProperty.Register(nameof (ScrollThickness), typeof (int), typeof (RadScrollLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadScrollBarElement.VerticalScrollBarWidth, ElementPropertyOptions.None));
    public static readonly RadProperty ForceViewportWidthProperty = RadProperty.Register(nameof (ForceViewportWidth), typeof (bool), typeof (RadScrollLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty ForceViewportHeightProperty = RadProperty.Register(nameof (ForceViewportHeight), typeof (bool), typeof (RadScrollLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private int scrollThicknessCache = RadScrollBarElement.VerticalScrollBarWidth;
    private Point pixelsPerLineScroll = new Point(16, 16);
    internal const long UseScrollCallbackStateKey = 137438953472;
    internal const long IsScrollingStateKey = 274877906944;
    internal const long IsHorizScrollNeededStateKey = 549755813888;
    internal const long IsVertScrollNeededStateKey = 1099511627776;
    internal const long MeasureWithAvaibleSizeStateKey = 2199023255552;
    internal const long UsePhysicalScrollingStateKey = 4398046511104;
    internal const long RadScrollLayoutPanelLastStateKey = 4398046511104;
    private const int InitialPixelsPerLineScroll = 16;
    private RadScrollBarElement horizontalScrollBar;
    private RadScrollBarElement verticalScrollBar;
    private FillPrimitive blankSpot;
    private Size clientSize;
    private Size viewportSize;
    private Size extentSize;
    private ScrollState horizontalScrollState;
    private ScrollState verticalScrollState;
    private RadElement viewport;

    [Description("Occurs when the user scrolls the content of the viewport.")]
    [Category("Action")]
    public event RadScrollPanelHandler Scroll;

    protected virtual void OnScroll(Point oldValue, Point newValue)
    {
      if (this.Scroll == null)
        return;
      this.Scroll((object) this, new ScrollPanelEventArgs(oldValue, newValue));
    }

    [Category("Action")]
    [Description("Occurs when the need for horizontal or vertical scrollbar has changed.")]
    public event ScrollNeedsHandler ScrollNeedsChanged;

    protected virtual void OnScrollNeedsChanged(ScrollNeedsEventArgs args)
    {
      if (this.ScrollNeedsChanged == null)
        return;
      this.ScrollNeedsChanged((object) this, args);
    }

    [Category("Property Changed")]
    [Description("Occurs when property that affects the scrolling functionality is changed.")]
    public event RadPanelScrollParametersHandler ScrollParametersChanged;

    protected virtual void OnScrollParametersChanged(RadScrollBarElement scrollBar)
    {
      if (scrollBar == null || this.ScrollParametersChanged == null)
        return;
      this.ScrollParametersChanged((object) this, new RadPanelScrollParametersEventArgs(scrollBar.ScrollType == ScrollType.Horizontal, scrollBar.GetParameters()));
    }

    [Browsable(false)]
    public event ScrollViewportSetHandler ScrollViewportSet;

    protected virtual void OnNewViewportSet(RadElement oldViewport, RadElement newViewport)
    {
      if (this.ScrollViewportSet == null)
        return;
      this.ScrollViewportSet((object) this, new Telerik.WinControls.UI.ScrollViewportSet(oldViewport, newViewport));
    }

    public RadScrollBarElement HorizontalScrollBar
    {
      get
      {
        return this.horizontalScrollBar;
      }
    }

    public RadScrollBarElement VerticalScrollBar
    {
      get
      {
        return this.verticalScrollBar;
      }
    }

    public FillPrimitive BlankSpot
    {
      get
      {
        return this.blankSpot;
      }
    }

    internal int SmallHorizontalChange
    {
      get
      {
        return this.horizontalScrollBar.SmallChange;
      }
    }

    internal int SmallVerticalChange
    {
      get
      {
        return this.verticalScrollBar.SmallChange;
      }
    }

    internal int LargeHorizontalChange
    {
      get
      {
        return this.horizontalScrollBar.LargeChange;
      }
    }

    internal int LargeVerticalChange
    {
      get
      {
        return this.verticalScrollBar.LargeChange;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanHorizontalScroll
    {
      get
      {
        return this.GetBitState(549755813888L);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanVerticalScroll
    {
      get
      {
        return this.GetBitState(1099511627776L);
      }
    }

    [Category("Behavior")]
    [DefaultValue(ScrollState.AutoHide)]
    [Description("Determine when the horizontal scroll bar should be shown.")]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.horizontalScrollState;
      }
      set
      {
        this.horizontalScrollState = value;
        this.ResetLayout();
      }
    }

    [Description("Determine when the vertical scroll bar should be shown.")]
    [DefaultValue(ScrollState.AutoHide)]
    [Category("Behavior")]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.verticalScrollState;
      }
      set
      {
        this.verticalScrollState = value;
        this.ResetLayout();
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the thickness of the scrollbar.")]
    [Category("Behavior")]
    [DefaultValue(16)]
    public int ScrollThickness
    {
      get
      {
        return TelerikDpiHelper.ScaleInt(this.scrollThicknessCache, this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadScrollLayoutPanel.ScrollThicknessProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadElement Viewport
    {
      get
      {
        return this.viewport;
      }
      set
      {
        if (value == this.viewport)
          return;
        RadElement viewport = this.viewport;
        if (this.viewport != null)
          this.Children.Remove(this.viewport);
        this.viewport = value;
        if (this.viewport != null)
          this.Children.Add(this.viewport);
        this.SetViewportAutoSizeMode();
        this.OnNewViewportSet(viewport, value);
      }
    }

    [Description("Switch Physical / Logical scrolling (if possible).")]
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    public bool UsePhysicalScrolling
    {
      get
      {
        if (!(this.viewport is IRadScrollViewport))
          return true;
        IVirtualViewport viewport = this.viewport as IVirtualViewport;
        if (viewport != null && viewport.Virtualized)
          return false;
        return this.GetBitState(4398046511104L);
      }
      set
      {
        if (value == this.GetBitState(4398046511104L))
          return;
        this.BitState[4398046511104L] = value;
        this.SetViewportAutoSizeMode();
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Specifies the number of pixels for Line scroll (Small change) when Physical scrolling is used.")]
    public Point PixelsPerLineScroll
    {
      get
      {
        return this.pixelsPerLineScroll;
      }
      set
      {
        this.pixelsPerLineScroll = value;
        this.ResetLayout();
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Specifies the minimum scrolling value (in both horizontal and vertical directions)")]
    public Point MinValue
    {
      get
      {
        return new Point(this.horizontalScrollBar.Minimum, this.verticalScrollBar.Minimum);
      }
      set
      {
        this.horizontalScrollBar.Minimum = value.X;
        this.verticalScrollBar.Minimum = value.Y;
        this.ResetLayout();
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Specifies the maximum scrolling value (in both horizontal and vertical directions)")]
    public Point MaxValue
    {
      get
      {
        return new Point(this.horizontalScrollBar.Maximum, this.verticalScrollBar.Maximum);
      }
      set
      {
        this.horizontalScrollBar.Maximum = value.X;
        this.verticalScrollBar.Maximum = value.Y;
        this.OnNotifyPropertyChanged(nameof (MaxValue));
        this.ResetLayout();
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Specifies the current scrolling value (in both horizontal and vertical directions)")]
    public Point Value
    {
      get
      {
        return new Point(this.horizontalScrollBar.Value, this.verticalScrollBar.Value);
      }
      set
      {
        this.ScrollTo(value.X, value.Y);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ForceViewportWidth
    {
      get
      {
        return (bool) this.GetValue(RadScrollLayoutPanel.ForceViewportWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadScrollLayoutPanel.ForceViewportWidthProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool ForceViewportHeight
    {
      get
      {
        return (bool) this.GetValue(RadScrollLayoutPanel.ForceViewportHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadScrollLayoutPanel.ForceViewportHeightProperty, (object) value);
      }
    }

    public bool MeasureWithAvaibleSize
    {
      get
      {
        return this.GetBitState(2199023255552L);
      }
      set
      {
        this.SetBitState(2199023255552L, value);
      }
    }

    public RadScrollLayoutPanel()
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.horizontalScrollState = ScrollState.AutoHide;
      this.verticalScrollState = ScrollState.AutoHide;
      this.BitState[137438953472L] = true;
      this.ClipDrawing = true;
    }

    public RadScrollLayoutPanel(RadElement viewport)
    {
      this.Viewport = viewport;
    }

    public RadScrollLayoutPanel(RadElement viewport, int initialScrollThickness)
      : this(viewport)
    {
      this.ScrollThickness = initialScrollThickness;
    }

    protected override void CreateChildElements()
    {
      this.horizontalScrollBar = new RadScrollBarElement();
      this.horizontalScrollBar.ScrollType = ScrollType.Horizontal;
      this.horizontalScrollBar.Visibility = ElementVisibility.Collapsed;
      this.horizontalScrollBar.Minimum = 0;
      this.horizontalScrollBar.ZIndex = 1000;
      this.horizontalScrollBar.Class = "ScrollPanelHorizontalScrollBar";
      this.horizontalScrollBar.ThemeRole = "RadScrollBarElement";
      this.Children.Add((RadElement) this.horizontalScrollBar);
      this.verticalScrollBar = new RadScrollBarElement();
      this.verticalScrollBar.ScrollType = ScrollType.Vertical;
      this.verticalScrollBar.Visibility = ElementVisibility.Collapsed;
      this.verticalScrollBar.Minimum = 0;
      this.verticalScrollBar.ZIndex = 1000;
      this.verticalScrollBar.Class = "ScrollPanelVerticalScrollBar";
      this.verticalScrollBar.ThemeRole = "RadScrollBarElement";
      this.Children.Add((RadElement) this.verticalScrollBar);
      this.blankSpot = new FillPrimitive();
      this.blankSpot.Class = "ScrollPanelBlankSpotFill";
      this.blankSpot.Visibility = ElementVisibility.Collapsed;
      this.blankSpot.ZIndex = 1000;
      this.blankSpot.SmoothingMode = SmoothingMode.None;
      this.Children.Add((RadElement) this.blankSpot);
      this.horizontalScrollBar.Scroll += new ScrollEventHandler(this.OnHScroll);
      this.verticalScrollBar.Scroll += new ScrollEventHandler(this.OnVScroll);
      this.horizontalScrollBar.ScrollParameterChanged += new EventHandler(this.OnScrollBarParameterChanged);
      this.verticalScrollBar.ScrollParameterChanged += new EventHandler(this.OnScrollBarParameterChanged);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        if (object.ReferenceEquals((object) child, (object) this.viewport) && !this.MeasureWithAvaibleSize)
          child.Measure(new SizeF(float.PositiveInfinity, float.PositiveInfinity));
        else
          child.Measure(availableSize);
      }
      return SizeF.Empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      (this.viewport as IRadScrollViewport)?.InvalidateViewport();
      if (this.viewport == null)
      {
        base.ArrangeOverride(finalSize);
        this.horizontalScrollBar.Visibility = ElementVisibility.Collapsed;
        this.verticalScrollBar.Visibility = ElementVisibility.Collapsed;
        return finalSize;
      }
      this.clientSize = Size.Round(finalSize);
      Size size1 = Size.Round(this.viewport.DesiredSize);
      this.extentSize = this.UsePhysicalScrolling ? size1 : ((IRadScrollViewport) this.viewport).GetExtentSize();
      RadScrollLayoutPanel.ScrollFlags scrollingNeeds = this.GetScrollingNeeds(this.extentSize, this.clientSize);
      Size scrollBarsSize = this.GetScrollBarsSize(scrollingNeeds);
      scrollBarsSize.Width = Math.Max(scrollBarsSize.Width, (int) this.VerticalScrollBar.DesiredSize.Width);
      scrollBarsSize.Height = Math.Max(scrollBarsSize.Height, (int) this.HorizontalScrollBar.DesiredSize.Height);
      this.viewportSize = Size.Subtract(this.clientSize, scrollBarsSize);
      this.ResetScrollState(scrollingNeeds);
      Size size2 = Size.Empty;
      if (this.BlankSpot.Visibility == ElementVisibility.Visible)
        size2 = scrollBarsSize;
      float x1 = this.RightToLeft ? (float) scrollBarsSize.Width : 0.0f;
      float x2 = this.RightToLeft ? (float) scrollBarsSize.Width : 0.0f;
      float x3 = this.RightToLeft ? 0.0f : finalSize.Width - (float) scrollBarsSize.Width;
      float x4 = this.RightToLeft ? 0.0f : finalSize.Width - (float) size2.Width;
      RectangleF viewportRect = new RectangleF(x1, 0.0f, (float) this.viewportSize.Width, (float) this.viewportSize.Height);
      RectangleF finalRect1 = new RectangleF(x3, 0.0f, (float) scrollBarsSize.Width, Math.Max(0.0f, finalSize.Height - (float) scrollBarsSize.Height));
      RectangleF finalRect2 = new RectangleF(x2, finalSize.Height - (float) scrollBarsSize.Height, Math.Max(0.0f, finalSize.Width - (float) scrollBarsSize.Width), (float) scrollBarsSize.Height);
      RectangleF finalRect3 = new RectangleF(x4, finalSize.Height - (float) size2.Height, (float) size2.Width, (float) size2.Height);
      this.ArrangeViewPort(viewportRect, finalSize, scrollingNeeds);
      this.VerticalScrollBar.Arrange(finalRect1);
      this.HorizontalScrollBar.Arrange(finalRect2);
      this.BlankSpot.Arrange(finalRect3);
      this.ResetScrollPos();
      return finalSize;
    }

    protected virtual void ArrangeViewPort(
      RectangleF viewportRect,
      SizeF finalSize,
      RadScrollLayoutPanel.ScrollFlags flags)
    {
      bool flag1 = (flags & RadScrollLayoutPanel.ScrollFlags.Horizontal) == RadScrollLayoutPanel.ScrollFlags.Horizontal;
      bool flag2 = (flags & RadScrollLayoutPanel.ScrollFlags.Vertical) == RadScrollLayoutPanel.ScrollFlags.Vertical;
      if (flag1)
        viewportRect.Width = this.viewport.DesiredSize.Width;
      if (flag2)
        viewportRect.Height = this.viewport.DesiredSize.Height;
      this.viewport.Arrange(viewportRect);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.AutoSizeModeProperty)
      {
        this.SetViewportAutoSizeMode();
      }
      else
      {
        if (e.Property != RadScrollLayoutPanel.ScrollThicknessProperty)
          return;
        this.scrollThicknessCache = (int) e.NewValue;
        this.ResetLayout();
      }
    }

    public void ScrollTo(int xpos, int ypos)
    {
      Point point = this.Value;
      if (xpos < this.MinValue.X)
        xpos = this.MinValue.X;
      int num1 = Math.Min(this.MaxValue.X - this.horizontalScrollBar.LargeChange + 1, this.MaxValue.X);
      if (xpos > num1)
        xpos = num1;
      if (ypos < this.MinValue.Y)
        ypos = this.MinValue.Y;
      int num2 = Math.Min(this.MaxValue.Y - this.verticalScrollBar.LargeChange + 1, this.MaxValue.Y);
      if (ypos > num2)
        ypos = num2;
      this.ScrollWithInternal(xpos - point.X, ypos - point.Y, true);
    }

    public void ScrollWith(int xoffs, int yoffs)
    {
      Point point = Point.Add(this.Value, new Size(xoffs, yoffs));
      if (point.X < this.MinValue.X)
        xoffs = this.MinValue.X - this.Value.X;
      int num1 = Math.Min(this.MaxValue.X - this.horizontalScrollBar.LargeChange + 1, this.MaxValue.X);
      if (point.X > num1)
        xoffs = num1 - this.Value.X;
      if (point.Y < this.MinValue.Y)
        yoffs = this.MinValue.Y - this.Value.Y;
      int num2 = Math.Min(this.MaxValue.Y - this.verticalScrollBar.LargeChange + 1, this.MaxValue.Y);
      if (point.Y > num2)
        yoffs = num2 - this.Value.Y;
      this.ScrollWithInternal(xoffs, yoffs, true);
    }

    public void ScrollElementIntoView(RadElement childElement)
    {
      if (childElement == null)
        return;
      this.UpdateLayout();
      if (this.UsePhysicalScrolling)
      {
        Rectangle dest = new Rectangle(Point.Empty, this.viewportSize);
        Rectangle src = new Rectangle(childElement.BoundingRectangle.Location, Size.Add(childElement.BoundingRectangle.Size, childElement.Margin.Size));
        src.Offset((int) Math.Round((double) this.viewport.PositionOffset.Width), (int) Math.Round((double) this.viewport.PositionOffset.Height));
        Size size = RadCanvasViewport.CalcMinOffset(src, dest);
        this.ScrollWith(-size.Width, -size.Height);
      }
      else
      {
        if (this.viewport == null)
          return;
        Size size = ((IRadScrollViewport) this.viewport).ScrollOffsetForChildVisible(childElement, this.Value);
        this.ScrollWith(size.Width, size.Height);
      }
    }

    private void ScrollWithInternal(int xoffs, int yoffs, bool resetAll)
    {
      if (xoffs == 0 && yoffs == 0)
        return;
      Point point = this.Value;
      Size sz = new Size(xoffs, yoffs);
      Point validValue = this.GetValidValue(Point.Add(point, sz));
      if (resetAll)
      {
        this.SetScrollValueInternal(validValue);
        this.ResetLayout();
      }
      this.DoScroll(point, validValue);
    }

    private void OnScrollBarParameterChanged(object sender, EventArgs e)
    {
      this.OnScrollParametersChanged((RadScrollBarElement) sender);
    }

    private void OnHScroll(object sender, ScrollEventArgs e)
    {
      if (!this.GetBitState(137438953472L))
        return;
      this.ScrollWithInternal(e.NewValue - e.OldValue, 0, false);
    }

    private void OnVScroll(object sender, ScrollEventArgs e)
    {
      if (!this.GetBitState(137438953472L))
        return;
      this.ScrollWithInternal(0, e.NewValue - e.OldValue, false);
    }

    public void ResetLayout()
    {
      this.InvalidateMeasure();
      this.InvalidateArrange();
    }

    protected RadScrollLayoutPanel.ScrollFlags GetScrollingNeeds(
      Size extentSize,
      Size clientSize)
    {
      bool bitState1 = this.GetBitState(549755813888L);
      bool bitState2 = this.GetBitState(1099511627776L);
      this.BitState[549755813888L] = false;
      this.BitState[1099511627776L] = false;
      if (extentSize.Width > clientSize.Width)
      {
        this.BitState[549755813888L] = true;
        if (extentSize.Height > clientSize.Height - this.ScrollThickness)
          this.BitState[1099511627776L] = true;
      }
      else if (extentSize.Width > clientSize.Width - this.ScrollThickness)
      {
        if (extentSize.Height > clientSize.Height)
        {
          this.BitState[549755813888L] = true;
          this.BitState[1099511627776L] = true;
        }
        else if (extentSize.Height > clientSize.Height - this.ScrollThickness)
        {
          if (this.verticalScrollState != ScrollState.AlwaysHide && this.horizontalScrollState != ScrollState.AlwaysHide && (this.horizontalScrollState != ScrollState.AutoHide || this.verticalScrollState != ScrollState.AutoHide) && (this.verticalScrollBar.Visibility != ElementVisibility.Collapsed && this.verticalScrollState == ScrollState.AlwaysShow || this.horizontalScrollBar.Visibility != ElementVisibility.Collapsed && this.horizontalScrollState == ScrollState.AlwaysShow))
          {
            this.BitState[549755813888L] = true;
            this.BitState[1099511627776L] = true;
          }
        }
        else if (this.verticalScrollState == ScrollState.AlwaysShow)
          this.BitState[549755813888L] = true;
      }
      else if (extentSize.Height > clientSize.Height)
        this.BitState[1099511627776L] = true;
      else if (extentSize.Height > clientSize.Height - this.ScrollThickness && this.horizontalScrollState == ScrollState.AlwaysShow)
        this.BitState[1099511627776L] = true;
      bool flag1 = bitState1 != this.GetBitState(549755813888L);
      bool flag2 = bitState2 != this.GetBitState(1099511627776L);
      if (flag1 || flag2)
        this.OnScrollNeedsChanged(new ScrollNeedsEventArgs(bitState1, this.GetBitState(549755813888L), bitState2, this.GetBitState(1099511627776L)));
      RadScrollLayoutPanel.ScrollFlags scrollFlags = (RadScrollLayoutPanel.ScrollFlags) 0;
      if (this.GetBitState(549755813888L))
        scrollFlags |= RadScrollLayoutPanel.ScrollFlags.Horizontal;
      if (this.GetBitState(1099511627776L))
        scrollFlags |= RadScrollLayoutPanel.ScrollFlags.Vertical;
      return scrollFlags;
    }

    private void ResetScrollState(RadScrollLayoutPanel.ScrollFlags flags)
    {
      bool flag1 = (flags & RadScrollLayoutPanel.ScrollFlags.Horizontal) == RadScrollLayoutPanel.ScrollFlags.Horizontal;
      bool flag2 = (flags & RadScrollLayoutPanel.ScrollFlags.Vertical) == RadScrollLayoutPanel.ScrollFlags.Vertical;
      switch (this.horizontalScrollState)
      {
        case ScrollState.AutoHide:
          if (flag1)
            this.horizontalScrollBar.Enabled = true;
          this.horizontalScrollBar.Visibility = flag1 ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          break;
        case ScrollState.AlwaysShow:
          this.horizontalScrollBar.Enabled = flag1;
          this.horizontalScrollBar.Visibility = ElementVisibility.Visible;
          break;
        case ScrollState.AlwaysHide:
          this.horizontalScrollBar.Enabled = flag1;
          this.horizontalScrollBar.Visibility = ElementVisibility.Collapsed;
          break;
      }
      switch (this.verticalScrollState)
      {
        case ScrollState.AutoHide:
          if (flag2)
            this.verticalScrollBar.Enabled = true;
          this.verticalScrollBar.Visibility = flag2 ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          break;
        case ScrollState.AlwaysShow:
          this.verticalScrollBar.Enabled = flag2;
          this.verticalScrollBar.Visibility = ElementVisibility.Visible;
          break;
        case ScrollState.AlwaysHide:
          this.verticalScrollBar.Enabled = flag2;
          this.verticalScrollBar.Visibility = ElementVisibility.Collapsed;
          break;
      }
      if (this.horizontalScrollBar.Visibility == ElementVisibility.Collapsed || this.verticalScrollBar.Visibility == ElementVisibility.Collapsed)
        this.blankSpot.Visibility = ElementVisibility.Collapsed;
      else
        this.blankSpot.Visibility = ElementVisibility.Visible;
    }

    internal void ResetScrollParamsInternal()
    {
      ScrollPanelParameters scrollParams = this.GetScrollParams();
      this.horizontalScrollBar.SetParameters(scrollParams.HorizontalScrollParameters);
      this.verticalScrollBar.SetParameters(scrollParams.VerticalScrollParameters);
    }

    private void ResetScrollPos()
    {
      this.ResetScrollParamsInternal();
      Point oldValue = this.Value;
      Point newValue = oldValue;
      if (this.UsePhysicalScrolling)
      {
        newValue.X = RadCanvasViewport.ValidatePosition(newValue.X, this.extentSize.Width - this.viewportSize.Width);
        newValue.Y = RadCanvasViewport.ValidatePosition(newValue.Y, this.extentSize.Height - this.viewportSize.Height);
      }
      else if (this.viewport != null)
        newValue = ((IRadScrollViewport) this.viewport).ResetValue(this.Value, this.viewportSize, this.extentSize);
      newValue = this.GetValidValue(newValue);
      this.SetScrollValueInternal(newValue);
      if (!(oldValue != newValue) && !(newValue == this.MinValue))
        return;
      this.DoScroll(oldValue, newValue);
    }

    private void OnSizeHScroll(Size clientSize)
    {
      if (this.horizontalScrollBar.Visibility == ElementVisibility.Collapsed)
        return;
      Size size = new Size(this.blankSpot.Visibility == ElementVisibility.Collapsed ? clientSize.Width : clientSize.Width - this.ScrollThickness, this.ScrollThickness);
      this.horizontalScrollBar.SetBounds(this.IsRtl(0, this.ScrollThickness), clientSize.Height - this.ScrollThickness, size.Width, size.Height);
    }

    private void OnSizeVScroll(Size clientSize)
    {
      if (this.verticalScrollBar.Visibility == ElementVisibility.Collapsed)
        return;
      Size size = new Size(this.ScrollThickness, this.blankSpot.Visibility == ElementVisibility.Collapsed ? clientSize.Height : clientSize.Height - this.ScrollThickness);
      this.verticalScrollBar.SetBounds(this.IsRtl(clientSize.Width - this.ScrollThickness, 0), 0, size.Width, size.Height);
    }

    private void OnSizeBlankSpot(Size clientSize)
    {
      if (this.blankSpot.Visibility == ElementVisibility.Collapsed)
        return;
      Size size = new Size(this.ScrollThickness, this.ScrollThickness);
      this.blankSpot.SetBounds(this.IsRtl(clientSize.Width - this.ScrollThickness, 0), clientSize.Height - this.ScrollThickness, size.Width, size.Height);
    }

    private void SetViewportAutoSizeMode()
    {
      if (this.viewport == null)
        return;
      this.viewport.BypassLayoutPolicies = true;
      RadAutoSizeMode autoSizeMode = this.viewport.AutoSizeMode;
      if (this.UsePhysicalScrolling || this.AutoSizeMode == RadAutoSizeMode.WrapAroundChildren)
        this.viewport.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      if (autoSizeMode != this.viewport.AutoSizeMode)
        return;
      this.InvalidateMeasure();
    }

    protected Size GetScrollBarsSize(RadScrollLayoutPanel.ScrollFlags flags)
    {
      int height = 0;
      if (this.horizontalScrollState == ScrollState.AlwaysShow || this.horizontalScrollState == ScrollState.AutoHide && (flags & RadScrollLayoutPanel.ScrollFlags.Horizontal) == RadScrollLayoutPanel.ScrollFlags.Horizontal)
        height = this.ScrollThickness;
      int width = 0;
      if (this.verticalScrollState == ScrollState.AlwaysShow || this.verticalScrollState == ScrollState.AutoHide && (flags & RadScrollLayoutPanel.ScrollFlags.Vertical) == RadScrollLayoutPanel.ScrollFlags.Vertical)
        width = this.ScrollThickness;
      return new Size(width, height);
    }

    private int ValidateRange(int value, int minimum, int maximum)
    {
      if (maximum >= minimum)
      {
        if (value < minimum)
          value = minimum;
        else if (value > maximum)
          value = maximum;
      }
      return value;
    }

    private Point GetValidValue(Point value)
    {
      Point maxValue = this.MaxValue;
      Point minValue = this.MinValue;
      return new Point(this.ValidateRange(value.X, minValue.X, maxValue.X), this.ValidateRange(value.Y, minValue.Y, maxValue.Y));
    }

    private ScrollPanelParameters GetScrollParams()
    {
      if (this.UsePhysicalScrolling)
        return new ScrollPanelParameters(0, Math.Max(1, this.extentSize.Width), this.pixelsPerLineScroll.X, Math.Max(1, this.viewportSize.Width), 0, Math.Max(1, this.extentSize.Height), this.pixelsPerLineScroll.Y, Math.Max(1, this.viewportSize.Height));
      return ((IRadScrollViewport) this.viewport).GetScrollParams(this.viewportSize, this.extentSize);
    }

    internal void SetScrollValueInternal(Point newValue)
    {
      this.BitState[137438953472L] = false;
      this.horizontalScrollBar.Value = newValue.X;
      this.verticalScrollBar.Value = newValue.Y;
      this.BitState[137438953472L] = true;
    }

    private void DoScroll(Point oldValue, Point newValue)
    {
      if (this.viewport == null)
        return;
      this.BitState[274877906944L] = true;
      if (this.UsePhysicalScrolling)
      {
        SizeF scaleFactor = new SizeF(1f / this.DpiScaleFactor.Width, 1f / this.DpiScaleFactor.Height);
        this.viewport.PositionOffset = new SizeF((float) -TelerikDpiHelper.ScaleInt(newValue.X, scaleFactor), (float) -TelerikDpiHelper.ScaleInt(newValue.Y, scaleFactor));
      }
      else
        ((IRadScrollViewport) this.viewport).DoScroll(oldValue, newValue);
      this.BitState[274877906944L] = false;
      this.OnScroll(oldValue, newValue);
    }

    private int IsRtl(int nonRTLvalue, int RTLvalue)
    {
      if (!this.RightToLeft)
        return nonRTLvalue;
      return RTLvalue;
    }

    [Flags]
    public enum ScrollFlags
    {
      Horizontal = 1,
      Vertical = 2,
    }
  }
}
