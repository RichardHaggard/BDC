// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDesktopAlert
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Media;
using System.Windows.Forms;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadDesktopAlertDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [DesignTimeVisible(true)]
  [ToolboxItem(true)]
  [Description("This class represents a Desktop Alert component which can be used to display a small window on the screen to notify the user that an event occurred. The location of the window, the way it appears,as well as its content can be customized.")]
  public class RadDesktopAlert : RadComponent
  {
    private static readonly object OpeningEventKey = new object();
    private static readonly object OpenedEventKey = new object();
    private static readonly object ClosingEventKey = new object();
    private static readonly object ClosedEventKey = new object();
    private static readonly Size DefaultSize = new Size(329, 95);
    private Size fixedSize = Size.Empty;
    private AlertScreenPosition screenPosition = AlertScreenPosition.BottomRight;
    private DesktopAlertPopup popup;
    private bool playSound;
    private SystemSound soundToPlay;
    private bool autoSize;
    private RadElement dpiScaleElement;

    public RadDesktopAlert()
    {
      this.dpiScaleElement = new RadElement();
      this.popup = this.CreatePopup();
      this.WireEvents();
    }

    public RadDesktopAlert(IContainer container)
      : this()
    {
      container.Add((IComponent) this);
    }

    protected override void DisposeManagedResources()
    {
      if (DesktopAlertManager.Instance.ContainsAlert(this))
        DesktopAlertManager.Instance.RemoveAlert(this);
      this.UnwireEvents();
      this.Hide();
      this.popup.Dispose();
      base.DisposeManagedResources();
    }

    private void WireEvents()
    {
      this.popup.PopupClosing += new RadPopupClosingEventHandler(this.OnPopup_Closing);
      this.popup.PopupClosed += new RadPopupClosedEventHandler(this.OnPopup_Closed);
      this.popup.PopupOpened += new RadPopupOpenedEventHandler(this.OnPopup_Opened);
      this.popup.VisibleChanged += new EventHandler(this.OnPopup_VisibleChanged);
    }

    private void UnwireEvents()
    {
      this.popup.PopupClosing -= new RadPopupClosingEventHandler(this.OnPopup_Closing);
      this.popup.PopupClosed -= new RadPopupClosedEventHandler(this.OnPopup_Closed);
      this.popup.PopupOpened -= new RadPopupOpenedEventHandler(this.OnPopup_Opened);
      this.popup.VisibleChanged -= new EventHandler(this.OnPopup_VisibleChanged);
    }

    protected virtual DesktopAlertPopup CreatePopup()
    {
      return new DesktopAlertPopup(this);
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether the control is automatically resized to display its entire contents by Height.")]
    [Browsable(true)]
    [Category("Layout")]
    [DefaultValue(false)]
    public virtual bool AutoSize
    {
      get
      {
        return this.autoSize;
      }
      set
      {
        if (this.autoSize == value)
          return;
        this.autoSize = value;
        this.popup.AlertElement.AutoSizeHeight = value;
        this.popup.AlertElement.InvalidateMeasure();
        this.OnNotifyPropertyChanged(nameof (AutoSize));
      }
    }

    [DefaultValue(RightToLeft.No)]
    [AmbientValue(0)]
    [Localizable(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.")]
    public virtual RightToLeft RightToLeft
    {
      get
      {
        return this.popup.RightToLeft;
      }
      set
      {
        this.popup.RightToLeft = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool PlaySound
    {
      get
      {
        return this.playSound;
      }
      set
      {
        if (this.playSound == value)
          return;
        this.playSound = value;
        this.OnNotifyPropertyChanged(nameof (PlaySound));
      }
    }

    [Browsable(false)]
    [DefaultValue(null)]
    public SystemSound SoundToPlay
    {
      get
      {
        return this.soundToPlay;
      }
      set
      {
        if (this.soundToPlay == value)
          return;
        this.soundToPlay = value;
        this.OnNotifyPropertyChanged(nameof (SoundToPlay));
      }
    }

    [Description("Gets or sets the initial opacity of the alert's popup.")]
    [DefaultValue(0.8f)]
    [Category("Appearance")]
    public float Opacity
    {
      get
      {
        return this.popup.Opacity;
      }
      set
      {
        this.popup.Opacity = value;
      }
    }

    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a boolean value determining whether the options button is shown.")]
    public bool ShowOptionsButton
    {
      get
      {
        return this.popup.ShowOptionsButton;
      }
      set
      {
        this.popup.ShowOptionsButton = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets a boolean value determining whether the pin button is shown.")]
    [DefaultValue(true)]
    public bool ShowPinButton
    {
      get
      {
        return this.popup.ShowPinButton;
      }
      set
      {
        this.popup.ShowPinButton = value;
      }
    }

    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a boolean value determining whether the close button is shown.")]
    public bool ShowCloseButton
    {
      get
      {
        return this.popup.ShowCloseButton;
      }
      set
      {
        this.popup.ShowCloseButton = value;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a boolean value determining whether the alert's popup will be pinned on the screen. If pinned, the alert's popup will not be automatically closed upon mouse click outside its bounds or if the AutoClose property is set to true.")]
    public bool IsPinned
    {
      get
      {
        return this.popup.IsPinned;
      }
      set
      {
        this.popup.IsPinned = value;
      }
    }

    [Description("Gets or sets a boolean value determining whether the popup can be moved by dragging the caption grip.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool CanMove
    {
      get
      {
        return this.popup.CanMove;
      }
      set
      {
        this.popup.CanMove = value;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a boolean value determining whether the alert's popup will be animated.")]
    [Category("Behavior")]
    public bool PopupAnimation
    {
      get
      {
        return this.popup.AnimationEnabled;
      }
      set
      {
        this.popup.AnimationEnabled = value;
      }
    }

    [DefaultValue(typeof (RadDirection), "Down")]
    [Description("Gets or sets a value determining the direction of the alert's popup animation.")]
    [Category("Behavior")]
    public RadDirection PopupAnimationDirection
    {
      get
      {
        return this.popup.DropDownAnimationDirection;
      }
      set
      {
        this.popup.DropDownAnimationDirection = value;
      }
    }

    [DefaultValue(50)]
    [Description("Gets or sets the count of the alert's drop-down animation frames.")]
    [Category("Behavior")]
    public int PopupAnimationFrames
    {
      get
      {
        return this.popup.AnimationFrames;
      }
      set
      {
        this.popup.AnimationFrames = value;
      }
    }

    [DefaultValue(typeof (RadEasingType), "Default")]
    [Category("Behavior")]
    [Description("Gets or sets the type of the drop-down animation easing.")]
    public RadEasingType PopupAnimationEasing
    {
      get
      {
        return this.popup.EasingType;
      }
      set
      {
        this.popup.EasingType = value;
      }
    }

    [DefaultValue(typeof (FadeAnimationType), "FadeIn, FadeOut")]
    [Description("Gets or sets a value from the FadeAnimationType enumerator that determines the type of fade animation performed when the alert's popup is opened/closed")]
    [Category("Behavior")]
    [Editor(typeof (FadeAnimationTypeEditor), typeof (UITypeEditor))]
    public FadeAnimationType FadeAnimationType
    {
      get
      {
        return this.popup.FadeAnimationType;
      }
      set
      {
        this.popup.FadeAnimationType = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(10)]
    [Description("Gets or sets the interval in milliseconds between two animation frames.")]
    public int FadeAnimationSpeed
    {
      get
      {
        return this.popup.FadeAnimationSpeed;
      }
      set
      {
        this.popup.FadeAnimationSpeed = value;
      }
    }

    [Description("Gets or sets the count of animation frames for the fade animation.")]
    [DefaultValue(80)]
    [Category("Behavior")]
    public int FadeAnimationFrames
    {
      get
      {
        return this.popup.FadeAnimationFrames;
      }
      set
      {
        this.popup.FadeAnimationFrames = value;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a boolean value determining whether the alert's popup is automatically closed.")]
    [Category("Behavior")]
    public bool AutoClose
    {
      get
      {
        return this.popup.AutoClose;
      }
      set
      {
        this.popup.AutoClose = value;
      }
    }

    [DefaultValue(10)]
    [Category("Behavior")]
    [Description("Gets or sets the amount of time in seconds after whichthe alert will be automatically closed.")]
    public int AutoCloseDelay
    {
      get
      {
        return this.popup.AutoCloseDelay;
      }
      set
      {
        this.popup.AutoCloseDelay = value;
      }
    }

    [Description("Gets or sets the position of the alert popup on the working area of the active screen.")]
    [Category("Appearance")]
    [DefaultValue(typeof (AlertScreenPosition), "BottomRight")]
    public virtual AlertScreenPosition ScreenPosition
    {
      get
      {
        return this.screenPosition;
      }
      set
      {
        if (this.screenPosition == value)
          return;
        this.screenPosition = value;
        this.OnNotifyPropertyChanged(nameof (ScreenPosition));
      }
    }

    [DefaultValue(typeof (Size), "0, 0")]
    [Category("Appearance")]
    [Description("Gets or sets the fixed size for the alert's popup. If the value is Size.Empty, the size of the popup is dynamically adjusted according to its content.")]
    public Size FixedSize
    {
      get
      {
        return this.fixedSize;
      }
      set
      {
        if (value.Height < 0 || value.Width < 0)
          throw new ArgumentException("The fixed size cannot have negative metrics.");
        if (!(this.fixedSize != value))
          return;
        this.fixedSize = value;
        this.OnNotifyPropertyChanged(nameof (FixedSize));
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets the content image of the alert.")]
    [Category("Appearance")]
    public Image ContentImage
    {
      get
      {
        return this.popup.Image;
      }
      set
      {
        if (this.popup.Image == value)
          return;
        this.popup.Image = value;
        this.UpdateSizeIfPopupIsShown();
      }
    }

    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("Gets or sets the alert's content text. This text can be HTML formatted for better appearance.")]
    [Category("Appearance")]
    public string ContentText
    {
      get
      {
        return this.popup.ContentText;
      }
      set
      {
        if (!(this.popup.ContentText != value))
          return;
        this.popup.ContentText = value;
        this.UpdateSizeIfPopupIsShown();
      }
    }

    private void UpdateSizeIfPopupIsShown()
    {
      if (!PopupManager.Default.ContainsPopup((IPopupControl) this.popup))
        return;
      bool animationEnabled = this.popup.AnimationEnabled;
      this.popup.AnimationEnabled = false;
      this.Hide();
      this.Show();
      this.popup.AnimationEnabled = animationEnabled;
    }

    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets or sets the alert's caption text.")]
    [Category("Appearance")]
    [DefaultValue("")]
    public string CaptionText
    {
      get
      {
        return this.popup.CaptionText;
      }
      set
      {
        this.popup.CaptionText = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection that holds the button items added to the alert.")]
    public RadItemCollection ButtonItems
    {
      get
      {
        return this.popup.ButtonItems;
      }
    }

    [Description("Gets the collection that holds the option items added to the alert's options button.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemCollection OptionItems
    {
      get
      {
        return this.popup.OptionItems;
      }
    }

    [Browsable(false)]
    public DesktopAlertPopup Popup
    {
      get
      {
        return this.popup;
      }
    }

    public RadElement DpiScaleElement
    {
      get
      {
        return this.dpiScaleElement;
      }
    }

    public event RadPopupOpeningEventHandler Opening
    {
      add
      {
        this.Events.AddHandler(RadDesktopAlert.OpeningEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDesktopAlert.OpeningEventKey, (Delegate) value);
      }
    }

    public event RadPopupOpenedEventHandler Opened
    {
      add
      {
        this.Events.AddHandler(RadDesktopAlert.OpenedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDesktopAlert.OpenedEventKey, (Delegate) value);
      }
    }

    public event RadPopupClosingEventHandler Closing
    {
      add
      {
        this.Events.AddHandler(RadDesktopAlert.ClosingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDesktopAlert.ClosingEventKey, (Delegate) value);
      }
    }

    public event RadPopupClosedEventHandler Closed
    {
      add
      {
        this.Events.AddHandler(RadDesktopAlert.ClosedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDesktopAlert.ClosedEventKey, (Delegate) value);
      }
    }

    public void Show()
    {
      if (DesktopAlertManager.Instance.ContainsAlert(this))
        return;
      RadPopupOpeningEventArgs args = new RadPopupOpeningEventArgs(this.popup.Location);
      this.OnPopupOpening((object) this, args);
      if (args.Cancel)
        return;
      SizeF monitorDpi = Telerik.WinControls.NativeMethods.GetMonitorDpi(Screen.FromPoint(this.Popup.Location), Telerik.WinControls.NativeMethods.DpiType.Effective);
      this.DpiScaleElement.DpiScaleChanged(monitorDpi);
      Size size = this.GetPopupSize();
      if (this.Popup.LastShowDpiScaleFactor.IsEmpty)
        size = TelerikDpiHelper.ScaleSize(size, new SizeF(1f / monitorDpi.Width, 1f / monitorDpi.Height));
      this.popup.Size = size;
      DesktopAlertManager.Instance.AddAlert(this);
      this.popup.Show(this.popup.Location);
      if (!PopupManager.Default.ContainsPopup((IPopupControl) this.popup) || !this.playSound || this.soundToPlay == null)
        return;
      this.soundToPlay.Play();
    }

    public void Hide()
    {
      this.popup.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    public void ResetLocationModifier()
    {
      this.popup.locationModifiedByUser = false;
    }

    public override IComponentTreeHandler GetOwnedTreeHandler()
    {
      return (IComponentTreeHandler) this.popup;
    }

    protected virtual Size GetPopupSize()
    {
      Size defaultSize = RadDesktopAlert.DefaultSize;
      SizeF scaleFactor = RadControl.EnableDpiScaling ? Telerik.WinControls.NativeMethods.GetMonitorDpi(Screen.FromPoint(this.Popup.Location), Telerik.WinControls.NativeMethods.DpiType.Effective) : new SizeF(1f, 1f);
      if (this.fixedSize != Size.Empty)
      {
        if (this.FixedSize.Width == 0 || (this.FixedSize.Height != 0 || !this.AutoSize))
          return TelerikDpiHelper.ScaleSize(this.fixedSize, scaleFactor);
        defaultSize.Width = this.FixedSize.Width;
      }
      Size desiredSize = new Size(defaultSize.Width, defaultSize.Height);
      this.popup.Size = desiredSize;
      this.popup.LoadElementTree(desiredSize);
      SizeF sizeF = new SizeF((float) defaultSize.Width, this.popup.AlertElement.DesiredSize.Height);
      sizeF = TelerikDpiHelper.ScaleSizeF(sizeF, scaleFactor);
      return sizeF.ToSize();
    }

    protected internal virtual bool GetLocationModifiedByUser()
    {
      return this.popup.locationModifiedByUser;
    }

    protected internal virtual void OnLocationChangeRequested(Point alertLocation)
    {
      if (this.popup.locationModifiedByUser || !(this.popup.Location != alertLocation))
        return;
      this.popup.Location = alertLocation;
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      if (propertyName == "ThemeName")
        this.popup.ThemeName = this.ThemeName;
      base.OnNotifyPropertyChanged(propertyName);
    }

    protected virtual void OnPopupClosing(object sender, RadPopupClosingEventArgs args)
    {
      RadPopupClosingEventHandler closingEventHandler = this.Events[RadDesktopAlert.ClosingEventKey] as RadPopupClosingEventHandler;
      if (closingEventHandler == null)
        return;
      closingEventHandler((object) this, args);
    }

    protected virtual void OnPopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      RadPopupClosedEventHandler closedEventHandler = this.Events[RadDesktopAlert.ClosedEventKey] as RadPopupClosedEventHandler;
      if (closedEventHandler == null)
        return;
      closedEventHandler((object) this, args);
    }

    protected virtual void OnPopupOpening(object sender, RadPopupOpeningEventArgs args)
    {
      RadPopupOpeningEventHandler openingEventHandler = this.Events[RadDesktopAlert.OpeningEventKey] as RadPopupOpeningEventHandler;
      if (openingEventHandler != null)
        openingEventHandler((object) this, (CancelEventArgs) args);
      if (!args.Cancel)
        return;
      DesktopAlertManager.Instance.RemoveAlert(this);
    }

    protected virtual void OnPopupOpened(object sender, EventArgs args)
    {
      RadPopupOpenedEventHandler openedEventHandler = this.Events[RadDesktopAlert.OpenedEventKey] as RadPopupOpenedEventHandler;
      if (openedEventHandler == null)
        return;
      openedEventHandler((object) this, args);
    }

    private void OnPopup_VisibleChanged(object sender, EventArgs e)
    {
      if (this.popup.Visible)
        return;
      DesktopAlertManager.Instance.RemoveAlert(this);
    }

    private void OnPopup_Closing(object sender, RadPopupClosingEventArgs args)
    {
      this.OnPopupClosing(sender, args);
    }

    private void OnPopup_Opened(object sender, EventArgs args)
    {
      this.OnPopupOpened(sender, args);
    }

    private void OnPopup_Closed(object sender, RadPopupClosedEventArgs args)
    {
      this.OnPopupClosed(sender, args);
    }
  }
}
