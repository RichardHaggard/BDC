// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPopupControlBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.WindowAnimation;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadPopupControlBase : RadControl, IPopupControl
  {
    private object FadeAnimationFinishedKey = new object();
    private object PopupOpeningEventKey = new object();
    private object PopupOpenedEventKey = new object();
    private object PopupClosingEventKey = new object();
    private object PopupClosedEventKey = new object();
    private object MouseWheelEventKey = new object();
    private VerticalPopupAlignment verticalPopupAlignment = VerticalPopupAlignment.TopToBottom;
    private FitToScreenModes fitToScreenMode = FitToScreenModes.FitWidth | FitToScreenModes.FitHeight;
    private ScreenSpaceMode screenSpaceMode = ScreenSpaceMode.WorkingArea;
    private AlternativeCorrectionMode alignmentRectangleOverlapMode = AlternativeCorrectionMode.Flip;
    private float opacity = 1f;
    private int fadeAnimationFrames = 10;
    private int fadeAnimationSpeed = 10;
    private bool animationStateOpening = true;
    private float opacityInternal = 1f;
    protected Rectangle lastAlignmentRectangle = Rectangle.Empty;
    protected SizeF dpiScaleFactor = new SizeF(1f, 1f);
    private SizeF lastShowDpiScaleFactor = new SizeF(1f, 1f);
    private PopupAnimationTypes animationType = PopupAnimationTypes.Fade;
    private Size backupSize = Size.Empty;
    private RadElement ownerElement;
    private IPopupControl ownerPopupInternal;
    private List<IPopupControl> children;
    private AlignmentCorrectionMode horizontalAlignmentCorrectionMode;
    private AlignmentCorrectionMode verticalAlignmentCorrectionMode;
    private HorizontalPopupAlignment horizontalPopupAlignment;
    private FadeAnimationType fadeAnimationType;
    private MethodInvoker performFadeInStepInvoker;
    private MethodInvoker performFadeOutStepInvoker;
    private bool dropShadow;
    private bool aeroEnabled;
    private bool isLayeredWindow;
    private Timer fadeAnimationTimer;
    private float calculatedAnimationStep;
    private RadPopupCloseReason lastCloseReason;
    private bool dropDownAnimating;
    private WindowAnimationEngine animationEngine;
    public RadPopupControlBase.PopupAnimationProperties AnimationProperties;
    private NotifyAnimationCallback callbackAnimating;
    private NotifyAnimationCallback callbackAnimationFinished;
    private bool shouldRestoreAutoSize;

    public RadPopupControlBase(RadElement owner)
    {
      this.ownerElement = owner;
      this.animationEngine = new WindowAnimationEngine();
      this.AnimationProperties = new RadPopupControlBase.PopupAnimationProperties(this.animationEngine);
      this.callbackAnimating = new NotifyAnimationCallback(this.OnAnimating);
      this.callbackAnimationFinished = new NotifyAnimationCallback(this.OnAnimationFinished);
      this.animationEngine.Animating += new AnimationEventHandler(this.OnAnimatingEvent);
      this.animationEngine.AnimationFinished += new AnimationEventHandler(this.OnAnimationFinishedEvent);
      int num = (int) this.RootElement.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) SystemColors.Window);
    }

    protected virtual bool ShouldRestoreAutoSize
    {
      get
      {
        return this.shouldRestoreAutoSize;
      }
    }

    [Category("Behavior")]
    public RadDirection DropDownAnimationDirection
    {
      get
      {
        return this.AnimationProperties.AnimationDirection;
      }
      set
      {
        this.AnimationProperties.AnimationDirection = value;
      }
    }

    [Category("Behavior")]
    public RadEasingType EasingType
    {
      get
      {
        return this.AnimationProperties.EasingType;
      }
      set
      {
        this.AnimationProperties.EasingType = value;
      }
    }

    [Category("Behavior")]
    public int AnimationFrames
    {
      get
      {
        return this.AnimationProperties.AnimationFrames;
      }
      set
      {
        this.AnimationProperties.AnimationFrames = value;
      }
    }

    [Category("Behavior")]
    public bool AnimationEnabled
    {
      get
      {
        return this.animationType != PopupAnimationTypes.None;
      }
      set
      {
        if (value == this.AnimationEnabled)
          return;
        this.animationType = !value ? PopupAnimationTypes.None : PopupAnimationTypes.Fade;
        this.OnNotifyPropertyChanged(nameof (AnimationEnabled));
      }
    }

    [Category("Behavior")]
    public PopupAnimationTypes AnimationType
    {
      get
      {
        return this.animationType;
      }
      set
      {
        if (this.animationType == value)
          return;
        this.animationType = value;
        this.OnNotifyPropertyChanged(nameof (AnimationType));
      }
    }

    protected internal Size NonAnimatedSize
    {
      get
      {
        if (this.dropDownAnimating)
          return this.backupSize;
        return this.Size;
      }
    }

    private float OpacityInternal
    {
      get
      {
        return this.opacityInternal;
      }
      set
      {
        if ((double) value == (double) this.opacityInternal)
          return;
        this.opacityInternal = value;
        this.UpdateOpacitySettings(value);
      }
    }

    [Category("Behavior")]
    public int FadeAnimationFrames
    {
      get
      {
        return this.fadeAnimationFrames;
      }
      set
      {
        if (value == this.fadeAnimationFrames || value <= 0)
          return;
        this.fadeAnimationFrames = value;
        this.OnNotifyPropertyChanged(nameof (FadeAnimationFrames));
        this.UpdateFadeAnimationSettings();
      }
    }

    [Category("Behavior")]
    public int FadeAnimationSpeed
    {
      get
      {
        return this.fadeAnimationSpeed;
      }
      set
      {
        if (value == this.fadeAnimationSpeed)
          return;
        this.fadeAnimationSpeed = value;
        this.OnNotifyPropertyChanged(nameof (FadeAnimationSpeed));
        this.UpdateFadeAnimationSettings();
      }
    }

    [Category("Appearance")]
    public float Opacity
    {
      get
      {
        return this.opacity;
      }
      set
      {
        if ((double) value == (double) this.opacity)
          return;
        if ((double) value < 0.0)
          this.opacity = 0.0f;
        else if ((double) value > 1.0)
          this.opacity = 1f;
        else if (OSFeature.Feature.IsPresent(OSFeature.LayeredWindows))
          this.opacity = value;
        this.opacityInternal = this.opacity;
        this.OnNotifyPropertyChanged(nameof (Opacity));
        this.UpdateOpacitySettings(value);
      }
    }

    [Category("Appearance")]
    public bool DropShadow
    {
      get
      {
        return this.dropShadow;
      }
      set
      {
        if (value == this.dropShadow)
          return;
        this.dropShadow = value;
        this.OnNotifyPropertyChanged(nameof (DropShadow));
        this.UpdateStyles();
      }
    }

    [Category("Appearance")]
    public bool EnableAeroEffects
    {
      get
      {
        return this.aeroEnabled;
      }
      set
      {
        if (value == this.aeroEnabled)
          return;
        this.aeroEnabled = value;
        this.OnNotifyPropertyChanged(nameof (EnableAeroEffects));
        this.UpdateAeroEffectState();
      }
    }

    [Editor(typeof (FadeAnimationTypeEditor), typeof (UITypeEditor))]
    [Category("Behavior")]
    public FadeAnimationType FadeAnimationType
    {
      get
      {
        return this.fadeAnimationType;
      }
      set
      {
        if (value == this.fadeAnimationType)
          return;
        this.fadeAnimationType = value;
        this.OnNotifyPropertyChanged(nameof (FadeAnimationType));
        this.UpdateFadeAnimationSettings();
      }
    }

    [Description("Gets or sets a value that determines how the size of the popup is adjusted according to the available screen space.")]
    [DefaultValue(typeof (FitToScreenModes), "FitWidth | FitHeight")]
    [Category("Behavior")]
    public FitToScreenModes FitToScreenMode
    {
      get
      {
        return this.fitToScreenMode;
      }
      set
      {
        if (value == this.fitToScreenMode)
          return;
        this.fitToScreenMode = value;
        this.OnNotifyPropertyChanged(nameof (FitToScreenMode));
      }
    }

    [Category("Behavior")]
    [DefaultValue(typeof (ScreenSpaceMode), "WorkingArea")]
    [Description("Gets or sets a value that determines what part of the screen is considered when positioning the popup.")]
    public ScreenSpaceMode ScreenSpaceMode
    {
      get
      {
        return this.screenSpaceMode;
      }
      set
      {
        if (value == this.screenSpaceMode)
          return;
        this.screenSpaceMode = value;
        this.OnNotifyPropertyChanged(nameof (ScreenSpaceMode));
      }
    }

    [DefaultValue(typeof (AlternativeCorrectionMode), "Flip")]
    [Category("Behavior")]
    [Description("Gets or sets a value that defines how the popup will be positioned according to the alignment rectangle when its location cannot be adjusted so that it meets all popup alignment and alignment correction mode requirements.")]
    public AlternativeCorrectionMode AlignmentRectangleOverlapMode
    {
      get
      {
        return this.alignmentRectangleOverlapMode;
      }
      set
      {
        if (value == this.alignmentRectangleOverlapMode)
          return;
        this.alignmentRectangleOverlapMode = value;
        this.OnNotifyPropertyChanged(nameof (AlignmentRectangleOverlapMode));
      }
    }

    [DefaultValue(AlignmentCorrectionMode.None)]
    [Category("Behavior")]
    public AlignmentCorrectionMode HorizontalAlignmentCorrectionMode
    {
      get
      {
        return this.horizontalAlignmentCorrectionMode;
      }
      set
      {
        if (value == this.horizontalAlignmentCorrectionMode)
          return;
        this.horizontalAlignmentCorrectionMode = value;
        this.OnNotifyPropertyChanged(nameof (HorizontalAlignmentCorrectionMode));
      }
    }

    [Category("Behavior")]
    [DefaultValue(AlignmentCorrectionMode.None)]
    public AlignmentCorrectionMode VerticalAlignmentCorrectionMode
    {
      get
      {
        return this.verticalAlignmentCorrectionMode;
      }
      set
      {
        if (value == this.verticalAlignmentCorrectionMode)
          return;
        this.verticalAlignmentCorrectionMode = value;
        this.OnNotifyPropertyChanged(nameof (VerticalAlignmentCorrectionMode));
      }
    }

    [Category("Behavior")]
    [DefaultValue(VerticalPopupAlignment.TopToBottom)]
    public VerticalPopupAlignment VerticalPopupAlignment
    {
      get
      {
        return this.verticalPopupAlignment;
      }
      set
      {
        if (value == this.verticalPopupAlignment)
          return;
        this.verticalPopupAlignment = value;
        this.OnNotifyPropertyChanged(nameof (VerticalPopupAlignment));
        this.UpdateLocation(this.lastAlignmentRectangle);
      }
    }

    [Category("Behavior")]
    [DefaultValue(HorizontalPopupAlignment.LeftToLeft)]
    public HorizontalPopupAlignment HorizontalPopupAlignment
    {
      get
      {
        return this.horizontalPopupAlignment;
      }
      set
      {
        if (value == this.horizontalPopupAlignment)
          return;
        this.horizontalPopupAlignment = value;
        this.OnNotifyPropertyChanged(nameof (HorizontalPopupAlignment));
        this.UpdateLocation(this.lastAlignmentRectangle);
      }
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = new CreateParams();
        createParams.Style = int.MinValue;
        createParams.Style |= 33554432;
        createParams.ExStyle = 128;
        createParams.ExStyle |= 8;
        if (this.isLayeredWindow)
          createParams.ExStyle |= 524288;
        if (this.dropShadow && OSFeature.IsPresent(SystemParameter.DropShadow))
          createParams.ClassStyle |= 131072;
        createParams.Width = this.Size.Width;
        createParams.Height = this.Size.Height;
        createParams.X = this.Location.X;
        createParams.Y = this.Location.Y;
        return createParams;
      }
    }

    [Browsable(false)]
    public RadElement OwnerElement
    {
      get
      {
        return this.ownerElement;
      }
    }

    [Browsable(false)]
    public IPopupControl OwnerPopup
    {
      get
      {
        if (this.ownerElement == null || !this.ownerElement.IsInValidState(true))
          return (IPopupControl) null;
        for (Control control = this.ownerElement.ElementTree.Control; control != null; control = control.Parent)
        {
          if (control is IPopupControl)
            return control as IPopupControl;
        }
        return (IPopupControl) null;
      }
    }

    public SizeF LastShowDpiScaleFactor
    {
      get
      {
        return this.lastShowDpiScaleFactor;
      }
      set
      {
        this.lastShowDpiScaleFactor = value;
      }
    }

    public new void Show()
    {
      this.ShowPopup(new Rectangle(this.Location, Size.Empty));
    }

    public void Show(Point screenLocation)
    {
      this.ShowPopup(new Rectangle(screenLocation, Size.Empty));
    }

    public void Show(Control control)
    {
      this.ShowPopup(new Rectangle(control.PointToScreen(Point.Empty), control.Size));
    }

    public new void Hide()
    {
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    protected override bool ProcessFocusRequested(RadElement element)
    {
      if (element == null)
        return false;
      return base.ProcessFocusRequested(element);
    }

    protected override void WndProc(ref Message m)
    {
      if (this.dropDownAnimating && this.IsInputMessage(m.Msg))
        return;
      if (m.Msg == 33)
        this.OnWMMouseActivate(ref m);
      else
        base.WndProc(ref m);
    }

    private bool IsInputMessage(int message)
    {
      if (message >= 512 && message <= 522 || message >= 256 && message <= 264 || message == Telerik.WinControls.NativeMethods.WM_MOUSEENTER)
        return true;
      return message == 675;
    }

    private void OnWMMouseActivate(ref Message m)
    {
      m.Result = (IntPtr) 3;
    }

    private void UpdateOpacitySettings(float opacity)
    {
      if (!this.isLayeredWindow && (double) opacity < 1.0)
      {
        this.isLayeredWindow = true;
        this.UpdateStyles();
      }
      if (this.isLayeredWindow)
      {
        Telerik.WinControls.NativeMethods.SetLayeredWindowAttributes(new HandleRef((object) this, this.Handle), 0, (byte) ((double) opacity * (double) byte.MaxValue), 2);
      }
      else
      {
        this.isLayeredWindow = false;
        this.UpdateStyles();
      }
    }

    internal void CallOnPopupOpened()
    {
      RadPopupOpenedEventHandler openedEventHandler = this.Events[this.PopupOpenedEventKey] as RadPopupOpenedEventHandler;
      if (openedEventHandler != null)
      {
        EventArgs args = new EventArgs();
        openedEventHandler((object) this, args);
      }
      this.ownerPopupInternal = this.OwnerPopup;
      this.OnPopupOpened();
    }

    protected virtual void OnPopupOpened()
    {
    }

    internal void CallOnPopupOpening(CancelEventArgs args)
    {
      RadPopupOpeningEventHandler openingEventHandler = this.Events[this.PopupOpeningEventKey] as RadPopupOpeningEventHandler;
      if (openingEventHandler != null)
        openingEventHandler((object) this, args);
      this.OnPopupOpening(args);
    }

    protected virtual void OnPopupOpening(CancelEventArgs args)
    {
    }

    protected virtual void OnPopupClosed(RadPopupClosedEventArgs args)
    {
      RadPopupClosedEventHandler closedEventHandler = this.Events[this.PopupClosedEventKey] as RadPopupClosedEventHandler;
      if (closedEventHandler == null)
        return;
      closedEventHandler((object) this, args);
    }

    protected virtual void OnPopupClosing(RadPopupClosingEventArgs args)
    {
      RadPopupClosingEventHandler closingEventHandler = this.Events[this.PopupClosingEventKey] as RadPopupClosingEventHandler;
      if (closingEventHandler == null)
        return;
      closingEventHandler((object) this, args);
    }

    private void UpdateFadeAnimationSettings()
    {
      if (this.fadeAnimationType == FadeAnimationType.None)
        return;
      if (this.fadeAnimationTimer == null)
      {
        this.fadeAnimationTimer = new Timer();
        this.fadeAnimationTimer.Tick += new EventHandler(this.OnFadeAnimationTimer_Tick);
        this.fadeAnimationTimer.Interval = this.fadeAnimationSpeed;
        this.performFadeInStepInvoker = new MethodInvoker(this.PerformFadeInStep);
        this.performFadeOutStepInvoker = new MethodInvoker(this.PerformFadeOutStep);
      }
      this.fadeAnimationTimer.Interval = this.fadeAnimationSpeed;
      this.calculatedAnimationStep = 1f / (float) this.fadeAnimationFrames;
    }

    private void OnFadeAnimationTimer_Tick(object sender, EventArgs e)
    {
      if (this.animationStateOpening)
      {
        if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
        {
          this.Invoke((Delegate) this.performFadeInStepInvoker);
        }
        else
        {
          this.fadeAnimationTimer.Stop();
          this.OnFadeAnimationFinished(this.animationStateOpening);
        }
      }
      else if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
      {
        this.Invoke((Delegate) this.performFadeOutStepInvoker);
      }
      else
      {
        this.fadeAnimationTimer.Stop();
        this.OnFadeAnimationFinished(this.animationStateOpening);
      }
    }

    private void PerformFadeOutStep()
    {
      if ((double) this.OpacityInternal - (double) this.calculatedAnimationStep > 0.0)
      {
        this.OpacityInternal -= this.calculatedAnimationStep;
      }
      else
      {
        this.OpacityInternal = 0.0f;
        this.fadeAnimationTimer.Stop();
        this.OnFadeAnimationFinished(this.animationStateOpening);
      }
    }

    private void PerformFadeInStep()
    {
      if ((double) this.OpacityInternal + (double) this.calculatedAnimationStep < (double) this.Opacity)
      {
        this.OpacityInternal += this.calculatedAnimationStep;
      }
      else
      {
        this.fadeAnimationTimer.Stop();
        this.OpacityInternal = this.Opacity;
        this.OnFadeAnimationFinished(this.animationStateOpening);
      }
    }

    private void OnFadeAnimationFinished(bool isFadingIn)
    {
      if (!isFadingIn)
        this.ClosePopupCore();
      RadPopupFadeAnimationFinishedEventHandler finishedEventHandler = this.Events[this.FadeAnimationFinishedKey] as RadPopupFadeAnimationFinishedEventHandler;
      if (finishedEventHandler == null)
        return;
      finishedEventHandler((object) this, new FadeAnimationEventArgs(isFadingIn));
    }

    protected virtual void InitializeDropDownAnimation(Point location)
    {
      SizeF scaleFactor = this.LastShowDpiScaleFactor.IsEmpty ? new SizeF(1f, 1f) : this.LastShowDpiScaleFactor;
      this.backupSize = this.GetBackupSize();
      this.animationEngine.AnimateMinimumToMaximum = true;
      Rectangle rectangle1 = new Rectangle(location, new Size(this.backupSize.Width, 0));
      Rectangle rectangle2 = new Rectangle(location, this.backupSize);
      bool flag = this.OwnerElement is PopupEditorBaseElement;
      switch (this.AnimationProperties.AnimationDirection)
      {
        case RadDirection.Left:
          rectangle1 = new Rectangle(new Point(location.X + this.Width, location.Y), new Size(0, this.Height));
          rectangle2 = new Rectangle(new Point(location.X, location.Y), this.Size);
          break;
        case RadDirection.Right:
          rectangle1 = new Rectangle(new Point(location.X, location.Y), new Size(0, this.Height));
          rectangle2 = new Rectangle(new Point(location.X, location.Y), this.Size);
          break;
        case RadDirection.Up:
          rectangle1 = new Rectangle(new Point(location.X, location.Y + this.Height), new Size(this.Width, 0));
          rectangle2 = new Rectangle(new Point(location.X, location.Y), this.Size);
          break;
        case RadDirection.Down:
          if (flag)
          {
            rectangle1 = new Rectangle(location, this.Size);
            rectangle2 = new Rectangle(location, new Size(this.Width, 0));
            break;
          }
          break;
      }
      if (this.OwnerElement != null && this.OwnerElement.DpiScaleFactor != scaleFactor)
      {
        rectangle1 = new Rectangle(rectangle1.Location, TelerikDpiHelper.ScaleSize(rectangle1.Size, scaleFactor));
        rectangle2 = new Rectangle(rectangle1.Location, TelerikDpiHelper.ScaleSize(rectangle2.Size, scaleFactor));
      }
      if (flag)
      {
        this.animationEngine.Minimum = rectangle2;
        this.animationEngine.Maximum = rectangle1;
      }
      else
      {
        this.animationEngine.Minimum = rectangle1;
        this.animationEngine.Maximum = rectangle2;
      }
      if (this.AutoSize)
      {
        this.AutoSize = false;
        this.shouldRestoreAutoSize = true;
      }
      this.Size = rectangle1.Size;
      this.Location = rectangle1.Location;
    }

    protected virtual Size GetBackupSize()
    {
      SizeF scaleFactor = new SizeF(1f, 1f);
      if (this.OwnerElement != null && this.OwnerElement.DpiScaleFactor != this.LastShowDpiScaleFactor)
      {
        SizeF sizeF = this.LastShowDpiScaleFactor.IsEmpty ? scaleFactor : this.LastShowDpiScaleFactor;
        scaleFactor = new SizeF(this.OwnerElement.DpiScaleFactor.Width / sizeF.Width, this.OwnerElement.DpiScaleFactor.Height / sizeF.Height);
      }
      return TelerikDpiHelper.ScaleSize(this.Size, scaleFactor);
    }

    protected void UpdateAeroEffectState()
    {
      bool compositionEnabled = DWMAPI.IsCompositionEnabled;
      DWMAPI.DWMBLURBEHIND blurInfo = new DWMAPI.DWMBLURBEHIND();
      if (compositionEnabled && this.EnableAeroEffects)
      {
        blurInfo.dwFlags = 1;
        blurInfo.fEnable = true;
      }
      else if (compositionEnabled && !this.EnableAeroEffects)
      {
        blurInfo.dwFlags = 1;
        blurInfo.fEnable = false;
      }
      if (!compositionEnabled)
        return;
      DWMAPI.DwmEnableBlurBehindWindow(this.Handle, ref blurInfo);
    }

    public void UpdateLocation(Rectangle alignmentRectangle)
    {
      this.lastAlignmentRectangle = alignmentRectangle;
      this.Location = this.GetCorrectedLocation(this.lastAlignmentRectangle);
    }

    public void UpdateLocation()
    {
      this.Location = this.GetCorrectedLocation(this.lastAlignmentRectangle);
    }

    protected virtual Point GetCorrectedLocation(Rectangle alignmentRectangle)
    {
      return this.GetCorrectedLocation(this.GetCurrentScreen(alignmentRectangle), alignmentRectangle);
    }

    protected virtual Point GetCorrectedLocation(
      Screen currentScreen,
      Rectangle alignmentRectangle)
    {
      Point calculatedLocation = new Point(this.GetHorizontalPopupLocation(alignmentRectangle), this.GetVerticalPopupLocation(alignmentRectangle));
      Point horizontalLocation = this.GetCorrectedHorizontalLocation(currentScreen, alignmentRectangle, calculatedLocation);
      Point verticalLocation = this.GetCorrectedVerticalLocation(currentScreen, alignmentRectangle, horizontalLocation);
      return this.CheckMakeLastLocationCorrection(alignmentRectangle, verticalLocation, this.GetAvailableBoundsFromScreen(currentScreen));
    }

    protected internal virtual Size ApplySizingConstraints(
      Size availableSize,
      Screen currentScreen)
    {
      int width = availableSize.Width;
      int height = availableSize.Height;
      Rectangle boundsFromScreen = this.GetAvailableBoundsFromScreen(currentScreen);
      if ((this.fitToScreenMode & FitToScreenModes.FitHeight) != FitToScreenModes.None && height > boundsFromScreen.Height)
        height = boundsFromScreen.Height;
      if ((this.fitToScreenMode & FitToScreenModes.FitWidth) != FitToScreenModes.None && width > boundsFromScreen.Width)
        width = boundsFromScreen.Width;
      return new Size(width, height);
    }

    protected internal virtual Screen GetCurrentScreen(Rectangle alignmentRectangle)
    {
      return Screen.FromRectangle(alignmentRectangle);
    }

    public Screen GetCurrentScreen()
    {
      return this.GetCurrentScreen(this.lastAlignmentRectangle);
    }

    protected internal virtual Rectangle GetAvailableBoundsFromScreen(Screen screen)
    {
      if (this.screenSpaceMode == ScreenSpaceMode.WorkingArea)
        return screen.WorkingArea;
      return screen.Bounds;
    }

    protected virtual Point GetCorrectedHorizontalLocation(
      Screen currentScreen,
      Rectangle alignmentRectangle,
      Point calculatedLocation)
    {
      Rectangle boundsFromScreen = this.GetAvailableBoundsFromScreen(currentScreen);
      switch (this.horizontalAlignmentCorrectionMode)
      {
        case AlignmentCorrectionMode.Smooth:
          return this.AlignHorizontallyWithoutSnapping(calculatedLocation, alignmentRectangle, boundsFromScreen);
        case AlignmentCorrectionMode.SnapToEdges:
          return this.SnapToEdgeHorizontally(calculatedLocation, alignmentRectangle, boundsFromScreen);
        case AlignmentCorrectionMode.SnapToOuterEdges:
          return this.SnapToOuterEdgeHorizontally(calculatedLocation, alignmentRectangle, boundsFromScreen);
        default:
          return calculatedLocation;
      }
    }

    private Point AlignHorizontallyWithoutSnapping(
      Point proposedLocation,
      Rectangle alignmentRectangle,
      Rectangle bounds)
    {
      int x = proposedLocation.X;
      Size size = this.Size;
      int num1 = x - bounds.Left;
      int num2 = bounds.Right - (x + size.Width);
      if (num1 > 0 && num2 < 0)
      {
        if (Math.Abs(num2) <= num1)
          return new Point(x - Math.Abs(num2), proposedLocation.Y);
      }
      else if (num1 < 0 && num2 > 0 && num2 >= Math.Abs(num1))
        return new Point(x + Math.Abs(num1), proposedLocation.Y);
      return proposedLocation;
    }

    private Point SnapToOuterEdgeHorizontally(
      Point proposedLocation,
      Rectangle alignmentRectangle,
      Rectangle bounds)
    {
      Rectangle rectangle = Rectangle.Intersect(alignmentRectangle, bounds);
      Size size = this.Size;
      if (rectangle != Rectangle.Empty && alignmentRectangle.Width != rectangle.Width)
      {
        if (alignmentRectangle.Right > bounds.Right && alignmentRectangle.Left > bounds.Left)
        {
          if (alignmentRectangle.Left - size.Width >= bounds.Left)
            return new Point(alignmentRectangle.Left - size.Width, proposedLocation.Y);
        }
        else if (alignmentRectangle.Left < bounds.Left && alignmentRectangle.Right <= bounds.Right && alignmentRectangle.Right + size.Width <= bounds.Right)
          return new Point(alignmentRectangle.Right, proposedLocation.Y);
      }
      else if (proposedLocation.X < bounds.Left)
      {
        if (rectangle == Rectangle.Empty)
          return new Point(bounds.Left, proposedLocation.Y);
        if (alignmentRectangle.Right + size.Width <= bounds.Right)
          return new Point(alignmentRectangle.Right, proposedLocation.Y);
      }
      else if (proposedLocation.X + size.Width > bounds.Right)
      {
        if (rectangle == Rectangle.Empty)
          return new Point(bounds.Right - size.Width, proposedLocation.Y);
        if (alignmentRectangle.Left - size.Width >= bounds.Left)
          return new Point(alignmentRectangle.Left - size.Width, proposedLocation.Y);
      }
      return proposedLocation;
    }

    private Point SnapToEdgeHorizontally(
      Point proposedLocation,
      Rectangle alignmentRectangle,
      Rectangle bounds)
    {
      int x = proposedLocation.X;
      Rectangle rectangle = Rectangle.Intersect(alignmentRectangle, bounds);
      Size size = this.Size;
      if (rectangle != Rectangle.Empty && alignmentRectangle.Width != rectangle.Width)
      {
        if (alignmentRectangle.Right > bounds.Right && alignmentRectangle.Left >= bounds.Left)
        {
          if (alignmentRectangle.Left + size.Width <= bounds.Right)
            return new Point(alignmentRectangle.Left, proposedLocation.Y);
          if (alignmentRectangle.Left <= bounds.Right && alignmentRectangle.Left - size.Width >= bounds.Left)
            return new Point(alignmentRectangle.Left - size.Width, proposedLocation.Y);
          if (bounds.Right - size.Width >= bounds.Left)
            return new Point(bounds.Right - size.Width, proposedLocation.Y);
        }
        else if (alignmentRectangle.Left < bounds.Left && alignmentRectangle.Right <= bounds.Right)
        {
          if (alignmentRectangle.Right - size.Width >= bounds.Left)
            return new Point(alignmentRectangle.Right - size.Width, proposedLocation.Y);
          if (alignmentRectangle.Right >= bounds.Left && alignmentRectangle.Right + size.Width <= bounds.Right)
            return new Point(alignmentRectangle.Right, proposedLocation.Y);
          if (bounds.Left - size.Width <= bounds.Right)
            return new Point(bounds.Left - size.Width, proposedLocation.Y);
        }
      }
      else if (x < bounds.Left)
      {
        if (rectangle == Rectangle.Empty)
          return new Point(bounds.Left, proposedLocation.Y);
        if (alignmentRectangle.Right - size.Width >= bounds.Left)
          return new Point(alignmentRectangle.Right - size.Width, proposedLocation.Y);
        if (alignmentRectangle.Left + size.Width <= bounds.Right)
          return new Point(alignmentRectangle.Left, proposedLocation.Y);
      }
      else if (x + size.Width > bounds.Right)
      {
        if (rectangle == Rectangle.Empty)
          return new Point(bounds.Right - size.Width, proposedLocation.Y);
        if (alignmentRectangle.Left + size.Width <= bounds.Right)
          return new Point(alignmentRectangle.Left, proposedLocation.Y);
        if (alignmentRectangle.Right - size.Width >= bounds.Left)
          return new Point(alignmentRectangle.Right - size.Width, proposedLocation.Y);
      }
      return proposedLocation;
    }

    protected virtual Point GetCorrectedVerticalLocation(
      Screen currentScreen,
      Rectangle alignmentRectangle,
      Point calculatedLocation)
    {
      Rectangle boundsFromScreen = this.GetAvailableBoundsFromScreen(currentScreen);
      switch (this.verticalAlignmentCorrectionMode)
      {
        case AlignmentCorrectionMode.Smooth:
          return this.AlignVerticallyWithoutSnapping(calculatedLocation, alignmentRectangle, boundsFromScreen);
        case AlignmentCorrectionMode.SnapToEdges:
          return this.SnapToEdgeVertically(calculatedLocation, alignmentRectangle, boundsFromScreen);
        case AlignmentCorrectionMode.SnapToOuterEdges:
          return this.SnapToOuterEdgeVertically(calculatedLocation, alignmentRectangle, boundsFromScreen);
        default:
          return calculatedLocation;
      }
    }

    private Point AlignVerticallyWithoutSnapping(
      Point proposedLocation,
      Rectangle alignmentRectangle,
      Rectangle bounds)
    {
      Size size = this.Size;
      int num1 = bounds.Bottom - (proposedLocation.Y + size.Height);
      int num2 = proposedLocation.Y - bounds.Top;
      if (num1 > 0 && num2 < 0)
      {
        if (Math.Abs(num2) <= num1)
          return new Point(proposedLocation.X, Math.Abs(num2) + proposedLocation.Y);
      }
      else if (num1 < 0 && num2 > 0 && num2 >= Math.Abs(num1))
        return new Point(proposedLocation.X, proposedLocation.Y - Math.Abs(num1));
      return proposedLocation;
    }

    private Point SnapToOuterEdgeVertically(
      Point proposedLocation,
      Rectangle alignmentRectangle,
      Rectangle bounds)
    {
      Rectangle rectangle = Rectangle.Intersect(alignmentRectangle, bounds);
      Size size = this.Size;
      size.Height = Math.Min(size.Height, bounds.Height);
      size.Width = Math.Min(size.Width, bounds.Width);
      if (rectangle != Rectangle.Empty && alignmentRectangle.Height != rectangle.Height)
      {
        if (alignmentRectangle.Bottom > bounds.Bottom && alignmentRectangle.Top >= bounds.Top)
        {
          if (alignmentRectangle.Top - size.Height >= bounds.Top)
            return new Point(proposedLocation.X, alignmentRectangle.Top - size.Height);
        }
        else if (alignmentRectangle.Top < bounds.Top && alignmentRectangle.Bottom <= bounds.Bottom && alignmentRectangle.Bottom + size.Height <= bounds.Bottom)
          return new Point(proposedLocation.X, alignmentRectangle.Bottom);
      }
      else if (proposedLocation.Y < bounds.Top)
      {
        if (rectangle == Rectangle.Empty)
          return new Point(proposedLocation.X, bounds.Top);
        if (alignmentRectangle.Bottom + size.Height <= bounds.Bottom)
          return new Point(proposedLocation.X, alignmentRectangle.Bottom);
      }
      else if (proposedLocation.Y + size.Height > bounds.Bottom)
      {
        if (rectangle == Rectangle.Empty)
          return new Point(proposedLocation.X, bounds.Bottom - size.Height);
        if (alignmentRectangle.Top - size.Height >= bounds.Top)
          return new Point(proposedLocation.X, alignmentRectangle.Top - size.Height);
      }
      return proposedLocation;
    }

    private Point SnapToEdgeVertically(
      Point proposedLocation,
      Rectangle alignmentRectangle,
      Rectangle bounds)
    {
      Rectangle rectangle = Rectangle.Intersect(alignmentRectangle, bounds);
      Size size = this.Size;
      if (rectangle != Rectangle.Empty && alignmentRectangle.Height != rectangle.Height)
      {
        if (alignmentRectangle.Bottom > bounds.Bottom && alignmentRectangle.Top >= bounds.Top)
        {
          if (alignmentRectangle.Top + size.Height <= bounds.Bottom)
            return new Point(proposedLocation.X, alignmentRectangle.Top);
          if (alignmentRectangle.Top <= bounds.Bottom && alignmentRectangle.Top - size.Height >= bounds.Top)
            return new Point(proposedLocation.X, alignmentRectangle.Top - size.Height);
          if (bounds.Bottom - size.Height >= bounds.Top)
            return new Point(proposedLocation.X, bounds.Bottom - size.Height);
        }
        else if (alignmentRectangle.Top < bounds.Top && alignmentRectangle.Bottom <= bounds.Bottom)
        {
          if (alignmentRectangle.Bottom - size.Height >= bounds.Top)
            return new Point(proposedLocation.X, alignmentRectangle.Bottom - size.Height);
          if (alignmentRectangle.Bottom >= bounds.Top && alignmentRectangle.Bottom + size.Height <= bounds.Bottom)
            return new Point(proposedLocation.X, alignmentRectangle.Bottom);
          if (bounds.Top + size.Height <= bounds.Bottom)
            return new Point(proposedLocation.X, bounds.Top);
        }
      }
      else if (proposedLocation.Y < bounds.Top)
      {
        if (rectangle == Rectangle.Empty)
          return new Point(proposedLocation.X, bounds.Top);
        if (alignmentRectangle.Bottom - size.Height >= bounds.Top)
          return new Point(proposedLocation.X, alignmentRectangle.Bottom - size.Height);
        if (alignmentRectangle.Top + size.Height <= bounds.Bottom)
          return new Point(proposedLocation.X, alignmentRectangle.Top);
      }
      else if (proposedLocation.Y + size.Height > bounds.Bottom)
      {
        if (rectangle == Rectangle.Empty)
          return new Point(proposedLocation.X, bounds.Bottom - size.Height);
        if (alignmentRectangle.Top + size.Height <= bounds.Bottom)
          return new Point(proposedLocation.X, alignmentRectangle.Top);
        if (alignmentRectangle.Bottom - bounds.Top >= size.Height)
          return new Point(proposedLocation.X, alignmentRectangle.Bottom - size.Height);
      }
      return proposedLocation;
    }

    protected virtual int GetHorizontalPopupLocation(Rectangle alignmentRectangle)
    {
      int num = 0;
      HorizontalPopupAlignment horizontalPopupAlignment = this.horizontalPopupAlignment;
      if (this.RightToLeft == RightToLeft.Yes)
      {
        switch (this.HorizontalPopupAlignment)
        {
          case HorizontalPopupAlignment.LeftToLeft:
            horizontalPopupAlignment = HorizontalPopupAlignment.RightToRight;
            break;
          case HorizontalPopupAlignment.LeftToRight:
            horizontalPopupAlignment = HorizontalPopupAlignment.RightToLeft;
            break;
          case HorizontalPopupAlignment.RightToLeft:
            horizontalPopupAlignment = HorizontalPopupAlignment.LeftToRight;
            break;
          case HorizontalPopupAlignment.RightToRight:
            horizontalPopupAlignment = HorizontalPopupAlignment.LeftToLeft;
            break;
        }
      }
      switch (horizontalPopupAlignment)
      {
        case HorizontalPopupAlignment.LeftToLeft:
          num = alignmentRectangle.Left;
          break;
        case HorizontalPopupAlignment.LeftToRight:
          num = alignmentRectangle.Right;
          break;
        case HorizontalPopupAlignment.RightToLeft:
          num = alignmentRectangle.Left - this.Width;
          break;
        case HorizontalPopupAlignment.RightToRight:
          num = alignmentRectangle.Right - this.Width;
          break;
      }
      return num;
    }

    protected virtual int GetVerticalPopupLocation(Rectangle alignmentRectangle)
    {
      int num = 0;
      switch (this.verticalPopupAlignment)
      {
        case VerticalPopupAlignment.TopToTop:
          num = alignmentRectangle.Top;
          break;
        case VerticalPopupAlignment.TopToBottom:
          num = alignmentRectangle.Bottom;
          break;
        case VerticalPopupAlignment.BottomToTop:
          num = alignmentRectangle.Top - this.Height;
          break;
        case VerticalPopupAlignment.BottomToBottom:
          num = alignmentRectangle.Bottom - this.Height;
          break;
      }
      return num;
    }

    protected virtual void AnimationStarting()
    {
      this.dropDownAnimating = true;
    }

    protected virtual Point OnAlternativeXLocationNeeded(
      Rectangle alignmentRectangle,
      Point proposedLocation,
      Rectangle availableBounds)
    {
      if (this.alignmentRectangleOverlapMode == AlternativeCorrectionMode.Overlap)
      {
        Rectangle rectangle = new Rectangle(proposedLocation, this.Size);
        if (rectangle.Left < availableBounds.Left)
          return new Point(availableBounds.Left, proposedLocation.Y);
        if (rectangle.Right > availableBounds.Right)
          return new Point(availableBounds.Right - this.Size.Width, proposedLocation.Y);
        return proposedLocation;
      }
      Size size = this.Size;
      if (availableBounds.Bottom - alignmentRectangle.Bottom >= size.Height)
        return new Point(availableBounds.Right - size.Width, alignmentRectangle.Bottom);
      if (alignmentRectangle.Top - availableBounds.Top >= size.Height)
        return new Point(availableBounds.Right - size.Width, alignmentRectangle.Top - size.Height);
      return proposedLocation;
    }

    protected virtual Point CheckMakeLastLocationCorrection(
      Rectangle alignmentRectangle,
      Point proposedLocation,
      Rectangle availableBounds)
    {
      Size size = this.Size;
      size.Height = Math.Min(size.Height, availableBounds.Height);
      size.Width = Math.Min(size.Width, availableBounds.Width);
      Rectangle b = new Rectangle(proposedLocation, size);
      Rectangle rectangle = Rectangle.Intersect(availableBounds, b);
      if (b.Width != rectangle.Width)
        return this.OnAlternativeXLocationNeeded(alignmentRectangle, proposedLocation, availableBounds);
      if (b.Height != rectangle.Height)
        return this.OnAlternativeYLocationNeeded(alignmentRectangle, proposedLocation, availableBounds);
      return proposedLocation;
    }

    protected virtual Point OnAlternativeYLocationNeeded(
      Rectangle alignmentRectangle,
      Point proposedLocation,
      Rectangle availableBounds)
    {
      if (this.alignmentRectangleOverlapMode == AlternativeCorrectionMode.Overlap)
      {
        Rectangle rectangle = new Rectangle(proposedLocation, this.Size);
        if (rectangle.Top < availableBounds.Top)
          return new Point(proposedLocation.X, availableBounds.Top);
        if (rectangle.Bottom > availableBounds.Bottom)
          return new Point(proposedLocation.X, availableBounds.Bottom - this.Size.Height);
        return proposedLocation;
      }
      Size size = this.Size;
      size.Height = Math.Min(size.Height, availableBounds.Height);
      size.Width = Math.Min(size.Width, availableBounds.Width);
      int y = proposedLocation.Y < availableBounds.Top ? availableBounds.Top : availableBounds.Bottom - size.Height;
      if (availableBounds.Right - alignmentRectangle.Right >= size.Width)
        return new Point(alignmentRectangle.Right, y);
      if (alignmentRectangle.Left - availableBounds.Left >= size.Width)
        return new Point(alignmentRectangle.Left - size.Width, availableBounds.Bottom - size.Height);
      return proposedLocation;
    }

    private void ClosePopupCore()
    {
      this.SetVisibleCore(false);
      this.dropDownAnimating = false;
    }

    protected virtual void ShowPopupCore(Size size, Point location)
    {
      SizeF sizeF = this.OwnerElement != null ? this.OwnerElement.DpiScaleFactor : this.dpiScaleFactor;
      bool isEmpty = this.LastShowDpiScaleFactor.IsEmpty;
      if (this.LastShowDpiScaleFactor != sizeF)
      {
        if (isEmpty)
          this.LastShowDpiScaleFactor = new SizeF(1f, 1f);
        SizeF factor = new SizeF(sizeF.Width / this.LastShowDpiScaleFactor.Width, sizeF.Height / this.LastShowDpiScaleFactor.Height);
        this.LastShowDpiScaleFactor = sizeF;
        this.Scale(factor);
        if (isEmpty)
          size = TelerikDpiHelper.ScaleSize(size, new SizeF(1f / factor.Width, 1f / factor.Height));
      }
      this.SetBoundsCore(location.X, location.Y, size.Width, size.Height, BoundsSpecified.All);
      Telerik.WinControls.NativeMethods.ShowWindow(this.Handle, 4);
      if (DWMAPI.IsCompositionEnabled && this.EnableAeroEffects)
        this.UpdateAeroEffectState();
      ControlHelper.BringToFront(this.Handle, false);
    }

    public List<IPopupControl> Children
    {
      get
      {
        if (this.children == null)
          this.children = new List<IPopupControl>();
        return this.children;
      }
    }

    public virtual void ShowPopup(Rectangle alignmentRectangle)
    {
      if (PopupManager.Default.ContainsPopup((IPopupControl) this))
        return;
      this.lastAlignmentRectangle = alignmentRectangle;
      Screen currentScreen = this.GetCurrentScreen();
      Point location = this.GetCorrectedLocation(alignmentRectangle);
      RadPopupOpeningEventArgs openingEventArgs = new RadPopupOpeningEventArgs(location);
      this.CallOnPopupOpening((CancelEventArgs) openingEventArgs);
      Size size = this.ApplySizingConstraints(this.Size, currentScreen);
      if (openingEventArgs.Cancel)
        return;
      location = new Point(openingEventArgs.CustomLocation.X, openingEventArgs.CustomLocation.Y);
      PopupManager.Default.AddPopup((IPopupControl) this);
      if ((this.animationType & PopupAnimationTypes.Fade) != PopupAnimationTypes.None && ThemeResolutionService.AllowAnimations)
      {
        this.InitializeDropDownAnimation(location);
        this.dropDownAnimating = true;
        this.animationEngine.Start();
        this.AnimationStarting();
      }
      if ((this.fadeAnimationType & FadeAnimationType.FadeIn) != FadeAnimationType.None && (this.animationType & PopupAnimationTypes.Fade) != PopupAnimationTypes.None && ThemeResolutionService.AllowAnimations)
      {
        this.animationStateOpening = true;
        this.OpacityInternal = 0.0f;
        this.ShowPopupCore(size, location);
        this.CallOnPopupOpened();
        if (this.fadeAnimationTimer.Enabled)
          return;
        this.fadeAnimationTimer.Start();
      }
      else
      {
        if ((double) this.opacityInternal != (double) this.Opacity)
          this.OpacityInternal = this.Opacity;
        this.ShowPopupCore(size, location);
        this.CallOnPopupOpened();
      }
    }

    public virtual void ClosePopup(PopupCloseInfo info)
    {
      if (!PopupManager.Default.ContainsPopup((IPopupControl) this) || !this.OnPopupClosing(info))
        return;
      if ((this.fadeAnimationType & FadeAnimationType.FadeOut) != FadeAnimationType.None && (this.animationType & PopupAnimationTypes.Fade) != PopupAnimationTypes.None && ThemeResolutionService.AllowAnimations)
      {
        this.lastCloseReason = info.CloseReason;
        this.animationStateOpening = false;
        this.OpacityInternal = this.opacity;
        if (!this.fadeAnimationTimer.Enabled)
        {
          this.UpdateStyles();
          PopupManager.Default.RemovePopup((IPopupControl) this);
          this.fadeAnimationTimer.Start();
          this.OnPopupClosed(info);
        }
        else
          info.Closed = false;
      }
      else if (this.Visible)
      {
        PopupManager.Default.RemovePopup((IPopupControl) this);
        this.ClosePopupCore();
        this.OnPopupClosed(info);
      }
      else
        info.Closed = false;
    }

    protected virtual bool OnPopupClosing(PopupCloseInfo info)
    {
      RadPopupClosingEventArgs args = new RadPopupClosingEventArgs(info.CloseReason);
      this.OnPopupClosing(args);
      if (!args.Cancel)
        return true;
      info.Closed = false;
      return false;
    }

    protected virtual void OnPopupClosed(PopupCloseInfo info)
    {
      this.OnPopupClosed(new RadPopupClosedEventArgs(info.CloseReason));
    }

    public virtual void ClosePopup(RadPopupCloseReason reason)
    {
      this.ClosePopup(new PopupCloseInfo(reason, (object) null));
    }

    public virtual bool CanClosePopup(RadPopupCloseReason reason)
    {
      return true;
    }

    public virtual bool OnKeyDown(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Back:
        case Keys.Return:
        case Keys.Escape:
          this.ClosePopup(RadPopupCloseReason.Keyboard);
          return true;
        default:
          return false;
      }
    }

    public virtual bool OnMouseWheel(Control target, int delta)
    {
      return target == this;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public new event MouseEventHandler MouseWheel
    {
      add
      {
        this.Events.AddHandler(this.MouseWheelEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(this.MouseWheelEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void CallMouseWheel(MouseEventArgs e)
    {
      MouseEventHandler mouseEventHandler = (MouseEventHandler) this.Events[this.MouseWheelEventKey];
      if (mouseEventHandler == null)
        return;
      mouseEventHandler((object) this, e);
    }

    private void OnAnimatingEvent(object sender, AnimationEventArgs e)
    {
      if (this.Disposing || this.IsDisposed || !this.IsHandleCreated)
        return;
      lock (this)
      {
        if (this.Disposing || this.IsDisposed || !this.IsHandleCreated)
          return;
        this.BeginInvoke((Delegate) this.callbackAnimating, (object) e);
      }
    }

    private void OnAnimationFinishedEvent(object sender, AnimationEventArgs e)
    {
      if (this.Disposing || this.IsDisposed || !this.IsHandleCreated)
        return;
      lock (this)
      {
        if (this.Disposing || this.IsDisposed || !this.IsHandleCreated)
          return;
        this.BeginInvoke((Delegate) this.callbackAnimationFinished, (object) e);
      }
    }

    protected virtual void OnAnimationFinished(AnimationEventArgs args)
    {
      if (this.Disposing || this.IsDisposed || !this.IsHandleCreated)
        return;
      this.dropDownAnimating = false;
      if (this.shouldRestoreAutoSize)
        this.AutoSize = true;
      this.Size = this.backupSize;
      this.Invalidate();
    }

    protected virtual void OnAnimating(AnimationEventArgs e)
    {
      if (this.Disposing || this.IsDisposed || !this.IsHandleCreated)
        return;
      Rectangle animationValue = (Rectangle) e.AnimationValue;
      this.Location = animationValue.Location;
      this.Size = animationValue.Size;
      this.Invalidate();
    }

    public event RadPopupFadeAnimationFinishedEventHandler FadeAnimationFinished
    {
      add
      {
        this.Events.AddHandler(this.FadeAnimationFinishedKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(this.FadeAnimationFinishedKey, (Delegate) value);
      }
    }

    public event RadPopupOpeningEventHandler PopupOpening
    {
      add
      {
        this.Events.AddHandler(this.PopupOpeningEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(this.PopupOpeningEventKey, (Delegate) value);
      }
    }

    public event RadPopupOpenedEventHandler PopupOpened
    {
      add
      {
        this.Events.AddHandler(this.PopupOpenedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(this.PopupOpenedEventKey, (Delegate) value);
      }
    }

    public event RadPopupClosingEventHandler PopupClosing
    {
      add
      {
        this.Events.AddHandler(this.PopupClosingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(this.PopupClosingEventKey, (Delegate) value);
      }
    }

    public event RadPopupClosedEventHandler PopupClosed
    {
      add
      {
        this.Events.AddHandler(this.PopupClosedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(this.PopupClosedEventKey, (Delegate) value);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.animationEngine.Animating -= new AnimationEventHandler(this.OnAnimatingEvent);
        this.animationEngine.AnimationFinished -= new AnimationEventHandler(this.OnAnimationFinishedEvent);
        this.animationEngine.Dispose();
        if (this.ownerPopupInternal != null && this.ownerPopupInternal.Children.Contains((IPopupControl) this))
          this.ownerPopupInternal.Children.Remove((IPopupControl) this);
        PopupManager.Default.RemovePopup((IPopupControl) this);
        if (this.fadeAnimationTimer != null)
        {
          this.fadeAnimationTimer.Stop();
          this.fadeAnimationTimer.Tick -= new EventHandler(this.OnFadeAnimationTimer_Tick);
          this.fadeAnimationTimer.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    public class PopupAnimationProperties : INotifyPropertyChanged
    {
      private RadDirection animationDirection = RadDirection.Down;
      private WindowAnimationEngine animationEngine;

      internal PopupAnimationProperties(WindowAnimationEngine animationEngine)
      {
        this.animationEngine = animationEngine;
      }

      public RadDirection AnimationDirection
      {
        get
        {
          return this.animationDirection;
        }
        set
        {
          if (value == this.animationDirection)
            return;
          this.animationDirection = value;
          this.OnNotifyPropertyChanged(nameof (AnimationDirection));
        }
      }

      public int AnimationFrames
      {
        get
        {
          return this.animationEngine.AnimationFrames;
        }
        set
        {
          if (this.animationEngine.AnimationFrames == value)
            return;
          this.animationEngine.AnimationFrames = value;
          this.OnNotifyPropertyChanged(nameof (AnimationFrames));
        }
      }

      public RadEasingType EasingType
      {
        get
        {
          return this.animationEngine.EasingType;
        }
        set
        {
          if (this.animationEngine.EasingType == value)
            return;
          this.animationEngine.EasingType = value;
          this.OnNotifyPropertyChanged(nameof (EasingType));
        }
      }

      public int AnimationStep
      {
        get
        {
          return this.animationEngine.AnimationStep;
        }
      }

      public WindowAnimationEngine AnimationEngine
      {
        get
        {
          return this.animationEngine;
        }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnNotifyPropertyChanged(string propertyName)
      {
        if (this.PropertyChanged == null)
          return;
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
      }
    }
  }
}
