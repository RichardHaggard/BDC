// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layout;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadMenuElement : RadItem
  {
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (RadMenuElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal));
    public static RadProperty AllItemsEqualHeightProperty = RadProperty.Register(nameof (AllItemsEqualHeight), typeof (bool), typeof (RadMenuElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty DropDownAnimationEnabledProperty = RadProperty.Register(nameof (DropDownAnimationEnabled), typeof (bool), typeof (RadMenuElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty DropDownAnimationEasingProperty = RadProperty.Register(nameof (DropDownAnimationEasing), typeof (RadEasingType), typeof (RadMenuElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadEasingType.Linear, ElementPropertyOptions.None));
    public static RadProperty DropDownAnimationFramesProperty = RadProperty.Register(nameof (DropDownAnimationFrames), typeof (int), typeof (RadMenuElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.None));
    private bool allowMerge = true;
    private WrapLayoutPanel layoutPanel;
    private RadItemOwnerCollection items;
    private RadItem contextItem;
    private StackLayoutPanel systemButtons;
    private RadImageButtonElement minimizeButton;
    private RadImageButtonElement maximizeButton;
    private RadImageButtonElement closeButton;
    private FillPrimitive fill;
    private BorderPrimitive border;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new System.Type[4]
      {
        typeof (RadMenuItemBase),
        typeof (RadMenuItem),
        typeof (RadMenuComboItem),
        typeof (RadMenuButtonItem)
      };
      this.items.DefaultType = typeof (RadMenuItem);
      this.items.ItemsChanged += new ItemChangedDelegate(this.ItemsChanged);
    }

    protected override void CreateChildElements()
    {
      this.contextItem = (RadItem) null;
      this.layoutPanel = new WrapLayoutPanel();
      this.layoutPanel.ZIndex = 10;
      int num = (int) this.layoutPanel.BindProperty(WrapLayoutPanel.OrientationProperty, (RadObject) this, RadMenuElement.OrientationProperty, PropertyBindingOptions.OneWay);
      this.items.Owner = (RadElement) this.layoutPanel;
      this.Children.Add((RadElement) this.layoutPanel);
      this.fill = new FillPrimitive();
      this.fill.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.fill.Visibility = ElementVisibility.Collapsed;
      this.fill.Class = "MenuFill";
      this.fill.ZIndex = 5;
      this.Children.Add((RadElement) this.fill);
      this.systemButtons = new StackLayoutPanel();
      this.systemButtons.Alignment = ContentAlignment.MiddleRight;
      this.systemButtons.ZIndex = 0;
      this.systemButtons.Class = "SystemButtonsStackLayoutPanel";
      this.minimizeButton = new RadImageButtonElement();
      this.minimizeButton.StretchHorizontally = false;
      this.minimizeButton.StretchVertically = false;
      this.minimizeButton.Class = "MinimizeButton";
      this.minimizeButton.ButtonFillElement.Name = "MinimizeButtonFill";
      this.systemButtons.Children.Add((RadElement) this.minimizeButton);
      this.maximizeButton = new RadImageButtonElement();
      this.maximizeButton.StretchHorizontally = false;
      this.maximizeButton.StretchVertically = false;
      this.maximizeButton.Class = "MaximizeButton";
      this.maximizeButton.ButtonFillElement.Name = "MaximizeButtonFill";
      this.systemButtons.Children.Add((RadElement) this.maximizeButton);
      this.closeButton = new RadImageButtonElement();
      this.closeButton.StretchHorizontally = false;
      this.closeButton.StretchVertically = false;
      this.closeButton.Class = "CloseButton";
      this.closeButton.ButtonFillElement.Name = "CloseButtonFill";
      this.systemButtons.Children.Add((RadElement) this.closeButton);
      this.systemButtons.ZIndex = 6;
      this.Children.Add((RadElement) this.systemButtons);
      this.border = new BorderPrimitive();
      this.border.Class = "MenuBorder";
      this.border.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.Children.Add((RadElement) this.border);
      this.systemButtons.Visibility = ElementVisibility.Collapsed;
    }

    [Description("Occurs when the menu Orientation property value changes.")]
    [Category("Property Changed")]
    public event EventHandler OrientationChanged;

    [Description("Occurs when the menu AllItemsEqualHeight property value changes.")]
    [Category("Property Changed")]
    public event EventHandler AllItemsEqualHeightChanged;

    public RadImageButtonElement MinimizeButton
    {
      get
      {
        return this.minimizeButton;
      }
      set
      {
        this.minimizeButton = value;
      }
    }

    public RadImageButtonElement MaximizeButton
    {
      get
      {
        return this.maximizeButton;
      }
      set
      {
        this.maximizeButton = value;
      }
    }

    public RadImageButtonElement CloseButton
    {
      get
      {
        return this.closeButton;
      }
      set
      {
        this.closeButton = value;
      }
    }

    public StackLayoutPanel SystemButtons
    {
      get
      {
        return this.systemButtons;
      }
      set
      {
        this.systemButtons = value;
      }
    }

    public RadMenuItemBase ContextItem
    {
      get
      {
        return this.contextItem as RadMenuItemBase;
      }
      set
      {
        this.contextItem = (RadItem) value;
      }
    }

    [Browsable(false)]
    public WrapLayoutPanel ItemsLayout
    {
      get
      {
        return this.layoutPanel;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [RadNewItem("Type here", true)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [DefaultValue(true)]
    [Description("Allow Merge")]
    [Category("Behavior")]
    public bool AllowMerge
    {
      get
      {
        return this.allowMerge;
      }
      set
      {
        if (this.allowMerge == value)
          return;
        this.allowMerge = value;
      }
    }

    [RadPropertyDefaultValue("Orientation", typeof (RadMenuElement))]
    [Description("Gets or sets the orientation of menu items. Horizontal or Vertical.")]
    [Browsable(true)]
    [Category("Behavior")]
    public Orientation Orientation
    {
      get
      {
        return (Orientation) this.GetValue(RadMenuElement.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuElement.OrientationProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets whether all items will appear with the same size (the size of the highest item in the collection).")]
    [Browsable(true)]
    [RadPropertyDefaultValue("AllItemsEqualHeight", typeof (RadMenuElement))]
    public bool AllItemsEqualHeight
    {
      get
      {
        return (bool) this.GetValue(RadMenuElement.AllItemsEqualHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuElement.AllItemsEqualHeightProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating whether the DropDown animation will be enabled when it shows.")]
    [Browsable(true)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("DropDownAnimationEnabled", typeof (RadMenuElement))]
    public bool DropDownAnimationEnabled
    {
      get
      {
        return (bool) this.GetValue(RadMenuElement.DropDownAnimationEnabledProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuElement.DropDownAnimationEnabledProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the type of the DropDown animation.")]
    [Browsable(true)]
    [RadPropertyDefaultValue("DropDownAnimationEasing", typeof (RadMenuElement))]
    public RadEasingType DropDownAnimationEasing
    {
      get
      {
        return (RadEasingType) this.GetValue(RadMenuElement.DropDownAnimationEasingProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuElement.DropDownAnimationEasingProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("DropDownAnimationFrames", typeof (RadMenuElement))]
    [Description("Gets or sets the number of frames that will be used when the DropDown is being animated.")]
    public int DropDownAnimationFrames
    {
      get
      {
        return (int) this.GetValue(RadMenuElement.DropDownAnimationFramesProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuElement.DropDownAnimationFramesProperty, (object) value);
      }
    }

    [Browsable(false)]
    public FillPrimitive MenuFill
    {
      get
      {
        return this.fill;
      }
    }

    [Browsable(false)]
    public BorderPrimitive MenuBorder
    {
      get
      {
        return this.border;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadMenuElement.OrientationProperty)
      {
        this.SetItemsRotationDegree((Orientation) e.NewValue);
        this.OnOrientationChanged(EventArgs.Empty);
      }
      if (e.Property == RadMenuElement.AllItemsEqualHeightProperty)
        this.OnAllItemsEqualHeightChanged(EventArgs.Empty);
      base.OnPropertyChanged(e);
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (args.RoutedEvent != RootRadElement.AutoSizeChangedEvent || this.ElementTree != null && !typeof (RadDropDownList).IsAssignableFrom(this.ElementTree.Control.GetType()))
        return;
      if (((AutoSizeEventArgs) args.OriginalEventArgs).AutoSize)
      {
        this.layoutPanel.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
        this.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      }
      else
      {
        this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
        this.layoutPanel.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      }
    }

    protected virtual void OnOrientationChanged(EventArgs args)
    {
      if (this.OrientationChanged == null)
        return;
      this.OrientationChanged((object) this, args);
    }

    protected virtual void OnAllItemsEqualHeightChanged(EventArgs args)
    {
      if (this.AllItemsEqualHeightChanged == null)
        return;
      this.AllItemsEqualHeightChanged((object) this, args);
    }

    private void ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation == ItemsChangeOperation.Inserted || operation == ItemsChangeOperation.Set)
      {
        int num = (int) target.SetDefaultValueOverride(RadElement.AngleTransformProperty, (object) this.GetRotationDegree(this.Orientation));
      }
      RadMenuItemBase radMenuItemBase = target as RadMenuItemBase;
      if (radMenuItemBase == null)
        return;
      switch (operation)
      {
        case ItemsChangeOperation.Inserted:
        case ItemsChangeOperation.Set:
          radMenuItemBase.Owner = (object) this;
          if (!this.IsDesignMode)
            radMenuItemBase.ClickMode = ClickMode.Press;
          radMenuItemBase.IsMainMenuItem = true;
          break;
        case ItemsChangeOperation.Removed:
          radMenuItemBase.Deselect();
          radMenuItemBase.Owner = (object) null;
          if (!radMenuItemBase.IsPopupShown)
            break;
          radMenuItemBase.HideChildItems();
          break;
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF finalRect = new RectangleF((float) ((double) finalSize.Width - (double) this.systemButtons.DesiredSize.Width - 3.0), 0.0f, this.systemButtons.DesiredSize.Width, this.systemButtons.DesiredSize.Height);
      if (this.RightToLeft)
        finalRect.X = 3f;
      this.systemButtons.Arrange(finalRect);
      return finalSize;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Width += (float) this.Padding.Horizontal;
      sizeF.Height += (float) this.Padding.Vertical;
      return sizeF;
    }

    private void SetItemsRotationDegree(Orientation orientation)
    {
      float rotationDegree = this.GetRotationDegree(orientation);
      this.SuspendLayout();
      foreach (RadObject radObject in (RadItemCollection) this.Items)
      {
        int num = (int) radObject.SetDefaultValueOverride(RadElement.AngleTransformProperty, (object) rotationDegree);
      }
      this.ResumeLayout(true);
    }

    private float GetRotationDegree(Orientation orientation)
    {
      return orientation != Orientation.Horizontal ? 90f : 0.0f;
    }
  }
}
