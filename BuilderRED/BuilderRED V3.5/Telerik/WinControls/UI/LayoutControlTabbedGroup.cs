// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlTabbedGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class LayoutControlTabbedGroup : LayoutControlItemBase, ILayoutControlItemsHost
  {
    private LayoutControlTabStripElement tabStrip;
    private RadItemOwnerCollection itemGroups;

    public LayoutControlTabbedGroup()
    {
      this.tabStrip.DrawFill = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.tabStrip = new LayoutControlTabStripElement();
      this.tabStrip.StripButtons = StripViewButtons.None;
      this.Children.Add((RadElement) this.tabStrip);
      this.itemGroups = new RadItemOwnerCollection();
      this.itemGroups.ItemTypes = new Type[1]
      {
        typeof (LayoutControlGroupItem)
      };
      this.itemGroups.Owner = (RadElement) this.tabStrip.ContentArea;
      this.itemGroups.ItemsChanged += new ItemChangedDelegate(this.itemGroups_ItemsChanged);
    }

    [Browsable(false)]
    public LayoutControlTabStripElement TabStrip
    {
      get
      {
        return this.tabStrip;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection ItemGroups
    {
      get
      {
        return this.itemGroups;
      }
    }

    public LayoutControlContainerElement SelectedLayoutContainer
    {
      get
      {
        if (this.tabStrip.SelectedTab == null)
          return (LayoutControlContainerElement) null;
        return this.tabStrip.SelectedTab.LayoutGroupItem.ContainerElement;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    public LayoutControlGroupItem SelectedGroup
    {
      get
      {
        if (this.tabStrip.SelectedTab == null)
          return (LayoutControlGroupItem) null;
        return this.tabStrip.SelectedTab.LayoutGroupItem;
      }
      set
      {
        this.tabStrip.SetSelectedGroup(value);
      }
    }

    public override Size MinSize
    {
      get
      {
        return this.CalcMinSize();
      }
      set
      {
      }
    }

    public override Size MaxSize
    {
      get
      {
        return this.CalcMaxSize();
      }
      set
      {
      }
    }

    RadItemOwnerCollection ILayoutControlItemsHost.Items
    {
      get
      {
        return this.ItemGroups;
      }
    }

    public int GetTargetDropIndex(Point hitPoint)
    {
      hitPoint.Offset(-this.TabStrip.ItemContainer.BoundingRectangle.X, -this.TabStrip.ItemContainer.BoundingRectangle.Y);
      int num = 0;
      foreach (LayoutControlTabStripItem controlTabStripItem in (IEnumerable<RadPageViewItem>) this.TabStrip.Items)
      {
        if (controlTabStripItem.BoundingRectangle.Contains(hitPoint))
        {
          if (hitPoint.X - controlTabStripItem.BoundingRectangle.X < controlTabStripItem.BoundingRectangle.Right - hitPoint.X)
            return num;
          return num + 1;
        }
        ++num;
      }
      return -1;
    }

    public Rectangle GetTargetBounds(int tabIndex)
    {
      Point location = new Point(this.Bounds.Location.X, this.Bounds.Location.Y);
      location.Offset(this.tabStrip.ItemContainer.BoundingRectangle.Location);
      RadPageViewItem radPageViewItem1 = tabIndex > 0 ? this.TabStrip.Items[tabIndex - 1] : (RadPageViewItem) null;
      RadPageViewItem radPageViewItem2 = tabIndex < this.TabStrip.Items.Count ? this.TabStrip.Items[tabIndex] : (RadPageViewItem) null;
      if (radPageViewItem1 == null && radPageViewItem2 == null)
        return Rectangle.Empty;
      int dx = radPageViewItem1 != null ? (radPageViewItem1.BoundingRectangle.Left + radPageViewItem1.BoundingRectangle.Right) / 2 : radPageViewItem2.BoundingRectangle.Left;
      int width = (radPageViewItem1 != null ? radPageViewItem1.BoundingRectangle.Width / 2 : 0) + (radPageViewItem2 != null ? radPageViewItem2.BoundingRectangle.Width / 2 : 0);
      location.Offset(dx, 0);
      return new Rectangle(location, new Size(width, this.TabStrip.ItemContainer.BoundingRectangle.Height));
    }

    protected virtual Size CalcMaxSize()
    {
      Size size = new Size(int.MaxValue, int.MaxValue);
      foreach (LayoutControlGroupItem itemGroup in (RadItemCollection) this.ItemGroups)
      {
        if (itemGroup.MaxSize.Width != 0)
          size.Width = Math.Min(size.Width, itemGroup.MaxSize.Width);
        if (itemGroup.MaxSize.Height != 0)
          size.Height = Math.Min(size.Height, itemGroup.MaxSize.Height);
      }
      size.Width = size.Width == int.MaxValue ? 0 : size.Width;
      size.Height = size.Height == int.MaxValue ? 0 : size.Height;
      if (size.Width != 0)
        size.Width += this.tabStrip.BoundingRectangle.Width - this.tabStrip.ContentArea.BoundingRectangle.Width;
      if (size.Height != 0)
        size.Height += this.tabStrip.BoundingRectangle.Height - this.tabStrip.ContentArea.BoundingRectangle.Height;
      return size;
    }

    protected virtual Size CalcMinSize()
    {
      Size empty = Size.Empty;
      foreach (LayoutControlGroupItem itemGroup in (RadItemCollection) this.ItemGroups)
      {
        if (itemGroup.MinSize.Width != 0)
          empty.Width = Math.Max(empty.Width, itemGroup.MinSize.Width);
        if (itemGroup.MinSize.Height != 0)
          empty.Height = Math.Max(empty.Height, itemGroup.MinSize.Height);
      }
      empty.Width += this.tabStrip.BoundingRectangle.Width - this.tabStrip.ContentArea.BoundingRectangle.Width;
      empty.Height += this.tabStrip.BoundingRectangle.Height - this.tabStrip.ContentArea.BoundingRectangle.Height;
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (!this.AutoSize)
        this.MeasureOverride(finalSize);
      SizeF sizeF = base.ArrangeOverride(finalSize);
      if (this.SelectedLayoutContainer != null)
        this.SelectedLayoutContainer.PerformControlLayout();
      return sizeF;
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (!(e.PropertyName == "IsHidden"))
        return;
      foreach (LayoutControlGroupItem itemGroup in (RadItemCollection) this.ItemGroups)
      {
        bool flag = !this.IsHidden && this.SelectedGroup == itemGroup;
        foreach (LayoutControlItemBase layoutControlItemBase in itemGroup.Items)
          layoutControlItemBase.IsHidden = !flag;
      }
    }

    private void itemGroups_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      while (this.tabStrip.Items.Count > 0)
        this.tabStrip.RemoveItem(this.tabStrip.Items[this.tabStrip.Items.Count - 1]);
      foreach (LayoutControlGroupItem itemGroup in (RadItemCollection) this.itemGroups)
        this.tabStrip.AddItem((RadPageViewItem) new LayoutControlTabStripItem(itemGroup));
      if (this.tabStrip.Items.Count > 0)
        this.tabStrip.SelectedItem = this.tabStrip.Items[this.tabStrip.Items.Count - 1];
      (this.ElementTree != null ? this.ElementTree.Control as RadLayoutControl : (RadLayoutControl) null)?.OnStructureChanged((object) this);
    }
  }
}
