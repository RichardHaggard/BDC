// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Elements;
using Telerik.WinControls.Keyboard;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls
{
  [Designer("Telerik.WinControls.UI.Design.RadItemDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(false)]
  [DefaultBindingProperty("Text")]
  [DefaultProperty("Text")]
  [DesignerSerializer("Telerik.WinControls.UI.Design.RadItemCodeDomSerializer, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [ComVisible(true)]
  public class RadItem : RadComponentElement, ISupportDrag, ISupportDrop, IShortcutProvider, IStylableElement, IStylableNode
  {
    private string keyTip = string.Empty;
    private int maxRippleRadius = 20;
    private int focusBorderMultiplier = 1;
    private int highlightPauseFramesCount = 15;
    private string accessibleDescription = string.Empty;
    private string accessibleName = string.Empty;
    private AccessibleRole accessibleRole = AccessibleRole.StaticText;
    public static RoutedEvent KeyDownEvent = RadElement.RegisterRoutedEvent(nameof (KeyDownEvent), typeof (RadItem));
    public static RoutedEvent KeyPressEvent = RadElement.RegisterRoutedEvent(nameof (KeyPressEvent), typeof (RadItem));
    public static RoutedEvent KeyUpEvent = RadElement.RegisterRoutedEvent(nameof (KeyUpEvent), typeof (RadItem));
    public static RadProperty UseDefaultDisabledPaintProperty = RadProperty.Register(nameof (UseDefaultDisabledPaint), typeof (bool), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextProperty = RadProperty.Register(nameof (Text), typeof (string), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.Cancelable));
    public static RadProperty TextOrientationProperty = RadProperty.Register(nameof (TextOrientation), typeof (Orientation), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty FlipTextProperty = RadProperty.Register(nameof (FlipText), typeof (bool), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static RadProperty StringAlignmentProperty = RadProperty.Register("StringAlignment", typeof (StringAlignment), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StringAlignment.Near, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsAddNewItemProperty = RadProperty.RegisterAttached("IsAddNewItem", typeof (bool), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata(BooleanBoxes.FalseBox, ElementPropertyOptions.None));
    public static RadProperty ToolTipTextProperty = RadProperty.Register(nameof (ToolTipText), typeof (string), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ""));
    public static RadProperty EnableElementShadowProperty = RadProperty.Register(nameof (EnableElementShadow), typeof (bool), typeof (RadItem), new RadPropertyMetadata((object) false));
    public static RadProperty ShadowDepthProperty = RadProperty.Register(nameof (ShadowDepth), typeof (int), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShadowColorProperty = RadProperty.Register(nameof (ShadowColor), typeof (Color), typeof (RadItem), new RadPropertyMetadata((object) Color.FromArgb(170, 0, 0, 0)));
    public static RadProperty EnableRippleAnimationProperty = RadProperty.Register(nameof (EnableRippleAnimation), typeof (bool), typeof (RadItem), new RadPropertyMetadata((object) false));
    public static RadProperty RippleAnimationColorProperty = RadProperty.Register(nameof (RippleAnimationColor), typeof (Color), typeof (RadItem), new RadPropertyMetadata((object) Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)));
    public static RadProperty EnableFocusBorderProperty = RadProperty.Register(nameof (EnableFocusBorder), typeof (bool), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsLayout));
    public static RadProperty FocusBorderColorProperty = RadProperty.Register(nameof (FocusBorderColor), typeof (Color), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Gray, ElementPropertyOptions.AffectsLayout));
    public static RadProperty FocusBorderWidthProperty = RadProperty.Register(nameof (FocusBorderWidth), typeof (int), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3, ElementPropertyOptions.AffectsLayout));
    public static RadProperty EnableFocusBorderAnimationProperty = RadProperty.Register(nameof (EnableFocusBorderAnimation), typeof (bool), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsLayout));
    public static RadProperty EnableHighlightProperty = RadProperty.Register(nameof (EnableHighlight), typeof (bool), typeof (RadItem), new RadPropertyMetadata((object) false));
    public static RadProperty HighlightColorProperty = RadProperty.Register(nameof (HighlightColor), typeof (Color), typeof (RadItem), new RadPropertyMetadata((object) Color.FromArgb(59, Color.White)));
    public static RadProperty EnableBorderHighlightProperty = RadProperty.Register(nameof (EnableBorderHighlight), typeof (bool), typeof (RadItem), new RadPropertyMetadata((object) false));
    public static RadProperty BorderHighlightColorProperty = RadProperty.Register(nameof (BorderHighlightColor), typeof (Color), typeof (RadItem), new RadPropertyMetadata((object) Color.White));
    public static RadProperty BorderHighlightThicknessProperty = RadProperty.Register(nameof (BorderHighlightThickness), typeof (int), typeof (RadItem), new RadPropertyMetadata((object) 1));
    public static RadProperty VisualStateProperty = RadProperty.Register(nameof (VisualState), typeof (string), typeof (RadItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.Cancelable));
    private static readonly object EventQueryAccessibilityHelp = new object();
    private static readonly object EventKeyDown = new object();
    private static readonly object EventKeyPress = new object();
    private static readonly object EventKeyUp = new object();
    private static readonly object ZoomGestureEventKey = new object();
    private static readonly object RotateGestureEventKey = new object();
    private static readonly object PanGestureEventKey = new object();
    private static readonly object PressAndTapGestureEventKey = new object();
    private static readonly object TwoFingerTapGestureEventKey = new object();
    public static readonly FocusCommand FocusCommand = new FocusCommand();
    internal const long DesignTimeAllowDropStateKey = 137438953472;
    internal const long DesignTimeAllowDragStateKey = 274877906944;
    internal const long ContainsMnemonicStateKey = 549755813888;
    internal const long AllowDragStateKey = 1099511627776;
    internal const long AllowDropStateKey = 2199023255552;
    internal const long EnableAnalyticsStateKey = 4398046511104;
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected const long RadItemLastStateKey = 4398046511104;
    public static readonly ClickCommand ActionCommand;
    private RadItemOwnerCollection ownerCollection;
    private RadShortcutCollection shortcuts;
    private bool shortcutFilterAdded;
    private ThemeSettingsOverrideCollection themeSettingsOverride;
    protected bool IsPaintingRipple;
    private Timer rippleTimer;
    private Point rippleCenter;
    private int rippleRadius;
    private int currentRippleStepNumber;
    private Timer focusBorderTimer;
    private Point focusBorderCenter;
    private int focusBorderRadius;
    private int currentFocusBorderAnmationStep;
    private Timer highlightRippleTimer;
    private float currentHighlightRadius;
    private bool isHighlightShrinking;
    private bool isHighlightRippleExpanding;
    private bool isHighlightGrowingToNormal;
    private float highlightRadiusStep;
    private int currenthighlightPauseFramesCount;
    private string themeRole;
    private ItemStateManagerBase stateManager;
    private StateManagerAttachmentData stateManagerAttachmentData;
    private bool suspendApplyOfThemeSettings;

    static RadItem()
    {
      RadItem.FocusCommand.Name = nameof (FocusCommand);
      RadItem.FocusCommand.Text = "This command gives the focus to a selected RadItem instance.";
      RadItem.FocusCommand.OwnerType = typeof (RadItem);
      RadItem.ActionCommand = new ClickCommand();
      RadItem.ActionCommand.Name = nameof (ActionCommand);
      RadItem.ActionCommand.Text = "This command rises the Click event of a selected RadItem instance.";
      RadItem.ActionCommand.OwnerType = typeof (RadItem);
    }

    public RadItem()
    {
      this.shortcuts = new RadShortcutCollection((IShortcutProvider) this);
      this.rippleTimer = new Timer();
      this.rippleTimer.Interval = 1;
      this.rippleTimer.Tick += new EventHandler(this.RippleTimer_Tick);
      this.focusBorderTimer = new Timer();
      this.focusBorderTimer.Interval = 1;
      this.focusBorderTimer.Tick += new EventHandler(this.FocusBorderTimer_Tick);
      this.highlightRippleTimer = new Timer();
      this.highlightRippleTimer.Interval = 1;
      this.highlightRippleTimer.Tick += new EventHandler(this.HighlightRippleTimer_Tick);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = true;
      this.AutoToolTip = this.DefaultAutoToolTip;
      this.InitializeStateManager();
      this.BitState[137438953472L] = true;
      this.BitState[274877906944L] = true;
      this.BitState[8589934592L] = true;
    }

    public event TextChangingEventHandler TextChanging;

    public event EventHandler TextChanged;

    public event EventHandler TextOrientationChanged;

    public event EventHandler FlipTextChanged;

    [Category("Appearance")]
    [RadPropertyDefaultValue("UseDefaultDisabledPaint", typeof (RadItem))]
    [Browsable(true)]
    [Description("Gets or sets whether the item should use the default way for painting the item when disabled (making it gray) or whether the disabled appearance should be controlled by the theme.")]
    public bool UseDefaultDisabledPaint
    {
      get
      {
        return (bool) this.GetValue(RadItem.UseDefaultDisabledPaintProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.UseDefaultDisabledPaintProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TextOrientation", typeof (RadItem))]
    [Description("Specifies the orientation of the text associated with this item. Whether it should appear horizontal or vertical.")]
    [Browsable(true)]
    [Category("Appearance")]
    [Localizable(true)]
    public virtual Orientation TextOrientation
    {
      get
      {
        return (Orientation) this.GetValue(RadItem.TextOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.TextOrientationProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("FlipText", typeof (RadItem))]
    [Localizable(true)]
    [Description("Specifies the text associated with this item will be flipped.")]
    [Browsable(true)]
    public virtual bool FlipText
    {
      get
      {
        return (bool) this.GetValue(RadItem.FlipTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.FlipTextProperty, (object) value);
      }
    }

    public override bool CanHaveOwnStyle
    {
      get
      {
        return true;
      }
    }

    [Description("Gets or sets the text associated with this item.")]
    [SettingsBindable(true)]
    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Localizable(true)]
    [Bindable(true)]
    [Category("Behavior")]
    [RadPropertyDefaultValue("", typeof (RadItem))]
    public virtual string Text
    {
      get
      {
        return (string) this.GetValue(RadItem.TextProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.TextProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool Selectable
    {
      get
      {
        return true;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool DesignTimeAllowDrop
    {
      get
      {
        return this.GetBitState(137438953472L);
      }
      set
      {
        this.SetBitState(137438953472L, value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool DesignTimeAllowDrag
    {
      get
      {
        return this.GetBitState(274877906944L);
      }
      set
      {
        this.SetBitState(274877906944L, value);
      }
    }

    protected virtual bool DefaultAutoToolTip
    {
      get
      {
        return false;
      }
    }

    [DefaultValue(true)]
    public override bool ShouldHandleMouseInput
    {
      get
      {
        return base.ShouldHandleMouseInput;
      }
      set
      {
        base.ShouldHandleMouseInput = value;
      }
    }

    [DefaultValue("")]
    [Category("Behavior")]
    [Localizable(true)]
    public virtual string KeyTip
    {
      get
      {
        return this.keyTip;
      }
      set
      {
        this.keyTip = value.ToUpper();
      }
    }

    protected internal RadItem ParentItem
    {
      get
      {
        RadElement radElement = (RadElement) this;
        while (radElement != null || radElement.Parent != null || radElement is RadItem)
          radElement = radElement.Parent;
        if (radElement != this)
          return radElement as RadItem;
        return (RadItem) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual InputBinding CommandBinding
    {
      get
      {
        if (this.ShortcutsHandler != null)
        {
          InputBindingsCollection bindingByComponent = this.ShortcutsHandler.Behavior.Shortcuts.InputBindings.GetBindingByComponent((IComponent) this);
          if (bindingByComponent != null && bindingByComponent.Count > 0)
            return bindingByComponent[0];
        }
        return (InputBinding) null;
      }
      set
      {
        if (this.ShortcutsHandler == null)
          return;
        if (value == null || value != null && value.IsEmpty)
        {
          this.ShortcutsHandler.Behavior.Shortcuts.InputBindings.RemoveBindingByComponent((IComponent) this);
        }
        else
        {
          if (this.ShortcutsHandler.Behavior.Shortcuts.InputBindings.Contains(value))
            return;
          this.ShortcutsHandler.Behavior.Shortcuts.InputBindings.Add(value);
        }
      }
    }

    protected virtual IComponentTreeHandler ShortcutsHandler
    {
      get
      {
        if (this.ElementTree == null)
          return (IComponentTreeHandler) null;
        return this.ElementTree.ComponentTreeHandler;
      }
    }

    protected internal virtual void SetOwnerCollection(RadItemOwnerCollection ownerCollection)
    {
      this.ownerCollection = ownerCollection;
    }

    protected override void DisposeManagedResources()
    {
      if (this.focusBorderTimer != null)
      {
        this.focusBorderTimer.Stop();
        this.focusBorderTimer.Tick -= new EventHandler(this.FocusBorderTimer_Tick);
        this.focusBorderTimer.Dispose();
      }
      if (this.rippleTimer != null)
      {
        this.rippleTimer.Stop();
        this.rippleTimer.Tick -= new EventHandler(this.RippleTimer_Tick);
        this.rippleTimer.Dispose();
      }
      if (this.highlightRippleTimer != null)
      {
        this.highlightRippleTimer.Stop();
        this.highlightRippleTimer.Tick -= new EventHandler(this.HighlightRippleTimer_Tick);
        this.highlightRippleTimer.Dispose();
      }
      if (this.ownerCollection != null)
      {
        RadElement owner = this.ownerCollection.Owner;
        if (owner != null && owner.IsInValidState(false))
        {
          int index = this.ownerCollection.IndexOf(this);
          if (index >= 0)
            this.ownerCollection.RemoveAt(index);
        }
        this.ownerCollection = (RadItemOwnerCollection) null;
      }
      if (this.shortcutFilterAdded)
        RadShortcutManager.Instance.RemoveShortcutProvider((IShortcutProvider) this);
      if (this.stateManagerAttachmentData != null)
        this.stateManagerAttachmentData.Dispose();
      base.DisposeManagedResources();
    }

    public bool Select()
    {
      if (!this.Selectable)
        return false;
      this.OnSelect();
      return true;
    }

    protected virtual void OnSelect()
    {
      if (!this.IsInValidState(true))
        return;
      (this.ElementTree.Control as IItemsControl)?.SelectItem(this);
    }

    public void Deselect()
    {
      if (!this.Selectable)
        return;
      this.OnDeselect();
    }

    protected virtual void OnDeselect()
    {
      if (!this.IsInValidState(true))
        return;
      IItemsControl control = this.ElementTree.Control as IItemsControl;
      if (control == null || !object.ReferenceEquals((object) control.GetSelectedItem(), (object) this))
        return;
      control.SelectItem((RadItem) null);
    }

    protected virtual void DoKeyDown(KeyEventArgs e)
    {
      this.OnKeyDown(e);
      if (this.ElementTree == null)
        return;
      this.ElementTree.ComponentTreeHandler.Behavior.UpdateScreenTip((ScreenTipNeededEventArgs) null);
    }

    protected virtual void DoKeyPress(KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    protected virtual void DoKeyUp(KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
    {
      add
      {
        this.Events.AddHandler(RadItem.EventQueryAccessibilityHelp, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.EventQueryAccessibilityHelp, (Delegate) value);
      }
    }

    [Description("Occurs when the RadItem has focus and the user presses a key down")]
    [Category("Key")]
    public event KeyEventHandler KeyDown
    {
      add
      {
        this.Events.AddHandler(RadItem.EventKeyDown, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.EventKeyDown, (Delegate) value);
      }
    }

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user presses a key")]
    public event KeyPressEventHandler KeyPress
    {
      add
      {
        this.Events.AddHandler(RadItem.EventKeyPress, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.EventKeyPress, (Delegate) value);
      }
    }

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user releases the pressed key up")]
    public event KeyEventHandler KeyUp
    {
      add
      {
        this.Events.AddHandler(RadItem.EventKeyUp, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.EventKeyUp, (Delegate) value);
      }
    }

    [Description("Occurs when a zoom gesture was sent by a touch input device.")]
    [Browsable(true)]
    [Category("Touch")]
    public event ZoomGestureEventHandler ZoomGesture
    {
      add
      {
        this.Events.AddHandler(RadItem.ZoomGestureEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.ZoomGestureEventKey, (Delegate) value);
      }
    }

    [Category("Touch")]
    [Browsable(true)]
    [Description("Occurs when a rotate gesture was sent by a touch input device.")]
    public event RotateGestureEventHandler RotateGesture
    {
      add
      {
        this.Events.AddHandler(RadItem.RotateGestureEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.RotateGestureEventKey, (Delegate) value);
      }
    }

    [Category("Touch")]
    [Browsable(true)]
    [Description("Occurs when a pan gesture was sent by a touch input device.")]
    public event PanGestureEventHandler PanGesture
    {
      add
      {
        this.Events.AddHandler(RadItem.PanGestureEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.PanGestureEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when a two-finger-tap gesture was sent by a touch input device.")]
    [Category("Touch")]
    public event GestureEventHandler TwoFingerTapGesture
    {
      add
      {
        this.Events.AddHandler(RadItem.TwoFingerTapGestureEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.TwoFingerTapGestureEventKey, (Delegate) value);
      }
    }

    [Category("Touch")]
    [Browsable(true)]
    [Description("Occurs when a press-and-tap gesture was sent by a touch input device.")]
    public event PressAndTapGestureEventHandler PressAndTapGesture
    {
      add
      {
        this.Events.AddHandler(RadItem.PressAndTapGestureEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadItem.PressAndTapGestureEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnKeyDown(KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadItem.EventKeyDown];
      if (keyEventHandler == null)
        return;
      keyEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnKeyPress(KeyPressEventArgs e)
    {
      KeyPressEventHandler pressEventHandler = (KeyPressEventHandler) this.Events[RadItem.EventKeyPress];
      if (pressEventHandler == null)
        return;
      pressEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnKeyUp(KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadItem.EventKeyUp];
      if (keyEventHandler == null)
        return;
      keyEventHandler((object) this, e);
    }

    public override void RaiseBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      int num = this.IsItemHovered ? 1 : 0;
      base.RaiseBubbleEvent(sender, args);
      if (args.Canceled || sender != this)
        return;
      if (args.RoutedEvent == RadItem.KeyDownEvent)
        this.DoKeyDown((KeyEventArgs) args.OriginalEventArgs);
      else if (args.RoutedEvent == RadItem.KeyPressEvent)
      {
        this.DoKeyPress((KeyPressEventArgs) args.OriginalEventArgs);
      }
      else
      {
        if (args.RoutedEvent != RadItem.KeyUpEvent)
          return;
        this.DoKeyUp((KeyEventArgs) args.OriginalEventArgs);
      }
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      base.OnPropertyChanging(args);
      if (args.Property != RadItem.TextProperty)
        return;
      TextChangingEventArgs e = new TextChangingEventArgs((string) args.OldValue, (string) args.NewValue, false);
      this.OnTextChanging(e);
      if (!e.Cancel)
        return;
      args.Cancel = true;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadItem.TextProperty)
      {
        this.InitializeMnemonic(this.Text);
        this.OnTextChanged(EventArgs.Empty);
      }
      else if (e.Property == RadItem.TextOrientationProperty)
        this.OnTextOrientationChanged(EventArgs.Empty);
      else if (e.Property == RadItem.FlipTextProperty)
        this.OnFlipTextChanged(EventArgs.Empty);
      else if (e.Property == RadElement.ClassProperty)
      {
        if (this.themeRole != null || string.IsNullOrEmpty(e.NewValue as string))
          return;
        this.ThemeRole = (string) e.NewValue;
        if (this.ElementTree == null)
          return;
        this.ElementTree.ApplyThemeToElement((RadObject) this);
      }
      else if (e.Property == RadItem.VisualStateProperty)
      {
        if (this.Style != null)
          this.Style.Apply((RadObject) this, false);
        else
          this.ApplyThemeSettingsOverride();
      }
      else
      {
        if (e.Property != RadElement.EnabledProperty || !(bool) e.NewValue || this.UseDefaultDisabledPaint)
          return;
        int num1 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
        int num2 = (int) this.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
        if (this.Style == null)
          return;
        this.Style.Apply((RadObject) this, true);
      }
    }

    private void InitializeMnemonic(string text)
    {
      this.BitState[549755813888L] = TelerikHelper.ContainsMnemonic(text);
    }

    protected virtual void OnTextChanging(TextChangingEventArgs e)
    {
      if (this.TextChanging == null)
        return;
      this.TextChanging((object) this, e);
    }

    protected virtual void OnTextChanged(EventArgs e)
    {
      if (this.TextChanged == null)
        return;
      this.TextChanged((object) this, e);
    }

    protected virtual void OnTextOrientationChanged(EventArgs e)
    {
      if (this.TextOrientationChanged == null)
        return;
      this.TextOrientationChanged((object) this, e);
    }

    protected virtual void OnFlipTextChanged(EventArgs e)
    {
      if (this.FlipTextChanged == null)
        return;
      this.FlipTextChanged((object) this, e);
    }

    protected override void PaintOverride(
      IGraphics screenRadGraphics,
      Rectangle clipRectangle,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      scale.Width *= this.ScaleTransform.Width;
      scale.Height *= this.ScaleTransform.Height;
      if (this.Visibility != ElementVisibility.Visible || this.Size.Width <= 0 || this.Size.Height <= 0)
        return;
      if (this.Enabled || !this.IsThisTheTopDisabledItem || !this.UseDefaultDisabledPaint)
      {
        this.Paint(screenRadGraphics, clipRectangle, angle + this.AngleTransform, scale, useRelativeTransformation);
        this.PaintVisualEffects(screenRadGraphics);
      }
      else
      {
        using (Brush backgroundBrush = (Brush) new SolidBrush(Color.Transparent))
        {
          Bitmap transformedBitmap = this.GetAsTransformedBitmap(clipRectangle, backgroundBrush, angle + this.AngleTransform, scale);
          if (transformedBitmap == null)
            return;
          Graphics underlayGraphics = (Graphics) screenRadGraphics.UnderlayGraphics;
          Matrix transform = underlayGraphics.Transform;
          underlayGraphics.ResetTransform();
          Rectangle boundingRectangle = this.ControlBoundingRectangle;
          boundingRectangle.Intersect(clipRectangle);
          if (boundingRectangle.Width > 0 && boundingRectangle.Height > 0)
            screenRadGraphics.DrawImage(boundingRectangle.Location, (Image) transformedBitmap, false);
          transformedBitmap.Dispose();
          underlayGraphics.Transform = transform;
        }
      }
    }

    private void PaintVisualEffects(IGraphics screenRadGraphics)
    {
      object state = screenRadGraphics.SaveState();
      screenRadGraphics.ResetTransform();
      this.PaintRippleAnimation(screenRadGraphics);
      this.PaintHighlight(screenRadGraphics);
      this.PaintBorderHighlight(screenRadGraphics);
      this.PaintFocusBorder(screenRadGraphics);
      screenRadGraphics.RestoreState(state);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left)
        return;
      this.StartHighlightShrink(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (e.Button != MouseButtons.Left)
        return;
      this.StartRippleAnimation(e);
      this.StartHighlightRipple(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.EnableHighlight && !this.EnableBorderHighlight)
        return;
      this.Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (!this.EnableHighlight && !this.EnableBorderHighlight)
        return;
      this.isHighlightShrinking = false;
      this.isHighlightRippleExpanding = false;
      this.isHighlightGrowingToNormal = false;
      this.CancelRunningHighlightTimer();
      this.Invalidate();
    }

    public bool EnableElementShadow
    {
      get
      {
        return (bool) this.GetValue(RadItem.EnableElementShadowProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.EnableElementShadowProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ShadowDepth", typeof (RadItem))]
    public int ShadowDepth
    {
      get
      {
        return (int) this.GetValue(RadItem.ShadowDepthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.ShadowDepthProperty, (object) value);
      }
    }

    public Color ShadowColor
    {
      get
      {
        return (Color) this.GetValue(RadItem.ShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.ShadowColorProperty, (object) value);
      }
    }

    private void PaintRippleAnimation(IGraphics screenRadGraphics)
    {
      if (!this.rippleTimer.Enabled)
        return;
      using (SolidBrush solidBrush = new SolidBrush(this.RippleAnimationColor))
      {
        Rectangle rect = new Rectangle(this.rippleCenter.X - this.rippleRadius, this.rippleCenter.Y - this.rippleRadius, this.rippleRadius * 2, this.rippleRadius * 2);
        ((Graphics) ((RadGdiGraphics) screenRadGraphics).UnderlayGraphics).FillEllipse((Brush) solidBrush, rect);
      }
    }

    public bool EnableRippleAnimation
    {
      get
      {
        return (bool) this.GetValue(RadItem.EnableRippleAnimationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.EnableRippleAnimationProperty, (object) value);
      }
    }

    public Color RippleAnimationColor
    {
      get
      {
        return (Color) this.GetValue(RadItem.RippleAnimationColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.RippleAnimationColorProperty, (object) value);
      }
    }

    private void RippleTimer_Tick(object sender, EventArgs e)
    {
      this.rippleRadius += (int) Math.Exp((double) this.currentRippleStepNumber * 0.15);
      this.Invalidate();
      ++this.currentRippleStepNumber;
      if (this.rippleRadius < this.maxRippleRadius)
        return;
      this.IsPaintingRipple = false;
      this.rippleTimer.Stop();
      Rectangle bounds = new Rectangle(this.rippleCenter.X - this.maxRippleRadius, this.rippleCenter.Y - this.maxRippleRadius, this.maxRippleRadius * 2, this.maxRippleRadius * 2);
      if (this.ElementTree == null || this.ElementTree.ComponentTreeHandler == null)
        return;
      this.ElementTree.ComponentTreeHandler.InvalidateElement((RadElement) this, bounds);
    }

    protected virtual void StartRippleAnimation(MouseEventArgs e)
    {
      if (!this.EnableRippleAnimation)
        return;
      this.currentRippleStepNumber = 0;
      this.rippleRadius = 0;
      int x = this.Location.X;
      int val1 = Math.Max(this.Size.Width - x, x);
      int y = this.Location.Y;
      int val2 = Math.Max(this.Size.Height - y, y);
      this.maxRippleRadius = Math.Max(val1, val2);
      this.IsPaintingRipple = true;
      this.rippleCenter = e.Location;
      this.rippleTimer.Start();
    }

    public bool EnableFocusBorder
    {
      get
      {
        return (bool) this.GetValue(RadItem.EnableFocusBorderProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.EnableFocusBorderProperty, (object) value);
      }
    }

    public Color FocusBorderColor
    {
      get
      {
        return (Color) this.GetValue(RadItem.FocusBorderColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.FocusBorderColorProperty, (object) value);
      }
    }

    public int FocusBorderWidth
    {
      get
      {
        return (int) this.GetValue(RadItem.FocusBorderWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.FocusBorderWidthProperty, (object) value);
      }
    }

    public bool EnableFocusBorderAnimation
    {
      get
      {
        return (bool) this.GetValue(RadItem.EnableFocusBorderAnimationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.EnableFocusBorderAnimationProperty, (object) value);
      }
    }

    private void FocusBorderTimer_Tick(object sender, EventArgs e)
    {
      this.focusBorderRadius += this.focusBorderMultiplier * (int) Math.Exp((double) this.currentFocusBorderAnmationStep * 0.15);
      ++this.currentFocusBorderAnmationStep;
      this.Invalidate();
      if (this.focusBorderRadius < 0)
      {
        this.focusBorderRadius = 0;
        this.focusBorderTimer.Stop();
      }
      else
      {
        if (this.focusBorderRadius <= this.Size.Width / 2)
          return;
        this.focusBorderRadius = this.Size.Width / 2;
        this.focusBorderTimer.Stop();
      }
    }

    private void PaintFocusBorder(IGraphics screenRadGraphics)
    {
      if (!this.EnableFocusBorder)
        return;
      using (SolidBrush solidBrush = new SolidBrush(this.FocusBorderColor))
      {
        Rectangle rect = new Rectangle(this.focusBorderCenter.X - this.focusBorderRadius, this.focusBorderCenter.Y, this.focusBorderRadius * 2, this.FocusBorderWidth);
        ((Graphics) ((RadGdiGraphics) screenRadGraphics).UnderlayGraphics).FillRectangle((Brush) solidBrush, rect);
      }
    }

    protected virtual void UpdateFocusBorder(bool isFocused)
    {
      if (!this.EnableFocusBorder)
        return;
      if (!this.EnableFocusBorderAnimation)
      {
        if (isFocused)
          this.focusBorderRadius = this.Size.Width / 2;
        else
          this.focusBorderRadius = 0;
      }
      else
      {
        if (isFocused)
        {
          this.focusBorderRadius = 0;
          this.focusBorderMultiplier = 1;
        }
        else
        {
          this.focusBorderRadius = this.Size.Width / 2;
          this.focusBorderMultiplier = -1;
        }
        this.focusBorderCenter = new Point(this.ControlBoundingRectangle.X + this.Size.Width / 2, this.ControlBoundingRectangle.Y + this.Size.Height - this.FocusBorderWidth);
        this.currentFocusBorderAnmationStep = 0;
        this.focusBorderTimer.Start();
      }
    }

    private int NormalHighlightRadius
    {
      get
      {
        if (this.Size.Width <= this.Size.Height)
          return this.Size.Height;
        return this.Size.Width;
      }
    }

    private int MinHighlightRadius
    {
      get
      {
        return (this.Size.Width > this.Size.Height ? this.Size.Width : this.Size.Height) / 4;
      }
    }

    private int MaxHighlightRadius
    {
      get
      {
        return (this.Size.Width > this.Size.Height ? this.Size.Width : this.Size.Height) * 3;
      }
    }

    public bool EnableHighlight
    {
      get
      {
        return (bool) this.GetValue(RadItem.EnableHighlightProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.EnableHighlightProperty, (object) value);
      }
    }

    public Color HighlightColor
    {
      get
      {
        return (Color) this.GetValue(RadItem.HighlightColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.HighlightColorProperty, (object) value);
      }
    }

    private void PaintHighlight(IGraphics screenRadGraphics)
    {
      if (!this.Enabled || !this.EnableHighlight || (this.ElementTree == null || this.ElementTree.Control == null))
        return;
      Point client = this.ElementTree.Control.PointToClient(Control.MousePosition);
      if (!this.ControlBoundingRectangle.Contains(client))
        return;
      bool flag = !this.isHighlightShrinking && !this.isHighlightRippleExpanding && !this.isHighlightGrowingToNormal;
      if (flag)
        this.currentHighlightRadius = (float) this.NormalHighlightRadius;
      int num = (int) Math.Round((double) this.currentHighlightRadius);
      Rectangle rect = new Rectangle(client.X - num, client.Y - num, num * 2, num * 2);
      GraphicsPath path = new GraphicsPath();
      path.AddEllipse(rect);
      PathGradientBrush pathGradientBrush = new PathGradientBrush(path);
      pathGradientBrush.CenterPoint = (PointF) client;
      if (flag || this.isHighlightGrowingToNormal)
        pathGradientBrush.InterpolationColors = new ColorBlend(4)
        {
          Colors = new Color[4]
          {
            Color.Transparent,
            Color.Transparent,
            this.HighlightColor,
            this.HighlightColor
          },
          Positions = new float[4]{ 0.0f, 0.1f, 0.9f, 1f }
        };
      else
        pathGradientBrush.InterpolationColors = new ColorBlend(6)
        {
          Colors = new Color[6]
          {
            Color.Transparent,
            Color.Transparent,
            this.HighlightColor,
            this.HighlightColor,
            Color.Transparent,
            Color.Transparent
          },
          Positions = new float[6]
          {
            0.0f,
            0.35f,
            0.6f,
            0.65f,
            0.9f,
            1f
          }
        };
      Graphics underlayGraphics = (Graphics) ((RadGdiGraphics) screenRadGraphics).UnderlayGraphics;
      GraphicsState gstate = underlayGraphics.Save();
      underlayGraphics.SetClip(this.ControlBoundingRectangle);
      underlayGraphics.FillPath((Brush) pathGradientBrush, path);
      underlayGraphics.Restore(gstate);
      path.Dispose();
      pathGradientBrush.Dispose();
    }

    private void HighlightRippleTimer_Tick(object sender, EventArgs e)
    {
      if (this.isHighlightShrinking)
      {
        this.currentHighlightRadius += this.highlightRadiusStep;
        this.Invalidate();
        if ((double) this.currentHighlightRadius > (double) this.MinHighlightRadius)
          return;
        this.CancelRunningHighlightTimer();
      }
      else if (this.isHighlightRippleExpanding)
      {
        this.currentHighlightRadius += this.highlightRadiusStep;
        this.Invalidate();
        if ((double) this.currentHighlightRadius < (double) this.MaxHighlightRadius)
          return;
        this.isHighlightRippleExpanding = false;
        this.CancelRunningHighlightTimer();
        this.StarthigHlightExpandToNormal();
      }
      else
      {
        if (!this.isHighlightGrowingToNormal)
          return;
        if (this.currenthighlightPauseFramesCount < this.highlightPauseFramesCount)
        {
          ++this.currenthighlightPauseFramesCount;
        }
        else
        {
          this.currentHighlightRadius += this.highlightRadiusStep;
          this.Invalidate();
          if ((double) this.currentHighlightRadius < (double) this.NormalHighlightRadius)
            return;
          this.isHighlightGrowingToNormal = false;
          this.currenthighlightPauseFramesCount = 0;
          this.CancelRunningHighlightTimer();
        }
      }
    }

    private void StartHighlightShrink(MouseEventArgs e)
    {
      if (!this.EnableHighlight)
        return;
      this.isHighlightShrinking = true;
      this.isHighlightRippleExpanding = false;
      this.isHighlightGrowingToNormal = false;
      int num = 8;
      this.currentHighlightRadius = (float) this.NormalHighlightRadius;
      this.highlightRadiusStep = (float) (this.NormalHighlightRadius - this.MinHighlightRadius) / (float) num;
      this.highlightRadiusStep *= -1f;
      this.highlightRippleTimer.Start();
    }

    private void StartHighlightRipple(MouseEventArgs e)
    {
      if (!this.EnableHighlight)
        return;
      this.isHighlightShrinking = false;
      this.isHighlightRippleExpanding = true;
      this.isHighlightGrowingToNormal = false;
      int num = 15;
      this.currentHighlightRadius = (float) this.NormalHighlightRadius;
      this.highlightRadiusStep = (float) (this.MaxHighlightRadius - this.MinHighlightRadius) / (float) num;
      this.highlightRippleTimer.Start();
    }

    private void StarthigHlightExpandToNormal()
    {
      if (!this.EnableHighlight)
        return;
      this.isHighlightShrinking = false;
      this.isHighlightRippleExpanding = false;
      this.isHighlightGrowingToNormal = true;
      int num = 10;
      this.currentHighlightRadius = 1f;
      this.highlightRadiusStep = ((float) this.NormalHighlightRadius - this.currentHighlightRadius) / (float) num;
      this.highlightRippleTimer.Start();
    }

    private void CancelRunningHighlightTimer()
    {
      this.highlightRippleTimer.Stop();
    }

    public bool EnableBorderHighlight
    {
      get
      {
        return (bool) this.GetValue(RadItem.EnableBorderHighlightProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.EnableBorderHighlightProperty, (object) value);
      }
    }

    public Color BorderHighlightColor
    {
      get
      {
        return (Color) this.GetValue(RadItem.BorderHighlightColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.BorderHighlightColorProperty, (object) value);
      }
    }

    public int BorderHighlightThickness
    {
      get
      {
        return (int) this.GetValue(RadItem.BorderHighlightThicknessProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.BorderHighlightThicknessProperty, (object) value);
      }
    }

    private void PaintBorderHighlight(IGraphics screenRadGraphics)
    {
      if (!this.Enabled || !this.EnableBorderHighlight)
        return;
      Point client = this.ElementTree.Control.PointToClient(Control.MousePosition);
      if (!this.ControlBoundingRectangle.Contains(client))
        return;
      Rectangle boundingRectangle = this.ControlBoundingRectangle;
      RectangleF rectangleF = new RectangleF((float) (client.X - boundingRectangle.Width / 4), (float) (client.Y - boundingRectangle.Height * 2), (float) (boundingRectangle.Width / 2), (float) (boundingRectangle.Height * 4));
      GraphicsPath path = new GraphicsPath();
      path.AddRectangle(rectangleF);
      int highlightThickness = this.BorderHighlightThickness;
      RectangleF b = new RectangleF((float) (boundingRectangle.X + highlightThickness), (float) (boundingRectangle.Y + highlightThickness), (float) (boundingRectangle.Width - 2 * highlightThickness), (float) (boundingRectangle.Height - 2 * highlightThickness));
      RectangleF rect = RectangleF.Intersect(rectangleF, b);
      PathGradientBrush pathGradientBrush = new PathGradientBrush(path);
      pathGradientBrush.CenterPoint = (PointF) client;
      pathGradientBrush.CenterColor = this.BorderHighlightColor;
      pathGradientBrush.SurroundColors = new Color[1]
      {
        Color.Transparent
      };
      Graphics underlayGraphics = (Graphics) ((RadGdiGraphics) screenRadGraphics).UnderlayGraphics;
      GraphicsState gstate = underlayGraphics.Save();
      underlayGraphics.SetClip(this.ControlBoundingRectangle);
      underlayGraphics.SetClip(rect, CombineMode.Exclude);
      underlayGraphics.FillPath((Brush) pathGradientBrush, path);
      underlayGraphics.Restore(gstate);
      pathGradientBrush.Dispose();
      path.Dispose();
    }

    protected internal virtual bool ContainsText()
    {
      return true;
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "ToolTipText")
        return new bool?(!string.IsNullOrEmpty((string) property.GetValue((object) this)));
      if (property.Name == "AutoToolTip")
        return new bool?((bool) property.GetValue((object) this));
      if (property.Name == "AccessibleName")
        return new bool?(this.AccessibleName != this.Text);
      if (property.Name == "AccessibleDescription")
        return new bool?(this.AccessibleDescription != this.Text);
      return new bool?();
    }

    protected internal virtual bool ProcessDialogKey(Keys keyData)
    {
      if (keyData != Keys.Return && keyData != Keys.Space)
        return false;
      this.DoClick(EventArgs.Empty);
      return true;
    }

    protected internal virtual bool ProcessCmdKey(ref Message m, Keys keyData)
    {
      return false;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool ProcessMnemonic(char charCode)
    {
      this.DoClick(EventArgs.Empty);
      return true;
    }

    protected virtual string MnemonicText
    {
      get
      {
        char mnemonic = WindowsFormsUtils.GetMnemonic(this.Text, false);
        if (mnemonic != char.MinValue)
          return "Alt+" + (object) mnemonic;
        return (string) null;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override bool SerializeProperties
    {
      get
      {
        return base.SerializeProperties;
      }
      set
      {
        base.SerializeProperties = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallRaiseKeyDown(KeyEventArgs e)
    {
      this.RaiseKeyDown(e);
    }

    protected virtual void RaiseKeyDown(KeyEventArgs e)
    {
      this.RaiseRoutedEvent((RadElement) this, new RoutedEventArgs((EventArgs) e, RadItem.KeyDownEvent));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallRaiseKeyPress(KeyPressEventArgs e)
    {
      this.RaiseKeyPress(e);
    }

    protected virtual void RaiseKeyPress(KeyPressEventArgs e)
    {
      this.RaiseRoutedEvent((RadElement) this, new RoutedEventArgs((EventArgs) e, RadItem.KeyPressEvent));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallRaiseKeyUp(KeyEventArgs e)
    {
      this.RaiseKeyUp(e);
    }

    protected virtual void RaiseKeyUp(KeyEventArgs e)
    {
      this.RaiseRoutedEvent((RadElement) this, new RoutedEventArgs((EventArgs) e, RadItem.KeyUpEvent));
    }

    public override string ToolTipText
    {
      get
      {
        string toolTipText = base.ToolTipText;
        if (!this.AutoToolTip || !string.IsNullOrEmpty(toolTipText))
          return toolTipText;
        string text = this.Text;
        if (WindowsFormsUtils.ContainsMnemonic(text))
        {
          char[] chArray = new char[1]{ '&' };
          text = string.Join("", text.Split(chArray));
        }
        return text;
      }
      set
      {
        base.ToolTipText = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ThemeRole
    {
      get
      {
        if (string.IsNullOrEmpty(this.themeRole))
          return this.GetThemeEffectiveType().Name;
        return this.themeRole;
      }
      set
      {
        if (!(this.themeRole != value))
          return;
        this.themeRole = value;
        if (this.stateManager == null)
          return;
        this.stateManager.ItemStateChanged((RadObject) this, (RadPropertyChangedEventArgs) null);
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string VisualState
    {
      get
      {
        return this.GetValue(RadItem.VisualStateProperty) as string;
      }
      set
      {
        int num = (int) this.SetValue(RadItem.VisualStateProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual ItemStateManagerBase StateManager
    {
      get
      {
        this.InitializeStateManager();
        return this.stateManager;
      }
      set
      {
        if (this.stateManager != null)
          this.stateManager.Detach(this.stateManagerAttachmentData);
        this.stateManager = value;
        if (this.stateManager == null)
          return;
        this.AttachStateManager();
      }
    }

    private void AttachStateManager()
    {
      this.stateManagerAttachmentData = this.stateManager.AttachToItem((RadObject) this);
    }

    private void InitializeStateManager()
    {
      if (this.stateManager != null)
        return;
      ItemStateManagerFactoryBase stateManagerFactory = ItemStateManagerFactoryRegistry.GetStateManagerFactory(this.ThemeEffectiveType, true);
      string str;
      if (stateManagerFactory != null)
      {
        this.stateManager = stateManagerFactory.StateManagerInstance;
        this.stateManagerAttachmentData = this.stateManager.AttachToItem((RadObject) this);
        str = this.stateManager.GetInitialState((RadObject) this);
      }
      else
        str = this.ThemeRole;
      this.VisualState = str;
    }

    internal override PropertyDescriptorCollection ReplaceDefaultDescriptors(
      PropertyDescriptorCollection props)
    {
      PropertyDescriptorCollection descriptorCollection = base.ReplaceDefaultDescriptors(props);
      if ((bool) this.GetValue(RadElement.IsEditedInSpyProperty))
      {
        PropertyDescriptor property = TypeDescriptor.CreateProperty(this.GetType(), "VisualState", typeof (string), (Attribute) new BrowsableAttribute(true));
        descriptorCollection.Add(property);
      }
      return descriptorCollection;
    }

    protected internal void CallOnGesture(GestureEventArgs args)
    {
      this.OnGesture(args);
    }

    protected virtual void OnGesture(GestureEventArgs args)
    {
      switch (args.GestureType)
      {
        case GestureType.Zoom:
          this.OnZoomGesture((ZoomGestureEventArgs) args);
          break;
        case GestureType.Pan:
          this.OnPanGesture((PanGestureEventArgs) args);
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

    protected virtual void OnTwoFingerTapGesture(GestureEventArgs args)
    {
      GestureEventHandler gestureEventHandler = (GestureEventHandler) this.Events[RadItem.TwoFingerTapGestureEventKey];
      if (gestureEventHandler == null)
        return;
      args.Handled = true;
      gestureEventHandler((object) this, args);
    }

    protected virtual void OnPressAndTapGesture(PressAndTapGestureEventArgs args)
    {
      PressAndTapGestureEventHandler gestureEventHandler = (PressAndTapGestureEventHandler) this.Events[RadItem.PressAndTapGestureEventKey];
      if (gestureEventHandler == null)
        return;
      args.Handled = true;
      gestureEventHandler((object) this, args);
    }

    protected virtual void OnPanGesture(PanGestureEventArgs args)
    {
      PanGestureEventHandler gestureEventHandler = (PanGestureEventHandler) this.Events[RadItem.PanGestureEventKey];
      if (gestureEventHandler == null)
        return;
      args.Handled = true;
      gestureEventHandler((object) this, args);
    }

    protected virtual void OnRotateGesture(RotateGestureEventArgs args)
    {
      RotateGestureEventHandler gestureEventHandler = (RotateGestureEventHandler) this.Events[RadItem.RotateGestureEventKey];
      if (gestureEventHandler == null)
        return;
      args.Handled = true;
      gestureEventHandler((object) this, args);
    }

    protected virtual void OnZoomGesture(ZoomGestureEventArgs args)
    {
      ZoomGestureEventHandler gestureEventHandler = (ZoomGestureEventHandler) this.Events[RadItem.ZoomGestureEventKey];
      if (gestureEventHandler == null)
        return;
      args.Handled = true;
      gestureEventHandler((object) this, args);
    }

    bool ISupportDrag.CanDrag(Point dragStartPoint)
    {
      if (!this.AllowDrag)
        return false;
      return this.CanDragCore(dragStartPoint);
    }

    protected virtual bool CanDragCore(Point dragStartPoint)
    {
      if (this.ElementState == ElementState.Loaded)
        return this.ControlBoundingRectangle.Contains(dragStartPoint);
      return false;
    }

    object ISupportDrag.GetDataContext()
    {
      return this.GetDragContextCore();
    }

    protected virtual object GetDragContextCore()
    {
      return (object) null;
    }

    Image ISupportDrag.GetDragHint()
    {
      return this.GetDragHintCore();
    }

    protected virtual Image GetDragHintCore()
    {
      return (Image) this.GetAsBitmapEx(Color.Transparent, 0.0f, new SizeF(1f, 1f));
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowDrag
    {
      get
      {
        return this.BitState[1099511627776L];
      }
      set
      {
        this.SetBitState(1099511627776L, value);
      }
    }

    void ISupportDrop.DragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      this.ProcessDragDrop(dropLocation, dragObject);
    }

    protected virtual void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
    }

    bool ISupportDrop.DragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return this.ProcessDragOver(mousePosition, dragObject);
    }

    protected virtual bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return false;
    }

    void ISupportDrop.DragEnter(Point mousePosition, ISupportDrag dragObject)
    {
      this.ProcessDragEnter(mousePosition, dragObject);
    }

    protected virtual void ProcessDragEnter(Point mousePosition, ISupportDrag dragObject)
    {
    }

    void ISupportDrop.DragLeave(Point oldMousePosition, ISupportDrag dragObject)
    {
      this.ProcessDragLeave(oldMousePosition, dragObject);
    }

    protected virtual void ProcessDragLeave(Point oldMousePosition, ISupportDrag dragObject)
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool AllowDrop
    {
      get
      {
        return this.BitState[2199023255552L];
      }
      set
      {
        this.SetBitState(2199023255552L, value);
      }
    }

    [Browsable(false)]
    public RadShortcutCollection Shortcuts
    {
      get
      {
        return this.shortcuts;
      }
    }

    void IShortcutProvider.OnShortcut(ShortcutEventArgs e)
    {
      if (!this.CanHandleShortcut(e))
        return;
      this.PerformClick();
      e.Handled = true;
    }

    void IShortcutProvider.OnPartialShortcut(PartialShortcutEventArgs e)
    {
      if (!this.CanHandleShortcut((ShortcutEventArgs) e))
        return;
      e.Handled = true;
    }

    void IShortcutProvider.OnShortcutsChanged()
    {
      this.UpdateOnShortcutsChanged();
    }

    protected virtual void UpdateOnShortcutsChanged()
    {
      if (this.ElementState == ElementState.Disposed)
        throw new InvalidOperationException("Changing shortcuts of an already disposed item");
      if (this.shortcuts.Count > 0)
      {
        if (this.shortcutFilterAdded)
          return;
        RadShortcutManager.Instance.AddShortcutProvider((IShortcutProvider) this);
        this.shortcutFilterAdded = true;
      }
      else
      {
        if (!this.shortcutFilterAdded)
          return;
        RadShortcutManager.Instance.RemoveShortcutProvider((IShortcutProvider) this);
        this.shortcutFilterAdded = false;
      }
    }

    protected virtual bool CanHandleShortcut(ShortcutEventArgs e)
    {
      if (this.IsOnActiveForm(e.FocusedControl, true))
        return this.Enabled;
      return false;
    }

    protected virtual bool IsOnActiveForm(Control focusedControl, bool checkLoaded)
    {
      if (checkLoaded && this.ElementState != ElementState.Loaded)
        return false;
      return this.ElementTree.Control.FindForm() == (focusedControl != null ? focusedControl.FindForm() : Form.ActiveForm);
    }

    [Category("Accessibility")]
    [Description("Gets or sets the description that will be reported to accessibility client applications.")]
    [DefaultValue("")]
    public virtual string AccessibleDescription
    {
      get
      {
        if (!string.IsNullOrEmpty(this.accessibleDescription))
          return this.accessibleDescription;
        return this.Text;
      }
      set
      {
        this.accessibleDescription = value;
      }
    }

    [Category("Accessibility")]
    [DefaultValue("")]
    [Description("Gets or sets the name of the control for use by accessibility client applications.")]
    public virtual string AccessibleName
    {
      get
      {
        if (!string.IsNullOrEmpty(this.accessibleName))
          return this.accessibleName;
        return this.Text;
      }
      set
      {
        this.accessibleName = value;
      }
    }

    [Category("Accessibility")]
    [Description("Gets or sets the accessible role of the item, which specifies the type of user interface element of the item.")]
    [DefaultValue(AccessibleRole.StaticText)]
    public virtual AccessibleRole AccessibleRole
    {
      get
      {
        if (this.accessibleRole != AccessibleRole.Default)
          return this.accessibleRole;
        return AccessibleRole.StaticText;
      }
      set
      {
        this.accessibleRole = value;
      }
    }

    public void SetThemeValueOverride(RadProperty property, object value, string state)
    {
      if (this.themeSettingsOverride == null)
      {
        this.themeSettingsOverride = new ThemeSettingsOverrideCollection();
        this.themeSettingsOverride.CollectionChanged += new NotifyCollectionChangedEventHandler(this.themeSettingsOverride_CollectionChanged);
      }
      this.themeSettingsOverride.RemoveSetting(property, state, this);
      this.themeSettingsOverride.Add(new ThemeSettingOverride(property, value, state));
    }

    public void SetThemeValueOverride(
      RadProperty property,
      object value,
      string state,
      string childElementClass)
    {
      if (this.themeSettingsOverride == null)
      {
        this.themeSettingsOverride = new ThemeSettingsOverrideCollection();
        this.themeSettingsOverride.CollectionChanged += new NotifyCollectionChangedEventHandler(this.themeSettingsOverride_CollectionChanged);
      }
      this.themeSettingsOverride.RemoveSetting(property, state, this, childElementClass);
      this.themeSettingsOverride.Add(new ThemeSettingOverride(property, value, state, childElementClass));
    }

    public void SetThemeValueOverride(
      RadProperty property,
      object value,
      string state,
      System.Type childElementType)
    {
      if (this.themeSettingsOverride == null)
      {
        this.themeSettingsOverride = new ThemeSettingsOverrideCollection();
        this.themeSettingsOverride.CollectionChanged += new NotifyCollectionChangedEventHandler(this.themeSettingsOverride_CollectionChanged);
      }
      this.themeSettingsOverride.RemoveSetting(property, state, this, childElementType);
      this.themeSettingsOverride.Add(new ThemeSettingOverride(property, value, state, childElementType));
    }

    public void ResetThemeValueOverride(RadProperty property)
    {
      if (this.themeSettingsOverride == null)
        return;
      this.themeSettingsOverride.RemoveSetting(property, this);
    }

    public void ResetThemeValueOverride(RadProperty property, string state)
    {
      if (this.themeSettingsOverride == null)
        return;
      this.themeSettingsOverride.RemoveSetting(property, state, this);
    }

    public void ResetThemeValueOverrides()
    {
      if (this.themeSettingsOverride == null)
        return;
      this.themeSettingsOverride.RemoveAllSettings(this);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SuspendApplyOfThemeSettings()
    {
      this.suspendApplyOfThemeSettings = true;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ResumeApplyOfThemeSettings()
    {
      this.suspendApplyOfThemeSettings = false;
      this.ApplyThemeSettingOverrideStyles();
    }

    private void themeSettingsOverride_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      if (this.suspendApplyOfThemeSettings)
        return;
      this.ApplyThemeSettingOverrideStyles();
    }

    internal void ApplyThemeSettingOverrideStyles()
    {
      if (this.Style != null)
        this.Style.Apply((RadObject) this, true);
      else
        this.ApplyThemeSettingsOverride();
    }

    internal void ApplyThemeSettingsOverride()
    {
      if (this.themeSettingsOverride == null)
        return;
      this.themeSettingsOverride.ApplySettings((IStylableElement) this);
    }

    public List<string> GetAvailableVisualStates()
    {
      StateDescriptionNode availableStates = ItemStateManagerFactoryRegistry.GetStateManagerFactory(this.ThemeEffectiveType, true).StateManagerInstance.GetAvailableStates(this.ThemeRole);
      List<string> stringList = new List<string>();
      foreach (StateDescriptionNode node in availableStates.Nodes)
        stringList.Add(node.StateName);
      return stringList;
    }

    string IStylableElement.VisualState
    {
      get
      {
        return this.VisualState;
      }
      set
      {
        this.VisualState = value;
      }
    }

    string IStylableElement.ThemeRole
    {
      get
      {
        return this.ThemeRole;
      }
    }

    bool IStylableElement.FallbackToDefaultTheme
    {
      get
      {
        return this.ShouldFallbackToDefaultTheme();
      }
    }

    protected virtual bool ShouldFallbackToDefaultTheme()
    {
      return false;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ContainsMnemonic
    {
      get
      {
        return this.BitState[549755813888L];
      }
      set
      {
        this.SetBitState(549755813888L, value);
      }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the Analytics functionality is enable or disbale for this item.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool EnableAnalytics
    {
      get
      {
        return this.BitState[4398046511104L];
      }
      set
      {
        this.SetBitState(4398046511104L, value);
      }
    }
  }
}
