// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewOutlookElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadPageViewOutlookElement : RadPageViewStackElement
  {
    public static RadProperty ShowFewerButtonsImageProperty = RadProperty.Register(nameof (ShowFewerButtonsImage), typeof (Image), typeof (RadPageViewOutlookElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowMoreButtonsImageProperty = RadProperty.Register(nameof (ShowMoreButtonsImage), typeof (Image), typeof (RadPageViewOutlookElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    private static object ItemShownEventKey = new object();
    private static object ItemCollapsedEventKey = new object();
    private static object ItemCheckedEventKey = new object();
    private static object ItemUncheckedEventKey = new object();
    private static object ItemAssociatedButtonClickedEventKey = new object();
    public static Bitmap AssociatedButtonDefaultImage = Telerik.WinControls.ResourceHelper.ImageFromResource(typeof (RadPageViewOutlookAssociatedButton), "Telerik.WinControls.UI.RadPageView.Views.Outlook.Resources.AssociatedButtonDefaultImage.png");
    private OutlookViewOverflowGrip gripElement;
    private OverflowItemsContainer itemsContainer;
    private List<RadPageViewOutlookItem> hiddenItems;
    private List<RadPageViewOutlookItem> uncheckedItems;

    [Browsable(false)]
    public OverflowItemsContainer OverflowItemsContainer
    {
      get
      {
        return this.itemsContainer;
      }
    }

    [Browsable(false)]
    public OutlookViewOverflowGrip OverflowGrip
    {
      get
      {
        return this.gripElement;
      }
    }

    [Category("Appearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the image shown on the item in the overflow drop-down that is used to show more buttons in the control.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image ShowMoreButtonsImage
    {
      get
      {
        return (Image) this.GetValue(RadPageViewOutlookElement.ShowMoreButtonsImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewOutlookElement.ShowMoreButtonsImageProperty, (object) value);
      }
    }

    [TypeConverter(typeof (ImageTypeConverter))]
    [DefaultValue(null)]
    [Description("Gets or sets the image shown on the item in the overflow drop-down that is used to show fewer buttons in the control.")]
    [Category("Appearance")]
    public Image ShowFewerButtonsImage
    {
      get
      {
        return (Image) this.GetValue(RadPageViewOutlookElement.ShowFewerButtonsImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewOutlookElement.ShowFewerButtonsImageProperty, (object) value);
      }
    }

    internal List<RadPageViewOutlookItem> UncheckedItems
    {
      get
      {
        return this.uncheckedItems;
      }
    }

    protected override ValueUpdateResult SetValueCore(
      RadPropertyValue propVal,
      object propModifier,
      object newValue,
      ValueSource source)
    {
      if (source != ValueSource.DefaultValueOverride && (propVal.Property == RadPageViewStackElement.StackPositionProperty || propVal.Property == RadPageViewStackElement.ItemSelectionModeProperty || propVal.Property == RadPageViewElement.ItemContentOrientationProperty))
        return ValueUpdateResult.Canceled;
      return base.SetValueCore(propVal, propModifier, newValue, source);
    }

    protected override RadPageViewItem CreateItem()
    {
      return (RadPageViewItem) new RadPageViewOutlookItem();
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.hiddenItems = new List<RadPageViewOutlookItem>();
      this.uncheckedItems = new List<RadPageViewOutlookItem>();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ContentArea.ThemeRole = "OutlookViewContentArea";
      this.gripElement = new OutlookViewOverflowGrip();
      this.gripElement.MinSize = new Size(0, 10);
      this.Children.Add((RadElement) this.gripElement);
      this.itemsContainer = new OverflowItemsContainer(this);
      this.itemsContainer.MinSize = new Size(0, 35);
      this.Children.Add((RadElement) this.itemsContainer);
      this.WireEvents();
      int num1 = (int) this.itemsContainer.ShowMoreButtonsItem.BindProperty(RadButtonItem.ImageProperty, (RadObject) this, RadPageViewOutlookElement.ShowMoreButtonsImageProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.itemsContainer.ShowFewerButtonsItem.BindProperty(RadButtonItem.ImageProperty, (RadObject) this, RadPageViewOutlookElement.ShowFewerButtonsImageProperty, PropertyBindingOptions.OneWay);
    }

    private void WireEvents()
    {
      this.gripElement.Dragged += new OverflowGripDragHandler(this.OverflowGripElement_Dragged);
    }

    private void UnwireEvents()
    {
      this.gripElement.Dragged -= new OverflowGripDragHandler(this.OverflowGripElement_Dragged);
    }

    protected override SizeF MeasureItems(SizeF availableSize)
    {
      SizeF availableSize1;
      ref SizeF local1 = ref availableSize1;
      ref SizeF local2 = ref availableSize;
      double width = (double) local2.Width;
      double horizontal = (double) this.gripElement.Margin.Horizontal;
      double num1;
      float num2 = (float) (num1 = width - horizontal);
      local2.Width = (float) num1;
      double num3 = (double) num2;
      double height = (double) availableSize.Height;
      local1 = new SizeF((float) num3, (float) height);
      this.gripElement.Measure(availableSize1);
      availableSize1.Height -= this.gripElement.DesiredSize.Height;
      this.itemsContainer.Measure(availableSize1);
      availableSize1.Height -= this.itemsContainer.DesiredSize.Height;
      SizeF sizeF = base.MeasureItems(availableSize1);
      sizeF.Height = sizeF.Height + this.gripElement.DesiredSize.Height + this.itemsContainer.DesiredSize.Height;
      return sizeF;
    }

    protected override RectangleF ArrangeItems(RectangleF clientRect)
    {
      this.itemsContainer.Arrange(new RectangleF(new PointF(clientRect.Left + (float) this.itemsContainer.Margin.Left, clientRect.Bottom - (this.itemsContainer.DesiredSize.Height + (float) this.itemsContainer.Margin.Bottom)), new SizeF(clientRect.Width - (float) this.itemsContainer.Margin.Horizontal, this.itemsContainer.DesiredSize.Height)));
      clientRect.Height -= this.itemsContainer.DesiredSize.Height + (float) this.itemsContainer.Margin.Vertical;
      RectangleF rectangleF = base.ArrangeItems(clientRect);
      Padding margin = this.gripElement.Margin;
      this.gripElement.Arrange(new RectangleF(new PointF(clientRect.Left + (float) margin.Left, rectangleF.Bottom + (float) this.ContentArea.Margin.Bottom + (float) margin.Top), new SizeF(clientRect.Width - (float) this.gripElement.Margin.Horizontal, this.gripElement.DesiredSize.Height)));
      return rectangleF;
    }

    protected override RectangleF GetContentAreaRectangle(RectangleF clientRect)
    {
      RectangleF contentAreaRectangle = base.GetContentAreaRectangle(clientRect);
      contentAreaRectangle.Height -= this.gripElement.DesiredSize.Height + (float) this.gripElement.Margin.Vertical;
      return contentAreaRectangle;
    }

    protected override bool IsChildElementExternal(RadElement element)
    {
      if (element != this.gripElement && element != this.itemsContainer)
        return base.IsChildElementExternal(element);
      return false;
    }

    public RadPageViewOutlookItem[] GetHiddenItems()
    {
      return this.hiddenItems.ToArray();
    }

    public RadPageViewOutlookItem[] GetUncheckedItems()
    {
      return this.uncheckedItems.ToArray();
    }

    protected virtual int GetCurrentlyVisibleItemsCount()
    {
      int num = 0;
      foreach (RadElement radElement in (IEnumerable<RadPageViewItem>) this.Items)
      {
        if (radElement.Visibility == ElementVisibility.Visible)
          ++num;
      }
      return num;
    }

    public virtual void UncheckItem(RadPageViewOutlookItem item)
    {
      if (this.uncheckedItems.Contains(item))
        return;
      this.uncheckedItems.Add(item);
      item.Visibility = ElementVisibility.Collapsed;
      this.OnItemUnchecked((object) this, new OutlookViewItemEventArgs(item));
      if (this.hiddenItems.Contains(item) || this.GetCurrentlyVisibleItemsCount() > this.layoutInfo.itemCount)
        return;
      this.ShowFirstPossibleItem();
    }

    public virtual void CheckItem(RadPageViewOutlookItem item)
    {
      if (!this.uncheckedItems.Contains(item))
        return;
      this.uncheckedItems.Remove(item);
      this.OnItemChecked((object) this, new OutlookViewItemEventArgs(item));
      if (this.hiddenItems.Contains(item))
        return;
      item.Visibility = ElementVisibility.Visible;
      if (this.GetCurrentlyVisibleItemsCount() <= this.layoutInfo.itemCount)
        return;
      this.HideFirstPossibleItem();
    }

    public bool DragGripDown()
    {
      return this.HideFirstPossibleItem();
    }

    public bool DragGripUp()
    {
      return this.ShowFirstPossibleItem();
    }

    public void ShowItems(int itemCount)
    {
      for (bool flag = true; itemCount > 0 && flag; flag = this.DragGripUp())
        --itemCount;
    }

    public void HideItems(int itemCount)
    {
      for (bool flag = true; itemCount > 0 && flag; flag = this.DragGripDown())
        --itemCount;
    }

    internal virtual bool ShowFirstPossibleItem()
    {
      if (this.hiddenItems.Count == 0)
        return false;
      RadPageViewOutlookItem stackItem = (RadPageViewOutlookItem) null;
      for (int index = this.hiddenItems.Count - 1; index > -1; --index)
      {
        stackItem = this.hiddenItems[index];
        if (this.uncheckedItems.Contains(stackItem))
          stackItem = (RadPageViewOutlookItem) null;
        else
          break;
      }
      if (stackItem == null)
        return false;
      this.hiddenItems.Remove(stackItem);
      stackItem.Visibility = ElementVisibility.Visible;
      this.itemsContainer.UnregisterCollapsedItem(stackItem);
      this.OnItemShown((object) this, new OutlookViewItemEventArgs(stackItem));
      return true;
    }

    internal virtual bool HideFirstPossibleItem()
    {
      RadPageViewOutlookItem nextVisibleItem = this.GetNextVisibleItem();
      if (nextVisibleItem == null || this.hiddenItems.Contains(nextVisibleItem))
        return false;
      nextVisibleItem.Visibility = ElementVisibility.Collapsed;
      this.hiddenItems.Add(nextVisibleItem);
      this.itemsContainer.RegisterCollapsedItem(nextVisibleItem);
      this.OnItemCollapsed((object) this, new OutlookViewItemEventArgs(nextVisibleItem));
      return true;
    }

    protected virtual RadPageViewOutlookItem GetNextVisibleItem()
    {
      for (int index = this.Items.Count - 1; index > -1; --index)
      {
        RadPageViewOutlookItem pageViewOutlookItem = this.Items[index] as RadPageViewOutlookItem;
        if (pageViewOutlookItem.Visibility == ElementVisibility.Visible)
          return pageViewOutlookItem;
      }
      return (RadPageViewOutlookItem) null;
    }

    protected override void RemoveItemCore(RadPageViewItem item)
    {
      RadPageViewOutlookItem pageViewOutlookItem = item as RadPageViewOutlookItem;
      if (pageViewOutlookItem.AssociatedOverflowButton != null)
        pageViewOutlookItem.AssociatedOverflowButton.Dispose();
      this.hiddenItems.Remove(pageViewOutlookItem);
      this.uncheckedItems.Remove(pageViewOutlookItem);
      base.RemoveItemCore(item);
    }

    private void OverflowGripElement_Dragged(object sender, OverflowEventArgs args)
    {
      if (args.Up)
        this.DragGripUp();
      else
        this.DragGripDown();
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      base.DisposeManagedResources();
    }

    protected internal virtual void OnItemAssociatedButtonClick(object sender, EventArgs args)
    {
      EventHandler eventHandler = this.Events[RadPageViewOutlookElement.ItemAssociatedButtonClickedEventKey] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler(sender, args);
    }

    protected virtual void OnItemChecked(object sender, OutlookViewItemEventArgs args)
    {
      OutlookViewItemEventHandler itemEventHandler = this.Events[RadPageViewOutlookElement.ItemCheckedEventKey] as OutlookViewItemEventHandler;
      if (itemEventHandler == null)
        return;
      itemEventHandler(sender, args);
    }

    protected virtual void OnItemUnchecked(object sender, OutlookViewItemEventArgs args)
    {
      OutlookViewItemEventHandler itemEventHandler = this.Events[RadPageViewOutlookElement.ItemUncheckedEventKey] as OutlookViewItemEventHandler;
      if (itemEventHandler == null)
        return;
      itemEventHandler(sender, args);
    }

    protected virtual void OnItemShown(object sender, OutlookViewItemEventArgs args)
    {
      OutlookViewItemEventHandler itemEventHandler = this.Events[RadPageViewOutlookElement.ItemShownEventKey] as OutlookViewItemEventHandler;
      if (itemEventHandler == null)
        return;
      itemEventHandler(sender, args);
    }

    protected virtual void OnItemCollapsed(object sender, OutlookViewItemEventArgs args)
    {
      OutlookViewItemEventHandler itemEventHandler = this.Events[RadPageViewOutlookElement.ItemCollapsedEventKey] as OutlookViewItemEventHandler;
      if (itemEventHandler == null)
        return;
      itemEventHandler(sender, args);
    }

    public event EventHandler ItemAssociatedButtonClicked
    {
      add
      {
        this.Events.AddHandler(RadPageViewOutlookElement.ItemAssociatedButtonClickedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageViewOutlookElement.ItemAssociatedButtonClickedEventKey, (Delegate) value);
      }
    }

    public event OutlookViewItemEventHandler ItemShown
    {
      add
      {
        this.Events.AddHandler(RadPageViewOutlookElement.ItemShownEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageViewOutlookElement.ItemShownEventKey, (Delegate) value);
      }
    }

    public event OutlookViewItemEventHandler ItemCollapsed
    {
      add
      {
        this.Events.AddHandler(RadPageViewOutlookElement.ItemCollapsedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageViewOutlookElement.ItemCollapsedEventKey, (Delegate) value);
      }
    }

    public event OutlookViewItemEventHandler ItemChecked
    {
      add
      {
        this.Events.AddHandler(RadPageViewOutlookElement.ItemCheckedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageViewOutlookElement.ItemCheckedEventKey, (Delegate) value);
      }
    }

    public event OutlookViewItemEventHandler ItemUnchecked
    {
      add
      {
        this.Events.AddHandler(RadPageViewOutlookElement.ItemUncheckedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageViewOutlookElement.ItemUncheckedEventKey, (Delegate) value);
      }
    }
  }
}
