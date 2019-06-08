// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualizedScrollPanel`2
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class VirtualizedScrollPanel<Item, Element> : ScrollViewElement<VirtualizedStackContainer<Item>>
    where Item : class
    where Element : IVirtualizedElement<Item>, new()
  {
    private IList<Item> items;
    private ItemScroller<Item> scroller;
    private IVirtualizedElementProvider<Item> elementProvider;
    private bool autoSizeItems;

    protected override void CreateChildElements()
    {
      this.elementProvider = this.CreateElementProvider();
      this.scroller = this.CreateItemScroller();
      base.CreateChildElements();
      this.InitializeItemScroller(this.scroller);
      this.HScrollBar.Maximum = 0;
      this.WireEvents();
    }

    protected virtual void WireEvents()
    {
      this.HScrollBar.ValueChanged += new EventHandler(this.HScrollBar_ValueChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.scroller.ScrollerUpdated -= new EventHandler(this.scroller_ScrollerUpdated);
      this.HScrollBar.ValueChanged -= new EventHandler(this.HScrollBar_ValueChanged);
      if (!(this.items is ObservableCollection<Item>))
        return;
      (this.items as ObservableCollection<Item>).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.VirtualizedScrollPanel_CollectionChanged);
    }

    protected virtual IVirtualizedElementProvider<Item> CreateElementProvider()
    {
      return (IVirtualizedElementProvider<Item>) new VirtualizedPanelElementProvider<Item, Element>();
    }

    protected virtual ITraverser<Item> CreateItemTraverser(IList<Item> items)
    {
      return (ITraverser<Item>) new ItemsTraverser<Item>(items);
    }

    protected virtual ItemScroller<Item> CreateItemScroller()
    {
      return new ItemScroller<Item>();
    }

    protected virtual void InitializeItemScroller(ItemScroller<Item> scroller)
    {
      this.scroller.Scrollbar = this.VScrollBar;
      this.scroller.ElementProvider = this.elementProvider;
      this.scroller.ScrollerUpdated += new EventHandler(this.scroller_ScrollerUpdated);
    }

    protected override void InitializeViewElement(VirtualizedStackContainer<Item> viewElement)
    {
      base.InitializeViewElement(viewElement);
      this.ViewElement.ElementProvider = this.elementProvider;
      this.ViewElement.DataProvider = (IEnumerable) this.scroller;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.UnwireEvents();
      this.scroller.Dispose();
    }

    public virtual IList<Item> Items
    {
      get
      {
        return this.items;
      }
      set
      {
        if (this.items == value)
          return;
        if (this.items is ObservableCollection<Item>)
          (this.items as ObservableCollection<Item>).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.VirtualizedScrollPanel_CollectionChanged);
        this.items = value;
        this.scroller.Traverser = this.CreateItemTraverser(this.items);
        if (this.items is ObservableCollection<Item>)
          (this.items as ObservableCollection<Item>).CollectionChanged += new NotifyCollectionChangedEventHandler(this.VirtualizedScrollPanel_CollectionChanged);
        this.scroller.UpdateScrollRange();
        this.ViewElement.UpdateItems();
      }
    }

    public ItemScroller<Item> Scroller
    {
      get
      {
        return this.scroller;
      }
      internal set
      {
        this.scroller = value;
        this.ViewElement.DataProvider = (IEnumerable) this.scroller;
        if (value == null)
          return;
        this.InitializeItemScroller(this.scroller);
      }
    }

    public virtual bool FitItemsToSize
    {
      get
      {
        return this.ViewElement.FitElementsToSize;
      }
      set
      {
        if (this.ViewElement.FitElementsToSize == value)
          return;
        this.ViewElement.FitElementsToSize = value;
        this.UpdateFitToSizeMode();
        this.Scroller.UpdateScrollRange();
      }
    }

    public virtual Orientation Orientation
    {
      get
      {
        return this.ViewElement.Orientation;
      }
      set
      {
        if (this.ViewElement.Orientation == value)
          return;
        this.ViewElement.Orientation = value;
      }
    }

    public bool AutoSizeItems
    {
      get
      {
        return this.autoSizeItems;
      }
      set
      {
        if (this.autoSizeItems == value)
          return;
        this.autoSizeItems = value;
        this.OnAutoSizeChanged();
      }
    }

    public virtual int ItemSpacing
    {
      get
      {
        return this.Scroller.ItemSpacing;
      }
      set
      {
        this.Scroller.ItemSpacing = value;
        this.ViewElement.ItemSpacing = value;
        this.Scroller.UpdateScrollRange();
        this.ViewElement.UpdateItems();
      }
    }

    public virtual SizeF MeasureItem(Item item, SizeF availableSize)
    {
      IVirtualizedElement<Item> element = this.ViewElement.ElementProvider.GetElement(item, (object) null);
      RadElement radElement = (RadElement) element;
      this.SuspendLayout();
      this.Children.Add(radElement);
      element.Attach(item, (object) null);
      radElement.ResetLayout(true);
      radElement.Measure(availableSize);
      SizeF desiredSize = radElement.GetDesiredSize(false);
      this.Children.Remove(radElement);
      this.ViewElement.ElementProvider.CacheElement(element);
      element.Detach();
      this.ResumeLayout(false);
      return desiredSize;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.UpdateOnMeasure(availableSize))
        base.MeasureOverride(availableSize);
      return sizeF;
    }

    protected virtual SizeF GetItemDesiredSize(Item item)
    {
      return this.MeasureItem(item, new SizeF(float.PositiveInfinity, float.PositiveInfinity));
    }

    private void VirtualizedScrollPanel_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      this.scroller.UpdateScrollRange();
    }

    protected virtual void scroller_ScrollerUpdated(object sender, EventArgs e)
    {
      this.ViewElement.ScrollOffset = new SizeF(this.ViewElement.ScrollOffset.Width, (float) -this.Scroller.ScrollOffset);
      this.ViewElement.InvalidateMeasure();
    }

    protected virtual void HScrollBar_ValueChanged(object sender, EventArgs e)
    {
      this.ViewElement.ScrollOffset = new SizeF((float) -this.HScrollBar.Value, this.ViewElement.ScrollOffset.Height);
      this.ViewElement.InvalidateMeasure();
    }

    protected virtual void OnAutoSizeChanged()
    {
      this.Scroller.UpdateScrollRange();
      this.ViewElement.UpdateItems();
      this.ViewElement.InvalidateMeasure();
    }

    public Element GetElement(Item item)
    {
      foreach (RadElement child in this.ViewElement.Children)
      {
        IVirtualizedElement<Item> virtualizedElement = child as IVirtualizedElement<Item>;
        if (virtualizedElement != null && (object) virtualizedElement.Data == (object) item)
          return (Element) virtualizedElement;
      }
      return default (Element);
    }

    protected virtual bool UpdateOnMeasure(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      RadScrollBarElement scrollBarElement1 = this.HScrollBar;
      RadScrollBarElement scrollBarElement2 = this.VScrollBar;
      if (this.Orientation == Orientation.Horizontal)
      {
        scrollBarElement1 = this.VScrollBar;
        scrollBarElement2 = this.HScrollBar;
      }
      ElementVisibility visibility = scrollBarElement1.Visibility;
      if (this.FitItemsToSize)
      {
        this.HScrollBar.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        scrollBarElement1.LargeChange = (int) ((double) clientRectangle.Width - (double) scrollBarElement2.DesiredSize.Width - (double) this.ViewElement.Margin.Horizontal);
        scrollBarElement1.SmallChange = scrollBarElement1.LargeChange / 10;
        scrollBarElement1.Visibility = scrollBarElement1.LargeChange < scrollBarElement1.Maximum ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
      SizeF size = clientRectangle.Size;
      if (this.HScrollBar.Visibility == ElementVisibility.Visible)
        size.Height -= this.HScrollBar.DesiredSize.Height;
      this.scroller.ClientSize = size;
      return visibility != scrollBarElement1.Visibility;
    }

    protected virtual void UpdateFitToSizeMode()
    {
      if (this.FitItemsToSize)
      {
        this.HScrollBar.Maximum = 0;
      }
      else
      {
        int val2 = 0;
        if (this.Items != null)
        {
          foreach (Item obj in (IEnumerable<Item>) this.Items)
            val2 = Math.Max((int) this.GetItemDesiredSize(obj).Width, val2);
        }
        this.HScrollBar.Maximum = val2;
      }
    }
  }
}
