// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Elements;
using Telerik.WinControls.Layout;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadGalleryElementDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadGalleryElement : RadItem, IDropDownMenuOwner, IItemsOwner
  {
    private readonly Size buttonsPanelMaxSize = new Size(0, 54);
    private readonly Size arrowButtonsMinSize = new Size(15, 20);
    private RadCanvasViewport popupGroupsViewport = new RadCanvasViewport();
    private SizingMode dropDownSizingMode = SizingMode.UpDownAndRightBottom;
    public static RadProperty IsSelectedProperty = RadProperty.Register("IsSelected", typeof (bool), typeof (RadGalleryElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty MaxRowsProperty = RadProperty.Register(nameof (MaxRows), typeof (int), typeof (RadGalleryElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsParentMeasure));
    public static RadProperty MaxColumnsProperty = RadProperty.Register(nameof (MaxColumns), typeof (int), typeof (RadGalleryElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty MaxDropDownColumnsProperty = RadProperty.Register(nameof (MaxDropDownColumns), typeof (int), typeof (RadGalleryElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty MinDropDownColumnsProperty = RadProperty.Register(nameof (MinDropDownColumns), typeof (int), typeof (RadGalleryElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private static readonly object GalleryItemHoverEventKey = new object();
    private static readonly object DropDownOpeningEventKey = new object();
    private static readonly object DropDownOpenedEventKey = new object();
    private static readonly object DropDownClosingEventKey = new object();
    private static readonly object DropDownClosedEventKey = new object();
    internal const long FilterEnabledStateKey = 8796093022208;
    internal const long ItemSelectionStateKey = 17592186044416;
    internal const long ZoomItemsStateKey = 35184372088832;
    internal const long DropDownInheritThemeClassNameStateKey = 70368744177664;
    private const int defaultMenuHeight = 21;
    private const int minOffset = 13;
    private RadItemOwnerCollection items;
    private RadItemOwnerCollection groups;
    private RadItemOwnerCollection filters;
    private RadItemOwnerCollection tools;
    private IntegralScrollWrapPanel inribbonItemsLayoutPanel;
    private int currentLine;
    private RadImageButtonElement upButton;
    private RadImageButtonElement downButton;
    private RadImageButtonElement popupButton;
    private BorderPrimitive inribbonPanelBorder;
    private FillPrimitive inribbonFillPrimitive;
    private DropDownEditorLayoutPanel inribbonPanel;
    private RadGalleryButtonsLayoutPanel buttonsPanel;
    private StackLayoutPanel dropDownPanel;
    private RadScrollViewer popupScrollViewer;
    private RadGalleryMenuLayoutPanel subMenuLayoutPanel;
    private RadDropDownButtonElement filterDropDown;
    private RadGalleryItem selectedItem;
    private GalleryMouseOverBehavior zoomBehavior;
    private RadGalleryItem lastClickedItem;
    private RadGalleryPopupElement galleryPopupElement;
    private RadGalleryDropDown downMenu;

    static RadGalleryElement()
    {
      RadElement.CanFocusProperty.OverrideMetadata(typeof (RadGalleryElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BitState[8796093022208L] = true;
      this.tools = new RadItemOwnerCollection();
      this.tools.ItemTypes = new System.Type[3]
      {
        typeof (RadMenuItem),
        typeof (RadMenuButtonItem),
        typeof (RadMenuComboItem)
      };
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[1]
      {
        typeof (RadGalleryItem)
      };
      this.items.ItemsChanged += new ItemChangedDelegate(this.items_ItemsChanged);
      this.groups = new RadItemOwnerCollection();
      this.groups.ItemTypes = new System.Type[1]
      {
        typeof (RadGalleryGroupItem)
      };
      this.filters = new RadItemOwnerCollection();
      this.filters.ItemTypes = new System.Type[1]
      {
        typeof (RadGalleryGroupFilter)
      };
      this.ClipDrawing = true;
      this.zoomBehavior = new GalleryMouseOverBehavior(this);
      this.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
    }

    private void WireMenuDropDownEvents()
    {
      this.downMenu.MouseWheel += new MouseEventHandler(this.menu_MouseWheel);
      this.downMenu.DropDownOpening += new CancelEventHandler(this.menu_DropDownOpening);
      this.downMenu.DropDownOpened += new EventHandler(this.menu_DropDownOpened);
      this.downMenu.DropDownClosing += new RadPopupClosingEventHandler(this.menu_DropDownClosing);
      this.downMenu.DropDownClosed += new RadPopupClosedEventHandler(this.menu_DropDownClosed);
    }

    private void UnWireMenuDropDownEvents()
    {
      this.downMenu.MouseWheel -= new MouseEventHandler(this.menu_MouseWheel);
      this.downMenu.DropDownOpening -= new CancelEventHandler(this.menu_DropDownOpening);
      this.downMenu.DropDownOpened -= new EventHandler(this.menu_DropDownOpened);
      this.downMenu.DropDownClosing -= new RadPopupClosingEventHandler(this.menu_DropDownClosing);
      this.downMenu.DropDownClosed -= new RadPopupClosedEventHandler(this.menu_DropDownClosed);
    }

    protected override void DisposeManagedResources()
    {
      this.UnWireMenuDropDownEvents();
      base.DisposeManagedResources();
    }

    private void menu_MouseWheel(object sender, MouseEventArgs e)
    {
      if (e.Delta > 0)
        this.DoScrollLineUp();
      else if (e.Delta < 0)
        this.DoScrollLineDown();
      if (!(e is HandledMouseEventArgs) || this.downMenu == null || !this.downMenu.IsVisible)
        return;
      ((HandledMouseEventArgs) e).Handled = true;
    }

    [Browsable(false)]
    public RadGalleryDropDown GalleryDropDown
    {
      get
      {
        return this.downMenu;
      }
    }

    [Browsable(false)]
    public RadGalleryPopupElement GalleryPopupElement
    {
      get
      {
        return this.galleryPopupElement;
      }
    }

    [Browsable(false)]
    public FillPrimitive InRibbonFill
    {
      get
      {
        return this.inribbonFillPrimitive;
      }
    }

    [Browsable(false)]
    public BorderPrimitive InRibbonBorder
    {
      get
      {
        return this.inribbonPanelBorder;
      }
    }

    [Browsable(false)]
    public RadImageButtonElement UpButton
    {
      get
      {
        return this.upButton;
      }
    }

    [Browsable(false)]
    public RadImageButtonElement DownButton
    {
      get
      {
        return this.downButton;
      }
    }

    [Browsable(false)]
    public RadImageButtonElement PopupButton
    {
      get
      {
        return this.popupButton;
      }
    }

    [Browsable(true)]
    [DefaultValue(SizingMode.UpDownAndRightBottom)]
    [Category("Appearance")]
    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, veritcal or a combination of them.")]
    public SizingMode DropDownSizingMode
    {
      get
      {
        return this.dropDownSizingMode;
      }
      set
      {
        this.dropDownSizingMode = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether group filtering is enbled when filters are defined.")]
    public bool FilterEnabled
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

    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets a collection representing the group filters defined in this gallery.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Filters
    {
      get
      {
        return this.filters;
      }
    }

    [Category("Data")]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection representing the groups contained in this gallery.")]
    public RadItemOwnerCollection Groups
    {
      get
      {
        return this.groups;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Returns whether the gallery is currently dropped down.")]
    public bool IsDroppedDown
    {
      get
      {
        if (this.downMenu != null)
          return this.downMenu.IsVisible;
        return false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection representing the items contained in this gallery.")]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Data")]
    [RadNewItem("", false)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the selection of the gallery items is enabled or not.")]
    public bool ItemSelection
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

    [Description("Gets or sets the maximum number of columns to be shown in the in-ribbon part of the gallery.")]
    [Category("Behavior")]
    [RadPropertyDefaultValue("MaxColumns", typeof (RadGalleryElement))]
    public int MaxColumns
    {
      get
      {
        return (int) this.GetValue(RadGalleryElement.MaxColumnsProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGalleryElement.MaxColumnsProperty, (object) value);
        this.inribbonItemsLayoutPanel.MaxColumns = value;
      }
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("MaxDropDownColumns", typeof (RadGalleryElement))]
    [Description("Gets or sets the maximum number of columns to be shown in the drop-down portion of the gallery. ")]
    public int MaxDropDownColumns
    {
      get
      {
        return (int) this.GetValue(RadGalleryElement.MaxDropDownColumnsProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGalleryElement.MaxDropDownColumnsProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("MaxRows", typeof (RadGalleryElement))]
    [Category("Behavior")]
    [Description("Gets or sets the maximum number of rows to be shown in the in-ribbon part of the gallery.")]
    public int MaxRows
    {
      get
      {
        return (int) this.GetValue(RadGalleryElement.MaxRowsProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGalleryElement.MaxRowsProperty, (object) value);
        this.inribbonItemsLayoutPanel.MaxRows = value;
      }
    }

    [RadPropertyDefaultValue("MaxDropDownColumns", typeof (RadGalleryElement))]
    [Category("Behavior")]
    [Description("Gets or sets the minimum number of columns to be shown in the drop-down portion of the gallery. ")]
    public int MinDropDownColumns
    {
      get
      {
        return (int) this.GetValue(RadGalleryElement.MinDropDownColumnsProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGalleryElement.MinDropDownColumnsProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the currently selected item.")]
    [Browsable(false)]
    public RadGalleryItem SelectedItem
    {
      get
      {
        return this.selectedItem;
      }
      set
      {
        if (!this.ItemSelection)
          return;
        if (this.items.Contains((RadItem) value))
        {
          if (this.selectedItem != null)
            this.selectedItem.IsSelected = false;
          this.selectedItem = value;
          this.selectedItem.IsSelected = true;
        }
        if (value != null)
          return;
        if (this.selectedItem != null)
          this.selectedItem.IsSelected = false;
        this.selectedItem = value;
      }
    }

    [Category("Data")]
    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Tools
    {
      get
      {
        return this.tools;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether a gallery item is zoomed-in when mouse over it.")]
    [DefaultValue(false)]
    public bool ZoomItems
    {
      get
      {
        return this.GetBitState(35184372088832L);
      }
      set
      {
        foreach (RadElement radElement in (RadItemCollection) this.items)
        {
          if (value)
            radElement.AddBehavior((PropertyChangeBehavior) this.zoomBehavior);
          else
            radElement.RemoveBehaviors((PropertyChangeBehavior) this.zoomBehavior);
        }
        this.SetBitState(35184372088832L, value);
      }
    }

    private void menu_DropDownOpening(object sender, CancelEventArgs e)
    {
      RadPopupOpeningEventArgs openingEventArgs = e as RadPopupOpeningEventArgs;
      if (openingEventArgs != null)
        openingEventArgs.CustomLocation = this.PointToScreen(this.Location);
      this.OnDropDownOpening(e);
    }

    private void menu_DropDownOpened(object sender, EventArgs e)
    {
      this.OnDropDownOpened(e);
    }

    private void menu_DropDownClosing(object sender, RadPopupClosingEventArgs e)
    {
      this.OnDropDownClosing(e);
    }

    private void menu_DropDownClosed(object sender, RadPopupClosedEventArgs e)
    {
      this.OnDropDownClosed(e);
      if (this.Filters.Count > 0 && this.galleryPopupElement != null)
        this.galleryPopupElement.SelectedFilter = (RadGalleryGroupFilter) this.Filters[0];
      this.MinSize = Size.Empty;
      this.HideZoomPopups();
      this.ResetGalleryItemsAndGroups();
      this.ScrollToSelectedItem();
      this.AdjustGalleryAfterPopUpClose();
      if (!this.IsInValidState(true))
        return;
      this.ElementTree.Control.Invalidate();
    }

    protected override void OnBitStateChanged(long key, bool oldValue, bool newValue)
    {
      base.OnBitStateChanged(key, oldValue, newValue);
      if (key != 17592186044416L || newValue || this.selectedItem == null)
        return;
      this.selectedItem.IsSelected = false;
    }

    protected override void CreateChildElements()
    {
      this.filterDropDown = new RadDropDownButtonElement();
      this.filterDropDown.Alignment = ContentAlignment.TopLeft;
      this.filterDropDown.ExpandArrowButton = true;
      this.filterDropDown.ArrowButton.Arrow.Alignment = ContentAlignment.MiddleLeft;
      this.filterDropDown.Class = "RadGalleryPopupFilterDropDownButton";
      this.filterDropDown.ActionButton.ButtonFillElement.Class = "RadGalleryPopupFilterActionButtonFill";
      this.filterDropDown.ActionButton.BorderElement.Class = "RadGalleryPopupFilterActionButtonBorder";
      this.filterDropDown.ActionButton.Class = "RadGalleryPopupFilterActionButton";
      this.filterDropDown.ArrowButton.Fill.Class = "RadGalleryPopupFilterArrowButtonFill";
      this.filterDropDown.ArrowButton.Border.Class = "RadGalleryPopupFilterArrowButtonBorder";
      this.filterDropDown.ArrowButton.Arrow.Class = "RadGalleryPopupFilterArrowButtonArrow";
      this.filterDropDown.BorderElement.Class = "RadGalleryPopupFilterDropDownButtonBorder";
      this.popupGroupsViewport = new RadCanvasViewport();
      this.popupScrollViewer = new RadScrollViewer((RadElement) this.popupGroupsViewport);
      this.popupScrollViewer.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.popupScrollViewer.HorizontalScrollState = ScrollState.AlwaysHide;
      this.popupScrollViewer.UsePhysicalScrolling = true;
      this.popupScrollViewer.Class = "RadGalleryPopupScrollViewer";
      this.popupScrollViewer.FillElement.Class = "RadGalleryPopupScrollViewerFill";
      this.subMenuLayoutPanel = new RadGalleryMenuLayoutPanel();
      this.subMenuLayoutPanel.Alignment = ContentAlignment.BottomLeft;
      this.subMenuLayoutPanel.Class = "RadGalleryPoupMenuPanel";
      this.dropDownPanel = new StackLayoutPanel();
      this.dropDownPanel.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.dropDownPanel.Orientation = Orientation.Vertical;
      this.dropDownPanel.EqualChildrenWidth = true;
      this.downMenu = new RadGalleryDropDown(this);
      this.WireMenuDropDownEvents();
      this.downMenu.VerticalPopupAlignment = VerticalPopupAlignment.TopToTop;
      this.downMenu.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToLeft;
      this.downMenu.AnimationEnabled = false;
      this.galleryPopupElement = new RadGalleryPopupElement(this.Items, this.Groups, this.Filters, this.Tools);
      this.downMenu.PopupElement = (RadElement) this.galleryPopupElement;
      this.downMenu.LoadElementTree();
      this.dropDownPanel.Children.Add((RadElement) this.filterDropDown);
      this.dropDownPanel.Children.Add((RadElement) this.popupScrollViewer);
      this.dropDownPanel.Children.Add((RadElement) this.subMenuLayoutPanel);
      this.inribbonItemsLayoutPanel = new IntegralScrollWrapPanel();
      this.inribbonItemsLayoutPanel.MaxColumns = this.MaxColumns;
      this.inribbonItemsLayoutPanel.MaxRows = this.MaxRows;
      this.Items.Owner = (RadElement) this.inribbonItemsLayoutPanel;
      this.upButton = new RadImageButtonElement();
      this.upButton.Class = "GalleryUpButton";
      this.upButton.Enabled = false;
      this.upButton.MinSize = this.arrowButtonsMinSize;
      this.upButton.Click += new EventHandler(this.upButton_Click);
      this.upButton.BorderElement.Class = "GalleryUpButtonBorder";
      this.upButton.ButtonFillElement.Class = "GalleryArrowButtonFill";
      this.downButton = new RadImageButtonElement();
      this.downButton.Class = "GalleryDownButton";
      this.downButton.MinSize = this.arrowButtonsMinSize;
      this.downButton.Click += new EventHandler(this.downButton_Click);
      this.downButton.BorderElement.Class = "GalleryDownButtonBorder";
      this.downButton.ButtonFillElement.Class = "GalleryArrowButtonFill";
      this.popupButton = (RadImageButtonElement) new RadGalleryPopupButton(this);
      this.popupButton.Class = "GalleryPopupButtonButton";
      this.popupButton.MinSize = this.arrowButtonsMinSize;
      this.popupButton.Click += new EventHandler(this.popupButton_Click);
      this.popupButton.BorderElement.Class = "GalleryPopupButtonButtonBorder";
      this.popupButton.ButtonFillElement.Class = "GalleryArrowButtonFill";
      this.buttonsPanel = new RadGalleryButtonsLayoutPanel();
      this.buttonsPanel.MaxSize = this.buttonsPanelMaxSize;
      int num = (int) this.buttonsPanel.SetValue(DropDownEditorLayoutPanel.IsArrowButtonProperty, (object) true);
      this.buttonsPanel.Children.Add((RadElement) this.upButton);
      this.buttonsPanel.Children.Add((RadElement) this.downButton);
      this.buttonsPanel.Children.Add((RadElement) this.popupButton);
      this.inribbonPanel = new DropDownEditorLayoutPanel();
      this.inribbonPanel.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.inribbonFillPrimitive = new FillPrimitive();
      this.inribbonFillPrimitive.Class = "InribbonGalleryFill";
      this.inribbonPanel.Children.Add((RadElement) this.inribbonItemsLayoutPanel);
      this.inribbonPanel.Children.Add((RadElement) this.buttonsPanel);
      this.inribbonPanelBorder = new BorderPrimitive();
      this.inribbonPanelBorder.Visibility = ElementVisibility.Collapsed;
      this.inribbonPanelBorder.Class = "InribbonGalleryBorder";
      this.Children.Add((RadElement) this.inribbonPanelBorder);
      this.Children.Add((RadElement) this.inribbonFillPrimitive);
      this.Children.Add((RadElement) this.inribbonPanel);
    }

    private void items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      RadGalleryItem radGalleryItem = (RadGalleryItem) target;
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
          radGalleryItem.Click += new EventHandler(this.galleryItem_Clicked);
          radGalleryItem.MouseHover += new EventHandler(this.item_MouseHover);
          if (this.GetBitState(35184372088832L))
            radGalleryItem.AddBehavior((PropertyChangeBehavior) this.zoomBehavior);
          radGalleryItem.Owner = this;
          radGalleryItem.StretchHorizontally = false;
          radGalleryItem.StretchVertically = false;
          break;
        case ItemsChangeOperation.Removed:
          radGalleryItem.Click -= new EventHandler(this.galleryItem_Clicked);
          radGalleryItem.MouseHover -= new EventHandler(this.item_MouseHover);
          break;
      }
      this.SetUpDownButtons();
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      this.SetUpDownButtons();
      return sizeF;
    }

    private void item_MouseHover(object sender, EventArgs e)
    {
      this.OnGalleryItemHover(new GalleryItemHoverEventArgs(sender as RadGalleryItem));
    }

    private void galleryItem_Clicked(object sender, EventArgs e)
    {
      this.lastClickedItem = sender as RadGalleryItem;
      if (this.lastClickedItem == null)
        return;
      this.SelectedItem = this.lastClickedItem;
      if (this.lastClickedItem.ElementTree != null && this.lastClickedItem.ElementTree.Control is ZoomPopup || !this.downMenu.IsVisible)
        return;
      this.downMenu.CallOnMouseLeave(EventArgs.Empty);
      this.CloseDropDown();
    }

    public void CloseDropDown()
    {
      if (this.downMenu == null)
        return;
      this.downMenu.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    private void popupButton_Click(object sender, EventArgs e)
    {
      this.OnMouseLeave(EventArgs.Empty);
      this.popupButton.IsMouseOver = false;
      this.ShowDropDown();
    }

    private void upButton_Click(object sender, EventArgs e)
    {
      this.ScrollLineUp();
    }

    public void ScrollLineUp()
    {
      if (this.currentLine > 0)
        --this.currentLine;
      this.inribbonItemsLayoutPanel.ScrollToLine(this.currentLine);
      this.SetUpDownButtons();
    }

    private void downButton_Click(object sender, EventArgs e)
    {
      this.ScrollLineDown();
    }

    public void ScrollLineDown()
    {
      if (this.currentLine < this.inribbonItemsLayoutPanel.LineCount)
        ++this.currentLine;
      this.inribbonItemsLayoutPanel.ScrollToLine(this.currentLine);
      this.SetUpDownButtons();
    }

    private void SetUpDownButtons()
    {
      if (this.inribbonItemsLayoutPanel.LineCount > -1)
      {
        this.currentLine = this.inribbonItemsLayoutPanel.CurrentLine;
        this.upButton.Enabled = this.currentLine != 0;
        this.downButton.Enabled = this.inribbonItemsLayoutPanel.LineCount - 1 != this.currentLine;
      }
      else
      {
        this.upButton.Enabled = false;
        this.downButton.Enabled = false;
      }
    }

    public void ShowDropDown()
    {
      int galleryHeight = (int) this.CalculateGalleryHeight(this.Size.Width);
      this.downMenu.MaximumSize = this.GetMaxScreenSize();
      int val1 = Math.Min(galleryHeight, this.downMenu.MaximumSize.Height);
      if (this.ElementTree != null && this.ElementTree.Control is RadControl)
        this.downMenu.ThemeName = ((RadControl) this.ElementTree.Control).ThemeName;
      this.MinSize = this.Size;
      if (this.galleryPopupElement != null)
      {
        this.galleryPopupElement.ClearCollections();
        this.galleryPopupElement.Dispose();
        this.galleryPopupElement = (RadGalleryPopupElement) null;
      }
      SizeF initialSize = new SizeF((float) this.Size.Width, (float) Math.Min(val1, this.downMenu.MaximumSize.Height));
      SizeF minimumSize = new SizeF((float) this.Size.Width, (float) this.Size.Height + this.CalculateGalleryMinHeight());
      this.galleryPopupElement = new RadGalleryPopupElement(this.Items, this.Groups, this.Filters, this.Tools, initialSize, minimumSize, this.DropDownSizingMode);
      this.downMenu.Size = minimumSize.ToSize();
      this.downMenu.PopupElement = (RadElement) this.galleryPopupElement;
      this.downMenu.PopupElement.MinSize = this.downMenu.Size;
      this.downMenu.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      this.downMenu.Show();
      this.UpdatePopUpAfterShow();
      this.downMenu.PopupElement.ZIndex = 1000;
      this.downMenu.BringToFront();
      this.downMenu.PopupElement.BringToFront();
    }

    private Size GetMaxScreenSize()
    {
      Rectangle workingArea = Screen.FromPoint(this.PointToScreen(this.Location)).WorkingArea;
      Size size = workingArea.Size;
      size.Height = workingArea.Bottom - this.PointToScreen(this.Location).Y;
      return size;
    }

    private void UpdatePopUpAfterShow()
    {
      Application.DoEvents();
      foreach (RadElement radElement in (RadItemCollection) this.Items)
        radElement.Visibility = ElementVisibility.Visible;
      if (this.galleryPopupElement.ElementTree != null)
        this.galleryPopupElement.ElementTree.Control.Refresh();
      Application.DoEvents();
      this.galleryPopupElement.InvalidateMeasure();
      this.galleryPopupElement.InvalidateArrange();
      this.galleryPopupElement.UpdateLayout();
      if (this.galleryPopupElement.ElementTree.Control != null)
        this.galleryPopupElement.ElementTree.Control.Refresh();
      Application.DoEvents();
    }

    private float CalculateGalleryMinHeight()
    {
      int num1 = this.Size.Height * 2;
      if (this.Items.Count == 0)
        return (float) num1;
      SizeF desiredSize = this.Items[0].DesiredSize;
      int num2 = this.Filters.Count > 0 ? 21 : 0;
      float num3 = (float) (this.Tools.Count * 21);
      return (float) ((double) desiredSize.Height + (double) num3 + (double) num2 + 13.0);
    }

    private float CalculateGalleryHeight(int width)
    {
      if (this.Items.Count == 0)
        return 100f;
      SizeF desiredSize = this.Items[0].DesiredSize;
      int num1 = 1;
      float num2 = 0.0f;
      if (width > 0 && (double) desiredSize.Width > 0.0)
        num1 = Math.Max((int) ((double) width / (double) desiredSize.Width), 1);
      if (this.Groups.Count > 0)
      {
        foreach (RadGalleryGroupItem group in (RadItemCollection) this.Groups)
        {
          int num3 = group.Items.Count / num1;
          if (group.Items.Count % num1 != 0)
            ++num3;
          num2 += (float) num3 * desiredSize.Height;
          num2 += 21f;
        }
      }
      else
        num2 = (float) Math.Max(this.Items.Count / num1, 1) * desiredSize.Height;
      int num4 = this.Filters.Count > 0 ? 21 : 0;
      float num5 = (float) (this.Tools.Count * 21);
      return (float) ((double) num2 + (double) num5 + (double) num4 + 13.0);
    }

    private void AdjustGalleryAfterPopUpClose()
    {
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      RadRibbonBar control = this.ElementTree.Control as RadRibbonBar;
      Application.DoEvents();
      if (control != null)
      {
        control.RibbonBarElement.InvalidateMeasure();
        control.RibbonBarElement.InvalidateArrange();
        control.RibbonBarElement.UpdateLayout();
        control.Invalidate();
      }
      this.InvalidateMeasure();
      this.InvalidateArrange();
      this.inribbonItemsLayoutPanel.InvalidateMeasure();
      this.inribbonItemsLayoutPanel.InvalidateArrange();
      Application.DoEvents();
    }

    private void ScrollToSelectedItem()
    {
      if (this.lastClickedItem == null)
        return;
      this.inribbonItemsLayoutPanel.ScrollToElement((RadElement) this.lastClickedItem);
      this.SetUpDownButtons();
      this.lastClickedItem = (RadGalleryItem) null;
    }

    private void ResetGalleryItemsAndGroups()
    {
      if (this.groups.Count == 0)
      {
        this.galleryPopupElement.GroupHolderStackLayout.Children.Clear();
      }
      else
      {
        foreach (RadGalleryGroupItem group in (RadItemCollection) this.groups)
        {
          group.Items.Owner = (RadElement) this.inribbonItemsLayoutPanel;
          if (this.galleryPopupElement.GroupHolderStackLayout.Children.Contains((RadElement) group))
            this.galleryPopupElement.GroupHolderStackLayout.Children.Remove((RadElement) group);
        }
      }
      this.Items.Owner = (RadElement) this.inribbonItemsLayoutPanel;
      this.inribbonItemsLayoutPanel.InvalidateMeasure();
      this.inribbonItemsLayoutPanel.InvalidateArrange();
      this.inribbonItemsLayoutPanel.UpdateLayout();
    }

    private void HideZoomPopups()
    {
      foreach (RadGalleryItem radGalleryItem in (RadItemCollection) this.Items)
      {
        if (radGalleryItem.IsZoomShown())
          radGalleryItem.ZoomHide();
      }
    }

    internal void DoScrollLineUp()
    {
      if (!this.downMenu.IsVisible)
        return;
      this.popupScrollViewer.LineUp();
    }

    internal void DoScrollLineDown()
    {
      if (!this.downMenu.IsVisible)
        return;
      this.popupScrollViewer.LineDown();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (e.Delta > 0)
        this.ScrollLineUp();
      else if (e.Delta < 0)
        this.ScrollLineDown();
      if (!(e is HandledMouseEventArgs) || !this.downMenu.IsVisible)
        return;
      ((HandledMouseEventArgs) e).Handled = true;
    }

    [Browsable(true)]
    [Description("Occurs when the mouse pointer rests on the gallery item.")]
    [Category("Action")]
    public event GalleryItemHoverEventHandler GalleryItemHover
    {
      add
      {
        this.Events.AddHandler(RadGalleryElement.GalleryItemHoverEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadGalleryElement.GalleryItemHoverEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnGalleryItemHover(GalleryItemHoverEventArgs e)
    {
      GalleryItemHoverEventHandler hoverEventHandler = (GalleryItemHoverEventHandler) this.Events[RadGalleryElement.GalleryItemHoverEventKey];
      if (hoverEventHandler == null)
        return;
      hoverEventHandler((object) this, e);
    }

    [Description("Occurs before the drop-down window appears.")]
    [Browsable(true)]
    [Category("Behavior")]
    public event CancelEventHandler DropDownOpening
    {
      add
      {
        this.Events.AddHandler(RadGalleryElement.DropDownOpeningEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadGalleryElement.DropDownOpeningEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownOpening(CancelEventArgs e)
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadGalleryElement.DropDownOpeningEventKey];
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
        this.Events.AddHandler(RadGalleryElement.DropDownOpenedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadGalleryElement.DropDownOpenedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownOpened(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadGalleryElement.DropDownOpenedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Occurs when the drop-down is about to be closed.")]
    public event RadPopupClosingEventHandler DropDownClosing
    {
      add
      {
        this.Events.AddHandler(RadGalleryElement.DropDownClosingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadGalleryElement.DropDownClosingEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownClosing(RadPopupClosingEventArgs e)
    {
      RadPopupClosingEventHandler closingEventHandler = (RadPopupClosingEventHandler) this.Events[RadGalleryElement.DropDownClosingEventKey];
      if (closingEventHandler == null)
        return;
      closingEventHandler((object) this, e);
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Occurs when the drop-down window has closed.")]
    public event RadPopupClosedEventHandler DropDownClosed
    {
      add
      {
        this.Events.AddHandler(RadGalleryElement.DropDownClosedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadGalleryElement.DropDownClosedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownClosed(RadPopupClosedEventArgs e)
    {
      RadPopupClosedEventHandler closedEventHandler = (RadPopupClosedEventHandler) this.Events[RadGalleryElement.DropDownClosedEventKey];
      if (closedEventHandler == null)
        return;
      closedEventHandler((object) this, e);
    }

    [DefaultValue(false)]
    public bool DropDownInheritsThemeClassName
    {
      get
      {
        return this.GetBitState(70368744177664L);
      }
      set
      {
        this.SetBitState(70368744177664L, value);
      }
    }

    bool IDropDownMenuOwner.ControlDefinesThemeForElement(RadElement element)
    {
      return element.Class == "RadGalleryPopupFilterDropDownButton";
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.IsFocusedProperty || !this.IsFocused)
        return;
      this.popupButton.Focus();
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (!this.IsInValidState(true) || args.RoutedEvent != RootRadElement.OnRoutedImageListChanged)
        return;
      this.downMenu.ImageList = this.ElementTree.ComponentTreeHandler.ImageList;
    }
  }
}
