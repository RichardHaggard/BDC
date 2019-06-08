// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewColumnContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class DetailListViewColumnContainer : VirtualizedStackContainer<ListViewDetailColumn>
  {
    private bool scrollColumns = true;
    private SizeF availableSize;
    private SizeF desiredSize;
    private DetailListViewElement context;

    public DetailListViewColumnContainer()
    {
    }

    public DetailListViewColumnContainer(DetailListViewElement context)
    {
      this.context = context;
    }

    public DetailListViewElement Context
    {
      get
      {
        return this.context;
      }
      set
      {
        this.context = value;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.ItemSpacing = -1;
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Orientation = Orientation.Horizontal;
    }

    protected bool ScrollColumns
    {
      get
      {
        return this.scrollColumns;
      }
      set
      {
        this.scrollColumns = value;
      }
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      this.availableSize = availableSize;
      this.desiredSize = SizeF.Empty;
      return base.BeginMeasure(availableSize);
    }

    protected override SizeF EndMeasure()
    {
      if (this.Children.Count > 0)
      {
        if (this.Orientation == Orientation.Horizontal)
        {
          this.desiredSize.Width -= (float) this.ItemSpacing;
          this.desiredSize.Width = Math.Min(this.desiredSize.Width, this.availableSize.Width);
        }
        else
        {
          this.desiredSize.Height -= (float) this.ItemSpacing;
          this.desiredSize.Height = Math.Min(this.desiredSize.Height, this.availableSize.Height);
        }
      }
      return this.desiredSize;
    }

    protected override bool MeasureElement(IVirtualizedElement<ListViewDetailColumn> element)
    {
      DetailListViewCellElement listViewCellElement = element as DetailListViewCellElement;
      if (listViewCellElement == null)
        return false;
      SizeF sizeF = this.MeasureElementCore((RadElement) listViewCellElement, this.availableSize);
      this.desiredSize.Height = Math.Max(this.desiredSize.Height, sizeF.Height);
      this.desiredSize.Width += sizeF.Width;
      if (this.ScrollColumns)
        return (double) this.desiredSize.Width - (double) this.context.ColumnScrollBar.Value < (double) this.availableSize.Width;
      return true;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF withinBounds = new RectangleF(PointF.Empty, finalSize);
      float x = withinBounds.X;
      foreach (RadElement child in this.Children)
      {
        RectangleF rectangleF = new RectangleF(x, withinBounds.Y, child.DesiredSize.Width, finalSize.Height);
        if (this.RightToLeft)
          rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, withinBounds);
        if (this.ScrollColumns)
        {
          if (this.RightToLeft)
            rectangleF.X += (float) this.context.ColumnScroller.ScrollOffset;
          else
            rectangleF.X -= (float) this.context.ColumnScroller.ScrollOffset;
        }
        this.ArrangeElementCore(child, finalSize, rectangleF);
        x += child.DesiredSize.Width + (float) this.ItemSpacing;
      }
      return finalSize;
    }

    protected override bool IsItemVisible(ListViewDetailColumn data)
    {
      return data.Visible;
    }
  }
}
