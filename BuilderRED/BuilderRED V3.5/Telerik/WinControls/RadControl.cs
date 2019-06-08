// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Keyboard;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls
{
  [Designer("Telerik.WinControls.UI.Design.RadControlDesignerLite, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [TypeDescriptionProvider(typeof (ReplaceRadControlProvider))]
  [DesignerSerializer("Telerik.WinControls.UI.Design.RadControlCodeDomSerializer, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [ToolboxItem(false)]
  public class RadControl : ScrollableControl, INotifyPropertyChanged, ISupportInitializeNotification, ISupportInitialize, IComponentTreeHandler, ILayoutHandler, IPCHost, IAnalyticsProvider
  {
    private bool isFocusable = true;
    private bool enableGestures = true;
    private Size imageScalingSize = Size.Empty;
    private Size smallImageScalingSize = Size.Empty;
    private bool enableAnalytics = true;
    private bool enableRadAccessibilityObjects = true;
    private string analyticsName = string.Empty;
    private bool isDefaultBackColor = true;
    private bool isDefaultForeColor = true;
    private static readonly bool isTrial = false;
    private static bool enableCodedUITestsDefaultValue = false;
    private static bool useCompatibleTextRenderingDefaultValue = true;
    private static bool enableDpiScaling = true;
    private static readonly object ToolTipTextNeededEventKey = new object();
    private static readonly object PropertyChangedEventKey = new object();
    private static readonly object ScreenTipNeededEventKey = new object();
    private static readonly object InvalidatedEventKey = new object();
    private ComponentThemableElementTree elementTree;
    private ComponentInputBehavior behavior;
    private bool isInitializing;
    private bool loaded;
    private bool loading;
    private bool isPreferredSizeValid;
    protected bool isResizing2;
    protected bool isDisposing2;
    private bool isDisplayed;
    private bool isInitialized;
    private bool isPainting;
    private bool? enableCodedUITests;
    protected Rectangle invalidResizeRect;
    private ImageList imageList;
    private ContextLayoutManager layoutManager;
    private ImageList smallImageList;
    private int suspendUpdateCounter;
    private IntPtr ipcContext;
    protected bool isAccessibilityRequested;
    private bool helpSent;
    private GestureEventArgs lastEvent;
    private GestureEventArgs lastBeginEvent;
    private ulong lastEventArguments;
    private ulong lastBeginEventArguments;

    public RadControl()
    {
      MeasurementGraphics.IncreaseControlCount();
      this.Construct();
      this.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.smallImageScalingSize = new Size(16, 16);
      this.imageScalingSize = new Size(16, 16);
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.isDisposing2 = true;
      if (!disposing)
        return;
      this.SuspendLayout();
      ChordMessageFilter.UnregisterKeyTipsConsumer((IComponentTreeHandler) this);
      if (this.elementTree != null)
        this.elementTree.Dispose();
      if (this.layoutManager != null)
        this.layoutManager.Dispose();
      if (this.Region != null)
        this.Region.Dispose();
      MeasurementGraphics.DecreaseControlCount();
      this.Behavior.Dispose();
    }

    protected virtual void Construct()
    {
      this.layoutManager = new ContextLayoutManager((ILayoutHandler) this);
      this.behavior = this.CreateBehavior();
      this.elementTree = new ComponentThemableElementTree((IComponentTreeHandler) this);
      this.elementTree.InitializeRootElement();
      this.elementTree.RootElement.SetChildrenLocalValuesAsDefault(true);
    }

    protected virtual ComponentInputBehavior CreateBehavior()
    {
      return new ComponentInputBehavior((IComponentTreeHandler) this);
    }

    protected virtual RootRadElement CreateRootElement()
    {
      return new RootRadElement();
    }

    protected virtual void CreateChildItems(RadElement parent)
    {
      this.RootElement.Name = this.Name;
    }

    protected virtual void InitializeRootElement(RootRadElement rootElement)
    {
    }

    public virtual void LoadElementTree()
    {
      this.LoadElementTree(this.Size);
    }

    public virtual void LoadElementTree(Size desiredSize)
    {
      if (this.loaded)
        return;
      this.OnLoad(desiredSize);
    }

    public static bool EnableDpiScaling
    {
      get
      {
        return RadControl.enableDpiScaling;
      }
      set
      {
        RadControl.enableDpiScaling = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public static bool IsTrial
    {
      get
      {
        return RadControl.isTrial;
      }
    }

    [Browsable(false)]
    public bool IsLoaded
    {
      get
      {
        return this.loaded;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public ILayoutManager LayoutManager
    {
      get
      {
        return (ILayoutManager) this.layoutManager;
      }
    }

    [Browsable(false)]
    public virtual ComponentThemableElementTree ElementTree
    {
      get
      {
        return this.elementTree;
      }
    }

    [Description("Set or get the default value for UseCompatibleTextRendering property. ")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public static bool UseCompatibleTextRenderingDefaultValue
    {
      get
      {
        return RadControl.useCompatibleTextRenderingDefaultValue;
      }
      set
      {
        RadControl.useCompatibleTextRenderingDefaultValue = value;
      }
    }

    [Browsable(false)]
    public ComponentInputBehavior Behavior
    {
      get
      {
        return this.behavior;
      }
    }

    [Category("Data")]
    [Description("Gets the RootElement of a Control.")]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public RootRadElement RootElement
    {
      get
      {
        if (this.elementTree == null)
          return (RootRadElement) null;
        return this.elementTree.RootElement;
      }
    }

    [DefaultValue(typeof (Padding), "0, 0, 0, 0")]
    [Localizable(true)]
    [Description("ControlPadding")]
    [Category("Layout")]
    public new Padding Padding
    {
      get
      {
        return base.Padding;
      }
      set
      {
        base.Padding = value;
      }
    }

    [Category("StyleSheet")]
    [DefaultValue("")]
    [Editor("Telerik.WinControls.UI.Design.ThemeNameEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Browsable(true)]
    [Description("Gets or sets theme name.")]
    public virtual string ThemeName
    {
      get
      {
        return this.elementTree.ThemeName;
      }
      set
      {
        if (!(this.elementTree.ThemeName != value))
          return;
        this.elementTree.ThemeName = value;
        this.OnNotifyPropertyChanged(nameof (ThemeName));
      }
    }

    [Description("Gets or sets the text associated with this control.")]
    [SettingsBindable(true)]
    [DefaultValue("")]
    [Localizable(true)]
    [Bindable(true)]
    [Browsable(true)]
    [Category("Behavior")]
    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    [Category("StyleSheet")]
    [DefaultValue(true)]
    public bool EnableTheming
    {
      get
      {
        return this.ElementTree.EnableTheming;
      }
      set
      {
        this.ElementTree.EnableTheming = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("StyleSheet")]
    public virtual string ThemeClassName
    {
      get
      {
        if (this.elementTree == null)
          throw new ObjectDisposedException("Attempting to accept already disposed control");
        return this.elementTree.ThemeClassName;
      }
      set
      {
        if (this.elementTree.ThemeClassName == value)
          return;
        this.elementTree.ThemeClassName = value;
        this.OnNotifyPropertyChanged(nameof (ThemeClassName));
      }
    }

    [DefaultValue(null)]
    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets the ImageList that contains the images displayed by this control.")]
    public virtual ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        if (this.imageList == value)
          return;
        EventHandler eventHandler1 = new EventHandler(this.ImageList_RecreateHandle);
        EventHandler eventHandler2 = new EventHandler(this.ImageList_Disposed);
        if (this.imageList != null)
        {
          this.imageList.RecreateHandle -= eventHandler1;
          this.imageList.Disposed -= eventHandler2;
        }
        this.imageList = value;
        this.OnNotifyPropertyChanged(nameof (ImageList));
        if (this.imageList != null)
        {
          this.imageList.RecreateHandle += eventHandler1;
          this.imageList.Disposed += eventHandler2;
        }
        this.elementTree.RootElement.NotifyControlImageListChanged();
        ((IComponentTreeHandler) this).InvalidateIfNotSuspended();
      }
    }

    [Browsable(false)]
    [Category("Appearance")]
    [DefaultValue(typeof (Size), "16,16")]
    public Size ImageScalingSize
    {
      get
      {
        return this.imageScalingSize;
      }
      set
      {
        if (!(this.imageScalingSize != value))
          return;
        this.imageScalingSize = value;
        this.OnNotifyPropertyChanged(nameof (ImageScalingSize));
      }
    }

    [Description("Determines whether to use compatible text rendering engine (GDI+) or not (GDI).")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public virtual bool UseCompatibleTextRendering
    {
      get
      {
        return this.RootElement.UseCompatibleTextRendering;
      }
      set
      {
        this.RootElement.UseCompatibleTextRendering = value;
      }
    }

    [Description("Gets or sets a value indicating whether the control is automatically resized to display its entire contents.")]
    [DefaultValue(false)]
    [Category("Layout")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        if (this.AutoSize == value)
          return;
        this.ProcessAutoSizeChanged(value);
        this.RootElement.InvalidateMeasure();
        base.AutoSize = value;
        this.OnNotifyPropertyChanged(nameof (AutoSize));
      }
    }

    public override Size MaximumSize
    {
      get
      {
        return this.RootElement.MaxSize;
      }
      set
      {
        base.MaximumSize = value;
        this.RootElement.MaxSize = value;
      }
    }

    public override Size MinimumSize
    {
      get
      {
        return this.RootElement.MinSize;
      }
      set
      {
        base.MinimumSize = value;
        this.RootElement.MinSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool Focusable
    {
      get
      {
        return this.isFocusable;
      }
      set
      {
        this.isFocusable = value;
      }
    }

    public override ISite Site
    {
      get
      {
        return base.Site;
      }
      set
      {
        base.Site = value;
        ISite site = this.Site;
        if (site != null && site.DesignMode)
        {
          if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            LicenseManager.Validate(this.GetType());
          this.RootElement.SetIsDesignMode(true, true);
        }
        else
          this.RootElement.SetIsDesignMode(false, true);
      }
    }

    [Description("Gets or sets a value indicating whether the control causes validation to be performed on any controls that require validation when it receives focus.")]
    public new bool CausesValidation
    {
      get
      {
        if (this.ContainsFocus || this.Focused)
          return false;
        return base.CausesValidation;
      }
      set
      {
        base.CausesValidation = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsInitializing
    {
      get
      {
        return this.isInitializing;
      }
      internal set
      {
        this.isInitializing = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetIsInitializing(bool value)
    {
      this.isInitialized = value;
    }

    [DefaultValue(null)]
    [Description("Gets or sets the ImageList that contains the images displayed by this control.")]
    [Category("Appearance")]
    [Browsable(true)]
    public virtual ImageList SmallImageList
    {
      get
      {
        return this.smallImageList;
      }
      set
      {
        if (this.smallImageList == value)
          return;
        EventHandler eventHandler1 = new EventHandler(this.ImageList_RecreateHandle);
        EventHandler eventHandler2 = new EventHandler(this.SmallImageList_Disposed);
        if (this.smallImageList != null)
        {
          this.smallImageList.RecreateHandle -= eventHandler1;
          this.smallImageList.Disposed -= eventHandler2;
        }
        this.smallImageList = value;
        if (this.smallImageList != null)
        {
          this.smallImageList.RecreateHandle += eventHandler1;
          this.smallImageList.Disposed += eventHandler2;
        }
        this.elementTree.RootElement.NotifyControlImageListChanged();
        ((IComponentTreeHandler) this).InvalidateIfNotSuspended();
      }
    }

    [Browsable(false)]
    [Category("Appearance")]
    [DefaultValue(typeof (Size), "16,16")]
    public Size SmallImageScalingSize
    {
      get
      {
        return this.smallImageScalingSize;
      }
      set
      {
        this.smallImageScalingSize = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public bool IsDesignMode
    {
      get
      {
        return this.DesignMode;
      }
    }

    [Browsable(false)]
    public bool IsDisplayed
    {
      get
      {
        return this.isDisplayed;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadElement FocusedElement
    {
      get
      {
        return this.Behavior.FocusedElement;
      }
      set
      {
        this.Behavior.FocusedElement = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Accessibility")]
    [Description("Indicates focus cues display, when available, based on the corresponding control type and the current UI state.")]
    public virtual bool AllowShowFocusCues
    {
      get
      {
        return this.Behavior.AllowShowFocusCues;
      }
      set
      {
        this.Behavior.AllowShowFocusCues = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether ToolTips are shown for the RadItem objects contained in the RadControl.")]
    [DefaultValue(true)]
    public virtual bool ShowItemToolTips
    {
      get
      {
        return this.Behavior.ShowItemToolTips;
      }
      set
      {
        this.Behavior.ShowItemToolTips = value;
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public InputBindingsCollection CommandBindings
    {
      get
      {
        return this.Behavior.Shortcuts.InputBindings;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    public bool EnableKeyMap
    {
      get
      {
        return this.Behavior.EnableKeyTips;
      }
      set
      {
        this.Behavior.EnableKeyTips = value;
      }
    }

    private bool IsUpdateSuspended
    {
      get
      {
        return this.suspendUpdateCounter > 0;
      }
    }

    [Description("Gets or sets the BackColor of the control. This is actually the BackColor property of the root element.")]
    public override Color BackColor
    {
      get
      {
        return this.elementTree.RootElement.BackColor;
      }
      set
      {
        if (value == Color.Empty)
        {
          int num = (int) this.elementTree.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
          this.isDefaultBackColor = true;
          this.ResetBackColorThemeOverrides();
        }
        else
        {
          this.elementTree.RootElement.BackColor = value;
          this.isDefaultBackColor = false;
          this.SetBackColorThemeOverrides();
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeBackColor()
    {
      return this.ShouldSerializeProperty(VisualElement.BackColorProperty);
    }

    protected virtual void SetBackColorThemeOverrides()
    {
    }

    protected virtual void ResetBackColorThemeOverrides()
    {
    }

    [Description("Gets or sets the ForeColor of the control. This is actually the ForeColor property of the root element.")]
    public override Color ForeColor
    {
      get
      {
        return this.elementTree.RootElement.ForeColor;
      }
      set
      {
        if (value == Color.Empty)
        {
          int num = (int) this.elementTree.RootElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
          this.isDefaultForeColor = true;
          this.ResetForeColorThemeOverrides();
        }
        else
        {
          this.elementTree.RootElement.ForeColor = value;
          this.isDefaultForeColor = false;
          this.SetForeColorThemeOverrides();
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeForeColor()
    {
      return this.ShouldSerializeProperty(VisualElement.ForeColorProperty);
    }

    protected virtual void SetForeColorThemeOverrides()
    {
    }

    protected virtual void ResetForeColorThemeOverrides()
    {
    }

    [Description("Gets or sets the Font of the control. This is actually the Font property of the root element.")]
    public override Font Font
    {
      get
      {
        return this.elementTree.RootElement.Font;
      }
      set
      {
        if (value == null)
        {
          int num = (int) this.elementTree.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        }
        else
          this.elementTree.RootElement.Font = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeFont()
    {
      return this.ShouldSerializeProperty(VisualElement.FontProperty);
    }

    public event EventHandler ElementInvalidated
    {
      add
      {
        this.Events.AddHandler(RadControl.InvalidatedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadControl.InvalidatedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when a RadItem instance inside the RadControl requires ToolTip text. ")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Behavior")]
    public event ToolTipTextNeededEventHandler ToolTipTextNeeded
    {
      add
      {
        this.Events.AddHandler(RadControl.ToolTipTextNeededEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadControl.ToolTipTextNeededEventKey, (Delegate) value);
      }
    }

    [Description("Occurs prior the ScreenTip of a RadItem instance inside the RadControl is displayed.")]
    [Category("Behavior")]
    public event ScreenTipNeededEventHandler ScreenTipNeeded
    {
      add
      {
        this.Events.AddHandler(RadControl.ScreenTipNeededEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadControl.ScreenTipNeededEventKey, (Delegate) value);
      }
    }

    public event ThemeNameChangedEventHandler ThemeNameChanged;

    private void ImageList_RecreateHandle(object sender, EventArgs e)
    {
      if (!this.IsHandleCreated)
        return;
      ((IComponentTreeHandler) this).InvalidateIfNotSuspended();
    }

    private void ImageList_Disposed(object sender, EventArgs e)
    {
      this.ImageList = (ImageList) null;
      if (this.isDisposing2 || this.IsDisposed)
        return;
      this.CreateGraphics();
    }

    private void SmallImageList_Disposed(object sender, EventArgs e)
    {
      this.SmallImageList = (ImageList) null;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.Behavior.OnKeyDown(e))
        return;
      base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (this.Behavior.OnKeyUp(e))
        return;
      base.OnKeyUp(e);
    }

    protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
      if (this.Behavior.OnPreviewKeyDown(e))
        return;
      base.OnPreviewKeyDown(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (this.Behavior.OnKeyPress(e))
        return;
      base.OnKeyPress(e);
    }

    protected override void OnClick(EventArgs e)
    {
      if (this.Behavior.OnClick(e))
        return;
      base.OnClick(e);
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      if (this.Behavior.OnDoubleClick(e))
        return;
      base.OnDoubleClick(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.Behavior.OnMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.Behavior.OnMouseUp(e))
        return;
      base.OnMouseUp(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.Behavior.OnMouseEnter(e))
        return;
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.Behavior.OnMouseLeave(e))
        return;
      base.OnMouseLeave(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.Behavior.OnMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnMouseHover(EventArgs e)
    {
      if (this.Behavior.OnMouseHover(e))
        return;
      base.OnMouseHover(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (this.Behavior.OnMouseWheel(e))
        return;
      base.OnMouseWheel(e);
    }

    protected override void OnMouseCaptureChanged(EventArgs e)
    {
      this.Behavior.OnMouseCaptureChanged(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      if (this.Behavior.OnGotFocus(e))
        return;
      base.OnGotFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (this.Behavior.OnLostFocus(e))
        return;
      base.OnLostFocus(e);
    }

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      this.LoadElementTree(this.Size);
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
      base.OnHandleDestroyed(e);
      if (this.isDisposing2 || this.RecreatingHandle)
        return;
      this.elementTree.RootElement.OnUnload(this.elementTree, true);
      this.elementTree.RootElement.SetThemeApplied(false);
      this.loaded = false;
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      int num1 = (int) this.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Inherited);
      int num2 = (int) this.RootElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Inherited);
      int num3 = (int) this.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Inherited);
    }

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);
      int num = (int) this.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Inherited);
    }

    protected override void OnForeColorChanged(EventArgs e)
    {
      base.OnForeColorChanged(e);
      int num = (int) this.RootElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Inherited);
    }

    protected override void OnBackColorChanged(EventArgs e)
    {
      base.OnBackColorChanged(e);
      int num = (int) this.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Inherited);
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      this.RootElement.Enabled = this.Enabled;
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      base.OnBindingContextChanged(e);
      this.RootElement.BindingContext = this.BindingContext;
    }

    protected override void OnLocationChanged(EventArgs e)
    {
      base.OnLocationChanged(e);
      if (!this.loaded)
        return;
      this.RootElement.ForceLocationTo(this.Location);
    }

    protected override void OnPaddingChanged(EventArgs e)
    {
      Padding padding = TelerikDpiHelper.ScalePadding(this.Padding, new SizeF(1f / this.RootElement.DpiScaleFactor.Width, 1f / this.RootElement.DpiScaleFactor.Height));
      this.RootElement.Padding = padding;
      if (this.elementTree.RootElement.Children.Count > 0)
        this.elementTree.RootElement.Children[0].Padding = padding;
      base.OnPaddingChanged(e);
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      this.RootElement.RightToLeft = this.GetRightToLeft((Control) this, this.RightToLeft);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      int num = this.ShowFocusCues ? 1 : 0;
      this.isPainting = true;
      e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
      RadControlAnimationTimer.Suspend();
      this.RootElement.Paint((IGraphics) new RadGdiGraphics(e.Graphics), e.ClipRectangle);
      RadControlAnimationTimer.Resume();
      base.OnPaint(e);
      this.isPainting = false;
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
      if (this.DesignMode || !this.loaded || (this.isDisposing2 || this.isResizing2) || (this.layoutManager.IsUpdating || this.elementTree.RootElement.IsLayoutSuspended))
        return;
      this.elementTree.RootElement.InvalidateMeasure();
      this.elementTree.RootElement.UpdateLayout();
    }

    protected override void OnAutoSizeChanged(EventArgs e)
    {
      this.isPreferredSizeValid = false;
      base.OnAutoSizeChanged(e);
      this.elementTree.OnAutoSizeChanged(e);
    }

    protected override void WndProc(ref Message m)
    {
      if (IPCServer.ProcessRequest(ref m, (IPCHost) this))
        return;
      int msg = m.Msg;
      if (msg == 83)
      {
        this.OnHelpRequested(new HelpEventArgs(Control.MousePosition));
      }
      else
      {
        if (m.Msg == 132 && !this.helpSent && (Cursor.Current != (Cursor) null && Cursor.Current.Equals((object) Cursors.Help)))
        {
          this.helpSent = true;
          if (this.FindForm() != null)
          {
            Point mousePosition = Control.MousePosition;
            IntPtr handle = NativeMethods.WindowFromPoint(mousePosition.X, mousePosition.Y);
            if (handle != IntPtr.Zero && this == Control.FromHandle(handle) as RadControl)
            {
              NativeMethods.SendMessage(new HandleRef((object) this, this.Handle), 83, IntPtr.Zero, IntPtr.Zero);
              return;
            }
          }
          this.helpSent = false;
        }
        switch (msg)
        {
          case 281:
            if (this.ProcessGesture(m))
            {
              m.Result = (IntPtr) 1;
              return;
            }
            break;
          case 513:
          case 515:
          case 516:
          case 519:
          case 521:
            if (this.helpSent)
            {
              this.helpSent = false;
              return;
            }
            if (this.CausesValidation)
            {
              this.ProcessFocusRequested((RadElement) null);
              if (this.ValidationCancel)
              {
                this.DefWndProc(ref m);
                return;
              }
              break;
            }
            break;
          case 533:
            this.OnCaptureLosing();
            break;
          case 646:
            if (InputLanguage.CurrentInputLanguage.Culture.LCID != 1042)
            {
              this.DefWndProc(ref m);
              return;
            }
            break;
        }
        base.WndProc(ref m);
        if (msg == 24)
          this.isDisplayed = m.WParam != IntPtr.Zero;
        if (!this.ValidationCancel || !this.Capture)
          return;
        this.ProcessCaptureChangeRequested((RadElement) null, false);
      }
    }

    protected internal bool ValidationCancel
    {
      get
      {
        return (bool) typeof (Control).GetProperty("ValidationCancelled", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).GetValue((object) this, (object[]) null);
      }
    }

    protected virtual void OnInvalidated(RadElement element)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadControl.InvalidatedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) element, (EventArgs) null);
    }

    protected virtual void OnLoad(Size desiredSize)
    {
      if (this.isInitializing || this.loading)
        return;
      this.loading = true;
      if (!this.isInitializing && (!this.RootElement.IsThemeApplied || this.elementTree.Theme == null))
        this.elementTree.ApplyThemeToElementTree();
      this.elementTree.RootElement.NotifyControlImageListChanged();
      this.elementTree.RootElement.OnLoad(true);
      this.loaded = true;
      this.isPreferredSizeValid = false;
      desiredSize = this.Size;
      desiredSize = this.elementTree.PerformInnerLayout(true, this.Left, this.Top, desiredSize.Width, desiredSize.Height);
      if (this.AutoSize && !this.DesignMode)
        base.SetBoundsCore(this.Left, this.Top, desiredSize.Width, desiredSize.Height, BoundsSpecified.None);
      this.DisableGesture(GestureType.All);
      this.loading = false;
    }

    protected virtual void OnCaptureLosing()
    {
    }

    protected virtual void OnThemeChanged()
    {
      if (!this.isDefaultBackColor)
        this.SetBackColorThemeOverrides();
      if (this.isDefaultForeColor)
        return;
      this.SetForeColorThemeOverrides();
    }

    protected virtual void OnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      ToolTipTextNeededEventHandler neededEventHandler = this.Events[RadControl.ToolTipTextNeededEventKey] as ToolTipTextNeededEventHandler;
      if (neededEventHandler == null || this.IsDisposed || this.Disposing)
        return;
      neededEventHandler(sender, e);
    }

    protected virtual void OnScreenTipNeeded(object sender, ScreenTipNeededEventArgs e)
    {
      ScreenTipNeededEventHandler neededEventHandler = this.Events[RadControl.ScreenTipNeededEventKey] as ScreenTipNeededEventHandler;
      if (neededEventHandler == null || this.IsDisposed || this.Disposing)
        return;
      neededEventHandler(sender, e);
    }

    protected virtual void OnThemeNameChanged(ThemeNameChangedEventArgs e)
    {
      if (this.ThemeNameChanged == null)
        return;
      this.ThemeNameChanged((object) this, e);
    }

    [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
    protected override bool ProcessMnemonic(char charCode)
    {
      if (TelerikHelper.CanProcessMnemonic((Control) this) && (this.Focused || this.ContainsFocus) && this.Behavior.ProcessMnemonic(charCode))
        return true;
      return base.ProcessMnemonic(charCode);
    }

    protected virtual bool ProcessFocusRequested(RadElement element)
    {
      if (this.Focused || !this.isFocusable)
        return false;
      return this.Focus();
    }

    protected virtual bool ProcessCaptureChangeRequested(RadElement element, bool capture)
    {
      return this.Capture = capture;
    }

    protected virtual void ProcessAutoSizeChanged(bool value)
    {
      if (!this.RootElement.AutoSize)
        return;
      this.RootElement.StretchHorizontally = !value;
      this.RootElement.StretchVertically = !value;
      this.RootElement.SaveCurrentStretchModeAsDefault();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnMouseCaptureChanged(EventArgs e)
    {
      base.OnMouseCaptureChanged(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallBaseOnGotFocus(EventArgs e)
    {
      this.OnGotFocus(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallBaseOnLostFocus(EventArgs e)
    {
      this.OnLostFocus(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      this.OnToolTipTextNeeded(sender, e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnScreenTipNeeded(object sender, ScreenTipNeededEventArgs e)
    {
      this.OnScreenTipNeeded(sender, e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnMouseUp(MouseEventArgs e)
    {
      this.OnMouseUp(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnMouseDown(MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnClick(EventArgs e)
    {
      this.OnClick(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnDoubleClick(EventArgs e)
    {
      this.OnDoubleClick(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnMouseEnter(EventArgs e)
    {
      this.OnMouseEnter(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnMouseWheel(MouseEventArgs e)
    {
      this.OnMouseWheel(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnMouseLeave(EventArgs e)
    {
      this.OnMouseLeave(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnMouseMove(MouseEventArgs e)
    {
      this.OnMouseMove(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnMouseHover(EventArgs e)
    {
      this.OnMouseHover(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
      this.OnPreviewKeyDown(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallBaseOnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnKeyDown(KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallBaseOnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnKeyPress(KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    protected override bool IsInputKey(Keys keyData)
    {
      RadElement inputElement = this.GetInputElement();
      if (inputElement != null)
      {
        InputKeyEventArgs e = new InputKeyEventArgs(keyData);
        bool flag = inputElement.IsInputKey(e);
        if (e.Handled)
          return flag;
      }
      return base.IsInputKey(keyData);
    }

    protected virtual RadElement GetInputElement()
    {
      return this.FocusedElement;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallBaseOnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnKeyUp(KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnThemeNameChanged(ThemeNameChangedEventArgs e)
    {
      if (this.IsInitializing)
        return;
      this.OnThemeNameChanged(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnVisibleChanged(EventArgs e)
    {
      this.OnVisibleChanged(e);
    }

    public event EventHandler Initialized;

    bool ISupportInitializeNotification.IsInitialized
    {
      get
      {
        return this.isInitialized;
      }
    }

    public virtual void BeginInit()
    {
      this.isInitializing = true;
    }

    public virtual void EndInit()
    {
      this.isInitializing = false;
      if (this.behavior.CommandBindings.Count > 0)
        this.behavior.Shortcuts.AddShortcutsSupport();
      this.RootElement.BindingContext = this.BindingContext;
      this.isInitialized = true;
      if (this.IsHandleCreated && !this.loaded)
        this.LoadElementTree(this.Size);
      else if (this.loaded && !this.RootElement.IsThemeApplied)
        this.elementTree.ApplyThemeToElementTree();
      if (this.Initialized == null)
        return;
      this.Initialized((object) this, EventArgs.Empty);
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event PropertyChangedEventHandler PropertyChanged
    {
      add
      {
        this.Events.AddHandler(RadControl.PropertyChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadControl.PropertyChangedEventKey, (Delegate) value);
      }
    }

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      PropertyChangedEventHandler changedEventHandler = (PropertyChangedEventHandler) this.Events[RadControl.PropertyChangedEventKey];
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, e);
    }

    bool IComponentTreeHandler.IsDesignMode
    {
      get
      {
        return this.IsDesignMode;
      }
    }

    ComponentThemableElementTree IComponentTreeHandler.ElementTree
    {
      get
      {
        return this.elementTree;
      }
    }

    ComponentInputBehavior IComponentTreeHandler.Behavior
    {
      get
      {
        return this.Behavior;
      }
    }

    bool IComponentTreeHandler.Initializing
    {
      get
      {
        if (!this.loaded)
          return true;
        return this.isInitializing;
      }
    }

    string IComponentTreeHandler.ThemeClassName
    {
      get
      {
        return this.ThemeClassName;
      }
      set
      {
        this.ThemeClassName = value;
      }
    }

    bool IComponentTreeHandler.GetShowFocusCues()
    {
      return this.ShowFocusCues;
    }

    RootRadElement IComponentTreeHandler.CreateRootElement()
    {
      return this.CreateRootElement();
    }

    void IComponentTreeHandler.CreateChildItems(RadElement parent)
    {
      this.CreateChildItems(parent);
    }

    void IComponentTreeHandler.InitializeRootElement(RootRadElement rootElement)
    {
      this.InitializeRootElement(rootElement);
    }

    void IComponentTreeHandler.InvalidateElement(RadElement element)
    {
      if (this.IsUpdateSuspended || !this.Visible)
        return;
      this.AddInvalidatedRect(element.GetInvalidateBounds());
      this.OnInvalidated(element);
    }

    void IComponentTreeHandler.InvalidateElement(
      RadElement element,
      Rectangle bounds)
    {
      if (this.IsUpdateSuspended || this.isPainting || !this.Visible)
        return;
      this.OnInvalidated(element);
      this.AddInvalidatedRect(bounds);
    }

    void IComponentTreeHandler.InvalidateIfNotSuspended()
    {
      if (this.IsUpdateSuspended)
        return;
      this.Invalidate(false);
    }

    object IComponentTreeHandler.GetAmbientPropertyValue(RadProperty property)
    {
      if (property == VisualElement.BackColorProperty)
        return (object) base.BackColor;
      if (property == VisualElement.ForeColorProperty)
        return (object) base.ForeColor;
      if (property == VisualElement.FontProperty)
        return (object) base.Font;
      return RadProperty.UnsetValue;
    }

    void IComponentTreeHandler.ControlThemeChangedCallback()
    {
      this.OnThemeChanged();
    }

    void IComponentTreeHandler.OnAmbientPropertyChanged(RadProperty property)
    {
      if (property == VisualElement.BackColorProperty)
        base.OnBackColorChanged(EventArgs.Empty);
      else if (property == VisualElement.ForeColorProperty)
        base.OnForeColorChanged(EventArgs.Empty);
      else if (property == VisualElement.FontProperty)
      {
        base.OnFontChanged(EventArgs.Empty);
      }
      else
      {
        if (property != RadElement.PaddingProperty)
          return;
        this.Padding = this.RootElement.Padding;
      }
    }

    bool IComponentTreeHandler.OnFocusRequested(RadElement element)
    {
      return this.ProcessFocusRequested(element);
    }

    bool IComponentTreeHandler.OnCaptureChangeRequested(
      RadElement element,
      bool capture)
    {
      return this.ProcessCaptureChangeRequested(element, capture);
    }

    void IComponentTreeHandler.OnDisplayPropertyChanged(
      RadPropertyChangedEventArgs e)
    {
    }

    void IComponentTreeHandler.CallOnThemeNameChanged(
      ThemeNameChangedEventArgs e)
    {
      this.CallOnThemeNameChanged(e);
    }

    void IComponentTreeHandler.CallOnMouseCaptureChanged(EventArgs e)
    {
      this.CallOnMouseCaptureChanged(e);
    }

    void IComponentTreeHandler.CallOnToolTipTextNeeded(
      object sender,
      ToolTipTextNeededEventArgs e)
    {
      this.OnToolTipTextNeeded(sender, e);
    }

    void IComponentTreeHandler.CallOnScreenTipNeeded(
      object sender,
      ScreenTipNeededEventArgs e)
    {
      this.OnScreenTipNeeded(sender, e);
    }

    public override Size GetPreferredSize(Size proposedSize)
    {
      if (proposedSize == Size.Empty && new StackTrace().GetFrame(1).GetMethod().Name == "get_PreferredSize")
        return this.Size;
      Size preferredSize = base.GetPreferredSize(proposedSize);
      Size proposedSize1 = proposedSize.Width != int.MaxValue || proposedSize.Height != int.MaxValue || this.Site != null ? this.Size : preferredSize;
      if (this.loaded && (!this.isPreferredSizeValid || !this.RootElement.IsMeasureValid) || (this.elementTree.IsHorizontallyStretchable() || this.elementTree.IsVerticallyStretchable()))
      {
        proposedSize1 = this.elementTree.GetPreferredSize(proposedSize1, proposedSize);
        this.isPreferredSizeValid = true;
      }
      return proposedSize1;
    }

    private bool IsAutoSizeHorizontallyStretchable()
    {
      AnchorStyles anchorStyles = AnchorStyles.Left | AnchorStyles.Right;
      bool flag = (this.Anchor & anchorStyles) == anchorStyles;
      if (this.AutoSize && flag && this.RootElement != null)
        return this.RootElement.StretchHorizontally;
      return false;
    }

    private bool IsAutoSizeVerticallyStretchable()
    {
      AnchorStyles anchorStyles = AnchorStyles.Top | AnchorStyles.Bottom;
      bool flag = (this.Anchor & anchorStyles) == anchorStyles;
      if (this.AutoSize && flag && this.RootElement != null)
        return this.RootElement.StretchVertically;
      return false;
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (!this.loaded || this.isDisposing2 || this.Bounds == new Rectangle(x, y, width, height))
      {
        base.SetBoundsCore(x, y, width, height, specified);
      }
      else
      {
        Size elementDesiredSize = this.GetRootElementDesiredSize(x, y, width, height);
        if (this.invalidResizeRect != Rectangle.Empty)
        {
          this.Invalidate(this.invalidResizeRect, false);
          this.invalidResizeRect = Rectangle.Empty;
        }
        base.SetBoundsCore(x, y, elementDesiredSize.Width, elementDesiredSize.Height, specified);
      }
    }

    protected virtual Size GetRootElementDesiredSize(int x, int y, int width, int height)
    {
      this.isResizing2 = true;
      Size size = this.elementTree.PerformInnerLayout(true, x, y, width, height);
      this.isResizing2 = false;
      return size;
    }

    [Browsable(true)]
    [Description("Occurs when a zoom gesture was sent by a touch input device.")]
    [Category("Touch")]
    public event ZoomGestureEventHandler ZoomGesture;

    [Description("Occurs when a rotate gesture was sent by a touch input device.")]
    [Browsable(true)]
    [Category("Touch")]
    public event RotateGestureEventHandler RotateGesture;

    [Browsable(true)]
    [Description("Occurs when a pan gesture was sent by a touch input device.")]
    [Category("Touch")]
    public event PanGestureEventHandler PanGesture;

    [Description("Occurs when a two-finger-tap gesture was sent by a touch input device.")]
    [Browsable(true)]
    [Category("Touch")]
    public event GestureEventHandler TwoFingerTapGesture;

    [Category("Touch")]
    [Description("Occurs when a press-and-tap gesture was sent by a touch input device.")]
    [Browsable(true)]
    public event PressAndTapGestureEventHandler PressAndTapGesture;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(true)]
    public virtual bool EnableGestures
    {
      get
      {
        return this.enableGestures;
      }
      set
      {
        this.enableGestures = value;
        if (!this.enableGestures)
          this.DisableGesture(GestureType.All);
        else
          this.EnableGesture(GestureType.All);
      }
    }

    protected virtual void OnZoomGesture(ZoomGestureEventArgs args)
    {
      if (this.ZoomGesture == null)
        return;
      args.Handled = true;
      this.ZoomGesture((object) this, args);
    }

    protected virtual void OnRotateGesture(RotateGestureEventArgs args)
    {
      if (this.RotateGesture == null)
        return;
      args.Handled = true;
      this.RotateGesture((object) this, args);
    }

    protected virtual void OnPanGesture(PanGestureEventArgs args)
    {
      if (this.PanGesture == null)
        return;
      args.Handled = true;
      this.PanGesture((object) this, args);
    }

    protected virtual void OnTwoFingerTapGesture(GestureEventArgs args)
    {
      if (this.TwoFingerTapGesture == null)
        return;
      args.Handled = true;
      this.TwoFingerTapGesture((object) this, args);
    }

    protected virtual void OnPressAndTapGesture(PressAndTapGestureEventArgs args)
    {
      if (this.PressAndTapGesture == null)
        return;
      args.Handled = true;
      this.PressAndTapGesture((object) this, args);
    }

    protected virtual void OnGesture(GestureEventArgs args)
    {
      this.Behavior.OnGesture(args);
      switch (args.GestureType)
      {
        case GestureType.Zoom:
          this.OnZoomGesture((ZoomGestureEventArgs) args);
          break;
        case GestureType.Pan:
          this.OnPanGesture((PanGestureEventArgs) args);
          if (args.Handled)
            break;
          MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 0, args.Location.X, args.Location.Y, 0);
          Cursor.Position = this.PointToScreen(e.Location);
          if (args.IsBegin)
          {
            this.CallOnMouseDown(e);
            break;
          }
          if (args.IsEnd)
          {
            this.CallOnMouseUp(e);
            break;
          }
          this.CallOnMouseMove(e);
          break;
        case GestureType.Rotate:
          this.OnRotateGesture((RotateGestureEventArgs) args);
          break;
        case GestureType.TwoFingerTap:
          this.OnTwoFingerTapGesture(args);
          break;
        case GestureType.PressAndTap:
          this.OnPressAndTapGesture((PressAndTapGestureEventArgs) args);
          break;
      }
    }

    protected bool ProcessGesture(Message m)
    {
      if (!GestureInfoHelper.SupportsGestures || !this.EnableGestures)
        return false;
      GESTUREINFO pGestureInfo = new GESTUREINFO()
      {
        cbSize = (uint) Marshal.SizeOf(typeof (GESTUREINFO))
      };
      try
      {
        if (!GestureInfoHelper.GetGestureInfo(m.LParam, ref pGestureInfo))
          return false;
      }
      catch (TypeLoadException ex)
      {
        GestureInfoHelper.SupportsGestures = false;
        return false;
      }
      Point client = this.PointToClient(new Point((int) pGestureInfo.ptsLocation.x, (int) pGestureInfo.ptsLocation.y));
      Point center = client;
      bool flag = ((int) pGestureInfo.dwFlags & 1) != 0;
      GestureEventArgs args = (GestureEventArgs) null;
      switch (pGestureInfo.dwID)
      {
        case 1:
        case 2:
          return false;
        case 3:
          Point point = flag ? client : this.lastBeginEvent.Location;
          center = new Point((client.X + point.X) / 2, (client.Y + point.Y) / 2);
          args = (GestureEventArgs) new ZoomGestureEventArgs(flag ? 1.0 : (double) pGestureInfo.ullArguments / (double) this.lastEventArguments, center, pGestureInfo, this);
          break;
        case 4:
          Size offset = flag ? new Size(0, 0) : new Size(client.X - this.lastEvent.Location.X, client.Y - this.lastEvent.Location.Y);
          int number = NativeMethods.HiDWord((long) pGestureInfo.ullArguments);
          Size velocity = new Size((int) NativeMethods.LoWord(number), (int) NativeMethods.HiWord(number));
          args = (GestureEventArgs) new PanGestureEventArgs(offset, velocity, pGestureInfo, this);
          break;
        case 5:
          ushort num = flag ? (ushort) 0 : (ushort) this.lastEventArguments;
          args = (GestureEventArgs) new RotateGestureEventArgs(NativeMethods.GID_ROTATE_ANGLE_FROM_ARGUMENT((ulong) (ushort) pGestureInfo.ullArguments) - NativeMethods.GID_ROTATE_ANGLE_FROM_ARGUMENT((ulong) num), pGestureInfo, this);
          break;
        case 6:
          args = new GestureEventArgs(pGestureInfo, this);
          break;
        case 7:
          args = (GestureEventArgs) new PressAndTapGestureEventArgs(flag ? new Point((int) NativeMethods.LoWord((int) pGestureInfo.ullArguments), (int) NativeMethods.HiWord((int) pGestureInfo.ullArguments)) : new Point((int) NativeMethods.LoWord((int) this.lastBeginEventArguments), (int) NativeMethods.HiWord((int) this.lastBeginEventArguments)), pGestureInfo, this);
          break;
      }
      if (args == null)
        return true;
      this.OnGesture(args);
      if (flag)
      {
        this.lastBeginEvent = args;
        this.lastBeginEventArguments = pGestureInfo.ullArguments;
      }
      this.lastEvent = args;
      this.lastEventArguments = pGestureInfo.ullArguments;
      return true;
    }

    public void EnableGesture(GestureType type)
    {
      if (!GestureInfoHelper.SupportsGestures)
        return;
      NativeMethods.GESTURECONFIG[] pGestureConfig = (NativeMethods.GESTURECONFIG[]) null;
      switch (type)
      {
        case GestureType.All:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 0U,
              dwWant = 1U,
              dwBlock = 0U
            }
          };
          break;
        case GestureType.Zoom:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 3U,
              dwWant = 1U,
              dwBlock = 0U
            }
          };
          break;
        case GestureType.Pan:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 4U,
              dwWant = 23U,
              dwBlock = 8U
            }
          };
          break;
        case GestureType.Rotate:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 5U,
              dwWant = 1U,
              dwBlock = 0U
            }
          };
          break;
        case GestureType.TwoFingerTap:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 6U,
              dwWant = 1U,
              dwBlock = 0U
            }
          };
          break;
        case GestureType.PressAndTap:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 7U,
              dwWant = 1U,
              dwBlock = 0U
            }
          };
          break;
      }
      if (pGestureConfig == null)
        return;
      try
      {
        NativeMethods.SetGestureConfig(this.Handle, 0U, 1U, pGestureConfig, (uint) Marshal.SizeOf(typeof (NativeMethods.GESTURECONFIG)));
      }
      catch (TypeLoadException ex)
      {
        GestureInfoHelper.SupportsGestures = false;
      }
    }

    public void DisableGesture(GestureType type)
    {
      if (!GestureInfoHelper.SupportsGestures)
        return;
      NativeMethods.GESTURECONFIG[] pGestureConfig = (NativeMethods.GESTURECONFIG[]) null;
      switch (type)
      {
        case GestureType.All:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 0U,
              dwWant = 0U,
              dwBlock = 1U
            }
          };
          break;
        case GestureType.Zoom:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 3U,
              dwWant = 0U,
              dwBlock = 1U
            }
          };
          break;
        case GestureType.Pan:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 4U,
              dwWant = 0U,
              dwBlock = 1U
            }
          };
          break;
        case GestureType.Rotate:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 5U,
              dwWant = 0U,
              dwBlock = 1U
            }
          };
          break;
        case GestureType.TwoFingerTap:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 6U,
              dwWant = 0U,
              dwBlock = 1U
            }
          };
          break;
        case GestureType.PressAndTap:
          pGestureConfig = new NativeMethods.GESTURECONFIG[1]
          {
            new NativeMethods.GESTURECONFIG()
            {
              dwID = 7U,
              dwWant = 0U,
              dwBlock = 1U
            }
          };
          break;
      }
      if (pGestureConfig == null)
        return;
      try
      {
        NativeMethods.SetGestureConfig(this.Handle, 0U, 1U, pGestureConfig, (uint) Marshal.SizeOf(typeof (NativeMethods.GESTURECONFIG)));
      }
      catch (TypeLoadException ex)
      {
        GestureInfoHelper.SupportsGestures = false;
      }
    }

    public override void Refresh()
    {
      if (this.IsUpdateSuspended)
        return;
      base.Refresh();
    }

    public void SuspendUpdate()
    {
      ++this.suspendUpdateCounter;
    }

    public void ResumeUpdate()
    {
      this.ResumeUpdate(true);
    }

    public void ResumeUpdate(bool invalidate)
    {
      if (this.suspendUpdateCounter <= 0)
        return;
      --this.suspendUpdateCounter;
      if (!invalidate || this.suspendUpdateCounter != 0)
        return;
      this.Invalidate(true);
    }

    public void InvokeLayoutCallback(LayoutCallback callback)
    {
      if (!this.IsHandleCreated || this.Disposing || this.IsDisposed)
        return;
      this.BeginInvoke((Delegate) callback, (object) this.layoutManager);
    }

    public virtual void RegisterHostedControl(RadHostItem hostElement)
    {
      if (hostElement == null || hostElement.HostedControl.Parent == this)
        return;
      this.SuspendLayout();
      this.Controls.Add(hostElement.HostedControl);
      this.ResumeLayout(false);
    }

    public virtual void UnregisterHostedControl(RadHostItem hostElement, bool removeControl)
    {
      if (hostElement == null)
        return;
      this.SuspendLayout();
      if (hostElement.HostedControl != null && hostElement.HostedControl.Parent == this)
      {
        if (this.elementTree.IsLayoutSuspended)
          hostElement.HostedControl.ResumeLayout(false);
        if (removeControl)
          this.Controls.Remove(hostElement.HostedControl);
      }
      this.ResumeLayout(false);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool ControlDefinesThemeForElement(RadElement element)
    {
      return false;
    }

    public virtual StyleGroup ResolveStyleGroupForElement(
      StyleGroup styleGroup,
      RadObject element)
    {
      return styleGroup;
    }

    public string GetPlainText()
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = true;
      foreach (char ch in this.Text)
      {
        switch (ch)
        {
          case '<':
            flag = false;
            break;
          case '>':
            flag = true;
            break;
          default:
            if (flag)
            {
              stringBuilder.Append(ch);
              break;
            }
            break;
        }
      }
      return stringBuilder.ToString();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool CanEditUIElement(RadElement element)
    {
      if (!this.DesignMode)
        return true;
      return this.CanEditElementAtDesignTime(element);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadElement GetChildAt(int index)
    {
      return this.elementTree.RootElement.GetChildAt(index);
    }

    protected virtual bool ShouldSerializeProperty(RadProperty property)
    {
      return this.elementTree.RootElement.GetValueSource(property) > ValueSource.Style;
    }

    protected virtual bool CanEditElementAtDesignTime(RadElement element)
    {
      return true;
    }

    private bool GetRightToLeft(Control control, RightToLeft value)
    {
      switch (value)
      {
        case RightToLeft.No:
          return false;
        case RightToLeft.Yes:
          return true;
        case RightToLeft.Inherit:
          if (control.Parent != null)
            return this.GetRightToLeft(control.Parent, control.Parent.RightToLeft);
          return false;
        default:
          return false;
      }
    }

    private void AddInvalidatedRect(Rectangle rect)
    {
      if (rect.IsEmpty)
        return;
      rect.Inflate(1, 1);
      if (!this.isResizing2)
        this.Invalidate(rect, false);
      else if (this.invalidResizeRect == Rectangle.Empty)
        this.invalidResizeRect = rect;
      else
        this.invalidResizeRect = Rectangle.Union(rect, this.invalidResizeRect);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public Size GetControlDefaultSize()
    {
      return this.DefaultSize;
    }

    public static Size GetDpiScaledSize(Size dpi96Size)
    {
      Point systemDpi = NativeMethods.GetSystemDpi();
      return new Size((int) ((double) dpi96Size.Width * (double) systemDpi.X / 96.0), (int) ((double) dpi96Size.Height * (double) systemDpi.Y / 96.0));
    }

    public static SizeF GetDpiScaledSize(SizeF dpi96Size)
    {
      Point systemDpi = NativeMethods.GetSystemDpi();
      return new SizeF((float) ((double) dpi96Size.Width * (double) systemDpi.X / 96.0), (float) ((double) dpi96Size.Height * (double) systemDpi.Y / 96.0));
    }

    [Description("Gets or sets a value indicating whether the Analytics functionality is enabled or disabled for this control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual bool EnableAnalytics
    {
      get
      {
        return this.enableAnalytics;
      }
      set
      {
        this.enableAnalytics = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    [DefaultValue("")]
    [Description("Gets or sets the Analytics Name associated with this control. Gets or sets the Analytics Name associated with this control. By default the Control Name property is logged. If you want to customize the information which will be logged for this control set this property to a preferred value.")]
    public virtual string AnalyticsName
    {
      get
      {
        return this.analyticsName;
      }
      set
      {
        this.analyticsName = value;
      }
    }

    [Description("Gets or sets a value indicating whether the RadControls Accessible custom object is enabled.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(false)]
    public virtual bool EnableRadAccessibilityObjects
    {
      get
      {
        return this.enableRadAccessibilityObjects;
      }
      set
      {
        this.enableRadAccessibilityObjects = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the CodedUI Tests functionality is requested from external program such a Narrator.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool AccessibilityRequested
    {
      get
      {
        return this.isAccessibilityRequested;
      }
      set
      {
        this.isAccessibilityRequested = value;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the CodedUI Tests functionality is enabled.")]
    public virtual bool EnableCodedUITests
    {
      get
      {
        if (this.enableCodedUITests.HasValue)
          return this.enableCodedUITests.Value;
        return RadControl.enableCodedUITestsDefaultValue;
      }
      set
      {
        this.enableCodedUITests = new bool?(value);
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets the default value for EnableCodedUITests property.")]
    public static bool EnableCodedUITestsDefaultValue
    {
      get
      {
        return RadControl.enableCodedUITestsDefaultValue;
      }
      set
      {
        RadControl.enableCodedUITestsDefaultValue = value;
      }
    }

    protected virtual void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request == null)
        return;
      if (request.Type == IPCMessage.MessageTypes.GetPropertyValue)
      {
        if (this.FindPropertyInChildren(this, request))
          return;
        PropertyInfo property = this.GetType().GetProperty(request.Message, BindingFlags.Instance | BindingFlags.Public);
        if ((object) property == null)
          return;
        request.Data = property.GetValue((object) this, new object[0]);
      }
      else if (request.Type == IPCMessage.MessageTypes.SetPropertyValue)
      {
        PropertyInfo property = this.GetType().GetProperty(request.Message, BindingFlags.Instance | BindingFlags.Public);
        if ((object) property == null)
          return;
        property.SetValue((object) this, request.Data, new object[0]);
      }
      else if (request.Type == IPCMessage.MessageTypes.GetChildPropertyValue)
      {
        if (request.ControlType == "TabPage")
          ((RadControl) this.FindControl((Control) this, "RadPageView"))?.ProcessCodedUIMessage(ref request);
        else
          this.FindPropertyInChildren(this, request);
      }
      else if (request.Type == IPCMessage.MessageTypes.SetChildPropertyValue)
      {
        if (request.Data == null)
          return;
        int childCount = this.AccessibilityObject.GetChildCount();
        for (int index = 0; index < childCount; ++index)
        {
          AccessibleObject child = this.AccessibilityObject.GetChild(index);
          if (child.Name == request.ControlType)
          {
            RadItemAccessibleObject accessibleObject = child as RadItemAccessibleObject;
            if (accessibleObject == null)
              break;
            PropertyInfo property = accessibleObject.Owner.GetType().GetProperty(request.Message, BindingFlags.Instance | BindingFlags.Public);
            if ((object) property == null)
              break;
            property.SetValue(accessibleObject.Owner, request.Data, new object[0]);
            break;
          }
        }
      }
      else
      {
        int type = (int) request.Type;
      }
    }

    private Control FindControl(Control parentControl, string controlTypeToFind)
    {
      if (parentControl.GetType().Name == controlTypeToFind)
        return parentControl;
      foreach (Control control1 in (ArrangedElementCollection) parentControl.Controls)
      {
        Control control2 = this.FindControl(control1, controlTypeToFind);
        if (control2 != null)
          return control2;
      }
      return (Control) null;
    }

    protected virtual bool FindPropertyInChildren(RadControl radControl, IPCMessage request)
    {
      if (request.Data == null)
        return false;
      int childCount = radControl.AccessibilityObject.GetChildCount();
      for (int index = 0; index < childCount; ++index)
      {
        AccessibleObject child = radControl.AccessibilityObject.GetChild(index);
        if (child.Name == (string) request.Data)
        {
          RadItemAccessibleObject accessibleObject1 = child as RadItemAccessibleObject;
          if (accessibleObject1 != null)
          {
            PropertyInfo property = accessibleObject1.Owner.GetType().GetProperty(request.Message, BindingFlags.Instance | BindingFlags.Public);
            if ((object) property != null)
            {
              request.Data = property.GetValue(accessibleObject1.Owner, new object[0]);
              return true;
            }
          }
          RadControlAccessibleObject accessibleObject2 = child as RadControlAccessibleObject;
          if (accessibleObject2 != null)
          {
            PropertyInfo property = accessibleObject2.OwnerElement.GetType().GetProperty(request.Message, BindingFlags.Instance | BindingFlags.Public);
            if ((object) property != null)
            {
              request.Data = property.GetValue(accessibleObject2.OwnerElement, new object[0]);
              return true;
            }
            break;
          }
          break;
        }
      }
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallAccessibilityNotifyClients(AccessibleEvents accEvent, int childId)
    {
      this.AccessibilityNotifyClients(accEvent, childId);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    IntPtr IPCHost.Context
    {
      get
      {
        return this.ipcContext;
      }
      set
      {
        this.ipcContext = value;
      }
    }

    void IPCHost.ProcessMessage(ref IPCMessage request)
    {
      this.ProcessCodedUIMessage(ref request);
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      if ((double) factor.Width == 1.0 && (double) factor.Height == 1.0 || !RadControl.EnableDpiScaling)
        return;
      Size minimumSize = this.MinimumSize;
      Size maximumSize = this.MaximumSize;
      if (minimumSize != Size.Empty || maximumSize != Size.Empty)
      {
        this.MinimumSize = Size.Empty;
        this.MaximumSize = Size.Empty;
      }
      if ((specified & BoundsSpecified.Size) == BoundsSpecified.Size)
        this.RootElement.DpiScaleChanged(factor);
      base.ScaleControl(factor, specified);
      if (!(minimumSize != Size.Empty) && !(maximumSize != Size.Empty))
        return;
      this.MinimumSize = minimumSize;
      this.MaximumSize = maximumSize;
    }

    [SpecialName]
    string IComponentTreeHandler.get_Name()
    {
      return this.Name;
    }

    [SpecialName]
    void IComponentTreeHandler.set_Name(string _param1)
    {
      this.Name = _param1;
    }
  }
}
