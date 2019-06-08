// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTrackBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Data;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadTrackBarElement : StackLayoutElement
  {
    private TrackBarSnapModes snapMode = TrackBarSnapModes.SnapToTicks;
    private SizeF measuredSize = SizeF.Empty;
    private bool autoSizeCore = true;
    public static RadProperty IsVerticalProperty = RadProperty.Register(nameof (IsVertical), typeof (bool), typeof (RadTrackBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsTheme));
    private const string InvalidValueMessage = "Value of '{0}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.";
    private TrackBarRangeCollection ranges;
    private TrackBarThumbElement currentThumb;
    private TrackBarBodyElement bodyElement;
    private TrackBarArrowButton leftButton;
    private TrackBarArrowButton rightButton;
    internal float tickOffSet;
    private int largeChange;
    private int smallChange;
    private float value;
    private bool showButtons;
    private bool shouldTriggerValueChangeEvent;
    private bool allowKeyNavigation;

    static RadTrackBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TrackBarElementStateManager(), typeof (RadTrackBarElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.FitInAvailableSize = true;
      this.TextOrientation = Orientation.Horizontal;
      this.DrawText = false;
      this.MinSize = new Size(100, 0);
      this.SmallChange = 1;
    }

    protected override void CreateChildElements()
    {
      this.ranges = new TrackBarRangeCollection(this);
      this.ranges.CollectionChanged += new NotifyCollectionChangedEventHandler(this.RangeCollection_CollectionChanged);
      this.leftButton = new TrackBarArrowButton();
      this.leftButton.Class = "LeftArrowButton";
      this.leftButton.Click += new EventHandler(this.leftButton_Click);
      this.leftButton.Alignment = ContentAlignment.MiddleCenter;
      this.bodyElement = new TrackBarBodyElement();
      this.rightButton = new TrackBarArrowButton();
      this.rightButton.Class = "RightArrowButton";
      this.rightButton.Click += new EventHandler(this.rightButton_Click);
      this.rightButton.Alignment = ContentAlignment.MiddleCenter;
      this.SetElementsOrder();
      this.SetHandlesVisibility();
      this.Ranges.Add(new TrackBarRange(0.0f, 0.0f, "Default"));
      this.BodyElement.ScaleContainerElement.TopScaleElement.TickContainerElement.UpdateTickElements();
      this.BodyElement.ScaleContainerElement.BottomScaleElement.TickContainerElement.UpdateTickElements();
      this.BodyElement.ScaleContainerElement.TopScaleElement.LabelContainerElement.UpdateLabelElements();
      this.BodyElement.ScaleContainerElement.BottomScaleElement.LabelContainerElement.UpdateLabelElements();
    }

    [Browsable(false)]
    public TrackBarBodyElement BodyElement
    {
      get
      {
        return this.bodyElement;
      }
    }

    [Browsable(false)]
    public TrackBarArrowButton LeftButton
    {
      get
      {
        return this.leftButton;
      }
    }

    [Browsable(false)]
    public TrackBarArrowButton RightButton
    {
      get
      {
        return this.rightButton;
      }
    }

    [DefaultValue(0.0f)]
    public float Minimum
    {
      get
      {
        return this.ranges.Minimum;
      }
      set
      {
        if ((double) this.ranges.Minimum == (double) value)
          return;
        float num = (double) value < (double) this.ranges.Maximum ? value : this.ranges.Maximum - 1f;
        if ((double) num > (double) this.Value)
        {
          this.Value = num;
          this.ranges[0].Start = num;
        }
        this.ranges.Minimum = num;
        this.OnNotifyPropertyChanged(nameof (Minimum));
      }
    }

    [DefaultValue(20f)]
    public float Maximum
    {
      get
      {
        return this.ranges.Maximum;
      }
      set
      {
        if ((double) this.ranges.Maximum == (double) value)
          return;
        float num = (double) value > (double) this.ranges.Minimum ? value : this.ranges.Minimum + 1f;
        if ((double) num < (double) this.Value)
          this.Value = num;
        this.ranges.Maximum = num;
        this.OnNotifyPropertyChanged(nameof (Maximum));
      }
    }

    [DefaultValue(0.0f)]
    public float Value
    {
      get
      {
        if (this.Ranges.Count == 0)
          return this.Minimum;
        return this.ranges[0].End;
      }
      set
      {
        if ((double) this.value == (double) value)
          return;
        if ((double) value < (double) this.Minimum)
        {
          if (!this.DesignMode)
            throw new ArgumentOutOfRangeException(nameof (Value), string.Format("Value of '{0}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.", (object) value));
          int num = (int) MessageBox.Show(string.Format("Value of '{0}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.", (object) value));
          this.value = this.Minimum;
          this.ranges[0].End = this.Minimum;
        }
        else if ((double) value > (double) this.Maximum)
        {
          if (!this.DesignMode)
            throw new ArgumentOutOfRangeException(nameof (Value), string.Format("Value of '{0}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.", (object) value));
          int num = (int) MessageBox.Show(string.Format("Value of '{0}' is not valid for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'.", (object) value));
          this.value = this.Maximum;
          this.ranges[0].End = this.Maximum;
        }
        else
        {
          this.ranges[0].End = value;
          this.value = value;
        }
        this.OnNotifyPropertyChanged(nameof (Value));
        if (this.shouldTriggerValueChangeEvent)
        {
          this.OnValueChanged(new EventArgs());
          this.shouldTriggerValueChangeEvent = false;
        }
        ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "ValueChanged", (object) (int) this.value);
      }
    }

    public void FireScrollEvent(int oldValue)
    {
      int newValue = (int) this.value;
      if (oldValue == newValue)
        return;
      this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, oldValue, newValue));
    }

    [DefaultValue(TickStyles.None)]
    public TickStyles TickStyle
    {
      get
      {
        return this.bodyElement.ScaleContainerElement.TickStyle;
      }
      set
      {
        this.bodyElement.ScaleContainerElement.TickStyle = value;
        this.OnNotifyPropertyChanged(nameof (TickStyle));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool IsVertical
    {
      get
      {
        return (bool) this.GetValue(RadTrackBarElement.IsVerticalProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTrackBarElement.IsVerticalProperty, (object) value);
        this.OnNotifyPropertyChanged(nameof (IsVertical));
      }
    }

    [DefaultValue(true)]
    public bool ShowSlideArea
    {
      get
      {
        return this.bodyElement.ScaleContainerElement.TrackBarLineElement.ShowSlideArea;
      }
      set
      {
        this.bodyElement.ScaleContainerElement.TrackBarLineElement.ShowSlideArea = value;
      }
    }

    [DefaultValue(0)]
    public int LargeChange
    {
      get
      {
        return this.largeChange;
      }
      set
      {
        this.largeChange = value;
      }
    }

    [DefaultValue(1)]
    public int SmallChange
    {
      get
      {
        return this.smallChange;
      }
      set
      {
        this.smallChange = value;
      }
    }

    [DefaultValue(false)]
    public bool ShowTicks
    {
      get
      {
        return this.TickStyle != TickStyles.None;
      }
      set
      {
        if (value)
          this.TickStyle = TickStyles.Both;
        else
          this.TickStyle = TickStyles.None;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Color SliderAreaColor1
    {
      get
      {
        return this.bodyElement.ScaleContainerElement.TrackBarLineElement.SlideAreaColor1;
      }
      set
      {
        this.bodyElement.ScaleContainerElement.TrackBarLineElement.SlideAreaColor1 = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Color TickColor
    {
      get
      {
        return this.bodyElement.ScaleContainerElement.TopScaleElement.TickContainerElement.TickColor;
      }
      set
      {
        this.bodyElement.ScaleContainerElement.TopScaleElement.TickContainerElement.TickColor = value;
        this.bodyElement.ScaleContainerElement.BottomScaleElement.TickContainerElement.TickColor = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Color SliderAreaColor2
    {
      get
      {
        return this.bodyElement.ScaleContainerElement.TrackBarLineElement.SlideAreaColor2;
      }
      set
      {
        this.bodyElement.ScaleContainerElement.TrackBarLineElement.SlideAreaColor2 = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public float SliderAreaGradientAngle
    {
      get
      {
        return this.bodyElement.ScaleContainerElement.TrackBarLineElement.SlideAreaGradientAngle;
      }
      set
      {
        this.bodyElement.ScaleContainerElement.TrackBarLineElement.SlideAreaGradientAngle = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ThumbWidth
    {
      get
      {
        return this.ThumbSize.Width;
      }
      set
      {
        this.ThumbSize = new Size(value, this.ThumbSize.Height);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int TickFrequency
    {
      get
      {
        return this.SmallTickFrequency;
      }
      set
      {
        this.SmallTickFrequency = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SlideAreaWidth
    {
      get
      {
        return this.bodyElement.ScaleContainerElement.TrackBarLineElement.SlideAreaWidth;
      }
      set
      {
        this.bodyElement.ScaleContainerElement.TrackBarLineElement.SlideAreaWidth = value;
      }
    }

    [DefaultValue(TrackBarLabelStyle.None)]
    public TrackBarLabelStyle LabelStyle
    {
      get
      {
        return this.bodyElement.ScaleContainerElement.LabelStyles;
      }
      set
      {
        this.bodyElement.ScaleContainerElement.LabelStyles = value;
        this.OnNotifyPropertyChanged(nameof (LabelStyle));
      }
    }

    [DefaultValue(false)]
    public bool ShowButtons
    {
      get
      {
        return this.showButtons;
      }
      set
      {
        if (this.showButtons == value)
          return;
        this.showButtons = value;
        this.OnNotifyPropertyChanged(nameof (ShowButtons));
      }
    }

    [DefaultValue(5)]
    public int LargeTickFrequency
    {
      get
      {
        return this.BodyElement.ScaleContainerElement.LargeTickFrequency;
      }
      set
      {
        if (this.BodyElement.ScaleContainerElement.LargeTickFrequency == value)
          return;
        if (value < 0)
          value = 0;
        this.BodyElement.ScaleContainerElement.LargeTickFrequency = value;
        this.BodyElement.ScaleContainerElement.InvalidateMeasure(true);
        this.BodyElement.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (LargeTickFrequency));
      }
    }

    [DefaultValue(1)]
    public int SmallTickFrequency
    {
      get
      {
        return this.BodyElement.ScaleContainerElement.SmallTickFrequency;
      }
      set
      {
        if (this.SmallTickFrequency == value)
          return;
        if (value < 0)
          value = 0;
        this.BodyElement.ScaleContainerElement.SmallTickFrequency = value;
        this.BodyElement.ScaleContainerElement.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (SmallTickFrequency));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size ThumbSize
    {
      get
      {
        if (this.bodyElement.IndicatorContainerElement.Children.Count == 0)
          return Size.Empty;
        return (this.bodyElement.IndicatorContainerElement.Children[0] as TrackBarIndicatorElement).EndThumbElement.ThumbSize;
      }
      set
      {
        this.OnNotifyPropertyChanged(nameof (ThumbSize));
        foreach (TrackBarIndicatorElement child in this.bodyElement.IndicatorContainerElement.Children)
        {
          child.StartThumbElement.ThumbSize = value;
          child.EndThumbElement.ThumbSize = value;
        }
      }
    }

    [DefaultValue(TrackBarSnapModes.SnapToTicks)]
    public TrackBarSnapModes SnapMode
    {
      get
      {
        return this.snapMode;
      }
      set
      {
        if (this.snapMode == value)
          return;
        this.snapMode = value;
        this.OnNotifyPropertyChanged(nameof (SnapMode));
      }
    }

    [DefaultValue(TrackBarRangeMode.SingleThumb)]
    public TrackBarRangeMode TrackBarMode
    {
      get
      {
        return this.ranges.Mode;
      }
      set
      {
        if (this.ranges.Mode == value)
          return;
        this.ranges.Mode = value;
        this.OnNotifyPropertyChanged(nameof (TrackBarMode));
      }
    }

    public TrackBarRangeCollection Ranges
    {
      get
      {
        return this.ranges;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TrackBarThumbElement CurrentThumb
    {
      get
      {
        return this.currentThumb;
      }
      set
      {
        this.currentThumb = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal int MaxTickNumber
    {
      get
      {
        return (int) this.ranges.Maximum - (int) this.ranges.Minimum;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal float TickOffSet
    {
      get
      {
        return this.tickOffSet;
      }
      set
      {
        if ((double) Math.Abs(this.tickOffSet - value) <= 0.001)
          return;
        this.tickOffSet = value;
        this.OnNotifyPropertyChanged(nameof (TickOffSet));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal bool AutoSizeCore
    {
      get
      {
        return this.autoSizeCore;
      }
      set
      {
        this.autoSizeCore = value;
        this.OnNotifyPropertyChanged("TickAutoSizeCore");
      }
    }

    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        this.OnNotifyPropertyChanged("TickAutoSizeCore");
        base.AutoSize = value;
      }
    }

    [Description("Gets or Sets whether the selcted thumb should move on arrow key press.")]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool AllowKeyNavigation
    {
      get
      {
        return this.allowKeyNavigation;
      }
      set
      {
        this.allowKeyNavigation = value;
      }
    }

    [Description("Occurs when the value of the control changes")]
    [Category("Behavior")]
    public event EventHandler ValueChanged;

    [Description("Occurs when the trackBar slider moves")]
    [Category("Behavior")]
    public event ScrollEventHandler Scroll;

    public event LabelFormattingEventHandler LabelFormatting;

    public event TickFormattingEventHandler TickFormatting;

    public virtual void OnValueChanged(EventArgs e)
    {
      EventHandler valueChanged = this.ValueChanged;
      if (valueChanged == null)
        return;
      valueChanged((object) this, e);
    }

    public virtual void OnScroll(ScrollEventArgs e)
    {
      ScrollEventHandler scroll = this.Scroll;
      if (scroll == null)
        return;
      scroll((object) this, e);
    }

    protected internal virtual void OnLabelFormatting(TrackBarLabelElement labelElement)
    {
      LabelFormattingEventHandler labelFormatting = this.LabelFormatting;
      if (labelFormatting == null)
        return;
      labelFormatting((object) this, new LabelFormattingEventArgs(labelElement));
    }

    protected internal virtual void OnTickFormatting(TrackBarTickElement tickElement)
    {
      TickFormattingEventHandler tickFormatting = this.TickFormatting;
      if (tickFormatting == null)
        return;
      tickFormatting((object) this, new TickFormattingEventArgs(tickElement, tickElement.TickNumber));
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.BodyElement.ScaleContainerElement.TopScaleElement.LabelContainerElement.FormatLabels();
      this.BodyElement.ScaleContainerElement.BottomScaleElement.LabelContainerElement.FormatLabels();
      this.BodyElement.ScaleContainerElement.TopScaleElement.TickContainerElement.FormatTicks();
      this.BodyElement.ScaleContainerElement.BottomScaleElement.TickContainerElement.FormatTicks();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property.Name == "RightToLeft")
        this.OnNotifyPropertyChanged("RightToLeft");
      if (e.NewValue is Orientation)
        this.OnNotifyPropertyChanged("TrackBarOrientation");
      base.OnPropertyChanged(e);
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName == "ShowButtons" || propertyName == "LabelStyle" || (propertyName == "IsVertical" || propertyName == "TickStyle"))
        this.measuredSize = SizeF.Empty;
      if (propertyName == "Maximum" || propertyName == "Minimum" || (propertyName == "SmallTickFrequency" || propertyName == "LargeTickFrequency"))
      {
        this.bodyElement.ScaleContainerElement.TopScaleElement.TickContainerElement.UpdateTickElements();
        this.bodyElement.ScaleContainerElement.BottomScaleElement.TickContainerElement.UpdateTickElements();
        this.bodyElement.ScaleContainerElement.BottomScaleElement.LabelContainerElement.UpdateLabelElements();
        this.bodyElement.ScaleContainerElement.TopScaleElement.LabelContainerElement.UpdateLabelElements();
        this.bodyElement.InvalidateMeasure(true);
      }
      if (propertyName == "Value")
      {
        this.bodyElement.ScaleContainerElement.InvalidateMeasure(true);
        this.bodyElement.IndicatorContainerElement.InvalidateMeasure(true);
      }
      if (propertyName == "TickOffSet" || propertyName == "ThumbSize")
      {
        this.bodyElement.ScaleContainerElement.InvalidateMeasure(true);
        this.bodyElement.IndicatorContainerElement.InvalidateMeasure(true);
      }
      if (propertyName == "TrackBarOrientation" || propertyName == "TickAutoSizeCore")
      {
        if (this.Orientation == Orientation.Horizontal)
        {
          this.IsVertical = false;
          this.bodyElement.ScaleContainerElement.Orientation = Orientation.Vertical;
          this.bodyElement.ScaleContainerElement.TrackBarLineElement.IsVertical = false;
          this.bodyElement.ScaleContainerElement.TopScaleElement.Orientation = Orientation.Vertical;
          this.bodyElement.ScaleContainerElement.BottomScaleElement.Orientation = Orientation.Vertical;
          this.bodyElement.IndicatorContainerElement.StretchHorizontally = true;
          this.bodyElement.IndicatorContainerElement.StretchVertically = false;
          this.leftButton.IsVertical = false;
          this.rightButton.IsVertical = false;
          this.MinSize = new Size(100, 0);
          this.bodyElement.ScaleContainerElement.TopScaleElement.StretchHorizontally = true;
          this.bodyElement.ScaleContainerElement.BottomScaleElement.StretchHorizontally = true;
          if (this.ElementTree != null)
          {
            RadTrackBar control = this.ElementTree.Control as RadTrackBar;
            if (control != null)
            {
              if (control.AutoSize)
              {
                this.bodyElement.ScaleContainerElement.TopScaleElement.StretchVertically = false;
                this.bodyElement.ScaleContainerElement.BottomScaleElement.StretchVertically = false;
              }
              else
              {
                this.bodyElement.ScaleContainerElement.TopScaleElement.StretchVertically = true;
                this.bodyElement.ScaleContainerElement.BottomScaleElement.StretchVertically = true;
              }
            }
            else
            {
              this.bodyElement.ScaleContainerElement.TopScaleElement.StretchVertically = true;
              this.bodyElement.ScaleContainerElement.BottomScaleElement.StretchVertically = true;
            }
          }
        }
        else
        {
          this.IsVertical = true;
          this.bodyElement.ScaleContainerElement.Orientation = Orientation.Horizontal;
          this.bodyElement.ScaleContainerElement.TrackBarLineElement.IsVertical = true;
          this.bodyElement.ScaleContainerElement.TopScaleElement.Orientation = Orientation.Horizontal;
          this.bodyElement.ScaleContainerElement.BottomScaleElement.Orientation = Orientation.Horizontal;
          this.bodyElement.IndicatorContainerElement.StretchHorizontally = false;
          this.bodyElement.IndicatorContainerElement.StretchVertically = true;
          this.leftButton.IsVertical = true;
          this.rightButton.IsVertical = true;
          this.MinSize = new Size(0, 100);
          this.bodyElement.ScaleContainerElement.TopScaleElement.StretchVertically = true;
          this.bodyElement.ScaleContainerElement.BottomScaleElement.StretchVertically = true;
          if (this.ElementTree != null)
          {
            RadTrackBar control = this.ElementTree.Control as RadTrackBar;
            if (control != null)
            {
              if (control.AutoSize)
              {
                this.bodyElement.ScaleContainerElement.TopScaleElement.StretchHorizontally = false;
                this.bodyElement.ScaleContainerElement.BottomScaleElement.StretchHorizontally = false;
              }
              else
              {
                this.bodyElement.ScaleContainerElement.TopScaleElement.StretchHorizontally = true;
                this.bodyElement.ScaleContainerElement.BottomScaleElement.StretchHorizontally = true;
              }
            }
            else
            {
              this.bodyElement.ScaleContainerElement.TopScaleElement.StretchHorizontally = true;
              this.bodyElement.ScaleContainerElement.BottomScaleElement.StretchHorizontally = true;
            }
          }
        }
        this.SetElementsOrder();
        foreach (TrackBarIndicatorElement child in this.bodyElement.IndicatorContainerElement.Children)
        {
          child.Orientation = this.Orientation;
          child.UpdateIndicatorElement();
        }
        this.BodyElement.ScaleContainerElement.TopScaleElement.TickContainerElement.UpdateTickElements();
        this.BodyElement.ScaleContainerElement.BottomScaleElement.TickContainerElement.UpdateTickElements();
        this.BodyElement.ScaleContainerElement.TopScaleElement.LabelContainerElement.UpdateLabelElements();
        this.BodyElement.ScaleContainerElement.BottomScaleElement.LabelContainerElement.UpdateLabelElements();
        this.bodyElement.InvalidateArrange(true);
        this.OnNotifyPropertyChanged("RightToLeft");
      }
      if (propertyName == "ShowButtons")
        this.SetHandlesVisibility();
      if (propertyName == "TrackBarMode")
      {
        this.bodyElement.IndicatorContainerElement.UpdateIndicatorElements();
        this.bodyElement.InvalidateMeasure(true);
      }
      if (propertyName == "RightToLeft")
      {
        if (this.Orientation == Orientation.Horizontal)
        {
          if (this.RightToLeft)
          {
            this.leftButton.AngleTransform = 180f;
            this.rightButton.AngleTransform = 180f;
          }
          else
          {
            int num1 = (int) this.leftButton.ResetValue(RadElement.AngleTransformProperty, ValueResetFlags.Local);
            int num2 = (int) this.rightButton.ResetValue(RadElement.AngleTransformProperty, ValueResetFlags.Local);
          }
        }
        else
        {
          int num1 = (int) this.leftButton.ResetValue(RadElement.AngleTransformProperty, ValueResetFlags.Local);
          int num2 = (int) this.rightButton.ResetValue(RadElement.AngleTransformProperty, ValueResetFlags.Local);
        }
      }
      if (!(propertyName == "TrackBarMode"))
        return;
      this.OnNotifyPropertyChanged("TrackBarOrientation");
    }

    private void rightButton_Click(object sender, EventArgs e)
    {
      this.PerformThumbRangeMove(true);
    }

    private void leftButton_Click(object sender, EventArgs e)
    {
      this.PerformThumbRangeMove(false);
    }

    private void RangeCollection_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.ItemChanged)
      {
        this.InvalidateMeasure(true);
        if (this.TrackBarMode == TrackBarRangeMode.SingleThumb)
          this.shouldTriggerValueChangeEvent = true;
        this.Value = this.Ranges[0].End;
      }
      else
        this.BodyElement.IndicatorContainerElement.UpdateIndicatorElements();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (!this.AllowKeyNavigation)
        return;
      if (this.CurrentThumb == null)
      {
        TrackBarIndicatorElement child = this.BodyElement.IndicatorContainerElement.Children[0] as TrackBarIndicatorElement;
        if (child == null || child.Children.Count < 3)
          return;
        this.CurrentThumb = child.Children[this.Orientation == Orientation.Horizontal ? 2 : 0] as TrackBarThumbElement;
        if (this.CurrentThumb == null)
          return;
      }
      int oldValue = (int) this.Value;
      if (!this.CurrentThumb.IsFirst && (this.TrackBarMode == TrackBarRangeMode.SingleThumb || this.TrackBarMode == TrackBarRangeMode.StartFromTheBeginning))
      {
        if (e.KeyData == Keys.Home)
          this.CurrentThumb.RangeInfo.End = this.Minimum;
        else if (e.KeyData == Keys.End)
          this.CurrentThumb.RangeInfo.End = this.Maximum;
        else if (e.KeyData == Keys.Prior)
          this.CurrentThumb.RangeInfo.End -= (float) this.LargeChange;
        else if (e.KeyData == Keys.Next)
          this.CurrentThumb.RangeInfo.End += (float) this.LargeChange;
        else if (this.Orientation == Orientation.Horizontal)
        {
          if (e.KeyData == Keys.Left)
            this.CurrentThumb.RangeInfo.End -= (float) this.SmallChange;
          else if (e.KeyData == Keys.Right)
            this.CurrentThumb.RangeInfo.End += (float) this.SmallChange;
        }
        else if (e.KeyData == Keys.Down)
          this.CurrentThumb.RangeInfo.End -= (float) this.SmallChange;
        else if (e.KeyData == Keys.Up)
          this.CurrentThumb.RangeInfo.End += (float) this.SmallChange;
      }
      else if (this.Orientation == Orientation.Horizontal)
      {
        if (e.KeyData == Keys.Left)
          this.CurrentThumb.RangeInfo.Start -= (float) this.SmallChange;
        else if (e.KeyData == Keys.Right)
          this.CurrentThumb.RangeInfo.Start += (float) this.SmallChange;
      }
      else if (e.KeyData == Keys.Down)
        this.CurrentThumb.RangeInfo.Start -= (float) this.SmallChange;
      else if (e.KeyData == Keys.Up)
        this.CurrentThumb.RangeInfo.Start += (float) this.SmallChange;
      this.FireScrollEvent(oldValue);
    }

    private void SetElementsOrder()
    {
      this.Children.Clear();
      if (this.Orientation == Orientation.Horizontal)
      {
        this.Children.Add((RadElement) this.leftButton);
        this.Children.Add((RadElement) this.bodyElement);
        this.Children.Add((RadElement) this.rightButton);
      }
      else
      {
        this.Children.Add((RadElement) this.rightButton);
        this.Children.Add((RadElement) this.bodyElement);
        this.Children.Add((RadElement) this.leftButton);
      }
    }

    private void SetHandlesVisibility()
    {
      if (this.showButtons)
      {
        this.leftButton.Visibility = ElementVisibility.Visible;
        this.rightButton.Visibility = ElementVisibility.Visible;
      }
      else
      {
        this.leftButton.Visibility = ElementVisibility.Collapsed;
        this.rightButton.Visibility = ElementVisibility.Collapsed;
      }
    }

    private void PerformThumbRangeMove(bool isTopRightButton)
    {
      if (isTopRightButton)
        this.PerformThumbRangeMoveTopRight();
      else
        this.PerformThumbRangeMoveBottomLeft();
    }

    private void PerformThumbRangeMoveTopRight()
    {
      if (this.TrackBarMode == TrackBarRangeMode.SingleThumb || this.TrackBarMode == TrackBarRangeMode.StartFromTheBeginning)
      {
        if (this.currentThumb == null)
          this.ranges[0].End += (float) this.SmallChange;
        else
          this.currentThumb.RangeInfo.End += (float) this.SmallChange;
      }
      else if (this.currentThumb == null)
      {
        this.ranges[0].End += (float) this.SmallChange;
        this.ranges[0].Start += (float) this.SmallChange;
      }
      else if (this.currentThumb.IsFirst)
      {
        if ((double) this.currentThumb.RangeInfo.Start == (double) this.currentThumb.RangeInfo.End)
          this.currentThumb.RangeInfo.End += (float) this.SmallChange;
        this.currentThumb.RangeInfo.Start += (float) this.SmallChange;
      }
      else
      {
        if ((double) this.currentThumb.RangeInfo.Start == (double) this.currentThumb.RangeInfo.End)
          this.currentThumb.RangeInfo.Start += (float) this.SmallChange;
        this.currentThumb.RangeInfo.End += (float) this.SmallChange;
      }
    }

    private void PerformThumbRangeMoveBottomLeft()
    {
      if (this.TrackBarMode == TrackBarRangeMode.SingleThumb || this.TrackBarMode == TrackBarRangeMode.StartFromTheBeginning)
      {
        if (this.currentThumb == null)
          this.ranges[0].End -= (float) this.SmallChange;
        else
          this.currentThumb.RangeInfo.End -= (float) this.SmallChange;
      }
      else if (this.currentThumb == null)
      {
        this.ranges[0].End -= (float) this.SmallChange;
        this.ranges[0].Start -= (float) this.SmallChange;
      }
      else if (this.currentThumb.IsFirst)
      {
        if ((double) this.currentThumb.RangeInfo.Start == (double) this.currentThumb.RangeInfo.End)
          this.currentThumb.RangeInfo.End -= (float) this.SmallChange;
        this.currentThumb.RangeInfo.Start -= (float) this.SmallChange;
      }
      else
      {
        if ((double) this.currentThumb.RangeInfo.Start == (double) this.currentThumb.RangeInfo.End)
          this.currentThumb.RangeInfo.Start -= (float) this.SmallChange;
        this.currentThumb.RangeInfo.End -= (float) this.SmallChange;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.measuredSize == SizeF.Empty)
        this.measuredSize = sizeF;
      if ((double) sizeF.Width > (double) this.measuredSize.Width || (double) sizeF.Height > (double) this.measuredSize.Height)
        return this.measuredSize;
      return sizeF;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.measuredSize = SizeF.Empty;
    }
  }
}
