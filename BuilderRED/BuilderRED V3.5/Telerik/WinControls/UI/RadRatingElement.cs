// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRatingElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadRatingElement : StackLayoutElement
  {
    public static RadProperty ValueProperty = RadProperty.Register(nameof (Value), typeof (double?), typeof (RadRatingElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    private bool shouldPaintHover = true;
    private double maximum = 100.0;
    private double percentageRounding = 0.5;
    private PointF oldLocation = PointF.Empty;
    private LightVisualElement caption;
    private LightVisualElement subCaption;
    private LightVisualElement description;
    private StackLayoutElement elementsLayout;
    private RadItemOwnerCollection items;
    private ToolTip toolTip;
    private double toolTipValue;
    private string toolTipFormatString;
    private double toolTipPrecision;
    private Point toolTipOffset;
    private int toolTipDuration;
    private double selectedValue;
    private double currentValue;
    private double hoverValue;
    private double minimum;
    private bool readOnly;
    private RatingDirection direction;
    private RatingSelectionMode selectionMode;
    private bool isInRadGridView;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.toolTipValue = this.Minimum;
      this.ToolTipFormatString = "{0:0.0}";
      this.ToolTipPrecision = 0.1;
      this.ToolTipOffset = new Point(10, 15);
      this.ToolTipDuration = 2000;
      this.ClipDrawing = true;
    }

    protected override void CreateChildElements()
    {
      this.caption = new LightVisualElement();
      this.caption.Class = "Caption";
      this.caption.StretchVertically = false;
      this.caption.TextAlignment = ContentAlignment.TopLeft;
      this.subCaption = new LightVisualElement();
      this.subCaption.Class = "SubCaption";
      this.subCaption.StretchVertically = false;
      this.subCaption.TextAlignment = ContentAlignment.TopLeft;
      this.description = new LightVisualElement();
      this.description.Class = "Description";
      this.description.StretchVertically = false;
      this.description.TextAlignment = ContentAlignment.TopLeft;
      this.elementsLayout = new StackLayoutElement();
      this.elementsLayout.Orientation = Orientation.Horizontal;
      this.elementsLayout.StretchHorizontally = true;
      this.elementsLayout.StretchVertically = true;
      this.elementsLayout.ShouldHandleMouseInput = false;
      this.elementsLayout.ClipDrawing = true;
      this.elementsLayout.FitInAvailableSize = true;
      this.items = new RadItemOwnerCollection();
      this.items.Owner = (RadElement) this.elementsLayout;
      this.items.ItemTypes = this.GetItemsTypes();
      this.items.DefaultType = typeof (RatingStarVisualElement);
      this.Orientation = Orientation.Vertical;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Children.Add((RadElement) this.caption);
      this.Children.Add((RadElement) this.subCaption);
      this.Children.Add((RadElement) this.elementsLayout);
      this.Children.Add((RadElement) this.description);
    }

    [Bindable(true)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    public string Caption
    {
      get
      {
        return this.caption.Text;
      }
      set
      {
        if (!(this.caption.Text != value))
          return;
        this.caption.Text = value;
        this.OnNotifyPropertyChanged(nameof (Caption));
      }
    }

    public string SubCaption
    {
      get
      {
        return this.subCaption.Text;
      }
      set
      {
        if (!(this.subCaption.Text != value))
          return;
        this.subCaption.Text = value;
        this.OnNotifyPropertyChanged(nameof (SubCaption));
      }
    }

    public LightVisualElement CaptionElement
    {
      get
      {
        return this.caption;
      }
    }

    public LightVisualElement SubCaptionElement
    {
      get
      {
        return this.subCaption;
      }
    }

    public LightVisualElement DescriptionElement
    {
      get
      {
        return this.description;
      }
    }

    public StackLayoutElement ElementsLayout
    {
      get
      {
        return this.elementsLayout;
      }
    }

    public string ToolTipFormatString
    {
      get
      {
        return this.toolTipFormatString;
      }
      set
      {
        if (!(this.toolTipFormatString != value))
          return;
        this.toolTipFormatString = value;
        this.OnNotifyPropertyChanged(nameof (ToolTipFormatString));
      }
    }

    public virtual double SelectedValue
    {
      get
      {
        return this.selectedValue;
      }
      set
      {
        if (this.selectedValue == value)
          return;
        this.selectedValue = this.Maximum - value >= this.PercentageRounding / 100.0 * (this.Maximum - this.Minimum) ? (value - this.minimum >= this.PercentageRounding / 100.0 * (this.Maximum - this.Minimum) ? value : this.Minimum) : this.Maximum;
        this.OnNotifyPropertyChanged(nameof (SelectedValue));
      }
    }

    internal double CurrentValue
    {
      get
      {
        return this.currentValue;
      }
      set
      {
        if (this.currentValue == value)
          return;
        this.currentValue = value;
        this.OnNotifyPropertyChanged(nameof (CurrentValue));
      }
    }

    public double HoverValue
    {
      get
      {
        return this.hoverValue;
      }
      set
      {
        if (this.hoverValue == value)
          return;
        this.hoverValue = value;
        this.OnNotifyPropertyChanged(nameof (HoverValue));
      }
    }

    [DefaultValue(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Browsable(true)]
    public bool ShouldPaintHover
    {
      get
      {
        return this.shouldPaintHover;
      }
      set
      {
        if (this.shouldPaintHover == value)
          return;
        this.shouldPaintHover = value;
        this.OnNotifyPropertyChanged(nameof (ShouldPaintHover));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public double? Value
    {
      get
      {
        return (double?) this.GetValue(RadRatingElement.ValueProperty);
      }
      set
      {
        double? nullable1 = (double?) this.GetValue(RadRatingElement.ValueProperty);
        double? nullable2 = value;
        if ((nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
          return;
        ValueChangingEventArgs e = new ValueChangingEventArgs((object) value, (object) this.Value);
        this.OnValueChanging(e);
        if (e.Cancel)
          return;
        if (!value.HasValue)
        {
          int num1 = (int) this.SetValue(RadRatingElement.ValueProperty, (object) null);
        }
        else if (value.Value <= this.Minimum)
        {
          int num2 = (int) this.SetValue(RadRatingElement.ValueProperty, (object) this.Minimum);
        }
        else if (value.Value > this.Maximum)
        {
          int num3 = (int) this.SetValue(RadRatingElement.ValueProperty, (object) this.Maximum);
        }
        else
        {
          int num4 = (int) this.SetValue(RadRatingElement.ValueProperty, (object) value);
        }
        this.OnValueChanged(new EventArgs());
        this.OnNotifyPropertyChanged(nameof (Value));
      }
    }

    public double Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        if (this.minimum == value)
          return;
        if (value >= this.Maximum && this.ElementTree.Control != null && !(this.ElementTree.Control as RadControl).IsInitializing)
        {
          if (this.ElementTree.Control.Site != null)
            throw new ArgumentException("The Minimum should be lower than the Maximum");
          this.minimum = value;
          this.maximum = value + 1.0;
        }
        else
          this.minimum = value;
        if (this.Value.HasValue && this.Value.Value < this.Minimum)
          this.Value = new double?(this.Minimum);
        this.OnNotifyPropertyChanged(nameof (Minimum));
      }
    }

    [DefaultValue(100.0)]
    public double Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        if (this.maximum == value)
          return;
        if (value <= this.Minimum && this.ElementTree.Control != null && !(this.ElementTree.Control as RadControl).IsInitializing)
        {
          if (this.ElementTree.Control.Site != null)
            throw new ArgumentException("The MaxValue should be bigger than the MinValue");
          this.maximum = value;
          this.minimum = value - 1.0;
        }
        else
          this.maximum = value;
        if (this.Value.HasValue && this.Value.Value > this.Maximum)
          this.Value = new double?(this.Maximum);
        this.OnNotifyPropertyChanged(nameof (Maximum));
      }
    }

    [DefaultValue(Orientation.Horizontal)]
    public Orientation ElementOrientation
    {
      get
      {
        return this.elementsLayout.Orientation;
      }
      set
      {
        if (this.elementsLayout.Orientation == value)
          return;
        bool flag = value != Orientation.Horizontal;
        this.elementsLayout.Orientation = value;
        foreach (RadObject radObject in (RadItemCollection) this.Items)
        {
          int num = (int) radObject.SetValue(RatingVisualElement.IsVerticalProperty, (object) flag);
        }
        this.OnNotifyPropertyChanged(nameof (ElementOrientation));
        if (!this.AutoSize)
          return;
        this.InvalidateMeasure();
      }
    }

    [DefaultValue(RatingSelectionMode.Precise)]
    public RatingSelectionMode SelectionMode
    {
      get
      {
        return this.selectionMode;
      }
      set
      {
        if (this.selectionMode == value)
          return;
        this.selectionMode = value;
        this.OnNotifyPropertyChanged(nameof (SelectionMode));
      }
    }

    [DefaultValue(false)]
    public bool ReadOnly
    {
      get
      {
        return this.readOnly;
      }
      set
      {
        if (this.readOnly == value)
          return;
        this.readOnly = value;
        if (!this.readOnly)
          return;
        this.ResetValues();
        this.OnNotifyPropertyChanged(nameof (ReadOnly));
      }
    }

    public string Description
    {
      get
      {
        return this.description.Text;
      }
      set
      {
        if (!(this.description.Text != value))
          return;
        this.description.Text = value;
        this.OnNotifyPropertyChanged(nameof (Description));
      }
    }

    public RatingDirection Direction
    {
      get
      {
        return this.direction;
      }
      set
      {
        if (this.direction == value)
          return;
        this.direction = value;
        this.OnNotifyPropertyChanged(nameof (Direction));
      }
    }

    public double ToolTipPrecision
    {
      get
      {
        return this.toolTipPrecision;
      }
      set
      {
        if (this.toolTipPrecision == value)
          return;
        this.toolTipPrecision = value;
        this.OnNotifyPropertyChanged(nameof (ToolTipPrecision));
      }
    }

    public double PercentageRounding
    {
      get
      {
        return this.percentageRounding;
      }
      set
      {
        if (this.percentageRounding == value)
          return;
        this.percentageRounding = value;
        this.OnNotifyPropertyChanged(nameof (PercentageRounding));
      }
    }

    public Point ToolTipOffset
    {
      get
      {
        return this.toolTipOffset;
      }
      set
      {
        if (!(this.toolTipOffset != value))
          return;
        this.toolTipOffset = value;
        this.OnNotifyPropertyChanged(nameof (ToolTipOffset));
      }
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(1000)]
    public int ToolTipDuration
    {
      get
      {
        return this.toolTipDuration;
      }
      set
      {
        if (this.toolTipDuration == value)
          return;
        this.toolTipDuration = value;
        this.OnNotifyPropertyChanged(nameof (ToolTipDuration));
      }
    }

    public bool IsInRadGridView
    {
      get
      {
        return this.isInRadGridView;
      }
      set
      {
        if (this.isInRadGridView == value)
          return;
        this.isInRadGridView = value;
        foreach (RatingVisualItem ratingVisualItem in (RadItemCollection) this.items)
          ratingVisualItem.IsInRadGridView = value;
      }
    }

    [Browsable(true)]
    [System.ComponentModel.Description("Occurs when the value of the rating has been changed.")]
    [Category("Action")]
    public event EventHandler ValueChanged;

    [Category("Action")]
    [System.ComponentModel.Description("Occurs when the value of the rating is changing.")]
    public event ValueChangingEventHandler ValueChanging;

    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if ((PointF) e.Location == this.oldLocation)
      {
        base.OnMouseMove(e);
      }
      else
      {
        this.oldLocation = (PointF) e.Location;
        if (this.toolTip == null)
        {
          this.toolTip = this.ElementTree.ComponentTreeHandler.Behavior.ToolTip;
          this.toolTip.InitialDelay = 0;
        }
        if (!this.VisualElementsBounds().Contains(e.Location))
          this.ResetValues();
        int currentIndex = 0;
        if (this.RightToLeft && this.ElementOrientation == Orientation.Horizontal)
        {
          for (int index = this.items.Count - 1; index >= 0; --index)
          {
            if (new Rectangle(this.items[index].LocationToControl(), this.items[index].Size).Contains(e.Location) && !this.ReadOnly)
            {
              this.CurrentValue = this.UpdateCurrentValue(currentIndex, this.items[index] as RatingVisualElement, e);
              if (this.toolTipValue >= this.currentValue + this.ToolTipPrecision || this.toolTipValue <= this.currentValue - this.ToolTipPrecision)
              {
                this.toolTipValue = this.Maximum - this.currentValue >= this.PercentageRounding / 100.0 * (this.Maximum - this.Minimum) ? (this.CurrentValue - this.Minimum >= this.PercentageRounding / 100.0 * (this.Maximum - this.Minimum) ? this.currentValue : this.Minimum) : this.maximum;
                string text = string.Format(this.ToolTipFormatString, (object) this.toolTipValue);
                if (this.ElementTree.ComponentTreeHandler.Behavior.ShowItemToolTips)
                  this.toolTip.Show(text, (IWin32Window) this.ElementTree.Control, e.X + this.ToolTipOffset.X, e.Y + this.ToolTipOffset.Y, this.ToolTipDuration);
              }
              if (this.shouldPaintHover)
              {
                this.HoverValue = this.CurrentValue;
                this.PaintLayer(currentIndex, RatingElementType.Hover);
              }
            }
            ++currentIndex;
          }
        }
        else
        {
          foreach (RatingVisualElement element in (RadItemCollection) this.Items)
          {
            if (new Rectangle(element.LocationToControl(), element.Size).Contains(e.Location) && !this.ReadOnly)
            {
              this.CurrentValue = this.UpdateCurrentValue(currentIndex, element, e);
              if (this.toolTipValue >= this.currentValue + this.ToolTipPrecision || this.toolTipValue <= this.currentValue - this.ToolTipPrecision)
              {
                this.toolTipValue = this.Maximum - this.currentValue >= this.PercentageRounding / 100.0 * (this.Maximum - this.Minimum) ? (this.CurrentValue - this.Minimum >= this.PercentageRounding / 100.0 * (this.Maximum - this.Minimum) ? this.currentValue : this.Minimum) : this.maximum;
                string text = string.Format(this.ToolTipFormatString, (object) this.toolTipValue);
                if (this.ElementTree.ComponentTreeHandler.Behavior.ShowItemToolTips)
                  this.toolTip.Show(text, (IWin32Window) this.ElementTree.Control, e.X + this.ToolTipOffset.X, e.Y + this.ToolTipOffset.Y, this.ToolTipDuration);
              }
              if (this.shouldPaintHover)
              {
                this.HoverValue = this.CurrentValue;
                this.PaintLayer(currentIndex, RatingElementType.Hover);
              }
            }
            ++currentIndex;
          }
        }
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.ResetValues();
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      if (!this.VisualElementsBounds().Contains(((MouseEventArgs) e).Location) || this.ReadOnly)
        return;
      this.Value = new double?(this.SelectedValue);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.VisualElementsBounds().Contains(e.Location) || this.ReadOnly)
        return;
      this.SelectedValue = Math.Round(this.CurrentValue, 4);
      this.PaintLayer(this.GetElementIndex(new double?(this.SelectedValue)), RatingElementType.SelectedValue);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      foreach (RatingVisualElement ratingVisualElement in (RadItemCollection) this.Items)
      {
        ratingVisualElement.SelectedValueElement.ClipArea = ratingVisualElement.SelectedValueElement.EmptyArea;
        ratingVisualElement.SelectedValueElement.Invalidate();
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property.Name == "RightToLeft")
        this.OnNotifyPropertyChanged("RightToLeft");
      if (e.Property.Name == "Bounds")
        this.OnNotifyPropertyChanged("Bounds");
      base.OnPropertyChanged(e);
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName == "Bounds" || propertyName == "Minimum" || (propertyName == "Maximum" || propertyName == "Direction") || (propertyName == "RightToLeft" || propertyName == "Value"))
        this.PaintValueLevel();
      if (!(propertyName == "ElementOrientation"))
        return;
      if (this.ElementOrientation == Orientation.Horizontal)
      {
        if ((this.ElementTree.Control as RadControl).AutoSize)
          this.ElementsLayout.StretchHorizontally = true;
        this.ElementsLayout.Alignment = ContentAlignment.TopLeft;
        this.caption.TextAlignment = ContentAlignment.TopLeft;
        this.subCaption.TextAlignment = ContentAlignment.TopLeft;
        this.description.TextAlignment = ContentAlignment.TopLeft;
      }
      else
      {
        if ((this.ElementTree.Control as RadControl).AutoSize)
          this.ElementsLayout.StretchHorizontally = false;
        this.ElementsLayout.Alignment = ContentAlignment.MiddleCenter;
        this.caption.TextAlignment = ContentAlignment.MiddleCenter;
        this.subCaption.TextAlignment = ContentAlignment.MiddleCenter;
        this.description.TextAlignment = ContentAlignment.MiddleCenter;
      }
    }

    private System.Type[] GetItemsTypes()
    {
      return new System.Type[3]{ typeof (RatingStarVisualElement), typeof (RatingDiamondVisualElement), typeof (RatingHeartVisualElement) };
    }

    private void PaintLayer(int currentIndex, RatingElementType elementType)
    {
      int num = 0;
      if (this.RightToLeft && this.ElementOrientation == Orientation.Horizontal)
      {
        for (int index = this.items.Count - 1; index >= 0; --index)
        {
          RatingVisualElement element = this.items[index] as RatingVisualElement;
          if (num < currentIndex)
            this.SetLowerIndexElementsClipArea(elementType, element);
          else if (num == currentIndex)
            this.SetCurrentIndexElementClipArea(currentIndex, elementType, element);
          else
            this.SetHigherIndexElementsClipArea(elementType, element);
          ++num;
          element.Invalidate();
        }
      }
      else
      {
        foreach (RatingVisualElement element in (RadItemCollection) this.Items)
        {
          if (element.BoundingRectangle.X + element.BoundingRectangle.Width > this.ElementsLayout.BoundingRectangle.Width || this.ElementsLayout.BoundingRectangle.Width < element.MinSize.Width || this.ElementsLayout.BoundingRectangle.Height < element.BoundingRectangle.Height + element.BoundingRectangle.Y)
          {
            element.Visibility = ElementVisibility.Hidden;
          }
          else
          {
            element.Visibility = ElementVisibility.Visible;
            if (num < currentIndex)
              this.SetLowerIndexElementsClipArea(elementType, element);
            else if (num == currentIndex)
              this.SetCurrentIndexElementClipArea(currentIndex, elementType, element);
            else
              this.SetHigherIndexElementsClipArea(elementType, element);
            ++num;
            element.Invalidate();
          }
        }
      }
    }

    private void SetLowerIndexElementsClipArea(
      RatingElementType elementType,
      RatingVisualElement element)
    {
      switch (elementType)
      {
        case RatingElementType.Hover:
          element.HoverElement.ClipArea = this.GetElementClipArea((RatingBaseVisualElement) element.HoverElement, true);
          break;
        case RatingElementType.Value:
          element.ValueElement.ClipArea = this.GetElementClipArea((RatingBaseVisualElement) element.ValueElement, true);
          break;
        case RatingElementType.SelectedValue:
          element.SelectedValueElement.ClipArea = this.GetElementClipArea((RatingBaseVisualElement) element.SelectedValueElement, true);
          break;
        default:
          throw new InvalidEnumArgumentException();
      }
    }

    private void SetCurrentIndexElementClipArea(
      int currentIndex,
      RatingElementType elementType,
      RatingVisualElement element)
    {
      switch (elementType)
      {
        case RatingElementType.Hover:
          Point relativePoint1 = this.GetRelativePoint(currentIndex, (RatingBaseVisualElement) element.HoverElement, new double?(this.HoverValue));
          element.HoverElement.ClipArea = this.GetCurrentElementClipArea((RatingBaseVisualElement) element.HoverElement, relativePoint1);
          break;
        case RatingElementType.Value:
          Point relativePoint2 = this.GetRelativePoint(currentIndex, (RatingBaseVisualElement) element.ValueElement, this.Value);
          element.ValueElement.ClipArea = this.GetCurrentElementClipArea((RatingBaseVisualElement) element.ValueElement, relativePoint2);
          break;
        case RatingElementType.SelectedValue:
          Point relativePoint3 = this.GetRelativePoint(currentIndex, (RatingBaseVisualElement) element.SelectedValueElement, new double?(this.SelectedValue));
          element.SelectedValueElement.ClipArea = this.GetCurrentElementClipArea((RatingBaseVisualElement) element.SelectedValueElement, relativePoint3);
          break;
        default:
          throw new InvalidEnumArgumentException();
      }
    }

    private void SetHigherIndexElementsClipArea(
      RatingElementType elementType,
      RatingVisualElement element)
    {
      switch (elementType)
      {
        case RatingElementType.Hover:
          element.HoverElement.ClipArea = this.GetElementClipArea((RatingBaseVisualElement) element.HoverElement, false);
          break;
        case RatingElementType.Value:
          element.ValueElement.ClipArea = this.GetElementClipArea((RatingBaseVisualElement) element.ValueElement, false);
          break;
        case RatingElementType.SelectedValue:
          element.SelectedValueElement.ClipArea = this.GetElementClipArea((RatingBaseVisualElement) element.SelectedValueElement, false);
          break;
        default:
          throw new InvalidEnumArgumentException();
      }
    }

    private Rectangle GetElementClipArea(
      RatingBaseVisualElement element,
      bool elementIsBeforeCurrentElement)
    {
      if (!this.Value.HasValue && element is ValueRatingVisualElement)
        return element.EmptyArea;
      if (this.Direction == RatingDirection.Standard && this.ElementOrientation == Orientation.Horizontal || this.Direction == RatingDirection.Reversed && this.ElementOrientation == Orientation.Vertical)
      {
        if (elementIsBeforeCurrentElement)
          return element.FullArea;
        return element.EmptyArea;
      }
      if (elementIsBeforeCurrentElement)
        return element.EmptyArea;
      return element.FullArea;
    }

    private Rectangle GetCurrentElementClipArea(
      RatingBaseVisualElement element,
      Point point)
    {
      switch (this.SelectionMode)
      {
        case RatingSelectionMode.Precise:
          if (!this.Value.HasValue && element is ValueRatingVisualElement)
            return element.EmptyArea;
          return this.GetPreciseClipArea(element, point);
        case RatingSelectionMode.FullItem:
          double? nullable1 = this.Value;
          double minimum1 = this.Minimum;
          if (((nullable1.GetValueOrDefault() != minimum1 ? 0 : (nullable1.HasValue ? 1 : 0)) != 0 || !this.Value.HasValue) && element is ValueRatingVisualElement)
            return element.EmptyArea;
          return element.FullArea;
        case RatingSelectionMode.HalfItem:
          double? nullable2 = this.Value;
          double minimum2 = this.Minimum;
          if (((nullable2.GetValueOrDefault() != minimum2 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0 || !this.Value.HasValue) && element is ValueRatingVisualElement)
            return element.EmptyArea;
          return this.GetHalfItemClipArea(element, point);
        default:
          throw new InvalidEnumArgumentException();
      }
    }

    private Rectangle GetPreciseClipArea(RatingBaseVisualElement element, Point point)
    {
      int width = element.Size.Width;
      int height = element.Size.Height;
      if (this.ElementOrientation == Orientation.Horizontal)
      {
        switch (this.Direction)
        {
          case RatingDirection.Standard:
            return new Rectangle(0, 0, point.X, height);
          case RatingDirection.Reversed:
            return new Rectangle(width - point.X, 0, width, height);
          default:
            throw new InvalidEnumArgumentException();
        }
      }
      else
      {
        switch (this.Direction)
        {
          case RatingDirection.Standard:
            return new Rectangle(0, height - point.Y, width, height);
          case RatingDirection.Reversed:
            return new Rectangle(0, 0, width, point.Y);
          default:
            throw new InvalidEnumArgumentException();
        }
      }
    }

    private Rectangle GetHalfItemClipArea(RatingBaseVisualElement element, Point point)
    {
      int width = element.Size.Width;
      int height = element.Size.Height;
      if (this.ElementOrientation == Orientation.Horizontal)
      {
        if (point.X > width / 2)
          return element.FullArea;
        switch (this.Direction)
        {
          case RatingDirection.Standard:
            return new Rectangle(0, 0, width / 2, height);
          case RatingDirection.Reversed:
            return new Rectangle(width / 2, 0, width, height);
          default:
            throw new InvalidEnumArgumentException();
        }
      }
      else
      {
        if (point.Y > height / 2)
          return element.FullArea;
        switch (this.Direction)
        {
          case RatingDirection.Standard:
            return new Rectangle(0, height / 2, width, height);
          case RatingDirection.Reversed:
            return new Rectangle(0, 0, width, height / 2);
          default:
            throw new InvalidEnumArgumentException();
        }
      }
    }

    protected Rectangle VisualElementsBounds()
    {
      int x = int.MaxValue;
      int y = int.MaxValue;
      int num1 = 0;
      int num2 = 0;
      foreach (RatingVisualElement ratingVisualElement in (RadItemCollection) this.Items)
      {
        Point control = ratingVisualElement.LocationToControl();
        if (x > control.X)
          x = control.X;
        if (y > control.Y)
          y = control.Y;
        if (num1 < control.X + ratingVisualElement.Size.Width)
          num1 = control.X + ratingVisualElement.Size.Width;
        if (num2 < control.Y + ratingVisualElement.Size.Height)
          num2 = control.Y + ratingVisualElement.Size.Height;
      }
      return new Rectangle(x, y, num1 - x, num2 - y);
    }

    protected void ResetValues()
    {
      foreach (RatingVisualElement ratingVisualElement in (RadItemCollection) this.Items)
      {
        ratingVisualElement.HoverElement.ClipArea = ratingVisualElement.HoverElement.EmptyArea;
        ratingVisualElement.SelectedValueElement.ClipArea = ratingVisualElement.SelectedValueElement.EmptyArea;
        ratingVisualElement.Invalidate();
      }
      this.toolTipValue = this.Minimum;
      if (this.toolTip == null)
        return;
      this.toolTip.Hide((IWin32Window) this.ElementTree.Control);
    }

    protected virtual double UpdateCurrentValue(
      int currentIndex,
      RatingVisualElement element,
      MouseEventArgs e)
    {
      double num1 = (this.Maximum - this.Minimum) / (double) this.Items.Count;
      double num2 = 0.0;
      float num3 = (float) (e.X - element.LocationToControl().X);
      float num4 = (float) (e.Y - element.LocationToControl().Y);
      double num5;
      if (this.Direction == RatingDirection.Standard)
      {
        if (this.ElementOrientation == Orientation.Horizontal)
        {
          double num6 = num2 + num1 * (double) currentIndex;
          num5 = this.SelectionMode != RatingSelectionMode.FullItem ? (this.SelectionMode != RatingSelectionMode.HalfItem ? num6 + (double) num3 / (double) element.Size.Width * num1 : ((double) num3 > (double) (element.Size.Width / 2) ? num6 + num1 : num6 + num1 / 2.0)) : num6 + num1;
        }
        else
        {
          double num6 = num2 + (double) (this.Items.Count - (currentIndex + 1)) * num1;
          num5 = this.SelectionMode != RatingSelectionMode.FullItem ? (this.SelectionMode != RatingSelectionMode.HalfItem ? num6 + ((double) element.Size.Height - (double) num4) / (double) element.Size.Height * num1 : ((double) num4 <= (double) (element.Size.Height / 2) ? num6 + num1 : num6 + num1 / 2.0)) : num6 + num1;
        }
      }
      else if (this.ElementOrientation == Orientation.Horizontal)
      {
        double num6 = num2 + (double) (this.Items.Count - (currentIndex + 1)) * num1;
        num5 = this.SelectionMode != RatingSelectionMode.FullItem ? (this.SelectionMode != RatingSelectionMode.HalfItem ? num6 + ((double) element.Size.Width - (double) num3) / (double) element.Size.Width * num1 : ((double) num3 <= (double) (element.Size.Width / 2) ? num6 + num1 : num6 + num1 / 2.0)) : num6 + num1;
      }
      else
      {
        double num6 = num2 + num1 * (double) currentIndex;
        num5 = this.SelectionMode != RatingSelectionMode.FullItem ? (this.SelectionMode != RatingSelectionMode.HalfItem ? num6 + (double) num4 / (double) element.Size.Height * num1 : ((double) num4 > (double) (element.Size.Height / 2) ? num6 + num1 : num6 + num1 / 2.0)) : num6 + num1;
      }
      return num5 + this.Minimum;
    }

    protected virtual void PaintValueLevel()
    {
      if (this.Minimum == this.Maximum)
        return;
      this.PaintLayer(this.GetElementIndex(this.Value), RatingElementType.Value);
    }

    protected virtual int GetElementIndex(double? value)
    {
      double num1 = (this.Maximum - this.Minimum) / (double) this.Items.Count;
      int num2 = (int) Math.Ceiling(this.GetNormalizedValue(value) / num1 - 1.0);
      if (num2 < 0)
        num2 = 0;
      if (this.ElementOrientation == Orientation.Horizontal && this.Direction == RatingDirection.Reversed || this.ElementOrientation == Orientation.Vertical && this.Direction == RatingDirection.Standard)
        num2 = this.Items.Count - (num2 + 1);
      return num2;
    }

    protected virtual Point GetRelativePoint(
      int elementIndex,
      RatingBaseVisualElement currentVisualElement,
      double? value)
    {
      double num1 = (this.Maximum - this.Minimum) / (double) this.Items.Count;
      double normalizedValue = this.GetNormalizedValue(value);
      double num2 = this.ElementOrientation == Orientation.Horizontal && this.Direction == RatingDirection.Reversed || this.ElementOrientation == Orientation.Vertical && this.Direction == RatingDirection.Standard ? normalizedValue - num1 * (double) (this.Items.Count - (elementIndex + 1)) : normalizedValue - num1 * (double) elementIndex;
      return new Point((int) (num2 / num1 * (double) currentVisualElement.Size.Width), (int) (num2 / num1 * (double) currentVisualElement.Size.Height));
    }

    protected virtual double GetNormalizedValue(double? value)
    {
      if (value.HasValue)
        return value.Value - this.Minimum;
      return this.Minimum;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      float width = 0.0f;
      float height = 0.0f;
      foreach (RadElement child in this.Children)
      {
        child.Measure(availableSize);
        if ((double) child.DesiredSize.Width > (double) width)
          width = child.DesiredSize.Width;
        height += child.DesiredSize.Height;
      }
      return new SizeF(width, height);
    }
  }
}
