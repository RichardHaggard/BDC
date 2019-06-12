// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRangeSelectorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI.RangeSelector.InterfacesAndEnum;

namespace Telerik.WinControls.UI
{
  public class RadRangeSelectorElement : RangeSelectorVisualElementWithOrientation
  {
    private List<RangeSelectorScaleContainerElement> topLeftScales = new List<RangeSelectorScaleContainerElement>();
    private List<RangeSelectorScaleContainerElement> bottomRightScales = new List<RangeSelectorScaleContainerElement>();
    private bool showScroll = true;
    private ViewPosition scrollViewPosition = ViewPosition.BottomRight;
    private float zoomFactor = 1f;
    private int layoutsRefreshRateInMillisecond = 5;
    private RangeSelectorBodyElement bodyElement;
    private RangeSelectorScrollElement scrollSelectorElement;
    private Orientation orientation;
    private UpdateMode updateMode;
    private bool enableFastScrolling;
    private bool isMouseUp;
    private bool shouldFireSelectionChangeEvent;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = true;
      this.StretchHorizontally = true;
    }

    protected override void CreateChildElements()
    {
      this.bodyElement = new RangeSelectorBodyElement();
      this.scrollSelectorElement = new RangeSelectorScrollElement();
      this.Children.Add((RadElement) this.bodyElement);
      this.Children.Add((RadElement) this.scrollSelectorElement);
    }

    public virtual void InitializeElements()
    {
      IRangeSelectorElement associatedElement = this.BodyElement.ViewContainer.AssociatedElement as IRangeSelectorElement;
      this.TopLeftScales.Clear();
      this.BottomRightScales.Clear();
      if (associatedElement != null)
      {
        this.Children.Clear();
        float width = this.DesiredSize.Width;
        float height = this.DesiredSize.Height;
        associatedElement.InitializeRangeSelectorView(width, height);
        this.topLeftScales = associatedElement.GetTopLeftScales();
        this.bottomRightScales = associatedElement.GetBottomRightScales();
        foreach (RangeSelectorScaleContainerElement topLeftScale in this.topLeftScales)
        {
          ScaleInitializingEventArgs e = new ScaleInitializingEventArgs(topLeftScale);
          this.OnScaleInitializing(e);
          if (!e.Cancel)
            this.Children.Add((RadElement) e.ScaleElement);
          else
            topLeftScale.DisplayScale = false;
        }
        foreach (RangeSelectorScaleContainerElement bottomRightScale in this.bottomRightScales)
        {
          ScaleInitializingEventArgs e = new ScaleInitializingEventArgs(bottomRightScale);
          this.OnScaleInitializing(e);
          if (!e.Cancel)
            this.Children.Add((RadElement) e.ScaleElement);
          else
            bottomRightScale.DisplayScale = false;
        }
        this.ReorderScales();
        this.Children.Add((RadElement) this.bodyElement);
        this.Children.Add((RadElement) this.scrollSelectorElement);
        associatedElement.AssociatedViewStart = this.StartRange;
        associatedElement.AssociatedViewEnd = this.EndRange;
        associatedElement.UpdateAssociatedView();
      }
      this.ElementTree.ApplyThemeToElementTree();
    }

    public RangeSelectorBodyElement BodyElement
    {
      get
      {
        return this.bodyElement;
      }
    }

    public RangeSelectorScrollElement ScrollSelectorElement
    {
      get
      {
        return this.scrollSelectorElement;
      }
    }

    public List<RangeSelectorScaleContainerElement> TopLeftScales
    {
      get
      {
        return this.topLeftScales;
      }
    }

    public List<RangeSelectorScaleContainerElement> BottomRightScales
    {
      get
      {
        return this.bottomRightScales;
      }
    }

    public bool ShowScroll
    {
      get
      {
        if (this.AssociatedElement == null || !(this.AssociatedElement is IRangeSelectorElement))
          return false;
        return this.showScroll;
      }
      set
      {
        if (this.showScroll == value)
          return;
        this.showScroll = value;
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (ShowScroll));
      }
    }

    public ViewPosition ScrollViewPosition
    {
      get
      {
        return this.scrollViewPosition;
      }
      set
      {
        if (this.scrollViewPosition == value)
          return;
        this.scrollViewPosition = value;
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (ScrollViewPosition));
      }
    }

    public Orientation Orientation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        if (this.orientation == value)
          return;
        this.orientation = value;
        this.OnNotifyPropertyChanged(nameof (Orientation));
      }
    }

    public UpdateMode UpdateMode
    {
      get
      {
        return this.updateMode;
      }
      set
      {
        if (this.updateMode == value)
          return;
        this.updateMode = value;
        this.OnNotifyPropertyChanged(nameof (UpdateMode));
      }
    }

    public RadElement AssociatedElement
    {
      get
      {
        return this.bodyElement.ViewContainer.AssociatedElement;
      }
      set
      {
        this.bodyElement.ViewContainer.AssociatedElement = value;
        this.InitializeElements();
        this.ResetLayout(true);
        this.OnNotifyPropertyChanged(nameof (AssociatedElement));
      }
    }

    public float StartRange
    {
      get
      {
        return this.bodyElement.ViewContainer.TrackingElement.StartRange;
      }
      set
      {
        if ((double) value == (double) this.StartRange || (double) value < 0.0 || (double) value > (double) (100 - this.MinSelectionLength))
          return;
        this.bodyElement.ViewContainer.TrackingElement.SetupStartRangeWithAllEvents(value);
        this.OnNotifyPropertyChanged(nameof (StartRange));
      }
    }

    public float EndRange
    {
      get
      {
        return this.bodyElement.ViewContainer.TrackingElement.EndRange;
      }
      set
      {
        if ((double) value == (double) this.EndRange || (double) value < (double) this.MinSelectionLength || (double) value > 100.0)
          return;
        this.bodyElement.ViewContainer.TrackingElement.SetupEndRangeWithAllEvents(value);
        this.OnNotifyPropertyChanged(nameof (EndRange));
      }
    }

    [DefaultValue(0.0f)]
    public float RangeSelectorViewZoomStart
    {
      get
      {
        return this.bodyElement.ViewContainer.ZoomStart;
      }
      set
      {
        if ((double) value == (double) this.bodyElement.ViewContainer.ZoomStart)
          return;
        this.bodyElement.ViewContainer.ZoomStart = value;
        this.ScrollSelectorElement.Start = value;
        this.OnNotifyPropertyChanged(nameof (RangeSelectorViewZoomStart));
      }
    }

    [DefaultValue(100f)]
    public float RangeSelectorViewZoomEnd
    {
      get
      {
        return this.bodyElement.ViewContainer.ZoomEnd;
      }
      set
      {
        if ((double) value == (double) this.bodyElement.ViewContainer.ZoomEnd)
          return;
        this.bodyElement.ViewContainer.ZoomEnd = value;
        this.ScrollSelectorElement.End = value;
        this.OnNotifyPropertyChanged(nameof (RangeSelectorViewZoomEnd));
      }
    }

    public float SelectionRectangleStart
    {
      get
      {
        return (float) Math.Min(this.BodyElement.ViewContainer.SelectionRectangle.ValueOne, this.BodyElement.ViewContainer.SelectionRectangle.ValueTwo);
      }
      set
      {
        if ((double) value < 0.0 || (double) value > 100.0)
          return;
        if (this.BodyElement.ViewContainer.SelectionRectangle.ValueOne <= this.BodyElement.ViewContainer.SelectionRectangle.ValueTwo)
          this.BodyElement.ViewContainer.SelectionRectangle.ValueOne = (double) value;
        else
          this.BodyElement.ViewContainer.SelectionRectangle.ValueTwo = (double) value;
        this.OnNotifyPropertyChanged(nameof (SelectionRectangleStart));
      }
    }

    public float SelectionRectangleEnd
    {
      get
      {
        return (float) Math.Max(this.BodyElement.ViewContainer.SelectionRectangle.ValueOne, this.BodyElement.ViewContainer.SelectionRectangle.ValueTwo);
      }
      set
      {
        if ((double) value < 0.0 || (double) value > 100.0)
          return;
        if (this.BodyElement.ViewContainer.SelectionRectangle.ValueOne <= this.BodyElement.ViewContainer.SelectionRectangle.ValueTwo)
          this.BodyElement.ViewContainer.SelectionRectangle.ValueTwo = (double) value;
        else
          this.BodyElement.ViewContainer.SelectionRectangle.ValueOne = (double) value;
        this.OnNotifyPropertyChanged(nameof (SelectionRectangleEnd));
      }
    }

    [DefaultValue(true)]
    public bool ShowButtons
    {
      get
      {
        return this.bodyElement.ShowButtons;
      }
      set
      {
        this.bodyElement.ShowButtons = value;
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (ShowButtons));
      }
    }

    public int MinSelectionLength
    {
      get
      {
        return this.BodyElement.ViewContainer.TrackingElement.MinSelectionLength;
      }
      set
      {
        this.BodyElement.ViewContainer.TrackingElement.MinSelectionLength = value;
        this.OnNotifyPropertyChanged(nameof (MinSelectionLength));
      }
    }

    public float ZoomFactor
    {
      get
      {
        return this.zoomFactor;
      }
      set
      {
        this.zoomFactor = value;
        this.OnNotifyPropertyChanged(nameof (ZoomFactor));
      }
    }

    public float TotalZoomFactor
    {
      get
      {
        if (this.AssociatedElement == null || !(this.AssociatedElement is IRangeSelectorElement))
          return 1f;
        float num1 = 0.0f;
        float num2 = 0.0f;
        if ((double) this.RangeSelectorViewZoomStart != 0.0)
          num1 = this.RangeSelectorViewZoomStart / 100f;
        if ((double) this.RangeSelectorViewZoomEnd != 100.0)
          num2 = (float) ((100.0 - (double) this.RangeSelectorViewZoomEnd) / 100.0);
        return (float) (1.0 + ((double) num1 + (double) num2) * (double) this.ZoomFactor);
      }
    }

    public bool EnableFastScrolling
    {
      get
      {
        return this.enableFastScrolling;
      }
      set
      {
        this.enableFastScrolling = value;
        this.OnNotifyPropertyChanged(nameof (EnableFastScrolling));
      }
    }

    internal bool IsMouseUp
    {
      get
      {
        return this.isMouseUp;
      }
      set
      {
        this.isMouseUp = value;
      }
    }

    internal bool ShouldFireSelectionChangeEvent
    {
      get
      {
        return this.shouldFireSelectionChangeEvent;
      }
      set
      {
        this.shouldFireSelectionChangeEvent = value;
      }
    }

    public int LayoutsRefreshRate
    {
      get
      {
        return this.layoutsRefreshRateInMillisecond;
      }
      set
      {
        this.layoutsRefreshRateInMillisecond = value;
      }
    }

    internal long LayoutsRefreshRateInTicks
    {
      get
      {
        return (long) this.LayoutsRefreshRate * 10000L;
      }
    }

    [Description("Occurs when the value of left thumb is changing.")]
    [Category("Behavior")]
    public event ValueChangingEventHandler ThumbLeftValueChanging;

    [Description("Occurs when the value of left thumb is changed.")]
    [Category("Behavior")]
    public event EventHandler ThumbLeftValueChanged;

    [Description("Occurs when the value of right thumb is changing.")]
    [Category("Behavior")]
    public event ValueChangingEventHandler ThumbRightValueChanging;

    [Description("Occurs when the value of right thumb is changed.")]
    [Category("Behavior")]
    public event EventHandler ThumbRightValueChanged;

    [Description("Occurs when the whole selection of the controls is about to change.")]
    [Category("Behavior")]
    public event RangeSelectorSelectionChangingEventHandler SelectionChanging;

    [Description("Occurs when the whole selection of the controls is changed.")]
    [Category("Behavior")]
    public event EventHandler SelectionChanged;

    [Description("Occurs when scale of the controls is Initializing")]
    [Category("Behavior")]
    public event ScaleInitializingEventHandler ScaleInitializing;

    public virtual void OnThumbLeftValueChanging(ValueChangingEventArgs e)
    {
      ValueChangingEventHandler leftValueChanging = this.ThumbLeftValueChanging;
      if (leftValueChanging == null)
        return;
      leftValueChanging((object) this, e);
    }

    public virtual void OnThumbLeftValueChanged(EventArgs e)
    {
      EventHandler leftValueChanged = this.ThumbLeftValueChanged;
      if (leftValueChanged == null)
        return;
      leftValueChanged((object) this, e);
    }

    public virtual void OnThumbRightValueChanging(ValueChangingEventArgs e)
    {
      ValueChangingEventHandler rightValueChanging = this.ThumbRightValueChanging;
      if (rightValueChanging == null)
        return;
      rightValueChanging((object) this, e);
    }

    public virtual void OnThumbRightValueChanged(EventArgs e)
    {
      EventHandler rightValueChanged = this.ThumbRightValueChanged;
      if (rightValueChanged == null)
        return;
      rightValueChanged((object) this, e);
    }

    public virtual void OnSelectionChanging(RangeSelectorSelectionChangingEventArgs e)
    {
      RangeSelectorSelectionChangingEventHandler selectionChanging = this.SelectionChanging;
      if (selectionChanging == null)
        return;
      selectionChanging((object) this, e);
    }

    public virtual void OnSelectionChanged(EventArgs e)
    {
      EventHandler selectionChanged = this.SelectionChanged;
      if (selectionChanged == null)
        return;
      selectionChanged((object) this, e);
    }

    public virtual void OnScaleInitializing(ScaleInitializingEventArgs e)
    {
      ScaleInitializingEventHandler scaleInitializing = this.ScaleInitializing;
      if (scaleInitializing == null)
        return;
      scaleInitializing((object) this, e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property.Name == "RightToLeft")
        this.OnNotifyPropertyChanged("RightToLeft");
      base.OnPropertyChanged(e);
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      if (!this.CanRaisePropertyChangeNotifications((RadPropertyValue) null))
        return;
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName == "RightToLeft")
        this.RightToLeft = false;
      else if (propertyName == "RangeSelectorViewZoomEnd" || propertyName == "RangeSelectorViewZoomStart")
      {
        foreach (RangeSelectorScaleContainerElement topLeftScale in this.TopLeftScales)
        {
          if (topLeftScale.DisplayScale)
            topLeftScale.InvalidateArrange(true);
        }
        foreach (RangeSelectorScaleContainerElement bottomRightScale in this.BottomRightScales)
        {
          if (bottomRightScale.DisplayScale)
            bottomRightScale.InvalidateArrange(true);
        }
      }
      else if (propertyName == "Orientation")
      {
        if (this.Orientation == Orientation.Horizontal)
        {
          this.bodyElement.LeftArrow.StretchVertically = true;
          this.bodyElement.LeftArrow.StretchHorizontally = false;
          this.BodyElement.RightArrow.StretchVertically = true;
          this.BodyElement.RightArrow.StretchHorizontally = false;
          this.BodyElement.LeftArrow.IsVertical = false;
          this.BodyElement.RightArrow.IsVertical = false;
          this.BodyElement.ViewContainer.TrackingElement.LeftThumb.IsVertical = false;
          this.BodyElement.ViewContainer.TrackingElement.RightThumb.IsVertical = false;
          this.BodyElement.ViewContainer.TrackingElement.Range.IsVertical = false;
          this.BodyElement.ViewContainer.TrackingElement.IsVertical = false;
        }
        else
        {
          this.bodyElement.LeftArrow.StretchVertically = false;
          this.bodyElement.LeftArrow.StretchHorizontally = true;
          this.BodyElement.RightArrow.StretchVertically = false;
          this.BodyElement.RightArrow.StretchHorizontally = true;
          this.BodyElement.LeftArrow.IsVertical = true;
          this.BodyElement.RightArrow.IsVertical = true;
          this.BodyElement.ViewContainer.TrackingElement.LeftThumb.IsVertical = true;
          this.BodyElement.ViewContainer.TrackingElement.RightThumb.IsVertical = true;
          this.BodyElement.ViewContainer.TrackingElement.Range.IsVertical = true;
          this.BodyElement.ViewContainer.TrackingElement.IsVertical = true;
        }
        this.BodyElement.Orientation = this.Orientation;
      }
      else
      {
        if (!(propertyName == "StartRange") && !(propertyName == "EndRange"))
          return;
        (this.AssociatedElement as IRangeSelectorElement)?.UpdateAssociatedView();
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.InitializeElements();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      if (this.Orientation == Orientation.Horizontal)
      {
        int num = (int) this.BorderLeftWidth + (int) this.BorderRightWidth;
        empty.Width = availableSize.Width;
        foreach (RangeSelectorScaleContainerElement topLeftScale in this.TopLeftScales)
        {
          if (topLeftScale.DisplayScale)
          {
            topLeftScale.Measure(availableSize);
            empty.Height += topLeftScale.DesiredSize.Height;
          }
        }
        foreach (RangeSelectorScaleContainerElement bottomRightScale in this.BottomRightScales)
        {
          if (bottomRightScale.DisplayScale)
          {
            bottomRightScale.Measure(availableSize);
            empty.Height += bottomRightScale.DesiredSize.Height;
          }
        }
        if (this.ShowScroll)
        {
          this.ScrollSelectorElement.Measure(new SizeF(availableSize.Width - (float) num, availableSize.Height));
          empty.Height += this.ScrollSelectorElement.DesiredSize.Height;
        }
        this.BodyElement.Measure(new SizeF(availableSize.Width - (float) num, availableSize.Height - empty.Height));
      }
      else
      {
        empty.Width = 0.0f;
        foreach (RangeSelectorScaleContainerElement topLeftScale in this.TopLeftScales)
        {
          if (topLeftScale.DisplayScale)
          {
            topLeftScale.Measure(availableSize);
            empty.Width += topLeftScale.DesiredSize.Width;
          }
        }
        foreach (RangeSelectorScaleContainerElement bottomRightScale in this.BottomRightScales)
        {
          if (bottomRightScale.DisplayScale)
          {
            bottomRightScale.Measure(availableSize);
            empty.Width += bottomRightScale.DesiredSize.Width;
          }
        }
        if (this.ShowScroll)
        {
          this.ScrollSelectorElement.Measure(new SizeF(availableSize.Width, availableSize.Height));
          empty.Width += this.ScrollSelectorElement.DesiredSize.Width;
        }
        this.BodyElement.Measure(new SizeF(availableSize.Width - empty.Width, availableSize.Height));
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.Children.Count == 0)
        return base.ArrangeOverride(finalSize);
      if (this.Orientation == Orientation.Horizontal)
      {
        float y1 = 0.0f;
        if (this.ScrollViewPosition == ViewPosition.TopLeft)
        {
          this.ScrollSelectorElement.Arrange(new RectangleF((float) (int) this.BorderLeftWidth, y1, this.ScrollSelectorElement.DesiredSize.Width, this.ScrollSelectorElement.DesiredSize.Height));
          y1 += this.ScrollSelectorElement.DesiredSize.Height;
        }
        foreach (RangeSelectorScaleContainerElement topLeftScale in this.TopLeftScales)
        {
          if (topLeftScale.DisplayScale)
          {
            topLeftScale.Arrange(new RectangleF(0.0f, y1, topLeftScale.DesiredSize.Width, topLeftScale.DesiredSize.Height));
            y1 += topLeftScale.DesiredSize.Height;
          }
        }
        this.BodyElement.Arrange(new RectangleF((float) (int) this.BorderLeftWidth, y1, this.BodyElement.DesiredSize.Width, this.BodyElement.DesiredSize.Height));
        float y2 = y1 + this.BodyElement.DesiredSize.Height;
        foreach (RangeSelectorScaleContainerElement bottomRightScale in this.BottomRightScales)
        {
          if (bottomRightScale.DisplayScale)
          {
            bottomRightScale.Arrange(new RectangleF(0.0f, y2, bottomRightScale.DesiredSize.Width, bottomRightScale.DesiredSize.Height));
            y2 += bottomRightScale.DesiredSize.Height;
          }
        }
        if (this.ScrollViewPosition == ViewPosition.BottomRight)
        {
          this.ScrollSelectorElement.Arrange(new RectangleF((float) (int) this.BorderLeftWidth, y2, this.ScrollSelectorElement.DesiredSize.Width, this.ScrollSelectorElement.DesiredSize.Height));
          float num = y2 + this.ScrollSelectorElement.DesiredSize.Height;
        }
      }
      else
      {
        float x1 = 0.0f;
        if (this.ScrollViewPosition == ViewPosition.TopLeft)
        {
          this.ScrollSelectorElement.Arrange(new RectangleF(x1, 0.0f, this.ScrollSelectorElement.DesiredSize.Width, finalSize.Height));
          x1 += this.ScrollSelectorElement.DesiredSize.Width;
        }
        foreach (RangeSelectorScaleContainerElement topLeftScale in this.TopLeftScales)
        {
          if (topLeftScale.DisplayScale)
          {
            topLeftScale.Arrange(new RectangleF(x1, 0.0f, topLeftScale.DesiredSize.Width, topLeftScale.DesiredSize.Height));
            x1 += topLeftScale.DesiredSize.Width;
          }
        }
        this.BodyElement.Arrange(new RectangleF(x1, 0.0f, this.BodyElement.DesiredSize.Width, this.BodyElement.DesiredSize.Height));
        float x2 = x1 + this.BodyElement.DesiredSize.Width;
        foreach (RangeSelectorScaleContainerElement bottomRightScale in this.BottomRightScales)
        {
          if (bottomRightScale.DisplayScale)
          {
            bottomRightScale.Arrange(new RectangleF(x2, 0.0f, bottomRightScale.DesiredSize.Width, bottomRightScale.DesiredSize.Height));
            x2 += bottomRightScale.DesiredSize.Width;
          }
        }
        if (this.ScrollViewPosition == ViewPosition.BottomRight)
        {
          this.ScrollSelectorElement.Arrange(new RectangleF(x2, 0.0f, this.ScrollSelectorElement.DesiredSize.Width, finalSize.Height));
          float num = x2 + this.ScrollSelectorElement.DesiredSize.Width;
        }
      }
      return finalSize;
    }

    private void ReorderScales()
    {
      List<RangeSelectorScaleContainerElement> containerElementList1 = new List<RangeSelectorScaleContainerElement>();
      List<RangeSelectorScaleContainerElement> containerElementList2 = new List<RangeSelectorScaleContainerElement>();
      foreach (RangeSelectorScaleContainerElement topLeftScale in this.topLeftScales)
      {
        if (topLeftScale.ScalePostion == ViewPosition.TopLeft)
          containerElementList1.Add(topLeftScale);
        else
          containerElementList2.Add(topLeftScale);
      }
      foreach (RangeSelectorScaleContainerElement bottomRightScale in this.bottomRightScales)
      {
        if (bottomRightScale.ScalePostion == ViewPosition.TopLeft)
          containerElementList1.Add(bottomRightScale);
        else
          containerElementList2.Add(bottomRightScale);
      }
      this.topLeftScales = containerElementList1;
      this.bottomRightScales = containerElementList2;
    }
  }
}
