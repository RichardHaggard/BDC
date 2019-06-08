// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseListViewContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BaseListViewContainer : VirtualizedStackContainer<ListViewDataItem>
  {
    protected BaseListViewElement owner;
    private SizeF originalAvailableSize;
    protected SizeF cachedDesiredSize;

    public BaseListViewContainer(BaseListViewElement owner)
    {
      this.owner = owner;
    }

    protected override bool IsItemVisible(ListViewDataItem data)
    {
      return data.Visible;
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      this.SuspendThemeRefresh();
      foreach (ListViewDataItem data in this.DataProvider)
      {
        if (!data.IsMeasureValid)
        {
          if (!this.owner.AllowArbitraryItemHeight && !this.owner.AllowArbitraryItemWidth)
          {
            Size size = data is ListViewDataItemGroup ? this.owner.GroupItemSize : this.owner.ItemSize;
            if (this.owner.Owner.ViewType != ListViewType.IconsView && this.owner.FullRowSelect && !float.IsInfinity(availableSize.Width))
              size.Width = size.Width;
            data.ActualSize = size;
          }
          else
          {
            RadElement radElement = this.UpdateElement(data is ListViewDataItemGroup ? 1 : 0, data) as RadElement;
            if (radElement != null)
            {
              radElement.InvalidateMeasure();
              radElement.Measure(availableSize);
            }
          }
        }
      }
      this.owner.Owner.IsItemsMeasureValid = true;
      this.ResumeThemeRefresh();
      if (this.owner.Owner.ViewType == ListViewType.ListView && this.owner.AllowArbitraryItemWidth)
        this.owner.UpdateHScrollbarMaximum();
      if (this.owner.Owner.ViewType == ListViewType.IconsView)
        this.owner.Scroller.UpdateScrollRange();
      else
        this.owner.Scroller.UpdateScrollValue();
      this.cachedDesiredSize = SizeF.Empty;
      this.originalAvailableSize = availableSize;
      return base.BeginMeasure(availableSize);
    }

    protected override SizeF EndMeasure()
    {
      if (this.Children.Count > 0)
      {
        if (this.Orientation == Orientation.Horizontal)
          this.cachedDesiredSize.Width -= (float) this.ItemSpacing;
        else
          this.cachedDesiredSize.Height -= (float) this.ItemSpacing;
      }
      this.cachedDesiredSize.Width = Math.Min(this.originalAvailableSize.Width, this.cachedDesiredSize.Width);
      this.cachedDesiredSize.Height = Math.Min(this.originalAvailableSize.Height, this.cachedDesiredSize.Height);
      return this.cachedDesiredSize;
    }

    protected override SizeF MeasureElementCore(RadElement element, SizeF availableSize)
    {
      element.Measure(availableSize);
      return element.DesiredSize;
    }

    protected override int FindCompatibleElement(int position, ListViewDataItem data)
    {
      if (data is ListViewDataItemGroup)
      {
        for (int index = position + 1; index < this.Children.Count; ++index)
        {
          if (this.Children[index] is BaseListViewGroupVisualItem)
            return index;
        }
        return -1;
      }
      if (data == null)
        return -1;
      for (int index = position + 1; index < this.Children.Count; ++index)
      {
        if ((object) ((BaseListViewVisualItem) this.Children[index]).Data.GetType() == (object) data.GetType())
          return index;
      }
      return -1;
    }

    protected override IVirtualizedElement<ListViewDataItem> UpdateElement(
      int position,
      ListViewDataItem data)
    {
      object elementContext = this.GetElementContext();
      IVirtualizedElement<ListViewDataItem> element;
      if (position < this.Children.Count)
      {
        element = (IVirtualizedElement<ListViewDataItem>) this.Children[position];
        if (this.ElementProvider.ShouldUpdate(element, data, elementContext))
        {
          if (position < this.Children.Count - 1)
          {
            IVirtualizedElement<ListViewDataItem> child = (IVirtualizedElement<ListViewDataItem>) this.Children[position + 1];
            if (child.Data.Equals((object) data))
            {
              this.Children.Move(position + 1, position);
              child.Synchronize();
              return child;
            }
          }
          if (this.ElementProvider.IsCompatible(element, data, elementContext))
          {
            element.Detach();
            element.Attach(data, elementContext);
          }
          else
          {
            int compatibleElement = this.FindCompatibleElement(position, data);
            if (compatibleElement > position)
            {
              this.Children.Move(compatibleElement, position);
              element = (IVirtualizedElement<ListViewDataItem>) this.Children[position];
              element.Detach();
              element.Attach(data, elementContext);
            }
            else
            {
              position = Math.Min(position, this.Children.Count);
              element = this.ElementProvider.GetElement(data, elementContext);
              this.InsertElement(position, element, data);
            }
          }
        }
      }
      else
      {
        position = Math.Min(position, this.Children.Count);
        element = this.ElementProvider.GetElement(data, elementContext);
        this.InsertElement(position, element, data);
      }
      return element;
    }
  }
}
