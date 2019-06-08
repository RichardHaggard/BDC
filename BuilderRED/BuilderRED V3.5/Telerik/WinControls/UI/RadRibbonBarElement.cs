// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarElement
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
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Properties;
using Telerik.WinControls.UI.RibbonBar;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class RadRibbonBarElement : LightVisualElement
  {
    private bool collapsingEnabled = true;
    private bool expanded = true;
    private bool collapseRibbonOnTabDoubleClick = true;
    private float? highestTabContentPanelHeight = new float?();
    private readonly Color[] contextualColors = new Color[6]{ Color.FromArgb(15590008), Color.FromArgb(10871438), Color.FromArgb(15457154), Color.FromArgb(14267096), Color.FromArgb(16162701), Color.FromArgb(7905752) };
    private DateTime lastIconClick = DateTime.MinValue;
    public static RadProperty IsRibbonFormActiveProperty = RadProperty.Register("IsRibbonFormActive", typeof (bool), typeof (RadRibbonBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsBackstageModeProperty = RadProperty.Register("IsBackstageMode", typeof (bool), typeof (RadRibbonBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty RibbonFormWindowStateProperty = RadProperty.Register("RibbonFormWindowState", typeof (FormWindowState), typeof (RadRibbonBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) FormWindowState.Normal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty QuickAccessMenuHeightProperty = RadProperty.Register(nameof (QuickAccessMenuHeight), typeof (int), typeof (RadRibbonBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 30, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty QuickAccessToolbarBelowRibbonProperty = RadProperty.Register(nameof (QuickAccessToolbarBelowRibbonProperty), typeof (bool), typeof (RadRibbonBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private int suspendExpandButtonNotifications;
    private bool contentAreaColorsAltered;
    private Timer timer;
    private Control containerControl;
    private Form parentForm;
    private RibbonBarPopup ribbonPopup;
    private RibbonBarLocalizationSettings localizationSettings;
    private RibbonTab removedTab;
    private RibbonTabStripElement tabStrip;
    private RadApplicationMenuButtonElement dropDownButton;
    private RadMenuButtonItem optionsButton;
    private RadMenuButtonItem exitButton;
    private RadImageButtonElement helpButton;
    private RadToggleButtonElement expandButton;
    private RadRibbonBarBackstageView backstageControl;
    private ApplicationMenuStyle applicationMenuStyle;
    private RadMDIControlsItem mdiButton;
    private RadRibbonBarCaption ribbonCaption;
    private RadQuickAccessToolBar quickAccessToolBar;
    private ImagePrimitive iconPrimitive;
    private RadPageViewItem previousItem;
    private RibbonTab tabOldSelected;
    private RadItemOwnerCollection contextualTabGroups;
    private StackLayoutElement buttonsLayout;
    private FillPrimitive captionFill;
    private BorderPrimitive captionBorder;
    private bool? isWindowsSeven;
    private RadElement chunksHolder;
    private RadPageViewItem lastClickedTab;
    private RadPageViewItem lastPopupTab;

    static RadRibbonBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadRibbonBarElementStateManager(), typeof (RadRibbonBarElement));
    }

    public RadRibbonBarElement()
    {
      this.timer = new Timer();
      this.timer.Interval = 10;
      this.timer.Tick += new EventHandler(this.Timer_Tick);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      if (this.containerControl != null)
      {
        this.containerControl.SizeChanged -= new EventHandler(this.Control_SizeChanged);
        this.containerControl.VisibleChanged -= new EventHandler(this.Control_VisibleChanged);
      }
      if (this.ribbonPopup != null && this.ribbonPopup.IsPopupShown)
        this.ribbonPopup.ClosePopup(RadPopupCloseReason.CloseCalled);
      if (this.backstageControl != null)
        this.backstageControl.Dispose();
      this.timer.Stop();
      this.timer.Dispose();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.contextualTabGroups = new RadItemOwnerCollection();
      this.contextualTabGroups.DefaultType = typeof (ContextualTabGroup);
      this.contextualTabGroups.ItemTypes = new System.Type[1]
      {
        typeof (ContextualTabGroup)
      };
      this.contextualTabGroups.ItemsChanged += new ItemChangedDelegate(this.ContextualTabGroups_ItemsChanged);
    }

    protected override void CreateChildElements()
    {
      this.backstageControl = new RadRibbonBarBackstageView();
      this.CreateCaption();
      this.CreateDropDownButton();
      this.CreateTabStrip();
      this.CreateButtons();
      this.Children.Add((RadElement) this.iconPrimitive);
      this.Children.Add((RadElement) this.captionFill);
      this.Children.Add((RadElement) this.quickAccessToolBar);
      this.Children.Add((RadElement) this.ribbonCaption);
      this.Children.Add((RadElement) this.tabStrip);
      this.Children.Add((RadElement) this.dropDownButton);
      this.Children.Add((RadElement) this.buttonsLayout);
      this.CommandTabs.ItemsChanged += new ItemChangedDelegate(this.CommandTabs_ItemsChanged);
    }

    private void CreateCaption()
    {
      this.iconPrimitive = new ImagePrimitive();
      this.iconPrimitive.ShouldHandleMouseInput = true;
      this.iconPrimitive.ImageScaling = ImageScaling.SizeToFit;
      this.iconPrimitive.Alignment = ContentAlignment.MiddleCenter;
      this.iconPrimitive.MouseUp += new MouseEventHandler(this.iconPrimitive_MouseUp);
      this.iconPrimitive.Visibility = this.ApplicationMenuStyle == ApplicationMenuStyle.BackstageView ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      this.captionFill = (FillPrimitive) new RibbonBarCaptionFillPrimitive();
      this.captionFill.ZIndex = -1;
      this.captionFill.Class = "RibbonCaptionFill";
      this.captionBorder = new BorderPrimitive();
      this.captionBorder.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.captionBorder.Class = "RibbonCaptionBorder";
      this.captionFill.Children.Add((RadElement) this.captionBorder);
      this.quickAccessToolBar = new RadQuickAccessToolBar();
      this.quickAccessToolBar.Items.ItemsChanged += new ItemChangedDelegate(this.OnQuickAccessToolbar_ItemsChanged);
      this.quickAccessToolBar.ZIndex = 1;
      this.quickAccessToolBar.Alignment = ContentAlignment.MiddleCenter;
      this.ribbonCaption = new RadRibbonBarCaption();
      this.ribbonCaption.Caption = "RadRibbonBar";
      this.ribbonCaption.Alignment = ContentAlignment.MiddleCenter;
      int num = (int) this.ribbonCaption.BindProperty(RadItem.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.OneWay);
      this.contextualTabGroups.Owner = (RadElement) this.ribbonCaption.CaptionLayout;
    }

    private void CreateDropDownButton()
    {
      this.dropDownButton = new RadApplicationMenuButtonElement();
      this.dropDownButton.CanFocus = true;
      this.dropDownButton.ThemeRole = "RibbonBarApplicationButton";
      this.dropDownButton.DisplayStyle = DisplayStyle.Image;
      this.dropDownButton.StretchHorizontally = false;
      this.dropDownButton.StretchVertically = false;
      this.dropDownButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.dropDownButton.DropDownOpening += new CancelEventHandler(this.dropDownButton_DropDownOpening);
      this.dropDownButton.Click += new EventHandler(this.dropDownButton_Click);
      this.dropDownButton.ArrowButton.Click += new EventHandler(this.dropDownButton_Click);
      this.dropDownButton.ShowArrow = false;
      this.dropDownButton.ZIndex = 3;
      this.dropDownButton.Class = "ApplicationButton";
      if (this.dropDownButton.BorderElement != null)
      {
        this.dropDownButton.BorderElement.Class = "OfficeButtonBorder";
        this.dropDownButton.BorderElement.Visibility = ElementVisibility.Collapsed;
      }
      this.dropDownButton.ActionButton.Class = "OfficeButton";
      if (this.dropDownButton.ActionButton.BorderElement != null)
      {
        this.dropDownButton.ActionButton.BorderElement.Class = "OfficeButtonInnerBorder";
        this.dropDownButton.ActionButton.BorderElement.Visibility = ElementVisibility.Collapsed;
      }
      if (this.dropDownButton.ActionButton.ButtonFillElement != null)
        this.dropDownButton.ActionButton.ButtonFillElement.Class = "OfficeButtonFill";
      this.optionsButton = new RadMenuButtonItem();
      this.optionsButton.Click += new EventHandler(this.OfficeMenuButton_Click);
      this.optionsButton.Text = "Options";
      ((RadApplicationMenuDropDown) this.dropDownButton.DropDownMenu).ButtonItems.Add((RadItem) this.optionsButton);
      this.exitButton = new RadMenuButtonItem();
      this.exitButton.Click += new EventHandler(this.OfficeMenuButton_Click);
      this.exitButton.Text = "Exit";
      ((RadApplicationMenuDropDown) this.dropDownButton.DropDownMenu).ButtonItems.Add((RadItem) this.exitButton);
    }

    private void CreateTabStrip()
    {
      this.tabStrip = new RibbonTabStripElement();
      this.tabStrip.StateManager = new RadRibbonBarElementStateManager().StateManagerInstance;
      this.tabStrip.ItemFitMode = StripViewItemFitMode.Shrink;
      this.tabStrip.ItemSelected += new EventHandler<RadPageViewItemSelectedEventArgs>(this.tabStrip_ItemSelected);
      this.tabStrip.ItemSelecting += new EventHandler<RadPageViewItemSelectingEventArgs>(this.tabStrip_ItemSelecting);
      this.tabStrip.ItemClicked += new EventHandler(this.tabStrip_ItemClicked);
      this.tabStrip.StripButtons = StripViewButtons.All;
      this.tabStrip.StripButtons = StripViewButtons.None;
      this.AddBehavior((PropertyChangeBehavior) new RadRibbonBarElement.BackColorBehavior(this));
    }

    private void CreateButtons()
    {
      this.buttonsLayout = new StackLayoutElement();
      this.buttonsLayout.FitInAvailableSize = true;
      this.buttonsLayout.Orientation = Orientation.Horizontal;
      this.buttonsLayout.StretchHorizontally = false;
      this.buttonsLayout.StretchVertically = false;
      this.buttonsLayout.Class = "ButtonsContainer";
      this.expandButton = new RadToggleButtonElement();
      this.expandButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.expandButton.ToggleStateChanged += new StateChangedEventHandler(this.expandButton_ToggleStateChanged);
      this.expandButton.ToggleStateChanging += new StateChangingEventHandler(this.expandButton_ToggleStateChanging);
      this.expandButton.Class = "RibbonBarExpandButton";
      this.expandButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.DropDown2;
      this.expandButton.StretchHorizontally = false;
      this.expandButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.buttonsLayout.Children.Add((RadElement) this.expandButton);
      this.helpButton = new RadImageButtonElement();
      this.helpButton.Visibility = ElementVisibility.Collapsed;
      this.helpButton.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.ribbonBarHelpButton;
      this.helpButton.StretchHorizontally = false;
      this.helpButton.ImageAlignment = ContentAlignment.MiddleCenter;
      this.helpButton.Class = "RibbonBarHelpButton";
      this.buttonsLayout.Children.Add((RadElement) this.helpButton);
      this.MDIbutton = new RadMDIControlsItem();
      this.MDIbutton.Class = "MDI control box";
      this.MDIbutton.ThemeRole = "MdiButtonsItem";
      this.buttonsLayout.Children.Add((RadElement) this.MDIbutton);
    }

    public ImagePrimitive IconPrimitive
    {
      get
      {
        return this.iconPrimitive;
      }
    }

    private bool IsWindowsSeven
    {
      get
      {
        if (!this.isWindowsSeven.HasValue)
          this.isWindowsSeven = new bool?(Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor >= 1);
        return this.isWindowsSeven.Value;
      }
    }

    public int ApplicationMenuRightColumnWidth
    {
      get
      {
        return ((RadApplicationMenuDropDown) this.dropDownButton.DropDownMenu).RightColumnWidth;
      }
      set
      {
        ((RadApplicationMenuDropDown) this.dropDownButton.DropDownMenu).RightColumnWidth = value;
      }
    }

    public RadRibbonBarCaption RibbonCaption
    {
      get
      {
        return this.ribbonCaption;
      }
      set
      {
        this.ribbonCaption = value;
      }
    }

    public ApplicationMenuStyle ApplicationMenuStyle
    {
      get
      {
        return this.applicationMenuStyle;
      }
      set
      {
        if (value == this.applicationMenuStyle)
          return;
        this.applicationMenuStyle = value;
        this.OnApplicationMenuStyleChanged(EventArgs.Empty);
      }
    }

    public RadMDIControlsItem MDIbutton
    {
      get
      {
        return this.mdiButton;
      }
      internal set
      {
        this.mdiButton = value;
      }
    }

    public RadRibbonBarBackstageView BackstageControl
    {
      get
      {
        return this.backstageControl;
      }
      set
      {
        this.backstageControl = value;
        this.backstageControl.Owner = this;
      }
    }

    public StackLayoutElement ButtonsContainer
    {
      get
      {
        return this.buttonsLayout;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a boolean value determining whether the groups are collapsed according to the ribbon's size.")]
    [DefaultValue(true)]
    public bool CollapsingEnabled
    {
      get
      {
        return this.collapsingEnabled;
      }
      set
      {
        this.collapsingEnabled = value;
        ExpandableStackLayout.SetCollapsingEnabled((RadElement) this, value);
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    public bool MinimizeButton
    {
      get
      {
        return this.ribbonCaption.MinimizeButton.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.ribbonCaption.MinimizeButton.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    public bool MaximizeButton
    {
      get
      {
        return this.ribbonCaption.MaximizeButton.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.ribbonCaption.MaximizeButton.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [Description("Gets or sets a value indicating whether the ribbon bar will be collapsed or expanded on ribbon tab double click.")]
    [DefaultValue(true)]
    public bool CollapseRibbonOnTabDoubleClick
    {
      get
      {
        return this.collapseRibbonOnTabDoubleClick;
      }
      set
      {
        if (this.collapseRibbonOnTabDoubleClick == value)
          return;
        if (!value && this.CommandTabs.Count > 0)
          this.Expanded = true;
        this.collapseRibbonOnTabDoubleClick = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(nameof (CollapseRibbonOnTabDoubleClick)));
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    public bool CloseButton
    {
      get
      {
        return this.ribbonCaption.CloseButton.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.ribbonCaption.CloseButton.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [Description("Gets a collection of the command tabs.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RadEditItemsAction]
    [Editor("Telerik.WinControls.UI.Design.CommandTabsCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public RadRibbonBarCommandTabCollection CommandTabs
    {
      get
      {
        return this.tabStrip.TabItems;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RibbonBarLocalizationSettings LocalizationSettings
    {
      get
      {
        if (this.localizationSettings == null)
          this.localizationSettings = new RibbonBarLocalizationSettings(this);
        return this.localizationSettings;
      }
    }

    [Description("Gets a collection of the contextual tab groups.")]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RadNewItem("Add New Group...", true, true, false)]
    public RadItemOwnerCollection ContextualTabGroups
    {
      get
      {
        return this.contextualTabGroups;
      }
    }

    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Get or sets value indicating whether RibbonBar Help button is visible or hidden.")]
    public bool ShowHelpButton
    {
      get
      {
        return this.HelpButton.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.HelpButton.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [DefaultValue(true)]
    [Description("Get or sets value indicating whether RibbonBar Help button is visible or hidden.")]
    [Category("Appearance")]
    public bool ShowExpandButton
    {
      get
      {
        return this.ExpandButton.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.ExpandButton.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection of quick access toolbar items.")]
    public RadItemOwnerCollection QuickAccessMenuItems
    {
      get
      {
        return this.quickAccessToolBar.Items;
      }
    }

    [Browsable(true)]
    [RadPropertyDefaultValue("QuickAccessMenuHeight", typeof (RadRibbonBarElement))]
    [Category("Behavior")]
    [Description("Gets or sets the height of the quick access.")]
    public int QuickAccessMenuHeight
    {
      get
      {
        return (int) this.GetValue(RadRibbonBarElement.QuickAccessMenuHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadRibbonBarElement.QuickAccessMenuHeightProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("QuickAccessToolbarBelowRibbon", typeof (RadRibbonBarElement))]
    [Description("Gets or sets if the quick access toolbar is below the ribbon.")]
    [Browsable(true)]
    [Category("Behavior")]
    public bool QuickAccessToolbarBelowRibbon
    {
      get
      {
        return (bool) this.GetValue(RadRibbonBarElement.QuickAccessToolbarBelowRibbonProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadRibbonBarElement.QuickAccessToolbarBelowRibbonProperty, (object) value);
      }
    }

    [Localizable(true)]
    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]
    [Category("Behavior")]
    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the image of the start button placed in the top left corner.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public Image StartButtonImage
    {
      get
      {
        return this.dropDownButton.Image;
      }
      set
      {
        this.dropDownButton.Image = value;
      }
    }

    [Browsable(false)]
    [Category("Action")]
    [Description("Gets the application menu element")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadApplicationMenuButtonElement ApplicationButtonElement
    {
      get
      {
        return this.dropDownButton;
      }
    }

    [Browsable(false)]
    [Category("Action")]
    [Description("Gets the options menu button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadMenuButtonItem OptionsButton
    {
      get
      {
        return this.optionsButton;
      }
    }

    [Browsable(false)]
    [Category("Action")]
    [Description("Gets the exit menu button")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadMenuButtonItem ExitButton
    {
      get
      {
        return this.exitButton;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [Description("Gets the collection of the start button menu item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection StartMenuItems
    {
      get
      {
        return this.dropDownButton.Items;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [Description("Gets the collection of the start button menu items which appear on the right.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection StartMenuRightColumnItems
    {
      get
      {
        return (this.dropDownButton.DropDownMenu as RadApplicationMenuDropDown)?.RightColumnItems;
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [Description("Gets the collection of the start button menu items which appear on the right.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadItemOwnerCollection StartMenuBottomStrip
    {
      get
      {
        return (this.dropDownButton.DropDownMenu as RadApplicationMenuDropDown)?.ButtonItems;
      }
    }

    [Browsable(true)]
    [Category("Layout")]
    [DefaultValue(300)]
    [Description("Gets or sets the width of the start menu")]
    public int StartMenuWidth
    {
      get
      {
        RadApplicationMenuDropDown dropDownMenu = this.dropDownButton.DropDownMenu as RadApplicationMenuDropDown;
        if (dropDownMenu != null)
          return dropDownMenu.RightColumnWidth;
        return 0;
      }
      set
      {
        RadApplicationMenuDropDown dropDownMenu = this.dropDownButton.DropDownMenu as RadApplicationMenuDropDown;
        if (dropDownMenu == null)
          return;
        dropDownMenu.RightColumnWidth = value;
      }
    }

    [Browsable(false)]
    public RibbonTabStripElement TabStripElement
    {
      get
      {
        return this.tabStrip;
      }
    }

    [Browsable(false)]
    public RibbonTab SelectedCommandTab
    {
      get
      {
        return this.tabStrip.SelectedItem as RibbonTab;
      }
    }

    public bool Expanded
    {
      get
      {
        return this.expanded;
      }
      set
      {
        if (this.CommandTabs.Count == 0)
          throw new InvalidOperationException("Expand operation could not complete. There are no command tabs or there is not a command tab selected.");
        if (value == this.expanded)
          return;
        this.expanded = value;
        this.PerformExpandedCore();
      }
    }

    protected virtual void PerformExpandedCore()
    {
      if (!this.expanded)
      {
        this.PrepareRibbonPopup();
        this.Popup.RightToLeft = this.RightToLeft ? RightToLeft.Yes : RightToLeft.No;
      }
      else
        this.RevertPopupSettings();
      this.DoExpandCollapse();
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("Expanded"));
      this.OnRibbonBarExpandedStateChanged(EventArgs.Empty);
    }

    [Browsable(false)]
    public RadQuickAccessToolBar QuickAccessToolBar
    {
      get
      {
        return this.quickAccessToolBar;
      }
    }

    [Description("Represent the Ribbon Help button!")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadImageButtonElement HelpButton
    {
      get
      {
        return this.helpButton;
      }
      set
      {
        this.helpButton = value;
      }
    }

    [Description("Represent the Ribbon Expand/Collapse button!")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadToggleButtonElement ExpandButton
    {
      get
      {
        return this.expandButton;
      }
      set
      {
        this.expandButton = value;
      }
    }

    [Browsable(false)]
    public FillPrimitive CaptionFill
    {
      get
      {
        return this.captionFill;
      }
    }

    [Browsable(false)]
    public BorderPrimitive CaptionBorder
    {
      get
      {
        return this.captionBorder;
      }
    }

    [Category("Behavior")]
    [Description("Occurs just before a command tab is selected.")]
    public event CommandTabSelectingEventHandler CommandTabSelecting;

    [Category("Behavior")]
    [Description("Occurs when a command tab is selected.")]
    public event CommandTabEventHandler CommandTabSelected;

    [Category("Behavior")]
    [Description("Occurs when a command tab is expanded by double clicking a collapsed command tab item.")]
    public event CommandTabEventHandler CommandTabExpanded;

    [Description("Occurs when a command tab is collapsed by double clicking an expanded command tab item.")]
    [Category("Behavior")]
    public event CommandTabEventHandler CommandTabCollapsed;

    [Category("Behavior")]
    [Description("Occurs when the RadRibbonBar is either expanded or collapsed. The state of the control can be acquired from the Expanded property")]
    public event EventHandler ExpandedStateChanged;

    [Description("Occurs when the RadRibbonBar's  ApplicationMenuStyle is changed")]
    [Category("Behavior")]
    public event EventHandler ApplicationMenuStyleChanged;

    private void iconPrimitive_MouseUp(object sender, MouseEventArgs e)
    {
      RadRibbonForm form = this.ElementTree.Control.FindForm() as RadRibbonForm;
      if (form == null || !form.IsHandleCreated || (form.RibbonBar != this.ElementTree.Control || form.RibbonBar.CompositionEnabled))
        return;
      TimeSpan timeSpan = DateTime.Now - this.lastIconClick;
      this.lastIconClick = DateTime.Now;
      if (timeSpan.TotalMilliseconds > (double) SystemInformation.DoubleClickTime)
      {
        int num = 0;
        Rectangle screen = this.ElementTree.Control.RectangleToScreen(this.TabStripElement.ControlBoundingRectangle);
        IntPtr lparam = new IntPtr((num | screen.Y) << 16 | screen.X & (int) ushort.MaxValue);
        Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, form.Handle), 787, IntPtr.Zero, lparam);
      }
      else
      {
        int num = 61536;
        Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, form.Handle), 274, new IntPtr(num), IntPtr.Zero);
      }
    }

    protected virtual void OnCommandTabSelecting(CommandTabSelectingEventArgs args)
    {
      if (this.CommandTabSelecting == null)
        return;
      this.CommandTabSelecting((object) this, args);
    }

    protected virtual void OnCommandTabSelected(CommandTabEventArgs args)
    {
      if (this.CommandTabSelected == null)
        return;
      this.CommandTabSelected((object) this, args);
    }

    protected virtual void OnCommandTabExpanded(CommandTabEventArgs args)
    {
      if (this.CommandTabExpanded == null)
        return;
      this.CommandTabExpanded((object) this, args);
    }

    protected virtual void OnCommandTabCollapsed(CommandTabEventArgs args)
    {
      if (this.CommandTabCollapsed == null)
        return;
      this.CommandTabCollapsed((object) this, args);
    }

    protected virtual void OnApplicationMenuStyleChanged(EventArgs args)
    {
      bool flag = this.ApplicationMenuStyle == ApplicationMenuStyle.BackstageView;
      RadRibbonForm form = this.ElementTree.Control.FindForm() as RadRibbonForm;
      if (form != null && form.RibbonBar == this.ElementTree.Control && form.FormBehavior is RadRibbonFormBehavior)
        ((RadRibbonFormBehavior) form.FormBehavior).AdjustFormIconVisibility();
      int num1 = (int) this.SetValue(RadRibbonBarElement.IsBackstageModeProperty, (object) flag);
      int num2 = (int) this.dropDownButton.SetValue(RadRibbonBarElement.IsBackstageModeProperty, (object) flag);
      int num3 = (int) this.quickAccessToolBar.SetValue(RadRibbonBarElement.IsBackstageModeProperty, (object) flag);
      InnerItem innerItem = this.quickAccessToolBar.InnerItem;
      if (innerItem != null)
      {
        int num4 = (int) innerItem.SetValue(RadRibbonBarElement.IsBackstageModeProperty, (object) flag);
      }
      if (this.ApplicationMenuStyleChanged == null)
        return;
      this.ApplicationMenuStyleChanged((object) this, args);
    }

    protected virtual void OnRibbonBarExpandedStateChanged(EventArgs args)
    {
      if (this.ExpandedStateChanged != null)
      {
        this.ExpandedStateChanged((object) this, args);
        this.lastClickedTab = (RadPageViewItem) null;
      }
      if (this.Expanded || this.ElementTree.Control == null)
        return;
      RadControl control = this.ElementTree.Control as RadControl;
      if (control == null)
        return;
      ComponentBehavior behavior = (ComponentBehavior) control.Behavior;
      behavior.ToolTip.Hide((IWin32Window) control);
      behavior.HideScreenTip();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.RightToLeftProperty)
      {
        bool newValue = (bool) e.NewValue;
        this.TabStripElement.RightToLeft = newValue;
        if (this.ribbonPopup != null)
          this.Popup.RightToLeft = newValue ? RightToLeft.Yes : RightToLeft.No;
        if (newValue)
          this.dropDownButton.DropDownMenu.HorizontalPopupAlignment = HorizontalPopupAlignment.RightToRight;
        else
          this.dropDownButton.DropDownMenu.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToLeft;
      }
      else if (e.Property == RadRibbonBarElement.QuickAccessToolbarBelowRibbonProperty)
      {
        this.quickAccessToolBar.InnerItem.ShowFillAndBorder(!this.QuickAccessToolbarBelowRibbon);
        this.InvalidateMeasure();
      }
      else if (e.Property == RadRibbonBarElement.QuickAccessMenuHeightProperty)
      {
        this.quickAccessToolBar.MinSize = new Size(0, (int) e.NewValue);
        this.quickAccessToolBar.MaxSize = new Size(0, (int) e.NewValue);
      }
      else
      {
        if (e.Property != RadElement.BoundsProperty)
          return;
        if (this.ribbonPopup != null && this.lastClickedTab != null)
        {
          this.ribbonPopup.Size = new Size((int) this.tabStrip.DesiredSize.Width, (int) Math.Ceiling((double) this.GetMaximumTabStripHeight()));
          Point screen = this.ElementTree.Control.PointToScreen(this.ControlBoundingRectangle.Location);
          screen.Offset(0, this.lastClickedTab.ControlBoundingRectangle.Bottom - this.lastClickedTab.BorderThickness.Vertical);
          this.ribbonPopup.Location = screen;
        }
        if (this.ElementTree == null || this.ElementTree.Control == null)
          return;
        RadControl control = this.ElementTree.Control as RadControl;
        if (control == null || !TelerikHelper.IsMaterialTheme(control.ThemeName))
          return;
        control.RootElement.PaintControlShadow();
      }
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      RadPageViewItem radPageViewItem1 = sender as RadPageViewItem;
      if (radPageViewItem1 == null)
        return;
      RadRibbonBar control = (RadRibbonBar) this.ElementTree.Control;
      if (args.RoutedEvent == RadElement.MouseClickedEvent && !this.Expanded)
      {
        if (object.ReferenceEquals((object) sender, (object) this.lastClickedTab))
        {
          this.ribbonPopup.ClosePopup(RadPopupCloseReason.Mouse);
          this.lastClickedTab = (RadPageViewItem) null;
        }
        else
        {
          this.ShowPopup(radPageViewItem1);
          this.lastClickedTab = radPageViewItem1;
          if (this.lastPopupTab == radPageViewItem1)
            this.tabStrip.InvalidateMeasure(true);
          this.lastPopupTab = radPageViewItem1;
        }
      }
      if (args.RoutedEvent != RadElement.MouseDoubleClickedEvent || !this.collapseRibbonOnTabDoubleClick)
        return;
      if (radPageViewItem1.IsSelected && control.Expanded)
      {
        this.Expanded = false;
        this.tabStrip.SelectedItem = (RadPageViewItem) null;
      }
      else
      {
        if (!radPageViewItem1.IsSelected || control.Expanded)
          return;
        this.Expanded = true;
        foreach (RadPageViewItem radPageViewItem2 in (IEnumerable<RadPageViewItem>) this.tabStrip.Items)
        {
          if (radPageViewItem2.Text == radPageViewItem1.Text)
          {
            this.tabStrip.SelectItem(radPageViewItem2);
            break;
          }
        }
      }
    }

    public void ShowPopup(RadPageViewItem item)
    {
      this.ribbonPopup.Size = new Size(this.Size.Width, (int) Math.Ceiling((double) this.GetMaximumTabContentHeight()));
      Point screen = this.ElementTree.Control.PointToScreen(this.ControlBoundingRectangle.Location);
      screen.Offset(0, item.ControlBoundingRectangle.Bottom - item.BorderThickness.Vertical - (int) item.BorderTopWidth);
      this.ribbonPopup.ThemeName = this.ElementTree.ThemeName;
      this.ribbonPopup.BindingContext = this.BindingContext;
      ExpandableStackLayout.InvalidateAll((RadElement) this.ribbonPopup.RootElement);
      this.ribbonPopup.RootElement.UpdateLayout();
      this.ribbonPopup.ShowPopup(new Rectangle(screen, new Size(1, 1)));
    }

    private void tabStrip_ItemSelected(object sender, RadPageViewItemSelectedEventArgs e)
    {
      if (e.SelectedItem == null)
        return;
      this.SelectTab((RibbonTab) e.SelectedItem);
    }

    private void tabStrip_ItemSelecting(object sender, RadPageViewItemSelectingEventArgs e)
    {
      CommandTabSelectingEventArgs args = new CommandTabSelectingEventArgs((RibbonTab) e.SelectedItem, (RibbonTab) e.NextItem);
      this.OnCommandTabSelecting(args);
      if (args.Cancel)
      {
        e.Cancel = true;
      }
      else
      {
        if (e.SelectedItem == null)
          return;
        this.UnselectTab((RibbonTab) this.tabStrip.SelectedItem);
      }
    }

    private void tabStrip_ItemClicked(object sender, EventArgs e)
    {
      if (!this.BackstageControl.IsShown)
        return;
      this.backstageControl.HidePopup();
    }

    private void expandButton_ToggleStateChanging(object sender, StateChangingEventArgs args)
    {
      if (this.CommandTabs.Count != 0)
        return;
      args.Cancel = true;
    }

    private void expandButton_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (this.suspendExpandButtonNotifications != 0)
        return;
      this.Expanded = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
    }

    private void dropDownButton_Click(object sender, EventArgs e)
    {
      if (this.applicationMenuStyle != ApplicationMenuStyle.BackstageView || this.ElementTree.Control.Site != null)
        return;
      if (this.backstageControl.IsShown)
        this.backstageControl.HidePopup();
      else
        this.backstageControl.ShowPopup(this);
    }

    private void dropDownButton_DropDownOpening(object sender, CancelEventArgs e)
    {
      if (this.applicationMenuStyle != ApplicationMenuStyle.BackstageView || this.ElementTree.Control.Site != null)
        return;
      e.Cancel = true;
    }

    private void ContextualTabGroups_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
          ContextualTabGroup target1 = (ContextualTabGroup) target;
          this.ReplaceTabItemWithRibbonTab(target1);
          if (target1.BaseColor == Color.Empty)
          {
            int index = this.contextualTabGroups.Count % this.contextualColors.Length;
            target1.BaseColor = this.contextualColors[index];
          }
          this.UpdateContextTabsAndQAToolbarLayout();
          break;
        case ItemsChangeOperation.Removed:
          this.UpdateContextTabsAndQAToolbarLayout();
          break;
      }
    }

    private void OfficeMenuButton_Click(object sender, EventArgs e)
    {
      this.dropDownButton.HideDropDown();
    }

    private void OnQuickAccessToolbar_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation == ItemsChangeOperation.Inserted || operation == ItemsChangeOperation.Set)
      {
        if (target is RadButtonElement)
          target.AddBehavior((PropertyChangeBehavior) new RibbonButtonBorderBehavior());
        target.NotifyParentOnMouseInput = true;
      }
      this.UpdateContextTabsAndQAToolbarLayout();
    }

    private void CommandTabs_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      this.highestTabContentPanelHeight = new float?();
      RibbonTab commandTab = (RibbonTab) target;
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
          if (commandTab.Tab != null && !this.tabStrip.Items.Contains(commandTab.Tab))
          {
            if (this.tabStrip.SelectedItem == commandTab)
              this.SelectTab(commandTab);
            if (this.removedTab == commandTab)
            {
              this.tabStrip.SelectedItem = (RadPageViewItem) commandTab;
              break;
            }
            break;
          }
          break;
        case ItemsChangeOperation.Removed:
          this.tabStrip.RemoveItem(commandTab.Tab);
          this.removedTab = commandTab;
          if (this.tabStrip.SelectedItem == commandTab.Tab)
          {
            if (this.tabStrip.Items.Count > 0)
            {
              this.tabStrip.SelectedItem = this.tabStrip.Items[0];
              break;
            }
            this.tabStrip.SelectedItem = (RadPageViewItem) null;
            break;
          }
          break;
      }
      if (this.ribbonCaption == null || this.ribbonCaption.CaptionLayout == null)
        return;
      this.tabStrip.InvalidateMeasure();
      this.tabStrip.InvalidateArrange();
      this.tabStrip.UpdateLayout();
      this.ribbonCaption.InvalidateMeasure();
      this.ribbonCaption.InvalidateArrange();
      this.ribbonCaption.CaptionLayout.InvalidateMeasure();
      this.ribbonCaption.CaptionLayout.InvalidateArrange();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.parentForm == null && this.IsInValidState(false))
        this.parentForm = this.ElementTree.Control.FindForm();
      if (this.parentForm != null && this.parentForm.WindowState == FormWindowState.Minimized)
        return SizeF.Empty;
      if (this.parentForm != null && this.parentForm.WindowState == FormWindowState.Maximized)
        this.tabStrip.InvalidateMeasure(true);
      SizeF empty = SizeF.Empty;
      availableSize = this.GetClientRectangle(availableSize).Size;
      SizeF availableSize1 = availableSize;
      SizeF sizeForQaToolbar = this.GetSizeForQAToolbar(availableSize);
      this.iconPrimitive.Measure(sizeForQaToolbar);
      this.quickAccessToolBar.Measure(new SizeF(sizeForQaToolbar.Width - this.iconPrimitive.DesiredSize.Width, sizeForQaToolbar.Height));
      availableSize1.Width -= this.iconPrimitive.DesiredSize.Width;
      if (!this.QuickAccessToolbarBelowRibbon)
      {
        availableSize1.Width -= this.quickAccessToolBar.DesiredSize.Width;
        this.ribbonCaption.Measure(availableSize1);
        availableSize1.Width = availableSize.Width;
        empty.Height = Math.Max(this.quickAccessToolBar.DesiredSize.Height, this.ribbonCaption.DesiredSize.Height);
        empty.Width = Math.Max(this.quickAccessToolBar.DesiredSize.Width, this.ribbonCaption.DesiredSize.Width);
        availableSize1.Height -= empty.Height;
      }
      else
      {
        this.ribbonCaption.Measure(availableSize1);
        empty.Height += this.quickAccessToolBar.DesiredSize.Height + this.ribbonCaption.DesiredSize.Height;
        availableSize1.Height -= this.quickAccessToolBar.DesiredSize.Height + this.ribbonCaption.DesiredSize.Height;
      }
      this.tabStrip.Measure(availableSize1);
      if (this.expanded)
        empty.Height += this.GetMaximumTabStripHeight();
      else
        empty.Height += this.tabStrip.ItemContainer.DesiredSize.Height + (float) this.tabStrip.ItemContainer.Margin.Vertical + this.tabStrip.BorderTopWidth + this.tabStrip.BorderBottomWidth;
      empty.Width = Math.Max(empty.Width, this.tabStrip.DesiredSize.Width);
      this.captionFill.Measure(availableSize);
      this.dropDownButton.Measure(availableSize);
      this.buttonsLayout.Measure(availableSize);
      Padding borderThickness = this.GetBorderThickness(true);
      empty.Width += (float) borderThickness.Horizontal;
      empty.Height += (float) borderThickness.Vertical;
      empty.Width = Math.Min(empty.Width, availableSize.Width);
      empty.Height = Math.Min(empty.Height, availableSize.Height);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float y1 = clientRectangle.Y;
      float y2 = clientRectangle.Y;
      float y3;
      if (this.QuickAccessToolbarBelowRibbon)
      {
        y1 = clientRectangle.Bottom - this.quickAccessToolBar.DesiredSize.Height;
        y3 = y2 + this.ribbonCaption.DesiredSize.Height;
        this.ribbonCaption.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, this.ribbonCaption.DesiredSize.Height));
        this.captionFill.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, this.ribbonCaption.DesiredSize.Height));
      }
      else
      {
        y3 = y2 + Math.Max(this.quickAccessToolBar.DesiredSize.Height + (float) this.quickAccessToolBar.Margin.Vertical, this.ribbonCaption.DesiredSize.Height + (float) this.ribbonCaption.Margin.Vertical);
        this.ribbonCaption.Arrange(new RectangleF(clientRectangle.X + (this.RightToLeft ? 0.0f : this.quickAccessToolBar.DesiredSize.Width + this.iconPrimitive.DesiredSize.Width), clientRectangle.Y, this.ribbonCaption.DesiredSize.Width, Math.Max(this.quickAccessToolBar.DesiredSize.Height, this.ribbonCaption.DesiredSize.Height)));
        this.captionFill.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, Math.Max(this.quickAccessToolBar.DesiredSize.Height, this.ribbonCaption.DesiredSize.Height)));
      }
      float height = this.GetMaximumTabStripHeight();
      if (!this.expanded)
        height = this.tabStrip.ItemContainer.DesiredSize.Height + (float) this.tabStrip.ItemContainer.Margin.Vertical;
      this.tabStrip.Arrange(new RectangleF(clientRectangle.X, y3, clientRectangle.Width, height));
      float y4 = y3;
      if ((double) y4 + (double) this.dropDownButton.Margin.Top < 0.0)
        y4 = (float) -this.dropDownButton.Margin.Top;
      if (!this.RightToLeft)
      {
        this.dropDownButton.Arrange(new RectangleF(clientRectangle.X, y4, this.dropDownButton.DesiredSize.Width, this.dropDownButton.DesiredSize.Height));
        this.iconPrimitive.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, this.iconPrimitive.DesiredSize.Width, this.ribbonCaption.DesiredSize.Height));
        this.quickAccessToolBar.Arrange(new RectangleF(clientRectangle.X + (this.QuickAccessToolbarBelowRibbon ? 0.0f : this.iconPrimitive.DesiredSize.Width), y1, this.quickAccessToolBar.DesiredSize.Width, this.QuickAccessToolbarBelowRibbon ? this.quickAccessToolBar.DesiredSize.Height : Math.Max(this.quickAccessToolBar.DesiredSize.Height, this.ribbonCaption.DesiredSize.Height)));
        this.buttonsLayout.Arrange(new RectangleF(clientRectangle.Right - this.buttonsLayout.DesiredSize.Width, y3, this.buttonsLayout.DesiredSize.Width, this.buttonsLayout.DesiredSize.Height));
      }
      else
      {
        this.dropDownButton.Arrange(new RectangleF(clientRectangle.Right - this.dropDownButton.DesiredSize.Width, y4, this.dropDownButton.DesiredSize.Width, this.dropDownButton.DesiredSize.Height));
        this.iconPrimitive.Arrange(new RectangleF(clientRectangle.Right - this.iconPrimitive.DesiredSize.Width, clientRectangle.Y, this.iconPrimitive.DesiredSize.Width, this.ribbonCaption.DesiredSize.Height));
        this.quickAccessToolBar.Arrange(new RectangleF(clientRectangle.Right - (this.QuickAccessToolbarBelowRibbon ? 0.0f : this.iconPrimitive.DesiredSize.Width) - this.quickAccessToolBar.DesiredSize.Width, y1, this.quickAccessToolBar.DesiredSize.Width, this.QuickAccessToolbarBelowRibbon ? this.quickAccessToolBar.DesiredSize.Height : Math.Max(this.quickAccessToolBar.DesiredSize.Height, this.ribbonCaption.DesiredSize.Height)));
        this.buttonsLayout.Arrange(new RectangleF(clientRectangle.X, y3, this.buttonsLayout.DesiredSize.Width, this.buttonsLayout.DesiredSize.Height));
      }
      return finalSize;
    }

    private SizeF GetSizeForQAToolbar(SizeF availableSize)
    {
      SizeF sizeF;
      if (!this.QuickAccessToolbarBelowRibbon)
      {
        float width = availableSize.Width - (this.ribbonCaption.SystemButtons.DesiredSize.Width + this.ApplicationButtonElement.DesiredSize.Width + (float) this.quickAccessToolBar.Margin.Horizontal);
        if (!this.IsDesignMode)
        {
          ContextualTabGroup leftMostGroup = this.ribbonCaption.CaptionLayout.GetLeftMostGroup(false);
          if (leftMostGroup != null)
            width = this.RightToLeft ? availableSize.Width - (float) leftMostGroup.TabItems[0].ControlBoundingRectangle.Right : (float) leftMostGroup.TabItems[0].ControlBoundingRectangle.X;
        }
        sizeF = new SizeF(width, availableSize.Height);
      }
      else
        sizeF = new SizeF(availableSize);
      return sizeF;
    }

    private void UpdateContextTabsAndQAToolbarLayout()
    {
      this.InvalidateMeasure(true);
      this.UpdateLayout();
      if (this.ribbonCaption == null || this.ribbonCaption.CaptionLayout == null)
        return;
      this.RibbonCaption.CaptionLayout.InvalidateMeasure(true);
      this.RibbonCaption.CaptionLayout.UpdateLayout();
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      if (this.ApplicationMenuStyle == ApplicationMenuStyle.BackstageView && this.BackstageControl.IsShown && (this.BackstageControl.Parent != null && this.ElementTree.Control.Site == null))
        this.timer.Start();
      this.highestTabContentPanelHeight = new float?();
      this.InvalidateMeasure(true);
      this.InvalidateArrange(true);
      this.UpdateLayout();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.timer.Stop();
      this.BackstageControl.parentControl_SizeChanged((object) this.BackstageControl.Parent, EventArgs.Empty);
    }

    protected override void PaintOverride(
      IGraphics screenRadGraphics,
      Rectangle clipRectangle,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      if (this.parentForm == null)
        this.parentForm = this.ElementTree.Control.FindForm();
      base.PaintOverride(screenRadGraphics, clipRectangle, angle, scale, useRelativeTransformation);
      RadRibbonBar control = this.ElementTree.Control as RadRibbonBar;
      RadFormControlBase parentForm = this.parentForm as RadFormControlBase;
      if (control == null || !control.CompositionEnabled || (this.IsDesignMode || parentForm == null) || !(parentForm.FormBehavior is RadRibbonFormBehavior))
        return;
      this.ribbonCaption.DrawFill = false;
      this.captionFill.Visibility = ElementVisibility.Hidden;
      Color color = this.parentForm.WindowState != FormWindowState.Maximized || this.IsWindowsSeven ? Color.Black : Color.White;
      Rectangle boundingRectangle = this.ribbonCaption.CaptionLayout.CaptionTextElement.ControlBoundingRectangle;
      TelerikPaintHelper.DrawGlowingText((Graphics) screenRadGraphics.UnderlayGraphics, this.Text, (this.ribbonCaption.CaptionLayout.CaptionTextElement as TextPrimitive).Font, boundingRectangle, color, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
    }

    private void PrepareRibbonPopup()
    {
      if (this.ribbonPopup == null)
      {
        this.ribbonPopup = new RibbonBarPopup(this);
        this.ribbonPopup.RootElement.RightToLeft = this.ElementTree.RootElement.RightToLeft;
        this.ribbonPopup.ElementTree.EnableApplicationThemeName = this.ElementTree.EnableApplicationThemeName;
        this.chunksHolder = (RadElement) this.tabStrip.ContentArea;
        this.ribbonPopup.PopupClosed += new RadPopupClosedEventHandler(this.RadRibbonBar_PopupClosed);
      }
      if (!this.tabStrip.Children.Contains(this.chunksHolder))
        return;
      this.tabStrip.Children.Remove(this.chunksHolder);
      this.ribbonPopup.RootElement.Children.Add(this.chunksHolder);
      if (this.ElementTree != null)
      {
        this.ribbonPopup.ImageList = this.ElementTree.ComponentTreeHandler.ImageList;
        this.ribbonPopup.SmallImageList = this.ElementTree.ComponentTreeHandler.SmallImageList;
      }
      this.InvalidateMeasure();
    }

    private void RadRibbonBar_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      if (!this.IsInValidState(true))
        return;
      if (!(this.ElementTree.Control as RadRibbonBar).Expanded)
      {
        this.tabStrip.SelectedItem = (RadPageViewItem) null;
        this.lastClickedTab = (RadPageViewItem) null;
      }
      this.TabStripElement.InvalidateMeasure(true);
    }

    private void RevertPopupSettings()
    {
      this.ribbonPopup.ClosePopup(RadPopupCloseReason.CloseCalled);
      this.ribbonPopup.RootElement.Children.Remove(this.chunksHolder);
      this.tabStrip.Children.Add(this.chunksHolder);
      this.InvalidateMeasure();
    }

    [Browsable(false)]
    public RibbonBarPopup Popup
    {
      get
      {
        return this.ribbonPopup;
      }
    }

    protected override void UpdateReferences(
      ComponentThemableElementTree tree,
      bool updateInheritance,
      bool recursive)
    {
      base.UpdateReferences(tree, updateInheritance, recursive);
      if (this.containerControl == null)
      {
        this.containerControl = this.ElementTree.Control;
      }
      else
      {
        this.containerControl.ClientSizeChanged -= new EventHandler(this.Control_SizeChanged);
        this.containerControl.VisibleChanged -= new EventHandler(this.Control_VisibleChanged);
      }
      this.ElementTree.Control.ClientSizeChanged += new EventHandler(this.Control_SizeChanged);
      this.ElementTree.Control.VisibleChanged += new EventHandler(this.Control_VisibleChanged);
      this.containerControl = this.ElementTree.Control;
    }

    protected internal void Control_VisibleChanged(object sender, EventArgs e)
    {
      this.VisibleChangedCore();
    }

    internal void VisibleChangedCore()
    {
      if (this.IsDisposed || this.ElementTree != null && (this.ElementTree == null || this.ElementTree.Control.Disposing || this.ElementTree.Control.IsDisposed))
        return;
      this.tabStrip.InvalidateMeasure(true);
      this.tabStrip.InvalidateArrange(true);
      this.tabStrip.UpdateLayout();
      this.ribbonCaption.CaptionLayout.InvalidateMeasure();
      this.ribbonCaption.CaptionLayout.InvalidateArrange();
      this.InvalidateMeasure(true);
      this.InvalidateArrange(true);
      this.ribbonCaption.CaptionLayout.UpdateLayout();
      this.UpdateLayout();
    }

    private void Control_SizeChanged(object sender, EventArgs e)
    {
      if (this.IsDisposed || this.ElementTree != null && (this.ElementTree == null || this.ElementTree.Control.Disposing || this.ElementTree.Control.IsDisposed))
        return;
      this.ribbonCaption.CaptionLayout.InvalidateMeasure();
      this.ribbonCaption.CaptionLayout.InvalidateArrange();
      this.InvalidateMeasure();
      this.InvalidateArrange();
      this.ribbonCaption.CaptionLayout.UpdateLayout();
      this.UpdateLayout();
    }

    protected virtual float GetMaximumRibbonGroupMargin(ExpandableStackLayout groupsHolder)
    {
      int num = 0;
      foreach (RadRibbonBarGroup child in groupsHolder.Children)
      {
        if (child.Margin.Vertical > num)
          num = child.Margin.Vertical;
      }
      return (float) num;
    }

    protected virtual float GetMaximumTabContentHeight()
    {
      float num1 = 90f;
      foreach (RibbonTab commandTab in (RadItemCollection) this.CommandTabs)
      {
        if (commandTab.ContentLayout != null)
        {
          SizeF desiredSize = MeasurementControl.ThreadInstance.GetDesiredSize((RadElement) commandTab.ContentLayout, new SizeF(float.MaxValue, float.MaxValue));
          float num2 = (float) (LightVisualElement.GetBorderThickness((LightVisualElement) this.tabStrip.ContentArea, true).Vertical + this.tabStrip.ContentArea.Padding.Vertical) + this.GetMaximumRibbonGroupMargin(commandTab.ContentLayout) + desiredSize.Height;
          if ((double) num2 > (double) num1)
            num1 = num2;
        }
      }
      return num1;
    }

    protected virtual float GetMaximumTabStripHeight()
    {
      float num = this.tabStrip.ItemContainer.DesiredSize.Height + (float) this.tabStrip.ItemContainer.Margin.Vertical;
      if (!this.expanded)
        return num;
      if (this.highestTabContentPanelHeight.HasValue)
      {
        if ((double) this.tabStrip.DesiredSize.Height > (double) (this.highestTabContentPanelHeight.Value + num))
          this.highestTabContentPanelHeight = new float?(this.tabStrip.DesiredSize.Height - num);
        return this.highestTabContentPanelHeight.Value + num;
      }
      this.highestTabContentPanelHeight = new float?(this.GetMaximumTabContentHeight());
      return this.highestTabContentPanelHeight.Value + num;
    }

    protected virtual void DoExpandCollapse()
    {
      ++this.suspendExpandButtonNotifications;
      this.ribbonPopup.ElementTree.StyleVersion = this.styleVersion;
      if (this.expanded)
      {
        this.tabStrip.SelectedItem = (RadPageViewItem) this.tabOldSelected;
        RibbonTab selectedItem = this.tabStrip.SelectedItem as RibbonTab;
        if (selectedItem != null)
          selectedItem.Items.Owner.Visibility = ElementVisibility.Visible;
        this.expandButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      }
      else
      {
        (this.tabStrip.SelectedItem as RibbonTab).Items.Owner.Visibility = ElementVisibility.Collapsed;
        this.expandButton.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
        this.tabStrip.SelectedItem = (RadPageViewItem) null;
      }
      --this.suspendExpandButtonNotifications;
    }

    internal void CallOnCommandTabCollapsed(CommandTabEventArgs args)
    {
      this.OnCommandTabCollapsed(args);
    }

    internal void CallOnCommandTabExpanded(CommandTabEventArgs args)
    {
      this.OnCommandTabExpanded(args);
    }

    private void ReplaceTabItemWithRibbonTab(ContextualTabGroup target)
    {
      int count1 = this.TabStripElement.Items.Count;
      int count2 = target.TabItems.Count;
      for (int index1 = 0; index1 < count1; ++index1)
      {
        RibbonTab ribbonTab = (RibbonTab) this.TabStripElement.Items[index1];
        for (int index2 = 0; index2 < count2; ++index2)
        {
          RadPageViewItem tabItem = (RadPageViewItem) target.TabItems[index2];
          if (ribbonTab.obsoleteTab == tabItem)
            ;
        }
      }
    }

    private void UnselectTab(RibbonTab commandTab)
    {
      if (commandTab == null)
        return;
      commandTab.Items.Owner.Visibility = ElementVisibility.Collapsed;
    }

    private void SelectTab(RibbonTab commandTab)
    {
      if (commandTab == null || commandTab.Visibility != ElementVisibility.Visible)
        return;
      this.UnselectTab(this.tabOldSelected);
      if (this.BackstageControl.IsShown)
        this.backstageControl.HidePopup();
      this.tabOldSelected = (RibbonTab) this.TabStripElement.SelectedItem;
      commandTab.InvalidateMeasure();
      commandTab.Items.Owner.Visibility = ElementVisibility.Visible;
      commandTab.InvalidateMeasure();
      this.ChangeTabBaseFillStyle(this.FindContextualTabGroup(commandTab), (LightVisualElement) this.tabStrip.ContentArea, (LightVisualElement) commandTab);
      this.previousItem = (RadPageViewItem) commandTab;
      this.OnCommandTabSelected(new CommandTabEventArgs(commandTab));
    }

    protected virtual void ChangeTabBaseFillStyle(
      ContextualTabGroup group,
      LightVisualElement contentArea,
      LightVisualElement tabFill)
    {
      if (group != null)
      {
        this.contentAreaColorsAltered = true;
        contentArea.SetThemeValueOverride(LightVisualElement.DrawFillProperty, (object) true, "");
        contentArea.SetThemeValueOverride(VisualElement.BackColorProperty, (object) Color.FromArgb(200, group.BaseColor), "");
        tabFill.SetThemeValueOverride(LightVisualElement.DrawFillProperty, (object) true, "Selected");
        tabFill.SetThemeValueOverride(VisualElement.BackColorProperty, (object) Color.FromArgb(200, group.BaseColor), "Selected");
        tabFill.SetThemeValueOverride(LightVisualElement.BackColor2Property, (object) Color.FromArgb(100, group.BaseColor), "Selected");
        tabFill.SetThemeValueOverride(LightVisualElement.BackColor3Property, (object) Color.FromArgb(50, group.BaseColor), "Selected");
        tabFill.SetThemeValueOverride(LightVisualElement.BackColor4Property, (object) Color.FromArgb(100, group.BaseColor), "Selected");
      }
      else if (this.contentAreaColorsAltered)
      {
        contentArea.ResetThemeValueOverride(LightVisualElement.DrawFillProperty, "");
        contentArea.ResetThemeValueOverride(VisualElement.BackColorProperty, "");
        this.contentAreaColorsAltered = false;
      }
      if (this.previousItem == null || tabFill == this.previousItem)
        return;
      this.previousItem.ResetThemeValueOverride(LightVisualElement.DrawFillProperty, "Selected");
      this.previousItem.ResetThemeValueOverride(VisualElement.BackColorProperty, "Selected");
      this.previousItem.ResetThemeValueOverride(LightVisualElement.BackColor2Property, "Selected");
      this.previousItem.ResetThemeValueOverride(LightVisualElement.BackColor3Property, "Selected");
      this.previousItem.ResetThemeValueOverride(LightVisualElement.BackColor4Property, "Selected");
    }

    private ContextualTabGroup FindContextualTabGroup(RibbonTab commandTab)
    {
      ContextualTabGroup contextualTabGroup1 = (ContextualTabGroup) null;
      foreach (ContextualTabGroup contextualTabGroup2 in (RadItemCollection) this.contextualTabGroups)
      {
        if (contextualTabGroup2.TabItems.Contains((RadItem) commandTab.Tab))
        {
          contextualTabGroup1 = contextualTabGroup2;
          break;
        }
      }
      return contextualTabGroup1;
    }

    internal void SetParentForm()
    {
      this.MDIbutton.LayoutPropertyChanged();
    }

    private class BackColorBehavior : PropertyChangeBehavior
    {
      private RadRibbonBarElement owner;

      public BackColorBehavior(RadRibbonBarElement owner)
        : base(VisualElement.BackColorProperty)
      {
        this.owner = owner;
      }

      public override void OnPropertyChange(RadElement element, RadPropertyChangedEventArgs e)
      {
        base.OnPropertyChange(element, e);
        int num = (int) this.owner.tabStrip.ContentArea.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) this.owner.BackColor);
      }
    }
  }
}
