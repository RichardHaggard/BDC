// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarTableElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CalendarTableElement : CalendarVisualElement
  {
    private DateTime? leapYearFocusDate = new DateTime?();
    private int columns;
    private int rows;
    private int contentXShift;
    private int contentYShift;
    private LightVisualElement selectedElement;
    private bool allowFadeAndAnimation;
    private int zIndex;
    private DateTime selectionStartDate;
    private DateTime minSelectedDate;
    private CalendarCellElement lastSelectedCell;
    private DateTime selectionEndDate;
    private CalendarCellElement rowHeaderVisualElement;
    private CalendarCellElement columnHeaderVisualElement;
    private int horizontalCellSpacing;
    private int verticalCellSpacing;
    protected List<LightVisualElement> visualElements;
    protected CalendarView cachedView;
    private ZoomLevel level;
    private int oldSelectedYear;
    private LightVisualElement animationLayer1;
    private LightVisualElement animationLayer2;
    private LightVisualElement animationLayer3;
    private Bitmap startImage;
    private Bitmap endImage;
    private Bitmap animationBackgroundImage;
    private CalendarCellElement currentSelectedCell;
    private AnimatedPropertySetting layerOldImageOpacitySettings;
    private AnimatedPropertySetting layerOldImageScaleTransformSettings;
    private AnimatedPropertySetting layerOldImagePositionOffsetettings;
    private AnimatedPropertySetting layerNewImageOpacitySettings;
    private AnimatedPropertySetting layerNewImageScaleTransformSettings;
    private AnimatedPropertySetting layerNewImagePositionOffsetettings;
    private bool isAnimating;

    protected internal CalendarTableElement(
      CalendarVisualElement owner,
      RadCalendar calendar,
      CalendarView view)
      : base((CalendarVisualElement) null, calendar, view)
    {
      this.Owner = owner;
      this.cachedView = view;
      this.cachedView.Rows = this.Calendar.Rows;
      this.cachedView.Columns = this.Calendar.Columns;
      this.cachedView.PropertyChanged += new PropertyChangedEventHandler(this.view_PropertyChanged);
      this.InitializeChildren();
    }

    protected internal CalendarTableElement(CalendarVisualElement owner)
      : this(owner, (RadCalendar) null, (CalendarView) null)
    {
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireCalendarEvents(this.Calendar);
      this.cachedView.PropertyChanged -= new PropertyChangedEventHandler(this.view_PropertyChanged);
      int count = this.VisualElements.Count;
      for (int index = 0; index < count; ++index)
      {
        CalendarCellElement visualElement = this.VisualElements[index] as CalendarCellElement;
        if (visualElement != null)
        {
          visualElement.Calendar = (RadCalendar) null;
          visualElement.View = (CalendarView) null;
          visualElement.Dispose();
        }
      }
      this.cachedView = (CalendarView) null;
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (CalendarTableElement);
      this.AllowFadeAndAnimation = false;
    }

    private void UnwireCalendarEvents(RadCalendar calendar)
    {
      if (calendar == null)
        return;
      calendar.MouseUp -= new MouseEventHandler(this.calendar_MouseUp);
      calendar.MouseDown -= new MouseEventHandler(this.calendar_MouseDown);
      calendar.MouseMove -= new MouseEventHandler(this.calendar_MouseMove);
      calendar.PropertyChanged -= new PropertyChangedEventHandler(this.Calendar_PropertyChanged);
    }

    private void WireCalendarEvents(RadCalendar calendar)
    {
      if (calendar == null)
        return;
      calendar.MouseUp += new MouseEventHandler(this.calendar_MouseUp);
      calendar.MouseDown += new MouseEventHandler(this.calendar_MouseDown);
      calendar.MouseMove += new MouseEventHandler(this.calendar_MouseMove);
      calendar.PropertyChanged += new PropertyChangedEventHandler(this.Calendar_PropertyChanged);
    }

    private void CreateAnimationLayers()
    {
      this.animationLayer1 = new LightVisualElement();
      this.animationLayer1.StretchHorizontally = true;
      this.animationLayer1.StretchVertically = true;
      this.animationLayer1.ShouldHandleMouseInput = false;
      this.animationLayer1.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.animationLayer1.Image = (Image) this.startImage;
      this.animationLayer1.ImageAlignment = ContentAlignment.MiddleCenter;
      this.animationLayer1.ZIndex = 100001;
      this.animationLayer2 = new LightVisualElement();
      this.animationLayer2.StretchHorizontally = true;
      this.animationLayer2.StretchVertically = true;
      this.animationLayer2.ShouldHandleMouseInput = false;
      this.animationLayer2.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.animationLayer2.Image = (Image) this.endImage;
      this.animationLayer2.ImageAlignment = ContentAlignment.MiddleCenter;
      this.animationLayer2.ZIndex = 100001;
      this.animationLayer3 = new LightVisualElement();
      this.animationLayer3.StretchHorizontally = true;
      this.animationLayer3.StretchVertically = true;
      this.animationLayer3.ShouldHandleMouseInput = false;
      this.animationLayer3.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.animationLayer3.ImageAlignment = ContentAlignment.MiddleCenter;
      this.animationLayer3.ImageLayout = ImageLayout.Stretch;
      this.animationLayer3.ZIndex = 100000;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.CreateAnimationLayers();
    }

    public virtual int Rows
    {
      get
      {
        if (this.Level == ZoomLevel.Days)
          return this.View.Rows;
        return this.rows;
      }
    }

    public virtual int Columns
    {
      get
      {
        if (this.Level == ZoomLevel.Days)
          return this.View.Columns;
        return this.columns;
      }
    }

    public override CalendarView View
    {
      get
      {
        return this.cachedView;
      }
      set
      {
        if (this.cachedView == value)
          return;
        this.View.PropertyChanged -= new PropertyChangedEventHandler(this.view_PropertyChanged);
        value.PropertyChanged += new PropertyChangedEventHandler(this.view_PropertyChanged);
        this.cachedView = value;
        this.RefreshVisuals(true);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override RadCalendar Calendar
    {
      get
      {
        return base.Calendar;
      }
      internal set
      {
        if (this.Calendar == value)
          return;
        this.UnwireCalendarEvents(this.Calendar);
        base.Calendar = value;
        this.WireCalendarEvents(this.Calendar);
      }
    }

    public LightVisualElement SelectedElement
    {
      get
      {
        return this.selectedElement;
      }
      set
      {
        this.selectedElement = value;
      }
    }

    protected bool AllowFadeAndAnimation
    {
      get
      {
        return this.allowFadeAndAnimation;
      }
      set
      {
        this.allowFadeAndAnimation = value;
      }
    }

    internal List<LightVisualElement> VisualElements
    {
      get
      {
        if (this.visualElements == null)
          this.CreateVisuals();
        return this.visualElements;
      }
      set
      {
        this.visualElements = value;
      }
    }

    protected internal override CalendarVisualElement Owner
    {
      get
      {
        return base.Owner;
      }
      set
      {
        if (this.Owner == value)
          return;
        if (this.Owner != null)
          this.UnwireCalendarEvents(this.Owner.Calendar);
        base.Owner = value;
        if (this.Owner == null)
          return;
        this.UnwireCalendarEvents(this.Owner.Calendar);
        this.WireCalendarEvents(this.Owner.Calendar);
      }
    }

    protected internal virtual int CellHorizontalSpacing
    {
      get
      {
        return this.horizontalCellSpacing;
      }
      set
      {
        this.horizontalCellSpacing = value;
        this.InvalidateArrange();
      }
    }

    protected internal virtual int CellVerticalSpacing
    {
      get
      {
        return this.verticalCellSpacing;
      }
      set
      {
        this.verticalCellSpacing = value;
        this.InvalidateArrange();
      }
    }

    public ZoomLevel Level
    {
      get
      {
        return this.level;
      }
      set
      {
        if (this.Level == value || value < ZoomLevel.Days || value > ZoomLevel.YearsRange)
          return;
        bool isLevelUp = value > this.Level;
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
          this.startImage = this.GetAsBitmap((Brush) solidBrush, 0.0f, this.ScaleTransform);
        DrillDirection direction = value > this.level ? DrillDirection.Up : DrillDirection.Down;
        CalendarZoomChangingEventArgs args = new CalendarZoomChangingEventArgs(direction);
        this.Calendar.OnZoomChanging(args);
        if (args.Cancel)
        {
          if (direction != DrillDirection.Down || this.currentSelectedCell == null)
            return;
          this.SelectCell(this.currentSelectedCell);
        }
        else
        {
          if (this.Level == ZoomLevel.Years && value == ZoomLevel.YearsRange)
            this.oldSelectedYear = this.Calendar.FocusedDate.Year;
          this.level = value;
          this.ResetVisuals();
          using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
            this.animationBackgroundImage = this.GetAsBitmap((Brush) solidBrush, 0.0f, this.ScaleTransform);
          this.UpdateLayout();
          using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
            this.endImage = this.GetAsBitmap((Brush) solidBrush, 0.0f, this.ScaleTransform);
          this.Calendar.OnZoomChanged(new CalendarZoomChangedEventArgs(direction));
          this.Animate(isLevelUp);
        }
      }
    }

    public virtual void Initialize()
    {
    }

    public virtual void Initialize(int rows, int columns)
    {
      int capacity = rows * columns;
      this.visualElements = new List<LightVisualElement>(capacity);
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < capacity; ++index)
      {
        CalendarCellElement calendarCellElement = new CalendarCellElement((CalendarVisualElement) this);
        if (this.View.ShowSelector && this.Level == ZoomLevel.Days)
        {
          if (num1 == 0 || num2 == 0)
          {
            int num3 = (int) calendarCellElement.SetValue(CalendarCellElement.IsHeaderCellProperty, (object) true);
          }
        }
        else if (this.View.ShowRowHeaders)
        {
          if (num2 == 0 && this.Level == ZoomLevel.Days)
          {
            int num4 = (int) calendarCellElement.SetValue(CalendarCellElement.IsHeaderCellProperty, (object) true);
          }
        }
        else if (this.View.ShowColumnHeaders && this.Level == ZoomLevel.Days && num1 == 0)
        {
          int num5 = (int) calendarCellElement.SetValue(CalendarCellElement.IsHeaderCellProperty, (object) true);
        }
        calendarCellElement.Row = num1;
        calendarCellElement.Column = num2;
        ++num2;
        if (num2 == columns)
        {
          num2 = 0;
          ++num1;
        }
        this.visualElements.Add((LightVisualElement) calendarCellElement);
      }
    }

    public virtual void ResetVisuals()
    {
      this.cachedView = this.View;
      this.visualElements.Clear();
      this.DisposeChildren();
      this.View.ReInitialize();
      this.CreateVisuals();
      this.InitializeChildren();
      this.ResetCellsProperties();
    }

    private void Calendar_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Culture"))
        return;
      if (this.View != null && this.View.Calendar != null)
        this.InvalidateHeaders();
      this.Invalidate();
    }

    private void calendar_MouseMove(object sender, MouseEventArgs e)
    {
      this.DraggingSelection(e);
      if (e.Button != MouseButtons.Left || this.View.Children.Count != 0)
        return;
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty))
        {
          if (visualElement.ControlBoundingRectangle.Contains(e.Location))
          {
            if (this.Calendar.AllowFishEye && !visualElement.isAnimating && visualElement.AutoSize)
            {
              visualElement.ZIndex = this.VisualElements.Count + this.zIndex;
              ++this.zIndex;
              visualElement.PerformForwardAnimation();
              int num = (int) visualElement.SetValue(CalendarCellElement.IsZoomingProperty, (object) true);
            }
          }
          else if (this.Calendar.AllowFishEye && !visualElement.AutoSize)
          {
            visualElement.PerformReverseAnimation();
            int num = (int) visualElement.SetValue(CalendarCellElement.IsZoomingProperty, (object) false);
          }
        }
      }
    }

    private void calendar_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.Calendar.ReadOnly)
        return;
      if (this.View.Children.Count == 0)
      {
        foreach (CalendarCellElement visualElement in this.VisualElements)
        {
          if (this.Calendar.AllowFishEye && visualElement.AutoSize && this.Calendar.FocusedDate.Equals(visualElement.Date) && (visualElement.ControlBoundingRectangle.Contains(e.Location) && !(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty)))
          {
            visualElement.ZIndex = this.VisualElements.Count + this.zIndex;
            ++this.zIndex;
            int num = (int) visualElement.SetValue(CalendarCellElement.IsZoomingProperty, (object) true);
            visualElement.PerformForwardAnimation();
          }
        }
      }
      this.SetCalendarCellSelectedState(e);
    }

    private void calendar_MouseUp(object sender, MouseEventArgs e)
    {
      this.selectionStartDate = DateTime.MinValue;
      if (this.Calendar.MultiViewColumns > 1 || this.Calendar.MultiViewRows > 1)
      {
        foreach (MonthViewElement child in (this.Calendar.CalendarElement.CalendarVisualElement as MultiMonthViewElement).GetMultiTableElement().Children)
        {
          foreach (CalendarCellElement visualElement in child.TableElement.VisualElements)
          {
            if (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty) && (visualElement.AllowFishEye || this.Calendar.AllowFishEye))
            {
              visualElement.PerformReverseAnimation();
              int num = (int) visualElement.SetValue(CalendarCellElement.IsZoomingProperty, (object) false);
            }
          }
        }
      }
      else
      {
        if (this.View.Children.Count == 0)
        {
          foreach (CalendarCellElement visualElement in this.VisualElements)
          {
            if (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty) && (visualElement.AllowFishEye || this.Calendar.AllowFishEye))
            {
              visualElement.PerformReverseAnimation();
              int num = (int) visualElement.SetValue(CalendarCellElement.IsZoomingProperty, (object) false);
            }
          }
        }
        this.RenderContent();
      }
    }

    private void view_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.SetBehaviorOnPropertyChange(e.PropertyName);
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      if (!(args.RoutedEvent.EventName == "CellClickedEvent"))
        return;
      this.SelectedElement = sender as LightVisualElement;
      if (!this.AllowFadeAndAnimation)
        return;
      Rectangle bounds = sender.Parent.Bounds;
      this.AutoSize = false;
      AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting(RadElement.BoundsProperty, (object) new Rectangle(bounds.Right / 2, bounds.Bottom / 2, (int) (0.100000001490116 * (double) bounds.Width), (int) (0.100000001490116 * (double) bounds.Height)), (object) new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height), 10, 20);
      animatedPropertySetting.ApplyValue((RadObject) sender.Parent);
      animatedPropertySetting.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) => this.AutoSize = true);
      new AnimatedPropertySetting(VisualElement.OpacityProperty, (object) 0.0, (object) 1.0, 20, 40).ApplyValue((RadObject) sender.Parent);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      Size empty = Size.Empty;
      if (this.visualElements.Count > 0)
      {
        empty.Width = (int) ((double) this.visualElements[0].DesiredSize.Width * (double) this.columns);
        empty.Height = (int) ((double) this.visualElements[0].DesiredSize.Height * (double) this.rows);
      }
      return (SizeF) empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.View == null || this.visualElements.Count == 0 || (this.columns == 0 || this.rows == 0))
        return finalSize;
      int xCellSize;
      if (this.StretchHorizontally)
      {
        xCellSize = ((int) finalSize.Width - this.horizontalCellSpacing * this.columns) / this.Columns;
        if ((this.View.ShowRowHeaders || this.View.ShowSelector) && this.Level == ZoomLevel.Days)
          xCellSize = ((int) finalSize.Width - this.Calendar.HeaderWidth - this.horizontalCellSpacing * this.columns) / this.Columns;
      }
      else
        xCellSize = (int) this.visualElements[0].DesiredSize.Width;
      int yCellSize;
      if (this.StretchHorizontally)
      {
        yCellSize = ((int) finalSize.Height - this.verticalCellSpacing * this.rows) / this.Rows;
        if ((this.View.ShowColumnHeaders || this.View.ShowSelector) && this.Level == ZoomLevel.Days)
          yCellSize = ((int) finalSize.Height - this.Calendar.HeaderHeight - this.verticalCellSpacing * this.rows) / this.Rows;
      }
      else
        yCellSize = (int) this.visualElements[0].DesiredSize.Height;
      int xOffset = 0;
      int yOffset = 0;
      if (this.Level == ZoomLevel.Days && (this.View.ShowRowHeaders || this.View.ShowSelector) && this.Level == ZoomLevel.Days)
        xOffset = this.Calendar.HeaderWidth;
      if (this.Level == ZoomLevel.Days && (this.View.ShowColumnHeaders || this.View.ShowSelector) && this.Level == ZoomLevel.Days)
        yOffset = this.Calendar.HeaderHeight;
      this.ArrangeHeaders(xCellSize, yCellSize, xOffset, yOffset);
      this.ArrangeContentCells(xCellSize, yCellSize, xOffset, yOffset);
      if (this.animationLayer1 != null && this.animationLayer1.Parent == this)
        this.animationLayer1.Arrange(new RectangleF(new PointF(0.0f, 0.0f), this.animationLayer1.DesiredSize));
      if (this.animationLayer2 != null && this.animationLayer2.Parent == this)
        this.animationLayer2.Arrange(new RectangleF(new PointF(0.0f, 0.0f), this.animationLayer2.DesiredSize));
      if (this.animationLayer3 != null && this.animationLayer3.Parent == this)
        this.animationLayer3.Arrange(new RectangleF(new PointF(0.0f, 0.0f), this.animationLayer3.DesiredSize));
      return finalSize;
    }

    protected virtual void ArrangeHeaders(int xCellSize, int yCellSize, int xOffset, int yOffset)
    {
      if (this.View.ShowColumnHeaders && this.Level == ZoomLevel.Days)
      {
        this.columnHeaderVisualElement.Arrange((RectangleF) new Rectangle(xOffset, 0, (xCellSize + this.horizontalCellSpacing) * this.Calendar.Columns, this.Calendar.HeaderHeight));
        int columns = this.Columns;
        for (int index = 0; index < columns; ++index)
        {
          LightVisualElement element = this.GetElement(0, index + this.contentXShift);
          Point location = Point.Empty;
          location = new Point(index * xCellSize + xOffset, 0);
          location.X += (index + 1) * this.horizontalCellSpacing;
          ((CalendarCellElement) element).ProposedBounds = new Rectangle(location, new Size(xCellSize, yOffset));
          element.Arrange(new RectangleF((PointF) location, (SizeF) new Size(xCellSize, yOffset)));
          int x = location.X;
        }
      }
      if (this.View.ShowRowHeaders && this.Level == ZoomLevel.Days)
      {
        this.rowHeaderVisualElement.Arrange(new RectangleF(0.0f, (float) yOffset, (float) this.Calendar.HeaderWidth, (float) ((yCellSize + this.verticalCellSpacing) * this.Calendar.Rows)));
        int rows = this.Rows;
        for (int index = 0; index < rows; ++index)
        {
          LightVisualElement element = this.GetElement(index + this.contentYShift, 0);
          Point location = Point.Empty;
          location = new Point(0, index * yCellSize + yOffset);
          location.Y += (index + 1) * this.verticalCellSpacing;
          ((CalendarCellElement) element).ProposedBounds = new Rectangle(location, new Size(xOffset, yCellSize));
          element.Arrange(new RectangleF((PointF) location, (SizeF) new Size(xOffset, yCellSize)));
          int y = location.Y;
        }
      }
      if (!this.View.ShowSelector || this.Level != ZoomLevel.Days)
        return;
      ((CalendarCellElement) this.VisualElements[0]).ProposedBounds = new Rectangle(Point.Empty, new Size(xOffset, yOffset));
      this.VisualElements[0].Arrange(new RectangleF(0.0f, 0.0f, (float) xOffset, (float) yOffset));
    }

    protected virtual void ArrangeContentCells(
      int xCellSize,
      int yCellSize,
      int xOffset,
      int yOffset)
    {
      PointF empty = PointF.Empty;
      for (int row = 0; row < this.Rows; ++row)
      {
        empty.Y += row > 0 ? (float) yCellSize : (float) yOffset;
        empty.Y += (float) this.verticalCellSpacing;
        empty.X = 0.0f;
        for (int column = 0; column < this.Columns; ++column)
        {
          LightVisualElement lightVisualElement = this.Level != ZoomLevel.Days ? this.GetElement(row, column) : this.GetContentElement(row, column);
          empty.X += column > 0 ? (float) xCellSize : (float) xOffset;
          empty.X += (float) this.horizontalCellSpacing;
          if (lightVisualElement is CalendarCellElement)
            ((CalendarCellElement) lightVisualElement).ProposedBounds = new Rectangle(Point.Ceiling(empty), new Size(xCellSize, yCellSize));
          lightVisualElement.Arrange(new RectangleF(empty, (SizeF) new Size(xCellSize, yCellSize)));
        }
      }
    }

    private void Animate(bool isLevelUp)
    {
      if (this.startImage == null || this.endImage == null || this.animationBackgroundImage == null)
        return;
      if (this.isAnimating)
      {
        this.layerOldImageOpacitySettings.Cancel((RadObject) this.animationLayer1);
        this.layerOldImageScaleTransformSettings.Cancel((RadObject) this.animationLayer1);
        this.layerOldImagePositionOffsetettings.Cancel((RadObject) this.animationLayer1);
        this.layerNewImageOpacitySettings.Cancel((RadObject) this.animationLayer2);
        this.layerNewImageScaleTransformSettings.Cancel((RadObject) this.animationLayer2);
        this.layerNewImagePositionOffsetettings.Cancel((RadObject) this.animationLayer2);
      }
      this.animationLayer1.Image = (Image) this.startImage;
      this.animationLayer2.Image = (Image) this.endImage;
      this.animationLayer3.Image = (Image) this.animationBackgroundImage;
      this.Children.Add((RadElement) this.animationLayer1);
      this.Children.Add((RadElement) this.animationLayer2);
      this.Children.Add((RadElement) this.animationLayer3);
      int frames = 10;
      int interval = 20;
      float num = 0.2f;
      if (isLevelUp)
      {
        this.layerOldImageOpacitySettings = new AnimatedPropertySetting(LightVisualElement.ImageOpacityProperty, (object) 1.0, (object) 0.0, frames, interval);
        this.layerOldImageScaleTransformSettings = new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) new SizeF(1f, 1f), (object) new SizeF(num, num), frames, interval);
        this.layerOldImagePositionOffsetettings = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) new SizeF(0.0f, 0.0f), (object) new SizeF((float) ((double) this.animationLayer1.Image.Width * (1.0 - (double) num) / 2.0), (float) ((double) this.animationLayer1.Image.Height * (1.0 - (double) num) / 2.0)), frames, interval);
        this.layerNewImageOpacitySettings = new AnimatedPropertySetting(LightVisualElement.ImageOpacityProperty, (object) 0.0, (object) 1.0, frames, interval);
        this.layerNewImageScaleTransformSettings = new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) new SizeF(2f, 2f), (object) new SizeF(1f, 1f), frames, interval);
        this.layerNewImagePositionOffsetettings = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) new SizeF((float) -(this.animationLayer1.Image.Width / 2), (float) -(this.animationLayer1.Image.Height / 2)), (object) SizeF.Empty, frames, interval);
      }
      else
      {
        this.layerOldImageOpacitySettings = new AnimatedPropertySetting(LightVisualElement.ImageOpacityProperty, (object) 1.0, (object) 0.0, frames, interval);
        this.layerOldImageScaleTransformSettings = new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) new SizeF(1f, 1f), (object) new SizeF(2f, 2f), frames, interval);
        this.layerOldImagePositionOffsetettings = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) SizeF.Empty, (object) new SizeF((float) -(this.animationLayer1.Image.Width / 2), (float) -(this.animationLayer1.Image.Height / 2)), frames, interval);
        this.layerNewImageOpacitySettings = new AnimatedPropertySetting(LightVisualElement.ImageOpacityProperty, (object) 0.0, (object) 1.0, frames, interval);
        this.layerNewImageScaleTransformSettings = new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) new SizeF(num, num), (object) new SizeF(1f, 1f), frames, interval);
        this.layerNewImagePositionOffsetettings = new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) new SizeF((float) ((double) this.animationLayer1.Image.Width * (1.0 - (double) num) / 2.0), (float) ((double) this.animationLayer1.Image.Height * (1.0 - (double) num) / 2.0)), (object) SizeF.Empty, frames, interval);
      }
      this.layerNewImageOpacitySettings.AnimationFinished += new AnimationFinishedEventHandler(this.layerNewImageOpacitySettings_AnimationFinished);
      this.isAnimating = true;
      this.layerOldImageOpacitySettings.ApplyValue((RadObject) this.animationLayer1);
      this.layerOldImageScaleTransformSettings.ApplyValue((RadObject) this.animationLayer1);
      this.layerOldImagePositionOffsetettings.ApplyValue((RadObject) this.animationLayer1);
      this.layerNewImageOpacitySettings.ApplyValue((RadObject) this.animationLayer2);
      this.layerNewImageScaleTransformSettings.ApplyValue((RadObject) this.animationLayer2);
      this.layerNewImagePositionOffsetettings.ApplyValue((RadObject) this.animationLayer2);
    }

    private void layerNewImageOpacitySettings_AnimationFinished(
      object sender,
      AnimationStatusEventArgs e)
    {
      (sender as AnimatedPropertySetting).AnimationFinished -= new AnimationFinishedEventHandler(this.layerNewImageOpacitySettings_AnimationFinished);
      this.Children.Remove((RadElement) this.animationLayer1);
      this.Children.Remove((RadElement) this.animationLayer2);
      this.Children.Remove((RadElement) this.animationLayer3);
      this.isAnimating = false;
      this.ElementTree.Control.Invalidate();
    }

    private void ResetCellsSelectionLogicallyInRange(DateTime startDate, DateTime endDate)
    {
      DateTime dateTime = startDate;
      if (startDate > endDate)
      {
        startDate = endDate;
        endDate = dateTime;
      }
      List<DateTime> dateTimeList = new List<DateTime>();
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Date >= startDate && visualElement.Date <= endDate)
          dateTimeList.Add(visualElement.Date);
      }
      this.Calendar.SelectedDates.RemoveRange(dateTimeList.ToArray());
    }

    private void ResetCellsSelectionInRange(DateTime startDate, DateTime endDate)
    {
      DateTime dateTime = startDate;
      if (startDate > endDate)
      {
        startDate = endDate;
        endDate = dateTime;
      }
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Date >= startDate && visualElement.Date <= endDate)
          visualElement.Selected = false;
      }
    }

    private void ResetCellsSelectionLogically()
    {
      List<DateTime> dateTimeList = new List<DateTime>();
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Visibility == ElementVisibility.Visible)
          dateTimeList.Add(visualElement.Date);
      }
      this.Calendar.SelectedDates.RemoveRange(dateTimeList.ToArray());
    }

    internal void ResetCellsSelection()
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Visibility == ElementVisibility.Visible)
          visualElement.Selected = false;
      }
    }

    private void SelectCell(CalendarCellElement cell)
    {
      this.lastSelectedCell = cell;
      bool isHeader = (bool) cell.GetValue(CalendarCellElement.IsHeaderCellProperty);
      List<CalendarCellElement> selectionCells = new List<CalendarCellElement>();
      List<CalendarCellElement> calendarCellElementList = new List<CalendarCellElement>();
      CellSelectionType cellSelectionType = this.GetCellSelectionType(cell, isHeader);
      List<DateTime> selectionDates1 = new List<DateTime>();
      List<DateTime> previousSelectedDates = new List<DateTime>();
      foreach (DateTime selectedDate in this.Calendar.SelectedDates)
        previousSelectedDates.Add(selectedDate);
      if (cellSelectionType == CellSelectionType.Cell && isHeader || cell.Visibility != ElementVisibility.Visible)
        return;
      List<DateTime> dateTimeList = new List<DateTime>();
      switch (cellSelectionType)
      {
        case CellSelectionType.Row:
          int row1 = cell.Row;
          this.GetRowSelection(cell, dateTimeList, row1);
          break;
        case CellSelectionType.Column:
          int column1 = cell.Column;
          this.GetColumnSelection(cell, dateTimeList, column1);
          break;
        case CellSelectionType.Cell:
          this.GetSimpleCellSelection(cell, isHeader, dateTimeList, previousSelectedDates);
          break;
        case CellSelectionType.AllCells:
          this.GetAllCellsSelection(cell, dateTimeList);
          break;
      }
      if (this.Calendar.CallOnSelectionChanging(this.Calendar.SelectedDates, dateTimeList).Cancel)
        return;
      this.Calendar.SelectedDates.BeginUpdate();
      this.Calendar.BeginUpdate();
      switch (cellSelectionType)
      {
        case CellSelectionType.Row:
          int row2 = cell.Row;
          this.HandleRowSelection(cell, selectionCells, calendarCellElementList, row2);
          break;
        case CellSelectionType.Column:
          int column2 = cell.Column;
          this.HandleColumnSelection(cell, selectionCells, calendarCellElementList, column2);
          break;
        case CellSelectionType.Cell:
          this.HandleSimpleCellSelection(cell, isHeader, selectionCells, previousSelectedDates);
          break;
        case CellSelectionType.AllCells:
          this.HandleAllCellsSelection(cell, selectionCells);
          break;
      }
      this.Calendar.SelectedDates.BeginUpdate();
      foreach (CalendarCellElement calendarCellElement in selectionCells)
        selectionDates1.Add(calendarCellElement.Date);
      this.ResetCellsSelectionLogically();
      this.ResetCellsSelection();
      this.SelectCellsLogically(selectionDates1, selectionCells);
      if (isHeader)
      {
        List<DateTime> selectionDates2 = new List<DateTime>();
        foreach (CalendarCellElement calendarCellElement in calendarCellElementList)
          selectionDates2.Add(calendarCellElement.Date);
        this.SelectCellsLogically(selectionDates2, calendarCellElementList);
      }
      this.Calendar.SelectedDates.EndUpdate();
      this.Calendar.EndUpdate();
      this.Calendar.CallOnSelectionChanged();
    }

    private CellSelectionType GetCellSelectionType(
      CalendarCellElement cell,
      bool isHeader)
    {
      CellSelectionType cellSelectionType = CellSelectionType.Cell;
      if (isHeader && this.Calendar.AllowMultipleSelect && (this.Calendar.ShowViewSelector && this.Calendar.AllowViewSelector) && (cell.Row == 0 && cell.Column == 0))
      {
        cellSelectionType = CellSelectionType.AllCells;
        cell.isChecked = !cell.isChecked;
      }
      else
      {
        if ((this.Calendar.ShowRowHeaders || this.Calendar.ShowViewSelector) && (cell.Row == 0 && cell.Column == 0) && this.Calendar.ShowColumnHeaders)
          return cellSelectionType;
        if (isHeader && this.Calendar.AllowMultipleSelect && (this.Calendar.ShowColumnHeaders && this.Calendar.AllowColumnHeaderSelectors) && cell.Row == 0)
        {
          cellSelectionType = CellSelectionType.Column;
          cell.isChecked = !cell.isChecked;
        }
        else if (isHeader && this.Calendar.AllowMultipleSelect && (this.Calendar.ShowRowHeaders && this.Calendar.AllowRowHeaderSelectors) && cell.Column == 0)
        {
          cellSelectionType = CellSelectionType.Row;
          cell.isChecked = !cell.isChecked;
        }
      }
      return cellSelectionType;
    }

    private void HandleAllCellsSelection(
      CalendarCellElement cell,
      List<CalendarCellElement> selectionCells)
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (!(visualElement.Date > this.Calendar.RangeMaxDate) && !(visualElement.Date < this.Calendar.RangeMinDate)))
        {
          visualElement.Selected = cell.isChecked;
          if (visualElement != cell && visualElement.Selected)
          {
            bool flag = (bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty);
            visualElement.isChecked = false;
            if (!flag)
              selectionCells.Add(visualElement);
          }
        }
      }
    }

    private void GetAllCellsSelection(CalendarCellElement cell, List<DateTime> selectionCells)
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (!(visualElement.Date > this.Calendar.RangeMaxDate) && !(visualElement.Date < this.Calendar.RangeMinDate)) && (visualElement != cell && !(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty)))
          selectionCells.Add(visualElement.Date);
      }
    }

    private void HandleColumnSelection(
      CalendarCellElement cell,
      List<CalendarCellElement> selectionCells,
      List<CalendarCellElement> otherSelectionCells,
      int column)
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (!(visualElement.Date > this.Calendar.RangeMaxDate) && !(visualElement.Date < this.Calendar.RangeMinDate)))
        {
          if (visualElement.Selected && visualElement != cell && visualElement.Column != column)
            otherSelectionCells.Add(visualElement);
          visualElement.Selected = cell.isChecked;
          if (visualElement != cell && visualElement.Selected)
          {
            if (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty))
              visualElement.isChecked = false;
            if (visualElement.Column == column)
              selectionCells.Add(visualElement);
          }
        }
      }
    }

    private void GetColumnSelection(
      CalendarCellElement cell,
      List<DateTime> selectionCells,
      int column)
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (!(visualElement.Date > this.Calendar.RangeMaxDate) && !(visualElement.Date < this.Calendar.RangeMinDate)) && (visualElement != cell && visualElement.Column == column))
          selectionCells.Add(visualElement.Date);
      }
    }

    private void HandleRowSelection(
      CalendarCellElement cell,
      List<CalendarCellElement> selectionCells,
      List<CalendarCellElement> otherRowsSelectionCells,
      int row)
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (!(visualElement.Date > this.Calendar.RangeMaxDate) && !(visualElement.Date < this.Calendar.RangeMinDate)))
        {
          if (visualElement.Selected && visualElement != cell && visualElement.Row != row)
            otherRowsSelectionCells.Add(visualElement);
          visualElement.Selected = cell.isChecked;
          if (visualElement != cell && visualElement.Selected)
          {
            if (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty))
              visualElement.isChecked = false;
            if (visualElement.Row == row)
              selectionCells.Add(visualElement);
          }
        }
      }
    }

    private void GetRowSelection(CalendarCellElement cell, List<DateTime> selectionCells, int row)
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (!(visualElement.Date > this.Calendar.RangeMaxDate) && !(visualElement.Date < this.Calendar.RangeMinDate)) && (visualElement != cell && visualElement.Row == row))
          selectionCells.Add(visualElement.Date);
      }
    }

    public CalendarCellElement GetCellByDate(DateTime date)
    {
      foreach (CalendarCellElement child in this.Children)
      {
        if (child.Date.Equals(date))
          return child;
      }
      return (CalendarCellElement) null;
    }

    private void HandleSimpleCellSelection(
      CalendarCellElement cell,
      bool isHeader,
      List<CalendarCellElement> selectionCells,
      List<DateTime> previousSelectedDates)
    {
      if (isHeader)
        return;
      if (cell.OtherMonth && !this.Calendar.AllowMultipleView)
      {
        DateTime date = cell.Date;
        this.Calendar.FocusedDate = date;
        if (!previousSelectedDates.Contains(date))
        {
          this.Calendar.SelectedDate = date;
          CalendarCellElement cellByDate = this.GetCellByDate(date);
          selectionCells.Add(cellByDate);
        }
        if (!this.Calendar.AllowMultipleSelect)
          return;
        foreach (CalendarCellElement visualElement in this.VisualElements)
        {
          if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (!visualElement.Date.Equals(date) && previousSelectedDates.Contains(visualElement.Date)))
          {
            visualElement.Selected = true;
            selectionCells.Add(visualElement);
          }
        }
      }
      else
      {
        if (cell.Enabled && cell.Date <= this.Calendar.RangeMaxDate && cell.Date >= this.Calendar.RangeMinDate)
          cell.Selected = !cell.Selected;
        if (cell.Selected)
          selectionCells.Add(cell);
        if (this.Calendar.AllowMultipleSelect)
        {
          foreach (CalendarCellElement visualElement in this.VisualElements)
          {
            if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (visualElement != cell && visualElement.Selected))
              selectionCells.Add(visualElement);
          }
        }
        else
        {
          foreach (CalendarCellElement visualElement in this.VisualElements)
          {
            if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled)
              visualElement.isChecked = false;
          }
        }
        if (!cell.Selected)
          return;
        this.Calendar.FocusedDate = cell.Date;
        if (!this.Calendar.AllowMultipleView || this.Calendar.AllowMultipleSelect || !(this.Calendar.CalendarElement.CalendarVisualElement is MultiMonthViewElement))
          return;
        selectionCells.Clear();
        foreach (MonthViewElement visualElement1 in (this.Calendar.CalendarElement.CalendarVisualElement as MultiMonthViewElement).GetMultiTableElement().VisualElements)
        {
          visualElement1.TableElement.ResetCellsSelection();
          foreach (RadObject visualElement2 in visualElement1.TableElement.VisualElements)
          {
            CalendarCellElement calendarCellElement = visualElement2 as CalendarCellElement;
            if (calendarCellElement != null && !(bool) calendarCellElement.GetValue(CalendarCellElement.IsHeaderCellProperty) && calendarCellElement.Date == this.Calendar.FocusedDate)
              selectionCells.Add(calendarCellElement);
          }
        }
      }
    }

    private void GetSimpleCellSelection(
      CalendarCellElement cell,
      bool isHeader,
      List<DateTime> selectionCells,
      List<DateTime> previousSelectedDates)
    {
      if (isHeader)
        return;
      if (cell.OtherMonth && !this.Calendar.AllowMultipleView)
      {
        DateTime date = cell.Date;
        if (!previousSelectedDates.Contains(date))
        {
          CalendarCellElement cellByDate = this.GetCellByDate(date);
          selectionCells.Add(cellByDate.Date);
        }
        if (!this.Calendar.AllowMultipleSelect)
          return;
        foreach (CalendarCellElement visualElement in this.VisualElements)
        {
          if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (!visualElement.Date.Equals(date) && previousSelectedDates.Contains(visualElement.Date) && !selectionCells.Contains(visualElement.Date)))
            selectionCells.Add(visualElement.Date);
        }
      }
      else
      {
        if (cell.Enabled && cell.Date <= this.Calendar.RangeMaxDate && (cell.Date >= this.Calendar.RangeMinDate && !cell.Selected))
          selectionCells.Add(cell.Date);
        if (this.Calendar.AllowMultipleSelect)
        {
          foreach (CalendarCellElement visualElement in this.VisualElements)
          {
            if (visualElement.Visibility == ElementVisibility.Visible && visualElement.Enabled && (visualElement != cell && visualElement.Selected))
              selectionCells.Add(visualElement.Date);
          }
        }
        else
        {
          foreach (CalendarCellElement visualElement in this.VisualElements)
          {
            if (visualElement.Visibility == ElementVisibility.Visible)
            {
              int num = visualElement.Enabled ? 1 : 0;
            }
          }
        }
        if (!cell.Selected || !this.Calendar.AllowMultipleView || (this.Calendar.AllowMultipleSelect || !(this.Calendar.CalendarElement.CalendarVisualElement is MultiMonthViewElement)))
          return;
        foreach (MonthViewElement visualElement1 in (this.Calendar.CalendarElement.CalendarVisualElement as MultiMonthViewElement).GetMultiTableElement().VisualElements)
        {
          visualElement1.TableElement.ResetCellsSelection();
          foreach (RadObject visualElement2 in visualElement1.TableElement.VisualElements)
          {
            CalendarCellElement calendarCellElement = visualElement2 as CalendarCellElement;
            if (calendarCellElement != null && !(bool) calendarCellElement.GetValue(CalendarCellElement.IsHeaderCellProperty) && calendarCellElement.Date == this.Calendar.FocusedDate)
              selectionCells.Add(calendarCellElement.Date);
          }
        }
      }
    }

    private void SelectCellsLogically(
      List<DateTime> selectionDates,
      List<CalendarCellElement> selectionCells)
    {
      int index = 0;
      foreach (CalendarCellElement selectionCell in selectionCells)
      {
        selectionCell.Selected = true;
        selectionCell.Date = selectionDates[index];
        ++index;
      }
      if (!this.Calendar.AllowMultipleSelect)
        this.Calendar.SelectedDates.Clear();
      this.Calendar.SelectedDates.AddRange(selectionDates.ToArray());
    }

    private void DraggingSelection(MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left || !this.Calendar.AllowMultipleSelect || (!this.Calendar.AllowSelect || !(this.selectionStartDate != DateTime.MinValue)))
        return;
      this.SelectOnMouseMove(e);
    }

    private void SetCalendarCellSelectedState(MouseEventArgs e)
    {
      if (!this.Calendar.AllowSelect || this.View is MultiMonthView)
        return;
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Enabled && visualElement.HitTest(e.Location))
        {
          int res = this.Calendar.MultiViewRows * this.Calendar.MultiViewColumns;
          int realChildren = res;
          this.GetInitializedChildrenCount(ref res, ref realChildren);
          if (res == realChildren)
          {
            this.selectionStartDate = visualElement.Date;
            this.selectionEndDate = visualElement.Date;
            this.minSelectedDate = visualElement.Date;
            if (this.Level == ZoomLevel.Days)
            {
              this.SelectCell(visualElement);
              break;
            }
            if (this.Level == ZoomLevel.YearsRange)
              this.Calendar.FocusedDate = new DateTime(visualElement.Date.Year / 10 * 10 + this.oldSelectedYear - this.oldSelectedYear / 10 * 10, visualElement.Date.Month, visualElement.Date.Day);
            else
              this.Calendar.FocusedDate = visualElement.Date;
            this.currentSelectedCell = visualElement;
            --this.Level;
            if (this.Calendar.CalendarElement.CalendarNavigationElement == null)
              break;
            this.Calendar.CalendarElement.CalendarNavigationElement.SetText();
            break;
          }
        }
      }
    }

    private void SelectOnMouseMove(MouseEventArgs e)
    {
      if (this.View is MultiMonthView)
        return;
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (this.lastSelectedCell == null)
          this.lastSelectedCell = visualElement;
        if (visualElement.HitTest(e.Location) && this.lastSelectedCell.Date != visualElement.Date)
        {
          this.lastSelectedCell = visualElement;
          int res = this.Calendar.MultiViewRows * this.Calendar.MultiViewColumns;
          int realChildren = res;
          this.GetInitializedChildrenCount(ref res, ref realChildren);
          if (res == realChildren)
          {
            if (visualElement.OtherMonth || (bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty))
              break;
            this.SelectCellsRange(this.selectionStartDate, visualElement.Date);
            break;
          }
        }
      }
    }

    private void SelectCellsRange(DateTime selectionStart, DateTime selectionEnd)
    {
      if (!this.Calendar.AllowMultipleSelect || !this.Calendar.AllowSelect)
        return;
      DateTime dateTime1 = selectionStart;
      DateTime dateTime2 = selectionEnd;
      if (this.selectionEndDate < dateTime2)
        this.selectionEndDate = dateTime2;
      if (this.minSelectedDate > dateTime2)
        this.minSelectedDate = dateTime2;
      if (dateTime1 > dateTime2)
      {
        DateTime dateTime3 = dateTime1;
        dateTime1 = dateTime2;
        dateTime2 = dateTime3;
      }
      bool flag = false;
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Date >= dateTime1 && visualElement.Date <= dateTime2 && (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty) && visualElement.Enabled))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return;
      this.Calendar.SelectedDates.BeginUpdate();
      List<DateTime> newDates = new List<DateTime>();
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        if (visualElement.Date >= dateTime1 && visualElement.Date <= dateTime2 && (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty) && visualElement.Enabled))
          newDates.Add(visualElement.Date);
      }
      if (this.Calendar.CallOnSelectionChanging(this.Calendar.SelectedDates, newDates).Cancel)
      {
        this.Calendar.SelectedDates.EndUpdate();
      }
      else
      {
        this.ResetCellsSelectionInRange(this.minSelectedDate, this.selectionEndDate);
        this.ResetCellsSelectionLogicallyInRange(this.minSelectedDate, this.selectionEndDate);
        List<DateTime> dateTimeList = new List<DateTime>();
        foreach (CalendarCellElement visualElement in this.VisualElements)
        {
          if (visualElement.Date >= dateTime1 && visualElement.Date <= dateTime2 && (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty) && visualElement.Enabled))
          {
            visualElement.Selected = true;
            dateTimeList.Add(visualElement.Date);
          }
        }
        this.Calendar.SelectedDates.AddRange(dateTimeList.ToArray());
        this.Calendar.SelectedDates.EndUpdate();
        this.Calendar.CallOnSelectionChanged();
      }
    }

    private void GetInitializedChildrenCount(ref int res, ref int realChildren)
    {
      MultiMonthViewElement calendarVisualElement = this.Calendar.CalendarElement.CalendarVisualElement as MultiMonthViewElement;
      if (calendarVisualElement != null)
        realChildren = (calendarVisualElement.Children[0].Children[1] as CalendarMultiMonthViewTableElement).Children.Count;
      if (this.Calendar.MultiViewColumns <= 1 && this.Calendar.MultiViewRows <= 1)
      {
        res = realChildren = 1;
      }
      else
      {
        RadElement parent = this.Parent;
        res = 1;
        realChildren = 3;
        for (; parent != null; parent = parent.Parent)
        {
          if ((object) parent.GetType() == (object) typeof (MultiMonthViewElement))
          {
            res = realChildren = 1;
            break;
          }
        }
      }
    }

    protected virtual void SetBehaviorOnPropertyChange(string propertyName)
    {
      switch (propertyName)
      {
        case "AllowViewSelector":
        case "AllowRowHeaderSelectors":
        case "AllowColumnHeaderSelectors":
          this.RefreshVisuals(true);
          break;
        case "MonthLayout":
        case "Orientation":
        case "ShowColumnHeaders":
        case "ShowRowHeaders":
        case "HeaderHeight":
        case "HeaderWidth":
        case "MultiViewRows":
        case "MultiViewColumns":
        case "ShowViewSelector":
        case "Columns":
        case "Rows":
        case "ShowOtherMonthsDays":
          this.ResetVisuals();
          break;
        case "CellHorizontalSpacing":
          this.CellHorizontalSpacing = this.View.CellHorizontalSpacing;
          break;
        case "CellVerticalSpacing":
          this.CellVerticalSpacing = this.View.CellVerticalSpacing;
          break;
        case "ZoomFactor":
          this.SetCellsProperty("ZoomFactor");
          break;
        case "AllowFishEye":
          this.SetCellsProperty("AllowFishEye");
          break;
        case "ViewSelectorText":
          if (!this.Calendar.ShowViewSelector)
            break;
          this.VisualElements[0].Text = this.cachedView.ViewSelectorText;
          break;
        case "ViewSelectorImage":
          if (!this.Calendar.ShowViewSelector)
            break;
          this.VisualElements[0].Image = this.cachedView.ViewSelectorImage;
          break;
        case "RowHeaderImage":
          this.InvalidateRowHeaders((this.View as MonthView).FirstCalendarDay(this.View.EffectiveVisibleDate()));
          break;
        case "RowHeaderText":
          this.InvalidateRowHeaders((this.View as MonthView).FirstCalendarDay(this.View.EffectiveVisibleDate()));
          break;
        case "ColumnHeaderImage":
          this.InvalidateColumnHeaders((this.View as MonthView).FirstCalendarDay(this.View.EffectiveVisibleDate()));
          break;
        case "ColumnHeaderText":
          this.InvalidateColumnHeaders((this.View as MonthView).FirstCalendarDay(this.View.EffectiveVisibleDate()));
          break;
        case "CellPadding":
          this.SetCellsProperty("CellPadding");
          break;
        case "CellMargin":
          this.SetCellsProperty("CellMargin");
          break;
      }
    }

    private void SetCellsProperty(string propertyName)
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        switch (propertyName)
        {
          case "ZoomFactor":
            visualElement.ZoomFactor = this.cachedView.ZoomFactor;
            continue;
          case "AllowFishEye":
            visualElement.AllowFishEye = this.Calendar.AllowFishEye;
            continue;
          case "ShowOtherMonthsDays":
            if (visualElement.OtherMonth)
            {
              if (this.cachedView.ShowOtherMonthsDays)
              {
                visualElement.Visibility = ElementVisibility.Visible;
                continue;
              }
              visualElement.Visibility = ElementVisibility.Hidden;
              continue;
            }
            continue;
          case "CellMargin":
            visualElement.Margin = this.cachedView.CellMargin;
            continue;
          case "CellPadding":
            visualElement.Padding = this.cachedView.CellPadding;
            continue;
          case "Focused":
            bool flag = visualElement.Date == this.Calendar.FocusedDate;
            visualElement.Focused = flag;
            continue;
          default:
            continue;
        }
      }
    }

    protected virtual void InitializeChildren()
    {
      this.DisposeChildren();
      this.rowHeaderVisualElement = new CalendarCellElement((CalendarVisualElement) this);
      this.columnHeaderVisualElement = new CalendarCellElement((CalendarVisualElement) this);
      this.rowHeaderVisualElement.Class = "RowHeaderVisualElement";
      this.columnHeaderVisualElement.Class = "ColumnHeaderVisualElement";
      int num1 = (int) this.rowHeaderVisualElement.SetDefaultValueOverride(LightVisualElement.DrawFillProperty, (object) true);
      int num2 = (int) this.rowHeaderVisualElement.SetDefaultValueOverride(LightVisualElement.DrawBorderProperty, (object) true);
      int num3 = (int) this.columnHeaderVisualElement.SetDefaultValueOverride(LightVisualElement.DrawFillProperty, (object) true);
      int num4 = (int) this.columnHeaderVisualElement.SetDefaultValueOverride(LightVisualElement.DrawBorderProperty, (object) true);
      if (this.Level == ZoomLevel.Days)
      {
        this.Children.Add((RadElement) this.rowHeaderVisualElement);
        this.Children.Add((RadElement) this.columnHeaderVisualElement);
      }
      for (int index = 0; index < this.VisualElements.Count; ++index)
      {
        this.visualElements[index].ZIndex = 2 + index;
        this.Children.Add((RadElement) this.visualElements[index]);
      }
      this.CreateAnimationLayers();
    }

    protected internal virtual void CreateVisuals()
    {
      if (this.Level == ZoomLevel.Days)
      {
        this.View.GetContentOffset(out this.contentXShift, out this.contentYShift);
        this.View.GetViewRowsAndColumns(out this.rows, out this.columns);
        this.Initialize(this.rows, this.columns);
      }
      else
      {
        this.rows = 3;
        this.columns = 4;
        this.contentXShift = 0;
        this.contentYShift = 0;
        this.Initialize(3, 4);
      }
      this.SetViewProperties();
    }

    public override void RefreshVisuals(bool unconditional)
    {
      foreach (CalendarCellElement visualElement in this.VisualElements)
      {
        visualElement.Focused = false;
        visualElement.Today = false;
        visualElement.OutOfRange = false;
        visualElement.SpecialDay = false;
        visualElement.Children.Clear();
        visualElement.Image = (Image) null;
        if (unconditional)
          visualElement.Selected = false;
        else if ((!this.Calendar.SelectedDates.Contains(visualElement.Date) || !this.View.IsDateInView(visualElement.Date)) && visualElement.Selected)
          visualElement.Selected = false;
        visualElement.OtherMonth = false;
        visualElement.WeekEnd = false;
      }
      this.SetViewProperties();
    }

    internal virtual void SetVisualElementsText()
    {
      if (!this.Calendar.RTL)
      {
        for (int index = 0; index < this.VisualElements.Count; ++index)
        {
          switch (this.level)
          {
            case ZoomLevel.Days:
              this.VisualElements[index].Text = string.Concat((object) index);
              break;
            case ZoomLevel.Months:
              this.VisualElements[index].Text = this.Calendar.Culture.DateTimeFormat.AbbreviatedMonthNames[index] ?? "";
              break;
            case ZoomLevel.Years:
              int num1 = Math.Max(1, this.Calendar.Culture.Calendar.GetYear(this.Calendar.FocusedDate) / 10 * 10 - 1);
              this.VisualElements[index].Text = (num1 + index).ToString() ?? "";
              break;
            case ZoomLevel.YearsRange:
              int num2 = Math.Max(1, this.Calendar.Culture.Calendar.GetYear(this.Calendar.FocusedDate) / 100 * 100 - 10);
              int num3 = num2 + index * 10;
              int num4 = num2 + 9 + index * 10;
              switch (this.Calendar.CellAlign)
              {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                  this.VisualElements[index].Text = num3.ToString() + "-" + Environment.NewLine + (object) num4;
                  continue;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                  this.VisualElements[index].Text = " " + (object) num3 + "-" + Environment.NewLine + (object) num4;
                  continue;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                  this.VisualElements[index].Text = " " + (object) num3 + "-" + Environment.NewLine + (object) num4;
                  continue;
                default:
                  continue;
              }
          }
        }
      }
      else
      {
        int num1 = 0;
        int num2 = 3;
        for (int index = 0; index < this.VisualElements.Count; ++index)
        {
          switch (this.level)
          {
            case ZoomLevel.Days:
              this.VisualElements[index].Text = string.Concat((object) index);
              break;
            case ZoomLevel.Months:
              this.VisualElements[index].Text = this.Calendar.Culture.DateTimeFormat.AbbreviatedMonthNames[num2 + num1 * 4] ?? "";
              break;
            case ZoomLevel.Years:
              int num3 = this.Calendar.Culture.Calendar.GetYear(this.Calendar.FocusedDate) / 10 * 10 - 1;
              this.VisualElements[index].Text = (num3 + num2 + num1 * 4).ToString() ?? "";
              break;
            case ZoomLevel.YearsRange:
              int num4 = this.Calendar.Culture.Calendar.GetYear(this.Calendar.FocusedDate) / 100 * 100 - 10;
              int num5 = num4 + (num2 + num1 * 4) * 10;
              int num6 = num4 + 9 + (num2 + num1 * 4) * 10;
              switch (this.Calendar.CellAlign)
              {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                  this.VisualElements[index].Text = num5.ToString() + "-" + Environment.NewLine + (object) num6;
                  break;
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                  this.VisualElements[index].Text = " " + (object) num5 + "-" + Environment.NewLine + (object) num6;
                  break;
                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                  this.VisualElements[index].Text = num5.ToString() + "-" + Environment.NewLine + (object) num6;
                  break;
              }
          }
          --num2;
          if (num2 < 0)
          {
            ++num1;
            num2 = 3;
          }
        }
      }
    }

    internal virtual void SetViewProperties()
    {
      this.SetVisualElementsText();
      foreach (RadCalendarDay specialDay in this.Calendar.SpecialDays)
        specialDay.IsTemplateSet = false;
      ((MonthViewElement) this.Owner).TitleElement.Text = this.cachedView.GetTitleContent();
      this.RenderContent();
      if ((this.cachedView.ShowRowHeaders || this.cachedView.ShowSelector || this.cachedView.ShowColumnHeaders) && this.Level == ZoomLevel.Days)
        this.InvalidateHeaders();
      this.Calendar.Invalidate();
    }

    internal void InvalidateHeaders()
    {
      this.InvalidateHeaders(this.View.Orientation);
    }

    internal void InvalidateHeaders(Orientation orientation)
    {
      DateTime firstDay = (this.View as MonthView).FirstCalendarDay(this.View.EffectiveVisibleDate());
      if (this.View.ShowColumnHeaders && this.Level == ZoomLevel.Days)
        this.InvalidateColumnHeaders(firstDay);
      if (this.View.ShowRowHeaders && this.Level == ZoomLevel.Days)
        this.InvalidateRowHeaders(firstDay);
      this.InvalidateViewSelector();
    }

    internal void InvalidateViewSelector()
    {
      if (!this.View.ShowSelector || this.Level != ZoomLevel.Days)
        return;
      this.SetHeaderCell(this.GetElement(0, 0) as CalendarCellElement, CalendarTableElement.HeaderType.View, this.View.ViewSelectorText, this.View.ViewSelectorImage);
    }

    private void ResetCellsProperties()
    {
      this.SetCellsProperty("ZoomFactor");
      this.SetCellsProperty("AllowFishEye");
      this.SetCellsProperty("ShowOtherMonthsDays");
      this.SetCellsProperty("CellMargin");
      this.SetCellsProperty("CellPadding");
      if (!this.View.ShowSelector || this.Level != ZoomLevel.Days)
        return;
      this.VisualElements[0].Text = this.Calendar.ViewSelectorText;
      this.VisualElements[0].Image = this.Calendar.ViewSelectorImage;
    }

    protected internal virtual void RefreshSelectedDates()
    {
      for (int index = 0; index < this.VisualElements.Count; ++index)
      {
        CalendarCellElement visualElement = this.VisualElements[index] as CalendarCellElement;
        if (!(bool) visualElement.GetValue(CalendarCellElement.IsHeaderCellProperty))
        {
          visualElement.Selected = false;
          if (this.Calendar.SelectedDates.Contains(visualElement.Date))
          {
            bool flag = this.Calendar.SelectedDates.Contains(visualElement.Date);
            if (flag && this.ShouldApplyStyle(visualElement.Date) && !visualElement.Selected)
              visualElement.Selected = flag;
          }
        }
      }
    }

    protected internal virtual void Recreate()
    {
    }

    protected internal virtual void RenderContent()
    {
      this.RenderContent(this.cachedView);
    }

    protected internal virtual void RenderContent(CalendarView view)
    {
      if (this.Level == ZoomLevel.Days)
        this.RenderContentLevelZero(view);
      else if (this.Level == ZoomLevel.Months)
        this.RenderContentLevelOne(view);
      else if (this.Level == ZoomLevel.Years)
        this.RenderContentLevelTwo(view);
      else
        this.RenderContentLevelThree(view);
    }

    protected virtual void RenderContentLevelThree(CalendarView view)
    {
      int year = Math.Max(1, this.Calendar.FocusedDate.Year / 100 * 100 - 10);
      int lastDayFromYear = this.GetLastDayFromYear(year);
      DateTime dateTime = new DateTime(year, this.Calendar.FocusedDate.Month, lastDayFromYear);
      for (int row = 0; row < this.Rows; ++row)
      {
        for (int column = 0; column < this.Columns; ++column)
        {
          LightVisualElement contentElement = this.GetContentElement(row, column);
          if (this.Calendar != null && this.Calendar.RTL)
            contentElement = this.GetContentElement(row, this.Columns - 1 - column);
          if (contentElement != null)
            this.SetCalendarCell(contentElement, dateTime);
          dateTime = !(this.Calendar.CurrentCalendar.MaxSupportedDateTime > dateTime.AddYears(10)) ? this.Calendar.CurrentCalendar.MaxSupportedDateTime : this.Calendar.CurrentCalendar.AddYears(dateTime, 10);
        }
      }
    }

    private int GetLastDayFromYear(int year)
    {
      int num = this.Calendar.FocusedDate.Day;
      if (num > DateTime.DaysInMonth(year, this.Calendar.FocusedDate.Month))
      {
        num = DateTime.DaysInMonth(year, this.Calendar.FocusedDate.Month);
        this.leapYearFocusDate = new DateTime?(this.Calendar.FocusedDate);
      }
      return num;
    }

    protected virtual void RenderContentLevelTwo(CalendarView view)
    {
      int year = Math.Max(1, this.Calendar.FocusedDate.Year / 10 * 10 - 1);
      int lastDayFromYear = this.GetLastDayFromYear(year);
      DateTime dateTime = new DateTime(year, this.Calendar.FocusedDate.Month, lastDayFromYear);
      for (int row = 0; row < this.Rows; ++row)
      {
        for (int column = 0; column < this.Columns; ++column)
        {
          LightVisualElement contentElement = this.GetContentElement(row, column);
          if (this.Calendar != null && this.Calendar.RTL)
            contentElement = this.GetContentElement(row, this.Columns - 1 - column);
          if (contentElement != null)
            this.SetCalendarCell(contentElement, dateTime);
          dateTime = !(this.Calendar.CurrentCalendar.MaxSupportedDateTime > dateTime.AddYears(1)) ? this.Calendar.CurrentCalendar.MaxSupportedDateTime : this.Calendar.CurrentCalendar.AddYears(dateTime, 1);
        }
      }
    }

    protected virtual void RenderContentLevelOne(CalendarView view)
    {
      DateTime dateTime = new DateTime(this.Calendar.FocusedDate.Year, 1, this.Calendar.FocusedDate.Day);
      for (int row = 0; row < this.Rows; ++row)
      {
        for (int column = 0; column < this.Columns; ++column)
        {
          LightVisualElement contentElement = this.GetContentElement(row, column);
          if (this.Calendar != null && this.Calendar.RTL)
            contentElement = this.GetContentElement(row, this.Columns - 1 - column);
          if (contentElement != null)
            this.SetCalendarCell(contentElement, dateTime);
          dateTime = !(this.Calendar.CurrentCalendar.MaxSupportedDateTime > dateTime.AddMonths(1)) ? this.Calendar.CurrentCalendar.MaxSupportedDateTime : this.Calendar.CurrentCalendar.AddMonths(dateTime, 1);
        }
      }
    }

    protected virtual void RenderContentLevelZero(CalendarView view)
    {
      if (this.leapYearFocusDate.HasValue)
      {
        DateTime dateTime = this.leapYearFocusDate.Value;
        this.leapYearFocusDate = new DateTime?();
        this.Calendar.FocusedDate = dateTime;
      }
      DateTime dateTime1 = view.ViewRenderStartDate;
      if (view.Orientation == Orientation.Horizontal)
      {
        for (int row = 0; row < this.Rows; ++row)
        {
          for (int column = 0; column < this.Columns; ++column)
          {
            LightVisualElement contentElement = this.GetContentElement(row, column);
            if (this.Calendar != null && this.Calendar.RTL)
              contentElement = this.GetContentElement(row, this.Columns - 1 - column);
            if (contentElement != null)
              this.SetCalendarCell(contentElement, dateTime1);
            dateTime1 = this.Calendar.CurrentCalendar.MaxSupportedDateTime.Ticks <= new TimeSpan(1, 0, 0, 0).Ticks + dateTime1.Ticks ? this.Calendar.CurrentCalendar.MaxSupportedDateTime : this.Calendar.CurrentCalendar.AddDays(dateTime1, 1);
          }
        }
      }
      else
      {
        if (view.Orientation != Orientation.Vertical)
          return;
        int num = 0;
        for (int column = 0; column < this.Columns; ++column)
        {
          for (int row = 0; row < this.Rows; ++row)
          {
            LightVisualElement contentElement = this.GetContentElement(row, column);
            if (this.Calendar != null && this.Calendar.RTL)
              contentElement = this.GetContentElement(this.Rows - 1 - row, column);
            if (contentElement != null)
              this.SetCalendarCell(contentElement, dateTime1);
            this.SetCalendarCell(contentElement, dateTime1);
            num += column + this.Rows;
            dateTime1 = this.Calendar.CurrentCalendar.AddDays(dateTime1, 1);
          }
        }
      }
    }

    private void SetCellAlignment(LightVisualElement tempCell)
    {
      tempCell.TextAlignment = this.Calendar.CellAlign;
    }

    private bool ShouldApplyStyle(DateTime processedDate)
    {
      return this.Calendar.ShowOtherMonthsDays || processedDate >= this.View.ViewStartDate && processedDate <= this.View.ViewEndDate;
    }

    private void InvalidateColumnHeaders(DateTime firstDay)
    {
      if (!this.View.ShowColumnHeaders)
        return;
      int dayOfWeek = (int) this.Calendar.CurrentCalendar.GetDayOfWeek(firstDay);
      int columns = this.Columns;
      for (int index = 0; index < columns; ++index)
      {
        if (this.View.Orientation == Orientation.Horizontal)
        {
          string empty = string.Empty;
          Image headerImage = (Image) null;
          int weekDay = dayOfWeek % 7;
          string headerText = (this.View as MonthView).GetDayHeaderString(weekDay);
          if (!string.IsNullOrEmpty(this.Calendar.ColumnHeaderText))
            headerText = this.Calendar.ColumnHeaderText;
          if (this.Calendar.ColumnHeaderImage != null)
            headerImage = this.Calendar.ColumnHeaderImage;
          CalendarCellElement element = (CalendarCellElement) this.GetElement(0, index + this.contentXShift);
          this.SetCellAlignment((LightVisualElement) element);
          if (this.Calendar != null && this.Calendar.RTL)
            element = (CalendarCellElement) this.GetElement(0, columns - 1 - index + this.contentXShift);
          element.ToolTipText = this.Calendar.DateTimeFormat.GetDayName((DayOfWeek) weekDay);
          this.SetHeaderCell(element, CalendarTableElement.HeaderType.Column, headerText, headerImage);
          ++dayOfWeek;
        }
        else
        {
          CalendarCellElement element = (CalendarCellElement) this.GetElement(0, index + this.contentXShift);
          DayOfWeek firstDayOfWeek = this.Calendar.FirstDayOfWeek != FirstDayOfWeek.Default ? (DayOfWeek) this.Calendar.FirstDayOfWeek : this.Calendar.DateTimeFormat.FirstDayOfWeek;
          int weekOfYear = this.Calendar.CurrentCalendar.GetWeekOfYear(firstDay, this.Calendar.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
          int weeks = 1;
          if (this.Calendar.MonthLayout == MonthLayout.Layout_14rows_x_3columns)
            weeks = 2;
          if (this.Calendar.MonthLayout == MonthLayout.Layout_21rows_x_2columns)
            weeks = 3;
          firstDay = this.Calendar.CurrentCalendar.AddWeeks(firstDay, weeks);
          string empty = string.Empty;
          Image headerImage = (Image) null;
          string rowHeaderText = weekOfYear.ToString();
          if (!string.IsNullOrEmpty(this.Calendar.RowHeaderText))
            rowHeaderText = this.Calendar.RowHeaderText;
          if (this.Calendar.RowHeaderImage != null)
            headerImage = this.Calendar.RowHeaderImage;
          this.SetHeaderCell(element, CalendarTableElement.HeaderType.Column, rowHeaderText, headerImage);
          element.Visibility = ElementVisibility.Visible;
          this.SetCellAlignment((LightVisualElement) element);
          int num = 0;
          for (int row = 1; row < this.rows; ++row)
          {
            if (this.GetElement(row, index + this.contentXShift).Visibility != ElementVisibility.Visible)
              ++num;
          }
          if (num == this.Rows)
            element.Visibility = ElementVisibility.Collapsed;
        }
      }
    }

    private void InvalidateRowHeaders(DateTime firstDay)
    {
      if (!this.View.ShowRowHeaders)
        return;
      DayOfWeek firstDayOfWeek = this.Calendar.FirstDayOfWeek != FirstDayOfWeek.Default ? (DayOfWeek) this.Calendar.FirstDayOfWeek : this.Calendar.DateTimeFormat.FirstDayOfWeek;
      int rows = this.Rows;
      int dayOfWeek = (int) this.Calendar.CurrentCalendar.GetDayOfWeek(firstDay);
      for (int index1 = 0; index1 < rows; ++index1)
      {
        if (this.View.Orientation == Orientation.Horizontal)
        {
          int num1;
          if (this.Calendar.DateTimeFormat.CalendarWeekRule == CalendarWeekRule.FirstDay)
          {
            bool flag = false;
            int year = firstDay.Year;
            for (int index2 = 0; index2 < 7; ++index2)
            {
              if (firstDay.AddDays((double) index2).Year != year && index1 == 0)
                flag = true;
            }
            num1 = !flag ? this.Calendar.CurrentCalendar.GetWeekOfYear(firstDay, this.Calendar.DateTimeFormat.CalendarWeekRule, firstDayOfWeek) : 1;
          }
          else
            num1 = this.Calendar.CurrentCalendar.GetWeekOfYear(firstDay, this.Calendar.DateTimeFormat.CalendarWeekRule, firstDayOfWeek);
          int weeks = 1;
          if (this.Calendar.MonthLayout == MonthLayout.Layout_14columns_x_3rows)
            weeks = 2;
          if (this.Calendar.MonthLayout == MonthLayout.Layout_21columns_x_2rows)
            weeks = 3;
          firstDay = this.Calendar.CurrentCalendar.AddWeeks(firstDay, weeks);
          string empty = string.Empty;
          Image headerImage = (Image) null;
          string rowHeaderText = num1.ToString();
          if (!string.IsNullOrEmpty(this.Calendar.RowHeaderText))
            rowHeaderText = this.Calendar.RowHeaderText;
          if (this.Calendar.RowHeaderImage != null)
            headerImage = this.Calendar.RowHeaderImage;
          int contentYshift = this.contentYShift;
          CalendarCellElement element = (CalendarCellElement) this.GetElement(index1 + contentYshift, 0);
          this.SetCellAlignment((LightVisualElement) element);
          element.ToolTipText = rowHeaderText;
          this.SetHeaderCell(element, CalendarTableElement.HeaderType.Row, rowHeaderText, headerImage);
          element.Visibility = ElementVisibility.Visible;
          int num2 = 0;
          for (int column = 1; column < this.columns; ++column)
          {
            if (this.GetElement(index1 + contentYshift, column).Visibility != ElementVisibility.Visible)
              ++num2;
          }
          if (num2 == this.Columns)
            element.Visibility = ElementVisibility.Collapsed;
        }
        else
        {
          string empty = string.Empty;
          Image headerImage = (Image) null;
          int weekDay = dayOfWeek % 7;
          string headerText = (this.View as MonthView).GetDayHeaderString(weekDay);
          if (!string.IsNullOrEmpty(this.Calendar.ColumnHeaderText))
            headerText = this.Calendar.ColumnHeaderText;
          if (this.Calendar.ColumnHeaderImage != null)
            headerImage = this.Calendar.ColumnHeaderImage;
          int contentYshift = this.contentYShift;
          CalendarCellElement element = (CalendarCellElement) this.GetElement(index1 + contentYshift, 0);
          if (this.Calendar != null && this.Calendar.RTL)
            element = (CalendarCellElement) this.GetElement(rows - 1 - index1 + contentYshift, 0);
          element.ToolTipText = this.Calendar.DateTimeFormat.GetDayName((DayOfWeek) weekDay);
          this.SetCellAlignment((LightVisualElement) element);
          this.SetHeaderCell(element, CalendarTableElement.HeaderType.Column, headerText, headerImage);
          ++dayOfWeek;
        }
      }
    }

    internal void SetHeaderCell(
      CalendarCellElement cell,
      CalendarTableElement.HeaderType type,
      string headerText,
      Image headerImage)
    {
      if (headerText != string.Empty && cell.Text != headerText)
        cell.Text = headerText;
      else if (headerText == string.Empty)
        cell.Text = "x";
      if (!cell.Date.Equals(this.View.ViewStartDate))
        cell.isChecked = false;
      cell.Date = this.View.ViewStartDate;
      int num = (int) cell.SetValue(CalendarCellElement.IsHeaderCellProperty, (object) true);
      if (cell.Image == headerImage)
        return;
      cell.Image = headerImage;
    }

    protected internal virtual void SetCalendarCell(
      LightVisualElement processedCell,
      DateTime processedDate)
    {
      this.SetCellAlignment(processedCell);
      CultureInfo culture = this.Calendar.Culture;
      if (this.Level == ZoomLevel.Days)
        processedCell.Text = processedDate.ToString(this.Calendar.DayCellFormat, (IFormatProvider) culture);
      CalendarCellElement calendarCellElement = processedCell as CalendarCellElement;
      calendarCellElement.Date = processedDate;
      bool flag1 = this.Calendar.SelectedDates.Contains(processedDate);
      calendarCellElement.isChecked = flag1;
      DayOfWeek dayOfWeek = this.Calendar.CurrentCalendar.GetDayOfWeek(processedDate);
      bool flag2 = dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
      bool flag3 = processedDate == DateTime.Today;
      bool flag4 = processedDate < this.Calendar.RangeMinDate || processedDate > this.Calendar.RangeMaxDate;
      bool flag5 = processedDate < this.View.ViewStartDate || this.View.ViewEndDate < processedDate;
      if (this.Level == ZoomLevel.Days)
      {
        if (processedDate < this.Calendar.RangeMinDate || processedDate > this.Calendar.RangeMaxDate)
          calendarCellElement.Enabled = false;
        else
          calendarCellElement.Enabled = true;
      }
      bool flag6 = this.Level != ZoomLevel.YearsRange ? this.Calendar.HasValueFocusedData() && processedDate == this.Calendar.FocusedDate : this.Calendar.HasValueFocusedData() && processedDate.Year == this.Calendar.FocusedDate.Year / 10 * 10;
      if (flag5)
        calendarCellElement.Visibility = !this.Calendar.ShowOtherMonthsDays ? ElementVisibility.Hidden : ElementVisibility.Visible;
      else
        calendarCellElement.Visibility = ElementVisibility.Visible;
      if (flag1 && this.ShouldApplyStyle(processedDate) && (this.Level == ZoomLevel.Days && !calendarCellElement.Selected))
        calendarCellElement.Selected = flag1;
      if (flag6)
      {
        if (!calendarCellElement.Focused)
          calendarCellElement.Focused = flag6;
        calendarCellElement.SetFocus();
      }
      if (flag3 && this.Level == ZoomLevel.Days && !calendarCellElement.Today)
        calendarCellElement.Today = flag3;
      if (flag4 && !calendarCellElement.OutOfRange)
        calendarCellElement.OutOfRange = flag4;
      if (flag5 && this.Level != ZoomLevel.Months)
      {
        if (this.Level == ZoomLevel.Years)
        {
          if (processedDate.Year == this.Calendar.FocusedDate.Year / 10 * 10 - 1 || processedDate.Year == this.Calendar.FocusedDate.Year / 10 * 10 - 1 + 11)
            calendarCellElement.OtherMonth = flag5;
        }
        else if (this.Level == ZoomLevel.YearsRange)
        {
          if (processedDate.Year == this.Calendar.FocusedDate.Year / 100 * 100 - 10 || processedDate.Year == this.Calendar.FocusedDate.Year / 100 * 100 + 100)
            calendarCellElement.OtherMonth = flag5;
        }
        else if (!calendarCellElement.OtherMonth)
          calendarCellElement.OtherMonth = flag5;
        calendarCellElement.Visibility = !this.Calendar.ShowOtherMonthsDays ? ElementVisibility.Hidden : ElementVisibility.Visible;
      }
      if (flag2 && this.Level == ZoomLevel.Days)
      {
        if (!calendarCellElement.WeekEnd)
          calendarCellElement.WeekEnd = flag2;
      }
      else if (!flag4)
        this.ProcessCalendarDays(processedDate);
      RadCalendarDay specialDay = (this.View as MonthView).GetSpecialDay(processedDate);
      if (specialDay != null && !calendarCellElement.SpecialDay)
      {
        calendarCellElement.SpecialDay = true;
        calendarCellElement.Image = specialDay.Image;
        if (specialDay.TemplateItem != null && !flag5 && !specialDay.IsTemplateSet)
        {
          specialDay.IsTemplateSet = true;
          calendarCellElement.Children.Add((RadElement) specialDay.TemplateItem);
          calendarCellElement.UpdateLayout();
        }
      }
      RadCalendarDay day = new RadCalendarDay(processedDate, this.Calendar != null ? this.Calendar.SpecialDays : (CalendarDayCollection) null);
      if (flag3)
        day.SetToday(true);
      if (flag2)
        day.SetWeekend(true);
      day.Selectable = true;
      if (this.Calendar.SelectedDates.Contains(processedDate))
        day.Selected = true;
      this.Calendar.CallOnElementRender((LightVisualElement) calendarCellElement, day, this.View);
    }

    internal virtual void ProcessCalendarDays(DateTime processedDate)
    {
    }

    protected internal virtual LightVisualElement GetContentElement(
      int row,
      int column)
    {
      return this.GetElement(row + this.contentYShift, column + this.contentXShift);
    }

    protected internal virtual LightVisualElement GetElement(int row, int column)
    {
      for (int index = 0; index < this.visualElements.Count; ++index)
      {
        CalendarCellElement visualElement = this.visualElements[index] as CalendarCellElement;
        if (visualElement.Column == column && visualElement.Row == row)
          return (LightVisualElement) visualElement;
      }
      return (LightVisualElement) null;
    }

    protected internal virtual List<CalendarCellElement> GetElementsByColumn(
      int column)
    {
      List<CalendarCellElement> calendarCellElementList = new List<CalendarCellElement>(this.Rows);
      for (int index = 0; index < this.visualElements.Count; ++index)
      {
        CalendarCellElement visualElement = this.visualElements[index] as CalendarCellElement;
        if (visualElement.Column == column)
          calendarCellElementList.Add(visualElement);
      }
      return calendarCellElementList;
    }

    protected internal virtual List<CalendarCellElement> GetElementsByRow(
      int row)
    {
      List<CalendarCellElement> calendarCellElementList = new List<CalendarCellElement>(this.Columns);
      for (int index = 0; index < this.visualElements.Count; ++index)
      {
        CalendarCellElement visualElement = this.visualElements[index] as CalendarCellElement;
        if (visualElement.Row == row)
          calendarCellElementList.Add(visualElement);
      }
      return calendarCellElementList;
    }

    protected internal virtual List<CalendarCellElement> GetViewElements()
    {
      List<CalendarCellElement> calendarCellElementList = new List<CalendarCellElement>(this.VisualElements.Count);
      for (int index = 0; index < this.VisualElements.Count; ++index)
      {
        CalendarCellElement visualElement = this.VisualElements[index] as CalendarCellElement;
        if (visualElement != null && visualElement.Date != DateTime.MinValue)
          calendarCellElementList.Add(visualElement);
      }
      return calendarCellElementList;
    }

    protected internal enum HeaderType
    {
      Row,
      Column,
      View,
    }
  }
}
