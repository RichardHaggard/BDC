// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualizedStackContainer`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class VirtualizedStackContainer<T> : BaseVirtualizedContainer<T>
  {
    private Orientation orientation = Orientation.Vertical;
    private bool fitElementsToSize = true;
    private SizeF scrollOffset;
    private SizeF desiredSize;
    private SizeF availableSize;
    private SizeF originalAvailableSize;
    private int itemSpacing;
    private float artificialOffset;
    protected float offset;

    public SizeF CurrentDesiredSize
    {
      get
      {
        return this.desiredSize;
      }
    }

    public SizeF OriginalAvailableSize
    {
      get
      {
        return this.originalAvailableSize;
      }
    }

    public SizeF RemainingAvailableSize
    {
      get
      {
        return this.availableSize;
      }
    }

    public float ArtificialOffset
    {
      get
      {
        return this.artificialOffset;
      }
    }

    public int ItemSpacing
    {
      get
      {
        return this.itemSpacing;
      }
      set
      {
        if (this.itemSpacing == value)
          return;
        this.itemSpacing = value;
        this.InvalidateMeasure();
      }
    }

    public Orientation Orientation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        if (this.orientation == value)
          return;
        this.orientation = value;
        this.InvalidateMeasure();
      }
    }

    public bool FitElementsToSize
    {
      get
      {
        return this.fitElementsToSize;
      }
      set
      {
        if (this.fitElementsToSize == value)
          return;
        this.fitElementsToSize = value;
        this.InvalidateArrange();
      }
    }

    public SizeF ScrollOffset
    {
      get
      {
        return this.scrollOffset;
      }
      set
      {
        if (!(this.scrollOffset != value))
          return;
        this.scrollOffset = value;
        this.InvalidateMeasure();
      }
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      if (!base.BeginMeasure(availableSize))
        return false;
      this.artificialOffset = 0.0f;
      this.originalAvailableSize = availableSize;
      this.desiredSize = SizeF.Empty;
      this.availableSize = availableSize;
      if ((double) this.availableSize.Width == 0.0 || (double) this.availableSize.Height == 0.0)
      {
        while (this.Children.Count > 0)
          this.RemoveElement(0);
        return false;
      }
      this.InitializeOffset();
      return true;
    }

    protected override bool MeasureElement(IVirtualizedElement<T> element)
    {
      RadElement element1 = element as RadElement;
      if (element1 == null)
        return false;
      SizeF sizeF = this.MeasureElementCore(element1, this.availableSize);
      if (this.orientation == Orientation.Vertical)
      {
        bool flag = (double) this.availableSize.Height > (double) sizeF.Height;
        float num = sizeF.Height + (float) this.ItemSpacing;
        this.availableSize.Height -= num;
        this.desiredSize.Height += num;
        this.desiredSize.Width = Math.Max(this.desiredSize.Width, sizeF.Width);
        return flag;
      }
      bool flag1 = (double) this.availableSize.Width > 0.0;
      float num1 = sizeF.Width + (float) this.ItemSpacing;
      this.availableSize.Width -= num1;
      this.desiredSize.Width += num1;
      this.desiredSize.Height = Math.Max(this.desiredSize.Height, sizeF.Height);
      return flag1;
    }

    protected virtual SizeF MeasureElementCore(RadElement element, SizeF availableSize)
    {
      SizeF elementSize = this.ElementProvider.GetElementSize(((IVirtualizedElement<T>) element).Data);
      if (this.Orientation == Orientation.Vertical)
      {
        if ((double) elementSize.Height > 0.0)
          availableSize.Height = elementSize.Height;
        if (!this.FitElementsToSize)
          availableSize.Width = float.PositiveInfinity;
      }
      else
      {
        availableSize.Width = elementSize.Width;
        if (!this.FitElementsToSize)
          availableSize.Height = float.PositiveInfinity;
      }
      element.Measure(availableSize);
      return element.DesiredSize;
    }

    protected override SizeF EndMeasure()
    {
      if (this.Children.Count > 0)
      {
        if (this.Orientation == Orientation.Horizontal)
          this.desiredSize.Width -= (float) this.ItemSpacing;
        else
          this.desiredSize.Height -= (float) this.ItemSpacing;
      }
      this.desiredSize.Width = Math.Min(this.originalAvailableSize.Width, this.desiredSize.Width);
      this.desiredSize.Height = Math.Min(this.originalAvailableSize.Height, this.desiredSize.Height);
      return this.desiredSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.InitializeOffset();
      foreach (RadElement child in this.Children)
      {
        if (this.orientation == Orientation.Vertical)
        {
          IVirtualizedElement<T> virtualizedElement = (IVirtualizedElement<T>) child;
          float width = this.FitElementsToSize ? finalSize.Width : child.DesiredSize.Width;
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

    protected virtual RectangleF ArrangeElementCore(
      RadElement element,
      SizeF finalSize,
      RectangleF arrangeRect)
    {
      element.Arrange(arrangeRect);
      return arrangeRect;
    }

    protected virtual void InitializeOffset()
    {
      if (this.orientation == Orientation.Vertical)
      {
        this.offset = this.ScrollOffset.Height + this.ArtificialOffset;
        this.availableSize.Height -= this.offset;
      }
      else
      {
        this.offset = !this.RightToLeft ? this.ScrollOffset.Width : this.availableSize.Width - this.ScrollOffset.Width;
        this.availableSize.Width -= this.offset;
      }
    }

    protected void AddArtificialOffset(float offset)
    {
      this.artificialOffset += offset;
      this.availableSize.Height -= offset;
      this.desiredSize.Height += offset;
    }
  }
}
