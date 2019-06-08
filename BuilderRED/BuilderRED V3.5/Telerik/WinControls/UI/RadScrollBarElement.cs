// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [DefaultEvent("Scroll")]
  [ComVisible(false)]
  [DefaultProperty("Value")]
  public class RadScrollBarElement : RadItem
  {
    private Size thumbDragDelta = Size.Empty;
    private double thumbLengthProportion = double.NaN;
    private bool protectPressedProperty = true;
    private int maximum = 100;
    private int smallChange = 1;
    private int largeChange = 10;
    private int scrollTimerDelay = 60;
    public static readonly RadProperty PressedProperty = RadProperty.Register("Pressed", typeof (bool), typeof (RadScrollBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static readonly RadProperty IsMouseOverScrollBarProperty = RadProperty.Register("IsMouseOverScrollBar", typeof (bool), typeof (RadScrollBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue));
    public static readonly RadProperty GradientAngleCorrectionProperty = RadProperty.Register(nameof (GradientAngleCorrection), typeof (float), typeof (RadScrollBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -90f, ElementPropertyOptions.None));
    public static readonly RadProperty ThumbLengthProportionProperty = RadProperty.Register(nameof (ThumbLengthProportion), typeof (double), typeof (RadScrollBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1.0, ElementPropertyOptions.None));
    public static readonly RadProperty MinThumbLengthProperty = RadProperty.Register(nameof (MinThumbLength), typeof (int), typeof (RadScrollBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 10, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty ScrollTypeProperty = RadProperty.RegisterAttached(nameof (ScrollType), typeof (ScrollType), typeof (RadScrollBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ScrollType.Horizontal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static int HorizontalScrollBarHeight = 17;
    public static int VerticalScrollBarWidth = 17;
    public const ScrollType DefaultScrollType = ScrollType.Horizontal;
    private const string ValueMoreThanZeroMessage = "Value of '{0}' is not valid for '{1}'. '{1}' must be greater than or equal to 0.";
    private const string InvalidValueMessage = "Value of '{0}' is not valid for 'Value'. 'Value' must be between 'Minimum' and 'Maximum'.";
    private const float DefaultAngleCorrection = -90f;
    private const int DefaultMaximum = 100;
    private const int DefaultMinimum = 0;
    private const int DefaultValue = 0;
    private const int DefaultSmallChange = 1;
    private const int DefaultLargeChange = 10;
    private const double DefaultThumbLengthProportion = -1.0;
    private Point lastMouseDownLocation;
    private Rectangle clientRect;
    private Size firstButtonSize;
    private Size secondButtonSize;
    private Size thumbSize;
    private FillPrimitive background;
    private FillPrimitive pressedForeground;
    private ScrollBarButton firstButton;
    private ScrollBarButton secondButton;
    private ScrollBarThumb thumb;
    private Timer scrollDelay;
    private Timer scrollTimer;
    private ScrollEventType? currentScroll;
    private BorderPrimitive borderElement;
    private bool supressScrollParameterChangedEvent;
    private int thumbLength;
    private int minimum;
    private int value;
    private bool clampValue;

    static RadScrollBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ScrollButtonStateManagerFactory(), typeof (RadScrollBarElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.scrollDelay = new Timer();
      this.scrollDelay.Interval = 400;
      this.scrollDelay.Tick += new EventHandler(this.OnScrollDelay);
      this.scrollTimer = new Timer();
      this.scrollTimer.Interval = this.scrollTimerDelay;
      this.scrollTimer.Tick += new EventHandler(this.OnScrollTimer);
    }

    protected override void DisposeManagedResources()
    {
      this.scrollDelay.Tick -= new EventHandler(this.OnScrollDelay);
      this.scrollTimer.Tick -= new EventHandler(this.OnScrollTimer);
      this.scrollDelay.Dispose();
      this.scrollTimer.Dispose();
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      this.background = new FillPrimitive();
      this.background.Class = "ScrollBarFill";
      this.background.ZIndex = 0;
      this.Children.Add((RadElement) this.background);
      this.AddBehavior((PropertyChangeBehavior) new GradientAngleBehavior((RadElement) this.background));
      this.pressedForeground = new FillPrimitive();
      this.pressedForeground.Class = "ScrollBarPressedFill";
      this.pressedForeground.ZIndex = 1;
      this.pressedForeground.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.pressedForeground);
      this.firstButton = new ScrollBarButton(this.GetFirstButtonDirection(this.ScrollType));
      this.firstButton.NotifyParentOnMouseInput = true;
      this.firstButton.ZIndex = 1;
      this.firstButton.Class = "ScrollBarFirstButton";
      this.firstButton.ThemeRole = "ScrollBarFirstButton";
      this.Children.Add((RadElement) this.firstButton);
      this.AddBehavior((PropertyChangeBehavior) new GradientAngleBehavior((RadElement) this.firstButton.ButtonBorder, true));
      this.AddBehavior((PropertyChangeBehavior) new GradientAngleBehavior((RadElement) this.firstButton.ButtonFill, true));
      this.secondButton = new ScrollBarButton(this.GetSecondButtonDirection(this.ScrollType));
      this.secondButton.NotifyParentOnMouseInput = true;
      this.secondButton.ZIndex = 1;
      this.secondButton.Class = "ScrollBarSecondButton";
      this.secondButton.ThemeRole = "ScrollBarSecondButton";
      this.Children.Add((RadElement) this.secondButton);
      this.AddBehavior((PropertyChangeBehavior) new GradientAngleBehavior((RadElement) this.secondButton.ButtonBorder, true));
      this.AddBehavior((PropertyChangeBehavior) new GradientAngleBehavior((RadElement) this.secondButton.ButtonFill, true));
      this.thumb = new ScrollBarThumb();
      this.thumb.NotifyParentOnMouseInput = true;
      this.thumb.ZIndex = 1;
      this.thumb.Class = "ScrollBarThumb";
      this.Children.Add((RadElement) this.thumb);
      this.AddBehavior((PropertyChangeBehavior) new GradientAngleBehavior((RadElement) this.thumb.ThumbFill));
      this.AddBehavior((PropertyChangeBehavior) new GradientAngleBehavior((RadElement) this.thumb.ThumbBorder));
      this.borderElement = new BorderPrimitive();
      this.borderElement.Class = "ScrollBarBorder";
      this.borderElement.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.borderElement);
      this.AddBehavior((PropertyChangeBehavior) new GradientAngleBehavior((RadElement) this.borderElement));
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.RecalculateAngleCorrection();
    }

    [Category("Action")]
    [Description("Occurs when the user moves the scroll thumb.")]
    public event ScrollEventHandler Scroll;

    [Description("Occurs when the value (i.e. the scroll thumb position) changes.")]
    [Category("Action")]
    public event EventHandler ValueChanged;

    [Description("Occurs when a scrolling parameter value changes (Maximum, Minimum, LargeChange and SmallChange).")]
    [Category("Behavior")]
    public event EventHandler ScrollParameterChanged;

    public bool ClampValue
    {
      get
      {
        return this.clampValue;
      }
      set
      {
        this.clampValue = value;
      }
    }

    public FillPrimitive FillElement
    {
      get
      {
        return this.background;
      }
    }

    public BorderPrimitive BorderElement
    {
      get
      {
        return this.borderElement;
      }
    }

    public ScrollBarButton FirstButton
    {
      get
      {
        return this.firstButton;
      }
    }

    public ScrollBarButton SecondButton
    {
      get
      {
        return this.secondButton;
      }
    }

    [Category("Behavior")]
    [Description("Proportional area occupied by the thumb in the scrolling area of the scroll bar (for values between 0.0 and 0.1; < 0.0 => auto)")]
    [RadPropertyDefaultValue("ThumbLengthProportion", typeof (RadScrollBarElement))]
    public double ThumbLengthProportion
    {
      get
      {
        if (double.IsNaN(this.thumbLengthProportion))
          this.thumbLengthProportion = (double) this.GetValue(RadScrollBarElement.ThumbLengthProportionProperty);
        return this.thumbLengthProportion;
      }
      set
      {
        int num = (int) this.SetValue(RadScrollBarElement.ThumbLengthProportionProperty, (object) value);
      }
    }

    [Description("Minimum length of the thumb")]
    [RadPropertyDefaultValue("MinThumbLength", typeof (RadScrollBarElement))]
    [Category("Behavior")]
    public int MinThumbLength
    {
      get
      {
        int num = (int) this.GetValue(RadScrollBarElement.MinThumbLengthProperty);
        if (this.ScrollType == ScrollType.Horizontal)
          return (int) ((double) num * (double) this.DpiScaleFactor.Width);
        return (int) ((double) num * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        if (value == (int) this.GetValue(RadScrollBarElement.MinThumbLengthProperty))
          return;
        int num = (int) this.SetValue(RadScrollBarElement.MinThumbLengthProperty, (object) value);
        this.SetupThumb();
      }
    }

    [Category("Behavior")]
    [System.ComponentModel.DefaultValue(0)]
    [Description("Thumb length in pixels")]
    public int ThumbLength
    {
      get
      {
        if (this.thumbLength == 0)
          this.thumbLength = this.CalcThumbLength(this.ThumbLengthProportion);
        return this.thumbLength;
      }
    }

    [RadPropertyDefaultValue("GradientAngleCorrection", typeof (RadScrollBarElement))]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public float GradientAngleCorrection
    {
      get
      {
        return (float) this.GetValue(RadScrollBarElement.GradientAngleCorrectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadScrollBarElement.GradientAngleCorrectionProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("The upper limit value of the scrollable range.")]
    [System.ComponentModel.DefaultValue(100)]
    public int Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        if (this.maximum == value)
          return;
        if (this.minimum > value)
          this.minimum = value;
        if (value < this.value)
          this.Value = value;
        this.maximum = value;
        this.SetupThumb();
        this.OnScrollParameterChanged();
        this.OnNotifyPropertyChanged(nameof (Maximum));
      }
    }

    [Category("Behavior")]
    [System.ComponentModel.DefaultValue(0)]
    [Description("The lower limit value of the scrollable range.")]
    public int Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        if (this.minimum == value)
          return;
        if (this.maximum < value)
          this.maximum = value;
        if (value > this.value)
          this.value = value;
        this.minimum = value;
        this.SetupThumb();
        this.OnScrollParameterChanged();
      }
    }

    [System.ComponentModel.DefaultValue(0)]
    [Category("Behavior")]
    [Description("The value that the scroll thumb position represents.")]
    public int Value
    {
      get
      {
        return this.value;
      }
      set
      {
        if (this.value == value)
          return;
        if (value < this.Minimum || value > this.Maximum)
        {
          if (!this.clampValue)
            throw new ArgumentOutOfRangeException(nameof (Value), string.Format("Value of '{0}' is not valid for 'Value'. 'Value' must be between 'Minimum' and 'Maximum'.", (object) value));
          value = Math.Max(Math.Min(value, this.Maximum), this.Minimum);
        }
        int oldValue = this.value;
        this.value = value;
        this.SetupThumb(value, true);
        this.OnValueChanged(oldValue, this.value);
      }
    }

    [System.ComponentModel.DefaultValue(1)]
    [Category("Behavior")]
    [Description("The amount by which the scroll thumb position changes when a user clicks arrow, presses arrow keys or calls some of LineXXX() functions.")]
    public int SmallChange
    {
      get
      {
        return Math.Min(this.smallChange, this.LargeChange);
      }
      set
      {
        if (this.smallChange == value)
          return;
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (SmallChange), string.Format("Value of '{0}' is not valid for '{1}'. '{1}' must be greater than or equal to 0.", (object) value, (object) nameof (SmallChange)));
        this.smallChange = value;
        this.OnScrollParameterChanged();
      }
    }

    [System.ComponentModel.DefaultValue(10)]
    [Category("Behavior")]
    [Description("The amount by which the scroll thumb position changes when a user clicks the scroll bar, presses PageUp/PageDown keys or calls some of PageXXX() functions.")]
    public int LargeChange
    {
      get
      {
        return Math.Min(this.largeChange, this.Maximum - this.Minimum + 1);
      }
      set
      {
        if (this.largeChange == value)
          return;
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof (LargeChange), string.Format("Value of '{0}' is not valid for '{1}'. '{1}' must be greater than or equal to 0.", (object) value, (object) nameof (LargeChange)));
        this.largeChange = value;
        this.SetupThumb();
        this.OnScrollParameterChanged();
      }
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("ScrollType", typeof (RadScrollBarElement))]
    [Description("Specifies whether the scroll bar should be horizontal or vertical.")]
    public ScrollType ScrollType
    {
      get
      {
        return (ScrollType) this.GetValue(RadScrollBarElement.ScrollTypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadScrollBarElement.ScrollTypeProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the thumb element of this scrollbar")]
    [Browsable(false)]
    public ScrollBarThumb ThumbElement
    {
      get
      {
        return this.thumb;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [System.ComponentModel.DefaultValue(60)]
    [Description("Gets or sets the scroll timer delay")]
    [Browsable(false)]
    public int ScrollTimerDelay
    {
      get
      {
        return this.scrollTimerDelay;
      }
      set
      {
        if (value == this.scrollTimerDelay)
          return;
        this.scrollTimerDelay = value;
        if (this.scrollTimer == null)
          return;
        this.scrollTimer.Interval = value;
      }
    }

    public ScrollBarParameters GetParameters()
    {
      return new ScrollBarParameters(this.Minimum, this.Maximum, this.SmallChange, this.LargeChange);
    }

    public void SetParameters(ScrollBarParameters parameters)
    {
      this.supressScrollParameterChangedEvent = true;
      this.Minimum = parameters.Minimum;
      this.Maximum = parameters.Maximum;
      this.SmallChange = parameters.SmallChange;
      this.LargeChange = parameters.LargeChange;
      this.supressScrollParameterChangedEvent = false;
      this.OnScrollParameterChanged();
    }

    public void PerformSmallDecrement(int numSteps)
    {
      this.PerformScrollWith(numSteps, ScrollEventType.SmallDecrement);
    }

    public void PerformSmallIncrement(int numSteps)
    {
      this.PerformScrollWith(numSteps, ScrollEventType.SmallIncrement);
    }

    public void PerformLargeDecrement(int numSteps)
    {
      this.PerformScrollWith(numSteps, ScrollEventType.LargeDecrement);
    }

    public void PerformLargeIncrement(int numSteps)
    {
      this.PerformScrollWith(numSteps, ScrollEventType.LargeIncrement);
    }

    public void PerformFirst()
    {
      this.PerformScrollToValue(this.Minimum, ScrollEventType.First);
    }

    public void PerformLast()
    {
      this.PerformScrollToValue(this.Maximum, ScrollEventType.Last);
    }

    public void PerformScrollTo(Point position)
    {
      if (!this.Enabled)
        return;
      this.FireThumbTrackMessage();
      Point thumbLocation = this.PointFromScreen(position);
      this.SetThumbPosition(thumbLocation, true);
      this.SetThumbPosition(thumbLocation, false);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.lastMouseDownLocation = e.Location;
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.currentScroll = this.GetScrollType(e.Location);
      if (!this.currentScroll.HasValue)
        return;
      this.Capture = true;
      ScrollEventType? currentScroll1 = this.currentScroll;
      if ((currentScroll1.GetValueOrDefault() != ScrollEventType.SmallDecrement ? 0 : (currentScroll1.HasValue ? 1 : 0)) != 0)
      {
        int num1 = (int) this.firstButton.SetValue(RadButtonItem.IsPressedProperty, (object) true);
      }
      else
      {
        ScrollEventType? currentScroll2 = this.currentScroll;
        if ((currentScroll2.GetValueOrDefault() != ScrollEventType.SmallIncrement ? 0 : (currentScroll2.HasValue ? 1 : 0)) != 0)
        {
          int num2 = (int) this.secondButton.SetValue(RadButtonItem.IsPressedProperty, (object) true);
        }
      }
      this.OnScrollTimer((object) this, new EventArgs());
      this.scrollDelay.Start();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.Capture || !this.scrollTimer.Enabled)
        return;
      this.scrollTimer.Stop();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
        return;
      this.scrollTimer.Stop();
      this.scrollDelay.Stop();
      this.Capture = false;
      ScrollEventType? currentScroll1 = this.currentScroll;
      if ((currentScroll1.GetValueOrDefault() != ScrollEventType.SmallDecrement ? 0 : (currentScroll1.HasValue ? 1 : 0)) != 0)
      {
        int num1 = (int) this.firstButton.SetValue(RadButtonItem.IsPressedProperty, (object) false);
      }
      else
      {
        ScrollEventType? currentScroll2 = this.currentScroll;
        if ((currentScroll2.GetValueOrDefault() != ScrollEventType.SmallIncrement ? 0 : (currentScroll2.HasValue ? 1 : 0)) != 0)
        {
          int num2 = (int) this.secondButton.SetValue(RadButtonItem.IsPressedProperty, (object) false);
        }
      }
      this.EndScroll(this.Value);
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      Point thumbPos = this.ValueToThumbPos(this.value);
      if (this.ScrollType == ScrollType.Vertical)
        thumbPos.Y += args.Offset.Height;
      else
        thumbPos.X += args.Offset.Width;
      int newValue = this.ThumbPosToValue(thumbPos);
      if (newValue > this.Maximum - this.LargeChange + 1)
        newValue = this.Maximum - this.LargeChange + 1;
      if (newValue < this.Minimum)
        newValue = this.Minimum;
      if (this.Value != newValue)
      {
        ScrollEventArgs args1 = new ScrollEventArgs(ScrollEventType.ThumbPosition, this.value, newValue);
        this.Value = newValue;
        this.OnScroll(args1);
      }
      args.Handled = true;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadScrollBarElement.PressedProperty && this.protectPressedProperty)
        throw new InvalidOperationException("The property \"PressedProperty\" cannot be set directly");
      if (e.Property == RadScrollBarElement.ScrollTypeProperty)
      {
        ScrollType newValue = (ScrollType) e.NewValue;
        if (this.firstButton != null)
          this.firstButton.Direction = this.GetFirstButtonDirection(newValue);
        if (this.secondButton != null)
          this.secondButton.Direction = this.GetSecondButtonDirection(newValue);
        this.RecalculateAngleCorrection();
      }
      else if (e.Property == RadScrollBarElement.GradientAngleCorrectionProperty)
        this.RecalculateAngleCorrection();
      else if (e.Property == RadElement.IsMouseOverElementProperty)
      {
        int num1 = (int) this.SetValue(RadScrollBarElement.IsMouseOverScrollBarProperty, (object) (bool) e.NewValue);
      }
      else if (e.Property == RadScrollBarElement.ThumbLengthProportionProperty)
      {
        this.thumbLengthProportion = double.NaN;
        this.SetupThumb();
      }
      else
      {
        if (e.Property != RadElement.VisibilityProperty || (ElementVisibility) e.NewValue == ElementVisibility.Visible)
          return;
        this.scrollTimer.Stop();
        this.scrollDelay.Stop();
        if (!this.Capture)
          return;
        this.Capture = false;
        ScrollEventType? currentScroll1 = this.currentScroll;
        if ((currentScroll1.GetValueOrDefault() != ScrollEventType.SmallDecrement ? 0 : (currentScroll1.HasValue ? 1 : 0)) != 0)
        {
          int num2 = (int) this.firstButton.SetValue(RadButtonItem.IsPressedProperty, (object) false);
        }
        else
        {
          ScrollEventType? currentScroll2 = this.currentScroll;
          if ((currentScroll2.GetValueOrDefault() != ScrollEventType.SmallIncrement ? 0 : (currentScroll2.HasValue ? 1 : 0)) != 0)
          {
            int num3 = (int) this.secondButton.SetValue(RadButtonItem.IsPressedProperty, (object) false);
          }
        }
        this.EndScroll(this.Value);
      }
    }

    protected virtual void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, new EventArgs());
    }

    protected virtual void OnValueChanged(int oldValue, int newValue)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, (EventArgs) new ScrollEventArgs(ScrollEventType.ThumbPosition, oldValue, newValue));
    }

    protected virtual void OnScroll(ScrollEventArgs args)
    {
      if (this.Scroll == null)
        return;
      this.Scroll((object) this, args);
    }

    protected virtual void OnScrollParameterChanged()
    {
      if (this.supressScrollParameterChangedEvent || this.ScrollParameterChanged == null)
        return;
      this.ScrollParameterChanged((object) this, new EventArgs());
    }

    private void OnScrollDelay(object sender, EventArgs e)
    {
      this.scrollDelay.Stop();
      this.OnScrollTimer(sender, e);
      this.scrollTimer.Start();
    }

    private void OnScrollTimer(object sender, EventArgs e)
    {
      if (!this.Enabled || !this.currentScroll.HasValue)
        return;
      ScrollEventType? scrollType = this.GetScrollType(this.lastMouseDownLocation);
      if (!scrollType.HasValue)
        return;
      ScrollEventType? nullable = scrollType;
      ScrollEventType? currentScroll = this.currentScroll;
      if ((nullable.GetValueOrDefault() != currentScroll.GetValueOrDefault() ? 0 : (nullable.HasValue == currentScroll.HasValue ? 1 : 0)) == 0)
        return;
      ScrollEventType scrollEventType = scrollType.Value;
      this.ScrollWith(this.GetStepFromScrollEventType(scrollEventType), scrollEventType);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        SizeF availableSize1 = availableSize;
        if (child is ScrollBarButton)
        {
          if (this.ScrollType == ScrollType.Horizontal)
            availableSize1.Width /= 2f;
          else
            availableSize1.Height /= 2f;
        }
        child.Measure(availableSize1);
      }
      return SizeF.Add(SizeF.Add(this.AccumulateDesiredSize(this.AccumulateDesiredSize(this.AccumulateDesiredSize(empty, (RadElement) this.firstButton), (RadElement) this.thumb), (RadElement) this.secondButton), (SizeF) this.Padding.Size), (SizeF) this.BorderThickness.Size);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (!this.IsInValidState(true))
        return finalSize;
      RectangleF finalRect = new RectangleF(PointF.Empty, finalSize);
      this.borderElement.Arrange(finalRect);
      if (this.background.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
        this.background.Arrange(finalRect);
      finalRect.Location = PointF.Add(finalRect.Location, new SizeF((float) this.BorderThickness.Left, (float) this.BorderThickness.Top));
      finalRect.Size = SizeF.Subtract(finalRect.Size, (SizeF) this.BorderThickness.Size);
      if (this.background.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
        this.background.Arrange(finalRect);
      finalRect.Location = PointF.Add(finalRect.Location, new SizeF((float) this.Padding.Left, (float) this.Padding.Top));
      finalRect.Size = SizeF.Subtract(finalRect.Size, (SizeF) this.Padding.Size);
      if (this.background.FitToSizeMode == RadFitToSizeMode.FitToParentContent)
        this.background.Arrange(finalRect);
      finalRect.Location = PointF.Subtract(finalRect.Location, new SizeF((float) this.BorderThickness.Left, (float) this.BorderThickness.Top));
      finalRect.Size = SizeF.Add(finalRect.Size, (SizeF) this.BorderThickness.Size);
      this.clientRect = Rectangle.Round(finalRect);
      if (this.ScrollType == ScrollType.Horizontal)
      {
        float height = finalRect.Height;
        float width = height + (float) Math.Max(this.firstButton.Padding.Horizontal, this.firstButton.Padding.Vertical);
        this.firstButtonSize = Size.Round(new SizeF(width, height));
        this.secondButtonSize = Size.Round(new SizeF(width, height));
        this.thumbSize = Size.Round(new SizeF((float) this.ThumbLength, finalRect.Height));
      }
      else
      {
        float width = finalRect.Width;
        float height = width + (float) Math.Max(this.firstButton.Padding.Horizontal, this.firstButton.Padding.Vertical);
        this.firstButtonSize = Size.Round(new SizeF(width, height));
        this.secondButtonSize = Size.Round(new SizeF(Math.Max(finalRect.Width, this.secondButton.DesiredSize.Height), height));
        this.thumbSize = Size.Round(new SizeF(finalRect.Width, (float) this.ThumbLength));
      }
      PointF pointF = PointF.Add((PointF) this.ValueToThumbPos(this.value), this.thumbDragDelta);
      this.thumbLength = 0;
      if (this.ScrollType == ScrollType.Horizontal)
      {
        this.firstButton.Arrange(new RectangleF(finalRect.X, finalRect.Y, (float) this.firstButtonSize.Width, (float) this.firstButtonSize.Height));
        this.secondButton.Arrange(new RectangleF(finalRect.Right - (float) this.secondButtonSize.Width, finalRect.Y, (float) this.secondButtonSize.Width, (float) this.secondButtonSize.Height));
        this.thumb.Arrange(new RectangleF(pointF.X, pointF.Y, (float) this.thumbSize.Width, (float) this.thumbSize.Height));
        this.pressedForeground.Arrange(finalRect);
      }
      else
      {
        this.firstButton.Arrange(new RectangleF(finalRect.X, finalRect.Y, (float) this.firstButtonSize.Width, (float) this.firstButtonSize.Height));
        this.secondButton.Arrange(new RectangleF(finalRect.X, finalRect.Bottom - (float) this.secondButtonSize.Height, (float) this.secondButtonSize.Width, (float) this.secondButtonSize.Height));
        this.thumb.Arrange(new RectangleF(pointF.X, pointF.Y, (float) this.thumbSize.Width, (float) this.thumbSize.Height));
        this.pressedForeground.Arrange(finalRect);
      }
      return finalSize;
    }

    private SizeF AccumulateDesiredSize(SizeF prevDesiredSize, RadElement element)
    {
      SizeF desiredSize = element.DesiredSize;
      if (this.ScrollType == ScrollType.Horizontal)
      {
        prevDesiredSize.Width += desiredSize.Width;
        if ((double) prevDesiredSize.Height < (double) desiredSize.Height)
          prevDesiredSize.Height = desiredSize.Height;
      }
      else
      {
        if ((double) prevDesiredSize.Width < (double) desiredSize.Width)
          prevDesiredSize.Width = desiredSize.Width;
        prevDesiredSize.Height += desiredSize.Height;
      }
      return prevDesiredSize;
    }

    private int CalcThumbLength(double thumbProportion)
    {
      int scrollingPixels = this.GetScrollingPixels(false);
      int val1;
      if (thumbProportion < 0.0)
      {
        val1 = 0;
        int num = this.Maximum - this.Minimum + 1;
        if (num > 1)
          val1 = (int) Math.Round((double) this.LargeChange * (double) scrollingPixels / (double) num);
        else if (num == 1)
          val1 = scrollingPixels;
      }
      else
        val1 = thumbProportion < 1.0 ? (int) Math.Round(thumbProportion * (double) scrollingPixels) : scrollingPixels;
      return Math.Min(Math.Max(val1, this.MinThumbLength), scrollingPixels);
    }

    internal void FireThumbTrackMessage()
    {
      int num = this.Value;
      this.CallOnScroll(ScrollEventType.ThumbTrack, num, num);
    }

    internal void SetThumbPosition(Point thumbLocation, bool dragging)
    {
      int oldValue1 = this.Value;
      int num = this.ThumbPosToValue(thumbLocation);
      int scrollMax = this.GetScrollMax();
      if (num > scrollMax)
        num = scrollMax;
      else if (num < this.Minimum)
        num = this.Minimum;
      if (dragging)
      {
        if (num == oldValue1)
          return;
        this.CallOnScroll(ScrollEventType.ThumbTrack, oldValue1, num);
        int oldValue2 = this.value;
        this.value = num;
        this.OnValueChanged(oldValue2, num);
        this.ArrangeOverride((SizeF) this.Size);
      }
      else
      {
        if (num != oldValue1)
        {
          this.CallOnScroll(ScrollEventType.ThumbPosition, oldValue1, num);
          this.Value = num;
        }
        this.CallOnScroll(ScrollEventType.ThumbPosition, num, num);
        this.EndScroll(num);
      }
    }

    private void PerformScrollWith(int numSteps, ScrollEventType scrollType)
    {
      if (!this.Enabled)
        return;
      this.ScrollWith(numSteps * this.GetStepFromScrollEventType(scrollType), scrollType);
      this.EndScroll(this.Value);
    }

    private void PerformScrollToValue(int newValue, ScrollEventType scrollType)
    {
      if (!this.Enabled)
        return;
      this.ScrollTo(newValue, scrollType);
      this.EndScroll(this.Value);
    }

    private void ScrollWith(int step, ScrollEventType scrollType)
    {
      int oldValue = this.Value;
      int newValue = oldValue + step;
      if (step > 0)
      {
        int scrollMax = this.GetScrollMax();
        if (newValue > scrollMax)
          newValue = scrollMax;
      }
      else if (newValue > this.Maximum)
        newValue = this.Maximum;
      if (newValue < this.Minimum)
        newValue = this.Minimum;
      this.CallOnScroll(scrollType, oldValue, newValue);
      this.Value = newValue;
    }

    private void ScrollTo(int newValue, ScrollEventType scrollType)
    {
      int oldValue = this.Value;
      int scrollMax = this.GetScrollMax();
      if (newValue > scrollMax)
        newValue = scrollMax;
      if (newValue < this.Minimum)
        newValue = this.Minimum;
      this.CallOnScroll(scrollType, oldValue, newValue);
      this.Value = newValue;
    }

    private void EndScroll(int endValue)
    {
      this.CallOnScroll(ScrollEventType.EndScroll, endValue, endValue);
      this.currentScroll = new ScrollEventType?();
    }

    private ScrollButtonDirection GetFirstButtonDirection(ScrollType scrType)
    {
      return scrType != ScrollType.Horizontal ? ScrollButtonDirection.Up : ScrollButtonDirection.Left;
    }

    private ScrollButtonDirection GetSecondButtonDirection(ScrollType scrType)
    {
      return scrType != ScrollType.Horizontal ? ScrollButtonDirection.Down : ScrollButtonDirection.Right;
    }

    private bool IsHorizontalRTL()
    {
      if (this.ScrollType == ScrollType.Horizontal)
        return this.RightToLeft;
      return false;
    }

    private int GetScrollMax()
    {
      if (this.LargeChange > 0)
        return this.Maximum - this.LargeChange + 1;
      return this.Maximum;
    }

    private int GetDeltaValue()
    {
      int num = this.GetScrollMax() - this.Minimum;
      if (num < 0)
        throw new ArithmeticException("ScrollBar: ScrollMax - minimum = " + num.ToString());
      return num;
    }

    private int GetScrollingPixels(bool excludeThumb)
    {
      int num;
      if (this.ScrollType == ScrollType.Horizontal)
      {
        num = this.clientRect.Width - this.firstButtonSize.Width - this.secondButtonSize.Width;
        if (excludeThumb)
          num -= this.thumbSize.Width;
      }
      else
      {
        num = this.clientRect.Height - this.firstButtonSize.Height - this.secondButtonSize.Height;
        if (excludeThumb)
          num -= this.thumbSize.Height;
      }
      return num;
    }

    private int ValueToScrollPixel(int value)
    {
      int num = 0;
      int scrollingPixels = this.GetScrollingPixels(true);
      if (scrollingPixels > 0)
      {
        if (value < this.Minimum)
          num = 0;
        else if (value < this.GetScrollMax())
        {
          int deltaValue = this.GetDeltaValue();
          num = deltaValue == 0 ? 0 : (int) Math.Round((double) (value - this.Minimum) * (double) scrollingPixels / (double) deltaValue);
        }
        else
          num = scrollingPixels;
        if (this.IsHorizontalRTL())
          num = scrollingPixels - num;
      }
      return num;
    }

    private int ScrollPixelToValue(int scrollPos)
    {
      int scrollingPixels = this.GetScrollingPixels(true);
      if (scrollingPixels <= 0)
        return this.Minimum;
      int deltaValue = this.GetDeltaValue();
      if (deltaValue == 0)
        return this.Minimum;
      if (this.IsHorizontalRTL())
        scrollPos = scrollingPixels - scrollPos;
      return (int) Math.Round((double) scrollPos * (double) deltaValue / (double) scrollingPixels) + this.Minimum;
    }

    private Point ValueToThumbPos(int value)
    {
      Point empty = Point.Empty;
      int scrollPixel = this.ValueToScrollPixel(value);
      if (this.ScrollType == ScrollType.Horizontal)
      {
        empty.X = this.firstButtonSize.Width + scrollPixel;
        empty.Y = 0;
      }
      else
      {
        empty.X = 0;
        empty.Y = this.firstButtonSize.Height + scrollPixel;
      }
      empty.X += this.clientRect.X;
      empty.Y += this.clientRect.Y;
      return empty;
    }

    private int ThumbPosToValue(Point thumbPos)
    {
      return this.ScrollPixelToValue(this.ScrollType != ScrollType.Horizontal ? thumbPos.Y - this.clientRect.Y - this.firstButtonSize.Height : thumbPos.X - this.clientRect.X - this.firstButtonSize.Width);
    }

    private ScrollEventType? GetScrollType(Point mouseLocation)
    {
      Point point = mouseLocation;
      ScrollEventType? nullable = new ScrollEventType?();
      if (this.thumb.ControlBoundingRectangle.Contains(mouseLocation))
        return new ScrollEventType?();
      if (this.firstButton.ControlBoundingRectangle.Contains(mouseLocation))
        return new ScrollEventType?(this.IsHorizontalRTL() ? ScrollEventType.SmallIncrement : ScrollEventType.SmallDecrement);
      if (this.secondButton.ControlBoundingRectangle.Contains(mouseLocation))
        return new ScrollEventType?(this.IsHorizontalRTL() ? ScrollEventType.SmallDecrement : ScrollEventType.SmallIncrement);
      Rectangle boundingRectangle = this.thumb.ControlBoundingRectangle;
      if (this.ScrollType == ScrollType.Horizontal)
      {
        if (point.X < boundingRectangle.Left)
          nullable = new ScrollEventType?(this.RightToLeft ? ScrollEventType.LargeIncrement : ScrollEventType.LargeDecrement);
        else if (point.X > boundingRectangle.Right)
          nullable = new ScrollEventType?(this.RightToLeft ? ScrollEventType.LargeDecrement : ScrollEventType.LargeIncrement);
      }
      else if (point.Y < boundingRectangle.Top)
        nullable = new ScrollEventType?(ScrollEventType.LargeDecrement);
      else if (point.Y > boundingRectangle.Bottom)
        nullable = new ScrollEventType?(ScrollEventType.LargeIncrement);
      return nullable;
    }

    private int GetStepFromScrollEventType(ScrollEventType scrollEventType)
    {
      switch (scrollEventType)
      {
        case ScrollEventType.SmallDecrement:
          return -this.SmallChange;
        case ScrollEventType.SmallIncrement:
          return this.SmallChange;
        case ScrollEventType.LargeDecrement:
          return -this.LargeChange;
        case ScrollEventType.LargeIncrement:
          return this.LargeChange;
        default:
          return 0;
      }
    }

    private void CallOnScroll(ScrollEventType scrollType, int oldValue, int newValue)
    {
      ScrollOrientation scroll = this.ScrollType == ScrollType.Vertical ? ScrollOrientation.VerticalScroll : ScrollOrientation.HorizontalScroll;
      this.OnScroll(new ScrollEventArgs(scrollType, oldValue, newValue, scroll));
    }

    private void RecalculateAngleCorrection()
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      foreach (PropertyChangeBehavior behavior in (List<PropertyChangeBehavior>) this.GetBehaviors())
        (behavior as GradientAngleBehavior)?.RecalculateOnGradientAngleCorrectionChanged();
    }

    private void DumpLayoutInfo(string str, RadElement affectedElement)
    {
      if (this.ScrollType == ScrollType.Horizontal)
        Console.Write("Horiz ", (object) affectedElement);
      else
        Console.Write("Vert ", (object) affectedElement);
      Console.Write(str);
      if (object.ReferenceEquals((object) this, (object) affectedElement))
        Console.Write(" [THIS]");
      else if (object.ReferenceEquals((object) this.Parent, (object) affectedElement))
        Console.Write(" [PARENT]");
      else if (object.ReferenceEquals((object) this.firstButton, (object) affectedElement))
        Console.Write(" [FIRST]");
      else if (object.ReferenceEquals((object) this.secondButton, (object) affectedElement))
        Console.Write(" [SECOND]");
      else if (object.ReferenceEquals((object) this.thumb, (object) affectedElement))
        Console.Write(" [THUMB]");
      else if (object.ReferenceEquals((object) this.background, (object) affectedElement))
        Console.Write(" [BACKGROUND]");
      else
        Console.Write(" [UNKNOWN]");
      Console.WriteLine(" ScrollBar = {0}; first = {1}; second = {2}; thumb = {3}", new object[4]
      {
        (object) this.Size,
        (object) this.firstButton.Size,
        (object) this.secondButton.Size,
        (object) this.thumb.Size
      });
    }

    private void SetPressed(bool Pressed)
    {
      this.protectPressedProperty = false;
      int num = (int) this.SetValue(RadScrollBarElement.PressedProperty, (object) Pressed);
      this.protectPressedProperty = true;
    }

    private void SetupThumb()
    {
      this.SetupThumb(this.Value, false);
    }

    private void SetupThumb(int newValue, bool thumbLocationOnly)
    {
      this.thumbLength = 0;
      this.ArrangeOverride(new SizeF((SizeF) this.Size));
    }

    private void SetupButtons()
    {
      int num = 20;
      Size size = Size.Subtract(Size.Subtract(this.Size, this.Padding.Size), this.BorderThickness.Size);
      if (this.ScrollType == ScrollType.Horizontal)
      {
        this.firstButton.SetBounds(0, 0, num, size.Height);
        this.secondButton.SetBounds(size.Width - num, 0, num, size.Height);
      }
      else
      {
        this.firstButton.SetBounds(0, 0, size.Width, num);
        this.secondButton.SetBounds(0, size.Height - num, size.Width, num);
      }
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      return SystemSkinManager.DefaultElement;
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      return this.GetXPVisualStyle();
    }

    protected override void InitializeSystemSkinPaint()
    {
      base.InitializeSystemSkinPaint();
      int num = (int) this.thumb.SetValue(RadElement.MarginProperty, (object) Padding.Empty);
    }

    protected override void UnitializeSystemSkinPaint()
    {
      base.UnitializeSystemSkinPaint();
      int num = (int) this.thumb.ResetValue(RadElement.MarginProperty, ValueResetFlags.Local);
    }

    protected override bool ShouldPaintChild(RadElement element)
    {
      bool? paintSystemSkin = this.paintSystemSkin;
      if ((!paintSystemSkin.GetValueOrDefault() ? 0 : (paintSystemSkin.HasValue ? 1 : 0)) != 0 && (element is FillPrimitive || element is BorderPrimitive))
        return false;
      return base.ShouldPaintChild(element);
    }

    protected override void PaintElementSkin(IGraphics graphics)
    {
      Rectangle bounds1 = new Rectangle(new Point(this.firstButton.ControlBoundingRectangle.Right, 0), new Size(this.thumb.ControlBoundingRectangle.Left - this.firstButton.ControlBoundingRectangle.Right, this.Bounds.Height));
      Rectangle bounds2 = new Rectangle(new Point(this.thumb.ControlBoundingRectangle.Right, 0), new Size(this.secondButton.ControlBoundingRectangle.Left - this.thumb.ControlBoundingRectangle.Right, this.Bounds.Height));
      Rectangle bounds3 = new Rectangle(new Point(0, this.firstButton.ControlBoundingRectangle.Bottom), new Size(this.Bounds.Width, this.thumb.ControlBoundingRectangle.Top - this.firstButton.ControlBoundingRectangle.Bottom));
      Rectangle bounds4 = new Rectangle(new Point(0, this.thumb.ControlBoundingRectangle.Bottom), new Size(this.Bounds.Width, this.secondButton.ControlBoundingRectangle.Top - this.thumb.ControlBoundingRectangle.Bottom));
      Graphics underlayGraphics = graphics.UnderlayGraphics as Graphics;
      if (!this.Enabled)
      {
        if (this.ScrollType == ScrollType.Horizontal)
        {
          VisualStyleElement disabled1 = VisualStyleElement.ScrollBar.LeftTrackHorizontal.Disabled;
          this.PaintVisualStyleElement(underlayGraphics, disabled1, bounds1);
          VisualStyleElement disabled2 = VisualStyleElement.ScrollBar.RightTrackHorizontal.Disabled;
          this.PaintVisualStyleElement(underlayGraphics, disabled2, bounds2);
        }
        else
        {
          VisualStyleElement disabled1 = VisualStyleElement.ScrollBar.UpperTrackVertical.Disabled;
          this.PaintVisualStyleElement(underlayGraphics, disabled1, bounds3);
          VisualStyleElement disabled2 = VisualStyleElement.ScrollBar.LowerTrackVertical.Disabled;
          this.PaintVisualStyleElement(underlayGraphics, disabled2, bounds4);
        }
      }
      else if (this.ScrollType == ScrollType.Horizontal)
      {
        if (!this.IsMouseDown && !this.IsMouseOver)
        {
          VisualStyleElement normal1 = VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal;
          this.PaintVisualStyleElement(underlayGraphics, normal1, bounds1);
          VisualStyleElement normal2 = VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal;
          this.PaintVisualStyleElement(underlayGraphics, normal2, bounds2);
        }
        else if (this.IsMouseOver && !this.IsMouseDown)
        {
          VisualStyleElement element1 = VisualStyleElement.ScrollBar.LeftTrackHorizontal.Hot;
          Point client = this.ElementTree.Control.PointToClient(Control.MousePosition);
          if (!bounds1.Contains(client))
            element1 = VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal;
          this.PaintVisualStyleElement(underlayGraphics, element1, bounds4);
          VisualStyleElement element2 = VisualStyleElement.ScrollBar.RightTrackHorizontal.Hot;
          if (!bounds2.Contains(client))
            element2 = VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal;
          this.PaintVisualStyleElement(underlayGraphics, element2, bounds4);
        }
        else
        {
          if (!this.IsMouseDown)
            return;
          VisualStyleElement element1 = VisualStyleElement.ScrollBar.LeftTrackHorizontal.Pressed;
          Point client = this.ElementTree.Control.PointToClient(Control.MousePosition);
          if (!bounds1.Contains(client))
            element1 = VisualStyleElement.ScrollBar.LeftTrackHorizontal.Normal;
          this.PaintVisualStyleElement(underlayGraphics, element1, bounds1);
          VisualStyleElement element2 = VisualStyleElement.ScrollBar.RightTrackHorizontal.Pressed;
          if (!bounds2.Contains(client))
            element2 = VisualStyleElement.ScrollBar.RightTrackHorizontal.Normal;
          this.PaintVisualStyleElement(underlayGraphics, element2, bounds2);
        }
      }
      else if (!this.IsMouseDown && !this.IsMouseOver)
      {
        VisualStyleElement normal1 = VisualStyleElement.ScrollBar.UpperTrackVertical.Normal;
        this.PaintVisualStyleElement(underlayGraphics, normal1, bounds3);
        VisualStyleElement normal2 = VisualStyleElement.ScrollBar.LowerTrackVertical.Normal;
        this.PaintVisualStyleElement(underlayGraphics, normal2, bounds4);
      }
      else if (this.IsMouseOver && !this.IsMouseDown)
      {
        VisualStyleElement element1 = VisualStyleElement.ScrollBar.UpperTrackVertical.Hot;
        Point client = this.ElementTree.Control.PointToClient(Control.MousePosition);
        if (!bounds3.Contains(client))
          element1 = VisualStyleElement.ScrollBar.UpperTrackVertical.Normal;
        this.PaintVisualStyleElement(underlayGraphics, element1, bounds3);
        VisualStyleElement element2 = VisualStyleElement.ScrollBar.LowerTrackVertical.Hot;
        if (!bounds4.Contains(client))
          element2 = VisualStyleElement.ScrollBar.LowerTrackVertical.Normal;
        this.PaintVisualStyleElement(underlayGraphics, element2, bounds4);
      }
      else
      {
        if (!this.IsMouseDown)
          return;
        VisualStyleElement element1 = VisualStyleElement.ScrollBar.UpperTrackVertical.Pressed;
        Point client = this.ElementTree.Control.PointToClient(Control.MousePosition);
        if (!bounds3.Contains(client))
          element1 = VisualStyleElement.ScrollBar.UpperTrackVertical.Normal;
        this.PaintVisualStyleElement(underlayGraphics, element1, bounds3);
        VisualStyleElement element2 = VisualStyleElement.ScrollBar.LowerTrackVertical.Pressed;
        if (!bounds4.Contains(client))
          element2 = VisualStyleElement.ScrollBar.LowerTrackVertical.Normal;
        this.PaintVisualStyleElement(underlayGraphics, element2, bounds4);
      }
    }

    protected virtual void PaintVisualStyleElement(
      Graphics graphics,
      VisualStyleElement element,
      Rectangle bounds)
    {
      if (!SystemSkinManager.Instance.SetCurrentElement(element))
        return;
      SystemSkinManager.Instance.PaintCurrentElement(graphics, bounds);
    }

    protected virtual void PaintVerticalVisualStyleElements()
    {
    }

    protected virtual void PaintHorizontalVisualStylesElements()
    {
    }
  }
}
