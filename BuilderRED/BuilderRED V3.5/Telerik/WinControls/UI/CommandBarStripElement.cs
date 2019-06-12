// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarStripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.CommandBarStripElementDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class CommandBarStripElement : RadCommandBarVisualElement
  {
    public static RadProperty DesiredLocationProperty = RadProperty.Register(nameof (DesiredLocation), typeof (PointF), typeof (CommandBarStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new PointF(-1f, -1f), ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange));
    public static RadProperty VisibleInCommandBarProperty = RadProperty.Register(nameof (VisibleInCommandBar), typeof (bool), typeof (CommandBarStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange));
    protected internal PointF cachedDesiredLocation = new PointF(-1f, -1f);
    private bool visibleInCommandBar = true;
    private bool enableDragging = true;
    private Size overflowMenuMinSize = new Size(50, 25);
    private Size overflowMenuMaxSize = new Size(270, 0);
    private const float minMeasureSize = 25f;
    private Size localMinSize;
    private RadCommandBarItemsPanel itemsLayout;
    private RadCommandBarBaseItemCollection items;
    private RadCommandBarGrip grip;
    private RadCommandBarOverflowButton overflowButton;
    private bool enableFloating;
    private CommandBarFloatingForm floatingForm;

    public event CancelEventHandler BeginDrag;

    public event MouseEventHandler Drag;

    public event EventHandler EndDrag;

    public event RadCommandBarBaseItemCollectionItemChangedDelegate ItemsChanged;

    public event EventHandler ItemClicked;

    public event EventHandler ItemOverflowed;

    public event EventHandler ItemOutOfOverflow;

    public event CancelEventHandler OverflowMenuOpening;

    public event EventHandler OverflowMenuOpened;

    public event CancelEventHandler OverflowMenuClosing;

    public event EventHandler OverflowMenuClosed;

    public event CancelEventHandler VisibleInCommandBarChanging;

    public event EventHandler VisibleInCommandBarChanged;

    public event CancelEventHandler ItemVisibleInStripChanging;

    public event EventHandler ItemVisibleInStripChanged;

    public event CancelEventHandler LineChanging;

    public event EventHandler LineChanged;

    public event EventHandler OrientationChanged;

    public event CancelEventHandler OrientationChanging;

    [Description("Gets the form in which the items are placed where the strip is floating.")]
    [Browsable(false)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CommandBarFloatingForm FloatingForm
    {
      get
      {
        return this.floatingForm;
      }
      set
      {
        this.floatingForm = value;
      }
    }

    [Browsable(false)]
    [Description("Gets the layout panel in which the items are arranged.")]
    public RadCommandBarItemsPanel ItemsLayout
    {
      get
      {
        return this.itemsLayout;
      }
    }

    [Description("Gets or sets Overflow menu single strip minimum size.")]
    [DefaultValue(typeof (Size), "50, 25")]
    [Browsable(false)]
    public Size OverflowMenuMinSize
    {
      get
      {
        return new Size((int) Math.Round((double) this.overflowMenuMinSize.Width * (double) this.DpiScaleFactor.Width), (int) Math.Round((double) this.overflowMenuMinSize.Height * (double) this.DpiScaleFactor.Height));
      }
      set
      {
        this.overflowMenuMinSize = value;
      }
    }

    [DefaultValue(typeof (Size), "270, 0")]
    [Description("Gets or sets Overflow menu single strip maximum size.")]
    [Browsable(false)]
    public Size OverflowMenuMaxSize
    {
      get
      {
        return new Size((int) Math.Round((double) this.overflowMenuMaxSize.Width * (double) this.DpiScaleFactor.Width), (int) Math.Round((double) this.overflowMenuMaxSize.Height * (double) this.DpiScaleFactor.Height));
      }
      set
      {
        this.overflowMenuMaxSize = value;
      }
    }

    [DefaultValue(typeof (PointF), "-1,-1")]
    [Category("Behavior")]
    [Description("Gets or sets the desired location of the strip element.")]
    [Browsable(false)]
    public PointF DesiredLocation
    {
      get
      {
        return new PointF(this.cachedDesiredLocation.X * this.DpiScaleFactor.Width, this.cachedDesiredLocation.Y * this.DpiScaleFactor.Height);
      }
      set
      {
        if (this.cachedDesiredLocation == value)
          return;
        this.cachedDesiredLocation = value;
        if (this.IsLayoutSuspended)
          return;
        int num = (int) this.SetValue(CommandBarStripElement.DesiredLocationProperty, (object) value);
        CommandBarRowElement parent = this.Parent as CommandBarRowElement;
        if (this.ShouldChangeLines() && parent != null)
        {
          CancelEventArgs e = new CancelEventArgs();
          this.OnLineChanging(e);
          if (!e.Cancel)
          {
            parent.MoveCommandStripInOtherLine(this);
            this.OnLineChanged(new EventArgs());
          }
        }
        if (this.Parent == null)
          return;
        this.Parent.InvalidateMeasure(true);
      }
    }

    [Description("Gets or sets if the strip can be dragged.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool EnableDragging
    {
      get
      {
        return this.enableDragging;
      }
      set
      {
        this.enableDragging = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets if the strip can be dragged.")]
    [Browsable(true)]
    public bool EnableFloating
    {
      get
      {
        return this.enableFloating;
      }
      set
      {
        this.enableFloating = value;
      }
    }

    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets the delta of the drag.")]
    public PointF Delta
    {
      get
      {
        return this.grip.Delta;
      }
    }

    [Browsable(false)]
    [Description("Gets whether the strip is beeing dragged.")]
    [Category("Appearance")]
    public bool IsDrag
    {
      get
      {
        return this.grip.IsDrag;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets whether the strip is visible in the command bar.")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool VisibleInCommandBar
    {
      get
      {
        return this.visibleInCommandBar;
      }
      set
      {
        if (this.visibleInCommandBar == value || this.OnVisibleInCommandBarChanging(new CancelEventArgs()))
          return;
        this.visibleInCommandBar = value;
        int num1 = (int) this.SetValue(RadElement.MinSizeProperty, (object) (value ? this.localMinSize : Size.Empty));
        int num2 = (int) this.SetValue(CommandBarStripElement.VisibleInCommandBarProperty, (object) value);
        if (this.Parent != null)
          this.Parent.InvalidateMeasure(true);
        this.OnVisibleInCommandBarChanged(new EventArgs());
      }
    }

    [RadPropertyDefaultValue("Orientation", typeof (CommandBarRowElement))]
    [Category("Behavior")]
    [Description("Gets or sets the elements orientation inside the line element.")]
    [Browsable(false)]
    public override Orientation Orientation
    {
      get
      {
        return base.Orientation;
      }
      set
      {
        if (this.Orientation == value || this.OnOrientationChanging(new CancelEventArgs()))
          return;
        this.SetOrientationCore(value);
        this.OnOrientationChanged(new EventArgs());
      }
    }

    [Browsable(false)]
    public bool HasOverflowedItems
    {
      get
      {
        return this.overflowButton.HasOverflowedItems;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the RadCommandBarGrip element of the strip.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadCommandBarGrip Grip
    {
      get
      {
        return this.grip;
      }
      set
      {
        this.grip = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the RadCommandBarOverflowButton element of the strip.")]
    public RadCommandBarOverflowButton OverflowButton
    {
      get
      {
        return this.overflowButton;
      }
      set
      {
        this.overflowButton = value;
      }
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      this.DesiredLocation = this.Orientation != Orientation.Horizontal ? new PointF(this.DesiredLocation.X, this.DesiredLocation.Y + (float) args.Offset.Height) : new PointF(this.DesiredLocation.X + (float) args.Offset.Width, this.DesiredLocation.Y);
      args.Handled = true;
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      if (args.RoutedEvent == RadCommandBarGrip.BeginDraggingEvent)
      {
        CancelEventArgs originalEventArgs = (CancelEventArgs) args.OriginalEventArgs;
        this.OnBeginDragging((object) sender, originalEventArgs);
        args.Canceled = originalEventArgs.Cancel;
      }
      if (args.RoutedEvent == RadCommandBarGrip.EndDraggingEvent)
      {
        EventArgs originalEventArgs = args.OriginalEventArgs;
        this.OnEndDragging((object) sender, originalEventArgs);
      }
      if (args.RoutedEvent == RadCommandBarGrip.DraggingEvent)
      {
        MouseEventArgs originalEventArgs = (MouseEventArgs) args.OriginalEventArgs;
        this.OnDragging((object) sender, originalEventArgs);
      }
      if (args.RoutedEvent == RadCommandBarBaseItem.ClickEvent)
        this.OnItemClicked((object) sender, args.OriginalEventArgs);
      if (args.RoutedEvent == RadCommandBarBaseItem.VisibleInStripChangedEvent)
        this.OnItemVisibleInStripChanged((object) sender, args.OriginalEventArgs);
      if (args.RoutedEvent == RadCommandBarBaseItem.VisibleInStripChangingEvent)
        this.OnItemVisibleInStripChanging((object) sender, args.OriginalEventArgs as CancelEventArgs);
      base.OnBubbleEvent(sender, args);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      availableSize = this.GetClientRectangle(availableSize).Size;
      if (!this.visibleInCommandBar && this.Site == null || this.Visibility == ElementVisibility.Collapsed)
      {
        this.grip.Measure(SizeF.Empty);
        this.itemsLayout.Measure(SizeF.Empty);
        this.overflowButton.Measure(SizeF.Empty);
        return SizeF.Empty;
      }
      SizeF sizeF = (SizeF) this.localMinSize;
      if (this.Orientation == Orientation.Horizontal)
      {
        this.grip.Measure(availableSize);
        float width1 = availableSize.Width - this.grip.DesiredSize.Width;
        this.overflowButton.Measure(new SizeF(width1, availableSize.Height));
        this.itemsLayout.Measure(new SizeF(width1 - this.overflowButton.DesiredSize.Width, availableSize.Height));
        float height = Math.Max(Math.Max(this.grip.DesiredSize.Height, this.overflowButton.DesiredSize.Height), this.itemsLayout.DesiredSize.Height);
        float width2 = Math.Max(this.grip.DesiredSize.Width + this.overflowButton.DesiredSize.Width + this.itemsLayout.DesiredSize.Width, 25f);
        if (!float.IsInfinity(availableSize.Height))
          height = availableSize.Height;
        sizeF = new SizeF(width2, height);
      }
      else
      {
        this.grip.Measure(new SizeF(availableSize.Width, availableSize.Height));
        float height1 = availableSize.Height - this.grip.DesiredSize.Height;
        this.overflowButton.Measure(new SizeF(availableSize.Width, height1));
        float height2 = height1 - this.overflowButton.DesiredSize.Height;
        this.itemsLayout.Measure(new SizeF(availableSize.Width, height2));
        float width = Math.Max(Math.Max(this.grip.DesiredSize.Width, this.overflowButton.DesiredSize.Width), this.itemsLayout.DesiredSize.Width);
        float height3 = Math.Max(this.grip.DesiredSize.Height + this.overflowButton.DesiredSize.Height + this.itemsLayout.DesiredSize.Height, 25f);
        if (!float.IsInfinity(availableSize.Width))
          width = availableSize.Width;
        sizeF = new SizeF(width, height3);
      }
      Padding borderThickness = this.GetBorderThickness(true);
      sizeF.Width += (float) (borderThickness.Left + borderThickness.Right + this.Padding.Left + this.Padding.Right + this.Margin.Left + this.Margin.Right);
      sizeF.Height += (float) (borderThickness.Top + borderThickness.Bottom + this.Padding.Top + this.Padding.Bottom + this.Margin.Top + this.Margin.Bottom);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.Orientation == Orientation.Horizontal)
      {
        PointF empty = PointF.Empty;
        if (this.RightToLeft)
        {
          this.overflowButton.Arrange(new RectangleF(empty, new SizeF(this.overflowButton.DesiredSize.Width, finalSize.Height)));
          empty.X += this.overflowButton.DesiredSize.Width;
        }
        else
        {
          this.grip.Arrange(new RectangleF(empty, new SizeF(this.grip.DesiredSize.Width, finalSize.Height)));
          empty.X += this.grip.DesiredSize.Width;
        }
        float width = this.itemsLayout.StretchHorizontally ? finalSize.Width - this.grip.DesiredSize.Width - this.overflowButton.DesiredSize.Width : this.itemsLayout.DesiredSize.Width;
        this.itemsLayout.Arrange(new RectangleF(new PointF(empty.X, clientRectangle.Y), new SizeF(width, clientRectangle.Height)));
        empty.X += this.itemsLayout.DesiredSize.Width;
        empty.X = Math.Max(empty.X, finalSize.Width - this.overflowButton.DesiredSize.Width);
        if (!this.RightToLeft)
        {
          this.overflowButton.Arrange(new RectangleF(empty, new SizeF(this.overflowButton.DesiredSize.Width, finalSize.Height)));
          empty.X += this.overflowButton.DesiredSize.Width;
        }
        else
        {
          this.grip.Arrange(new RectangleF(empty, new SizeF(this.grip.DesiredSize.Width, finalSize.Height)));
          empty.X += this.grip.DesiredSize.Width;
        }
        return finalSize;
      }
      PointF empty1 = PointF.Empty;
      this.grip.Arrange(new RectangleF(empty1, new SizeF(finalSize.Width, this.grip.DesiredSize.Height)));
      empty1.Y += this.grip.DesiredSize.Height;
      float height = this.itemsLayout.StretchVertically ? finalSize.Height - this.grip.DesiredSize.Height - this.overflowButton.DesiredSize.Height : this.itemsLayout.DesiredSize.Height;
      this.itemsLayout.Arrange(new RectangleF(new PointF(clientRectangle.X, empty1.Y), new SizeF(clientRectangle.Width, height)));
      empty1.Y += this.itemsLayout.DesiredSize.Height;
      empty1.Y = Math.Max(empty1.Y, finalSize.Height - this.overflowButton.DesiredSize.Height);
      this.overflowButton.Arrange(new RectangleF(empty1, new SizeF(finalSize.Width, this.overflowButton.DesiredSize.Height)));
      empty1.Y += this.overflowButton.DesiredSize.Height;
      return finalSize;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.MinSize = new Size(30, 30);
      this.DrawBorder = true;
      this.Text = "";
      int num1 = (int) this.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
      int num2 = (int) this.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      this.items = new RadCommandBarBaseItemCollection();
      this.grip = new RadCommandBarGrip(this);
      this.overflowButton = this.CreateCommandBarOverflowButton();
      this.itemsLayout = new RadCommandBarItemsPanel(this.items, this.overflowButton.ItemsLayout);
      this.items.Owner = (RadElement) this.itemsLayout;
      this.items.ItemTypes = new System.Type[10]
      {
        typeof (CommandBarButton),
        typeof (CommandBarDropDownButton),
        typeof (CommandBarDropDownList),
        typeof (CommandBarHostItem),
        typeof (CommandBarSeparator),
        typeof (CommandBarLabel),
        typeof (CommandBarTextBox),
        typeof (CommandBarToggleButton),
        typeof (CommandBarSplitButton),
        typeof (CommandBarMaskedEditBox)
      };
      this.Children.Add((RadElement) this.grip);
      this.Children.Add((RadElement) this.itemsLayout);
      this.Children.Add((RadElement) this.overflowButton);
      this.WireEvents();
      this.localMinSize = this.MinSize;
    }

    protected virtual RadCommandBarOverflowButton CreateCommandBarOverflowButton()
    {
      return new RadCommandBarOverflowButton(this);
    }

    [DefaultValue("")]
    public override string Text
    {
      get
      {
        return "";
      }
      set
      {
        base.Text = value;
      }
    }

    [DefaultValue(typeof (Size), "30, 30")]
    public override Size MinSize
    {
      get
      {
        return base.MinSize;
      }
      set
      {
        this.localMinSize = value;
        if (!this.visibleInCommandBar && this.Site == null)
          return;
        base.MinSize = value;
      }
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
    }

    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RadNewItem("", false, false, false)]
    [RefreshProperties(RefreshProperties.All)]
    public RadCommandBarBaseItemCollection Items
    {
      get
      {
        return this.items;
      }
    }

    protected virtual void OnBeginDragging(object sender, CancelEventArgs args)
    {
      if (this.BeginDrag == null)
        return;
      this.BeginDrag(sender, args);
    }

    protected virtual void OnEndDragging(object sender, EventArgs args)
    {
      if (this.EndDrag == null)
        return;
      this.EndDrag(sender, args);
    }

    protected virtual void OnDragging(object sender, MouseEventArgs args)
    {
      if (this.Drag == null)
        return;
      this.Drag(sender, args);
    }

    protected virtual void OnOverflowMenuOpening(object sender, CancelEventArgs e)
    {
      if (this.OverflowMenuOpening == null)
        return;
      this.OverflowMenuOpening((object) this, e);
    }

    protected virtual void OnOverflowMenuOpened(object sender, EventArgs e)
    {
      if (this.OverflowMenuOpened == null)
        return;
      this.OverflowMenuOpened((object) this, e);
    }

    protected virtual void OnOverflowMenuClosing(object sender, CancelEventArgs e)
    {
      if (this.OverflowMenuClosing == null)
        return;
      this.OverflowMenuClosing((object) this, e);
    }

    protected virtual void OnOverflowMenuClosed(object sender, EventArgs e)
    {
      if (this.OverflowMenuClosed == null)
        return;
      this.OverflowMenuClosed((object) this, e);
    }

    protected virtual void OnItemsChanged(
      RadCommandBarBaseItemCollection changed,
      RadCommandBarBaseItem target,
      ItemsChangeOperation operation)
    {
      if (this.ItemsChanged == null)
        return;
      this.ItemsChanged(changed, target, operation);
    }

    protected internal virtual void OnItemClicked(object sender, EventArgs e)
    {
      if (this.ItemClicked == null)
        return;
      this.ItemClicked(sender, e);
    }

    protected virtual bool OnVisibleInCommandBarChanging(CancelEventArgs e)
    {
      if (this.VisibleInCommandBarChanging == null)
        return false;
      this.VisibleInCommandBarChanging((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnVisibleInCommandBarChanged(EventArgs e)
    {
      if (this.VisibleInCommandBarChanged == null)
        return;
      this.VisibleInCommandBarChanged((object) this, e);
    }

    protected virtual bool OnLineChanging(CancelEventArgs e)
    {
      if (this.LineChanging == null)
        return false;
      this.LineChanging((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnLineChanged(EventArgs e)
    {
      if (this.LineChanged == null)
        return;
      this.LineChanged((object) this, e);
    }

    protected virtual void OnItemOverflowed(object sender, EventArgs e)
    {
      if (this.ItemOverflowed == null)
        return;
      this.ItemOverflowed(sender, e);
    }

    protected virtual void OnItemOutOfOverflow(object sender, EventArgs e)
    {
      if (this.ItemOutOfOverflow == null)
        return;
      this.ItemOutOfOverflow(sender, e);
    }

    protected internal virtual bool OnItemVisibleInStripChanging(object sender, CancelEventArgs e)
    {
      if (this.ItemVisibleInStripChanging == null)
        return false;
      if (e == null)
        e = new CancelEventArgs();
      this.ItemVisibleInStripChanging(sender, e);
      return e.Cancel;
    }

    protected internal virtual void OnItemVisibleInStripChanged(object sender, EventArgs e)
    {
      if (this.ItemVisibleInStripChanged == null)
        return;
      this.ItemVisibleInStripChanged(sender, e);
    }

    protected virtual void OnOrientationChanged(EventArgs e)
    {
      if (this.OrientationChanged == null)
        return;
      this.OrientationChanged((object) this, e);
    }

    protected virtual bool OnOrientationChanging(CancelEventArgs e)
    {
      if (this.OrientationChanging == null)
        return false;
      this.OrientationChanging((object) this, e);
      return e.Cancel;
    }

    private bool ShouldChangeLines()
    {
      return this.Orientation == Orientation.Horizontal && ((double) this.DesiredLocation.Y < (double) (this.ControlBoundingRectangle.Top - SystemInformation.DragSize.Height * 2) || (double) this.DesiredLocation.Y > (double) (this.ControlBoundingRectangle.Bottom + SystemInformation.DragSize.Height * 2)) || this.Orientation == Orientation.Vertical && ((double) this.DesiredLocation.X < (double) (this.ControlBoundingRectangle.Left - SystemInformation.DragSize.Height * 2) || (double) this.DesiredLocation.X > (double) (this.ControlBoundingRectangle.Right + SystemInformation.DragSize.Height * 2));
    }

    protected internal void ForceEndDrag()
    {
      this.grip.EndDrag();
    }

    protected internal void ForceBeginDrag()
    {
      this.grip.BeginDrag(new MouseEventArgs(MouseButtons.Left, 1, this.Location.X, this.Location.Y, 0));
    }

    protected override Image GetDragHintCore()
    {
      return (Image) null;
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      return base.CanDragCore(dragStartPoint);
    }

    public SizeF GetExpectedSize(SizeF availableSize)
    {
      if (!this.visibleInCommandBar || this.Visibility == ElementVisibility.Collapsed)
        return new SizeF(0.0f, 0.0f);
      SizeF expectedSize = this.itemsLayout.GetExpectedSize(availableSize);
      this.overflowButton.Measure(availableSize);
      this.grip.Measure(availableSize);
      if (this.Orientation == Orientation.Horizontal)
      {
        expectedSize.Width += this.overflowButton.DesiredSize.Width + this.grip.DesiredSize.Width;
        expectedSize.Height = Math.Max(this.overflowButton.DesiredSize.Height, expectedSize.Height);
        expectedSize.Height = Math.Max(this.grip.DesiredSize.Height, expectedSize.Height);
      }
      else
      {
        expectedSize.Height += this.overflowButton.DesiredSize.Height + this.grip.DesiredSize.Height;
        expectedSize.Width = Math.Max(this.overflowButton.DesiredSize.Width, expectedSize.Width);
        expectedSize.Width = Math.Max(this.grip.DesiredSize.Width, expectedSize.Width);
      }
      Padding borderThickness = this.GetBorderThickness(true);
      expectedSize.Width += (float) (borderThickness.Left + borderThickness.Right + this.Padding.Left + this.Padding.Right + this.Margin.Left + this.Margin.Right);
      expectedSize.Height += (float) (borderThickness.Top + borderThickness.Bottom + this.Padding.Top + this.Padding.Bottom + this.Margin.Top + this.Margin.Bottom);
      return expectedSize;
    }

    protected virtual void WireEvents()
    {
      this.items.ItemsChanged += new RadCommandBarBaseItemCollectionItemChangedDelegate(this.OnItemsChanged);
      this.overflowButton.OverflowMenuOpening += new CancelEventHandler(this.OnOverflowMenuOpening);
      this.overflowButton.OverflowMenuOpened += new EventHandler(this.OnOverflowMenuOpened);
      this.overflowButton.OverflowMenuClosing += new CancelEventHandler(this.OnOverflowMenuClosing);
      this.overflowButton.OverflowMenuClosed += new EventHandler(this.OnOverflowMenuClosed);
      this.itemsLayout.ItemOverflowed += new EventHandler(this.OnItemOverflowed);
      this.itemsLayout.ItemOutOfOverflow += new EventHandler(this.OnItemOutOfOverflow);
    }

    protected virtual void UnwireEvents()
    {
      this.items.ItemsChanged -= new RadCommandBarBaseItemCollectionItemChangedDelegate(this.OnItemsChanged);
      this.overflowButton.OverflowMenuOpening -= new CancelEventHandler(this.OnOverflowMenuOpening);
      this.overflowButton.OverflowMenuOpened -= new EventHandler(this.OnOverflowMenuOpened);
      this.overflowButton.OverflowMenuClosing -= new CancelEventHandler(this.OnOverflowMenuClosing);
      this.overflowButton.OverflowMenuClosed -= new EventHandler(this.OnOverflowMenuClosed);
      this.itemsLayout.ItemOverflowed -= new EventHandler(this.OnItemOverflowed);
      this.itemsLayout.ItemOutOfOverflow -= new EventHandler(this.OnItemOutOfOverflow);
    }

    protected internal void SetOrientationCore(Orientation value)
    {
      this.itemsLayout.Orientation = value;
      this.grip.Orientation = value;
      this.overflowButton.Orientation = value;
      this.cachedDesiredLocation = new PointF(this.DesiredLocation.Y, this.DesiredLocation.X);
      this.cachedOrientation = value;
      if (value == Orientation.Vertical)
        this.GradientAngle += 90f;
      else
        this.GradientAngle -= 90f;
      bool stretchHorizontally = this.StretchHorizontally;
      this.StretchHorizontally = this.StretchVertically;
      this.StretchVertically = stretchHorizontally;
    }
  }
}
