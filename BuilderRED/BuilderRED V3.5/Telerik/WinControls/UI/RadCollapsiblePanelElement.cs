// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCollapsiblePanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadCollapsiblePanelElement : LightVisualElement
  {
    private bool enableAnimation = true;
    protected static readonly RadProperty DefaultAnimatedProperty = RadElement.BoundsProperty;
    protected static readonly RadProperty SlideAnimationAnimatedProperty = RadElement.PositionOffsetProperty;
    public static readonly RadProperty ExpandDirectionProperty = RadProperty.Register(nameof (ExpandDirection), typeof (RadDirection), typeof (RadCollapsiblePanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDirection.Down, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty ContentSizingModeProperty = RadProperty.Register(nameof (ContentSizingMode), typeof (CollapsiblePanelContentSizingMode), typeof (RadCollapsiblePanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) CollapsiblePanelContentSizingMode.None, ElementPropertyOptions.AffectsLayout));
    public static readonly RadProperty AnimationIntervalProperty = RadProperty.Register(nameof (AnimationInterval), typeof (int), typeof (RadCollapsiblePanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 30));
    public static readonly RadProperty AnimationFramesProperty = RadProperty.Register(nameof (AnimationFrames), typeof (int), typeof (RadCollapsiblePanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 15));
    public static readonly RadProperty IsExpandedProperty = RadProperty.Register(nameof (IsExpanded), typeof (bool), typeof (RadCollapsiblePanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout) { Cancelable = true });
    public static readonly RadProperty AnimationEasingTypeProperty = RadProperty.Register(nameof (AnimationEasingType), typeof (RadEasingType), typeof (RadCollapsiblePanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadEasingType.Default));
    public static readonly RadProperty AnimationTypeProperty = RadProperty.Register(nameof (AnimationType), typeof (CollapsiblePanelAnimationType), typeof (RadCollapsiblePanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) CollapsiblePanelAnimationType.Reveal));
    private static readonly string NotSupportedEnumFormatString = "The provided animation type is not supported - {0}";
    private CollapsiblePanelHeaderElement headerElement;
    private CollapsiblePanelLayoutElement layoutElement;
    private bool isAnimating;
    private readonly RadCollapsiblePanel ownerControl;
    private Rectangle ownerBoundsCache;
    private bool suspendLayoutElementSynchronization;
    private bool isAnimatingFromMethod;
    private bool preventExpandCollapse;
    private ScrollState savedVerticalScrollState;
    private ScrollState savedHorizontalScrollState;

    public CollapsiblePanelHeaderElement HeaderElement
    {
      get
      {
        return this.headerElement;
      }
    }

    public RadDirection ExpandDirection
    {
      get
      {
        return (RadDirection) this.GetValue(RadCollapsiblePanelElement.ExpandDirectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCollapsiblePanelElement.ExpandDirectionProperty, (object) value);
      }
    }

    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(RadCollapsiblePanelElement.IsExpandedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCollapsiblePanelElement.IsExpandedProperty, (object) value);
      }
    }

    public bool EnableAnimation
    {
      get
      {
        return this.enableAnimation;
      }
      set
      {
        this.enableAnimation = value;
      }
    }

    public CollapsiblePanelContentSizingMode ContentSizingMode
    {
      get
      {
        return (CollapsiblePanelContentSizingMode) this.GetValue(RadCollapsiblePanelElement.ContentSizingModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCollapsiblePanelElement.ContentSizingModeProperty, (object) value);
      }
    }

    public bool IsAnimating
    {
      get
      {
        return this.isAnimating;
      }
      protected set
      {
        if (value && !this.EnableAnimation)
          return;
        this.isAnimating = value;
      }
    }

    public int AnimationInterval
    {
      get
      {
        return (int) this.GetValue(RadCollapsiblePanelElement.AnimationIntervalProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCollapsiblePanelElement.AnimationIntervalProperty, (object) value);
      }
    }

    public int AnimationFrames
    {
      get
      {
        return (int) this.GetValue(RadCollapsiblePanelElement.AnimationFramesProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCollapsiblePanelElement.AnimationFramesProperty, (object) value);
      }
    }

    public bool ShowHeaderLine
    {
      get
      {
        return this.HeaderElement.ShowHeaderLine;
      }
      set
      {
        this.HeaderElement.ShowHeaderLine = value;
      }
    }

    public RadHorizontalAlignment HorizontalHeaderAlignment
    {
      get
      {
        return this.HeaderElement.HorizontalHeaderAlignment;
      }
      set
      {
        this.HeaderElement.HorizontalHeaderAlignment = value;
      }
    }

    public RadVerticalAlignment VerticalHeaderAlignment
    {
      get
      {
        return this.HeaderElement.VerticalHeaderAlignment;
      }
      set
      {
        this.HeaderElement.VerticalHeaderAlignment = value;
      }
    }

    public CollapsiblePanelLayoutElement LayoutElement
    {
      get
      {
        return this.layoutElement;
      }
    }

    public string HeaderText
    {
      get
      {
        return this.HeaderElement.HeaderTextElement.Text;
      }
      set
      {
        this.HeaderElement.HeaderTextElement.Text = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Rectangle OwnerBoundsCache
    {
      get
      {
        return this.ownerBoundsCache;
      }
      set
      {
        this.ownerBoundsCache = value;
      }
    }

    public RadEasingType AnimationEasingType
    {
      get
      {
        return (RadEasingType) this.GetValue(RadCollapsiblePanelElement.AnimationEasingTypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCollapsiblePanelElement.AnimationEasingTypeProperty, (object) value);
      }
    }

    public CollapsiblePanelAnimationType AnimationType
    {
      get
      {
        return (CollapsiblePanelAnimationType) this.GetValue(RadCollapsiblePanelElement.AnimationTypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadCollapsiblePanelElement.AnimationTypeProperty, (object) value);
      }
    }

    static RadCollapsiblePanelElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadCollapsiblePanelElementStateManagerFactory(RadCollapsiblePanelElement.ExpandDirectionProperty, RadCollapsiblePanelElement.IsExpandedProperty), typeof (RadCollapsiblePanelElement));
    }

    public RadCollapsiblePanelElement(RadCollapsiblePanel ownerControl)
    {
      this.ownerControl = ownerControl;
    }

    protected override void CreateChildElements()
    {
      this.headerElement = this.CreateHeaderElement();
      this.headerElement.HeaderButtonElement.Click += new EventHandler(this.OnHeaderButtonClick);
      this.layoutElement = this.CreateLayoutElement();
      int num1 = (int) this.HeaderElement.BindProperty(CollapsiblePanelHeaderElement.ExpandDirectionProperty, (RadObject) this, RadCollapsiblePanelElement.ExpandDirectionProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.HeaderElement.BindProperty(CollapsiblePanelHeaderElement.IsExpandedProperty, (RadObject) this, RadCollapsiblePanelElement.IsExpandedProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.LayoutElement.BindProperty(CollapsiblePanelLayoutElement.ExpandDirectionProperty, (RadObject) this, RadCollapsiblePanelElement.ExpandDirectionProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.LayoutElement.BindProperty(CollapsiblePanelLayoutElement.IsExpandedProperty, (RadObject) this, RadCollapsiblePanelElement.IsExpandedProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.HeaderElement.HeaderButtonElement.BindProperty(CollapsiblePanelButtonElement.ExpandDirectionProperty, (RadObject) this, RadCollapsiblePanelElement.ExpandDirectionProperty, PropertyBindingOptions.TwoWay);
      int num6 = (int) this.HeaderElement.HeaderButtonElement.BindProperty(CollapsiblePanelButtonElement.IsExpandedProperty, (RadObject) this, RadCollapsiblePanelElement.IsExpandedProperty, PropertyBindingOptions.TwoWay);
      this.Children.Add((RadElement) this.layoutElement);
      this.Children.Add((RadElement) this.headerElement);
    }

    protected virtual CollapsiblePanelHeaderElement CreateHeaderElement()
    {
      return new CollapsiblePanelHeaderElement();
    }

    protected virtual CollapsiblePanelLayoutElement CreateLayoutElement()
    {
      return new CollapsiblePanelLayoutElement();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      float num1 = (float) this.ownerControl.Height;
      float num2 = (float) this.ownerControl.Width;
      RadDirection expandDirection = this.ExpandDirection;
      foreach (RadElement child in this.Children)
      {
        child.Measure(availableSize);
        switch (expandDirection)
        {
          case RadDirection.Left:
          case RadDirection.Right:
            num2 = Math.Max(child.DesiredSize.Width, num2);
            if ((double) availableSize.Height != double.PositiveInfinity)
            {
              num1 = availableSize.Height;
              continue;
            }
            continue;
          case RadDirection.Up:
          case RadDirection.Down:
            num1 = Math.Max(child.DesiredSize.Height, num1);
            if ((double) availableSize.Width != double.PositiveInfinity)
            {
              num2 = availableSize.Width;
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      return new SizeF(num2, num1);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      PointF empty1 = PointF.Empty;
      SizeF empty2 = SizeF.Empty;
      PointF fillElementPoint = PointF.Empty;
      SizeF fillElementSize = SizeF.Empty;
      this.ArrangeAccordingToDirection(finalSize, ref empty2, ref fillElementPoint, ref fillElementSize, ref empty1);
      this.HeaderElement.Arrange(new RectangleF(empty1, empty2));
      if (this.IsAnimating && !this.isAnimatingFromMethod)
        return finalSize;
      if (this.ContentSizingMode != CollapsiblePanelContentSizingMode.None)
      {
        fillElementSize = this.GetNewElementSizeAndAdjustChildControlsAccordingToSizingMode(fillElementSize);
        fillElementPoint = this.GetFillElementPointAccordingToSizingMode(fillElementPoint, fillElementSize, finalSize, empty2);
      }
      RectangleF finalRect = new RectangleF(fillElementPoint, fillElementSize);
      if ((this.ContentSizingMode & CollapsiblePanelContentSizingMode.FitToContentHeight) == CollapsiblePanelContentSizingMode.FitToContentHeight)
      {
        if (this.ExpandDirection == RadDirection.Down)
          finalRect.Height += (float) this.LayoutElement.Margin.Top;
        else if (this.ExpandDirection == RadDirection.Up)
        {
          finalRect.Height += (float) this.LayoutElement.Margin.Bottom;
          finalRect.Y -= (float) this.LayoutElement.Margin.Bottom;
        }
      }
      if ((this.ContentSizingMode & CollapsiblePanelContentSizingMode.FitToContentWidth) == CollapsiblePanelContentSizingMode.FitToContentWidth)
      {
        if (this.ExpandDirection == RadDirection.Right)
          finalRect.Width += (float) this.LayoutElement.Margin.Left;
        else if (this.ExpandDirection == RadDirection.Left)
        {
          finalRect.Width += (float) this.LayoutElement.Margin.Right;
          finalRect.X -= (float) this.LayoutElement.Margin.Right;
        }
      }
      this.LayoutElement.Arrange(finalRect);
      if (this.IsExpanded && !this.suspendLayoutElementSynchronization)
        this.ownerControl.ControlsContainer.SynchronizeWithElement((RadElement) this.LayoutElement);
      return finalSize;
    }

    private PointF GetFillElementPointAccordingToSizingMode(
      PointF fillElementPoint,
      SizeF fillElementSize,
      SizeF finalSize,
      SizeF headerElementSize)
    {
      if ((this.ContentSizingMode & CollapsiblePanelContentSizingMode.FitToContentWidth) == CollapsiblePanelContentSizingMode.FitToContentWidth && this.ExpandDirection == RadDirection.Left)
        fillElementPoint.X += finalSize.Width - fillElementSize.Width - headerElementSize.Width;
      if ((this.ContentSizingMode & CollapsiblePanelContentSizingMode.FitToContentHeight) == CollapsiblePanelContentSizingMode.FitToContentHeight && this.ExpandDirection == RadDirection.Up)
        fillElementPoint.Y += finalSize.Height - fillElementSize.Height - headerElementSize.Height;
      return fillElementPoint;
    }

    private void ArrangeAccordingToDirection(
      SizeF finalSize,
      ref SizeF headerElementSize,
      ref PointF fillElementPoint,
      ref SizeF fillElementSize,
      ref PointF headerElementPoint)
    {
      if (this.ExpandDirection == RadDirection.Down)
      {
        headerElementSize = new SizeF(finalSize.Width, this.HeaderElement.DesiredSize.Height);
        fillElementPoint = new PointF(0.0f, this.HeaderElement.DesiredSize.Height);
        fillElementSize = new SizeF(finalSize.Width, finalSize.Height - this.HeaderElement.DesiredSize.Height);
      }
      else if (this.ExpandDirection == RadDirection.Up)
      {
        headerElementPoint = new PointF(0.0f, finalSize.Height - this.HeaderElement.DesiredSize.Height);
        headerElementSize = new SizeF(finalSize.Width, this.HeaderElement.DesiredSize.Height);
        fillElementSize = new SizeF(finalSize.Width, finalSize.Height - this.HeaderElement.DesiredSize.Height);
      }
      else if (this.ExpandDirection == RadDirection.Left)
      {
        headerElementPoint = new PointF(finalSize.Width - this.HeaderElement.DesiredSize.Width, 0.0f);
        headerElementSize = new SizeF(this.HeaderElement.DesiredSize.Width, finalSize.Height);
        fillElementSize = new SizeF(finalSize.Width - this.HeaderElement.DesiredSize.Width, finalSize.Height);
      }
      else
      {
        if (this.ExpandDirection != RadDirection.Right)
          return;
        headerElementSize = new SizeF(this.HeaderElement.DesiredSize.Width, finalSize.Height);
        fillElementPoint = new PointF(this.HeaderElement.DesiredSize.Width, 0.0f);
        fillElementSize = new SizeF(finalSize.Width - this.HeaderElement.DesiredSize.Width, finalSize.Height);
      }
    }

    private SizeF GetNewElementSizeAndAdjustChildControlsAccordingToSizingMode(
      SizeF originalSize)
    {
      SizeF sizeF = originalSize;
      bool flag1 = (this.ContentSizingMode & CollapsiblePanelContentSizingMode.FitToContentHeight) == CollapsiblePanelContentSizingMode.FitToContentHeight;
      bool flag2 = (this.ContentSizingMode & CollapsiblePanelContentSizingMode.FitToContentWidth) == CollapsiblePanelContentSizingMode.FitToContentWidth;
      if (flag1 || flag2)
      {
        int val1_1 = int.MinValue;
        int val1_2 = int.MinValue;
        int val1_3 = int.MinValue;
        int val1_4 = int.MinValue;
        Padding padding = this.ownerControl.ControlsContainer.PanelContainer.Padding;
        foreach (Control control in (ArrangedElementCollection) this.ownerControl.ControlsContainer.PanelContainer.Controls)
        {
          if (flag1)
          {
            val1_1 = Math.Max(val1_1, control.Bounds.Location.Y + control.Bounds.Size.Height + control.Margin.Bottom + padding.Bottom + this.ownerControl.ControlsContainer.VerticalScrollbar.Value);
            val1_3 = Math.Max(val1_3, control.Width);
          }
          if (flag2)
          {
            val1_2 = Math.Max(val1_2, control.Bounds.Location.X + control.Bounds.Size.Width + control.Margin.Right + padding.Right + this.ownerControl.ControlsContainer.HorizontalScrollbar.Value);
            val1_4 = Math.Max(val1_4, control.Height);
          }
        }
        if (flag1)
        {
          if ((double) sizeF.Width <= (double) (val1_1 + val1_3))
            this.ownerControl.ControlsContainer.HorizontalScrollBarState = ScrollState.AlwaysHide;
          else
            this.ownerControl.ControlsContainer.HorizontalScrollBarState = ScrollState.AutoHide;
          sizeF.Height = (float) val1_1;
        }
        if (flag2)
        {
          if ((double) sizeF.Height <= (double) (val1_2 + val1_4))
            this.ownerControl.ControlsContainer.VerticalScrollBarState = ScrollState.AlwaysHide;
          else
            this.ownerControl.ControlsContainer.VerticalScrollBarState = ScrollState.AutoHide;
          sizeF.Width = (float) val1_2;
        }
      }
      return sizeF;
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      base.OnPropertyChanging(args);
      if (args.Property != RadCollapsiblePanelElement.IsExpandedProperty)
        return;
      bool newValue = (bool) args.NewValue;
      bool stopAnimations = this.ElementTree.Control.Site != null;
      if (newValue)
        this.Expand(true, stopAnimations);
      else
        this.Collapse(true, stopAnimations);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      bool flag = e.Property == RadCollapsiblePanelElement.ExpandDirectionProperty;
      if (flag)
      {
        switch ((RadDirection) e.NewValue)
        {
          case RadDirection.Left:
          case RadDirection.Right:
            this.HeaderElement.Orientation = Orientation.Vertical;
            break;
          case RadDirection.Up:
          case RadDirection.Down:
            this.HeaderElement.Orientation = Orientation.Horizontal;
            break;
        }
        if (!this.IsExpanded)
          this.SetOwnerControlBounds(new Point?(), new Size?());
      }
      if (!flag && e.Property != RadElement.RightToLeftProperty)
        return;
      this.HeaderElement.FlipText = this.RightToLeft && this.ExpandDirectionIsHorizontal();
    }

    protected virtual void OnHeaderButtonClick(object sender, EventArgs e)
    {
      this.ToggleExpandCollapse();
    }

    public virtual void Expand()
    {
      this.Expand(false, false);
    }

    public virtual void Expand(bool ignoreIsExpanded, bool stopAnimations)
    {
      this.ownerControl.LoadElementTree();
      bool isExpanded = !ignoreIsExpanded && this.IsExpanded;
      if (this.IsAnimating || isExpanded || (this.ElementTree.RootElement.ElementState != ElementState.Loaded || this.preventExpandCollapse) || !this.OnExpanding())
        return;
      this.ExecuteExpandPreparations();
      bool previousIsAnimatingValue = false;
      if (stopAnimations)
      {
        previousIsAnimatingValue = this.EnableAnimation;
        this.EnableAnimation = false;
      }
      AnimatedPropertySetting animation = this.CreateAnimation(isExpanded);
      AnimationFinishedEventHandler animationFinishedHandler = (AnimationFinishedEventHandler) null;
      animationFinishedHandler = (AnimationFinishedEventHandler) ((param0, param1) =>
      {
        this.preventExpandCollapse = true;
        animation.AnimationFinished -= animationFinishedHandler;
        this.IsExpanded = true;
        if (stopAnimations)
          this.EnableAnimation = previousIsAnimatingValue;
        this.preventExpandCollapse = false;
        this.ExecuteExpandFinalizations();
        int num = (int) this.SetAnimatedObjectValue(animation.Property, animation.EndValue);
        this.OnExpanded();
      });
      animation.AnimationFinished += animationFinishedHandler;
      RadObject objectToBeAnimated = this.GetObjectToBeAnimated();
      if (this.EnableAnimation)
      {
        animation.ApplyValue(objectToBeAnimated);
      }
      else
      {
        int num = (int) this.SetAnimatedObjectValue(animation.Property, animation.EndValue);
        animationFinishedHandler((object) animation, new AnimationStatusEventArgs(objectToBeAnimated));
      }
      if (!this.ElementTree.RootElement.EnableElementShadow)
        return;
      this.ownerControl.RootElement.PaintControlShadow();
    }

    public virtual void Collapse()
    {
      this.Collapse(false, false);
    }

    public virtual void Collapse(bool ignoreIsExpanded, bool stopAnimations)
    {
      this.ownerControl.LoadElementTree();
      bool isExpanded = ignoreIsExpanded || this.IsExpanded;
      if (this.IsAnimating || !isExpanded || (this.ElementTree.RootElement.ElementState != ElementState.Loaded || this.preventExpandCollapse) || !this.OnCollapsing())
        return;
      this.ExecuteCollapsePreparations();
      bool previousIsAnimatingValue = false;
      if (stopAnimations)
      {
        previousIsAnimatingValue = this.EnableAnimation;
        this.EnableAnimation = false;
      }
      AnimatedPropertySetting animation = this.CreateAnimation(isExpanded);
      AnimationFinishedEventHandler animationFinishedHandler = (AnimationFinishedEventHandler) null;
      animationFinishedHandler = (AnimationFinishedEventHandler) ((param0, param1) =>
      {
        this.preventExpandCollapse = true;
        animation.AnimationFinished -= animationFinishedHandler;
        this.IsExpanded = false;
        if (stopAnimations)
          this.EnableAnimation = previousIsAnimatingValue;
        this.preventExpandCollapse = false;
        this.ExecuteCollapseFinalizations();
        int num = (int) this.SetAnimatedObjectValue(animation.Property, animation.EndValue);
        this.OnCollapsed();
      });
      animation.AnimationFinished += animationFinishedHandler;
      RadObject objectToBeAnimated = this.GetObjectToBeAnimated();
      if (this.EnableAnimation)
      {
        animation.ApplyValue(objectToBeAnimated);
      }
      else
      {
        bool animationsEnabled = AnimatedPropertySetting.AnimationsEnabled;
        AnimatedPropertySetting.AnimationsEnabled = false;
        animation.NumFrames = 1;
        animation.Interval = 1;
        animation.ApplyValue(objectToBeAnimated);
        AnimatedPropertySetting.AnimationsEnabled = animationsEnabled;
      }
      if (!this.ElementTree.RootElement.EnableElementShadow)
        return;
      this.ownerControl.RootElement.PaintControlShadow();
    }

    public virtual void ToggleExpandCollapse()
    {
      if (!this.IsExpanded)
        this.Expand();
      else
        this.Collapse();
    }

    public virtual void ToggleExpandCollapse(bool ignoreIsExpanded, bool stopAnimations)
    {
      if (!this.IsExpanded)
        this.Expand(ignoreIsExpanded, stopAnimations);
      else
        this.Collapse(ignoreIsExpanded, stopAnimations);
    }

    protected virtual AnimatedPropertySetting CreateAnimation()
    {
      return this.CreateAnimation(this.IsExpanded);
    }

    protected virtual AnimatedPropertySetting CreateAnimation(bool isExpanded)
    {
      AnimatedPropertySetting animatedPropertySetting = !isExpanded ? this.CreateExpandAnimation() : this.CreateCollapseAnimation();
      switch (this.AnimationType)
      {
        case CollapsiblePanelAnimationType.Reveal:
          animatedPropertySetting.Property = RadCollapsiblePanelElement.DefaultAnimatedProperty;
          break;
        case CollapsiblePanelAnimationType.Slide:
          animatedPropertySetting.Property = RadCollapsiblePanelElement.SlideAnimationAnimatedProperty;
          break;
        default:
          throw new InvalidOperationException(string.Format(RadCollapsiblePanelElement.NotSupportedEnumFormatString, (object) this.AnimationType));
      }
      animatedPropertySetting.Interval = this.AnimationInterval;
      animatedPropertySetting.NumFrames = this.AnimationFrames;
      animatedPropertySetting.ApplyEasingType = this.AnimationEasingType;
      animatedPropertySetting.RemoveAfterApply = true;
      return animatedPropertySetting;
    }

    protected virtual void SetControlBoundsAfterCollapsed()
    {
      int headerSize = this.GetHeaderSize(new RadDirection?());
      switch (this.ExpandDirection)
      {
        case RadDirection.Left:
          int width;
          int x;
          if (this.ownerControl.MinimumSize.Width == 0)
          {
            width = headerSize;
            x = this.ownerControl.Location.X + this.ownerControl.Width - width;
          }
          else
          {
            int num = this.ownerControl.Size.Width - this.ownerControl.MinimumSize.Width;
            x = this.ownerControl.Location.X + num;
            width = this.ownerControl.Size.Width - num;
          }
          if (this.ownerControl.Dock != DockStyle.None)
            x = 0;
          this.SetOwnerControlBounds(new Point?(new Point(x, this.ownerControl.Location.Y)), new Size?(new Size(width, this.ownerControl.Size.Height)));
          this.InvalidateMeasure(true);
          this.UpdateLayout();
          break;
        case RadDirection.Right:
          this.SuspendLayout();
          this.ownerControl.Width = headerSize;
          this.ResumeLayout(true);
          break;
        case RadDirection.Up:
          int height;
          int y;
          if (this.ownerControl.MinimumSize.Height == 0)
          {
            height = headerSize;
            y = this.ownerControl.Location.Y + this.ownerControl.Height - height;
          }
          else
          {
            int num = this.ownerControl.Size.Height - this.ownerControl.MinimumSize.Height;
            y = this.ownerControl.Location.Y + num;
            height = this.ownerControl.Height - num;
          }
          if (this.ownerControl.Dock != DockStyle.None)
            y = 0;
          this.SetOwnerControlBounds(new Point?(new Point(this.ownerControl.Location.X, y)), new Size?(new Size(this.ownerControl.Size.Width, height)));
          this.InvalidateMeasure(true);
          this.UpdateLayout();
          break;
        case RadDirection.Down:
          this.SuspendLayout();
          this.ownerControl.Height = headerSize;
          this.ResumeLayout(true);
          break;
      }
    }

    protected AnimatedPropertySetting CreateExpandAnimation()
    {
      AnimatedPropertySetting animation = new AnimatedPropertySetting();
      switch (this.AnimationType)
      {
        case CollapsiblePanelAnimationType.Reveal:
          this.SetupRevealExpandAnimation(animation);
          break;
        case CollapsiblePanelAnimationType.Slide:
          this.SetupSlideExpandAnimation(animation);
          break;
        default:
          throw new InvalidOperationException(string.Format(RadCollapsiblePanelElement.NotSupportedEnumFormatString, (object) this.AnimationType));
      }
      return animation;
    }

    protected void SetupRevealExpandAnimation(AnimatedPropertySetting animation)
    {
      switch (this.ExpandDirection)
      {
        case RadDirection.Left:
          animation.StartValue = (object) new Rectangle(new Point(this.ownerControl.Size.Width - this.HeaderElement.Size.Width - (this.LayoutElement.Margin.Right + this.LayoutElement.Margin.Left), this.LayoutElement.Location.Y), new Size(0, this.LayoutElement.Size.Height));
          animation.EndValue = (object) new Rectangle(new Point(this.LayoutElement.BoundingRectangle.X, this.LayoutElement.BoundingRectangle.Y), this.LayoutElement.Size);
          break;
        case RadDirection.Right:
          animation.StartValue = (object) new Rectangle(this.LayoutElement.BoundingRectangle.Location, new Size(0, this.LayoutElement.Size.Height));
          animation.EndValue = (object) new Rectangle(this.LayoutElement.BoundingRectangle.Location, this.LayoutElement.Size);
          break;
        case RadDirection.Up:
          animation.StartValue = (object) new Rectangle(new Point(this.LayoutElement.Location.X, this.ownerControl.Size.Height - this.HeaderElement.Size.Height - (this.LayoutElement.Margin.Top + this.LayoutElement.Margin.Bottom)), new Size(this.LayoutElement.Size.Width, 0));
          animation.EndValue = (object) new Rectangle(new Point(this.LayoutElement.Margin.Left - this.LayoutElement.Margin.Right, this.Size.Height - this.LayoutElement.Size.Height - this.HeaderElement.Size.Height - this.LayoutElement.Margin.Bottom), this.LayoutElement.Size);
          break;
        case RadDirection.Down:
          animation.StartValue = (object) new Rectangle(this.LayoutElement.BoundingRectangle.Location, new Size(this.LayoutElement.Size.Width, 0));
          animation.EndValue = (object) new Rectangle(this.LayoutElement.BoundingRectangle.Location, this.LayoutElement.Size);
          break;
      }
    }

    protected void SetupSlideExpandAnimation(AnimatedPropertySetting animation)
    {
      animation.EndValue = (object) SizeF.Empty;
      animation.StartValue = (object) this.LayoutElement.ImagePrimitive.PositionOffset;
    }

    protected AnimatedPropertySetting CreateCollapseAnimation()
    {
      AnimatedPropertySetting animation = new AnimatedPropertySetting();
      if (this.AnimationType == CollapsiblePanelAnimationType.Reveal)
        this.SetupRevealCollapseAnimation(animation);
      else if (this.AnimationType == CollapsiblePanelAnimationType.Slide)
        this.SetupSlideCollapseAnimation(animation);
      return animation;
    }

    protected void SetupRevealCollapseAnimation(AnimatedPropertySetting animation)
    {
      switch (this.ExpandDirection)
      {
        case RadDirection.Left:
          animation.StartValue = (object) new Rectangle(this.ownerControl.ControlsContainer.Location, this.ownerControl.ControlsContainer.Size);
          animation.EndValue = (object) new Rectangle(new Point(this.ownerControl.ControlsContainer.Location.X + this.ownerControl.ControlsContainer.Size.Width, 0), new Size(0, this.ownerControl.ControlsContainer.Size.Height));
          break;
        case RadDirection.Right:
          animation.StartValue = (object) new Rectangle(this.ownerControl.ControlsContainer.Location, this.ownerControl.ControlsContainer.Size);
          animation.EndValue = (object) new Rectangle(new Point(this.ownerControl.ControlsContainer.Location.X, 0), new Size(0, this.ownerControl.ControlsContainer.Size.Height));
          break;
        case RadDirection.Up:
          animation.StartValue = (object) new Rectangle(this.ownerControl.ControlsContainer.Location, this.ownerControl.ControlsContainer.Size);
          animation.EndValue = (object) new Rectangle(new Point(0, this.ownerControl.ControlsContainer.Location.Y + this.ownerControl.ControlsContainer.Size.Height), new Size(this.ownerControl.ControlsContainer.Size.Width, 0));
          break;
        case RadDirection.Down:
          animation.StartValue = (object) new Rectangle(this.ownerControl.ControlsContainer.Location, this.ownerControl.ControlsContainer.Size);
          animation.EndValue = (object) new Rectangle(this.ownerControl.ControlsContainer.Location, new Size(this.ownerControl.ControlsContainer.Size.Width, 0));
          break;
      }
    }

    protected void SetupSlideCollapseAnimation(AnimatedPropertySetting animation)
    {
      animation.StartValue = (object) SizeF.Empty;
      switch (this.ExpandDirection)
      {
        case RadDirection.Left:
          animation.EndValue = (object) new SizeF((float) (this.ownerControl.Width - this.LayoutElement.Margin.Left - this.HeaderElement.Size.Width), 0.0f);
          break;
        case RadDirection.Right:
          animation.EndValue = (object) new SizeF((float) -(this.ownerControl.Width - this.LayoutElement.Margin.Left - this.HeaderElement.Size.Width), 0.0f);
          break;
        case RadDirection.Up:
          animation.EndValue = (object) new SizeF(0.0f, (float) (this.ownerControl.Height - this.LayoutElement.Margin.Bottom - this.HeaderElement.Size.Height));
          break;
        case RadDirection.Down:
          animation.EndValue = (object) new SizeF(0.0f, (float) -(this.ownerControl.Height - this.LayoutElement.Margin.Top - this.HeaderElement.Size.Height));
          break;
      }
    }

    protected virtual void ExecuteCollapsePreparations()
    {
      this.IsAnimating = true;
      this.isAnimatingFromMethod = true;
      CollapsiblePanelControlsContainer controlsContainer = this.ownerControl.ControlsContainer;
      if (!this.ExpandDirectionIsHorizontal())
      {
        if (!controlsContainer.VerticalScrollbar.Visible)
        {
          this.savedVerticalScrollState = controlsContainer.VerticalScrollBarState;
          controlsContainer.VerticalScrollBarState = ScrollState.AlwaysHide;
        }
      }
      else if (!controlsContainer.HorizontalScrollbar.Visible)
      {
        this.savedHorizontalScrollState = controlsContainer.HorizontalScrollBarState;
        controlsContainer.HorizontalScrollBarState = ScrollState.AlwaysHide;
      }
      switch (this.AnimationType)
      {
        case CollapsiblePanelAnimationType.Reveal:
          this.ownerControl.ControlsContainer.SuspendChildControlsLayout();
          break;
        case CollapsiblePanelAnimationType.Slide:
          if (this.ownerControl.ControlsContainer.Width > 0 && this.ownerControl.ControlsContainer.Height > 0)
            this.LayoutElement.Image = (Image) this.ownerControl.ControlsContainer.ToBitmap();
          this.suspendLayoutElementSynchronization = true;
          if (!this.ExpandDirectionIsHorizontal())
          {
            this.ownerControl.ControlsContainer.Height = 0;
            break;
          }
          this.ownerControl.ControlsContainer.Width = 0;
          break;
        default:
          throw new InvalidOperationException(string.Format(RadCollapsiblePanelElement.NotSupportedEnumFormatString, (object) this.AnimationType));
      }
    }

    protected virtual void ExecuteCollapseFinalizations()
    {
      this.SaveOwnerControlBounds();
      this.SetControlBoundsAfterCollapsed();
      this.IsAnimating = false;
      this.isAnimatingFromMethod = false;
      this.suspendLayoutElementSynchronization = false;
      switch (this.AnimationType)
      {
        case CollapsiblePanelAnimationType.Reveal:
          this.ownerControl.ControlsContainer.ResumeChildControlsLayout();
          break;
        case CollapsiblePanelAnimationType.Slide:
          break;
        default:
          throw new InvalidOperationException(string.Format(RadCollapsiblePanelElement.NotSupportedEnumFormatString, (object) this.AnimationType));
      }
    }

    protected virtual void ExecuteExpandPreparations()
    {
      this.IsAnimating = true;
      this.isAnimatingFromMethod = true;
      Point? location = new Point?();
      Size? size = new Size?();
      if (this.ExpandDirection == RadDirection.Left)
      {
        location = new Point?(new Point(this.OwnerBoundsCache.Location.X - this.OwnerBoundsCache.Width + this.GetHeaderSize(new RadDirection?()), this.OwnerBoundsCache.Y));
        int num = this.ownerControl.Width - this.GetHeaderSize(new RadDirection?());
        if (num > 0)
          size = new Size?(new Size(this.OwnerBoundsCache.Width + num, this.OwnerBoundsCache.Height));
      }
      else if (this.ExpandDirection == RadDirection.Up)
      {
        location = new Point?(new Point(this.OwnerBoundsCache.X, this.OwnerBoundsCache.Location.Y - this.OwnerBoundsCache.Height + this.GetHeaderSize(new RadDirection?())));
        int num = this.ownerControl.Height - this.GetHeaderSize(new RadDirection?());
        if (num > 0)
          size = new Size?(new Size(this.OwnerBoundsCache.Width, this.OwnerBoundsCache.Height + num));
      }
      this.SetOwnerControlBounds(location, size);
      switch (this.AnimationType)
      {
        case CollapsiblePanelAnimationType.Reveal:
          this.ownerControl.ControlsContainer.SuspendChildControlsLayout();
          break;
        case CollapsiblePanelAnimationType.Slide:
          break;
        default:
          throw new InvalidOperationException(string.Format(RadCollapsiblePanelElement.NotSupportedEnumFormatString, (object) this.AnimationType));
      }
    }

    protected virtual void ExecuteExpandFinalizations()
    {
      this.IsAnimating = false;
      this.isAnimatingFromMethod = false;
      CollapsiblePanelControlsContainer controlsContainer = this.ownerControl.ControlsContainer;
      if (!this.ExpandDirectionIsHorizontal())
        controlsContainer.VerticalScrollBarState = this.savedVerticalScrollState;
      else
        controlsContainer.HorizontalScrollBarState = this.savedHorizontalScrollState;
      switch (this.AnimationType)
      {
        case CollapsiblePanelAnimationType.Reveal:
          this.ownerControl.ControlsContainer.ResumeChildControlsLayout(true);
          break;
        case CollapsiblePanelAnimationType.Slide:
          if (this.LayoutElement.Image == null)
            break;
          this.LayoutElement.Image.Dispose();
          this.LayoutElement.Image = (Image) null;
          break;
        default:
          throw new InvalidOperationException(string.Format(RadCollapsiblePanelElement.NotSupportedEnumFormatString, (object) this.AnimationType));
      }
    }

    protected virtual RadObject GetObjectToBeAnimated()
    {
      switch (this.AnimationType)
      {
        case CollapsiblePanelAnimationType.Reveal:
          return (RadObject) this.ownerControl.ControlsContainer.RootElement;
        case CollapsiblePanelAnimationType.Slide:
          return (RadObject) this.LayoutElement.ImagePrimitive;
        default:
          throw new InvalidOperationException(string.Format(RadCollapsiblePanelElement.NotSupportedEnumFormatString, (object) this.AnimationType));
      }
    }

    protected virtual void SetOwnerControlBounds(Point? location = null, Size? size = null)
    {
      if (!location.HasValue)
        location = new Point?(this.OwnerBoundsCache.Location);
      if (!size.HasValue)
        size = new Size?(this.OwnerBoundsCache.Size);
      this.ownerControl.Location = location.Value;
      if ((this.ExpandDirection == RadDirection.Up || this.ExpandDirection == RadDirection.Down) && (this.ownerControl.Dock == DockStyle.Left || this.ownerControl.Dock == DockStyle.Right || this.ownerControl.Dock == DockStyle.Fill) || (this.ExpandDirection == RadDirection.Left || this.ExpandDirection == RadDirection.Right) && (this.ownerControl.Dock == DockStyle.Top || this.ownerControl.Dock == DockStyle.Bottom || this.ownerControl.Dock == DockStyle.Fill) || this.ownerControl.Parent is RadLayoutControl)
        return;
      this.ownerControl.Size = size.Value;
    }

    protected virtual void SaveOwnerControlBounds()
    {
      this.OwnerBoundsCache = new Rectangle(this.ownerControl.Location, this.ownerControl.Size);
    }

    private ValueUpdateResult SetAnimatedObjectValue(
      RadProperty prop,
      object value)
    {
      return this.GetObjectToBeAnimated().SetValue(prop, value);
    }

    internal int GetHeaderSize(RadDirection? direction = null)
    {
      ref RadDirection? local1 = ref direction;
      RadDirection? nullable = direction;
      int num1 = nullable.HasValue ? (int) nullable.GetValueOrDefault() : (int) this.ExpandDirection;
      local1 = new RadDirection?((RadDirection) num1);
      int num2 = 0;
      ref RadDirection? local2 = ref direction;
      RadDirection valueOrDefault = local2.GetValueOrDefault();
      if (local2.HasValue)
      {
        switch (valueOrDefault)
        {
          case RadDirection.Left:
            num2 = this.HeaderElement.Size.Width + this.HeaderElement.Margin.Left + this.HeaderElement.Margin.Right + this.HeaderElement.Location.X;
            break;
          case RadDirection.Right:
            num2 = this.HeaderElement.Size.Width + this.HeaderElement.Margin.Left + this.HeaderElement.Margin.Right + this.HeaderElement.Location.X;
            break;
          case RadDirection.Up:
            num2 = this.HeaderElement.Size.Height + this.HeaderElement.Margin.Top + this.HeaderElement.Margin.Bottom + this.headerElement.Location.Y;
            break;
          case RadDirection.Down:
            num2 = this.HeaderElement.Location.Y + this.HeaderElement.Size.Height + this.HeaderElement.Margin.Top + this.HeaderElement.Margin.Bottom;
            break;
        }
      }
      return num2;
    }

    private bool ExpandDirectionIsHorizontal()
    {
      if (this.ExpandDirection != RadDirection.Left)
        return this.ExpandDirection == RadDirection.Right;
      return true;
    }

    public event EventHandler Expanded;

    public event EventHandler Collapsed;

    public event CancelEventHandler Expanding;

    public event CancelEventHandler Collapsing;

    protected virtual void OnExpanded()
    {
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Expanded");
      if (this.Expanded == null)
        return;
      this.Expanded((object) this, EventArgs.Empty);
    }

    protected virtual void OnCollapsed()
    {
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Collapsed");
      if (this.Collapsed == null)
        return;
      this.Collapsed((object) this, EventArgs.Empty);
    }

    protected virtual bool OnExpanding()
    {
      CancelEventArgs e = new CancelEventArgs(false);
      if (this.Expanding != null)
        this.Expanding((object) this, e);
      return !e.Cancel;
    }

    protected virtual bool OnCollapsing()
    {
      CancelEventArgs e = new CancelEventArgs(false);
      if (this.Collapsing != null)
        this.Collapsing((object) this, e);
      return !e.Cancel;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      SizeF scaleFactor1 = new SizeF(scaleFactor.Width / this.DpiScaleFactor.Width, scaleFactor.Height / this.DpiScaleFactor.Height);
      base.DpiScaleChanged(scaleFactor);
      this.ownerBoundsCache = TelerikDpiHelper.ScaleRectangle(this.ownerBoundsCache, scaleFactor1);
    }
  }
}
