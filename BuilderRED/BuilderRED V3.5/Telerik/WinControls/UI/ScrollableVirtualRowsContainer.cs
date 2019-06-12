// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollableVirtualRowsContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ScrollableVirtualRowsContainer : VirtualizedStackContainer<int>
  {
    private VirtualGridTableElement tableElement;
    private float topOffset;

    public float TopOffset
    {
      get
      {
        return this.topOffset;
      }
      set
      {
        this.topOffset = value;
      }
    }

    public VirtualGridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
      set
      {
        this.tableElement = value;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Orientation = Orientation.Vertical;
    }

    protected override bool IsItemVisible(int data)
    {
      if (this.tableElement.ViewInfo.ParentViewInfo == null || this.Orientation != Orientation.Vertical || (double) this.CurrentDesiredSize.Height + (double) this.ElementProvider.GetElementSize(data).Height + (double) this.ItemSpacing >= (double) this.TopOffset)
        return base.IsItemVisible(data);
      this.AddArtificialOffset(this.ElementProvider.GetElementSize(data).Height + (float) this.ItemSpacing);
      return false;
    }

    protected override SizeF MeasureElementCore(RadElement element, SizeF availableSize)
    {
      VirtualGridRowElement virtualGridRowElement = element as VirtualGridRowElement;
      if (virtualGridRowElement == null || !virtualGridRowElement.IsChildViewVisible)
        return base.MeasureElementCore(element, availableSize);
      SizeF elementSize = this.ElementProvider.GetElementSize(virtualGridRowElement.Data);
      if (this.Orientation == Orientation.Vertical)
      {
        if ((double) elementSize.Height > 0.0)
          availableSize.Height = Math.Min(elementSize.Height, availableSize.Height + 100f);
        if (!this.FitElementsToSize)
          availableSize.Width = float.PositiveInfinity;
      }
      else
      {
        availableSize.Width = Math.Min(elementSize.Width, availableSize.Width);
        if (!this.FitElementsToSize)
          availableSize.Height = float.PositiveInfinity;
      }
      element.Measure(availableSize);
      return element.DesiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.InitializeOffset();
      foreach (RadElement child in this.Children)
      {
        if (this.Orientation == Orientation.Vertical)
        {
          IVirtualizedElement<int> virtualizedElement = (IVirtualizedElement<int>) child;
          float width = child.DesiredSize.Width;
          float height = this.ElementProvider.GetElementSize(virtualizedElement.Data).Height;
          RectangleF arrangeRect = new RectangleF(this.ScrollOffset.Width, this.offset, width, height);
          if (this.RightToLeft)
            arrangeRect.X = finalSize.Width - width;
          arrangeRect = this.ArrangeElementCore(child, finalSize, arrangeRect);
          this.offset += arrangeRect.Height + (float) this.ItemSpacing;
        }
        else
        {
          float height = this.FitElementsToSize || child.StretchVertically ? finalSize.Height : child.DesiredSize.Height;
          this.offset += this.ArrangeElementCore(child, finalSize, new RectangleF(this.offset, 0.0f, child.DesiredSize.Width, height)).Width + (float) this.ItemSpacing;
        }
      }
      return finalSize;
    }

    protected override IVirtualizedElement<int> UpdateElement(
      int position,
      int data)
    {
      if (this.tableElement == null)
        return base.UpdateElement(position, data);
      object elementContext = this.GetElementContext();
      if (position < this.Children.Count)
      {
        IVirtualizedElement<int> element = (IVirtualizedElement<int>) this.Children[position];
        if (this.ElementProvider.ShouldUpdate(element, data, elementContext))
        {
          if (this.tableElement.IsRowExpanded(data))
          {
            for (int indexFrom = position; indexFrom < this.Children.Count; ++indexFrom)
            {
              VirtualGridRowElement child = this.Children[indexFrom] as VirtualGridRowElement;
              if (child != null && child.IsChildViewInitialized && child.IsCompatible(data, elementContext))
              {
                child.Detach();
                child.Attach(data, elementContext);
                if (indexFrom != position)
                  this.Children.Move(indexFrom, position);
                return (IVirtualizedElement<int>) child;
              }
            }
            if ((this.ElementProvider as VirtualRowsElementProvider).TryGetElementWithChildView(data, elementContext, out element))
            {
              this.Children.Insert(position, (RadElement) element);
              return element;
            }
          }
          else
          {
            for (int indexFrom = position; indexFrom < this.Children.Count; ++indexFrom)
            {
              VirtualGridRowElement child = this.Children[indexFrom] as VirtualGridRowElement;
              if (child != null && !child.IsChildViewInitialized)
              {
                child.Detach();
                child.Attach(data, elementContext);
                if (indexFrom != position)
                  this.Children.Move(indexFrom, position);
                return (IVirtualizedElement<int>) child;
              }
            }
            if ((this.ElementProvider as VirtualRowsElementProvider).TryGetElementWithoutChildView(data, elementContext, out element))
            {
              this.Children.Insert(position, (RadElement) element);
              return element;
            }
          }
        }
      }
      return base.UpdateElement(position, data);
    }
  }
}
