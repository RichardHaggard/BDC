// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadRibbonBarGroupDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RadRibbonBarGroup : CollapsibleElement, IItemsElement
  {
    private static readonly Padding defaultMargin = new Padding(2);
    private static readonly Size defaultMaxSize = new Size(0, 100);
    private static readonly Size defaultMinSize = new Size(20, 86);
    public static RadProperty OldImageProperty = RadProperty.Register("OldImage", typeof (Image), typeof (RadRibbonBarGroup), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    public static RadProperty OldImageIndexProperty = RadProperty.Register("OldImageIndex", typeof (int), typeof (RadRibbonBarGroup), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.None));
    private RadItemOwnerCollection items;
    private StackLayoutPanel stackLayoutPanel;
    private RadDropDownButtonElement dropDownElement;
    private RadButtonElement dialogButton;
    private TextPrimitive textPrimitive;
    private BorderPrimitive borderPrimitive;
    private FillPrimitive captionElementFill;
    private FillPrimitive groupFill;
    private FillPrimitive bodyElementFill;
    private int collapsingPriority;
    private bool wasDropDownOpened;
    private ElementWithCaptionLayoutPanel elementWithCaptionLayoutPanel;
    private Image collapsedImage;

    static RadRibbonBarGroup()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (RadRibbonBarGroup));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[14]
      {
        typeof (RadButtonElement),
        typeof (RadCheckBoxElement),
        typeof (RadDropDownButtonElement),
        typeof (RadDropDownListElement),
        typeof (RadGalleryElement),
        typeof (RadImageButtonElement),
        typeof (RadProgressBarElement),
        typeof (RadRepeatButtonElement),
        typeof (RadRadioButtonElement),
        typeof (RadSplitButtonElement),
        typeof (RadTextBoxElement),
        typeof (RadToggleButtonElement),
        typeof (RadTrackBarElement),
        typeof (RadRibbonBarButtonGroup)
      };
      this.items.ExcludedTypes = new System.Type[1]
      {
        typeof (RadTextBoxElement)
      };
      this.AutoSize = true;
      this.MinSize = RadRibbonBarGroup.defaultMinSize;
      this.MaxSize = RadRibbonBarGroup.defaultMaxSize;
      this.Margin = RadRibbonBarGroup.defaultMargin;
      this.Shape = (ElementShape) new RoundRectShape(3);
    }

    protected override void CreateChildElements()
    {
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "RibbonBarChunkBorder";
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.groupFill = new FillPrimitive();
      this.groupFill.Class = "RibbonBarGroupMainFill";
      this.groupFill.ZIndex = -1;
      int num1 = (int) this.groupFill.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) Color.Transparent);
      int num2 = (int) this.groupFill.SetDefaultValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid);
      this.textPrimitive = new TextPrimitive();
      this.textPrimitive.RadPropertyChanged += new RadPropertyChangedEventHandler(this.textPrimitive_RadPropertyChanged);
      int num3 = (int) this.textPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.textPrimitive.Class = "RibbonBarChunkCaption";
      this.textPrimitive.Alignment = ContentAlignment.MiddleCenter;
      this.textPrimitive.Padding = new Padding(0);
      this.elementWithCaptionLayoutPanel = new ElementWithCaptionLayoutPanel();
      DockLayoutPanel dockLayoutPanel = new DockLayoutPanel();
      this.captionElementFill = new FillPrimitive();
      this.captionElementFill.AutoSizeMode = RadAutoSizeMode.Auto;
      int num4 = (int) this.captionElementFill.SetValue(ElementWithCaptionLayoutPanel.CaptionElementProperty, (object) true);
      this.captionElementFill.Class = "ChunkCaptionFill";
      this.captionElementFill.Children.Add((RadElement) dockLayoutPanel);
      this.dialogButton = new RadButtonElement();
      this.dialogButton.Padding = new Padding(0, 3, 0, 0);
      int num5 = (int) this.dialogButton.SetDefaultValueOverride(RadButtonItem.ImageProperty, (object) Telerik.WinControls.ResourceHelper.ImageFromResource(typeof (RadRibbonBarGroup), "Telerik.WinControls.UI.Resources.RibbonDialogButton.png"));
      this.dialogButton.Alignment = ContentAlignment.BottomRight;
      this.dialogButton.Visibility = ElementVisibility.Collapsed;
      this.dialogButton.Class = "DialogButtonClass";
      int num6 = (int) this.dialogButton.SetValue(DockLayoutPanel.DockProperty, (object) Dock.Right);
      dockLayoutPanel.Children.Add((RadElement) this.dialogButton);
      dockLayoutPanel.Children.Add((RadElement) this.textPrimitive);
      dockLayoutPanel.LastChildFill = true;
      this.elementWithCaptionLayoutPanel.Children.Add((RadElement) this.captionElementFill);
      this.bodyElementFill = new FillPrimitive();
      this.bodyElementFill.AutoSizeMode = RadAutoSizeMode.Auto;
      this.bodyElementFill.Class = "ChunkBodyFill";
      this.bodyElementFill.Padding = new Padding(2, 2, 2, 0);
      BorderPrimitive borderPrimitive = new BorderPrimitive();
      borderPrimitive.Class = "BodyBorder";
      borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      borderPrimitive.GradientStyle = GradientStyles.Linear;
      borderPrimitive.ForeColor = Color.Transparent;
      borderPrimitive.ForeColor2 = Color.White;
      borderPrimitive.ForeColor3 = Color.White;
      borderPrimitive.ForeColor4 = Color.White;
      this.bodyElementFill.Children.Add((RadElement) borderPrimitive);
      this.stackLayoutPanel = new StackLayoutPanel();
      this.elementWithCaptionLayoutPanel.Children.Add((RadElement) this.bodyElementFill);
      this.bodyElementFill.Children.Add((RadElement) this.stackLayoutPanel);
      this.dropDownElement = (RadDropDownButtonElement) new RadRibbonBarGroupDropDownButtonElement();
      this.dropDownElement.DropDownInheritsThemeClassName = true;
      this.dropDownElement.Visibility = ElementVisibility.Collapsed;
      this.dropDownElement.ActionButton.Shape = (ElementShape) new RoundRectShape(4);
      this.dropDownElement.BorderElement.Class = "GroupDropDownButtonBorder";
      this.dropDownElement.BorderElement.Visibility = ElementVisibility.Collapsed;
      RadDropDownMenuElement popupElement = this.dropDownElement.DropDownMenu.PopupElement as RadDropDownMenuElement;
      popupElement.Fill.Class = "RibbonBarGroupDropDownFill";
      popupElement.Border.Class = "RibbonBarGroupDropDownBorder";
      popupElement.Class = "RibbonBarGroupDropDownElement";
      this.dropDownElement.DropDownMenu.RootElement.Class = "RibbonBarGroupDropDownRoot";
      FillPrimitive fillPrimitive = new FillPrimitive();
      fillPrimitive.Visibility = ElementVisibility.Collapsed;
      fillPrimitive.Class = "ChunkBodyFill";
      this.dropDownElement.DropDownMenu.RootElement.Children.Add((RadElement) fillPrimitive);
      this.dropDownElement.Image = (Image) Telerik.WinControls.ResourceHelper.ImageFromResource(typeof (RadRibbonBarGroup), "Telerik.WinControls.UI.Resources.dropDown.png");
      this.dropDownElement.DisplayStyle = DisplayStyle.ImageAndText;
      this.dropDownElement.TextImageRelation = TextImageRelation.ImageAboveText;
      this.dropDownElement.ShowArrow = false;
      this.dropDownElement.Margin = new Padding(4, 4, 4, 4);
      this.dropDownElement.ActionButton.BorderElement.Visibility = ElementVisibility.Hidden;
      this.dropDownElement.ActionButton.BorderElement.Class = "GroupDropDownButtonInnerBorder";
      this.dropDownElement.ActionButton.Padding = new Padding(4, 10, 4, 28);
      int num7 = (int) this.dropDownElement.BindProperty(RadItem.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.OneWay);
      this.dropDownElement.DropDownOpening += new CancelEventHandler(this.dropDownElement_DropDownOpening);
      this.Children.Add((RadElement) this.dropDownElement);
      this.dropDownElement.ImageAlignment = ContentAlignment.MiddleCenter;
      this.dropDownElement.ThemeRole = "RibbonGroupDropDownButton";
      this.Children.Add((RadElement) this.elementWithCaptionLayoutPanel);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.groupFill);
      this.items.Owner = (RadElement) this.stackLayoutPanel;
      this.items.ItemsChanged += new ItemChangedDelegate(this.ItemChanged);
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadRibbonBarGroup);
      }
    }

    public BorderPrimitive GroupBorder
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    public FillPrimitive GroupFill
    {
      get
      {
        return this.groupFill;
      }
    }

    public FillPrimitive CaptionFill
    {
      get
      {
        return this.captionElementFill;
      }
    }

    public FillPrimitive BodyFill
    {
      get
      {
        return this.bodyElementFill;
      }
    }

    [Description("Show or hide the dialog button")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool ShowDialogButton
    {
      get
      {
        return this.dialogButton.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (value)
        {
          this.dialogButton.Visibility = ElementVisibility.Visible;
          this.textPrimitive.Padding = new Padding(this.textPrimitive.Padding.Left, this.textPrimitive.Padding.Top, this.textPrimitive.Padding.Right + 10, this.textPrimitive.Padding.Bottom);
        }
        else
        {
          this.dialogButton.Visibility = ElementVisibility.Collapsed;
          this.textPrimitive.Padding = new Padding(this.textPrimitive.Padding.Left, this.textPrimitive.Padding.Top, this.textPrimitive.Padding.Right - 10, this.textPrimitive.Padding.Bottom);
        }
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadButtonElement DialogButton
    {
      get
      {
        return this.dialogButton;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadDropDownButtonElement DropDownElement
    {
      get
      {
        return this.dropDownElement;
      }
    }

    [RadNewItem("", false, false, false)]
    [Browsable(true)]
    [Category("Data")]
    [Description("Gets a collection of the items placed in the chunk.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [Description("Gets or sets the orientation of the items inside the group. Possible values are: Horizontal and Vertical.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(typeof (Orientation), "Horizontal")]
    public Orientation Orientation
    {
      get
      {
        return this.stackLayoutPanel.Orientation;
      }
      set
      {
        this.stackLayoutPanel.Orientation = value;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    [Category("Behavior")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image CollapsedImage
    {
      get
      {
        return this.collapsedImage;
      }
      set
      {
        this.collapsedImage = value;
        this.dropDownElement.Image = value;
      }
    }

    [Description("Get or Set collapsing order weight - bigger mean to start collapsing from this RadRibbonbarGroup")]
    [DefaultValue(0)]
    public int CollapsingPriority
    {
      get
      {
        return this.collapsingPriority;
      }
      set
      {
        this.collapsingPriority = value;
      }
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "Margin")
        return new bool?(this.Margin != RadRibbonBarGroup.defaultMargin);
      if (property.Name == "MinSize")
        return new bool?(this.MinSize != RadRibbonBarGroup.defaultMinSize);
      if (property.Name == "MaxSize")
        return new bool?(this.MaxSize != RadRibbonBarGroup.defaultMaxSize);
      return base.ShouldSerializeProperty(property);
    }

    public override string ToString()
    {
      return "RibbonBarGroup: " + this.Text;
    }

    internal ChunkVisibilityState VisibilityState
    {
      get
      {
        return (ChunkVisibilityState) this.CollapseStep;
      }
    }

    private bool ChangeImages(RadButtonElement button, bool display)
    {
      if (button.SmallImage == null && button.SmallImageIndex == -1 && button.SmallImageKey == string.Empty)
        return false;
      if (!display)
      {
        button.UseSmallImageList = true;
        this.PreserveOldImage(button);
        if (button.SmallImage != null)
          button.Image = button.SmallImage;
        else if (button.SmallImageIndex != -1)
          button.ImageIndex = button.SmallImageIndex;
        else if (button.SmallImageKey != string.Empty)
          button.ImageKey = button.SmallImageKey;
      }
      else
      {
        button.UseSmallImageList = false;
        this.RestoreOldImage(button);
      }
      return true;
    }

    private void PreserveOldImage(RadButtonElement button)
    {
      if (button.Image != null)
      {
        int num1 = (int) button.SetValue(RadButtonElement.LargeImageProperty, (object) button.Image);
      }
      else if (button.ImageIndex != -1)
      {
        int num2 = (int) button.SetValue(RadButtonElement.LargeImageIndexProperty, (object) button.ImageIndex);
      }
      else
      {
        if (!(button.ImageKey == string.Empty))
          return;
        int num3 = (int) button.SetValue(RadButtonElement.LargeImageKeyProperty, (object) button.ImageKey);
      }
    }

    private void RestoreOldImage(RadButtonElement button)
    {
      if (button.LargeImage != null)
      {
        button.Image = button.LargeImage;
        int num = (int) button.SetValue(RadButtonElement.LargeImageProperty, (object) null);
      }
      else if (button.LargeImageIndex != -1)
      {
        button.ImageIndex = button.LargeImageIndex;
        int num = (int) button.SetValue(RadButtonElement.LargeImageIndexProperty, (object) -1);
      }
      else
      {
        if (!(button.LargeImageKey == string.Empty))
          return;
        button.ImageKey = button.LargeImageKey;
        int num = (int) button.SetValue(RadButtonElement.LargeImageKeyProperty, (object) string.Empty);
      }
    }

    public override bool ExpandElementToStep(int collapseStep)
    {
      bool flag1 = false;
      if (!this.CanCollapseOrExpandElement(ChunkVisibilityState.Expanded))
        return flag1;
      this.InvalidateIfNeeded();
      bool flag2;
      if (this.CollapseStep == 4)
      {
        this.ExpandChunkFromDropDown();
        --this.CollapseStep;
        flag2 = true;
        this.CollapseCollection(2);
      }
      else
        flag2 = this.ExpandCollection(collapseStep);
      return flag2;
    }

    private bool CanCollapseOrExpandElement(ChunkVisibilityState state)
    {
      return !this.IsDesignMode;
    }

    public override bool CollapseElementToStep(int nextStep)
    {
      if (!this.CanCollapseOrExpandElement(ChunkVisibilityState.Collapsed))
        return false;
      this.InvalidateIfNeeded();
      bool flag;
      if (nextStep == 4)
      {
        this.CollapseChunkToDropDown();
        this.CollapseStep = nextStep;
        flag = true;
      }
      else
        flag = this.CollapseCollection(nextStep);
      return flag;
    }

    private void CollapseChunkToDropDown()
    {
      this.ExpandElementToStep(1);
      ExpandableStackLayout.InvalidateAll((RadElement) this);
      this.UpdateLayout();
      SizeF size = (SizeF) this.Size;
      this.elementWithCaptionLayoutPanel.SuspendThemeRefresh();
      this.Children.Remove((RadElement) this.elementWithCaptionLayoutPanel);
      this.elementWithCaptionLayoutPanel.ResumeThemeRefresh();
      RadRibbonBarGroup.RadGroupItem radGroupItem = new RadRibbonBarGroup.RadGroupItem();
      radGroupItem.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      radGroupItem.Children.Add((RadElement) this.elementWithCaptionLayoutPanel);
      radGroupItem.MinSize = new Size(size.ToSize().Width, radGroupItem.MinSize.Height);
      RadDropDownMenu dropDownMenu = this.dropDownElement.DropDownMenu;
      RadDropDownMenuElement popupElement = (RadDropDownMenuElement) dropDownMenu.PopupElement;
      (popupElement.LayoutPanel as StackLayoutPanel).Orientation = this.Orientation;
      popupElement.Layout.LeftColumnMinWidth = 0;
      this.dropDownElement.Items.Add((RadItem) radGroupItem);
      this.dropDownElement.Visibility = ElementVisibility.Visible;
      this.CollapseStep = 4;
      dropDownMenu.SetTheme();
    }

    private void ExpandChunkFromDropDown()
    {
      this.dropDownElement.Visibility = ElementVisibility.Collapsed;
      this.dropDownElement.Items.Clear();
      this.Children.Add((RadElement) this.elementWithCaptionLayoutPanel);
      string str = this.ElementTree.ThemeName;
      if (string.IsNullOrEmpty(str))
        str = ThemeResolutionService.ApplicationThemeName;
      if (!this.wasDropDownOpened)
        return;
      this.wasDropDownOpened = false;
      this.ElementTree.ThemeName = str;
      this.ElementTree.ApplyThemeToElement((RadObject) this, true);
    }

    public override bool CanExpandToStep(int nextStep)
    {
      if (nextStep >= this.CollapseStep || nextStep == 4)
        return false;
      if (this.CollapseStep == 4)
        return true;
      this.InvalidateIfNeeded();
      for (int index = 0; index < this.collapsableChildren.Count; ++index)
      {
        if (this.collapsableChildren[index].CanExpandToStep(nextStep))
          return true;
      }
      return false;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool CanCollapseToStep(int nextStep)
    {
      if (!this.AllowCollapsed || nextStep <= this.CollapseStep)
        return false;
      if (nextStep == 4)
        return true;
      this.InvalidateIfNeeded();
      for (int index = 0; index < this.collapsableChildren.Count; ++index)
      {
        if (this.collapsableChildren[index].CanCollapseToStep(nextStep))
          return true;
      }
      return false;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override int CollapseMaxSteps
    {
      get
      {
        return 4;
      }
    }

    [Category("Action")]
    [Description("Occurs when Dialog Button is clicked")]
    public event EventHandler DialogButtonClick;

    private void dropDownElement_DropDownOpening(object sender, CancelEventArgs e)
    {
      string str = this.ElementTree.ThemeName;
      if (string.IsNullOrEmpty(str))
        str = ThemeResolutionService.ApplicationThemeName;
      this.dropDownElement.DropDownMenu.PopupElement.ElementTree.ThemeName = str;
      this.dropDownElement.DropDownMenu.PopupElement.ElementTree.ApplyThemeToElementTree();
      this.wasDropDownOpened = true;
    }

    private void textPrimitive_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.PaddingProperty)
        return;
      this.textPrimitive.Padding = new Padding(0);
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      if (args.RoutedEvent != RadElement.MouseClickedEvent || sender != this.dialogButton)
        return;
      this.OnDialogButtonClick((object) sender, args.OriginalEventArgs);
    }

    public void ItemClicked(RadItem item)
    {
    }

    protected virtual void OnDialogButtonClick(object sender, EventArgs e)
    {
      if (this.DialogButtonClick == null)
        return;
      this.DialogButtonClick(sender, e);
    }

    private void ItemChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      this.invalidateCollapsableChildrenCollection = true;
      if (operation != ItemsChangeOperation.Inserted && operation != ItemsChangeOperation.Set)
        return;
      if ((object) target.GetType() == (object) typeof (RadButtonElement))
      {
        RadButtonElement radButtonElement = target as RadButtonElement;
        if (string.IsNullOrEmpty(radButtonElement.Class))
          radButtonElement.Class = "RibbonBarButtonElement";
        radButtonElement.ButtonFillElement.Class = "ButtonInRibbonFill";
        radButtonElement.BorderElement.Class = "ButtonInRibbonBorder";
        if (this.ElementTree != null)
          this.ElementTree.ApplyThemeToElement((RadObject) radButtonElement);
      }
      else if (target is RadRibbonBarButtonGroup)
        target.MinSize = new Size(22, 22);
      else if (target is RadDropDownListElement)
      {
        target.MinSize = new Size(140, 0);
        target.StretchVertically = false;
        target.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      }
      else if ((object) target.GetType() == (object) typeof (RadRadioButtonElement))
      {
        target.MinSize = new Size(20, 0);
        target.StretchVertically = false;
        target.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      }
      else if (target is RadTextBoxElement)
      {
        target.MinSize = new Size(140, 0);
        target.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
        Padding padding = target.Padding;
        padding.Left = 0;
        padding.Right = 0;
        target.Padding = padding;
      }
      else if (target is RadGalleryElement)
        target.Padding = new Padding(2, 2, 0, 0);
      else if ((object) target.GetType() == (object) typeof (RadCheckBoxElement))
        target.StretchVertically = false;
      target.NotifyParentOnMouseInput = true;
    }

    private class RadGroupItem : RadItem
    {
      protected override System.Type ThemeEffectiveType
      {
        get
        {
          return typeof (RadRibbonBarGroup);
        }
      }
    }
  }
}
