// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewStripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadPageViewStripElement : RadPageViewElement
  {
    public static RadProperty ShowItemPinButtonProperty = RadProperty.Register(nameof (ShowItemPinButton), typeof (bool), typeof (RadPageViewStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty NewItemVisibilityProperty = RadProperty.Register(nameof (NewItemVisibility), typeof (StripViewNewItemVisibility), typeof (RadPageViewStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StripViewNewItemVisibility.Hidden, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AnimatedStripScrollingProperty = RadProperty.Register(nameof (AnimatedStripScrolling), typeof (bool), typeof (RadPageViewStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.None));
    public static RadProperty StripScrollingAnimationProperty = RadProperty.Register(nameof (StripScrollingAnimation), typeof (RadEasingType), typeof (RadPageViewStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadEasingType.InOutQuad, ElementPropertyOptions.None));
    public static RadProperty StripButtonsProperty = RadProperty.Register(nameof (StripButtons), typeof (StripViewButtons), typeof (RadPageViewStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StripViewButtons.Auto, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty StripAlignmentProperty = RadProperty.Register(nameof (StripAlignment), typeof (StripViewAlignment), typeof (RadPageViewStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StripViewAlignment.Top, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemFitModeProperty = RadProperty.Register(nameof (ItemFitMode), typeof (StripViewItemFitMode), typeof (RadPageViewStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StripViewItemFitMode.None, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ItemAlignmentProperty = RadProperty.Register(nameof (ItemAlignment), typeof (StripViewItemAlignment), typeof (RadPageViewStripElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StripViewItemAlignment.Near, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private StripViewItemContainer itemContainer;
    private RadPageViewStripNewItem newItem;
    private RadPageViewItem previewItem;

    static RadPageViewStripElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new StripViewElementStateManager(), typeof (RadPageViewStripElement));
    }

    protected override void CreateChildElements()
    {
      this.itemContainer = this.CreateItemContainer();
      this.Children.Add((RadElement) this.itemContainer);
      this.newItem = new RadPageViewStripNewItem();
      this.newItem.Owner = (RadPageViewElement) this;
      base.CreateChildElements();
    }

    protected virtual StripViewItemContainer CreateItemContainer()
    {
      return new StripViewItemContainer();
    }

    internal override PageViewLayoutInfo ItemLayoutInfo
    {
      get
      {
        return (PageViewLayoutInfo) this.itemContainer.ItemLayout.LayoutInfo;
      }
    }

    protected override RadElement ItemsParent
    {
      get
      {
        return (RadElement) this.itemContainer.ItemLayout;
      }
    }

    [Browsable(false)]
    public RadPageViewStripItem NewItem
    {
      get
      {
        return (RadPageViewStripItem) this.newItem;
      }
    }

    [Browsable(false)]
    public RadPageViewItem PreviewItem
    {
      get
      {
        return this.previewItem;
      }
      set
      {
        if (this.previewItem == value)
          return;
        if (this.previewItem != null)
          this.previewItem.IsPreview = false;
        this.previewItem = value;
        if (this.previewItem != null)
          this.previewItem.IsPreview = true;
        this.InvalidateMeasure(true);
        this.OnNotifyPropertyChanged(nameof (PreviewItem));
      }
    }

    [Description("Determines if the PinButton will be displayed in each item, allowing that item to be pinned.")]
    [Category("Behavior")]
    [RadPropertyDefaultValue("ShowItemPinButton", typeof (RadPageViewStripElement))]
    public bool ShowItemPinButton
    {
      get
      {
        return (bool) this.GetValue(RadPageViewStripElement.ShowItemPinButtonProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripElement.ShowItemPinButtonProperty, (object) value);
      }
    }

    [Description("Gets or sets the visibility of the internal NewItem.")]
    [Category("Behavior")]
    public virtual StripViewNewItemVisibility NewItemVisibility
    {
      get
      {
        return (StripViewNewItemVisibility) this.GetValue(RadPageViewStripElement.NewItemVisibilityProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripElement.NewItemVisibilityProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("AnimatedStripScrolling", typeof (RadPageViewStripElement))]
    [Category("Behavior")]
    [Description("Determines whether strip scrolling will be animated.")]
    public bool AnimatedStripScrolling
    {
      get
      {
        return (bool) this.GetValue(RadPageViewStripElement.AnimatedStripScrollingProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripElement.AnimatedStripScrollingProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("StripScrollingAnimation", typeof (RadPageViewStripElement))]
    [Category("Behavior")]
    [Description("Gets or sets the easing type of the strip scroll animation.")]
    public RadEasingType StripScrollingAnimation
    {
      get
      {
        return (RadEasingType) this.GetValue(RadPageViewStripElement.StripScrollingAnimationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripElement.StripScrollingAnimationProperty, (object) value);
      }
    }

    [Browsable(false)]
    public StripViewItemContainer ItemContainer
    {
      get
      {
        return this.itemContainer;
      }
    }

    [Category("Behavior")]
    [RadPropertyDefaultValue("StripButtons", typeof (RadPageViewStripElement))]
    [Description("Determines scroll buttons' visibility.")]
    public StripViewButtons StripButtons
    {
      get
      {
        return (StripViewButtons) this.GetValue(RadPageViewStripElement.StripButtonsProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripElement.StripButtonsProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Determines the alignment of items within the strip layout.")]
    [RadPropertyDefaultValue("ItemAlignment", typeof (RadPageViewStripElement))]
    public StripViewItemAlignment ItemAlignment
    {
      get
      {
        return (StripViewItemAlignment) this.GetValue(RadPageViewStripElement.ItemAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripElement.ItemAlignmentProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ItemFitMode", typeof (RadPageViewStripElement))]
    [Description("Determines the fit mode to be applied when measuring child items.")]
    [Category("Appearance")]
    public StripViewItemFitMode ItemFitMode
    {
      get
      {
        return (StripViewItemFitMode) this.GetValue(RadPageViewStripElement.ItemFitModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripElement.ItemFitModeProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("StripAlignment", typeof (RadPageViewStripElement))]
    [Description("Gets or sets the alignment of item strip within the view.")]
    public StripViewAlignment StripAlignment
    {
      get
      {
        return (StripViewAlignment) this.GetValue(RadPageViewStripElement.StripAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStripElement.StripAlignmentProperty, (object) value);
      }
    }

    public MultiLineItemFitMode MultiLineItemFitMode
    {
      get
      {
        return this.ItemContainer.ItemLayout.MultiLineItemFitMode;
      }
      set
      {
        this.ItemContainer.ItemLayout.MultiLineItemFitMode = value;
      }
    }

    public override RectangleF GetItemsRect()
    {
      return (RectangleF) this.itemContainer.ItemLayout.ControlBoundingRectangle;
    }

    protected override RadPageViewItem CreateItem()
    {
      return (RadPageViewItem) new RadPageViewStripItem();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadPageViewStripElement.StripAlignmentProperty)
      {
        this.UpdateItemOrientation((IEnumerable) this.Items);
        this.UpdateItemContainer((StripViewAlignment) e.NewValue);
        if (this.SelectedItem != null && this.EnsureSelectedItemVisible)
          this.EnsureItemVisible(this.SelectedItem);
      }
      else if (e.Property == RadPageViewStripElement.StripScrollingAnimationProperty)
        this.itemContainer.ItemLayout.SetScrollAnimation((RadEasingType) e.NewValue);
      else if (e.Property == RadPageViewStripElement.AnimatedStripScrollingProperty)
        this.itemContainer.ItemLayout.EnableScrolling((bool) e.NewValue);
      else if (e.Property == RadPageViewStripElement.StripButtonsProperty)
      {
        if ((StripViewButtons) e.NewValue == StripViewButtons.None)
          this.itemContainer.ButtonsPanel.Visibility = ElementVisibility.Collapsed;
        else
          this.itemContainer.ButtonsPanel.Visibility = ElementVisibility.Visible;
      }
      else if (e.Property == RadPageViewStripElement.NewItemVisibilityProperty)
        this.UpdateNewItem();
      if (!RadPageViewStripElement.PropertyInvalidatesScrollOffset(e.Property) || !this.EnsureSelectedItemVisible || this.SelectedItem == null)
        return;
      this.itemContainer.ItemLayout.EnsureVisible(this.SelectedItem);
    }

    protected internal override PageViewContentOrientation GetAutomaticItemOrientation(
      bool content)
    {
      switch (this.StripAlignment)
      {
        case StripViewAlignment.Right:
          return PageViewContentOrientation.Vertical90;
        case StripViewAlignment.Bottom:
          return !content ? PageViewContentOrientation.Horizontal180 : PageViewContentOrientation.Horizontal;
        case StripViewAlignment.Left:
          return PageViewContentOrientation.Vertical270;
        default:
          return PageViewContentOrientation.Horizontal;
      }
    }

    protected override bool EnsureItemVisibleCore(RadPageViewItem item)
    {
      return this.itemContainer.ItemLayout.EnsureVisible(item);
    }

    protected internal override bool IsNextKey(Keys key)
    {
      switch (this.StripAlignment)
      {
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          return key == Keys.Down;
        default:
          return base.IsNextKey(key);
      }
    }

    protected internal override bool IsPreviousKey(Keys key)
    {
      switch (this.StripAlignment)
      {
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          return key == Keys.Up;
        default:
          return base.IsPreviousKey(key);
      }
    }

    protected internal override void StartItemDrag(RadPageViewItem item)
    {
      base.StartItemDrag(item);
      this.itemContainer.ItemLayout.EnableScrolling(false);
    }

    protected internal override void EndItemDrag(RadPageViewItem item)
    {
      base.EndItemDrag(item);
      this.itemContainer.ItemLayout.EnableScrolling(this.AnimatedStripScrolling);
    }

    protected override void UpdateItemOrientation(IEnumerable items)
    {
      base.UpdateItemOrientation(items);
      base.UpdateItemOrientation((IEnumerable) new RadPageViewItem[1]
      {
        (RadPageViewItem) this.newItem
      });
    }

    protected override void AddItemCore(RadPageViewItem item)
    {
      base.AddItemCore(item);
      this.ItemsParent.SuspendLayout();
      if (this.newItem.Parent == this.ItemsParent)
        this.ItemsParent.Children.Remove((RadElement) this.newItem);
      switch (this.NewItemVisibility)
      {
        case StripViewNewItemVisibility.Front:
          this.ItemsParent.Children.Insert(0, (RadElement) this.newItem);
          break;
        case StripViewNewItemVisibility.End:
          this.ItemsParent.Children.Add((RadElement) this.newItem);
          break;
      }
      this.ItemsParent.ResumeLayout(false);
    }

    protected internal override void CloseItem(RadPageViewItem item)
    {
      if (item == this.newItem)
        return;
      base.CloseItem(item);
    }

    protected override bool CanDropOverItem(RadPageViewItem dragItem, RadPageViewItem hitItem)
    {
      if (hitItem == this.newItem)
        return false;
      return base.CanDropOverItem(dragItem, hitItem);
    }

    protected override void DisposeManagedResources()
    {
      this.newItem.Dispose();
      base.DisposeManagedResources();
    }

    protected internal override void SetSelectedItem(RadPageViewItem item)
    {
      if (this.ItemContainer.ItemLayout.ScrollAnimation.IsAnimating((RadObject) this.ItemContainer.ItemLayout))
        this.ItemContainer.ItemLayout.ScrollAnimation.Stop((RadObject) this.ItemContainer.ItemLayout);
      base.SetSelectedItem(item);
    }

    private void UpdateNewItem()
    {
      if (this.newItem.Parent == this.ItemsParent)
        this.ItemsParent.Children.Remove((RadElement) this.newItem);
      switch (this.NewItemVisibility)
      {
        case StripViewNewItemVisibility.Front:
          this.ItemsParent.Children.Insert(0, (RadElement) this.newItem);
          break;
        case StripViewNewItemVisibility.End:
          this.ItemsParent.Children.Add((RadElement) this.newItem);
          break;
      }
    }

    protected virtual void OnNewItemRequested()
    {
      if (this.Owner == null)
        return;
      this.Owner.OnNewPageRequested(EventArgs.Empty);
    }

    protected internal override void OnItemMouseDown(RadPageViewItem sender, MouseEventArgs e)
    {
      if (sender == this.newItem)
        return;
      base.OnItemMouseDown(sender, e);
    }

    protected internal override void OnItemClick(RadPageViewItem sender, EventArgs e)
    {
      base.OnItemClick(sender, e);
      if (sender != this.newItem)
        return;
      this.OnNewItemRequested();
    }

    public StripViewButtons HitTestButtons(Point controlClient)
    {
      foreach (RadPageViewButtonElement child in this.itemContainer.ButtonsPanel.Children)
      {
        if (child.ControlBoundingRectangle.Contains(controlClient))
          return (StripViewButtons) child.Tag;
      }
      return StripViewButtons.Auto;
    }

    protected override bool IsChildElementExternal(RadElement element)
    {
      if (element != this.newItem && element != this.itemContainer)
        return base.IsChildElementExternal(element);
      return false;
    }

    protected override SizeF MeasureItems(SizeF availableSize)
    {
      SizeF availableSize1 = availableSize;
      SizeF empty = SizeF.Empty;
      this.itemContainer.Measure(availableSize1);
      if (this.StripAlignment == StripViewAlignment.Top || this.StripAlignment == StripViewAlignment.Bottom)
      {
        empty.Height += this.itemContainer.DesiredSize.Height;
        availableSize1.Height -= this.itemContainer.DesiredSize.Height;
        if (this.ContentArea.Visibility != ElementVisibility.Collapsed)
          this.ContentArea.Measure(availableSize1);
        empty.Height += this.ContentArea.DesiredSize.Height;
        empty.Width += this.ContentArea.DesiredSize.Width;
      }
      else
      {
        empty.Width += this.itemContainer.DesiredSize.Width;
        availableSize1.Width -= this.itemContainer.DesiredSize.Width;
        if (this.ContentArea.Visibility != ElementVisibility.Collapsed)
          this.ContentArea.Measure(availableSize1);
        empty.Width += this.ContentArea.DesiredSize.Width;
        empty.Height += this.ContentArea.DesiredSize.Height;
      }
      if (this.StretchHorizontally && !float.IsInfinity(availableSize.Width))
        empty.Width = availableSize.Width;
      if (this.StretchVertically && !float.IsInfinity(availableSize.Height))
        empty.Height = availableSize.Height;
      empty.Width = Math.Min(empty.Width, availableSize.Width);
      empty.Height = Math.Min(empty.Height, availableSize.Height);
      return empty;
    }

    protected override RectangleF ArrangeItems(RectangleF itemsRect)
    {
      RectangleF rectangleF = itemsRect;
      if (this.itemContainer.Visibility != ElementVisibility.Collapsed)
      {
        SizeF desiredSize = this.itemContainer.DesiredSize;
        switch (this.StripAlignment)
        {
          case StripViewAlignment.Right:
            rectangleF = this.ArrangeRight(itemsRect, desiredSize);
            break;
          case StripViewAlignment.Bottom:
            rectangleF = this.ArrangeBottom(itemsRect, desiredSize);
            break;
          case StripViewAlignment.Left:
            rectangleF = this.ArrangeLeft(itemsRect, desiredSize);
            break;
          default:
            rectangleF = this.ArrangeTop(itemsRect, desiredSize);
            break;
        }
      }
      return rectangleF;
    }

    private RectangleF ArrangeLeft(RectangleF client, SizeF stripSize)
    {
      Padding margin = this.itemContainer.Margin;
      RectangleF finalRect = new RectangleF(client.X + (float) margin.Left, client.Y + (float) margin.Top, stripSize.Width, stripSize.Height - (float) margin.Vertical);
      this.itemContainer.Arrange(finalRect);
      return new RectangleF(finalRect.Right + (float) margin.Right, client.Y, client.Width - finalRect.Width - (float) margin.Horizontal, client.Height);
    }

    private RectangleF ArrangeTop(RectangleF client, SizeF stripSize)
    {
      Padding margin = this.itemContainer.Margin;
      RectangleF finalRect = new RectangleF(client.X + (float) margin.Left, client.Y + (float) margin.Top, stripSize.Width - (float) margin.Horizontal, stripSize.Height);
      this.itemContainer.Arrange(finalRect);
      return new RectangleF(client.X, finalRect.Bottom + (float) margin.Vertical, client.Width, client.Height - finalRect.Height - (float) margin.Vertical);
    }

    private RectangleF ArrangeRight(RectangleF client, SizeF stripSize)
    {
      Padding margin = this.itemContainer.Margin;
      RectangleF finalRect = new RectangleF(client.Right - (float) margin.Right - stripSize.Width, client.Y + (float) margin.Top, stripSize.Width, stripSize.Height - (float) margin.Vertical);
      this.itemContainer.Arrange(finalRect);
      return new RectangleF(client.X, client.Y, client.Width - finalRect.Width - (float) margin.Horizontal, client.Height);
    }

    private RectangleF ArrangeBottom(RectangleF client, SizeF stripSize)
    {
      Padding margin = this.itemContainer.Margin;
      RectangleF finalRect = new RectangleF(client.X + (float) margin.Left, client.Bottom - (float) margin.Bottom - stripSize.Height, stripSize.Width - (float) margin.Horizontal, stripSize.Height);
      this.itemContainer.Arrange(finalRect);
      return new RectangleF(client.X, client.Y, client.Width, client.Height - finalRect.Height - (float) margin.Vertical);
    }

    private void UpdateItemContainer(StripViewAlignment align)
    {
      switch (align)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          int num1 = (int) this.itemContainer.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) false);
          int num2 = (int) this.itemContainer.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) true);
          break;
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          int num3 = (int) this.itemContainer.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
          int num4 = (int) this.itemContainer.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
          break;
      }
      this.itemContainer.ButtonsPanel.SetContentOrientation(this.GetAutomaticItemOrientation(true), true);
    }

    internal static bool PropertyInvalidatesScrollOffset(RadProperty property)
    {
      if (property != RadPageViewStripElement.StripAlignmentProperty && property != RadPageViewStripElement.ItemFitModeProperty && (property != RadPageViewStripElement.ItemAlignmentProperty && property != RadPageViewElement.ItemSizeModeProperty))
        return property == RadPageViewElement.ItemContentOrientationProperty;
      return true;
    }
  }
}
