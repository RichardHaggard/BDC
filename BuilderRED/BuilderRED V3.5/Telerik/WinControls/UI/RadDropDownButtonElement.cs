// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownButtonElement
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
using Telerik.WinControls.Design;
using Telerik.WinControls.Elements;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.RadDropDownButtonElementDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ComVisible(false)]
  public class RadDropDownButtonElement : RadItem, IDropDownMenuOwner, ISiteProvider, IItemsOwner, IImageElement
  {
    private RadDirection dropDownDirection = RadDirection.Down;
    private static Size ArrowButtonDefaultSize = new Size(12, 12);
    public static RadProperty IsPressedProperty = RadProperty.Register(nameof (IsPressed), typeof (bool), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty IsDropDownShownProperty = RadProperty.Register(nameof (IsDropDownShown), typeof (bool), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty MouseOverStateProperty = RadProperty.Register("MouseOverState", typeof (DropDownButtonMouseOverState), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DropDownButtonMouseOverState.None, ElementPropertyOptions.None));
    public static RadProperty ShowArrowProperty = RadProperty.Register(nameof (ShowArrow), typeof (bool), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageProperty = RadProperty.Register(nameof (Image), typeof (Image), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DisplayStyleProperty = RadProperty.Register(nameof (DisplayStyle), typeof (DisplayStyle), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DisplayStyle.ImageAndText, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ImageIndexProperty = RadProperty.Register(nameof (ImageIndex), typeof (int), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageKeyProperty = RadProperty.Register(nameof (ImageKey), typeof (string), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageAlignmentProperty = RadProperty.Register(nameof (ImageAlignment), typeof (ContentAlignment), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty TextAlignmentProperty = RadProperty.Register(nameof (TextAlignment), typeof (ContentAlignment), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleCenter, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsParentArrange));
    public static RadProperty TextImageRelationProperty = RadProperty.Register(nameof (TextImageRelation), typeof (TextImageRelation), typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextImageRelation.Overlay, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    internal const long PreparedForDesignTimeStateKey = 8796093022208;
    internal const long DropDownInheritThemeClassNameStateKey = 17592186044416;
    internal const long RadDropDownButtonElementLastStateKey = 17592186044416;
    protected RadDropDownButtonPopup menu;
    private RadButtonElement actionButton;
    protected DropDownEditorLayoutPanel layoutPanel;
    private RadArrowButtonElement arrowButton;
    private BorderPrimitive borderPrimitive;
    private static readonly object DropDownOpeningEventKey;
    private static readonly object DropDownOpenedEventKey;
    private static readonly object DropDownClosedEventKey;
    private static readonly object DropDownItemClickedEventKey;
    private static readonly object DropDownClosingEventKey;
    public static Dictionary<RadProperty, RadProperty> mappedStyleProperties;

    private static void AddMappedPropertyMappings()
    {
      RadDropDownButtonElement.mappedStyleProperties.Add(ImagePrimitive.ImageProperty, RadButtonItem.ImageProperty);
      RadDropDownButtonElement.mappedStyleProperties.Add(ImagePrimitive.ImageKeyProperty, RadButtonItem.ImageKeyProperty);
    }

    public override RadProperty MapStyleProperty(
      RadProperty propertyToMap,
      string settingType)
    {
      RadProperty radProperty = (RadProperty) null;
      if (RadDropDownButtonElement.mappedStyleProperties.TryGetValue(propertyToMap, out radProperty))
        return radProperty;
      return base.MapStyleProperty(propertyToMap, settingType);
    }

    static RadDropDownButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new DropDownButtonStateManagerFatory(), typeof (RadDropDownButtonElement));
      RadDropDownButtonElement.mappedStyleProperties = new Dictionary<RadProperty, RadProperty>();
      RadDropDownButtonElement.DropDownOpeningEventKey = new object();
      RadDropDownButtonElement.DropDownOpenedEventKey = new object();
      RadDropDownButtonElement.DropDownClosedEventKey = new object();
      RadDropDownButtonElement.DropDownClosingEventKey = new object();
      RadDropDownButtonElement.DropDownItemClickedEventKey = new object();
      RadDropDownButtonElement.AddMappedPropertyMappings();
      RadElement.CanFocusProperty.OverrideMetadata(typeof (RadDropDownButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    }

    public RadDropDownButtonElement()
    {
      this.menu = this.CreateDropDown();
      this.menu.DropDownOpening += new CancelEventHandler(this.menu_DropDownOpening);
      this.menu.DropDownOpened += new EventHandler(this.menu_DropDownOpened);
      this.menu.DropDownClosed += new RadPopupClosedEventHandler(this.menu_DropDownClosed);
      this.menu.DropDownClosing += new RadPopupClosingEventHandler(this.menu_DropDownClosing);
    }

    protected override void DisposeManagedResources()
    {
      this.menu.Dispose();
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ThemeRole = "DropDownButton";
    }

    protected override void CreateChildElements()
    {
      this.arrowButton = new RadArrowButtonElement();
      this.arrowButton.MinSize = RadDropDownButtonElement.ArrowButtonDefaultSize;
      int num1 = (int) this.arrowButton.SetValue(DropDownEditorLayoutPanel.IsArrowButtonProperty, (object) true);
      this.arrowButton.Class = "DropDownButtonArrowButton";
      this.arrowButton.MouseEnter += new EventHandler(this.arrowButton_MouseEnter);
      this.arrowButton.MouseLeave += new EventHandler(this.arrowButton_MouseLeave);
      this.actionButton = (RadButtonElement) new ActionButtonElement();
      int num2 = (int) this.actionButton.BindProperty(RadButtonItem.DisplayStyleProperty, (RadObject) this, RadDropDownButtonElement.DisplayStyleProperty, PropertyBindingOptions.OneWay);
      int num3 = (int) this.actionButton.BindProperty(RadButtonItem.ImageAlignmentProperty, (RadObject) this, RadDropDownButtonElement.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      int num4 = (int) this.actionButton.BindProperty(RadButtonItem.TextAlignmentProperty, (RadObject) this, RadDropDownButtonElement.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      int num5 = (int) this.actionButton.BindProperty(RadButtonItem.TextImageRelationProperty, (RadObject) this, RadDropDownButtonElement.TextImageRelationProperty, PropertyBindingOptions.OneWay);
      int num6 = (int) this.actionButton.BindProperty(RadItem.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num7 = (int) this.actionButton.BindProperty(RadButtonItem.ImageIndexProperty, (RadObject) this, RadDropDownButtonElement.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num8 = (int) this.actionButton.BindProperty(RadButtonItem.ImageProperty, (RadObject) this, RadDropDownButtonElement.ImageProperty, PropertyBindingOptions.TwoWay);
      int num9 = (int) this.actionButton.BindProperty(RadButtonItem.ImageKeyProperty, (RadObject) this, RadDropDownButtonElement.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      int num10 = (int) this.actionButton.SetValue(DropDownEditorLayoutPanel.IsContentProperty, (object) true);
      this.actionButton.MouseLeave += new EventHandler(this.actionButton_MouseLeave);
      this.actionButton.MouseEnter += new EventHandler(this.actionButton_MouseEnter);
      this.actionButton.Click += new EventHandler(this.actionButton_Click);
      this.actionButton.DoubleClick += new EventHandler(this.actionButton_DoubleClick);
      this.actionButton.NotifyParentOnMouseInput = true;
      this.layoutPanel = new DropDownEditorLayoutPanel();
      int num11 = (int) this.layoutPanel.BindProperty(RadElement.AutoSizeModeProperty, (RadObject) this, RadElement.AutoSizeModeProperty, PropertyBindingOptions.OneWay);
      this.layoutPanel.Children.Add((RadElement) this.arrowButton);
      this.layoutPanel.Children.Add((RadElement) this.actionButton);
      DockLayoutPanel.SetDock((RadElement) this.arrowButton, Dock.Right);
      DockLayoutPanel.SetDock((RadElement) this.actionButton, Dock.Left);
      this.layoutPanel.LastChildFill = true;
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.borderPrimitive.Class = "DropDownButtonBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.layoutPanel);
    }

    protected virtual RadDropDownButtonPopup CreateDropDown()
    {
      RadDropDownButtonPopup dropDownButtonPopup = new RadDropDownButtonPopup(this);
      if (this.IsInValidState(true))
        dropDownButtonPopup.ImageList = this.ElementTree.ComponentTreeHandler.ImageList;
      return dropDownButtonPopup;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadDropDownMenu DropDownMenu
    {
      get
      {
        return (RadDropDownMenu) this.menu;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadArrowButtonElement ArrowButton
    {
      get
      {
        return this.arrowButton;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement ActionButton
    {
      get
      {
        return this.actionButton;
      }
    }

    public Size ArrowButtonMinSize
    {
      get
      {
        if (this.arrowButton != null)
          return this.arrowButton.MinSize;
        return Size.Empty;
      }
      set
      {
        if (this.arrowButton == null)
          return;
        this.arrowButton.MinSize = value;
      }
    }

    [Description("Gets or sets a value indicating the position where the arrow button appears in drop-down button.")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("ArrowPosition", typeof (DropDownEditorLayoutPanel))]
    [Browsable(true)]
    public virtual DropDownButtonArrowPosition ArrowPosition
    {
      get
      {
        return this.layoutPanel.ArrowPosition;
      }
      set
      {
        this.layoutPanel.ArrowPosition = value;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    [Description("Gets or sets a value indicating the direction in which the dropdown item emerges from its parent container.")]
    [Category("Appearance")]
    public RadDirection DropDownDirection
    {
      get
      {
        return this.dropDownDirection;
      }
      set
      {
        this.dropDownDirection = value;
      }
    }

    [Browsable(false)]
    [RadDefaultValue("ExpandArrow", typeof (DropDownEditorLayoutPanel))]
    public bool ExpandArrowButton
    {
      get
      {
        return this.layoutPanel.ExpandArrow;
      }
      set
      {
        this.layoutPanel.ExpandArrow = value;
      }
    }

    [Browsable(false)]
    public bool IsDropDownShown
    {
      get
      {
        return (bool) this.GetValue(RadDropDownButtonElement.IsDropDownShownProperty);
      }
    }

    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.menu.Items;
      }
    }

    [Description("Indicates whether the DropDown of the button should have two columns or one column.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool HasTwoColumnDropDown
    {
      get
      {
        return this.menu.IsTwoColumnMenu;
      }
      set
      {
        this.menu.IsTwoColumnMenu = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether an arrow button is displayed on the drop-down button.")]
    [RadPropertyDefaultValue("ShowArrow", typeof (RadDropDownButtonElement))]
    public virtual bool ShowArrow
    {
      get
      {
        return (bool) this.GetValue(RadDropDownButtonElement.ShowArrowProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownButtonElement.ShowArrowProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("Image", typeof (RadButtonItem))]
    [Category("Appearance")]
    [Description("Gets or sets the image that is displayed on a button element.")]
    public virtual Image Image
    {
      get
      {
        return (Image) this.GetValue(RadDropDownButtonElement.ImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownButtonElement.ImageProperty, (object) value);
      }
    }

    [Description("Gets or sets the image list index value of the image displayed on the button control.")]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ImageIndex", typeof (RadButtonItem))]
    public virtual int ImageIndex
    {
      get
      {
        return (int) this.GetValue(RadDropDownButtonElement.ImageIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownButtonElement.ImageIndexProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ImageKey", typeof (RadButtonItem))]
    [Description("Gets or sets the key accessor for the image in the ImageList.")]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RelatedImageList("ElementTree.Control.ImageList")]
    public virtual string ImageKey
    {
      get
      {
        return (string) this.GetValue(RadDropDownButtonElement.ImageKeyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownButtonElement.ImageKeyProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ImageTextRelation", typeof (RadButtonItem))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the position of text and image relative to each other.")]
    public virtual TextImageRelation TextImageRelation
    {
      get
      {
        return (TextImageRelation) this.GetValue(RadDropDownButtonElement.TextImageRelationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownButtonElement.TextImageRelationProperty, (object) value);
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the alignment of image content on the drawing surface.")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("ImageAlignment", typeof (RadButtonItem))]
    public virtual ContentAlignment ImageAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(RadDropDownButtonElement.ImageAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownButtonElement.ImageAlignmentProperty, (object) value);
      }
    }

    [Description("Gets or sets the alignment of text content on the drawing surface.")]
    [RadPropertyDefaultValue("TextAlignment", typeof (RadButtonItem))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public virtual ContentAlignment TextAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(RadDropDownButtonElement.TextAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownButtonElement.TextAlignmentProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Specifies the logical combination of image and text primitives in the element.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [RadPropertyDefaultValue("DisplayStyle", typeof (RadButtonItem))]
    [Browsable(true)]
    public DisplayStyle DisplayStyle
    {
      get
      {
        return (DisplayStyle) this.GetValue(RadDropDownButtonElement.DisplayStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownButtonElement.DisplayStyleProperty, (object) value);
      }
    }

    [ReadOnly(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets a value indicating whether the button item is in the pressed state.")]
    public bool IsPressed
    {
      get
      {
        return (bool) this.GetValue(RadDropDownButtonElement.IsPressedProperty);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadElement BorderElement
    {
      get
      {
        return (RadElement) this.borderPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public DropDownEditorLayoutPanel Layout
    {
      get
      {
        return this.layoutPanel;
      }
    }

    public override string ToolTipText
    {
      get
      {
        return base.ToolTipText;
      }
      set
      {
        base.ToolTipText = value;
        if (this.actionButton != null)
          this.actionButton.ToolTipText = value;
        if (this.arrowButton == null)
          return;
        this.arrowButton.ToolTipText = value;
      }
    }

    public override bool AutoToolTip
    {
      get
      {
        return base.AutoToolTip;
      }
      set
      {
        base.AutoToolTip = value;
        if (this.actionButton != null)
          this.actionButton.AutoToolTip = value;
        if (this.arrowButton == null)
          return;
        this.arrowButton.AutoToolTip = value;
      }
    }

    private void menu_DropDownOpening(object sender, CancelEventArgs e)
    {
      if (this.IsInValidState(true))
        this.DropDownMenu.ImageList = this.ElementTree.ComponentTreeHandler.ImageList;
      this.OnDropDownOpening(e);
    }

    private void menu_DropDownOpened(object sender, EventArgs e)
    {
      int num = (int) this.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, (object) true);
      this.OnDropDownOpened(e);
    }

    private void menu_DropDownClosed(object sender, RadPopupClosedEventArgs e)
    {
      int num = (int) this.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, (object) false);
      this.OnDropDownClosed((EventArgs) e);
    }

    private void menu_DropDownClosing(object sender, RadPopupClosingEventArgs args)
    {
      this.OnDropDownClosing(args);
    }

    private void arrowButton_MouseEnter(object sender, EventArgs e)
    {
      this.SetMouseOverState(DropDownButtonMouseOverState.OverArrowButton);
    }

    private void arrowButton_MouseLeave(object sender, EventArgs e)
    {
      this.SetMouseOverState(DropDownButtonMouseOverState.None);
    }

    private void actionButton_MouseEnter(object sender, EventArgs e)
    {
      this.SetMouseOverState(DropDownButtonMouseOverState.OverActionButton);
    }

    private void actionButton_MouseLeave(object sender, EventArgs e)
    {
      this.SetMouseOverState(DropDownButtonMouseOverState.None);
    }

    protected override void DoClick(EventArgs e)
    {
      base.DoClick(e);
      this.ShowDropDownOnClick();
    }

    protected virtual void ShowDropDownOnClick()
    {
      this.ShowDropDown();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadDropDownButtonElement.IsDropDownShownProperty)
      {
        foreach (RadObject radObject in this.ChildrenHierarchy)
        {
          int num = (int) radObject.SetValue(RadDropDownButtonElement.IsDropDownShownProperty, e.NewValue);
        }
      }
      else if (e.Property == RadDropDownButtonElement.ShowArrowProperty)
      {
        this.arrowButton.Visibility = (bool) e.NewValue ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        this.InvalidateMeasure();
        this.InvalidateArrange();
      }
      else
      {
        if (e.Property != RadElement.RightToLeftProperty)
          return;
        bool newValue = (bool) e.NewValue;
        if (this.Shape != null)
          this.Shape.IsRightToLeft = newValue;
        if (this.ArrowButton != null && this.ArrowButton.Shape != null)
          this.ArrowButton.Shape.IsRightToLeft = newValue;
        if (this.ActionButton == null || this.ActionButton.Shape == null)
          return;
        this.ActionButton.Shape.IsRightToLeft = newValue;
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      IItemsElement itemsElement = (IItemsElement) null;
      for (RadElement parent = this.Parent; parent != null && itemsElement == null; parent = parent.Parent)
        itemsElement = parent as IItemsElement;
      itemsElement?.ItemClicked((RadItem) this);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (!this.Enabled)
        return;
      if (e.KeyCode == Keys.Return && !this.IsDropDownShown && this.IsFocused)
      {
        int num1 = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
        this.PerformClick();
        int num2 = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
      }
      if (e.KeyCode != Keys.Space || !this.IsFocused)
        return;
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      if (!this.Enabled || e.KeyCode != Keys.Space)
        return;
      if (!(bool) this.GetValue(RadButtonItem.IsPressedProperty))
        return;
      try
      {
        this.PerformClick();
      }
      finally
      {
        int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
      }
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      this.DoOnBubbleEvent(sender, args);
    }

    private void actionButton_DoubleClick(object sender, EventArgs e)
    {
      this.OnDoubleClick(e);
    }

    private void actionButton_Click(object sender, EventArgs e)
    {
      this.OnClick(e);
    }

    internal virtual void DoOnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      if (args.RoutedEvent != RadElement.MouseDownEvent || sender != this.arrowButton && sender != this.actionButton || this.menu == null)
        return;
      if (!this.menu.IsVisible && this.Items.Count > 0)
      {
        this.ShowDropDown();
      }
      else
      {
        if (!this.menu.IsVisible)
          return;
        this.menu.ClosePopup(RadPopupCloseReason.Mouse);
      }
    }

    public virtual void ShowDropDown(Point location)
    {
      Rectangle screenRect = RadPopupHelper.GetScreenRect((RadElement) this);
      Rectangle rectangle = RadPopupHelper.EnsureBoundsInScreen(new Rectangle(location, this.menu.Size), screenRect);
      this.menu.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      this.menu.Show(rectangle.Location);
    }

    public virtual void ShowDropDown()
    {
      this.menu.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      if (this.IsDesignMode)
        this.menu.Show((RadItem) this, 0, RadDirection.Down);
      else
        this.menu.Show((RadItem) this, 0, this.dropDownDirection);
    }

    public virtual void HideDropDown()
    {
      this.menu.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Occurs before the drop-down window appears.")]
    public event CancelEventHandler DropDownOpening
    {
      add
      {
        this.Events.AddHandler(RadDropDownButtonElement.DropDownOpeningEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownButtonElement.DropDownOpeningEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownOpening(CancelEventArgs e)
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadDropDownButtonElement.DropDownOpeningEventKey];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler((object) this, e);
    }

    [Description("Occurs before the drop-down window appears.")]
    [Browsable(true)]
    [Category("Behavior")]
    public event EventHandler DropDownOpened
    {
      add
      {
        this.Events.AddHandler(RadDropDownButtonElement.DropDownOpenedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownButtonElement.DropDownOpenedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownOpened(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadDropDownButtonElement.DropDownOpenedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Browsable(true)]
    [Description("Occurs when the drop-down window has closed.")]
    [Category("Behavior")]
    public event EventHandler DropDownClosed
    {
      add
      {
        this.Events.AddHandler(RadDropDownButtonElement.DropDownClosedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownButtonElement.DropDownClosedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownClosed(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadDropDownButtonElement.DropDownClosedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Category("Behavior")]
    [Description("Occurs when the drop-down window is about to close.")]
    [Browsable(true)]
    public event RadPopupClosingEventHandler DropDownClosing
    {
      add
      {
        this.Events.AddHandler(RadDropDownButtonElement.DropDownClosingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownButtonElement.DropDownClosingEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownClosing(RadPopupClosingEventArgs args)
    {
      RadPopupClosingEventHandler closingEventHandler = (RadPopupClosingEventHandler) this.Events[RadDropDownButtonElement.DropDownClosingEventKey];
      if (closingEventHandler == null)
        return;
      closingEventHandler((object) this, args);
    }

    public ISite GetSite()
    {
      ISite site = (ISite) null;
      if (this.ElementTree != null && this.ElementTree.Control.Site != null)
        site = this.ElementTree.Control.Site;
      return site;
    }

    [DefaultValue(false)]
    public bool DropDownInheritsThemeClassName
    {
      get
      {
        return this.GetBitState(17592186044416L);
      }
      set
      {
        this.SetBitState(17592186044416L, value);
      }
    }

    bool IDropDownMenuOwner.ControlDefinesThemeForElement(RadElement element)
    {
      return false;
    }

    private void SetMouseOverState(DropDownButtonMouseOverState state)
    {
      int num1 = (int) this.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) state);
      int num2 = (int) this.arrowButton.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) state);
      foreach (RadObject radObject in this.arrowButton.ChildrenHierarchy)
      {
        int num3 = (int) radObject.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) state);
      }
      int num4 = (int) this.actionButton.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) state);
      foreach (RadObject radObject in this.actionButton.ChildrenHierarchy)
      {
        int num3 = (int) radObject.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) state);
      }
      int num5 = (int) this.borderPrimitive.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) state);
    }
  }
}
