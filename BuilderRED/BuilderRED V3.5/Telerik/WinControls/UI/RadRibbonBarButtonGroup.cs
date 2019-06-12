// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBarButtonGroup
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
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [Designer("Telerik.WinControls.UI.Design.RadRibbonBarButtonGroupDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(false)]
  public class RadRibbonBarButtonGroup : CollapsibleElement, IItemsElement
  {
    public static RoutedEvent OnRoutedButtonSelected = RadElement.RegisterRoutedEvent(nameof (OnRoutedButtonSelected), typeof (RadRibbonBarButtonGroup));
    public static RoutedEvent OnRoutedButtonDeselected = RadElement.RegisterRoutedEvent(nameof (OnRoutedButtonDeselected), typeof (RadRibbonBarButtonGroup));
    public static RadProperty IsSelectedProperty = RadProperty.Register("IsSelected", typeof (bool), typeof (RadRibbonBarButtonGroup), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (RadRibbonBarButtonGroup), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));
    internal static RadProperty InternalOrientationProperty = RadProperty.Register("InternalOrientation", typeof (Orientation), typeof (RadRibbonBarButtonGroup), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.CanInheritValue));
    internal static RadProperty IsItemAtEndIndexProperty = RadProperty.Register("IsItemAtEndIndex", typeof (bool), typeof (RadRibbonBarButtonGroup), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private BorderPrimitive borderPrimitive;
    private RadItemOwnerCollection items;
    private StackLayoutPanel layoutPanel;
    private FillPrimitive fillPrimitive;

    [Description("Gets the collection of items in the button group.")]
    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadNewItem("", false, false, false)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the orientation of the elements inside the button group: Horizontal or Vertical.")]
    [DefaultValue(Orientation.Horizontal)]
    public Orientation Orientation
    {
      get
      {
        return (Orientation) this.GetValue(RadRibbonBarButtonGroup.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadRibbonBarButtonGroup.OrientationProperty, (object) value);
      }
    }

    [DefaultValue(true)]
    [Browsable(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the border is shown.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ShowBorder
    {
      get
      {
        return this.borderPrimitive.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.borderPrimitive.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
        foreach (RadItem radItem in (RadItemCollection) this.Items)
        {
          if ((object) radItem.GetType() == (object) typeof (RadButtonElement))
            this.SynchShowBorder((RadButtonElement) radItem);
        }
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the back color is shown.")]
    [Category("Behavior")]
    public bool ShowBackColor
    {
      get
      {
        return this.fillPrimitive.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.fillPrimitive.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [DefaultValue(typeof (Size), "22, 22")]
    public override Size MinSize
    {
      get
      {
        return base.MinSize;
      }
      set
      {
        base.MinSize = value;
      }
    }

    internal StackLayoutPanel LayoutPanel
    {
      get
      {
        return this.layoutPanel;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Shape = (ElementShape) new RoundRectShape(3);
      this.StretchVertically = false;
    }

    protected override void CreateChildElements()
    {
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "ButtonGroupBorder";
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "ButtonGroupFill";
      this.layoutPanel = new StackLayoutPanel();
      this.items = new RadItemOwnerCollection((RadElement) this.layoutPanel);
      this.items.ItemsChanged += new ItemChangedDelegate(this.ItemChanged);
      this.items.ItemTypes = new System.Type[16]
      {
        typeof (RadButtonElement),
        typeof (RadCheckBoxElement),
        typeof (RadDropDownListElement),
        typeof (RadDropDownButtonElement),
        typeof (RadImageButtonElement),
        typeof (RadLabelElement),
        typeof (RadMaskedEditBoxElement),
        typeof (RadProgressBarElement),
        typeof (RadRadioButtonElement),
        typeof (RadRepeatButtonElement),
        typeof (RadRibbonBarButtonGroup),
        typeof (RadSplitButtonElement),
        typeof (RadTextBoxElement),
        typeof (RadToggleButtonElement),
        typeof (RadTrackBarElement),
        typeof (RibbonBarGroupSeparator)
      };
      int num = (int) this.layoutPanel.BindProperty(StackLayoutPanel.OrientationProperty, (RadObject) this, RadRibbonBarButtonGroup.OrientationProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.layoutPanel);
      this.Children.Add((RadElement) this.borderPrimitive);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadRibbonBarButtonGroup.OrientationProperty)
        return;
      int num1 = (int) this.SetDefaultValueOverride(RadRibbonBarButtonGroup.InternalOrientationProperty, e.NewValue);
      foreach (RadItem radItem in (RadItemCollection) this.items)
      {
        if (radItem is RibbonBarGroupSeparator)
        {
          int num2 = (int) radItem.SetDefaultValueOverride(RibbonBarGroupSeparator.OrientationProperty, e.NewValue);
        }
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (this.items.Count <= 0)
        return;
      if (this.items[0] is RadButtonElement && this.Orientation == Orientation.Horizontal)
      {
        int num = (int) (this.items[0] as RadButtonElement).BorderElement.SetDefaultValueOverride(BorderPrimitive.LeftWidthProperty, (object) 0.0f);
      }
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].GetType().Name == "RadButtonElement")
        {
          this.items[index].Shape = (ElementShape) null;
          this.SynchShowBorder(this.items[index] as RadButtonElement);
        }
      }
    }

    protected virtual void ResetItemsIsAtUnevenIndexProperty()
    {
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].GetType().Equals(typeof (RadButtonElement)))
        {
          RadButtonElement radButtonElement = this.items[index] as RadButtonElement;
          if ((object) radButtonElement.StateManager.GetType() != (object) typeof (ItemInButtonGroupStateManager))
            radButtonElement.StateManager = new ItemInButtonGroupStateManager().StateManagerInstance;
          int num = (int) radButtonElement.SetDefaultValueOverride(RadRibbonBarButtonGroup.IsItemAtEndIndexProperty, (object) (index + 1 == this.items.Count));
        }
      }
    }

    public void ItemChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation == ItemsChangeOperation.Inserted || operation == ItemsChangeOperation.Removed)
        this.ResetItemsIsAtUnevenIndexProperty();
      this.invalidateCollapsableChildrenCollection = true;
      if (operation != ItemsChangeOperation.Inserted && operation != ItemsChangeOperation.Set)
        return;
      target.NotifyParentOnMouseInput = true;
      if ((object) target.GetType() == (object) typeof (RadButtonElement))
      {
        RadButtonElement target1 = target as RadButtonElement;
        target1.ButtonFillElement.Class = "ButtonInRibbonFill";
        target1.ThemeRole = "ButtonInRibbonButtonGroup";
        this.SynchShowBorder(target1);
        if (this.ElementTree != null)
          this.ElementTree.ApplyThemeToElement((RadObject) target1);
      }
      if ((object) target.GetType() == (object) typeof (RibbonBarGroupSeparator))
      {
        int num1 = (int) target.SetDefaultValueOverride(RibbonBarGroupSeparator.OrientationProperty, (object) this.Orientation);
      }
      if (target is RadRibbonBarButtonGroup)
      {
        int num2 = (int) target.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(22, 22));
      }
      if (target is RadDropDownListElement)
      {
        int num3 = (int) target.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(140, 0));
        int num4 = (int) target.SetDefaultValueOverride(RadElement.AutoSizeModeProperty, (object) RadAutoSizeMode.WrapAroundChildren);
      }
      if (target is RadTextBoxElement)
      {
        RadTextBoxElement radTextBoxElement = (RadTextBoxElement) target;
        int num3 = (int) target.SetDefaultValueOverride(RadElement.MinSizeProperty, (object) new Size(140, 0));
        int num4 = (int) target.SetDefaultValueOverride(RadElement.AutoSizeModeProperty, (object) RadAutoSizeMode.WrapAroundChildren);
        Padding padding = radTextBoxElement.Padding;
        padding.Left = 0;
        padding.Right = 0;
        radTextBoxElement.Padding = padding;
        int num5 = (int) radTextBoxElement.SetDefaultValueOverride(RadElement.PaddingProperty, (object) padding);
      }
      if (target is RadGalleryElement)
      {
        int num6 = (int) target.SetDefaultValueOverride(RadElement.PaddingProperty, (object) new Padding(2, 2, 0, 0));
      }
      if (target is RadSplitButtonElement)
      {
        int num3 = (int) target.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
        int num4 = (int) target.Children[0].SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
      }
      if (target is RadCheckBoxElement)
      {
        int num7 = (int) target.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) false);
      }
      if (!(target is RadRadioButtonElement))
        return;
      int num8 = (int) target.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) false);
      int num9 = (int) target.SetDefaultValueOverride(RadElement.AutoSizeModeProperty, (object) RadAutoSizeMode.WrapAroundChildren);
    }

    private void SynchShowBorder(RadButtonElement target)
    {
      RadElement borderElement = (RadElement) target.BorderElement;
      if (this.ShowBorder)
      {
        target.Class = "ButtonElement";
        if (borderElement != null)
          borderElement.Class = "ButtonInGroupBorder";
        if (this.ElementTree == null)
          return;
        this.ElementTree.ApplyThemeToElement((RadObject) target);
        if (borderElement.Visibility == ElementVisibility.Visible)
          return;
        int num = (int) borderElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      else
      {
        target.Class = "RibbonBarButtonElement";
        if (borderElement == null)
          return;
        borderElement.Class = "ButtonInRibbonBorder";
        if (this.ElementTree == null)
          return;
        this.ElementTree.ApplyThemeToElement((RadObject) target);
        if (borderElement.Visibility != ElementVisibility.Visible)
          return;
        int num = (int) borderElement.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Hidden);
      }
    }

    public void ItemClicked(RadItem item)
    {
      if (this.GetIsSelected((RadElement) item) || !this.CanSelectButton(item))
        return;
      this.SetSelectedItem((RadElement) item);
    }

    public void SetSelectedItem(RadElement element)
    {
      this.SetIsSelected(element, true);
      this.RefreshItems(element);
    }

    protected virtual bool CanSelectButton(RadItem item)
    {
      return true;
    }

    public virtual void RefreshItems(RadElement element)
    {
      foreach (RadElement childElement in (RadItemCollection) this.Items)
      {
        if (childElement != element)
          this.SetIsSelected(childElement, false);
      }
    }

    public void SetIsSelected(RadElement childElement, bool value)
    {
      if (this.GetIsSelected(childElement) == value)
        return;
      int num = (int) childElement.SetValue(RadRibbonBarButtonGroup.IsSelectedProperty, (object) value);
      this.RaiseButtonSelected(childElement);
    }

    public bool GetIsSelected(RadElement childElement)
    {
      return (bool) childElement.GetValue(RadRibbonBarButtonGroup.IsSelectedProperty);
    }

    private void RaiseButtonSelected(RadElement button)
    {
      if (this.GetIsSelected(button))
        this.RaiseRoutedEvent(new RoutedEventArgs(EventArgs.Empty, RadRibbonBarButtonGroup.OnRoutedButtonSelected), button);
      else
        this.RaiseRoutedEvent(new RoutedEventArgs(EventArgs.Empty, RadRibbonBarButtonGroup.OnRoutedButtonDeselected), button);
    }

    private void RaiseRoutedEvent(RoutedEventArgs args, RadElement button)
    {
      button.RaiseTunnelEvent(button, args);
      if (args.Canceled)
        return;
      this.RaiseBubbleEvent((RadElement) this, args);
    }

    public override bool ExpandElementToStep(int nextStep)
    {
      this.InvalidateIfNeeded();
      return this.ExpandCollection(nextStep);
    }

    public override bool CollapseElementToStep(int nextStep)
    {
      this.InvalidateIfNeeded();
      return this.CollapseCollection(nextStep);
    }

    public override bool CanExpandToStep(int step)
    {
      if (step > this.CollapseStep)
        return false;
      this.InvalidateIfNeeded();
      if (this.Orientation == Orientation.Vertical)
      {
        if (this.collapsableChildren.Count == 0)
          return false;
        bool flag = step > 0;
        foreach (CollapsibleElement collapsableChild in (IEnumerable<CollapsibleElement>) this.collapsableChildren)
          flag |= collapsableChild.CanExpandToStep(step);
        return flag;
      }
      if (this.collapsableChildren.Count <= 0)
        return false;
      bool flag1 = step <= this.CollapseMaxSteps;
      foreach (CollapsibleElement collapsableChild in (IEnumerable<CollapsibleElement>) this.collapsableChildren)
        flag1 |= collapsableChild.CanCollapseToStep(step);
      return flag1;
    }

    public override bool CanCollapseToStep(int step)
    {
      if (step < this.CollapseStep)
        return false;
      this.InvalidateIfNeeded();
      if (this.Orientation == Orientation.Vertical)
      {
        if (this.collapsableChildren.Count == 0)
          return false;
        bool flag = false;
        foreach (CollapsibleElement collapsableChild in (IEnumerable<CollapsibleElement>) this.collapsableChildren)
          flag |= collapsableChild.CanCollapseToStep(step);
        return flag;
      }
      if (this.collapsableChildren.Count <= 0)
        return false;
      bool flag1 = false;
      foreach (CollapsibleElement collapsableChild in (IEnumerable<CollapsibleElement>) this.collapsableChildren)
        flag1 |= collapsableChild.CanCollapseToStep(step);
      return flag1;
    }

    public override int CollapseMaxSteps
    {
      get
      {
        return 3;
      }
    }
  }
}
