// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadToggleSwitchElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls.UI
{
  public class RadToggleSwitchElement : LightVisualElement
  {
    public static RadProperty ThumbOffsetProperty = RadProperty.Register(nameof (ThumbOffset), typeof (int), typeof (RadToggleSwitchElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ThumbTicknessProperty = RadProperty.Register(nameof (ThumbTickness), typeof (int), typeof (RadToggleSwitchElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsOnProperty = RadProperty.Register(nameof (IsOn), typeof (bool), typeof (RadToggleSwitchElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    internal const bool DefaultValue = true;
    internal const int ThumbTicknessDefaultValue = 20;
    internal const double SwitchElasticityDefaultValue = 0.5;
    internal const int AnimationIntervalDefaultValue = 10;
    internal const bool AllowAnimationDefaultValue = true;
    internal const int AnimationFramesDefaultValue = 20;
    internal const bool IsAnimatingDefaultValue = false;
    internal const string OnTextDefaultValue = "ON";
    internal const string OffTextDefaultValue = "OFF";
    internal const ToggleStateMode ToggleStateModeDefaultValue = ToggleStateMode.ClickAndDrag;
    private ToggleSwitchPartElement offElement;
    private ToggleSwitchPartElement onElement;
    private ToggleSwitchThumbElement thumb;
    private BorderPrimitive borderPrimitive;
    private bool value;
    private double switchElasticity;
    private bool readOnly;
    private ToggleStateMode toggleStateMode;
    private int animationInterval;
    private int animationFrames;
    private AnimatedPropertySetting currentAnimation;
    private bool isAnimating;
    private bool allowAnimation;
    private int mouseOffset;
    private int mouseDownXcoordinate;
    private bool isDraggingMouse;

    public event ValueChangingEventHandler ValueChanging;

    public event EventHandler ValueChanged;

    public event AnimationStartedEventHandler AnimationStarted;

    public event AnimationFinishedEventHandler AnimationFinished;

    static RadToggleSwitchElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ToggleSwitchElementStateManager(), typeof (RadToggleSwitchElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Value = true;
      this.ThumbTickness = 20;
      this.SwitchElasticity = 0.5;
      this.AnimationInterval = 10;
      this.AllowAnimation = true;
      this.AnimationFrames = 20;
      this.IsAnimating = false;
      this.readOnly = false;
      this.ToggleStateMode = ToggleStateMode.ClickAndDrag;
      this.currentAnimation = new AnimatedPropertySetting();
      this.currentAnimation.AnimationStarted += new AnimationStartedEventHandler(this.Animation_AnimationStarted);
      this.currentAnimation.AnimationFinished += new AnimationFinishedEventHandler(this.Animation_AnimationFinished);
    }

    protected override void CreateChildElements()
    {
      this.OnElement = this.CreatePartElement();
      this.OnElement.ShouldHandleMouseInput = true;
      this.OnElement.NotifyParentOnMouseInput = true;
      this.OnElement.Text = "ON";
      this.OnElement.DrawFill = true;
      this.OnElement.Class = "OnElement";
      this.OnElement.ThemeRole = "ToggleSwitchOnElement";
      this.Children.Add((RadElement) this.OnElement);
      this.OffElement = this.CreatePartElement();
      this.OffElement.ShouldHandleMouseInput = true;
      this.OffElement.NotifyParentOnMouseInput = true;
      this.OffElement.Text = "OFF";
      this.OffElement.DrawFill = true;
      this.OffElement.Class = "OffElement";
      this.OffElement.ThemeRole = "ToggleSwitchOffElement";
      this.Children.Add((RadElement) this.OffElement);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "ToggleSwitchBorder";
      this.BindBorderPrimitiveProperties();
      this.borderPrimitive.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Thumb = this.CreateThumbElement();
      this.Thumb.ShouldHandleMouseInput = true;
      this.Thumb.NotifyParentOnMouseInput = true;
      this.Thumb.DrawFill = true;
      this.Thumb.Class = "Thumb";
      this.Thumb.ThemeRole = "ToggleSwitchThumb";
      int num = (int) this.Thumb.BindProperty(ToggleSwitchThumbElement.IsOnProperty, (RadObject) this, RadToggleSwitchElement.IsOnProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.Thumb);
      this.MouseDown += new MouseEventHandler(this.RadToggleSwitchElement_MouseDown);
      this.MouseMove += new MouseEventHandler(this.RadToggleSwitchElement_MouseMove);
      this.MouseUp += new MouseEventHandler(this.RadToggleSwitchElement_MouseUp);
      this.KeyDown += new KeyEventHandler(this.RadToggleSwitchElement_KeyDown);
    }

    private void BindBorderPrimitiveProperties()
    {
      int num1 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.BottomColorProperty, (RadObject) this, LightVisualElement.BorderBottomColorProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.BottomShadowColorProperty, (RadObject) this, LightVisualElement.BorderBottomShadowColorProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.BottomWidthProperty, (RadObject) this, LightVisualElement.BorderBottomWidthProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.BorderBoxStyleProperty, (RadObject) this, LightVisualElement.BorderBoxStyleProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.borderPrimitive.BindProperty(VisualElement.ForeColorProperty, (RadObject) this, LightVisualElement.BorderColorProperty, PropertyBindingOptions.TwoWay);
      int num6 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.ForeColor2Property, (RadObject) this, LightVisualElement.BorderColor2Property, PropertyBindingOptions.TwoWay);
      int num7 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.ForeColor3Property, (RadObject) this, LightVisualElement.BorderColor3Property, PropertyBindingOptions.TwoWay);
      int num8 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.ForeColor4Property, (RadObject) this, LightVisualElement.BorderColor4Property, PropertyBindingOptions.TwoWay);
      int num9 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.BorderDashPatternProperty, (RadObject) this, LightVisualElement.BorderDashPatternProperty, PropertyBindingOptions.TwoWay);
      int num10 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.BorderDashStyleProperty, (RadObject) this, LightVisualElement.BorderDashStyleProperty, PropertyBindingOptions.TwoWay);
      int num11 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.BorderDrawModeProperty, (RadObject) this, LightVisualElement.BorderDrawModeProperty, PropertyBindingOptions.TwoWay);
      int num12 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.GradientAngleProperty, (RadObject) this, LightVisualElement.BorderGradientAngleProperty, PropertyBindingOptions.TwoWay);
      int num13 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.GradientStyleProperty, (RadObject) this, LightVisualElement.BorderGradientStyleProperty, PropertyBindingOptions.TwoWay);
      int num14 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.InnerColorProperty, (RadObject) this, LightVisualElement.BorderInnerColorProperty, PropertyBindingOptions.TwoWay);
      int num15 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.InnerColor2Property, (RadObject) this, LightVisualElement.BorderInnerColor2Property, PropertyBindingOptions.TwoWay);
      int num16 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.InnerColor3Property, (RadObject) this, LightVisualElement.BorderInnerColor3Property, PropertyBindingOptions.TwoWay);
      int num17 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.InnerColor4Property, (RadObject) this, LightVisualElement.BorderInnerColor4Property, PropertyBindingOptions.TwoWay);
      int num18 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.LeftColorProperty, (RadObject) this, LightVisualElement.BorderLeftColorProperty, PropertyBindingOptions.TwoWay);
      int num19 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.LeftShadowColorProperty, (RadObject) this, LightVisualElement.BorderLeftShadowColorProperty, PropertyBindingOptions.TwoWay);
      int num20 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.LeftWidthProperty, (RadObject) this, LightVisualElement.BorderLeftWidthProperty, PropertyBindingOptions.TwoWay);
      int num21 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.RightColorProperty, (RadObject) this, LightVisualElement.BorderRightColorProperty, PropertyBindingOptions.TwoWay);
      int num22 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.RightShadowColorProperty, (RadObject) this, LightVisualElement.BorderRightShadowColorProperty, PropertyBindingOptions.TwoWay);
      int num23 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.RightWidthProperty, (RadObject) this, LightVisualElement.BorderRightWidthProperty, PropertyBindingOptions.TwoWay);
      int num24 = (int) this.borderPrimitive.BindProperty(RadElement.BorderThicknessProperty, (RadObject) this, RadElement.BorderThicknessProperty, PropertyBindingOptions.TwoWay);
      int num25 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.TopColorProperty, (RadObject) this, LightVisualElement.BorderTopColorProperty, PropertyBindingOptions.TwoWay);
      int num26 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.TopShadowColorProperty, (RadObject) this, LightVisualElement.BorderTopShadowColorProperty, PropertyBindingOptions.TwoWay);
      int num27 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.TopWidthProperty, (RadObject) this, LightVisualElement.BorderTopWidthProperty, PropertyBindingOptions.TwoWay);
      int num28 = (int) this.borderPrimitive.BindProperty(BorderPrimitive.WidthProperty, (RadObject) this, LightVisualElement.BorderWidthProperty, PropertyBindingOptions.TwoWay);
      int num29 = (int) this.borderPrimitive.BindProperty(RadElement.ShouldPaintProperty, (RadObject) this, LightVisualElement.DrawBorderProperty, PropertyBindingOptions.TwoWay);
    }

    protected virtual ToggleSwitchPartElement CreatePartElement()
    {
      return new ToggleSwitchPartElement(this);
    }

    protected virtual ToggleSwitchThumbElement CreateThumbElement()
    {
      return new ToggleSwitchThumbElement();
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.currentAnimation.Stop((RadObject) this);
      this.currentAnimation.AnimationStarted -= new AnimationStartedEventHandler(this.Animation_AnimationStarted);
      this.currentAnimation.AnimationFinished -= new AnimationFinishedEventHandler(this.Animation_AnimationFinished);
    }

    [System.ComponentModel.DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to use animation when changing its state.")]
    public bool AllowAnimation
    {
      get
      {
        if (!ThemeResolutionService.AllowAnimations)
          return false;
        return this.allowAnimation;
      }
      set
      {
        this.allowAnimation = value;
      }
    }

    [System.ComponentModel.DefaultValue(10)]
    [Description("Gets or sets the animation interval.")]
    public int AnimationInterval
    {
      get
      {
        return this.animationInterval;
      }
      set
      {
        if (value < 1)
          return;
        this.animationInterval = value;
      }
    }

    [Description("Gets or sets the animation frames.")]
    [System.ComponentModel.DefaultValue(20)]
    public int AnimationFrames
    {
      get
      {
        return this.animationFrames;
      }
      set
      {
        if (value < 1)
          return;
        this.animationFrames = value;
      }
    }

    [System.ComponentModel.DefaultValue(false)]
    [Description("Gets a value indicating whether the control is currently animating.")]
    [Browsable(false)]
    public bool IsAnimating
    {
      get
      {
        return this.isAnimating;
      }
      set
      {
        this.isAnimating = value;
      }
    }

    public ToggleSwitchPartElement OnElement
    {
      get
      {
        return this.onElement;
      }
      protected set
      {
        this.onElement = value;
      }
    }

    public ToggleSwitchPartElement OffElement
    {
      get
      {
        return this.offElement;
      }
      protected set
      {
        this.offElement = value;
      }
    }

    public ToggleSwitchThumbElement Thumb
    {
      get
      {
        return this.thumb;
      }
      protected set
      {
        this.thumb = value;
      }
    }

    [Description("Gets or sets the value.")]
    [System.ComponentModel.DefaultValue(true)]
    public bool Value
    {
      get
      {
        return this.value;
      }
      set
      {
        if (value == this.Value)
          return;
        ValueChangingEventArgs e = new ValueChangingEventArgs((object) value, (object) this.Value);
        this.OnValueChanging(e);
        if (!e.Cancel)
        {
          this.value = value;
          int num = (int) this.SetValue(RadToggleSwitchElement.IsOnProperty, (object) this.value);
          this.OnValueChanged();
          if (this.IsAnimating)
            return;
          this.InvalidateMeasure();
        }
        else
          this.CancelAnimation();
      }
    }

    [VsbBrowsable(true)]
    [System.ComponentModel.DefaultValue(20)]
    [Description("Gets or sets width of the thumb.")]
    public int ThumbTickness
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadToggleSwitchElement.ThumbTicknessProperty), this.DpiScaleFactor);
      }
      set
      {
        if (value < 0)
          return;
        int num = (int) this.SetValue(RadToggleSwitchElement.ThumbTicknessProperty, (object) value);
        this.InvalidateMeasure();
      }
    }

    [System.ComponentModel.DefaultValue(0.5)]
    [Description("Determines how far the switch needs to be dragged before it snaps to the opposite side.")]
    public double SwitchElasticity
    {
      get
      {
        return this.switchElasticity;
      }
      set
      {
        if (0.0 > value || value > 1.0)
          return;
        this.switchElasticity = value;
      }
    }

    [System.ComponentModel.DefaultValue("ON")]
    [Description("Gets or sets the text displayed when the state is On.")]
    public string OnText
    {
      get
      {
        return this.OnElement.Text;
      }
      set
      {
        this.OnElement.Text = value;
      }
    }

    [Description("Gets or sets the text displayed when the state is Off.")]
    [System.ComponentModel.DefaultValue("OFF")]
    public string OffText
    {
      get
      {
        return this.OffElement.Text;
      }
      set
      {
        this.OffElement.Text = value;
      }
    }

    public int ThumbOffset
    {
      get
      {
        return (int) this.GetValue(RadToggleSwitchElement.ThumbOffsetProperty);
      }
      set
      {
        value = Math.Max(0, Math.Min(this.ControlBoundingRectangle.Width - this.ThumbTickness, value));
        if (value == this.ThumbOffset)
          return;
        int num = (int) this.SetValue(RadToggleSwitchElement.ThumbOffsetProperty, (object) value);
      }
    }

    public bool IsOn
    {
      get
      {
        return (bool) this.GetValue(RadToggleSwitchElement.IsOnProperty);
      }
    }

    public ToggleStateMode ToggleStateMode
    {
      get
      {
        if (this.readOnly)
          return ToggleStateMode.None;
        return this.toggleStateMode;
      }
      set
      {
        this.toggleStateMode = value;
      }
    }

    public bool ReadOnly
    {
      get
      {
        if (!this.readOnly)
          return this.toggleStateMode == ToggleStateMode.None;
        return true;
      }
      set
      {
        this.readOnly = value;
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      PointF location = new PointF(clientRectangle.X, clientRectangle.Y);
      if (!this.IsAnimating)
      {
        int num1 = (int) this.SetValue(RadToggleSwitchElement.ThumbOffsetProperty, (object) (this.Value ? (int) ((double) finalSize.Width - (double) this.ThumbTickness) : 0));
      }
      if (!this.RightToLeft)
      {
        int num2 = (int) ((double) finalSize.Width - (double) clientRectangle.X - (double) (this.ThumbTickness / 2));
        int num3 = (int) ((double) finalSize.Width - ((double) finalSize.Width - ((double) clientRectangle.X + (double) clientRectangle.Width)) - (double) (this.ThumbTickness / 2));
        location.X -= finalSize.Width - (float) this.ThumbTickness - (float) this.ThumbOffset;
        this.OnElement.Arrange(new RectangleF(location, new SizeF((float) num2, clientRectangle.Height)));
        location.X += (float) num2;
        if (this.ThumbTickness % 2 != 0)
          --num3;
        this.OffElement.Arrange(new RectangleF(location, new SizeF((float) num3, clientRectangle.Height)));
        this.borderPrimitive.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
        location.X -= (float) (this.ThumbTickness / 2);
        if (this.ThumbTickness % 2 != 0)
          --location.X;
        this.Thumb.Arrange(new RectangleF(location.X, 0.0f, (float) this.ThumbTickness, finalSize.Height));
      }
      else
      {
        int num2 = (int) ((double) finalSize.Width - (double) clientRectangle.X - (double) (this.ThumbTickness / 2));
        int num3 = (int) ((double) finalSize.Width - ((double) finalSize.Width - ((double) clientRectangle.X + (double) clientRectangle.Width)) - (double) (this.ThumbTickness / 2));
        location.X += clientRectangle.Width;
        location.X += finalSize.Width - (float) this.ThumbTickness - (float) this.ThumbOffset;
        location.X -= (float) num3;
        this.OnElement.Arrange(new RectangleF(location, new SizeF((float) num3, clientRectangle.Height)));
        location.X -= (float) num2;
        if (this.ThumbTickness % 2 != 0)
        {
          --num2;
          ++location.X;
        }
        this.OffElement.Arrange(new RectangleF(location, new SizeF((float) num2, clientRectangle.Height)));
        this.borderPrimitive.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
        location.X += (float) (num2 - this.ThumbTickness / 2);
        this.Thumb.Arrange(new RectangleF(location.X, 0.0f, (float) this.ThumbTickness, finalSize.Height));
      }
      return finalSize;
    }

    protected override RectangleF GetClientRectangle(SizeF finalSize)
    {
      RectangleF rectangleF = new RectangleF((float) this.Padding.Left, (float) this.Padding.Top, finalSize.Width - (float) this.Padding.Horizontal, finalSize.Height - (float) this.Padding.Vertical);
      Padding borderThickness = this.borderPrimitive.GetBorderThickness();
      rectangleF.X += (float) borderThickness.Left;
      rectangleF.Y += (float) borderThickness.Top;
      rectangleF.Width -= (float) borderThickness.Horizontal;
      rectangleF.Height -= (float) borderThickness.Vertical;
      rectangleF.Width = Math.Max(0.0f, rectangleF.Width);
      rectangleF.Height = Math.Max(0.0f, rectangleF.Height);
      return rectangleF;
    }

    public void Toggle()
    {
      this.Toggle(this.AllowAnimation);
    }

    public void Toggle(bool animate)
    {
      this.IsAnimating = this.AllowAnimation;
      this.Value = !this.Value;
      if (animate)
        this.ProcessAnimation();
      else
        this.ThumbOffset = this.Value ? this.ControlBoundingRectangle.Width - this.ThumbTickness : 0;
    }

    public void SetToggleState(bool newValue)
    {
      this.SetToggleState(newValue, this.AllowAnimation);
    }

    public void SetToggleState(bool newValue, bool animate)
    {
      this.IsAnimating = this.AllowAnimation;
      this.Value = newValue;
      if (!animate)
        return;
      this.ProcessAnimation();
    }

    protected virtual void ProcessAnimation()
    {
      Decimal animationFrames = (Decimal) this.AnimationFrames;
      int num1 = this.ControlBoundingRectangle.Width - this.ThumbTickness;
      int num2;
      int num3;
      if (this.Value)
      {
        num2 = 0;
        num3 = num1;
      }
      else
      {
        num2 = num1;
        num3 = 0;
      }
      if (this.IsAnimating || this.isDraggingMouse)
      {
        num2 = this.ThumbOffset;
        if (this.Value)
          animationFrames *= (Decimal) (num1 - this.ThumbOffset) / (Decimal) num1;
        else
          animationFrames *= (Decimal) this.ThumbOffset / (Decimal) num1;
      }
      this.IsAnimating = true;
      this.currentAnimation.Property = RadToggleSwitchElement.ThumbOffsetProperty;
      this.currentAnimation.RemoveAfterApply = true;
      this.currentAnimation.StartValue = (object) num2;
      this.currentAnimation.EndValue = (object) num3;
      this.currentAnimation.NumFrames = (int) animationFrames;
      this.currentAnimation.Interval = this.AnimationInterval;
      this.currentAnimation.ApplyValue((RadObject) this);
    }

    public void CancelAnimation()
    {
      if (this.currentAnimation == null)
        return;
      int? currentValue = this.currentAnimation.GetCurrentValue((RadObject) this) as int?;
      this.currentAnimation.Cancel((RadObject) this);
      if (!currentValue.HasValue)
        return;
      this.ThumbOffset = currentValue.Value;
    }

    private void Animation_AnimationStarted(object sender, AnimationStatusEventArgs e)
    {
      this.OnAnimationStarted(e);
    }

    private void Animation_AnimationFinished(object sender, AnimationStatusEventArgs e)
    {
      this.currentAnimation.Stop((RadObject) this);
      this.IsAnimating = false;
      this.UpdateElementsMouseOver();
      this.OnAnimationFinished(e);
    }

    private void UpdateElementsMouseOver()
    {
      this.OnElement.UpdateContainsMouse();
      this.OffElement.UpdateContainsMouse();
      this.Thumb.UpdateContainsMouse();
      this.OnElement.IsMouseOver = this.OnElement.ContainsMouse;
      this.OffElement.IsMouseOver = this.OffElement.ContainsMouse;
      this.Thumb.IsMouseOver = this.Thumb.ContainsMouse;
    }

    private void RadToggleSwitchElement_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.ToggleStateMode == ToggleStateMode.None)
        return;
      if (this.AllowAnimation)
        this.CancelAnimation();
      this.Capture = true;
      this.IsAnimating = true;
      this.mouseDownXcoordinate = e.X;
      if (!this.RightToLeft)
        this.mouseOffset = e.X - this.ThumbOffset;
      else
        this.mouseOffset = e.X + this.ThumbOffset;
    }

    private void RadToggleSwitchElement_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.ToggleStateMode == ToggleStateMode.None || this.ToggleStateMode == ToggleStateMode.Click || !this.Capture)
        return;
      this.ThumbOffset = this.RightToLeft ? this.mouseOffset - e.X : e.X - this.mouseOffset;
      this.isDraggingMouse = true;
      this.InvalidateMeasure();
    }

    private void RadToggleSwitchElement_MouseUp(object sender, MouseEventArgs e)
    {
      this.Capture = false;
      int num1 = (int) this.OnElement.ResetValue(RadElement.IsMouseDownProperty, ValueResetFlags.Local);
      int num2 = (int) this.OffElement.ResetValue(RadElement.IsMouseDownProperty, ValueResetFlags.Local);
      int num3 = (int) this.Thumb.ResetValue(RadElement.IsMouseDownProperty, ValueResetFlags.Local);
      if (this.ToggleStateMode == ToggleStateMode.None)
        return;
      if (this.ToggleStateMode == ToggleStateMode.Drag)
      {
        int width = SystemInformation.DragSize.Width;
        if (e.X > this.mouseDownXcoordinate - width && e.X < this.mouseDownXcoordinate + width)
          return;
      }
      else if (this.ToggleStateMode == ToggleStateMode.Click)
      {
        int width = SystemInformation.DragSize.Width;
        if (e.X < this.mouseDownXcoordinate - width || e.X > this.mouseDownXcoordinate + width)
          return;
      }
      bool flag = false;
      if (!this.isDraggingMouse)
      {
        this.Toggle(this.AllowAnimation);
        flag = true;
      }
      else
      {
        int num4 = this.ControlBoundingRectangle.Width - this.ThumbTickness;
        if (!this.Value)
        {
          if ((double) this.ThumbOffset >= (double) num4 * this.SwitchElasticity)
          {
            this.Value = true;
            if (!this.AllowAnimation)
              this.ThumbOffset = num4;
          }
          else if (!this.AllowAnimation)
          {
            this.Value = false;
            this.ThumbOffset = 0;
          }
        }
        else if ((double) this.ThumbOffset <= (double) num4 - (double) num4 * this.SwitchElasticity)
        {
          this.Value = false;
          if (!this.AllowAnimation)
            this.ThumbOffset = 0;
        }
        else if (!this.AllowAnimation)
        {
          this.Value = true;
          this.ThumbOffset = num4;
        }
        this.isDraggingMouse = false;
      }
      if (!this.AllowAnimation || flag)
        return;
      this.ProcessAnimation();
    }

    private void RadToggleSwitchElement_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.ReadOnly)
        return;
      if (e.KeyCode == Keys.Space)
      {
        this.Toggle(this.AllowAnimation);
      }
      else
      {
        bool newValue;
        if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Right)
        {
          newValue = true;
        }
        else
        {
          if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Left)
            return;
          newValue = false;
        }
        if (this.RightToLeft)
          newValue = !newValue;
        this.SetToggleState(newValue, this.AllowAnimation);
      }
    }

    protected virtual void OnValueChanging(ValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    protected virtual void OnValueChanged()
    {
      if (this.ElementTree != null && this.ElementTree.Control != null)
      {
        RadControl control = this.ElementTree.Control as RadControl;
        string traceEvent = this.Value ? "Checked" : "Unchecked";
        ControlTraceMonitor.TrackAtomicFeature(control, traceEvent, (object) control.Name);
      }
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    protected virtual void OnAnimationStarted(AnimationStatusEventArgs e)
    {
      if (this.AnimationStarted == null)
        return;
      this.AnimationStarted((object) this, e);
    }

    protected virtual void OnAnimationFinished(AnimationStatusEventArgs e)
    {
      if (this.AnimationFinished == null)
        return;
      this.AnimationFinished((object) this, e);
    }

    protected override void PaintBorder(IGraphics graphics, float angle, SizeF scale)
    {
    }
  }
}
