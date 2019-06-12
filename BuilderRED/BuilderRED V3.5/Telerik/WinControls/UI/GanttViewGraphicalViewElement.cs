// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewGraphicalViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class GanttViewGraphicalViewElement : GanttViewBaseViewElement
  {
    public static RadProperty LinksColorProperty = RadProperty.Register(nameof (LinksColor), typeof (Color), typeof (GanttViewGraphicalViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Gray, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LinksSelectionClickTresholdProperty = RadProperty.Register(nameof (LinksSelectionClickTreshold), typeof (int), typeof (GanttViewGraphicalViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LinksSelectionColorSpreadProperty = RadProperty.Register(nameof (LinksSelectionColorSpread), typeof (int), typeof (GanttViewGraphicalViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 7, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LinksHandlesSizeProperty = RadProperty.Register(nameof (LinksHandlesSize), typeof (Size), typeof (GanttViewGraphicalViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(10, 10), ElementPropertyOptions.InvalidatesLayout));
    public static RadProperty ShowTodayIndicatorProperty = RadProperty.Register(nameof (ShowTodayIndicator), typeof (bool), typeof (GanttViewGraphicalViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout));
    public static RadProperty ShowTimelineTodayIndicatorProperty = RadProperty.Register(nameof (ShowTimelineTodayIndicator), typeof (bool), typeof (GanttViewGraphicalViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout));
    private double? saveGraphicsOpacity = new double?();
    private TimeSpan onePixelTime = new TimeSpan(0, 30, 0);
    private DateTime timelineStart = DateTime.Now;
    private DateTime timelineEnd = DateTime.Now.AddYears(1);
    private ObservableCollection<GanttViewTimelineDataItem> timelineItems;
    private GanttViewTimelineContainer timelineContainer;
    private GanttViewTimelineScroller timelineScroller;
    private RadScrollBarElement horizontalScrollBar;
    private GanttViewTodayIndicatorElement todayIndicatorElement;
    private GanttViewTimelineTodayIndicatorElement timelineTodayIndicatorElement;
    private BaseGanttViewTimelineBehavior timelineBehavior;
    private TimeRange timelineRange;
    private bool automaticTimelineTimeRange;
    private List<GanttViewDataItem> flatItemsCollection;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.timelineItems = new ObservableCollection<GanttViewTimelineDataItem>();
      this.flatItemsCollection = new List<GanttViewDataItem>();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ViewElement.ShouldHandleMouseInput = true;
      this.ViewElement.NotifyParentOnMouseInput = true;
      this.todayIndicatorElement = new GanttViewTodayIndicatorElement();
      this.Children.Add((RadElement) this.todayIndicatorElement);
      this.timelineContainer = new GanttViewTimelineContainer(this);
      this.timelineContainer.ElementProvider = (IVirtualizedElementProvider<GanttViewTimelineDataItem>) new GanttViewTimelineElementProvider(this);
      this.Children.Add((RadElement) this.timelineContainer);
      this.timelineTodayIndicatorElement = new GanttViewTimelineTodayIndicatorElement();
      this.Children.Add((RadElement) this.timelineTodayIndicatorElement);
      this.horizontalScrollBar = new RadScrollBarElement();
      this.horizontalScrollBar.ScrollType = ScrollType.Horizontal;
      this.horizontalScrollBar.MinSize = new Size(0, RadScrollBarElement.HorizontalScrollBarHeight);
      this.horizontalScrollBar.StretchVertically = false;
      this.horizontalScrollBar.StretchHorizontally = true;
      this.Children.Add((RadElement) this.horizontalScrollBar);
      this.timelineScroller = new GanttViewTimelineScroller();
      this.timelineScroller.AsynchronousScrolling = false;
      this.timelineScroller.ElementProvider = this.TimelineContainer.ElementProvider;
      this.timelineScroller.Scrollbar = this.horizontalScrollBar;
      this.timelineScroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.HScrollBar.Visibility = ElementVisibility.Collapsed;
    }

    public GanttViewGraphicalViewElement(RadGanttViewElement ganttView)
      : base(ganttView)
    {
      this.timelineScroller.Traverser = (ITraverser<GanttViewTimelineDataItem>) new ItemsTraverser<GanttViewTimelineDataItem>((IList<GanttViewTimelineDataItem>) this.timelineItems);
      this.TimelineContainer.DataProvider = (IEnumerable) this.timelineScroller;
      this.timelineScroller.ScrollerUpdated += new EventHandler(this.timelineScroller_ScrollerUpdated);
      this.TimelineBehavior = new BaseGanttViewTimelineBehavior();
      this.BuildTimelineDataItems();
      this.FitItemsToSize = true;
      this.Scroller.ScrollMode = ItemScrollerScrollModes.Smooth;
      this.Scroller.Traverser = (ITraverser<GanttViewDataItem>) new GanttViewTraverser(ganttView);
      this.Scroller.ScrollerUpdated += new EventHandler(this.Scroller_ScrollerUpdated);
      this.GanttViewElement.RootCreated += new EventHandler(this.GanttViewElement_RootCreated);
    }

    protected override void DisposeManagedResources()
    {
      this.Scroller.ScrollerUpdated -= new EventHandler(this.Scroller_ScrollerUpdated);
      this.timelineScroller.ScrollerUpdated -= new EventHandler(this.timelineScroller_ScrollerUpdated);
      this.GanttViewElement.RootCreated -= new EventHandler(this.GanttViewElement_RootCreated);
      this.GanttViewElement.Items.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.Items_CollectionChanged);
      this.GanttViewElement.Links.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.Links_CollectionChanged);
      base.DisposeManagedResources();
    }

    public int LinksSelectionClickTreshold
    {
      get
      {
        return (int) this.GetValue(GanttViewGraphicalViewElement.LinksSelectionClickTresholdProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewGraphicalViewElement.LinksSelectionClickTresholdProperty, (object) value);
      }
    }

    public int LinksSelectionColorSpread
    {
      get
      {
        return (int) this.GetValue(GanttViewGraphicalViewElement.LinksSelectionColorSpreadProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewGraphicalViewElement.LinksSelectionColorSpreadProperty, (object) value);
      }
    }

    public RadScrollBarElement HorizontalScrollBarElement
    {
      get
      {
        return this.horizontalScrollBar;
      }
    }

    public bool ShowTodayIndicator
    {
      get
      {
        return (bool) this.GetValue(GanttViewGraphicalViewElement.ShowTodayIndicatorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewGraphicalViewElement.ShowTodayIndicatorProperty, (object) value);
      }
    }

    public GanttViewTodayIndicatorElement TodayIndicatorElement
    {
      get
      {
        return this.todayIndicatorElement;
      }
    }

    public bool ShowTimelineTodayIndicator
    {
      get
      {
        return (bool) this.GetValue(GanttViewGraphicalViewElement.ShowTimelineTodayIndicatorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewGraphicalViewElement.ShowTimelineTodayIndicatorProperty, (object) value);
      }
    }

    public GanttViewTimelineTodayIndicatorElement TimelineTodayIndicatorElement
    {
      get
      {
        return this.timelineTodayIndicatorElement;
      }
    }

    public GanttViewTimelineScroller TimelineScroller
    {
      get
      {
        return this.timelineScroller;
      }
    }

    public GanttViewTimelineContainer TimelineContainer
    {
      get
      {
        return this.timelineContainer;
      }
    }

    public ObservableCollection<GanttViewTimelineDataItem> TimelineItems
    {
      get
      {
        return this.timelineItems;
      }
    }

    public BaseGanttViewTimelineBehavior TimelineBehavior
    {
      get
      {
        return this.timelineBehavior;
      }
      set
      {
        if (this.timelineBehavior == value)
          return;
        this.timelineBehavior = value;
        this.timelineBehavior.GraphicalViewElement = this;
      }
    }

    public DateTime TimelineStart
    {
      get
      {
        return this.timelineStart;
      }
      set
      {
        if (!(this.timelineStart != value))
          return;
        this.timelineStart = value;
        this.OnNotifyPropertyChanged(nameof (TimelineStart));
        this.UpdateTimelineZoom();
        if (!(this.TimelineEnd <= value))
          return;
        this.TimelineEnd = value.AddDays(1.0);
      }
    }

    public DateTime TimelineEnd
    {
      get
      {
        return this.timelineEnd;
      }
      set
      {
        if (!(this.timelineEnd != value))
          return;
        this.timelineEnd = value;
        this.OnNotifyPropertyChanged(nameof (TimelineEnd));
        this.UpdateTimelineZoom();
        if (!(this.TimelineStart >= value))
          return;
        this.TimelineStart = value.AddDays(-1.0);
      }
    }

    public TimeRange TimelineRange
    {
      get
      {
        return this.timelineRange;
      }
      set
      {
        if (this.timelineRange == value)
          return;
        this.timelineRange = value;
        this.UpdateTimelineZoom();
        this.UpdateInnerState();
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (TimelineRange));
      }
    }

    public bool AutomaticTimelineTimeRange
    {
      get
      {
        return this.automaticTimelineTimeRange;
      }
      set
      {
        if (this.AutomaticTimelineTimeRange == value)
          return;
        this.automaticTimelineTimeRange = value;
        this.OnNotifyPropertyChanged(nameof (AutomaticTimelineTimeRange));
      }
    }

    public TimeSpan OnePixelTime
    {
      get
      {
        return new TimeSpan((long) ((double) this.onePixelTime.Ticks / (double) this.DpiScaleFactor.Width));
      }
      set
      {
        bool zoomin = this.onePixelTime > value;
        this.onePixelTime = value;
        this.UpdateInnerState();
        this.UpdateTimelineDataItems();
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (OnePixelTime));
        this.OnOnePixelTimeChanged(zoomin);
      }
    }

    public Color LinksColor
    {
      get
      {
        return (Color) this.GetValue(GanttViewGraphicalViewElement.LinksColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewGraphicalViewElement.LinksColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("LinksHandlesSize", typeof (GanttViewGraphicalViewElement))]
    public Size LinksHandlesSize
    {
      get
      {
        return TelerikDpiHelper.ScaleSize((Size) this.GetValue(GanttViewGraphicalViewElement.LinksHandlesSizeProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(GanttViewGraphicalViewElement.LinksHandlesSizeProperty, (object) value);
      }
    }

    protected internal GanttViewLinkDataItem NewLink { get; set; }

    protected bool IsCreatingLink { get; set; }

    private void UpdateTimelineDataItems()
    {
      foreach (GanttViewTimelineDataItem timelineItem in (Collection<GanttViewTimelineDataItem>) this.TimelineItems)
        timelineItem.OnePixelTime = this.OnePixelTime;
    }

    private void BuildTimelineDataItems()
    {
      this.TimelineItems.Clear();
      foreach (GanttViewTimelineDataItem timelineDataItem in (IEnumerable<GanttViewTimelineDataItem>) this.TimelineBehavior.BuildTimelineDataItems(this.TimelineRange))
        this.TimelineItems.Add(timelineDataItem);
    }

    protected virtual Point[] ApplyOffset(List<Point> lines)
    {
      Point[] pointArray = new Point[lines.Count];
      for (int index = 0; index < lines.Count; ++index)
        pointArray[index] = new Point(lines[index].X - this.HorizontalScrollBarElement.Value, lines[index].Y - this.VScrollBar.Value);
      return pointArray;
    }

    protected internal virtual bool ShouldDrawLink(GanttViewLinkDataItem link, Point[] linkLines)
    {
      for (int index = 0; index < linkLines.Length - 1; ++index)
      {
        GanttViewDataItem ganttViewDataItem1 = link.StartItem;
        if (ganttViewDataItem1 == null || ganttViewDataItem1.Parent == null && !this.GanttViewElement.Root.Items.Contains(ganttViewDataItem1))
          return false;
        for (; ganttViewDataItem1.Parent != null; ganttViewDataItem1 = ganttViewDataItem1.Parent)
        {
          if (!ganttViewDataItem1.Parent.Expanded)
            return false;
        }
        GanttViewDataItem ganttViewDataItem2 = link.EndItem;
        if (ganttViewDataItem2 == null || ganttViewDataItem2.Parent == null && !this.GanttViewElement.Root.Items.Contains(ganttViewDataItem2))
          return false;
        for (; ganttViewDataItem2.Parent != null; ganttViewDataItem2 = ganttViewDataItem2.Parent)
        {
          if (!ganttViewDataItem2.Parent.Expanded)
            return false;
        }
        if (!link.StartItem.Visible || !link.EndItem.Visible)
          return false;
        Rectangle bounds = this.ViewElement.Bounds;
        bounds.Offset(0, this.GanttViewElement.HeaderHeight);
        if (this.LineIntersectsRect(linkLines[index], linkLines[index + 1], bounds))
          return true;
      }
      return false;
    }

    protected internal virtual void CalculateLinkLines()
    {
      foreach (GanttViewLinkDataItem link in (Collection<GanttViewLinkDataItem>) this.GanttViewElement.Links)
      {
        if (link.StartItem != null && link.EndItem != null)
          this.CalculateLinkLines(link, new Point?());
      }
    }

    protected internal virtual void CalculateLinkLines(GanttViewDataItem item)
    {
      foreach (GanttViewLinkDataItem link in (Collection<GanttViewLinkDataItem>) this.GanttViewElement.Links)
      {
        if (link.StartItem == item || link.EndItem == item)
          this.CalculateLinkLines(link, new Point?());
      }
    }

    public virtual void CalculateLinkLines(GanttViewLinkDataItem link, Point? point)
    {
      if (link.StartItem == null || link.EndItem == null)
        return;
      link.Lines.Clear();
      switch (link.LinkType)
      {
        case TasksLinkType.FinishToFinish:
          this.CalculateFinishToFinishLines(link, point);
          break;
        case TasksLinkType.FinishToStart:
          this.CalculateFinishToStartLines(link, point);
          break;
        case TasksLinkType.StartToFinish:
          this.CalculateStartToFinishLines(link, point);
          break;
        case TasksLinkType.StartToStart:
          this.CalculateStartToStartLines(link, point);
          break;
      }
    }

    protected virtual void CalculateStartToStartLines(GanttViewLinkDataItem link, Point? point)
    {
      int num = (int) Math.Round((double) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) / 2.0, MidpointRounding.AwayFromZero);
      int x1;
      int y1;
      int x2;
      int y2;
      if (point.HasValue && link.StartItem.GanttViewElement == null)
      {
        x1 = point.Value.X;
        y1 = point.Value.Y;
        x2 = (int) ((link.EndItem.Start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.EndItem) + num;
      }
      else if (point.HasValue && link.EndItem.GanttViewElement == null)
      {
        x1 = (int) ((link.StartItem.Start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num;
        x2 = point.Value.X;
        y2 = point.Value.Y;
      }
      else
      {
        x1 = (int) ((link.StartItem.Start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num;
        x2 = (int) ((link.EndItem.Start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.EndItem) + num;
      }
      link.Lines.Add(new Point(x1, y1));
      if (x1 < x2)
      {
        link.Lines.Add(new Point(x1 - this.GanttViewElement.MinimumLinkLength, y1));
        link.Lines.Add(new Point(x1 - this.GanttViewElement.MinimumLinkLength, y2));
      }
      else
      {
        link.Lines.Add(new Point(x2 - this.GanttViewElement.MinimumLinkLength, y1));
        link.Lines.Add(new Point(x2 - this.GanttViewElement.MinimumLinkLength, y2));
      }
      link.Lines.Add(new Point(x2, y2));
    }

    protected virtual void CalculateStartToFinishLines(GanttViewLinkDataItem link, Point? point)
    {
      int num = (int) Math.Round((double) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) / 2.0, MidpointRounding.AwayFromZero);
      int x1;
      int y1;
      int x2;
      int y2;
      if (point.HasValue && link.StartItem.GanttViewElement == null)
      {
        x1 = point.Value.X;
        y1 = point.Value.Y;
        x2 = (int) ((link.EndItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.EndItem) + num;
      }
      else if (point.HasValue && link.EndItem.GanttViewElement == null)
      {
        x1 = (int) ((link.StartItem.Start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num;
        x2 = point.Value.X;
        y2 = point.Value.Y;
      }
      else
      {
        x1 = (int) ((link.StartItem.Start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num;
        x2 = (int) ((link.EndItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.EndItem) + num;
      }
      link.Lines.Add(new Point(x1, y1));
      if (x1 - this.GanttViewElement.MinimumLinkLength > x2 + this.GanttViewElement.MinimumLinkLength)
      {
        int x3 = x1 - (x1 - x2) / 2;
        link.Lines.Add(new Point(x3, y1));
        link.Lines.Add(new Point(x3, y2));
      }
      else
      {
        int y3 = y1 + (y2 - y1) / 2;
        link.Lines.Add(new Point(x1 - this.GanttViewElement.MinimumLinkLength, y1));
        link.Lines.Add(new Point(x1 - this.GanttViewElement.MinimumLinkLength, y3));
        link.Lines.Add(new Point(x2 + this.GanttViewElement.MinimumLinkLength, y3));
        link.Lines.Add(new Point(x2 + this.GanttViewElement.MinimumLinkLength, y2));
      }
      link.Lines.Add(new Point(x2, y2));
    }

    protected virtual void CalculateFinishToStartLines(GanttViewLinkDataItem link, Point? point)
    {
      int num = (int) Math.Round((double) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) / 2.0, MidpointRounding.AwayFromZero);
      int x1;
      int y1;
      int x2;
      int y2;
      if (point.HasValue && link.StartItem.GanttViewElement == null)
      {
        x1 = point.Value.X;
        y1 = point.Value.Y;
        x2 = (int) ((link.EndItem.Start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.EndItem) + num;
      }
      else if (point.HasValue && link.EndItem.GanttViewElement == null)
      {
        x1 = (int) ((link.StartItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num;
        x2 = point.Value.X;
        y2 = point.Value.Y;
      }
      else
      {
        x1 = (int) ((link.StartItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num;
        x2 = (int) ((link.EndItem.Start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.EndItem) + num;
      }
      link.Lines.Add(new Point(x1, y1));
      if (x1 + this.GanttViewElement.MinimumLinkLength < x2 - this.GanttViewElement.MinimumLinkLength)
      {
        int x3 = x1 + (x2 - x1) / 2;
        link.Lines.Add(new Point(x3, y1));
        link.Lines.Add(new Point(x3, y2));
      }
      else
      {
        int y3 = y1 + (y2 - y1) / 2;
        link.Lines.Add(new Point(x1 + this.GanttViewElement.MinimumLinkLength, y1));
        link.Lines.Add(new Point(x1 + this.GanttViewElement.MinimumLinkLength, y3));
        link.Lines.Add(new Point(x2 - this.GanttViewElement.MinimumLinkLength, y3));
        link.Lines.Add(new Point(x2 - this.GanttViewElement.MinimumLinkLength, y2));
      }
      link.Lines.Add(new Point(x2, y2));
    }

    protected virtual void CalculateFinishToFinishLines(GanttViewLinkDataItem link, Point? point)
    {
      int num1 = (int) Math.Round((double) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) / 2.0, MidpointRounding.AwayFromZero);
      int num2 = (int) ((link.StartItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
      int num3 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num1;
      int x1;
      int y1;
      int x2;
      int y2;
      if (point.HasValue && link.StartItem.GanttViewElement == null)
      {
        x1 = point.Value.X;
        y1 = point.Value.Y;
        x2 = (int) ((link.EndItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.EndItem) + num1;
      }
      else if (point.HasValue && link.EndItem.GanttViewElement == null)
      {
        x1 = (int) ((link.StartItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num1;
        x2 = point.Value.X;
        y2 = point.Value.Y;
      }
      else
      {
        x1 = (int) ((link.StartItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.StartItem) + num1;
        x2 = (int) ((link.EndItem.End - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
        y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * this.flatItemsCollection.IndexOf(link.EndItem) + num1;
      }
      link.Lines.Add(new Point(x1, y1));
      if (x1 < x2)
      {
        link.Lines.Add(new Point(x2 + this.GanttViewElement.MinimumLinkLength, y1));
        link.Lines.Add(new Point(x2 + this.GanttViewElement.MinimumLinkLength, y2));
      }
      else
      {
        link.Lines.Add(new Point(x1 + this.GanttViewElement.MinimumLinkLength, y1));
        link.Lines.Add(new Point(x1 + this.GanttViewElement.MinimumLinkLength, y2));
      }
      link.Lines.Add(new Point(x2, y2));
    }

    protected internal virtual void PopulateFlatTasksCollection()
    {
      this.flatItemsCollection.Clear();
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      while (ganttViewTraverser.MoveNext())
        this.flatItemsCollection.Add(ganttViewTraverser.Current);
    }

    protected virtual bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
    {
      if (p1.X < r.X && p2.X < r.X || p1.X > r.Right && p2.X > r.Right || (p1.Y < r.Y && p2.Y < r.Y || p1.Y > r.Bottom && p2.Y > r.Bottom))
        return false;
      if (this.LineIntersectsLine(p1, p2, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) || this.LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) || (this.LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) || this.LineIntersectsLine(p1, p2, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y))))
        return true;
      if (r.Contains(p1))
        return r.Contains(p2);
      return false;
    }

    protected virtual bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
    {
      float num1 = (float) ((l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y));
      float num2 = (float) ((l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X));
      if ((double) num2 == 0.0)
        return false;
      float num3 = num1 / num2;
      float num4 = (float) ((l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y)) / num2;
      return (double) num3 >= 0.0 && (double) num3 <= 1.0 && ((double) num4 >= 0.0 && (double) num4 <= 1.0);
    }

    public void Update(RadGanttViewElement.UpdateActions updateAction)
    {
      if (updateAction == RadGanttViewElement.UpdateActions.Reset)
      {
        this.HorizontalScrollBarElement.Value = this.HorizontalScrollBarElement.Minimum;
        this.VScrollBar.Value = this.VScrollBar.Minimum;
        this.SuspendLayout();
        while (this.ViewElement.Children.Count > 0)
        {
          GanttGraphicalViewBaseItemElement child = (GanttGraphicalViewBaseItemElement) this.ViewElement.Children[0];
          this.ViewElement.Children.Remove((RadElement) child);
          child.Detach();
        }
        this.ResumeLayout(false);
        this.Scroller.ElementProvider.ClearCache();
      }
      if (updateAction == RadGanttViewElement.UpdateActions.Reset || updateAction == RadGanttViewElement.UpdateActions.Resume)
      {
        int num = this.Scroller.Scrollbar.Value;
        this.UpdateScrollers(updateAction);
        this.UpdateInnerState();
        if (updateAction == RadGanttViewElement.UpdateActions.Resume)
        {
          if (num > this.VScrollBar.Maximum - this.VScrollBar.LargeChange)
            num = this.VScrollBar.Maximum - this.VScrollBar.LargeChange;
          if (num >= 0)
            this.Scroller.Scrollbar.Value = num;
        }
      }
      if (updateAction == RadGanttViewElement.UpdateActions.ExpandedChanged)
        this.UpdateInnerState();
      this.ViewElement.UpdateItems();
      if (updateAction != RadGanttViewElement.UpdateActions.StateChanged && updateAction != RadGanttViewElement.UpdateActions.Resume)
        return;
      this.SynchronizeItemElements();
    }

    protected internal virtual void UpdateScrollers(RadGanttViewElement.UpdateActions updateAction)
    {
      if (this.GanttViewElement.Root.Items.Count > 0)
      {
        if (this.Scroller.Traverser.Current != null && this.Scroller.Traverser.Current.GanttViewElement != this.GanttViewElement)
        {
          this.Scroller.Traverser.Reset();
          this.Scroller.UpdateScrollRange();
          this.Scroller.UpdateScrollValue();
        }
        else
          this.Scroller.UpdateScrollRange();
      }
      else
      {
        this.VScrollBar.Value = this.VScrollBar.Minimum;
        this.VScrollBar.Maximum = this.VScrollBar.Minimum;
      }
    }

    protected internal virtual void SynchronizeItemElements()
    {
      foreach (GanttViewBaseItemElement child in this.ViewElement.Children)
        child.Synchronize();
      this.Invalidate();
    }

    protected internal virtual void UpdateInnerState()
    {
      this.PopulateFlatTasksCollection();
      this.CalculateLinkLines();
    }

    public virtual void UpdateTimelineZoom()
    {
      TimeSpan timeSpan = this.TimelineBehavior.AdjustedTimelineEnd - this.TimelineBehavior.AdjustedTimelineStart;
      if (timeSpan < new TimeSpan(24, 0, 0))
        timeSpan = new TimeSpan(24, 0, 0);
      if ((int) (timeSpan.TotalSeconds / this.OnePixelTime.TotalSeconds) > this.Size.Width)
        return;
      this.OnePixelTime = DateTime.Now.AddSeconds(timeSpan.TotalSeconds / (double) this.Size.Width) - DateTime.Now;
    }

    protected internal virtual void UpdateTextViewScroller()
    {
      if (this.GanttViewElement.TextViewElement.Scroller.Scrollbar.Maximum != this.Scroller.Scrollbar.Maximum)
        this.GanttViewElement.TextViewElement.UpdateScrollers(RadGanttViewElement.UpdateActions.None);
      this.GanttViewElement.TextViewElement.Scroller.Scrollbar.Value = this.Scroller.Scrollbar.Value;
    }

    protected virtual void OnOnePixelTimeChanged(bool zoomin)
    {
      if (!this.AutomaticTimelineTimeRange)
        return;
      TimeRange autoTimeRange = this.TimelineBehavior.GetAutoTimeRange(this.TimelineRange, zoomin);
      if (this.TimelineRange == autoTimeRange)
        return;
      this.TimelineRange = autoTimeRange;
    }

    public virtual RectangleF GetDrawRectangle(GanttViewDataItem item, DateTime datetime)
    {
      return this.GetDrawRectangle(item, datetime, datetime, true, true, true);
    }

    public virtual RectangleF GetDrawRectangle(
      GanttViewDataItem item,
      DateTime start,
      DateTime end)
    {
      return this.GetDrawRectangle(item, start, end, true, true, true);
    }

    public virtual RectangleF GetDrawRectangle(
      GanttViewDataItem item,
      DateTime start,
      DateTime end,
      bool addScrollOffset)
    {
      return this.GetDrawRectangle(item, start, end, addScrollOffset, true, true);
    }

    public virtual RectangleF GetDrawRectangle(
      GanttViewDataItem item,
      DateTime start,
      DateTime end,
      bool addHScrollOffset,
      bool addVScrollOffset,
      bool addHeaderHeight)
    {
      int num = this.flatItemsCollection.IndexOf(item);
      if (num < 0)
        return RectangleF.Empty;
      float x = (float) ((start - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds);
      if (addHScrollOffset)
        x -= (float) this.HorizontalScrollBarElement.Value;
      float y = !addVScrollOffset ? (float) (num * (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing)) : (float) (num * (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) - this.VScrollBar.Value);
      if (addHeaderHeight)
        y += (float) this.GanttViewElement.HeaderHeight;
      float width = (float) ((end - start).TotalSeconds / this.OnePixelTime.TotalSeconds);
      return new RectangleF(x, y, width, (float) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing));
    }

    public virtual bool ScrollTo(DateTime dateTime)
    {
      if (dateTime < this.TimelineBehavior.AdjustedTimelineStart || dateTime > this.TimelineBehavior.AdjustedTimelineEnd)
        return false;
      float num1 = (float) this.ViewElement.Size.Width / 2f;
      float num2 = (float) ((dateTime - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds) - num1;
      if ((double) num2 < 0.0)
        this.TimelineScroller.Scrollbar.Value = this.TimelineScroller.Scrollbar.Minimum;
      else if ((double) num2 > (double) (this.TimelineScroller.Scrollbar.Maximum - this.TimelineScroller.Scrollbar.LargeChange))
        this.TimelineScroller.Scrollbar.Value = this.TimelineScroller.Scrollbar.Maximum - this.TimelineScroller.Scrollbar.LargeChange;
      else
        this.TimelineScroller.Scrollbar.Value = (int) num2;
      return true;
    }

    public virtual GanttViewLinkDataItem GetLink(Point location)
    {
      Point point = this.PointFromControl(location);
      point.Offset(this.HorizontalScrollBarElement.Value, this.GanttViewElement.TextViewElement.Scroller.Scrollbar.Value);
      using (Pen pen = new Pen(Color.Black, (float) this.LinksSelectionClickTreshold))
      {
        pen.Alignment = PenAlignment.Center;
        foreach (GanttViewLinkDataItem link in (Collection<GanttViewLinkDataItem>) this.GanttViewElement.Links)
        {
          GraphicsPath graphicsPath = new GraphicsPath();
          graphicsPath.AddLines(link.Lines.ToArray());
          if (graphicsPath.IsOutlineVisible(point, pen))
            return link;
        }
      }
      return (GanttViewLinkDataItem) null;
    }

    protected override void PaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      if (this.Opacity < 1.0)
      {
        this.saveGraphicsOpacity = new double?(graphics.Opacity);
        graphics.ChangeOpacity(this.Opacity * graphics.Opacity);
      }
      this.ViewElement.Paint(graphics, clipRectange, angle, scale, useRelativeTransformation);
      ((Graphics) graphics.UnderlayGraphics).SetClip(this.ViewElement.BoundingRectangle);
      if (this.flatItemsCollection.Count == 0)
        this.UpdateInnerState();
      this.todayIndicatorElement.Paint(graphics, clipRectange, angle, scale, useRelativeTransformation);
      this.DrawLinkLines(graphics);
      foreach (RadElement child1 in this.ViewElement.GetChildren(ChildrenListOptions.ZOrdered))
      {
        foreach (RadElement child2 in child1.GetChildren(ChildrenListOptions.ZOrdered))
          this.PaintChild(child2, graphics, clipRectange, angle, scale, useRelativeTransformation);
        if (this.GanttViewElement.EnableCustomPainting)
        {
          GanttGraphicalViewBaseItemElement element = child1 as GanttGraphicalViewBaseItemElement;
          if (element != null)
            this.GanttViewElement.OnItemPaint(new GanttViewItemPaintEventArgs(element, (Graphics) graphics.UnderlayGraphics));
        }
      }
      ((Graphics) graphics.UnderlayGraphics).ResetClip();
      foreach (RadElement child in this.GetChildren(ChildrenListOptions.ZOrdered))
      {
        if (child != this.ViewElement && child != this.TodayIndicatorElement)
          this.PaintChild(child, graphics, clipRectange, angle, scale, useRelativeTransformation);
      }
      if (!this.saveGraphicsOpacity.HasValue || !this.saveGraphicsOpacity.HasValue)
        return;
      graphics.ChangeOpacity(this.saveGraphicsOpacity.Value);
    }

    protected virtual void DrawLinkLines(IGraphics graphics)
    {
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      for (int index = 0; index < this.GanttViewElement.Links.Count; ++index)
      {
        GanttViewLinkDataItem link = this.GanttViewElement.Links[index];
        if (link != this.GanttViewElement.GanttViewBehavior.EditedLink)
        {
          if (link.Lines == null || link.Lines.Count == 0)
            this.CalculateLinkLines(link, new Point?());
          Point[] pointArray = this.ApplyOffset(link.Lines);
          if (this.ShouldDrawLink(link, pointArray))
          {
            using (Pen pen1 = new Pen(this.LinksColor, this.DpiScaleFactor.Width))
            {
              pen1.EndCap = LineCap.ArrowAnchor;
              GanttViewLinkItemFormattingEventArgs e = new GanttViewLinkItemFormattingEventArgs(link, pen1);
              this.GanttViewElement.OnGraphicalViewLinkItemFormatting(e);
              ((Graphics) graphics.UnderlayGraphics).DrawLines(e.Pen, pointArray);
              if (link.Selected)
              {
                using (Pen pen2 = new Pen(Color.FromArgb(64, this.LinksColor)))
                {
                  pen2.Alignment = PenAlignment.Center;
                  for (int selectionColorSpread = this.LinksSelectionColorSpread; selectionColorSpread >= 0; selectionColorSpread -= 2)
                  {
                    pen2.Width = (float) selectionColorSpread;
                    ((Graphics) graphics.UnderlayGraphics).DrawLines(pen2, pointArray);
                  }
                }
              }
            }
          }
        }
      }
      if (this.GanttViewElement.GanttViewBehavior.NewLink != null)
      {
        Point[] points = this.ApplyOffset(this.GanttViewElement.GanttViewBehavior.NewLink.Lines);
        if (points.Length > 1)
        {
          using (Pen pen = new Pen(this.LinksColor, this.DpiScaleFactor.Width))
          {
            pen.EndCap = LineCap.ArrowAnchor;
            GanttViewLinkItemFormattingEventArgs e = new GanttViewLinkItemFormattingEventArgs(this.GanttViewElement.GanttViewBehavior.NewLink, pen);
            this.GanttViewElement.OnGraphicalViewLinkItemFormatting(e);
            ((Graphics) graphics.UnderlayGraphics).DrawLines(e.Pen, points);
          }
        }
      }
      if (this.GanttViewElement.GanttViewBehavior.EditedLink == null)
        return;
      Point[] points1 = this.ApplyOffset(this.GanttViewElement.GanttViewBehavior.EditedLink.Lines);
      if (points1.Length <= 1)
        return;
      using (Pen pen = new Pen(this.LinksColor, this.DpiScaleFactor.Width))
      {
        pen.EndCap = LineCap.ArrowAnchor;
        GanttViewLinkItemFormattingEventArgs e = new GanttViewLinkItemFormattingEventArgs(this.GanttViewElement.GanttViewBehavior.EditedLink, pen);
        this.GanttViewElement.OnGraphicalViewLinkItemFormatting(e);
        ((Graphics) graphics.UnderlayGraphics).DrawLines(e.Pen, points1);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (this.HorizontalScrollBarElement.Visibility != ElementVisibility.Collapsed)
      {
        this.HorizontalScrollBarElement.Measure(clientRectangle.Size);
        num1 = this.HorizontalScrollBarElement.DesiredSize.Height;
      }
      if (this.Scroller.Scrollbar.Visibility != ElementVisibility.Collapsed)
      {
        num2 = this.Scroller.Scrollbar.DesiredSize.Width;
        this.VScrollBar.Measure(new SizeF(clientRectangle.Size.Width, (float) ((double) clientRectangle.Height - (double) this.GanttViewElement.HeaderHeight - 1.0)));
      }
      this.TimelineScroller.ClientSize = new SizeF(clientRectangle.Size.Width - num2, clientRectangle.Height);
      this.TimelineScroller.UpdateScrollRange();
      SizeF availableSize1 = new SizeF(clientRectangle.Width - num2, (float) ((double) availableSize.Height - (double) num1 - (double) this.GanttViewElement.HeaderHeight - 1.0));
      this.Scroller.ClientSize = availableSize1;
      this.Scroller.UpdateScrollRange();
      this.TimelineContainer.Measure(new SizeF(availableSize1.Width, (float) this.GanttViewElement.HeaderHeight));
      this.ViewElement.Measure(availableSize1);
      if (this.TimelineBehavior.AdjustedTimelineStart < DateTime.Now && this.TimelineBehavior.AdjustedTimelineEnd > DateTime.Now)
      {
        if (this.TimelineTodayIndicatorElement.Visibility == ElementVisibility.Visible)
          this.TimelineTodayIndicatorElement.Measure(new SizeF(clientRectangle.Size.Width, (float) (this.GanttViewElement.HeaderHeight - 1)));
        if (this.TodayIndicatorElement.Visibility == ElementVisibility.Visible)
          this.TodayIndicatorElement.Measure(new SizeF(clientRectangle.Size.Width, (float) ((double) clientRectangle.Height - (double) num1 - (double) this.GanttViewElement.HeaderHeight - 1.0)));
      }
      SizeF sizeF = new SizeF(this.TimelineContainer.DesiredSize.Width, (float) this.GanttViewElement.HeaderHeight);
      sizeF.Height += this.ViewElement.DesiredSize.Height;
      sizeF.Height += this.HorizontalScrollBarElement.DesiredSize.Height;
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (this.Scroller.Scrollbar.Visibility != ElementVisibility.Collapsed)
        num2 = this.Scroller.Scrollbar.DesiredSize.Width;
      if (this.HorizontalScrollBarElement.Visibility != ElementVisibility.Collapsed)
      {
        RectangleF finalRect = new RectangleF();
        finalRect.X = clientRectangle.X;
        finalRect.Y = clientRectangle.Height - this.HorizontalScrollBarElement.DesiredSize.Height;
        finalRect.Height = this.HorizontalScrollBarElement.DesiredSize.Height;
        finalRect.Width = clientRectangle.Width - num2;
        num1 = finalRect.Height;
        this.HorizontalScrollBarElement.Arrange(finalRect);
      }
      if (this.TimelineContainer.Visibility != ElementVisibility.Collapsed)
        this.TimelineContainer.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width - num2, (float) this.GanttViewElement.HeaderHeight));
      RectangleF viewElementRect = new RectangleF(clientRectangle.X, (float) ((double) clientRectangle.Y + (double) this.GanttViewElement.HeaderHeight + 1.0), clientRectangle.Width, clientRectangle.Height - (float) this.GanttViewElement.HeaderHeight - num1);
      this.Layout.Arrange(clientRectangle);
      this.ArrangeHScrollBar(ref viewElementRect, clientRectangle);
      this.ArrangeVScrollBar(ref viewElementRect, RectangleF.Empty, viewElementRect);
      this.ViewElement.Arrange(viewElementRect);
      if (this.TimelineBehavior.AdjustedTimelineStart < DateTime.Now && this.TimelineBehavior.AdjustedTimelineEnd > DateTime.Now)
      {
        float x = (float) ((DateTime.Now - this.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.OnePixelTime.TotalSeconds) - (float) this.HorizontalScrollBarElement.Value;
        if ((double) x < 0.0)
        {
          this.TodayIndicatorElement.Visibility = ElementVisibility.Collapsed;
          this.TimelineTodayIndicatorElement.Visibility = ElementVisibility.Collapsed;
        }
        else
        {
          if (this.ShowTodayIndicator && this.TodayIndicatorElement.Visibility != ElementVisibility.Visible)
            this.TodayIndicatorElement.Visibility = ElementVisibility.Visible;
          if (this.ShowTimelineTodayIndicator && this.TimelineTodayIndicatorElement.Visibility != ElementVisibility.Visible)
            this.TimelineTodayIndicatorElement.Visibility = ElementVisibility.Visible;
        }
        if (this.ShowTimelineTodayIndicator)
          this.TimelineTodayIndicatorElement.Arrange(new RectangleF(x, clientRectangle.Y, this.TodayIndicatorElement.DesiredSize.Width, (float) (this.GanttViewElement.HeaderHeight - 1)));
        if (this.ShowTodayIndicator)
          this.TodayIndicatorElement.Arrange(new RectangleF(x, (float) this.GanttViewElement.HeaderHeight, this.TodayIndicatorElement.DesiredSize.Width, viewElementRect.Height));
      }
      return finalSize;
    }

    protected override bool UpdateOnMeasure(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      RadScrollBarElement scrollBarElement = this.HorizontalScrollBarElement;
      RadScrollBarElement vscrollBar = this.VScrollBar;
      ElementVisibility visibility1 = scrollBarElement.Visibility;
      ElementVisibility visibility2 = vscrollBar.Visibility;
      SizeF size = clientRectangle.Size;
      size.Height -= (float) this.GanttViewElement.HeaderHeight;
      if (scrollBarElement.Visibility != ElementVisibility.Collapsed || this.GanttViewElement.GraphicalViewElement.HorizontalScrollBarElement.Visibility != ElementVisibility.Collapsed)
        size.Height -= scrollBarElement.DesiredSize.Height;
      this.Scroller.ClientSize = size;
      this.TimelineScroller.ClientSize = size;
      this.TimelineScroller.UpdateScrollRange();
      this.Scroller.UpdateScrollRange();
      bool flag = false;
      if (vscrollBar.Visibility == ElementVisibility.Visible)
      {
        size.Width -= vscrollBar.DesiredSize.Width;
        this.Scroller.ClientSize = size;
        this.TimelineScroller.ClientSize = size;
        flag = true;
      }
      if (scrollBarElement.Visibility != visibility1 && scrollBarElement.Visibility == ElementVisibility.Visible)
      {
        size.Height -= scrollBarElement.DesiredSize.Height;
        this.Scroller.ClientSize = size;
        this.TimelineScroller.ClientSize = size;
        flag = true;
      }
      if (flag)
      {
        this.TimelineScroller.UpdateScrollRange();
        this.Scroller.UpdateScrollRange();
      }
      if (visibility1 == scrollBarElement.Visibility)
        return visibility2 != scrollBarElement.Visibility;
      return true;
    }

    private void Scroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.UpdateTextViewScroller();
    }

    private void timelineScroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.TimelineContainer.InvalidateMeasure();
      this.InvalidateArrange(true);
      this.Invalidate();
    }

    private void GanttViewElement_RootCreated(object sender, EventArgs e)
    {
      this.GanttViewElement.Items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Items_CollectionChanged);
      this.GanttViewElement.Links.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Links_CollectionChanged);
      this.GanttViewElement.BindingProvider.Root.PropertyChanged += new PropertyChangedEventHandler(this.Root_PropertyChanged);
    }

    private void Root_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "DataSource"))
        return;
      this.UpdateInnerState();
    }

    private void Links_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.UpdateInnerState();
    }

    private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.UpdateInnerState();
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (e.PropertyName == "TimelineStart" || e.PropertyName == "TimelineEnd" || e.PropertyName == "TimelineRange")
      {
        this.BuildTimelineDataItems();
        this.CalculateLinkLines();
        this.TimelineScroller.UpdateScrollValue();
        if (DateTime.Now < this.TimelineBehavior.AdjustedTimelineStart || DateTime.Now > this.TimelineBehavior.AdjustedTimelineEnd)
        {
          this.TodayIndicatorElement.Visibility = ElementVisibility.Collapsed;
          this.TimelineTodayIndicatorElement.Visibility = ElementVisibility.Collapsed;
        }
        else
        {
          this.TodayIndicatorElement.Visibility = this.ShowTodayIndicator ? ElementVisibility.Visible : ElementVisibility.Collapsed;
          this.TimelineTodayIndicatorElement.Visibility = this.ShowTimelineTodayIndicator ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        }
      }
      else
      {
        if (!(e.PropertyName == "ShowTodayIndicator") && !(e.PropertyName == "ShowTimelineTodayIndicator"))
          return;
        this.TodayIndicatorElement.Visibility = this.ShowTodayIndicator ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        this.TimelineTodayIndicatorElement.Visibility = this.ShowTimelineTodayIndicator ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        this.Invalidate(true);
      }
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.UpdateInnerState();
      this.UpdateTimelineDataItems();
      this.UpdateTimelineZoom();
    }
  }
}
