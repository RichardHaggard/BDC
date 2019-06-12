// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DesktopAlertPopup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class DesktopAlertPopup : RadPopupControlBase
  {
    private bool canMove = true;
    private int autoCloseDelayInSeconds = 10;
    private bool autoClose = true;
    private RadDesktopAlertElement alertElement;
    private bool isPinned;
    private int autoCloseSuspendCount;
    private Point oldMousePosition;
    private float? oldOpacity;
    private Timer autoCloseTimer;
    private RadDesktopAlert ownerAlert;
    internal bool locationModifiedByUser;

    public DesktopAlertPopup(RadDesktopAlert owner)
      : this(owner.DpiScaleElement, owner)
    {
    }

    public DesktopAlertPopup(RadElement element, RadDesktopAlert owner)
      : base(element)
    {
      this.ownerAlert = owner;
      this.FadeAnimationType = FadeAnimationType.FadeIn | FadeAnimationType.FadeOut;
      this.Opacity = 0.8f;
      this.FadeAnimationFrames = 80;
      this.AnimationFrames = 50;
      this.EasingType = RadEasingType.Default;
      this.LastShowDpiScaleFactor = SizeF.Empty;
    }

    [Description("Gets or sets a boolean value determining whether the alert's popup is automatically closed.")]
    [DefaultValue(true)]
    public bool AutoClose
    {
      get
      {
        return this.autoClose;
      }
      set
      {
        if (this.autoClose == value)
          return;
        this.autoClose = value;
        this.OnNotifyPropertyChanged(nameof (AutoClose));
      }
    }

    [Description("Gets or sets the amount of time in seconds after whichthe alert will be automatically closed.")]
    [DefaultValue(10)]
    public int AutoCloseDelay
    {
      get
      {
        return this.autoCloseDelayInSeconds;
      }
      set
      {
        if (value < 0)
          throw new ArgumentException("The auto-close delay must be non-negative.");
        if (this.autoCloseDelayInSeconds == value)
          return;
        this.autoCloseDelayInSeconds = value;
        this.OnNotifyPropertyChanged(nameof (AutoCloseDelay));
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a boolean value determining whether the options button is shown.")]
    public bool ShowOptionsButton
    {
      get
      {
        return this.alertElement.ShowOptionsButton;
      }
      set
      {
        this.alertElement.ShowOptionsButton = value;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a boolean value determining whether the pin button is shown.")]
    public bool ShowPinButton
    {
      get
      {
        return this.alertElement.ShowPinButton;
      }
      set
      {
        this.alertElement.ShowPinButton = value;
      }
    }

    [Description("Gets or sets a boolean value determining whether the close button is shown.")]
    [DefaultValue(true)]
    public bool ShowCloseButton
    {
      get
      {
        return this.alertElement.ShowCloseButton;
      }
      set
      {
        this.alertElement.ShowCloseButton = value;
      }
    }

    public bool IsPinned
    {
      get
      {
        return this.isPinned;
      }
      set
      {
        if (this.isPinned == value)
          return;
        this.isPinned = value;
        this.OnNotifyPropertyChanged(nameof (IsPinned));
      }
    }

    public bool CanMove
    {
      get
      {
        return this.canMove;
      }
      set
      {
        if (this.canMove == value)
          return;
        this.canMove = value;
        this.OnNotifyPropertyChanged(nameof (CanMove));
      }
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (DesktopAlertPopup).FullName;
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    public RadDesktopAlertElement AlertElement
    {
      get
      {
        return this.alertElement;
      }
    }

    [Description("Gets or sets the caption text of the desktop alert.")]
    public virtual string CaptionText
    {
      get
      {
        return this.alertElement.CaptionText;
      }
      set
      {
        this.alertElement.CaptionText = value;
      }
    }

    [Description("Gets or sets the content text of the desktop alert.")]
    public virtual string ContentText
    {
      get
      {
        return this.alertElement.ContentText;
      }
      set
      {
        this.alertElement.ContentText = value;
      }
    }

    [Description("Gets or sets the content image of the alert.")]
    public virtual Image Image
    {
      get
      {
        return this.alertElement.ContentImage;
      }
      set
      {
        this.alertElement.ContentImage = value;
      }
    }

    public RadItemCollection ButtonItems
    {
      get
      {
        return (RadItemCollection) this.alertElement.ButtonsPanel.Items;
      }
    }

    public RadItemCollection OptionItems
    {
      get
      {
        return (RadItemCollection) this.alertElement.CaptionElement.TextAndButtonsElement.OptionsButton.Items;
      }
    }

    private void EvaluateAutoCloseParameters()
    {
      if (!this.autoClose)
        return;
      if (this.autoCloseTimer == null)
      {
        this.autoCloseTimer = new Timer();
        this.autoCloseTimer.Tick += new EventHandler(this.OnAutoCloseTimer_Tick);
      }
      this.autoCloseTimer.Interval = this.autoCloseDelayInSeconds * 1000;
      this.autoCloseTimer.Stop();
      this.autoCloseTimer.Start();
    }

    public void SuspendAutoClose()
    {
      if (!PopupManager.Default.ContainsPopup((IPopupControl) this) || !this.autoClose || this.autoCloseTimer == null)
        return;
      if (this.autoCloseSuspendCount == 0)
        this.autoCloseTimer.Stop();
      ++this.autoCloseSuspendCount;
    }

    public void ResumeAutoClose()
    {
      if (!PopupManager.Default.ContainsPopup((IPopupControl) this) || !this.autoClose || this.autoCloseTimer == null)
        return;
      if (this.autoCloseSuspendCount > 0)
        --this.autoCloseSuspendCount;
      if (this.autoCloseSuspendCount != 0)
        return;
      this.autoCloseTimer.Start();
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      System.Type type = element.GetType();
      if (type.Equals(typeof (RadButtonElement)) || type.Equals(typeof (RadDropDownButtonElement)) || type.Equals(typeof (RadToggleButtonElement)))
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.alertElement = new RadDesktopAlertElement();
      this.alertElement.CaptionElement.TextAndButtonsElement.CloseButton.Click += new EventHandler(this.OnAlertCloseButton_Click);
      this.alertElement.CaptionElement.TextAndButtonsElement.PinButton.ToggleStateChanged += new StateChangedEventHandler(this.PinButton_ToggleStateChanged);
      this.alertElement.ThemeRole = "DesktopAlertElement";
      parent.Children.Add((RadElement) this.alertElement);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.autoCloseTimer != null)
        {
          this.autoCloseTimer.Tick -= new EventHandler(this.OnAutoCloseTimer_Tick);
          this.autoCloseTimer.Dispose();
          this.autoCloseTimer = (Timer) null;
        }
        this.alertElement.CaptionElement.TextAndButtonsElement.CloseButton.Click -= new EventHandler(this.OnAlertCloseButton_Click);
        this.alertElement.CaptionElement.TextAndButtonsElement.PinButton.ToggleStateChanged -= new StateChangedEventHandler(this.PinButton_ToggleStateChanged);
      }
      base.Dispose(disposing);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (!this.canMove)
      {
        base.OnMouseMove(e);
      }
      else
      {
        if (Control.MouseButtons == MouseButtons.Left && this.alertElement.CaptionElement.CaptionGrip.ControlBoundingRectangle.Contains(this.oldMousePosition))
        {
          Point point = Point.Subtract(Control.MousePosition, new Size(this.oldMousePosition));
          if (!this.locationModifiedByUser && point != this.Location)
          {
            this.locationModifiedByUser = true;
            DesktopAlertManager.Instance.UpdateAlertsOrder();
          }
          this.Location = point;
        }
        base.OnMouseMove(e);
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.oldMousePosition = e.Location;
      base.OnMouseDown(e);
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      return false;
    }

    public override bool OnKeyDown(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Back:
        case Keys.Return:
        case Keys.Escape:
          return false;
        default:
          return base.OnKeyDown(keyData);
      }
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName == "IsPinned")
      {
        this.alertElement.CaptionElement.TextAndButtonsElement.PinButton.ToggleState = this.isPinned ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      }
      else
      {
        if (!(propertyName == "AnimationEnabled") || !(this.LastShowDpiScaleFactor == SizeF.Empty))
          return;
        this.LastShowDpiScaleFactor = new SizeF(1f, 1f);
      }
    }

    private void PinButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
      {
        this.IsPinned = true;
        this.SuspendAutoClose();
      }
      else
      {
        this.IsPinned = false;
        this.ResumeAutoClose();
      }
    }

    protected override void OnPopupOpening(CancelEventArgs args)
    {
      base.OnPopupOpening(args);
      this.autoCloseSuspendCount = 0;
      this.EvaluateAutoCloseParameters();
    }

    private void OnAutoCloseTimer_Tick(object sender, EventArgs e)
    {
      this.autoCloseTimer.Stop();
      if (this.isPinned)
        return;
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (PopupManager.Default.ContainsPopup((IPopupControl) this))
      {
        this.SuspendAutoClose();
        this.oldOpacity = new float?(this.Opacity);
        this.Opacity = 1f;
      }
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (PopupManager.Default.ContainsPopup((IPopupControl) this))
      {
        this.ResumeAutoClose();
        if (this.oldOpacity.HasValue)
        {
          this.Opacity = this.oldOpacity.Value;
          this.oldOpacity = new float?();
        }
      }
      base.OnMouseLeave(e);
    }

    private void OnAlertCloseButton_Click(object sender, EventArgs e)
    {
      this.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    protected override void ShowPopupCore(Size size, Point location)
    {
      SizeF dpiScaleFactor = this.ownerAlert.DpiScaleElement.DpiScaleFactor;
      bool isEmpty = this.LastShowDpiScaleFactor.IsEmpty;
      if (this.LastShowDpiScaleFactor != dpiScaleFactor)
      {
        if (isEmpty)
          this.LastShowDpiScaleFactor = new SizeF(1f, 1f);
        SizeF factor = new SizeF(dpiScaleFactor.Width / this.LastShowDpiScaleFactor.Width, dpiScaleFactor.Height / this.LastShowDpiScaleFactor.Height);
        this.LastShowDpiScaleFactor = dpiScaleFactor;
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

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      base.SetBoundsCore(x, y, width, height, specified);
    }
  }
}
