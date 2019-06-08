// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadFormControlBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Keyboard;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public abstract class RadFormControlBase : Form, IComponentTreeHandler, ILayoutHandler, ISupportInitializeNotification, ISupportInitialize, INotifyPropertyChanged, IAnalyticsProvider
  {
    private Timer timer = new Timer();
    private Size smallImageScalingSize = Size.Empty;
    private bool enableAnalytics = true;
    private string analyticsName = "";
    private Point? initialFormLocation = new Point?();
    private Size cachedClientSize = new Size(-1, -1);
    private static readonly object ToolTipTextNeededEventKey = (object) null;
    private static readonly object ScreenTipNeededEventKey = new object();
    private const int designTimeDpi = 96;
    protected int oldDpi;
    protected int currentDpi;
    private bool scaling;
    private bool isDpiScalingSuspended;
    private bool isMoving;
    protected bool shouldScale;
    private Size minSizeState;
    private Size maxSizeState;
    private Stack<Dictionary<Control, AnchorStyles>> anchorsStack;
    private Stack<Dictionary<Control, KeyValuePair<Size, Size>>> minMaxStack;
    private FormControlBehavior formBehavior;
    protected bool isBehaviorPrepared;
    private FormBorderStyle formBorderStyle;
    private bool loaded;
    private bool disposing;
    public bool controlIsInitializingRootComponent;
    protected bool isPainting;
    private bool isInitializing;
    private ComponentThemableElementTree elementTree;
    private ComponentInputBehavior inputBehavior;
    private ImageList imageList;
    private ImageList smallImageList;
    private int suspendUpdateCounter;
    private FormWindowState oldState;
    private IButtonControl acceptButton;
    private FieldInfo clientWidthField;
    private FieldInfo clientHeightField;
    private FieldInfo formStateSetClientSizeField;
    private FieldInfo formStateField;
    private FieldInfo serializedClientSizeFi;
    private bool wmWindowPosChangedProcessing;
    private ContextLayoutManager contextLayoutManager;
    private bool isInitialized;
    private static readonly object PropertyChangedEventKey;

    static RadFormControlBase()
    {
      RadFormControlBase.ToolTipTextNeededEventKey = new object();
      RadFormControlBase.PropertyChangedEventKey = new object();
    }

    public RadFormControlBase()
    {
      this.Construct();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.formBorderStyle = base.FormBorderStyle;
      base.FormBorderStyle = FormBorderStyle.None;
      this.AutoScaleMode = AutoScaleMode.None;
      this.LoadElementTree(this.Size);
      this.timer.Tick += new EventHandler(this.Timer_Tick);
    }

    protected virtual void Construct()
    {
      this.contextLayoutManager = new ContextLayoutManager((ILayoutHandler) this);
      this.inputBehavior = new ComponentInputBehavior((IComponentTreeHandler) this);
      this.elementTree = new ComponentThemableElementTree((IComponentTreeHandler) this);
      this.InitializeReflectedFields();
    }

    private void InitializeReflectedFields()
    {
      this.clientWidthField = typeof (Control).GetField("clientWidth", BindingFlags.Instance | BindingFlags.NonPublic);
      this.clientHeightField = typeof (Control).GetField("clientHeight", BindingFlags.Instance | BindingFlags.NonPublic);
      this.formStateSetClientSizeField = typeof (Form).GetField("FormStateSetClientSize", BindingFlags.Static | BindingFlags.NonPublic);
      this.formStateField = typeof (Form).GetField("formState", BindingFlags.Instance | BindingFlags.NonPublic);
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
          this.RootElement.SetIsDesignMode(true, true);
        else
          this.RootElement.SetIsDesignMode(false, true);
      }
    }

    protected virtual bool GetUseNewLayout()
    {
      return true;
    }

    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      this.LoadElementTree(this.Size);
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

    protected virtual void OnLoad(Size desiredSize)
    {
      if (this.isInitializing)
        return;
      if (this.elementTree.RootElement == null)
        this.elementTree.InitializeRootElement();
      if (!this.RootElement.IsThemeApplied)
        this.elementTree.ApplyThemeToElementTree();
      this.elementTree.RootElement.NotifyControlImageListChanged();
      this.elementTree.RootElement.CallOnLoad(true);
      this.loaded = true;
      desiredSize = this.elementTree.PerformInnerLayout(true, this.Left, this.Top, desiredSize.Width, desiredSize.Height);
      if (!this.AutoSize)
        return;
      base.SetBoundsCore(this.Left, this.Top, desiredSize.Width, desiredSize.Height, BoundsSpecified.All);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.IsDesignMode)
      {
        this.initialFormLocation = new Point?(this.Location);
      }
      else
      {
        if (!RadControl.EnableDpiScaling || !TelerikHelper.IsWindows8OrLower && !TelerikHelper.IsWindows10CreatorsUpdateOrHigher)
          return;
        SizeF sizeF = TelerikDpiHelper.ScaleSizeF(Telerik.WinControls.NativeMethods.GetMonitorDpi(Screen.FromControl((Control) this), Telerik.WinControls.NativeMethods.DpiType.Effective), new SizeF(1f / this.RootElement.DpiScaleFactor.Width, 1f / this.RootElement.DpiScaleFactor.Height));
        this.oldDpi = this.currentDpi;
        this.currentDpi = (int) Math.Round((double) sizeF.Width * 96.0, MidpointRounding.AwayFromZero);
        this.HandleDpiChanged();
      }
    }

    internal void CallBaseOnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
    }

    internal void CallBaseOnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.formBehavior == null || !this.isBehaviorPrepared)
      {
        base.OnPaint(e);
      }
      else
      {
        if (this.formBehavior.OnAssociatedFormPaint(e))
          return;
        base.OnPaint(e);
      }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      if (this.formBehavior == null || !this.isBehaviorPrepared)
      {
        base.OnPaintBackground(e);
      }
      else
      {
        if (this.formBehavior.OnAssociatedFormPaintBackground(e))
          return;
        base.OnPaintBackground(e);
      }
    }

    [Browsable(false)]
    [DefaultValue("")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("Gets or sets the Analytics Name associated with this Form.")]
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

    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Description("Gets or sets a value indicating whether the Analytics functionality is enabled or disbaled for this control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

    public bool IsLoaded
    {
      get
      {
        return this.loaded;
      }
    }

    public override Size MaximumSize
    {
      get
      {
        return base.MaximumSize;
      }
      set
      {
        base.MaximumSize = this.RootElement.MaxSize = value;
      }
    }

    public override Size MinimumSize
    {
      get
      {
        return base.MinimumSize;
      }
      set
      {
        base.MinimumSize = value;
      }
    }

    public new FormBorderStyle FormBorderStyle
    {
      get
      {
        if (this.formBehavior == null || !this.isBehaviorPrepared)
          return base.FormBorderStyle;
        return this.formBorderStyle;
      }
      set
      {
        if (this.formBehavior != null && this.isBehaviorPrepared)
        {
          this.formBorderStyle = value;
          this.SetIconPrimitiveVisibility(this.formBorderStyle != FormBorderStyle.FixedToolWindow);
          this.UpdateStyles();
        }
        else
          base.FormBorderStyle = value;
      }
    }

    protected abstract void SetIconPrimitiveVisibility(bool visible);

    public FormControlBehavior FormBehavior
    {
      get
      {
        return this.formBehavior;
      }
      set
      {
        this.ResetFormBehavior(false);
        if (value == null)
          return;
        Size clientSize = this.ClientSize;
        this.formBehavior = value;
        this.PrepareBehavior();
        this.RecreateHandle();
        this.ClientSize = clientSize;
      }
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None && (keyData & Keys.KeyCode) == Keys.Return)
      {
        IButtonControl acceptButton = this.acceptButton;
        if (acceptButton != null)
        {
          acceptButton.PerformClick();
          return true;
        }
      }
      return base.ProcessDialogKey(keyData);
    }

    protected override void UpdateDefaultButton()
    {
      base.UpdateDefaultButton();
      ContainerControl containerControl = (ContainerControl) this;
      while (containerControl.ActiveControl is ContainerControl)
      {
        containerControl = containerControl.ActiveControl as ContainerControl;
        if (containerControl is Form)
        {
          containerControl = (ContainerControl) this;
          break;
        }
      }
      if (!(containerControl.ActiveControl is IButtonControl))
        this.SetDefaultButton(this.AcceptButton);
      else
        this.SetDefaultButton((IButtonControl) containerControl.ActiveControl);
    }

    private void SetDefaultButton(IButtonControl button)
    {
      IButtonControl acceptButton = this.acceptButton;
      if (acceptButton == button)
        return;
      acceptButton?.NotifyDefault(false);
      this.acceptButton = button;
      button?.NotifyDefault(true);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the ComponentInputBehavior instance that handles all logic and user interaction in RadControl.")]
    public virtual ComponentInputBehavior Behavior
    {
      get
      {
        return this.inputBehavior;
      }
    }

    [Category("StyleSheet")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual string ThemeClassName
    {
      get
      {
        return this.elementTree.ThemeClassName;
      }
      set
      {
        if (!(this.elementTree.ThemeClassName != value))
          return;
        this.elementTree.ThemeClassName = value;
        this.OnNotifyPropertyChanged(nameof (ThemeClassName));
      }
    }

    [Description("Gets or sets the ImageList that contains the images displayed by this control.")]
    [DefaultValue(null)]
    [Browsable(true)]
    [Category("Appearance")]
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
        EventHandler eventHandler1 = new EventHandler(this.ImageListRecreateHandle);
        EventHandler eventHandler2 = new EventHandler(this.DetachSmallImageList);
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
        this.ElementTree.RootElement.NotifyControlImageListChanged();
        this.InvalidateIfNotSuspended();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool AllowTheming
    {
      get
      {
        if (this.FormBehavior is ThemedFormBehavior)
          return (this.FormBehavior as ThemedFormBehavior).AllowTheming;
        return false;
      }
      set
      {
        if (!(this.FormBehavior is ThemedFormBehavior))
          return;
        (this.FormBehavior as ThemedFormBehavior).AllowTheming = value;
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

    [DefaultValue(false)]
    [Category("Accessibility")]
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

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether ToolTips are shown for the RadItem objects contained in the RadControl.")]
    [Category("Behavior")]
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
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public InputBindingsCollection CommandBindings
    {
      get
      {
        return this.Behavior.Shortcuts.InputBindings;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
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

    [Description("Gets or sets the BackColor of the control. This is actually the BackColor property of the root element.")]
    public override Color BackColor
    {
      get
      {
        return this.elementTree.RootElement.BackColor;
      }
      set
      {
        if (this.IsDesignMode && this.AllowTheming && (value == Control.DefaultBackColor && Environment.StackTrace.Contains("System.Windows.Forms.Design.DocumentDesigner.Initialize(IComponent component)")))
        {
          int num1 = (int) this.elementTree.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        }
        else if (value == Color.Empty)
        {
          int num2 = (int) this.elementTree.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        }
        else
          this.elementTree.RootElement.BackColor = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeBackColor()
    {
      return this.ShouldSerializeProperty(VisualElement.BackColorProperty);
    }

    protected override void OnBackColorChanged(EventArgs e)
    {
      base.OnBackColorChanged(e);
      int num = (int) this.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Inherited);
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
        }
        else
          this.elementTree.RootElement.ForeColor = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeForeColor()
    {
      return this.ShouldSerializeProperty(VisualElement.ForeColorProperty);
    }

    protected override void OnForeColorChanged(EventArgs e)
    {
      base.OnForeColorChanged(e);
      int num = (int) this.RootElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Inherited);
    }

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

    protected override void OnFontChanged(EventArgs e)
    {
      base.OnFontChanged(e);
      int num = (int) this.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Inherited);
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      int num1 = (int) this.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Inherited);
      int num2 = (int) this.RootElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Inherited);
      int num3 = (int) this.RootElement.ResetValue(VisualElement.FontProperty, ValueResetFlags.Inherited);
    }

    protected virtual bool ShouldSerializeProperty(RadProperty property)
    {
      return this.elementTree.RootElement.GetValueSource(property) > ValueSource.Style;
    }

    internal void SetFormStyle(ControlStyles style, bool value)
    {
      this.SetStyle(style, value);
    }

    internal void CallUpdateStyles()
    {
      this.UpdateStyles();
    }

    internal void CallRecreateHandle()
    {
      this.RecreateHandle();
    }

    internal void CallInitializeFormBehavior()
    {
      if (this.formBehavior != null)
        return;
      this.formBehavior = this.InitializeFormBehavior();
      if (this.formBehavior == null)
        return;
      this.PrepareBehavior();
    }

    private void PrepareBehavior()
    {
      this.formBehavior.SetBaseWndProcCallback(new WndProcInvoker(this.CallBaseWndProc));
      this.formBehavior.SetDefWndProcCallback(new WndProcInvoker(this.CallDefWndProc));
      if (this.formBehavior.HandlesCreateChildItems)
        this.formBehavior.CreateChildItems((RadElement) this.RootElement);
      this.isBehaviorPrepared = true;
      int num = (int) this.RootElement.SetValue(RootRadElement.ApplyShapeToControlProperty, (object) true);
      this.UpdateStyles();
    }

    protected abstract FormControlBehavior InitializeFormBehavior();

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public void ResetFormBehavior(bool callInitialize)
    {
      this.isBehaviorPrepared = false;
      if (this.formBehavior != null && this.formBehavior.HandlesCreateChildItems)
      {
        this.formBehavior.Dispose();
        this.formBehavior = (FormControlBehavior) null;
        this.RootElement.DisposeChildren();
        int num = (int) this.RootElement.ResetValue(RootRadElement.ApplyShapeToControlProperty, ValueResetFlags.Local);
      }
      if (!callInitialize)
      {
        base.FormBorderStyle = FormBorderStyle.Sizable;
        this.UpdateStyles();
        this.Region = (Region) null;
      }
      else
        this.CallInitializeFormBehavior();
      this.RecreateHandle();
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        if (this.formBehavior != null)
          return this.formBehavior.CreateParams(createParams);
        return createParams;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.disposing = true;
        this.SuspendLayout();
        if (this.elementTree != null)
          this.elementTree.Dispose();
        if (this.contextLayoutManager != null)
        {
          this.contextLayoutManager.Dispose();
          this.contextLayoutManager = (ContextLayoutManager) null;
        }
        if (this.inputBehavior != null)
          this.inputBehavior.Dispose();
        if (this.formBehavior != null)
          this.formBehavior.Dispose();
        if (this.Region != null)
          this.Region.Dispose();
        this.timer.Tick -= new EventHandler(this.Timer_Tick);
        this.timer.Dispose();
      }
      base.Dispose(disposing);
    }

    protected override void WndProc(ref Message m)
    {
      if (this.IsDisposed || this.disposing)
      {
        base.WndProc(ref m);
      }
      else
      {
        if (m.Msg == 70)
        {
          if (this.IsDesignMode)
            this.PerformDesignModeLocationCorrection(ref m);
        }
        else if (m.Msg == 736)
        {
          if (this.IsDpiScalingSuspended || !RadControl.EnableDpiScaling)
            return;
          this.oldDpi = this.currentDpi;
          this.currentDpi = (int) (short) (int) m.WParam;
          if (this.oldDpi != this.currentDpi)
          {
            if (this.isMoving)
              this.shouldScale = true;
            else
              this.HandleDpiChanged();
          }
        }
        if (this.formBehavior == null || !this.isBehaviorPrepared)
        {
          base.WndProc(ref m);
        }
        else
        {
          if (this.formBehavior.HandleWndProc(ref m))
            return;
          if (m.Msg == 71)
            this.wmWindowPosChangedProcessing = true;
          base.WndProc(ref m);
          if (m.Msg != 71)
            return;
          this.wmWindowPosChangedProcessing = false;
        }
      }
    }

    protected virtual void PerformDesignModeLocationCorrection(ref Message msg)
    {
      Telerik.WinControls.NativeMethods.WINDOWPOS structure = (Telerik.WinControls.NativeMethods.WINDOWPOS) Marshal.PtrToStructure(msg.LParam, typeof (Telerik.WinControls.NativeMethods.WINDOWPOS));
      ScrollableControl parent = this.Parent as ScrollableControl;
      if (parent != null)
      {
        if (!parent.HorizontalScroll.Visible && this.initialFormLocation.HasValue)
          structure.x = this.initialFormLocation.Value.X;
        if (!parent.VerticalScroll.Visible && this.initialFormLocation.HasValue)
          structure.y = this.initialFormLocation.Value.Y;
      }
      Marshal.StructureToPtr((object) structure, msg.LParam, true);
      msg.Result = IntPtr.Zero;
    }

    protected virtual void CallBaseWndProc(ref Message m)
    {
      base.WndProc(ref m);
    }

    private void CallDefWndProc(ref Message m)
    {
      this.DefWndProc(ref m);
    }

    protected override void OnResizeBegin(EventArgs e)
    {
      base.OnResizeBegin(e);
      this.isMoving = true;
    }

    protected override void OnResizeEnd(EventArgs e)
    {
      base.OnResizeEnd(e);
      this.isMoving = false;
      if (!this.shouldScale || !RadControl.EnableDpiScaling)
        return;
      this.shouldScale = false;
      this.HandleDpiChanged();
    }

    protected override void OnMove(EventArgs e)
    {
      base.OnMove(e);
      if (!this.shouldScale || !this.CanPerformScaling() || !RadControl.EnableDpiScaling)
        return;
      this.shouldScale = false;
      this.HandleDpiChanged();
    }

    private bool CanPerformScaling()
    {
      return Screen.FromControl((Control) this).Bounds.Contains(this.Bounds);
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

    void IComponentTreeHandler.OnAmbientPropertyChanged(RadProperty property)
    {
      if (property == VisualElement.BackColorProperty)
        base.OnBackColorChanged(EventArgs.Empty);
      else if (property == VisualElement.ForeColorProperty)
      {
        base.OnForeColorChanged(EventArgs.Empty);
      }
      else
      {
        if (property != VisualElement.FontProperty)
          return;
        base.OnFontChanged(EventArgs.Empty);
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

    protected virtual bool ProcessFocusRequested(RadElement element)
    {
      if (this.Focused)
        return false;
      return this.Focus();
    }

    protected virtual bool ProcessCaptureChangeRequested(RadElement element, bool capture)
    {
      return this.Capture = capture;
    }

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      PropertyChangedEventHandler changedEventHandler = (PropertyChangedEventHandler) this.Events[RadFormControlBase.PropertyChangedEventKey];
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, e);
    }

    protected virtual void CreateChildItems(RadElement parent)
    {
      parent.Name = this.Name;
      this.CallInitializeFormBehavior();
    }

    protected virtual RootRadElement CreateRootElement()
    {
      return (RootRadElement) new FormRootElement(this);
    }

    protected internal virtual void OnThemeNameChanged(ThemeNameChangedEventArgs e)
    {
      if (this.ThemeNameChanged == null)
        return;
      this.ThemeNameChanged((object) this, e);
    }

    protected virtual void OnThemeChanged()
    {
      if (!this.loaded || this.RootElement.ElementState != ElementState.Loaded || !this.isBehaviorPrepared)
        return;
      this.RootElement.InvalidateMeasure(true);
      this.RootElement.UpdateLayout();
      if (this.WindowState != FormWindowState.Maximized)
      {
        Size size1 = this.ClientSize;
        if (this.Site != null)
        {
          IDesignerHost service = (IDesignerHost) this.Site.GetService(typeof (IDesignerHost));
          if (service != null)
          {
            IDesigner designer = service.GetDesigner((IComponent) this);
            if ((object) this.serializedClientSizeFi == null)
              this.serializedClientSizeFi = designer.GetType().BaseType.GetField("serializedClientSize", BindingFlags.Instance | BindingFlags.NonPublic);
            if ((object) this.serializedClientSizeFi != null)
            {
              Size size2 = (Size) this.serializedClientSizeFi.GetValue((object) designer);
              if (size2 != Size.Empty)
                size1 = size2;
            }
          }
        }
        this.SetClientSizeCore(size1.Width, size1.Height);
      }
      this.PerformLayout((Control) this, "ClientSize");
    }

    protected internal virtual void OnDisplayPropertyChanged(RadPropertyChangedEventArgs e)
    {
    }

    protected virtual void OnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      ToolTipTextNeededEventHandler neededEventHandler = this.Events[RadFormControlBase.ToolTipTextNeededEventKey] as ToolTipTextNeededEventHandler;
      if (neededEventHandler == null || this.IsDisposed || this.Disposing)
        return;
      neededEventHandler(sender, e);
    }

    private void ImageListRecreateHandle(object sender, EventArgs e)
    {
      if (!this.IsHandleCreated)
        return;
      this.InvalidateIfNotSuspended();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      if (this.formBehavior != null)
        this.formBehavior.FormHandleCreated();
      base.OnHandleCreated(e);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (!this.IsMdiChild || !this.Visible)
        return;
      this.UpdateBounds();
    }

    internal void CallUpdateBounds()
    {
      this.UpdateBounds();
    }

    private void DetachImageList(object sender, EventArgs e)
    {
      this.ImageList = (ImageList) null;
    }

    private void DetachSmallImageList(object sender, EventArgs e)
    {
      this.SmallImageList = (ImageList) null;
    }

    void IComponentTreeHandler.InitializeRootElement(RootRadElement rootElement)
    {
    }

    RootRadElement IComponentTreeHandler.CreateRootElement()
    {
      return this.CreateRootElement();
    }

    void IComponentTreeHandler.CreateChildItems(RadElement parent)
    {
      this.CreateChildItems(parent);
    }

    public event ThemeNameChangedEventHandler ThemeNameChanged;

    [Category("Behavior")]
    [Description("Occurs when a RadItem instance inside the RadControl requires ToolTip text. ")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event ToolTipTextNeededEventHandler ToolTipTextNeeded
    {
      add
      {
        this.Events.AddHandler(RadFormControlBase.ToolTipTextNeededEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadFormControlBase.ToolTipTextNeededEventKey, (Delegate) value);
      }
    }

    [Category("Behavior")]
    [Description("Occurs prior the ScreenTip of a RadItem instance inside the RadControl is displayed.")]
    public event ScreenTipNeededEventHandler ScreenTipNeeded
    {
      add
      {
        this.Events.AddHandler(RadFormControlBase.ScreenTipNeededEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadFormControlBase.ScreenTipNeededEventKey, (Delegate) value);
      }
    }

    void IComponentTreeHandler.CallOnThemeNameChanged(
      ThemeNameChangedEventArgs e)
    {
      this.CallOnThemeNameChanged(e);
    }

    internal void CallOnThemeNameChanged(ThemeNameChangedEventArgs e)
    {
      this.OnThemeNameChanged(e);
    }

    protected override void OnAutoSizeChanged(EventArgs e)
    {
      base.OnAutoSizeChanged(e);
      this.ElementTree.OnAutoSizeChanged(e);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.loaded)
        this.elementTree.PerformInnerLayout(true, this.Location.X, this.Location.Y, this.Width, this.Height);
      if (!this.EnableAnalytics || this.oldState == this.WindowState)
        return;
      string analyticsName = this.AnalyticsName;
      ControlTraceMonitor.TrackAtomicFeature("RadForm", string.IsNullOrEmpty(analyticsName) ? this.Text : analyticsName, (object) this.WindowState);
      this.oldState = this.WindowState;
    }

    internal void CallSetClientSizeCore(int x, int y)
    {
      this.SetClientSizeCore(x, y);
    }

    protected override void CreateHandle()
    {
      if (!this.IsDisposed)
        base.CreateHandle();
      if (!(this.cachedClientSize != new Size(-1, -1)))
        return;
      this.SetClientSizeCore(this.cachedClientSize.Width, this.cachedClientSize.Height);
      this.cachedClientSize = new Size(-1, -1);
    }

    protected override void SetClientSizeCore(int x, int y)
    {
      if (!this.loaded || !this.IsHandleCreated)
      {
        this.cachedClientSize = new Size(x, y);
        base.SetClientSizeCore(x, y);
      }
      else if (this.FormBehavior != null && (object) this.clientWidthField != null && ((object) this.clientHeightField != null && (object) this.formStateField != null) && (object) this.formStateSetClientSizeField != null)
      {
        this.Size = new Size(x + this.FormBehavior.ClientMargin.Horizontal, y + this.FormBehavior.ClientMargin.Vertical);
        this.clientWidthField.SetValue((object) this, (object) x);
        this.clientHeightField.SetValue((object) this, (object) y);
        BitVector32.Section index = (BitVector32.Section) this.formStateSetClientSizeField.GetValue((object) this);
        BitVector32 bitVector32 = (BitVector32) this.formStateField.GetValue((object) this);
        bitVector32[index] = 1;
        this.formStateField.SetValue((object) this, (object) bitVector32);
        this.OnClientSizeChanged(EventArgs.Empty);
        bitVector32[index] = 0;
        this.formStateField.SetValue((object) this, (object) bitVector32);
      }
      else
        base.SetClientSizeCore(x, y);
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (this.loaded && this.wmWindowPosChangedProcessing)
      {
        if (this.FormBehavior != null)
        {
          try
          {
            FieldInfo field1 = typeof (Form).GetField("FormStateExWindowBoundsWidthIsClientSize", BindingFlags.Static | BindingFlags.NonPublic);
            FieldInfo field2 = typeof (Form).GetField("formStateEx", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo field3 = typeof (Form).GetField("restoredWindowBounds", BindingFlags.Instance | BindingFlags.NonPublic);
            if ((object) field1 != null)
            {
              if ((object) field2 != null)
              {
                if ((object) field3 != null)
                {
                  Rectangle rectangle = (Rectangle) field3.GetValue((object) this);
                  BitVector32.Section index = (BitVector32.Section) field1.GetValue((object) this);
                  if (((BitVector32) field2.GetValue((object) this))[index] == 1)
                  {
                    width = rectangle.Width + this.FormBehavior.ClientMargin.Horizontal;
                    height = rectangle.Height + this.FormBehavior.ClientMargin.Vertical;
                  }
                }
              }
            }
          }
          catch
          {
          }
        }
      }
      if (!this.scaling)
        base.SetBoundsCore(x, y, width, height, specified);
      if (!this.isInitializing && this.cachedClientSize != new Size(-1, -1) && this.FormBehavior != null)
      {
        Padding padding = TelerikDpiHelper.ScalePadding(this.FormBehavior.ClientMargin, new SizeF(1f / this.RootElement.DpiScaleFactor.Width, 1f / this.RootElement.DpiScaleFactor.Height));
        this.cachedClientSize = new Size(width - padding.Horizontal, height - padding.Vertical);
      }
      if (!this.loaded || this.IsDesignMode)
        return;
      this.elementTree.PerformInnerLayout(true, x, y, width, height);
    }

    public override Size GetPreferredSize(Size proposedSize)
    {
      Size size = base.GetPreferredSize(proposedSize);
      if (!this.IsLoaded || !this.isBehaviorPrepared || (DWMAPI.IsCompositionEnabled || this.FormBorderStyle == FormBorderStyle.None))
        return size;
      size = new Size(size.Width + this.formBehavior.ClientMargin.Horizontal, size.Height + this.formBehavior.ClientMargin.Top);
      return size;
    }

    internal Size CallGetPrefferedSize(Size proposedSize)
    {
      return base.GetPreferredSize(proposedSize);
    }

    internal void CallOnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
    }

    public void InvalidateIfNotSuspended()
    {
      this.InvalidateIfNotSuspended(false);
    }

    internal void InvalidateIfNotSuspended(bool invalidateChildren)
    {
      if (this.IsUpdateSuspended)
        return;
      this.Invalidate(invalidateChildren);
    }

    void IComponentTreeHandler.OnDisplayPropertyChanged(
      RadPropertyChangedEventArgs e)
    {
      this.OnDisplayPropertyChanged(e);
    }

    public bool IsDesignMode
    {
      get
      {
        return this.DesignMode;
      }
    }

    void IComponentTreeHandler.CallOnMouseCaptureChanged(EventArgs e)
    {
      this.CallOnMouseCaptureChanged(e);
    }

    internal void CallOnMouseCaptureChanged(EventArgs e)
    {
      this.OnMouseCaptureChanged(e);
    }

    void IComponentTreeHandler.CallOnToolTipTextNeeded(
      object sender,
      ToolTipTextNeededEventArgs e)
    {
      this.OnToolTipTextNeeded(sender, e);
    }

    public ComponentThemableElementTree ElementTree
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

    [DefaultValue("")]
    [Editor("Telerik.WinControls.UI.Design.ThemeNameEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Browsable(true)]
    [Category("StyleSheet")]
    [Description("Gets or sets theme name.")]
    public string ThemeName
    {
      get
      {
        return this.ElementTree.ThemeName;
      }
      set
      {
        if (!(this.ElementTree.ThemeName != value))
          return;
        this.ElementTree.ThemeName = value;
        this.OnNotifyPropertyChanged(nameof (ThemeName));
      }
    }

    [Browsable(true)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [Description("Gets the RootElement of a Control.")]
    public RootRadElement RootElement
    {
      get
      {
        return this.ElementTree.RootElement;
      }
    }

    public void InvalidateElement(RadElement element)
    {
      if (this.IsUpdateSuspended || !this.Visible)
        return;
      Rectangle boundingRectangle = element.ControlBoundingRectangle;
      if (this.isBehaviorPrepared && this.IsLoaded)
        this.formBehavior.InvalidateElement(element, boundingRectangle);
      else
        this.AddInvalidatedRect(boundingRectangle);
      int num = RadElement.TraceInvalidation ? 1 : 0;
    }

    public void InvalidateElement(RadElement element, Rectangle bounds)
    {
      if (this.IsUpdateSuspended || this.isPainting || !this.Visible)
        return;
      if (this.isBehaviorPrepared && this.IsLoaded)
        this.formBehavior.InvalidateElement(element, bounds);
      else
        this.AddInvalidatedRect(bounds);
      int num = RadElement.TraceInvalidation ? 1 : 0;
    }

    protected void AddInvalidatedRect(Rectangle rect)
    {
      if (rect.IsEmpty)
        return;
      rect.Inflate(1, 1);
      this.Invalidate(rect, false);
    }

    public void SuspendUpdate()
    {
      ++this.suspendUpdateCounter;
    }

    public void ResumeUpdate()
    {
      this.ResumeUpdate(true);
    }

    protected bool IsUpdateSuspended
    {
      get
      {
        return this.suspendUpdateCounter > 0;
      }
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

    ImageList IComponentTreeHandler.SmallImageList
    {
      get
      {
        return this.SmallImageList;
      }
      set
      {
        this.SmallImageList = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(null)]
    [Description("Gets or sets the ImageList that contains the images displayed by this control.")]
    [Category("Appearance")]
    public ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        if (this.imageList == value)
          return;
        EventHandler eventHandler1 = new EventHandler(this.ImageListRecreateHandle);
        EventHandler eventHandler2 = new EventHandler(this.DetachImageList);
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
        this.ElementTree.RootElement.NotifyControlImageListChanged();
        this.InvalidateIfNotSuspended();
      }
    }

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

    public Size ImageScalingSize
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

    bool IComponentTreeHandler.Initializing
    {
      get
      {
        if (!this.loaded)
          return true;
        return this.isInitializing;
      }
    }

    public void RegisterHostedControl(RadHostItem hostElement)
    {
      if (hostElement == null || this.Controls.Contains(hostElement.HostedControl))
        return;
      this.Controls.Add(hostElement.HostedControl);
      if (!this.ElementTree.IsLayoutSuspended)
        return;
      hostElement.HostedControl.SuspendLayout();
    }

    public void UnregisterHostedControl(RadHostItem hostElement, bool removeControl)
    {
      if (hostElement == null || !this.Controls.Contains(hostElement.HostedControl))
        return;
      if (this.ElementTree.IsLayoutSuspended)
        hostElement.HostedControl.ResumeLayout(false);
      if (!removeControl)
        return;
      this.Controls.Remove(hostElement.HostedControl);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool ControlDefinesThemeForElement(RadElement element)
    {
      return false;
    }

    protected virtual void OnScreenTipNeeded(object sender, ScreenTipNeededEventArgs e)
    {
      ScreenTipNeededEventHandler neededEventHandler = this.Events[RadFormControlBase.ScreenTipNeededEventKey] as ScreenTipNeededEventHandler;
      if (neededEventHandler == null || this.IsDisposed || this.Disposing)
        return;
      neededEventHandler(sender, e);
    }

    internal void CallOnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      this.OnToolTipTextNeeded(sender, e);
    }

    internal void CallOnScreenTipNeeded(object sender, ScreenTipNeededEventArgs e)
    {
      this.OnScreenTipNeeded(sender, e);
    }

    void IComponentTreeHandler.CallOnScreenTipNeeded(
      object sender,
      ScreenTipNeededEventArgs e)
    {
      this.OnScreenTipNeeded(sender, e);
    }

    public void ControlThemeChangedCallback()
    {
      this.OnThemeChanged();
    }

    bool IComponentTreeHandler.GetShowFocusCues()
    {
      return this.ShowFocusCues;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ILayoutManager LayoutManager
    {
      get
      {
        return (ILayoutManager) this.contextLayoutManager;
      }
    }

    public void InvokeLayoutCallback(LayoutCallback callback)
    {
      if (this.IsDisposed || this.Disposing || !this.IsHandleCreated)
        return;
      this.BeginInvoke((Delegate) callback, (object) this.LayoutManager);
    }

    public event EventHandler Initialized;

    public bool IsInitialized
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
      if (this.inputBehavior.CommandBindings.Count > 0)
        this.inputBehavior.Shortcuts.AddShortcutsSupport();
      this.isInitialized = true;
      if (this.IsHandleCreated && !this.loaded)
        this.OnLoad(EventArgs.Empty);
      else if (this.loaded && !this.RootElement.IsThemeApplied && !this.isInitializing)
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
        this.Events.AddHandler(RadFormControlBase.PropertyChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadFormControlBase.PropertyChangedEventKey, (Delegate) value);
      }
    }

    protected virtual void HandleDpiChanged()
    {
      float num = 1f;
      if (this.oldDpi != 0)
        num = (float) this.currentDpi / (float) this.oldDpi;
      else if (this.oldDpi == 0)
        num = (float) this.currentDpi / 96f;
      if ((double) num == 1.0)
        return;
      this.maxSizeState = this.MaximumSize;
      this.minSizeState = this.MinimumSize;
      this.MinimumSize = Size.Empty;
      this.MaximumSize = Size.Empty;
      this.SaveAnchorStatesAndMinimumMaximum();
      this.Scale(new SizeF(num, num));
      this.RestoreAnchorStatesAndMinimumMaximum();
      this.MinimumSize = TelerikDpiHelper.ScaleSize(this.minSizeState, new SizeF(num, num));
      this.MaximumSize = TelerikDpiHelper.ScaleSize(this.maxSizeState, new SizeF(num, num));
    }

    private void SaveAnchorStatesAndMinimumMaximum()
    {
      if (this.anchorsStack == null)
        this.anchorsStack = new Stack<Dictionary<Control, AnchorStyles>>();
      if (this.minMaxStack == null)
        this.minMaxStack = new Stack<Dictionary<Control, KeyValuePair<Size, Size>>>();
      Dictionary<Control, AnchorStyles> dictionary1 = new Dictionary<Control, AnchorStyles>();
      Dictionary<Control, KeyValuePair<Size, Size>> dictionary2 = new Dictionary<Control, KeyValuePair<Size, Size>>();
      Queue<Control> controlQueue = new Queue<Control>();
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        controlQueue.Enqueue(control);
      while (controlQueue.Count > 0)
      {
        Control key = controlQueue.Dequeue();
        if (key.Dock == DockStyle.None && key.Anchor != AnchorStyles.None && key.Anchor != (AnchorStyles.Top | AnchorStyles.Left))
        {
          dictionary1.Add(key, key.Anchor);
          key.Anchor = AnchorStyles.None;
        }
        if (key is RadControl && (key.MinimumSize != Size.Empty || key.MaximumSize != Size.Empty))
        {
          dictionary2.Add(key, new KeyValuePair<Size, Size>(key.MinimumSize, key.MaximumSize));
          key.MinimumSize = Size.Empty;
          key.MaximumSize = Size.Empty;
        }
        foreach (Control control in (ArrangedElementCollection) key.Controls)
          controlQueue.Enqueue(control);
      }
      this.anchorsStack.Push(dictionary1);
      this.minMaxStack.Push(dictionary2);
    }

    private void RestoreAnchorStatesAndMinimumMaximum()
    {
      Dictionary<Control, AnchorStyles> dictionary1 = this.anchorsStack.Pop();
      Dictionary<Control, KeyValuePair<Size, Size>> dictionary2 = this.minMaxStack.Pop();
      Queue<Control> controlQueue = new Queue<Control>();
      foreach (Control control in (ArrangedElementCollection) this.Controls)
        controlQueue.Enqueue(control);
      while (controlQueue.Count > 0)
      {
        Control key = controlQueue.Dequeue();
        if (dictionary1.ContainsKey(key))
        {
          key.Anchor = dictionary1[key];
          dictionary1.Remove(key);
        }
        if (dictionary2.ContainsKey(key))
        {
          key.MinimumSize = dictionary2[key].Key;
          key.MaximumSize = dictionary2[key].Value;
          dictionary2.Remove(key);
        }
        foreach (Control control in (ArrangedElementCollection) key.Controls)
          controlQueue.Enqueue(control);
      }
      dictionary1.Clear();
      dictionary2.Clear();
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      if (factor == this.RootElement.DpiScaleFactor || (double) factor.Width == 1.0 && (double) factor.Height == 1.0)
        return;
      Rectangle bounds = this.Bounds;
      bool hasOwnToolbar = this.HasOwnToolbar;
      bool allowTheming = this.AllowTheming;
      if (this.oldDpi != 0)
        bounds = this.GetScaledBounds(bounds, factor, specified);
      if (hasOwnToolbar)
      {
        if ((double) factor.Width > 1.0)
        {
          bounds.Width += (int) Math.Round(7.0 * (double) factor.Width, MidpointRounding.AwayFromZero);
          bounds.Height += (int) Math.Round(17.0 * (double) factor.Height, MidpointRounding.AwayFromZero);
        }
        else
        {
          bounds.Width -= 7;
          bounds.Height -= 17;
        }
      }
      if (this.oldDpi == 0 && (double) factor.Width > 1.0)
      {
        bounds.Height += this.FormBehavior.ClientMargin.Bottom;
        bounds = this.GetScaledBounds(bounds, factor, specified);
        if (this.currentDpi == 0)
          this.currentDpi = (int) Math.Round((double) factor.Width * 96.0, MidpointRounding.AwayFromZero);
      }
      this.RootElement.DpiScaleChanged(factor);
      if (hasOwnToolbar && !allowTheming)
        this.AllowTheming = false;
      this.scaling = true;
      base.ScaleControl(factor, specified);
      this.scaling = false;
      if (!hasOwnToolbar)
        return;
      if (allowTheming)
        this.AllowTheming = true;
      this.Size = bounds.Size;
      this.timer.Interval = 1;
      this.timer.Start();
    }

    protected virtual bool HasOwnToolbar
    {
      get
      {
        return this.AllowTheming;
      }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.timer.Stop();
      this.ElementTree.PerformInnerLayout(true, this.Location.X, this.Location.Y, this.Width, this.Height);
    }

    public bool IsDpiScalingSuspended
    {
      get
      {
        return this.isDpiScalingSuspended;
      }
    }

    public void SuspendDpiScaling()
    {
      this.isDpiScalingSuspended = true;
    }

    public void ResumeDpiScaling()
    {
      this.isDpiScalingSuspended = false;
    }

    [SpecialName]
    string IComponentTreeHandler.get_Name()
    {
      return this.Name;
    }

    [SpecialName]
    void IComponentTreeHandler.set_Name(string value)
    {
      this.Name = value;
    }
  }
}
