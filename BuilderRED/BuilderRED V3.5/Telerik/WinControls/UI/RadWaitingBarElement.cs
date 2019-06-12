// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadWaitingBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadWaitingBarElement : LightVisualElement
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register("IsVertical", typeof (bool), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsWaitingProperty = RadProperty.Register(nameof (IsWaiting), typeof (bool), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty StretchIndicatorsHorizontallyProperty = RadProperty.Register(nameof (StretchIndicatorsHorizontally), typeof (bool), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsLayout));
    public static RadProperty StretchIndicatorsVerticallyProperty = RadProperty.Register(nameof (StretchIndicatorsVertically), typeof (bool), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout));
    public static RadProperty WaitingBarOrientationProperty = RadProperty.Register(nameof (WaitingBarOrientation), typeof (Orientation), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty WaitingDirectionProperty = RadProperty.Register(nameof (WaitingDirection), typeof (ProgressOrientation), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ProgressOrientation.Right, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty WaitingIndicatorSizeProperty = RadProperty.Register(nameof (WaitingIndicatorSize), typeof (Size), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(50, 14), ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty WaitingSpeedProperty = RadProperty.Register(nameof (WaitingSpeed), typeof (int), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90, ElementPropertyOptions.AffectsArrange));
    public static RadProperty WaitingStepProperty = RadProperty.Register(nameof (WaitingStep), typeof (int), typeof (RadWaitingBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsArrange));
    private WaitingBarContentElement contentElement;
    protected Timer timer;
    protected bool continueWaiting;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the instance of the WaitingBarContentElement class which represents the waiting bar content element")]
    public WaitingBarContentElement ContentElement
    {
      get
      {
        return this.contentElement;
      }
    }

    [Description("Gets a collection of BaseWaitingBarIndicatorElement elements which contains all waiting indicators of RadWaitingBar")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WaitingBarIndicatorCollection WaitingIndicators
    {
      get
      {
        return this.ContentElement.WaitingIndicators;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets an instance of the WaitingBarTextElement class which represents the waiting bar text element")]
    public WaitingBarTextElement TextElement
    {
      get
      {
        return this.ContentElement.TextElement;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets an instance of the WaitingBarSeparatorElement class which represents the waiting bar separator element")]
    public WaitingBarSeparatorElement SeparatorElement
    {
      get
      {
        return this.ContentElement.SeparatorElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(null)]
    [Description("Gets and sets the Image of the element's indicator")]
    public Image IndicatorImage
    {
      get
      {
        if (this.WaitingIndicators.Count > 0)
          return this.WaitingIndicators[0].Image;
        return (Image) null;
      }
      set
      {
        for (int index = 0; index < this.WaitingIndicators.Count; ++index)
          this.WaitingIndicators[index].Image = value;
      }
    }

    [DefaultValue(-1)]
    [Browsable(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets and sets the ImageIndex of the element's indicator")]
    public int IndicatorImageIndex
    {
      get
      {
        if (this.WaitingIndicators.Count > 0)
          return this.WaitingIndicators[0].ImageIndex;
        return -1;
      }
      set
      {
        for (int index = 0; index < this.WaitingIndicators.Count; ++index)
          this.WaitingIndicators[index].ImageIndex = value;
      }
    }

    [Browsable(true)]
    [DefaultValue("")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets and sets the ImageKey of the element's indicator")]
    public string IndicatorImageKey
    {
      get
      {
        if (this.WaitingIndicators.Count > 0)
          return this.WaitingIndicators[0].ImageKey;
        return "";
      }
      set
      {
        for (int index = 0; index < this.WaitingIndicators.Count; ++index)
          this.WaitingIndicators[index].ImageKey = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Shows text in RadWaitingBar.")]
    public bool ShowText
    {
      get
      {
        return this.TextElement.DrawText;
      }
      set
      {
        this.TextElement.DrawText = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Indicates whether the indicators are stretched horizontally")]
    public bool StretchIndicatorsHorizontally
    {
      get
      {
        return (bool) this.GetValue(RadWaitingBarElement.StretchIndicatorsHorizontallyProperty);
      }
      set
      {
        if (this.StretchIndicatorsHorizontally == value)
          return;
        int num = (int) this.SetValue(RadWaitingBarElement.StretchIndicatorsHorizontallyProperty, (object) value);
        for (int index = 0; index < this.WaitingIndicators.Count; ++index)
          this.WaitingIndicators[index].StretchHorizontally = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Indicates whether the indicators are stretched vertically")]
    public bool StretchIndicatorsVertically
    {
      get
      {
        return (bool) this.GetValue(RadWaitingBarElement.StretchIndicatorsVerticallyProperty);
      }
      set
      {
        if (this.StretchIndicatorsVertically == value)
          return;
        int num = (int) this.SetValue(RadWaitingBarElement.StretchIndicatorsVerticallyProperty, (object) value);
        for (int index = 0; index < this.WaitingIndicators.Count; ++index)
          this.WaitingIndicators[index].StretchVertically = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(WaitingBarStyles.Indeterminate)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Sets the style of the WaitingBarElement")]
    public WaitingBarStyles WaitingStyle
    {
      get
      {
        return this.ContentElement.WaitingStyle;
      }
      set
      {
        if (this.ContentElement.WaitingStyle == value)
          return;
        this.ContentElement.WaitingStyle = value;
        this.InvalidateMeasure(true);
        this.InvalidateArrange(true);
      }
    }

    [Description("Gets and sets the size of the indicator in pixels")]
    [Browsable(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Size WaitingIndicatorSize
    {
      get
      {
        Size size = (Size) this.GetValue(RadWaitingBarElement.WaitingIndicatorSizeProperty);
        size = new Size((int) Math.Round((double) size.Width * (double) this.DpiScaleFactor.Width), (int) Math.Round((double) size.Height * (double) this.DpiScaleFactor.Height));
        return size;
      }
      set
      {
        if (!(this.WaitingIndicatorSize != value))
          return;
        int num = (int) this.SetValue(RadWaitingBarElement.WaitingIndicatorSizeProperty, (object) new Size(value.Width, value.Height));
        this.InvalidateMeasure(true);
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Indicates whether the element is currently waiting")]
    public bool IsWaiting
    {
      get
      {
        return this.timer.Enabled;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(Orientation.Horizontal)]
    [Description("When set to vertical the RadWaitingBar WaitingDirection property is set to Bottom. When set to horizontal the RadWaitingBar WaitingDirection is property is set to Right")]
    [Browsable(true)]
    [Category("Behavior")]
    public virtual Orientation WaitingBarOrientation
    {
      get
      {
        return (Orientation) this.GetValue(RadWaitingBarElement.WaitingBarOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadWaitingBarElement.WaitingBarOrientationProperty, (object) value);
        bool flag = false;
        if (this.WaitingDirection == ProgressOrientation.Left || this.WaitingDirection == ProgressOrientation.Right)
          flag = true;
        if (!flag && value == Orientation.Horizontal)
          this.WaitingDirection = ProgressOrientation.Right;
        if (!flag || value != Orientation.Vertical)
          return;
        this.WaitingDirection = ProgressOrientation.Bottom;
      }
    }

    [Browsable(true)]
    [Description("Gets and sets the direction of waiting")]
    [DefaultValue(ProgressOrientation.Right)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ProgressOrientation WaitingDirection
    {
      get
      {
        return (ProgressOrientation) this.GetValue(RadWaitingBarElement.WaitingDirectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadWaitingBarElement.WaitingDirectionProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DefaultValue(90)]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets and sets the speed of the animation. Higher value moves the indicator more quickly across the bar")]
    public int WaitingSpeed
    {
      get
      {
        return (int) this.GetValue(RadWaitingBarElement.WaitingSpeedProperty);
      }
      set
      {
        if (value < 0 || value > 100)
          throw new ArgumentOutOfRangeException();
        int num = (int) this.SetValue(RadWaitingBarElement.WaitingSpeedProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(1)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets and sets the number of pixels the indicator moves each step")]
    public int WaitingStep
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(RadWaitingBarElement.WaitingStepProperty) * (double) this.DpiScaleFactor.Width);
      }
      set
      {
        if (value < 0 || value > 100)
          throw new ArgumentOutOfRangeException();
        int num = (int) this.SetValue(RadWaitingBarElement.WaitingStepProperty, (object) value);
      }
    }

    [Description("Occurs when the control starts waiting")]
    [Category("Behavior")]
    public event EventHandler WaitingStarted;

    [Description("Occurs when the control ends waiting")]
    [Category("Behavior")]
    public event EventHandler WaitingStopped;

    protected virtual void OnStartWaiting(EventArgs e)
    {
      if (this.WaitingStarted == null)
        return;
      this.WaitingStarted((object) this, e);
    }

    protected virtual void OnStopWaiting(EventArgs e)
    {
      if (this.WaitingStopped == null)
        return;
      this.WaitingStopped((object) this, e);
    }

    static RadWaitingBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadWaitingBarStateManager(), typeof (RadWaitingBarElement));
    }

    public RadWaitingBarElement()
    {
      this.timer = new Timer();
      this.timer.Tick += new EventHandler(this.Timer_Tick);
      this.continueWaiting = false;
    }

    protected override void DisposeManagedResources()
    {
      if (this.timer != null)
      {
        this.timer.Dispose();
        this.timer = (Timer) null;
      }
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.contentElement = new WaitingBarContentElement();
      this.Children.Add((RadElement) this.ContentElement);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawBorder = true;
      this.DrawFill = true;
      this.DrawText = true;
      this.ClipDrawing = true;
    }

    public virtual void StartWaiting()
    {
      if (!this.IsInValidState(false))
        return;
      if (this.WaitingSpeed != 0 && this.WaitingStep != 0 && this.Enabled)
      {
        this.timer.Interval = 100 - this.WaitingSpeed + 1;
        this.timer.Start();
        this.ContentElement.IsWaiting = true;
        this.OnStartWaiting(EventArgs.Empty);
      }
      if (this.WaitingSpeed != 0 && this.WaitingStep != 0)
        return;
      this.continueWaiting = true;
    }

    public virtual void StopWaiting()
    {
      if (!this.IsInValidState(false))
        return;
      this.timer.Stop();
      this.continueWaiting = false;
      this.ContentElement.IsWaiting = false;
      this.OnStopWaiting(EventArgs.Empty);
    }

    public void ResetWaiting()
    {
      if (!this.Enabled)
        return;
      this.ContentElement.ResetWaiting();
    }

    protected virtual void OnWaitingStep()
    {
      if (this.ContentElement.IsOldWaitingStyle)
      {
        this.ContentElement.IncrementOffset(this.WaitingStep);
        this.InvalidateArrange(true);
      }
      else
      {
        foreach (BaseWaitingBarIndicatorElement waitingIndicator in (Collection<BaseWaitingBarIndicatorElement>) this.WaitingIndicators)
          waitingIndicator.Animate(this.WaitingStep);
        this.Invalidate();
      }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.OnWaitingStep();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      Padding borderThickness = this.GetBorderThickness(false);
      SizeF empty = SizeF.Empty;
      availableSize.Width -= (float) (borderThickness.Horizontal + this.Padding.Horizontal);
      availableSize.Height -= (float) (borderThickness.Vertical + this.Padding.Vertical);
      this.ContentElement.Measure(availableSize);
      empty.Width = this.ContentElement.DesiredSize.Width + (float) borderThickness.Horizontal + (float) this.Padding.Horizontal;
      empty.Height = this.ContentElement.DesiredSize.Height + (float) borderThickness.Vertical + (float) this.Padding.Vertical;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      this.ContentElement.Arrange(this.GetClientRectangle(finalSize));
      return finalSize;
    }

    protected virtual float GetTransformAngle(ProgressOrientation direction)
    {
      float num = 0.0f;
      if (direction == ProgressOrientation.Top || direction == ProgressOrientation.Bottom)
        num = 270f;
      return num;
    }

    protected virtual void TransformElements()
    {
      this.ContentElement.TextElement.AngleTransform = this.GetTransformAngle(this.WaitingDirection);
      this.ResetWaiting();
    }

    protected virtual void UpdateElementsState(ProgressOrientation direction)
    {
      bool flag = false;
      if (direction == ProgressOrientation.Top || direction == ProgressOrientation.Bottom)
        flag = true;
      int num1 = (int) this.SetValue(RadWaitingBarElement.IsVerticalProperty, (object) flag);
      if (!this.ContentElement.IsOldWaitingStyle)
        return;
      int num2 = (int) this.SeparatorElement.SetValue(WaitingBarSeparatorElement.IsVerticalProperty, (object) flag);
      for (int index = 0; index < this.WaitingIndicators.Count; ++index)
      {
        WaitingBarIndicatorElement waitingIndicator = this.WaitingIndicators[index] as WaitingBarIndicatorElement;
        int num3 = (int) waitingIndicator.SetValue(WaitingBarIndicatorElement.IsVerticalProperty, (object) flag);
        int num4 = (int) waitingIndicator.SeparatorElement.SetValue(WaitingBarSeparatorElement.IsVerticalProperty, (object) flag);
      }
    }

    private void UpdateIndicatorStretch(ProgressOrientation progressOrientation)
    {
      if (progressOrientation == ProgressOrientation.Left || progressOrientation == ProgressOrientation.Right)
      {
        this.StretchIndicatorsHorizontally = false;
        this.StretchIndicatorsVertically = true;
      }
      else
      {
        this.StretchIndicatorsHorizontally = true;
        this.StretchIndicatorsVertically = false;
      }
    }

    private void UpdateElementOrientation(
      ProgressOrientation oldDirection,
      ProgressOrientation newDirection)
    {
      bool flag1 = false;
      bool flag2 = false;
      if (oldDirection == ProgressOrientation.Top || oldDirection == ProgressOrientation.Bottom)
        flag2 = true;
      if (newDirection == ProgressOrientation.Top || newDirection == ProgressOrientation.Bottom)
        flag1 = true;
      if (flag2 && !flag1)
      {
        this.WaitingBarOrientation = Orientation.Horizontal;
        if (this.GetValueSource(RadWaitingBarElement.WaitingIndicatorSizeProperty) == ValueSource.Local)
          return;
        int num = (int) this.SetDefaultValueOverride(RadWaitingBarElement.WaitingIndicatorSizeProperty, (object) new Size(this.WaitingIndicatorSize.Height, this.WaitingIndicatorSize.Width));
      }
      else
      {
        if (flag2 || !flag1)
          return;
        this.WaitingBarOrientation = Orientation.Vertical;
        if (this.GetValueSource(RadWaitingBarElement.WaitingIndicatorSizeProperty) == ValueSource.Local)
          return;
        int num = (int) this.SetDefaultValueOverride(RadWaitingBarElement.WaitingIndicatorSizeProperty, (object) new Size(this.WaitingIndicatorSize.Height, this.WaitingIndicatorSize.Width));
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadWaitingBarElement.WaitingDirectionProperty)
      {
        ProgressOrientation newValue = (ProgressOrientation) e.NewValue;
        this.UpdateElementsState(newValue);
        this.UpdateElementOrientation((ProgressOrientation) e.OldValue, newValue);
        this.UpdateIndicatorStretch(newValue);
        this.TransformElements();
        this.ContentElement.WaitingDirection = newValue;
      }
      if (e.Property == RadWaitingBarElement.WaitingStepProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue == 0 && this.WaitingSpeed != 0 && this.IsWaiting)
        {
          this.StopWaiting();
          this.continueWaiting = true;
        }
        if (newValue != 0 && this.WaitingSpeed != 0 && this.continueWaiting)
        {
          this.StartWaiting();
          this.continueWaiting = false;
        }
      }
      if (e.Property == RadWaitingBarElement.WaitingSpeedProperty)
      {
        int newValue = (int) e.NewValue;
        if (newValue == 0 && this.WaitingStep != 0 && this.IsWaiting)
        {
          this.StopWaiting();
          this.continueWaiting = true;
        }
        else if (newValue != 0 && this.WaitingStep != 0 && this.IsWaiting)
          this.timer.Interval = 100 - newValue + 1;
        else if (newValue != 0 && this.WaitingStep != 0 && (!this.IsWaiting && this.continueWaiting))
        {
          this.timer.Interval = 100 - newValue + 1;
          this.StartWaiting();
          this.continueWaiting = false;
        }
      }
      if (e.Property != RadElement.EnabledProperty || (bool) e.NewValue || !this.IsWaiting)
        return;
      this.StopWaiting();
    }
  }
}
