// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCarousel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.UI.Carousel;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [Description("Enables a user to select from a group of items, animated in Carousel-style rotation")]
  [DefaultEvent("SelectedItemChanged")]
  [Designer("Telerik.WinControls.UI.Design.RadCarouselDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [DefaultProperty("Items")]
  [Docking(DockingBehavior.Ask)]
  public class RadCarousel : RadControl
  {
    private static bool themeInitialized;
    private RadCarouselElement carouselElement;

    public RadCarousel()
    {
      if (!RadCarousel.themeInitialized)
      {
        RadCarousel.themeInitialized = true;
        this.LoadControlDefaultTheme();
      }
      Size defaultSize = this.DefaultSize;
      this.ElementTree.PerformInnerLayout(true, 0, 0, defaultSize.Width, defaultSize.Height);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.carouselElement = new RadCarouselElement();
      parent.Children.Add((RadElement) this.CarouselElement);
    }

    [Category("Layout")]
    [DefaultValue(true)]
    [Description("Enable or disable the re-animation of RadCarousel on form maximize, minimize or resize")]
    public virtual bool EnableAnimationOnFormResize
    {
      get
      {
        return this.CarouselElement.ItemsContainer.EnableAnimationOnFormResize;
      }
      set
      {
        this.CarouselElement.ItemsContainer.EnableAnimationOnFormResize = value;
      }
    }

    [Category("Layout")]
    [Browsable(false)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(240, 150));
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadCarouselElement CarouselElement
    {
      get
      {
        return this.carouselElement;
      }
    }

    [Description("Gets ot sets the number of animation frames between two positions")]
    [Browsable(true)]
    [DefaultValue(30)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Behavior")]
    public int AnimationFrames
    {
      get
      {
        return this.CarouselElement.AnimationFrames;
      }
      set
      {
        this.CarouselElement.AnimationFrames = value;
      }
    }

    [Description("Gets or sets the delay in ms. between two frames of animation")]
    [Category("Behavior")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(true)]
    [DefaultValue(40)]
    public int AnimationDelay
    {
      get
      {
        return this.CarouselElement.AnimationDelay;
      }
      set
      {
        this.CarouselElement.AnimationDelay = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating that the Carousel will loop items automatically.")]
    [Category("AutoLoopBehavior")]
    public bool EnableAutoLoop
    {
      get
      {
        return this.CarouselElement.CarouselItemContainer.EnableAutoLoop;
      }
      set
      {
        this.CarouselElement.CarouselItemContainer.EnableAutoLoop = value;
      }
    }

    [DefaultValue(AutoLoopDirections.Forward)]
    [Category("AutoLoopBehavior")]
    [Description("Gets or sets a value indicating whether carousel will increment or decrement item indexes when in auto-loop mode.")]
    public AutoLoopDirections AutoLoopDirection
    {
      get
      {
        return this.CarouselElement.CarouselItemContainer.AutoLoopDirection;
      }
      set
      {
        this.CarouselElement.CarouselItemContainer.AutoLoopDirection = value;
      }
    }

    [Category("AutoLoopBehavior")]
    [DefaultValue(AutoLoopPauseConditions.OnMouseOverCarousel)]
    [Description("Gets or sets a value indicating when carousel will pause looping if in auto-loop mode.")]
    public AutoLoopPauseConditions AutoLoopPauseCondition
    {
      get
      {
        return this.CarouselElement.CarouselItemContainer.AutoLoopPauseCondition;
      }
      set
      {
        this.CarouselElement.CarouselItemContainer.AutoLoopPauseCondition = value;
      }
    }

    [Category("AutoLoopBehavior")]
    [DefaultValue(3)]
    [Description("Gets or sets a value indicating the interval (in seconds) after which the carousel will resume looping when in auto-loop mode.")]
    public int AutoLoopPauseInterval
    {
      get
      {
        return this.CarouselElement.AutoLoopPauseInterval;
      }
      set
      {
        this.CarouselElement.AutoLoopPauseInterval = value;
      }
    }

    [AttributeProvider(typeof (IListSource))]
    [Description("Gets or sets the data source.")]
    [Category("Data")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(null)]
    public object DataSource
    {
      get
      {
        return this.CarouselElement.DataSource;
      }
      set
      {
        this.CarouselElement.DataSource = value;
      }
    }

    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadDescription("Items", typeof (RadCarouselElement))]
    [AttributeProvider("Telerik.WinControls.UI.RadCarouselElement", "Items")]
    [Category("Data")]
    public RadItemCollection Items
    {
      get
      {
        return this.carouselElement.Items;
      }
    }

    [AttributeProvider("Telerik.WinControls.UI.RadCarouselElement", "SelectedItem")]
    public virtual object SelectedItem
    {
      get
      {
        return this.CarouselElement.SelectedItem;
      }
      set
      {
        this.CarouselElement.SelectedItem = value;
      }
    }

    [Description("Gets or sets the currently selected item.")]
    [Category("Data")]
    [DefaultValue(0)]
    public virtual int SelectedIndex
    {
      get
      {
        return this.CarouselElement.SelectedIndex;
      }
      set
      {
        this.CarouselElement.SelectedIndex = value;
      }
    }

    [AttributeProvider("Telerik.WinControls.UI.RadCarouselElement", "SelectedValue")]
    public object SelectedValue
    {
      get
      {
        return this.CarouselElement.SelectedValue;
      }
      set
      {
        this.CarouselElement.SelectedValue = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the property to use as the actual value for the items.")]
    public string ValueMember
    {
      get
      {
        return this.CarouselElement.ValueMember;
      }
      set
      {
        this.CarouselElement.ValueMember = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether formatting is applied to the DisplayMember property.")]
    public bool FormattingEnabled
    {
      get
      {
        return this.CarouselElement.FormattingEnabled;
      }
      set
      {
        this.CarouselElement.FormattingEnabled = value;
      }
    }

    [Category("Behavior")]
    [Description("Number of items that carousel displays when VirtualMode is set to true")]
    [DefaultValue(10)]
    public int VisibleItemCount
    {
      get
      {
        return this.CarouselElement.CarouselItemContainer.VisibleItemCount;
      }
      set
      {
        this.CarouselElement.CarouselItemContainer.VisibleItemCount = value;
      }
    }

    [Category("Behavior")]
    [Description("Indicates the maximum number of items that will be displayed.")]
    [DefaultValue(true)]
    public bool VirtualMode
    {
      get
      {
        return this.CarouselElement.ItemsContainer.Virtualized;
      }
      set
      {
        this.CarouselElement.ItemsContainer.Virtualized = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(false)]
    [Description("Indicates whether items will cycle through carousel path")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool EnableLooping
    {
      get
      {
        return this.CarouselElement.ItemsContainer.EnableLooping;
      }
      set
      {
        this.CarouselElement.ItemsContainer.EnableLooping = value;
      }
    }

    [Editor(typeof (CarouselPathEditor), typeof (UITypeEditor))]
    [Description("Gets or sets the path which items will follow when animated.")]
    [TypeConverter(typeof (CarouselPathConverter))]
    [Category("Behavior")]
    public CarouselParameterPath CarouselPath
    {
      get
      {
        return (CarouselParameterPath) this.CarouselElement.ItemsContainer.CarouselPath;
      }
      set
      {
        bool enableRelativePath = this.EnableRelativePath;
        this.CarouselElement.ItemsContainer.CarouselPath = (ICarouselPath) value;
        this.EnableRelativePath = enableRelativePath;
      }
    }

    [Category("Behavior")]
    [Description("Get or set using the relative point coordinate.If set to true each point should be between 0 and 100")]
    [DefaultValue(true)]
    public bool EnableRelativePath
    {
      set
      {
        if (this.CarouselElement.ItemsContainer.CarouselPath == null)
          return;
        CarouselParameterPath carouselPath = (CarouselParameterPath) this.CarouselElement.ItemsContainer.CarouselPath;
        if (carouselPath.EnableRelativePath == value)
          return;
        carouselPath.EnableRelativePath = value;
        this.OnNotifyPropertyChanged(nameof (EnableRelativePath));
      }
      get
      {
        if (this.CarouselElement.ItemsContainer.CarouselPath == null)
          return true;
        return ((CarouselParameterPath) this.CarouselElement.ItemsContainer.CarouselPath).EnableRelativePath;
      }
    }

    [Category("Behavior")]
    [Description("Sets the way opacity is applied to carousel items.")]
    [DefaultValue(OpacityChangeConditions.ZIndex)]
    public OpacityChangeConditions OpacityChangeCondition
    {
      get
      {
        if (this.CarouselElement.ItemsContainer.CarouselPath == null)
          return OpacityChangeConditions.ZIndex;
        return this.CarouselElement.ItemsContainer.OpacityChangeCondition;
      }
      set
      {
        if (this.CarouselElement.ItemsContainer.CarouselPath == null)
          return;
        this.CarouselElement.ItemsContainer.OpacityChangeCondition = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(0.33)]
    [Description("Indicates the minimum value of the opacity applied to items")]
    public double MinFadeOpacity
    {
      set
      {
        if (this.CarouselElement.ItemsContainer.CarouselPath == null)
          return;
        this.CarouselElement.ItemsContainer.MinFadeOpacity = value;
      }
      get
      {
        if (this.CarouselElement.ItemsContainer.CarouselPath == null)
          return 0.0;
        return this.CarouselElement.ItemsContainer.MinFadeOpacity;
      }
    }

    [DefaultValue(RadEasingType.OutQuad)]
    [Description("Gets or sets the easing equation to be used for the animations.")]
    [Category("Behavior")]
    public RadEasingType EasingType
    {
      get
      {
        return this.CarouselElement.ItemsContainer.EasingType;
      }
      set
      {
        this.CarouselElement.ItemsContainer.EasingType = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the type of animation to be applied to carousel items")]
    [DefaultValue(Animations.All)]
    public Animations AnimationsToApply
    {
      get
      {
        return this.CarouselElement.ItemsContainer.AnimationsApplied;
      }
      set
      {
        this.CarouselElement.ItemsContainer.AnimationsApplied = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the action to be performed when a carousel item is clicked")]
    [DefaultValue(CarouselItemClickAction.SelectItem)]
    public CarouselItemClickAction ItemClickDefaultAction
    {
      get
      {
        return this.CarouselElement.ItemClickDefaultAction;
      }
      set
      {
        this.CarouselElement.ItemClickDefaultAction = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(0.333)]
    public double ItemReflectionPercentage
    {
      get
      {
        return this.CarouselElement.ItemReflectionPercentage;
      }
      set
      {
        this.CarouselElement.ItemReflectionPercentage = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Represents the button moving the items back one at a time")]
    [Category("Appearance")]
    public RadRepeatButtonElement ButtonPrevious
    {
      get
      {
        return this.carouselElement.ButtonPrevious;
      }
    }

    [Category("Appearance")]
    [Description("Represents the button moving the items forward one at a time")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadRepeatButtonElement ButtonNext
    {
      get
      {
        return this.carouselElement.ButtonNext;
      }
    }

    [Category("Appearance")]
    [DefaultValue(typeof (Size), "0,0")]
    [Description("Represents the navigation buttons offset")]
    public virtual Size NavigationButtonsOffset
    {
      get
      {
        return this.carouselElement.NavigationButtonsOffset;
      }
      set
      {
        this.carouselElement.NavigationButtonsOffset = value;
      }
    }

    [DefaultValue(NavigationButtonsPosition.Bottom)]
    [Description("Gets or sets the location of the navigation buttons")]
    [Category("Appearance")]
    public virtual NavigationButtonsPosition ButtonPositions
    {
      get
      {
        return this.carouselElement.ButtonPositions;
      }
      set
      {
        this.carouselElement.ButtonPositions = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Occurs when new databound carousel item is created.")]
    public event NewCarouselItemCreatingEventHandler NewCarouselItemCreating
    {
      add
      {
        this.CarouselElement.AddEventHandler(RadCarouselElement.NewCarouselItemCreatingEventKey, (Delegate) value);
      }
      remove
      {
        this.CarouselElement.RemoveEventHandler(RadCarouselElement.NewCarouselItemCreatingEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [Description("Occurs after an Item is databound.")]
    public event ItemDataBoundEventHandler ItemDataBound
    {
      add
      {
        this.CarouselElement.AddEventHandler(RadCarouselElement.ItemDataBoundEventKey, (Delegate) value);
      }
      remove
      {
        this.CarouselElement.RemoveEventHandler(RadCarouselElement.ItemDataBoundEventKey, (Delegate) value);
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Occurs when the SelectedIndex property has changed.")]
    public event EventHandler SelectedIndexChanged
    {
      add
      {
        this.CarouselElement.AddEventHandler(RadCarouselElement.SelectedIndexChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.CarouselElement.RemoveEventHandler(RadCarouselElement.SelectedIndexChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the SelectedValue property has changed.")]
    [Category("Behavior")]
    [Browsable(true)]
    public event EventHandler SelectedValueChanged
    {
      add
      {
        this.CarouselElement.AddEventHandler(RadCarouselElement.SelectedValueChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.CarouselElement.RemoveEventHandler(RadCarouselElement.SelectedValueChangedEventKey, (Delegate) value);
      }
    }

    [Category("Behavior")]
    [Description("Occurs when the selected items is changed.")]
    [Browsable(true)]
    public event EventHandler SelectedItemChanged
    {
      add
      {
        this.CarouselElement.AddEventHandler(RadCarouselElement.SelectedItemChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.CarouselElement.RemoveEventHandler(RadCarouselElement.SelectedItemChangedEventKey, (Delegate) value);
      }
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
    }

    private void LoadControlDefaultTheme()
    {
      Theme theme = new Theme("ControlDefault");
      StyleGroup styleGroup = new StyleGroup();
      styleGroup.Registrations.Add(new StyleRegistration());
      styleGroup.Registrations[0].RegistrationType = "ElementTypeControlType";
      styleGroup.Registrations[0].ElementType = "Telerik.WinControls.RootRadElement";
      styleGroup.Registrations[0].ControlType = "Telerik.WinControls.UI.RadCarousel";
      theme.StyleGroups.Add(styleGroup);
      styleGroup.PropertySettingGroups.Add(new PropertySettingGroup()
      {
        Selector = new ElementSelector(ElementSelectorTypes.ClassSelector, "PreviousButton"),
        PropertySettings = {
          new PropertySetting("BackColor", (object) Color.Transparent),
          new PropertySetting("DisplayStyle", (object) DisplayStyle.Image),
          new PropertySetting("Image", (object) Telerik\u002EWinControls\u002EUI\u002EResources.previousButton)
        }
      });
      styleGroup.PropertySettingGroups.Add(new PropertySettingGroup()
      {
        Selector = new ElementSelector(ElementSelectorTypes.ClassSelector, "NextButton"),
        PropertySettings = {
          new PropertySetting("BackColor", (object) Color.Transparent),
          new PropertySetting("DisplayStyle", (object) DisplayStyle.Image),
          new PropertySetting("Image", (object) Telerik\u002EWinControls\u002EUI\u002EResources.nextButton)
        }
      });
      PropertySettingGroup propertySettingGroup1 = new PropertySettingGroup() { Selector = new ElementSelector(ElementSelectorTypes.ClassSelector, "NextButton") };
      propertySettingGroup1.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.ClassSelector, "ButtonFill");
      propertySettingGroup1.PropertySettings.Add(new PropertySetting("Visibility", (object) ElementVisibility.Hidden));
      styleGroup.PropertySettingGroups.Add(propertySettingGroup1);
      PropertySettingGroup propertySettingGroup2 = new PropertySettingGroup() { Selector = new ElementSelector(ElementSelectorTypes.ClassSelector, "NextButton") };
      propertySettingGroup2.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.ClassSelector, "ButtonBorder");
      propertySettingGroup2.PropertySettings.Add(new PropertySetting("Visibility", (object) ElementVisibility.Collapsed));
      styleGroup.PropertySettingGroups.Add(propertySettingGroup2);
      PropertySettingGroup propertySettingGroup3 = new PropertySettingGroup() { Selector = new ElementSelector(ElementSelectorTypes.ClassSelector, "PreviousButton") };
      propertySettingGroup3.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.ClassSelector, "ButtonFill");
      propertySettingGroup3.PropertySettings.Add(new PropertySetting("Visibility", (object) ElementVisibility.Hidden));
      styleGroup.PropertySettingGroups.Add(propertySettingGroup3);
      PropertySettingGroup propertySettingGroup4 = new PropertySettingGroup() { Selector = new ElementSelector(ElementSelectorTypes.ClassSelector, "PreviousButton") };
      propertySettingGroup4.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.ClassSelector, "ButtonBorder");
      propertySettingGroup4.PropertySettings.Add(new PropertySetting("Visibility", (object) ElementVisibility.Collapsed));
      styleGroup.PropertySettingGroups.Add(propertySettingGroup4);
      PropertySettingGroup propertySettingGroup5 = new PropertySettingGroup() { Selector = new ElementSelector(ElementSelectorTypes.VisualStateSelector, "CarouselGenericItem") };
      propertySettingGroup5.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.TypeSelector, "Telerik.WinControls.Primitives.TextPrimitive");
      propertySettingGroup5.PropertySettings.Add(new PropertySetting("ForeColor", (object) Color.Transparent));
      styleGroup.PropertySettingGroups.Add(propertySettingGroup5);
      PropertySettingGroup propertySettingGroup6 = new PropertySettingGroup() { Selector = new ElementSelector(ElementSelectorTypes.VisualStateSelector, "CarouselGenericItem") };
      propertySettingGroup6.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.TypeSelector, "Telerik.WinControls.Primitives.BorderPrimitive");
      propertySettingGroup6.PropertySettings.Add(new PropertySetting("ForeColor", (object) Color.Transparent));
      propertySettingGroup6.PropertySettings.Add(new PropertySetting("GradientStyle", (object) GradientStyles.Solid));
      propertySettingGroup6.PropertySettings.Add(new PropertySetting("BoxSyle", (object) BorderBoxStyle.SingleBorder));
      styleGroup.PropertySettingGroups.Add(propertySettingGroup6);
      PropertySettingGroup propertySettingGroup7 = new PropertySettingGroup() { Selector = new ElementSelector(ElementSelectorTypes.VisualStateSelector, "CarouselGenericItem") };
      propertySettingGroup7.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.TypeSelector, "Telerik.WinControls.Primitives.FillPrimitive");
      propertySettingGroup7.PropertySettings.Add(new PropertySetting("BackColor", (object) Color.Transparent));
      propertySettingGroup7.PropertySettings.Add(new PropertySetting("ForeColor2", (object) Color.Transparent));
      propertySettingGroup7.PropertySettings.Add(new PropertySetting("GradientStyle", (object) GradientStyles.Radial));
      propertySettingGroup7.PropertySettings.Add(new PropertySetting("ZIndex", (object) 10));
      propertySettingGroup7.PropertySettings.Add(new PropertySetting("Visibility", (object) ElementVisibility.Collapsed));
      styleGroup.PropertySettingGroups.Add(propertySettingGroup7);
      PropertySettingGroup propertySettingGroup8 = new PropertySettingGroup() { Selector = new ElementSelector(ElementSelectorTypes.VisualStateSelector, "CarouselGenericItem.MouseOver") };
      propertySettingGroup8.Selector.ChildSelector = new ElementSelector(ElementSelectorTypes.TypeSelector, "Telerik.WinControls.Primitives.FillPrimitive");
      propertySettingGroup8.PropertySettings.Add(new PropertySetting("Visibility", (object) ElementVisibility.Visible));
      propertySettingGroup8.PropertySettings.Add(new PropertySetting()
      {
        Name = "BackColor",
        EndValue = (object) Color.Transparent
      });
      propertySettingGroup8.PropertySettings.Add(new PropertySetting()
      {
        Name = "BackColor2",
        EndValue = (object) Color.FromArgb(120, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)
      });
      propertySettingGroup8.PropertySettings.Add(new PropertySetting("GradientStyle", (object) GradientStyles.Radial));
      propertySettingGroup8.PropertySettings.Add(new PropertySetting("GradientAngle", (object) 90f));
      propertySettingGroup8.PropertySettings.Add(new PropertySetting("GradientPercentage", (object) 0.5f));
      propertySettingGroup8.PropertySettings.Add(new PropertySetting("GradientPercentage2", (object) 0.666f));
      propertySettingGroup8.PropertySettings.Add(new PropertySetting("NumberOfColors", (object) 2));
      styleGroup.PropertySettingGroups.Add(propertySettingGroup8);
      ThemeRepository.Add(theme);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element == this.ButtonNext || element == this.ButtonPrevious)
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    [Description("Gets or sets a value indicating whether the keyboard navigation is enabled.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    public virtual bool EnableKeyboardNavigation
    {
      get
      {
        return this.carouselElement.EnableKeyboardNavigation;
      }
      set
      {
        this.carouselElement.EnableKeyboardNavigation = value;
      }
    }

    protected override RadElement GetInputElement()
    {
      return (RadElement) this.carouselElement;
    }
  }
}
