// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPanoramaElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadPanoramaElement : LightVisualElement
  {
    public static RadProperty ScrollBarThicknessProperty = RadProperty.Register(nameof (ScrollBarThickness), typeof (int), typeof (RadPanoramaElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 16, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private Size cellSize = new Size(100, 100);
    private Size backgroundImageSize = Size.Empty;
    private double currentZoom = 1.0;
    private bool zoomedIn = true;
    private bool allowDragDrop = true;
    private bool enableZooming = true;
    private bool autoArrangeNewTiles = true;
    private PanoramaMouseWheelBehavior wheelBehavior = PanoramaMouseWheelBehavior.Zoom;
    private GridLayout itemsLayout;
    private StackLayoutPanel groupsLayout;
    private RadScrollBarElement scrollBar;
    private RadItemOwnerCollection items;
    private RadItemOwnerCollection groups;
    private RadDragDropService dragDropService;
    private ImagePrimitive backgroundImage;
    private ScrollService scrollService;
    private HorizontalScrollAlignment scrollBarAlignment;
    private bool showGroups;
    private bool scrollingBackground;
    private int minimumColumns;
    private DateTime lastScroll;
    private int bufferedSteps;

    [DefaultValue(PanoramaMouseWheelBehavior.Zoom)]
    public PanoramaMouseWheelBehavior MouseWheelBehavior
    {
      get
      {
        return this.wheelBehavior;
      }
      set
      {
        this.wheelBehavior = value;
      }
    }

    public bool AutoArrangeNewTiles
    {
      get
      {
        return this.autoArrangeNewTiles;
      }
      set
      {
        this.autoArrangeNewTiles = value;
      }
    }

    public bool EnableZooming
    {
      get
      {
        return this.enableZooming;
      }
      set
      {
        this.enableZooming = value;
      }
    }

    public bool ZoomedOut
    {
      get
      {
        return !this.zoomedIn;
      }
    }

    public int MinimumColumns
    {
      get
      {
        return this.minimumColumns;
      }
      set
      {
        if (this.minimumColumns == value)
          return;
        this.minimumColumns = value;
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (MinimumColumns));
      }
    }

    public bool AllowDragDrop
    {
      get
      {
        return this.allowDragDrop;
      }
      set
      {
        if (this.allowDragDrop == value)
          return;
        this.allowDragDrop = value;
        this.OnNotifyPropertyChanged(nameof (AllowDragDrop));
      }
    }

    public bool ShowGroups
    {
      get
      {
        return this.showGroups;
      }
      set
      {
        if (this.showGroups == value)
          return;
        this.showGroups = value;
        if (this.showGroups)
        {
          this.Children.Remove((RadElement) this.itemsLayout);
          this.Children.Add((RadElement) this.groupsLayout);
          this.InvalidateMeasure(true);
        }
        else
        {
          this.Children.Remove((RadElement) this.groupsLayout);
          this.Children.Add((RadElement) this.itemsLayout);
          this.InvalidateMeasure(true);
        }
        this.OnNotifyPropertyChanged(nameof (ShowGroups));
      }
    }

    public bool ScrollingBackground
    {
      get
      {
        return this.scrollingBackground;
      }
      set
      {
        if (value == this.scrollingBackground)
          return;
        this.scrollingBackground = value;
        this.UpdateViewOnScroll();
        this.OnNotifyPropertyChanged(nameof (ScrollingBackground));
      }
    }

    public HorizontalScrollAlignment ScrollBarAlignment
    {
      get
      {
        return this.scrollBarAlignment;
      }
      set
      {
        if (this.scrollBarAlignment == value)
          return;
        this.scrollBarAlignment = value;
        this.UpdateViewOnScroll();
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (ScrollBarAlignment));
      }
    }

    public int ScrollBarThickness
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadPanoramaElement.ScrollBarThicknessProperty), this.DpiScaleFactor);
      }
      set
      {
        if (this.ScrollBarThickness == value)
          return;
        int num = (int) this.SetValue(RadPanoramaElement.ScrollBarThicknessProperty, (object) value);
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (ScrollBarThickness));
      }
    }

    public Image PanelImage
    {
      get
      {
        return this.backgroundImage.Image;
      }
      set
      {
        Image image = this.backgroundImage.Image;
        this.backgroundImage.Image = value;
        if (image == value)
          return;
        this.OnNotifyPropertyChanged(nameof (PanelImage));
      }
    }

    public Size PanelImageSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.backgroundImageSize, this.DpiScaleFactor);
      }
      set
      {
        if (!(this.backgroundImageSize != value))
          return;
        this.backgroundImageSize = value;
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (PanelImageSize));
      }
    }

    public int ColumnsCount
    {
      get
      {
        return this.itemsLayout.Columns.Count;
      }
      set
      {
        if (this.MinimumColumns != 0)
          value = Math.Max(value, this.MinimumColumns);
        if (this.itemsLayout.Columns.Count == value)
          return;
        List<GridLayoutColumn> gridLayoutColumnList = new List<GridLayoutColumn>(value);
        for (int index = 0; index < value; ++index)
        {
          GridLayoutColumn gridLayoutColumn = new GridLayoutColumn();
          gridLayoutColumn.FixedWidth = (float) this.CellSize.Width;
          gridLayoutColumn.SizingType = GridLayoutSizingType.Fixed;
          gridLayoutColumnList.Add(gridLayoutColumn);
        }
        this.itemsLayout.Columns = gridLayoutColumnList;
        this.OnNotifyPropertyChanged(nameof (ColumnsCount));
      }
    }

    public int RowsCount
    {
      get
      {
        return this.itemsLayout.Rows.Count;
      }
      set
      {
        if (this.itemsLayout.Rows.Count == value)
          return;
        List<GridLayoutRow> gridLayoutRowList = new List<GridLayoutRow>(value);
        for (int index = 0; index < value; ++index)
        {
          GridLayoutRow gridLayoutRow = new GridLayoutRow();
          gridLayoutRow.FixedHeight = (float) this.CellSize.Height;
          gridLayoutRow.SizingType = GridLayoutSizingType.Fixed;
          gridLayoutRowList.Add(gridLayoutRow);
        }
        this.itemsLayout.Rows = gridLayoutRowList;
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (RowsCount));
      }
    }

    public Size CellSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize(this.cellSize, this.DpiScaleFactor);
      }
      set
      {
        if (!(value != this.cellSize))
          return;
        this.cellSize = value;
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (CellSize));
      }
    }

    public RadScrollBarElement ScrollBar
    {
      get
      {
        return this.scrollBar;
      }
    }

    public ImagePrimitive BackgroundImagePrimitive
    {
      get
      {
        return this.backgroundImage;
      }
    }

    public GridLayout TileLayout
    {
      get
      {
        return this.itemsLayout;
      }
    }

    public StackLayoutPanel GroupLayout
    {
      get
      {
        return this.groupsLayout;
      }
    }

    public RadDragDropService DragDropService
    {
      get
      {
        return this.dragDropService;
      }
      set
      {
        if (value == this.dragDropService)
          return;
        if (this.dragDropService.State == RadServiceState.Working)
          this.dragDropService.Stop(false);
        this.dragDropService = value;
        this.OnNotifyPropertyChanged(nameof (DragDropService));
      }
    }

    public ScrollService ScrollService
    {
      get
      {
        return this.scrollService;
      }
    }

    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    public RadItemOwnerCollection Groups
    {
      get
      {
        return this.groups;
      }
    }

    public void ScrollView(int offset)
    {
      this.ScrollView(offset, false);
    }

    public void ScrollView(int offset, bool buffered)
    {
      DateTime now = DateTime.Now;
      this.bufferedSteps += offset;
      if (buffered && now - this.lastScroll < TimeSpan.FromMilliseconds(50.0))
        return;
      this.lastScroll = now;
      int num = this.scrollBar.Value - this.bufferedSteps;
      this.bufferedSteps = 0;
      if (num > this.scrollBar.Maximum - this.scrollBar.LargeChange + 1)
        num = this.scrollBar.Maximum - this.scrollBar.LargeChange + 1;
      if (num < this.scrollBar.Minimum)
        num = this.scrollBar.Minimum;
      this.scrollBar.Value = num;
    }

    public void UpdateViewOnScroll()
    {
      SizeF panelSize = this.GetPanelSize();
      if (this.zoomedIn)
      {
        this.GetCurrentLayout().PositionOffset = new SizeF((float) -this.scrollBar.Value, 0.0f);
        if (!this.ScrollingBackground)
        {
          this.backgroundImage.PositionOffset = SizeF.Empty;
        }
        else
        {
          if ((double) panelSize.Width <= (double) this.scrollBar.LargeChange)
            return;
          this.backgroundImage.PositionOffset = new SizeF((float) ((double) -this.scrollBar.Value * (double) (this.backgroundImageSize.Width - this.scrollBar.LargeChange) / ((double) panelSize.Width - (double) this.scrollBar.LargeChange)), 0.0f);
        }
      }
      else
        this.GetCurrentLayout().PositionOffset = SizeF.Empty;
    }

    public void ZoomOut()
    {
      if (!this.enableZooming || !this.zoomedIn)
        return;
      this.scrollService.Stop();
      this.Capture = false;
      this.PauseAnimatedTiles();
      LayoutPanel currentLayout = this.GetCurrentLayout();
      SizeF sizeF1 = new SizeF((float) this.Bounds.Width / (float) currentLayout.Bounds.Width, (float) this.Bounds.Width / (float) currentLayout.Bounds.Width);
      new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) new SizeF(1f, 1f), (object) sizeF1, 5, 20).ApplyValue((RadObject) currentLayout);
      SizeF sizeF2 = new SizeF(0.0f, (float) (((double) this.Bounds.Height - (double) currentLayout.Bounds.Height * (double) sizeF1.Height) / 2.0));
      new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) currentLayout.PositionOffset, (object) sizeF2, 5, 20).ApplyValue((RadObject) currentLayout);
      this.zoomedIn = false;
    }

    public void ZoomIn(Point location)
    {
      if (!this.enableZooming || this.zoomedIn)
        return;
      LayoutPanel currentLayout = this.GetCurrentLayout();
      int num = (int) ((double) -location.X * 1.0 / (double) currentLayout.ScaleTransform.Width + (double) location.X);
      new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) currentLayout.ScaleTransform, (object) new SizeF(1f, 1f), 5, 20)
      {
        RemoveAfterApply = true
      }.ApplyValue((RadObject) currentLayout);
      AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) currentLayout.PositionOffset, (object) new SizeF((float) num, 0.0f), 5, 20);
      animatedPropertySetting.RemoveAfterApply = true;
      animatedPropertySetting.AnimationFinished += new AnimationFinishedEventHandler(this.zoomInOffset_AnimationFinished);
      animatedPropertySetting.ApplyValue((RadObject) currentLayout);
      this.zoomedIn = true;
    }

    protected virtual void UpdateScrollBar(SizeF availableSize)
    {
      bool flag = this.scrollBar.ScrollType == ScrollType.Horizontal;
      SizeF panelSize = this.GetPanelSize();
      this.scrollBar.Maximum = flag ? (int) panelSize.Width : (int) panelSize.Height;
      this.scrollBar.Minimum = 0;
      this.scrollBar.LargeChange = flag ? (int) availableSize.Width : (int) availableSize.Height;
      this.scrollBar.SmallChange = 1;
      if (this.scrollBar.LargeChange >= this.scrollBar.Maximum)
        this.scrollBar.Value = 0;
      else if (this.scrollBar.Value > this.scrollBar.Maximum - this.scrollBar.LargeChange + 1)
        this.scrollBar.Value = this.scrollBar.Maximum - this.scrollBar.LargeChange + 1;
      if (this.ScrollBarThickness == 0)
        this.scrollBar.Visibility = ElementVisibility.Hidden;
      else if (this.scrollBar.LargeChange >= this.scrollBar.Maximum)
        this.scrollBar.Visibility = ElementVisibility.Hidden;
      else
        this.scrollBar.Visibility = ElementVisibility.Visible;
    }

    protected virtual void UpdateCellCount()
    {
      if (this.dragDropService.State == RadServiceState.Working)
        return;
      int num = 1;
      int val1 = 1;
      foreach (RadElement radElement in (RadItemCollection) this.Items)
      {
        int val2_1 = (int) radElement.GetValue(GridLayout.ColumnIndexProperty) + (int) radElement.GetValue(GridLayout.ColSpanProperty);
        int val2_2 = (int) radElement.GetValue(GridLayout.RowIndexProperty) + (int) radElement.GetValue(GridLayout.RowSpanProperty);
        num = Math.Max(num, val2_1);
        val1 = Math.Max(val1, val2_2);
      }
      if (this.MinimumColumns > 0)
        num = Math.Max(this.MinimumColumns, num);
      this.ColumnsCount = num;
      if (!this.IsDesignMode || this.RowsCount >= val1)
        return;
      this.RowsCount = val1;
    }

    private SizeF GetPanelSize()
    {
      this.UpdateCellCount();
      if (this.ShowGroups)
        return this.groupsLayout.DesiredSize;
      return new SizeF((float) (this.ColumnsCount * this.CellSize.Width) * this.itemsLayout.ScaleTransform.Width, (float) (this.RowsCount * this.CellSize.Height) * this.itemsLayout.ScaleTransform.Height);
    }

    private void PauseAnimatedTiles()
    {
      foreach (RadTileElement radTileElement in (RadItemCollection) this.Items)
        (radTileElement as RadLiveTileElement)?.PauseAnimations();
      foreach (TileGroupElement group in (RadItemCollection) this.groups)
      {
        foreach (RadTileElement radTileElement in (RadItemCollection) group.Items)
          (radTileElement as RadLiveTileElement)?.PauseAnimations();
      }
    }

    private void ContinueAnimatedTiles()
    {
      foreach (RadTileElement radTileElement in (RadItemCollection) this.Items)
        (radTileElement as RadLiveTileElement)?.ContinueAnimations();
      foreach (TileGroupElement group in (RadItemCollection) this.groups)
      {
        foreach (RadTileElement radTileElement in (RadItemCollection) group.Items)
          (radTileElement as RadLiveTileElement)?.ContinueAnimations();
      }
    }

    private LayoutPanel GetCurrentLayout()
    {
      if (this.ShowGroups)
        return (LayoutPanel) this.groupsLayout;
      return (LayoutPanel) this.itemsLayout;
    }

    public void ScrollToItem(RadTileElement tile)
    {
      this.ScrollToItem(tile, 0);
    }

    public void ScrollToItem(RadTileElement tile, int desiredOffset)
    {
      int num = tile.ControlBoundingRectangle.X - this.scrollBar.ControlBoundingRectangle.X - desiredOffset + this.scrollBar.Value;
      if (num > this.scrollBar.Maximum - this.scrollBar.LargeChange + 1)
        num = this.scrollBar.Maximum - this.scrollBar.LargeChange + 1;
      else if (num < 0)
        num = 0;
      this.scrollBar.Value = num;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.scrollBar = new RadScrollBarElement();
      this.scrollBar.ZIndex = 1000;
      this.scrollBar.ScrollType = ScrollType.Horizontal;
      this.scrollBar.ValueChanged += new EventHandler(this.scrollBar_ValueChanged);
      this.Children.Add((RadElement) this.scrollBar);
      this.itemsLayout = new GridLayout();
      this.itemsLayout.StretchHorizontally = this.itemsLayout.StretchVertically = true;
      this.itemsLayout.RadPropertyChanged += new RadPropertyChangedEventHandler(this.layout_RadPropertyChanged);
      this.Children.Add((RadElement) this.itemsLayout);
      this.groupsLayout = new StackLayoutPanel();
      this.groupsLayout.RadPropertyChanged += new RadPropertyChangedEventHandler(this.layout_RadPropertyChanged);
      this.backgroundImage = new ImagePrimitive();
      this.backgroundImage.Class = "PanoramaBackgroundImage";
      this.backgroundImage.ZIndex = -5;
      this.backgroundImage.ImageLayout = ImageLayout.Stretch;
      this.backgroundImage.StretchHorizontally = this.backgroundImage.StretchVertically = true;
      this.Children.Add((RadElement) this.backgroundImage);
      this.items = new RadItemOwnerCollection();
      this.items.ItemsChanged += new ItemChangedDelegate(this.items_ItemsChanged);
      this.items.ItemTypes = new System.Type[2]
      {
        typeof (RadTileElement),
        typeof (RadLiveTileElement)
      };
      this.items.Owner = (RadElement) this.itemsLayout;
      this.groups = new RadItemOwnerCollection();
      this.groups.ItemTypes = new System.Type[1]
      {
        typeof (TileGroupElement)
      };
      this.groups.Owner = (RadElement) this.groupsLayout;
      this.dragDropService = (RadDragDropService) new TileDragDropService(this);
      this.scrollService = new ScrollService((RadElement) this, this.scrollBar);
      this.scrollService.EnableInertia = true;
      this.AllowDrop = true;
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF availableSize1 = this.GetPanelSize();
      base.MeasureOverride(availableSize);
      this.scrollBar.Measure(new SizeF(this.GetClientRectangle(availableSize).Size.Width, (float) this.ScrollBarThickness));
      if (this.showGroups)
      {
        this.GetCurrentLayout().Measure(LayoutUtils.InfinitySize);
        availableSize1 = this.GetCurrentLayout().DesiredSize;
      }
      else
        this.GetCurrentLayout().Measure(availableSize1);
      this.backgroundImage.Measure((SizeF) this.backgroundImageSize);
      if (float.IsInfinity(availableSize.Width))
        availableSize.Width = availableSize1.Width;
      if (float.IsInfinity(availableSize.Height))
      {
        availableSize.Height = availableSize1.Height;
        availableSize.Height += (float) this.ScrollBarThickness;
      }
      this.UpdateScrollBar(availableSize);
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      switch (this.scrollBarAlignment)
      {
        case HorizontalScrollAlignment.Top:
          this.scrollBar.Arrange(new RectangleF(clientRectangle.Location, new SizeF(clientRectangle.Width, (float) this.ScrollBarThickness)));
          clientRectangle.Height -= (float) this.ScrollBarThickness;
          clientRectangle.Y += this.scrollBar.Visibility == ElementVisibility.Visible ? (float) this.ScrollBarThickness : 0.0f;
          break;
        case HorizontalScrollAlignment.Bottom:
          this.scrollBar.Arrange(new RectangleF(new PointF(clientRectangle.Left, clientRectangle.Bottom - (float) this.ScrollBarThickness), new SizeF(clientRectangle.Width, (float) this.ScrollBarThickness)));
          clientRectangle.Height -= this.scrollBar.Visibility == ElementVisibility.Visible ? (float) this.ScrollBarThickness : 0.0f;
          break;
      }
      this.GetCurrentLayout().Arrange(new RectangleF(clientRectangle.Location, this.GetPanelSize()));
      this.backgroundImage.Arrange(new RectangleF(clientRectangle.Location, (SizeF) this.backgroundImageSize));
      return finalSize;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      if (!this.zoomedIn || args.Handled)
        return;
      RadTileElement elementAtPoint = this.ElementTree.GetElementAtPoint(args.Location) as RadTileElement;
      if (args.IsBegin && elementAtPoint != null && elementAtPoint.AllowDrag)
        return;
      this.ScrollView(args.Offset.Width, true);
      args.Handled = true;
    }

    protected override void OnZoomGesture(ZoomGestureEventArgs args)
    {
      if (!this.enableZooming)
        return;
      if (args.IsBegin)
        this.currentZoom = 1.0;
      this.currentZoom *= args.ZoomFactor;
      if (this.currentZoom < 0.75)
        this.ZoomOut();
      else if (this.currentZoom > 1.25)
        this.ZoomIn(args.Location);
      if (args.IsEnd)
        this.currentZoom = 1.0;
      args.Handled = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.zoomedIn)
        return;
      this.Capture = true;
      this.scrollService.MouseDown(e.Location);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.zoomedIn)
      {
        this.scrollService.MouseUp(e.Location);
        this.Capture = false;
      }
      else
        this.ZoomIn(e.Location);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.Capture)
        return;
      this.scrollService.MouseMove(e.Location);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (this.MouseWheelBehavior == PanoramaMouseWheelBehavior.Zoom || this.MouseWheelBehavior == PanoramaMouseWheelBehavior.ZoomAndScroll && Control.ModifierKeys == Keys.Control)
      {
        if (e.Delta < 0 && this.zoomedIn)
        {
          this.ZoomOut();
        }
        else
        {
          if (e.Delta <= 0 || this.zoomedIn)
            return;
          this.ZoomIn(e.Location);
        }
      }
      else
      {
        if (this.MouseWheelBehavior == PanoramaMouseWheelBehavior.None)
          return;
        this.scrollBar.Value = Math.Max(0, Math.Min(this.scrollBar.Maximum - this.scrollBar.LargeChange + 1, this.scrollBar.Value + Math.Sign(e.Delta) * -30));
      }
    }

    private void items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (target != null && !typeof (RadTileElement).IsAssignableFrom(target.GetType()))
        throw new ArgumentException("The Items collection accepts only objects or descendants of type RadTileElement");
      RadTileElement tileElement = target as RadTileElement;
      RadControl control = this.ElementTree.Control as RadControl;
      if (control != null && control.IsInitializing || this.ElementTree.Control.Site != null)
        return;
      if (this.AutoArrangeNewTiles && tileElement != null && operation == ItemsChangeOperation.Inserted)
      {
        int num1 = 0;
        int num2 = 0;
        bool flag = false;
        if (tileElement.GetValueSource(GridLayout.ColumnIndexProperty) > ValueSource.DefaultValue || tileElement.GetValueSource(GridLayout.RowIndexProperty) > ValueSource.DefaultValue)
        {
          this.UpdateCellCount();
          this.InvalidateMeasure();
          return;
        }
        int col = 0;
        while (!flag)
        {
          for (int row = 0; !flag && row < this.RowsCount; ++row)
          {
            if (this.CanPlace(tileElement, row, col))
            {
              num1 = row;
              num2 = col;
              flag = true;
            }
          }
          ++col;
        }
        tileElement.Column = num2;
        tileElement.Row = num1;
      }
      this.UpdateCellCount();
      this.InvalidateMeasure();
    }

    private bool CanPlace(RadTileElement tileElement, int row, int col)
    {
      Rectangle rect = new Rectangle(col, row, tileElement.ColSpan, tileElement.RowSpan);
      foreach (RadTileElement radTileElement in (RadItemCollection) this.Items)
      {
        if (radTileElement != tileElement && new Rectangle(radTileElement.Column, radTileElement.Row, radTileElement.ColSpan, radTileElement.RowSpan).IntersectsWith(rect))
          return false;
      }
      return true;
    }

    private void zoomInOffset_AnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      this.ContinueAnimatedTiles();
      AnimatedPropertySetting animatedPropertySetting = sender as AnimatedPropertySetting;
      if (animatedPropertySetting == null)
        return;
      this.ScrollView((int) ((SizeF) animatedPropertySetting.EndValue).Width);
    }

    private void scrollBar_ValueChanged(object sender, EventArgs e)
    {
      this.UpdateViewOnScroll();
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Scroll");
    }

    private void layout_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.PositionOffsetProperty && e.Property != RadElement.ScaleTransformProperty)
        return;
      this.Invalidate();
      if (this.showGroups)
        return;
      this.InvalidateMeasure(true);
    }
  }
}
