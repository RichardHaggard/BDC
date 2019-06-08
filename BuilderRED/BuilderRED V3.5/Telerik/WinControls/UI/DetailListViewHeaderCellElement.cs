// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewHeaderCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class DetailListViewHeaderCellElement : DetailListViewCellElement
  {
    private const int resizePointerOffset = 4;
    protected ArrowPrimitive arrow;
    private double zoomedColumnWidth;

    public DetailListViewHeaderCellElement(ListViewDetailColumn column)
      : base(column)
    {
    }

    protected virtual void BeginDragDrop()
    {
      if (!this.Data.Owner.AllowColumnReorder)
        return;
      (this.Data.Owner.ViewElement as DetailListViewElement).ColumnDragDropService.Start((object) this);
    }

    public virtual void BeginResize(Point mousePosition)
    {
      this.Data.Owner.ColumnResizingBehavior.BeginResize(this.Data, mousePosition);
    }

    public virtual bool IsInResizeLocation(Point point)
    {
      if (!this.Data.Owner.AllowColumnResize)
        return false;
      if (point.X >= this.ControlBoundingRectangle.X && point.X <= this.ControlBoundingRectangle.X + 4)
        return this.Parent.Children.IndexOf((RadElement) this) > 0;
      return point.X >= this.ControlBoundingRectangle.Right - 4 && point.X <= this.ControlBoundingRectangle.Right;
    }

    public override void Synchronize()
    {
      if (this.column.Owner != null)
      {
        int index = this.column.Owner.SortDescriptors.IndexOf(this.column.Name);
        if (index >= 0)
          this.arrow.Direction = this.column.Owner.SortDescriptors[index].Direction == ListSortDirection.Ascending ? Telerik.WinControls.ArrowDirection.Up : Telerik.WinControls.ArrowDirection.Down;
      }
      base.Synchronize();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.arrow = new ArrowPrimitive();
      this.arrow.Alignment = ContentAlignment.TopCenter;
      this.arrow.Margin = new Padding(0, 1, 0, 0);
      this.Children.Add((RadElement) this.arrow);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.Data.Owner.EndEdit();
      if (this.IsInResizeLocation(e.Location))
      {
        if (e.X <= this.ControlBoundingRectangle.X + 4 ^ this.RightToLeft)
        {
          int num = this.Parent.Children.IndexOf((RadElement) this);
          if (num <= 0)
            return;
          (this.Parent.Children[num - 1] as DetailListViewHeaderCellElement)?.BeginResize(e.Location);
        }
        else
          this.BeginResize(e.Location);
      }
      else
      {
        if (e.Button != MouseButtons.Left)
          return;
        this.BeginDragDrop();
      }
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      this.Data.Owner.EndEdit();
      MouseEventArgs mouseEventArgs = e as MouseEventArgs;
      if (mouseEventArgs == null || !this.IsInResizeLocation(mouseEventArgs.Location))
        return;
      if (mouseEventArgs.X <= this.ControlBoundingRectangle.X + 4 ^ this.RightToLeft)
      {
        int num = this.Parent.Children.IndexOf((RadElement) this);
        if (num <= 0)
          return;
        DetailListViewHeaderCellElement child = this.Parent.Children[num - 1] as DetailListViewHeaderCellElement;
        if (child == null)
          return;
        child.Data.AutoSizeMode = ListViewBestFitColumnMode.AllCells;
        child.Data.BestFit();
      }
      else
      {
        this.Data.AutoSizeMode = ListViewBestFitColumnMode.AllCells;
        this.Data.BestFit();
      }
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return this.Data.Owner.AllowColumnReorder;
    }

    protected override void OnZoomGesture(ZoomGestureEventArgs args)
    {
      base.OnZoomGesture(args);
      if (args.IsBegin)
        this.zoomedColumnWidth = (double) this.Data.Width;
      this.zoomedColumnWidth *= args.ZoomFactor;
      this.Data.Width = (float) (int) this.zoomedColumnWidth;
      args.Handled = true;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      RadDragDropService columnDragDropService = (RadDragDropService) (this.Data.Owner.ViewElement as DetailListViewElement).ColumnDragDropService;
      if (columnDragDropService.State != RadServiceState.Working && this.IsInResizeLocation(args.Location))
        return;
      if (args.IsBegin)
      {
        int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) true);
        columnDragDropService.BeginDrag(this.ElementTree.Control.PointToScreen(args.Location), (ISupportDrag) this);
      }
      if (columnDragDropService.State == RadServiceState.Working)
        columnDragDropService.DoMouseMove(this.ElementTree.Control.PointToScreen(args.Location));
      if (args.IsEnd)
      {
        columnDragDropService.EndDrag();
        int num1 = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
        int num2 = (int) this.ResetValue(RadElement.IsMouseDownProperty, ValueResetFlags.Local);
      }
      args.Handled = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.IgnoreColumnWidth)
        return sizeF;
      return new SizeF(this.Data.Width, Math.Max(sizeF.Height, this.Data.Owner.HeaderHeight));
    }
  }
}
