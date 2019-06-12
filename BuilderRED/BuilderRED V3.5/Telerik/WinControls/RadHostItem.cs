// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadHostItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadHostItem : RadItem
  {
    private bool clipControl = true;
    private Dictionary<string, object> initValues = new Dictionary<string, object>();
    internal static readonly object EventValidated = new object();
    internal static readonly object EventValidating = new object();
    internal static readonly object GotFocusEventKey = new object();
    internal static readonly object LostFocusEventKey = new object();
    internal const long RouteMessagesStateKey = 8796093022208;
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected const long RadHostItemLastStateKey = 8796093022208;
    private Control hostedControl;
    private RadHostItem.SizeSettingDirection sizeSettingDirection;
    private RadControl radControl;

    public RadHostItem(Control c)
    {
      this.hostedControl = c;
      this.radControl = this.HostedControl as RadControl;
      if (this.radControl != null)
      {
        this.radControl.RootElement.ForeColor = this.ForeColor;
        this.radControl.RootElement.BackColor = this.BackColor;
      }
      else
      {
        this.hostedControl.ForeColor = this.ForeColor;
        this.hostedControl.BackColor = this.BackColor;
      }
      this.hostedControl.Font = this.Font;
      this.WireEvents();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BitState[8796093022208L] = true;
      this.NotifyParentOnMouseInput = true;
      this.CanFocus = true;
    }

    protected override void DisposeManagedResources()
    {
      if (!this.hostedControl.Disposing && !this.hostedControl.IsDisposed)
        this.hostedControl.Dispose();
      base.DisposeManagedResources();
    }

    private void WireEvents()
    {
      this.hostedControl.MouseUp += new MouseEventHandler(this.hostedControl_MouseUp);
      this.hostedControl.MouseDown += new MouseEventHandler(this.hostedControl_MouseDown);
      this.hostedControl.MouseMove += new MouseEventHandler(this.hostedControl_MouseMove);
      this.hostedControl.MouseHover += new EventHandler(this.hostedControl_MouseHover);
      this.hostedControl.Click += new EventHandler(this.hostedControl_Click);
      this.hostedControl.DoubleClick += new EventHandler(this.hostedControl_DoubleClick);
      this.hostedControl.MouseEnter += new EventHandler(this.hostedControl_MouseEnter);
      this.hostedControl.MouseLeave += new EventHandler(this.hostedControl_MouseLeave);
      this.hostedControl.SizeChanged += new EventHandler(this.hostedControl_SizeChanged);
      this.hostedControl.Validating += new CancelEventHandler(this.HandleValidating);
      this.hostedControl.Validated += new EventHandler(this.HandleValidated);
      this.hostedControl.Disposed += new EventHandler(this.hostedControl_Disposed);
      this.hostedControl.LostFocus += new EventHandler(this.hostedControl_LostFocus);
      this.hostedControl.GotFocus += new EventHandler(this.hostedControl_GotFocus);
    }

    private void UnwireEvents()
    {
      this.hostedControl.MouseUp -= new MouseEventHandler(this.hostedControl_MouseUp);
      this.hostedControl.MouseDown -= new MouseEventHandler(this.hostedControl_MouseDown);
      this.hostedControl.MouseMove -= new MouseEventHandler(this.hostedControl_MouseMove);
      this.hostedControl.MouseHover -= new EventHandler(this.hostedControl_MouseHover);
      this.hostedControl.Click -= new EventHandler(this.hostedControl_Click);
      this.hostedControl.DoubleClick -= new EventHandler(this.hostedControl_DoubleClick);
      this.hostedControl.MouseEnter -= new EventHandler(this.hostedControl_MouseEnter);
      this.hostedControl.MouseLeave -= new EventHandler(this.hostedControl_MouseLeave);
      this.hostedControl.SizeChanged -= new EventHandler(this.hostedControl_SizeChanged);
      this.hostedControl.Validating -= new CancelEventHandler(this.HandleValidating);
      this.hostedControl.Validated -= new EventHandler(this.HandleValidated);
      this.hostedControl.Disposed -= new EventHandler(this.hostedControl_Disposed);
      this.hostedControl.LostFocus -= new EventHandler(this.hostedControl_LostFocus);
      this.hostedControl.GotFocus -= new EventHandler(this.hostedControl_GotFocus);
    }

    public Control HostedControl
    {
      get
      {
        return this.hostedControl;
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Description("Allow routing mouse messages to owner control")]
    public bool RouteMessages
    {
      get
      {
        return this.GetBitState(8796093022208L);
      }
      set
      {
        this.SetBitState(8796093022208L, value);
      }
    }

    [Category("Validation")]
    [DefaultValue(true)]
    public bool CausesValidation
    {
      get
      {
        return this.hostedControl.CausesValidation;
      }
      set
      {
        this.hostedControl.CausesValidation = value;
      }
    }

    public override bool Enabled
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
        if (this.HostedControl == null)
          return;
        this.HostedControl.Enabled = value;
      }
    }

    [DefaultValue(true)]
    public virtual bool ClipControl
    {
      get
      {
        return this.clipControl;
      }
      set
      {
        this.clipControl = value;
      }
    }

    [Category("Validation")]
    public event EventHandler Validated
    {
      add
      {
        this.Events.AddHandler(RadHostItem.EventValidated, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadHostItem.EventValidated, (Delegate) value);
      }
    }

    [Category("Validation")]
    public event CancelEventHandler Validating
    {
      add
      {
        this.Events.AddHandler(RadHostItem.EventValidating, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadHostItem.EventValidating, (Delegate) value);
      }
    }

    [Description("Occurs when the element recieves focus.")]
    [Browsable(false)]
    [Category("Property Changed")]
    public event EventHandler GotFocus
    {
      add
      {
        this.Events.AddHandler(RadHostItem.GotFocusEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadHostItem.GotFocusEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the element loses focus.")]
    [Browsable(false)]
    [Category("Property Changed")]
    public event EventHandler LostFocus
    {
      add
      {
        this.Events.AddHandler(RadHostItem.LostFocusEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadHostItem.LostFocusEventKey, (Delegate) value);
      }
    }

    public override bool Focus()
    {
      this.Focus(false);
      if (this.HostedControl is IComponentTreeHandler)
        return (this.HostedControl as IComponentTreeHandler).OnFocusRequested((RadElement) this);
      return false;
    }

    public void UpdateControlVisibility()
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      bool flag = this.Visibility == ElementVisibility.Visible;
      if (flag)
        flag = !this.HasInvisibleAncestor();
      this.hostedControl.Visible = flag;
    }

    protected virtual void OnValidating(CancelEventArgs e)
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadHostItem.EventValidating];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler((object) this, e);
    }

    protected virtual void OnValidated(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadHostItem.EventValidated];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnGotFocus(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadHostItem.GotFocusEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnLostFocus(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadHostItem.LostFocusEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.UpdateControlVisibility();
      this.InitializeValues();
    }

    private void InitializeValues()
    {
      if (this.ElementState != ElementState.Loaded || !this.initValues.ContainsKey("Font"))
        return;
      this.hostedControl.Font = (Font) this.initValues["Font"];
    }

    protected override void OnElementTreeChanged(ComponentThemableElementTree previousTree)
    {
      base.OnElementTreeChanged(previousTree);
      previousTree?.ComponentTreeHandler.UnregisterHostedControl(this, true);
      if (this.ElementTree == null)
        return;
      this.ElementTree.ComponentTreeHandler.RegisterHostedControl(this);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.hostedControl == null)
        return;
      if (e.Property == VisualElement.ForeColorProperty)
        this.SyncForeColor((Color) e.NewValue);
      else if (e.Property == VisualElement.BackColorProperty)
        this.SyncBackColor((Color) e.NewValue);
      else if (e.Property == VisualElement.FontProperty)
      {
        if (this.ElementState != ElementState.Loaded)
          this.initValues["Font"] = (object) (Font) e.NewValue;
        else
          this.SyncFont((Font) e.NewValue);
      }
      else if (e.Property == RadElement.RightToLeftProperty)
      {
        this.SyncRTL((bool) e.NewValue);
      }
      else
      {
        if (e.Property != RadElement.EnabledProperty)
          return;
        this.hostedControl.Enabled = (bool) e.NewValue;
        this.SyncBackColor(this.BackColor);
      }
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (args.RoutedEvent == RootRadElement.RootLayoutSuspendedEvent)
      {
        if (this.HostedControl != null)
          this.HostedControl.SuspendLayout();
      }
      else if (args.RoutedEvent == RootRadElement.RootLayoutResumedEvent && this.HostedControl != null)
        this.HostedControl.ResumeLayout(false);
      this.PerformRoutedEventAction(sender, args);
    }

    protected override void OnTransformationInvalidated()
    {
      this.SyncBoundsWithHostedControl();
    }

    private void hostedControl_GotFocus(object sender, EventArgs e)
    {
      this.OnGotFocus(e);
    }

    private void hostedControl_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus(e);
    }

    private void hostedControl_SizeChanged(object sender, EventArgs e)
    {
      if (!this.AutoSize)
        return;
      RadHostItem.SizeSettingDirection settingDirection = this.sizeSettingDirection;
      switch (settingDirection)
      {
        case RadHostItem.SizeSettingDirection.None:
        case RadHostItem.SizeSettingDirection.FromHostedControl:
          this.sizeSettingDirection = RadHostItem.SizeSettingDirection.FromHostedControl;
          this.InvalidateMeasure();
          if (this.IsDesignMode)
            this.UpdateLayout();
          if (settingDirection != RadHostItem.SizeSettingDirection.None)
            break;
          this.sizeSettingDirection = RadHostItem.SizeSettingDirection.None;
          break;
      }
    }

    private void hostedControl_Disposed(object sender, EventArgs e)
    {
      this.UnwireEvents();
    }

    private void hostedControl_MouseHover(object sender, EventArgs e)
    {
      if (this.ElementState != ElementState.Loaded || !this.GetBitState(8796093022208L))
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.OnMouseHover(e);
    }

    private void hostedControl_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.ElementState != ElementState.Loaded || !this.GetBitState(8796093022208L))
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.OnMouseMove(this.TranslateMouseEventArgsToControl(e));
    }

    private void hostedControl_MouseLeave(object sender, EventArgs e)
    {
      if (this.ElementState != ElementState.Loaded || !this.GetBitState(8796093022208L))
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.OnMouseLeave(e);
    }

    private void hostedControl_MouseEnter(object sender, EventArgs e)
    {
      if (this.ElementState != ElementState.Loaded || !this.GetBitState(8796093022208L))
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.OnMouseEnter(e);
    }

    private void hostedControl_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.ElementState != ElementState.Loaded || !this.GetBitState(8796093022208L))
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.OnMouseDown(this.TranslateMouseEventArgsToControl(e));
    }

    private void hostedControl_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.ElementState != ElementState.Loaded || !this.GetBitState(8796093022208L))
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.OnMouseUp(this.TranslateMouseEventArgsToControl(e));
    }

    private void hostedControl_Click(object sender, EventArgs e)
    {
      if (this.ElementState != ElementState.Loaded || !this.GetBitState(8796093022208L))
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.OnClick(e);
    }

    private void hostedControl_DoubleClick(object sender, EventArgs e)
    {
      if (this.ElementState != ElementState.Loaded || !this.GetBitState(8796093022208L))
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.OnDoubleClick(e);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.AutoSize && this.hostedControl != null)
      {
        sizeF = (SizeF) this.hostedControl.Size;
        if (this.StretchHorizontally)
          sizeF.Width = 0.0f;
        if (this.StretchVertically)
          sizeF.Height = 0.0f;
        sizeF.Height = Math.Min(sizeF.Height, availableSize.Height);
        sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      }
      return sizeF;
    }

    protected override void ArrangeCore(RectangleF finalRect)
    {
      base.ArrangeCore(finalRect);
      if (this.sizeSettingDirection == RadHostItem.SizeSettingDirection.FromHostedControl)
        return;
      this.sizeSettingDirection = RadHostItem.SizeSettingDirection.ToHostedControl;
      Size size1 = Size.Round(finalRect.Size);
      Size size2 = this.hostedControl.Size;
      size2.Width = Math.Min(size2.Width, size1.Width);
      size2.Height = Math.Min(size2.Height, size1.Height);
      this.SetControlBounds(new Rectangle(this.LocationToControl(), new Size(this.StretchHorizontally ? size1.Width : size2.Width, this.StretchVertically ? size1.Height : size2.Height)));
      this.sizeSettingDirection = RadHostItem.SizeSettingDirection.None;
    }

    protected virtual void SyncBoundsWithHostedControl()
    {
      if (!this.IsInValidState(true))
        return;
      Control control = this.ElementTree.Control;
      control.SuspendLayout();
      this.hostedControl.Location = this.LocationToControl();
      control.ResumeLayout(false);
    }

    protected void EnsureHostedControl()
    {
      if (this.ElementState != ElementState.Loaded)
        return;
      this.ElementTree.ComponentTreeHandler.RegisterHostedControl(this);
    }

    private void HandleValidated(object sender, EventArgs e)
    {
      this.OnValidated(e);
    }

    private void HandleValidating(object sender, CancelEventArgs e)
    {
      this.OnValidating(e);
    }

    private void SetControlBounds(Rectangle bounds)
    {
      if (!this.IsInValidState(true))
        return;
      Control control = this.ElementTree.Control;
      if (!(this.hostedControl.Bounds != bounds))
        return;
      control.SuspendLayout();
      if (this.ClipControl)
        this.hostedControl.MaximumSize = Size.Empty;
      this.hostedControl.Bounds = bounds;
      if (this.ClipControl)
      {
        if (this.hostedControl.Height != bounds.Height || this.hostedControl.Width != bounds.Width)
        {
          this.hostedControl.MaximumSize = bounds.Size;
          this.hostedControl.AutoSize = !this.hostedControl.AutoSize;
          this.hostedControl.AutoSize = !this.hostedControl.AutoSize;
        }
        else if (this.hostedControl.MaximumSize != Size.Empty)
        {
          this.hostedControl.MaximumSize = Size.Empty;
          this.hostedControl.AutoSize = !this.hostedControl.AutoSize;
          this.hostedControl.AutoSize = !this.hostedControl.AutoSize;
        }
      }
      control.ResumeLayout(false);
    }

    private void SyncRTL(bool rtl)
    {
      if (rtl)
        this.hostedControl.RightToLeft = RightToLeft.Yes;
      else
        this.hostedControl.RightToLeft = RightToLeft.No;
    }

    private void SyncFont(Font font)
    {
      bool flag = this.IsInValidState(true);
      if (flag)
        this.ElementTree.Control.SuspendLayout();
      float num = this.DpiScaleFactor.Height / NativeMethods.GetMonitorDpi(Screen.PrimaryScreen, NativeMethods.DpiType.Effective).Height;
      if (this.hostedControl.Font.FontFamily != font.FontFamily || this.hostedControl.Font.Style != font.Style || (double) this.hostedControl.Font.Size != (double) font.Size * (double) num)
      {
        this.hostedControl.SuspendLayout();
        this.hostedControl.Font = new Font(font.FontFamily, font.Size * num, font.Style);
        this.hostedControl.ResumeLayout(false);
      }
      if (!flag)
        return;
      this.ElementTree.Control.ResumeLayout(false);
    }

    private void SyncForeColor(Color foreColor)
    {
      if (this.hostedControl is System.Windows.Forms.TextBox && foreColor.A < byte.MaxValue)
        foreColor = Color.FromArgb((int) byte.MaxValue, foreColor);
      if (this.radControl != null)
        this.radControl.RootElement.ForeColor = foreColor;
      else
        this.hostedControl.ForeColor = foreColor;
    }

    private void SyncBackColor(Color backColor)
    {
      if (!this.Enabled && this.UseDefaultDisabledPaint)
        backColor = this.GetGrayScaledBackColor(backColor);
      if (backColor.A < byte.MaxValue && !ControlHelper.GetControlStyle(this.hostedControl, ControlStyles.SupportsTransparentBackColor))
        backColor = Color.FromArgb((int) byte.MaxValue, backColor);
      if (this.radControl != null)
        this.radControl.RootElement.BackColor = backColor;
      else
        this.hostedControl.BackColor = backColor;
    }

    private Color GetGrayScaledBackColor(Color backColor)
    {
      Bitmap bitmap1 = new Bitmap(16, 16);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
        graphics.FillRectangle((Brush) new SolidBrush(backColor), 0, 0, 16, 16);
      Bitmap bitmap2 = new Bitmap(16, 16);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap2))
        ControlPaint.DrawImageDisabled(graphics, (Image) bitmap1, 0, 0, Color.Transparent);
      Color pixel = bitmap2.GetPixel(5, 5);
      bitmap2.Dispose();
      bitmap1.Dispose();
      return pixel;
    }

    private void PerformRoutedEventAction(RadElement sender, RoutedEventArgs args)
    {
      if (args.RoutedEvent == RadElement.VisibilityChangingEvent)
      {
        this.UpdateControlVisibility();
      }
      else
      {
        if (args.RoutedEvent != RadElement.BoundsChangedEvent)
          return;
        this.SyncBoundsWithHostedControl();
      }
    }

    private MouseEventArgs TranslateMouseEventArgsToControl(MouseEventArgs e)
    {
      Point client = this.ElementTree.Control.PointToClient(this.hostedControl.PointToScreen(e.Location));
      return new MouseEventArgs(e.Button, e.Clicks, client.X, client.Y, e.Delta);
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.SyncFont(this.Font);
    }

    private enum SizeSettingDirection
    {
      None,
      ToHostedControl,
      FromHostedControl,
    }
  }
}
