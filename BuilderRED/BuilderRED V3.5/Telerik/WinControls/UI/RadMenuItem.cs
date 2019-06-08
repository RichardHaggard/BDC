// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  [DefaultEvent("Click")]
  [Designer("Telerik.WinControls.UI.Design.RadMenuItemDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [RadNewItem("Add New Item", true)]
  public class RadMenuItem : RadMenuItemBase
  {
    private ElementVisibility textSeparatorVisibility = ElementVisibility.Collapsed;
    private int mergeIndex = -1;
    public static RadProperty HintTextProperty = RadProperty.Register(nameof (HintText), typeof (string), typeof (RadMenuItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty CheckStateProperty = RadProperty.Register("CheckState", typeof (Telerik.WinControls.Enumerations.ToggleState), typeof (RadMenuItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Telerik.WinControls.Enumerations.ToggleState.Off, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ShowArrowProperty = RadProperty.Register(nameof (ShowArrow), typeof (bool), typeof (RadMenuItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsMeasure));
    public static RadProperty DescriptionFontProperty = RadProperty.Register(nameof (DescriptionFont), typeof (Font), typeof (RadMenuItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Control.DefaultFont, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty DescriptionTextVisibleProperty = RadProperty.Register(nameof (DescriptionTextVisible), typeof (bool), typeof (RadMenuItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private static readonly object ToggleStateChangedEventKey = new object();
    private static readonly object ToggleStateChangingEventKey = new object();
    public static readonly ActivateMenuItemCommand ActivateMenuItemCommand = new ActivateMenuItemCommand();
    private Form mdiChildFormToActivate;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private RadMenuItemLayout layout;
    private bool checkOnClick;
    private string accessibleName;

    static RadMenuItem()
    {
      RadMenuItem.ActivateMenuItemCommand.Name = nameof (ActivateMenuItemCommand);
      RadMenuItem.ActivateMenuItemCommand.Text = "This command activates the selected menu item.";
      RadMenuItem.ActivateMenuItemCommand.OwnerType = typeof (RadMenuItem);
    }

    public RadMenuItem()
      : this("", (object) null)
    {
    }

    public RadMenuItem(string text)
      : this(text, (object) null)
    {
    }

    public RadMenuItem(string text, object tag)
    {
      this.Text = text;
      this.Tag = tag;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (RadMenuItem);
    }

    protected override void CreateChildElements()
    {
      int num1 = (int) this.SetDefaultValueOverride(RadButtonItem.TextImageRelationProperty, (object) TextImageRelation.ImageBeforeText);
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "RadMenuItemFillPrimitive";
      this.fillPrimitive.BackColor = Color.Empty;
      this.fillPrimitive.GradientStyle = GradientStyles.Solid;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "RadMenuItemBorderPrimitive";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.layout = new RadMenuItemLayout();
      this.layout.Class = "RadMenuItemLayout";
      this.Children.Add((RadElement) this.layout);
      int num2 = (int) this.layout.ImagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.layout.ImagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.layout.ImagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.layout.Checkmark.BindProperty(RadCheckmark.CheckStateProperty, (RadObject) this, RadMenuItem.CheckStateProperty, PropertyBindingOptions.OneWay);
      int num6 = (int) this.layout.Text.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.OneWay);
      int num7 = (int) this.layout.Text.BindProperty(VisualElement.ForeColorProperty, (RadObject) this, VisualElement.ForeColorProperty, PropertyBindingOptions.TwoWay);
      int num8 = (int) this.layout.Description.BindProperty(VisualElement.FontProperty, (RadObject) this, RadMenuItem.DescriptionFontProperty, PropertyBindingOptions.OneWay);
      int num9 = (int) this.layout.Shortcut.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadMenuItem.HintTextProperty, PropertyBindingOptions.OneWay);
      int num10 = (int) this.layout.InternalLayoutPanel.BindProperty(ImageAndTextLayoutPanel.DisplayStyleProperty, (RadObject) this, RadButtonItem.DisplayStyleProperty, PropertyBindingOptions.OneWay);
      int num11 = (int) this.layout.InternalLayoutPanel.BindProperty(ImageAndTextLayoutPanel.ImageAlignmentProperty, (RadObject) this, RadButtonItem.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      int num12 = (int) this.layout.InternalLayoutPanel.BindProperty(ImageAndTextLayoutPanel.TextAlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      int num13 = (int) this.layout.InternalLayoutPanel.BindProperty(ImageAndTextLayoutPanel.TextImageRelationProperty, (RadObject) this, RadButtonItem.TextImageRelationProperty, PropertyBindingOptions.OneWay);
    }

    protected override void OnDropDownCreated()
    {
      base.OnDropDownCreated();
      this.DropDown.Items.ItemsChanged += new ItemChangedDelegate(this.ItemsChanged);
    }

    protected override void DisposeManagedResources()
    {
      this.mdiChildFormToActivate = (Form) null;
      if (this.DropDown != null)
        this.DropDown.Items.ItemsChanged -= new ItemChangedDelegate(this.ItemsChanged);
      base.DisposeManagedResources();
    }

    [Browsable(true)]
    [Category("Action")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Description("Occurs before the elements's state changes.")]
    public event StateChangingEventHandler ToggleStateChanging
    {
      add
      {
        this.Events.AddHandler(RadMenuItem.ToggleStateChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMenuItem.ToggleStateChangingEventKey, (Delegate) value);
      }
    }

    [Category("Action")]
    [Description("Occurs when the elements's state changes.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(true)]
    public event StateChangedEventHandler ToggleStateChanged
    {
      add
      {
        this.Events.AddHandler(RadMenuItem.ToggleStateChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMenuItem.ToggleStateChangedEventKey, (Delegate) value);
      }
    }

    internal Form MdiChildFormToActivate
    {
      get
      {
        return this.mdiChildFormToActivate;
      }
      set
      {
        this.mdiChildFormToActivate = value;
      }
    }

    [Localizable(true)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the text that appears as a HintText for a menu item.")]
    [DefaultValue("")]
    public string HintText
    {
      get
      {
        return (string) this.GetValue(RadMenuItem.HintTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuItem.HintTextProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [DefaultValue(Telerik.WinControls.Enumerations.ToggleState.Off)]
    [Browsable(true)]
    [Bindable(true)]
    public Telerik.WinControls.Enumerations.ToggleState ToggleState
    {
      get
      {
        return (Telerik.WinControls.Enumerations.ToggleState) this.GetValue(RadMenuItem.CheckStateProperty);
      }
      set
      {
        StateChangingEventArgs e = new StateChangingEventArgs(this.ToggleState, value, false);
        this.OnToggleStateChanging(e);
        if (e.Cancel)
          return;
        int num = (int) this.SetValue(RadMenuItem.CheckStateProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ShowArrow", typeof (RadMenuItem))]
    [Browsable(true)]
    [Description("Gets or sets if the arrow is shown when the menu item contains sub menu.")]
    public bool ShowArrow
    {
      get
      {
        return (bool) this.GetValue(RadMenuItem.ShowArrowProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuItem.ShowArrowProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("DescriptionFont - ex. of the descritpion text of an RadMenuItem. The property is inheritable through the element tree.")]
    [RadPropertyDefaultValue("DescriptionFont", typeof (RadMenuItem))]
    public virtual Font DescriptionFont
    {
      get
      {
        return (Font) this.GetValue(RadMenuItem.DescriptionFontProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuItem.DescriptionFontProperty, (object) value);
      }
    }

    [Description("Gets the visibility of description text element.")]
    [Category("Appearance")]
    [DefaultValue(false)]
    public virtual bool DescriptionTextVisible
    {
      get
      {
        return (bool) this.GetValue(RadMenuItem.DescriptionTextVisibleProperty);
      }
    }

    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Bindable(true)]
    [DefaultValue("")]
    [SettingsBindable(true)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the description text associated with this item. ")]
    public string DescriptionText
    {
      get
      {
        if (this.layout != null && this.layout.Description != null)
          return this.layout.Description.Text;
        return "";
      }
      set
      {
        if (this.Layout == null || this.Layout.Description == null)
          return;
        this.Layout.Description.Text = value;
        if (value != string.Empty)
          this.Layout.TextSeparator.Visibility = this.textSeparatorVisibility;
        else
          this.Layout.TextSeparator.Visibility = ElementVisibility.Collapsed;
      }
    }

    [Description("Gets or sets a value indicating whether a menu item should toggle its CheckState on mouse click.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool CheckOnClick
    {
      get
      {
        return this.checkOnClick;
      }
      set
      {
        this.checkOnClick = value;
      }
    }

    [Description("Gets the FillPrimitive of RadMenuItem responsible for the background appearance.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FillPrimitive FillPrimitive
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the BorderPrimitive of RadMenuItem responsible for appearance of the border.")]
    [Browsable(true)]
    public BorderPrimitive BorderPrimitive
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    [Description("Gets or sets a value indicating whether a menu item is in the checked, unchecked, or indeterminate state. ")]
    [Category("Appearance")]
    [Bindable(true)]
    [DefaultValue(false)]
    [Browsable(true)]
    public virtual bool IsChecked
    {
      get
      {
        return this.ToggleState != Telerik.WinControls.Enumerations.ToggleState.Off;
      }
      set
      {
        if (value == this.IsChecked)
          return;
        this.ToggleState = value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      }
    }

    [RelatedImageList("OwnerControl.ImageList")]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the index value of the image that is displayed on the item.")]
    public override int ImageIndex
    {
      get
      {
        return base.ImageIndex;
      }
      set
      {
        base.ImageIndex = value;
      }
    }

    [Category("Appearance")]
    [RelatedImageList("OwnerControl.ImageList")]
    [Browsable(true)]
    [Description("Gets or sets the key accessor for the image in the ImageList.")]
    public override string ImageKey
    {
      get
      {
        return base.ImageKey;
      }
      set
      {
        base.ImageKey = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(-1)]
    [Description("Gets or sets the position of a merged item within the current menu.")]
    [Category("Layout")]
    public int MergeIndex
    {
      get
      {
        int mergeIndex = this.mergeIndex;
        if (mergeIndex >= 0)
          return mergeIndex;
        return -1;
      }
      set
      {
        this.mergeIndex = value;
      }
    }

    [DefaultValue(ElementVisibility.Collapsed)]
    [Description("Gets or sets the visibility of the separator element between the text and the description text.")]
    [Category("Appearance")]
    public virtual ElementVisibility TextSeparatorVisibility
    {
      get
      {
        return this.textSeparatorVisibility;
      }
      set
      {
        this.textSeparatorVisibility = value;
        if (this.DescriptionText != string.Empty)
          this.Layout.TextSeparator.Visibility = value;
        else
          this.Layout.TextSeparator.Visibility = ElementVisibility.Collapsed;
      }
    }

    private bool IsRootMenuItem
    {
      get
      {
        if (this.ElementTree != null)
          return this.ElementTree.Control is RadMenu;
        return false;
      }
    }

    public virtual RadElement LeftColumnElement
    {
      get
      {
        if (this.layout == null)
          return (RadElement) null;
        if (this.Layout.ImagePrimitive.Image != null)
          return (RadElement) this.Layout.ImagePrimitive;
        return (RadElement) this.Layout.Checkmark;
      }
    }

    public virtual RadElement RightColumnElement
    {
      get
      {
        if (this.layout == null)
          return (RadElement) null;
        return (RadElement) this.Layout.ArrowPrimitive;
      }
    }

    public virtual RadDropDownMenuLayout MenuLayout
    {
      get
      {
        return this.FindAncestor<RadDropDownMenuLayout>();
      }
    }

    public virtual RadMenuItemLayout Layout
    {
      get
      {
        return this.layout;
      }
    }

    [Category("Accessibility")]
    [Localizable(true)]
    [Description("Gets or sets the name of the control for use by accessibility client applications.")]
    [DefaultValue("")]
    public override string AccessibleName
    {
      get
      {
        if (string.IsNullOrEmpty(this.accessibleName))
          return this.Text;
        return this.accessibleName;
      }
      set
      {
        this.accessibleName = value;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadButtonItem.ImageProperty || e.Property == RadButtonItem.ImageIndexProperty || e.Property == RadButtonItem.ImageKeyProperty)
      {
        if (this.IsRootMenuItem && this.Image == null && (this.ImageIndex == -1 && this.ImageKey == ""))
          this.Layout.Checkmark.Visibility = ElementVisibility.Collapsed;
        else
          this.Layout.Checkmark.Visibility = ElementVisibility.Visible;
      }
      else if (e.Property == RadMenuItem.ShowArrowProperty)
        this.UpdateArrow();
      else if (e.Property == RadMenuItem.HintTextProperty)
      {
        if (string.IsNullOrEmpty(e.NewValue as string))
          this.Layout.Shortcut.Visibility = ElementVisibility.Collapsed;
        else
          this.Layout.Shortcut.Visibility = ElementVisibility.Visible;
      }
      base.OnPropertyChanged(e);
      if (e.Property != RadMenuItem.CheckStateProperty)
        return;
      foreach (RadElement radElement in this.ChildrenHierarchy)
      {
        if (radElement is RadCheckmark)
        {
          int num1 = (int) radElement.SetValue(RadCheckmark.CheckStateProperty, e.NewValue);
        }
        int num2 = (int) radElement.SetValue(RadMenuItem.CheckStateProperty, e.NewValue);
      }
      this.OnToggleStateChanged(new StateChangedEventArgs(this.ToggleState));
    }

    private void UpdateArrow()
    {
      if (!this.ShowArrow)
      {
        if (this.IsMainMenuItem)
          this.Layout.ArrowPrimitive.Visibility = ElementVisibility.Collapsed;
        else
          this.Layout.ArrowPrimitive.Visibility = ElementVisibility.Hidden;
      }
      else if (this.Items.Count == 0)
      {
        this.Layout.ArrowPrimitive.Visibility = ElementVisibility.Hidden;
      }
      else
      {
        Telerik.WinControls.ArrowDirection arrowDirection = this.RightToLeft ? Telerik.WinControls.ArrowDirection.Left : Telerik.WinControls.ArrowDirection.Right;
        int num = (int) this.Layout.ArrowPrimitive.SetDefaultValueOverride(ArrowPrimitive.DirectionProperty, (object) arrowDirection);
        this.Layout.ArrowPrimitive.Visibility = ElementVisibility.Visible;
      }
    }

    protected override void OnBitStateChanged(long key, bool oldValue, bool newValue)
    {
      base.OnBitStateChanged(key, oldValue, newValue);
      if (key != 2251799813685248L)
        return;
      int num = (int) this.SetDefaultValueOverride(RadMenuItem.ShowArrowProperty, (object) !newValue);
    }

    protected override void OnClick(EventArgs e)
    {
      if (this.IsOnDropDown && this.checkOnClick && !this.DesignMode)
        this.IsChecked = !this.IsChecked;
      base.OnClick(e);
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, "Click");
    }

    protected virtual void ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation != ItemsChangeOperation.Inserted && operation != ItemsChangeOperation.Set && (operation != ItemsChangeOperation.Removed && operation != ItemsChangeOperation.Clearing) && operation != ItemsChangeOperation.Cleared)
        return;
      this.UpdateArrow();
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnToggleStateChanging(StateChangingEventArgs e)
    {
      StateChangingEventHandler changingEventHandler = (StateChangingEventHandler) this.Events[RadMenuItem.ToggleStateChangingEventKey];
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnToggleStateChanged(StateChangedEventArgs e)
    {
      StateChangedEventHandler changedEventHandler = (StateChangedEventHandler) this.Events[RadMenuItem.ToggleStateChangedEventKey];
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, e);
    }

    public bool GetArrowVisible()
    {
      if (!this.ShowArrow)
        return false;
      return this.Items.Count > 0;
    }

    protected override object CoerceValue(RadPropertyValue propVal, object baseValue)
    {
      if (propVal.Property != RadMenuItem.DescriptionTextVisibleProperty)
        return base.CoerceValue(propVal, baseValue);
      if (this.Layout != null && this.Layout.Description != null)
        return (object) (this.Layout.Description.Text != string.Empty);
      return (object) false;
    }

    protected virtual Telerik.WinControls.ArrowDirection TranslateArrowDirection(
      RadDirection dropdownDirection)
    {
      switch (dropdownDirection)
      {
        case RadDirection.Left:
        case RadDirection.Right:
          return Telerik.WinControls.ArrowDirection.Left;
        case RadDirection.Up:
        case RadDirection.Down:
          return Telerik.WinControls.ArrowDirection.Down;
        default:
          return Telerik.WinControls.ArrowDirection.Left;
      }
    }

    internal bool ShowKeyboardCue
    {
      get
      {
        return this.Layout.Text.ShowKeyboardCues;
      }
      set
      {
        this.Layout.Text.ShowKeyboardCues = value;
      }
    }

    protected override bool CanHandleShortcut(ShortcutEventArgs e)
    {
      if (!this.Enabled || !this.IsInValidState(true))
        return false;
      Control ownerControl = this.OwnerControl;
      if (ownerControl == null)
        return false;
      Form form1 = ownerControl.FindForm();
      if (form1 != null)
      {
        Form form2 = e.FocusedControl == null ? Form.ActiveForm : e.FocusedControl.FindForm();
        return form1 == form2;
      }
      RadElement owner = this.Owner as RadElement;
      if (owner != null && owner.ElementTree != null && owner.ElementTree.Control != null)
      {
        Form form2 = owner.ElementTree.Control.FindForm();
        if (form2 != null)
        {
          Form form3 = e.FocusedControl == null ? Form.ActiveForm : e.FocusedControl.FindForm();
          if (form2 != form3)
            return form2.ContainsFocus;
          return true;
        }
      }
      return true;
    }

    protected override void UpdateOnShortcutsChanged()
    {
      base.UpdateOnShortcutsChanged();
      string empty = string.Empty;
      if (this.Shortcuts.Count > 0)
      {
        int num1 = (int) this.SetDefaultValueOverride(RadMenuItem.HintTextProperty, (object) this.Shortcuts.GetDisplayText());
      }
      else
      {
        int num2 = (int) this.ResetValue(RadMenuItem.HintTextProperty, ValueResetFlags.DefaultValueOverride);
      }
    }
  }
}
