// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitGridBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class SplitGridBehavior : GridBehaviorImpl
  {
    private Point mouseDownLoction = Point.Empty;
    private Size tableSize = Size.Empty;
    private const int resizeTolerance = 2;
    private const int minSize = 100;
    private Cursor originalCursor;
    private GridTableElement tableToResize;
    private GridTableElement tableToShrink;

    public override bool OnClick(EventArgs e)
    {
      return false;
    }

    public override bool OnDoubleClick(EventArgs e)
    {
      return false;
    }

    public override bool ProcessKey(KeyEventArgs keys)
    {
      return false;
    }

    public override bool ProcessKeyDown(KeyEventArgs keys)
    {
      return false;
    }

    public override bool ProcessKeyPress(KeyPressEventArgs keys)
    {
      return false;
    }

    public override bool ProcessKeyUp(KeyEventArgs keys)
    {
      return false;
    }

    public override bool OnMouseWheel(MouseEventArgs e)
    {
      return false;
    }

    public override bool OnMouseDoubleClick(MouseEventArgs e)
    {
      return false;
    }

    public override bool OnContextMenu(MouseEventArgs e)
    {
      return false;
    }

    public override bool OnMouseDown(MouseEventArgs e)
    {
      this.mouseDownLoction = e.Location;
      if (e.Button == MouseButtons.Left)
      {
        GridTableElement tableElementAtPoint = this.GetGridTableElementAtPoint(e.Location);
        if (this.CanResizeTable(e.Location, tableElementAtPoint))
        {
          this.tableToResize = tableElementAtPoint;
          this.tableSize = this.tableToResize.Size;
          this.GridControl.Capture = true;
          GridTableElement tableElement1 = this.FindTableElement(0, this.GridViewElement.Panel.Children);
          GridTableElement tableElement2 = this.FindTableElement(this.GridViewElement.Panel.Children.IndexOf((RadElement) tableElement1) + 1, this.GridViewElement.Panel.Children);
          this.tableToShrink = this.tableToResize != tableElement1 ? tableElement1 : tableElement2;
          return true;
        }
      }
      return false;
    }

    public override bool OnMouseUp(MouseEventArgs e)
    {
      this.ResetFieldValues();
      return false;
    }

    public override bool OnMouseMove(MouseEventArgs e)
    {
      if (this.GetGridTableElementAtPoint(e.Location) == null)
      {
        this.GridViewElement.ElementTree.Control.Cursor = Cursors.Default;
        return false;
      }
      if (e.Button == MouseButtons.None && this.ShowSizeNSCursor(e.Location))
        return true;
      if (this.tableToResize == null)
        return false;
      this.ResizeTable(e.Location);
      return true;
    }

    public override bool OnMouseEnter(EventArgs e)
    {
      return false;
    }

    public override bool OnMouseLeave(EventArgs e)
    {
      return false;
    }

    private bool CanResizeTable(Point currentLocation, GridTableElement table)
    {
      if (table == null)
        return false;
      Rectangle boundingRectangle = table.ControlBoundingRectangle;
      if (this.GridViewElement.SplitMode == RadGridViewSplitMode.Horizontal)
      {
        if (currentLocation.Y >= boundingRectangle.Bottom - 2 && currentLocation.Y <= boundingRectangle.Bottom + 2)
          return true;
      }
      else if (this.GridViewElement.SplitMode == RadGridViewSplitMode.Vertical && currentLocation.X >= boundingRectangle.X && currentLocation.X <= boundingRectangle.X + 2)
        return true;
      return false;
    }

    private bool ShowSizeNSCursor(Point currentLocation)
    {
      GridTableElement tableElementAtPoint = this.GetGridTableElementAtPoint(currentLocation);
      if (tableElementAtPoint == null)
      {
        this.ResetControlCursor();
        return false;
      }
      if (this.CanResizeTable(currentLocation, tableElementAtPoint))
      {
        if (this.originalCursor == (Cursor) null)
        {
          this.originalCursor = this.GridViewElement.ElementTree.Control.Cursor;
          if (this.GridViewElement.SplitMode == RadGridViewSplitMode.Horizontal)
            this.GridViewElement.ElementTree.Control.Cursor = Cursors.SizeNS;
          else if (this.GridViewElement.SplitMode == RadGridViewSplitMode.Vertical)
            this.GridViewElement.ElementTree.Control.Cursor = Cursors.SizeWE;
        }
        return true;
      }
      this.ResetControlCursor();
      return false;
    }

    private void ResizeTable(Point currentLocation)
    {
      if (this.GridViewElement.SplitMode == RadGridViewSplitMode.Horizontal)
      {
        int height1 = this.tableSize.Height + (currentLocation.Y - this.mouseDownLoction.Y);
        int height2 = this.tableToResize.Parent.Size.Height - height1;
        if (this.GridViewElement.GroupPanelElement.Visibility == ElementVisibility.Visible)
          height2 -= this.GridViewElement.GroupPanelElement.Size.Height;
        if (height1 < 100 || height2 < 100)
          return;
        this.tableToResize.ForcedDesiredSize = (SizeF) new Size(this.tableToResize.Size.Width, height1);
        this.tableToShrink.ForcedDesiredSize = (SizeF) new Size(this.tableToShrink.Size.Width, height2);
        this.GridViewElement.Panel.InvalidateArrange();
        this.GridViewElement.Panel.UpdateLayout();
      }
      else
      {
        if (this.GridViewElement.SplitMode != RadGridViewSplitMode.Vertical)
          return;
        int width1 = this.tableSize.Width + (this.mouseDownLoction.X - currentLocation.X);
        int width2 = this.tableToResize.Parent.Size.Width - width1;
        if (width1 < 100 || width2 < 100)
          return;
        this.tableToResize.ForcedDesiredSize = (SizeF) new Size(width1, this.tableSize.Height);
        this.tableToShrink.ForcedDesiredSize = (SizeF) new Size(width2, this.tableToShrink.Size.Height);
      }
    }

    protected GridTableElement GetGridTableElementAtPoint(Point point)
    {
      for (RadElement radElement = ((RadControl) this.GridViewElement.ElementTree.Control).ElementTree.GetElementAtPoint(point); radElement != null; radElement = radElement.Parent)
      {
        GridTableElement gridTableElement = radElement as GridTableElement;
        if (gridTableElement != null && gridTableElement.Parent == this.GridViewElement.Panel)
          return gridTableElement;
      }
      return (GridTableElement) null;
    }

    private void ResetControlCursor()
    {
      if (!(this.originalCursor != (Cursor) null))
        return;
      this.GridViewElement.ElementTree.Control.Cursor = this.originalCursor;
      this.originalCursor = (Cursor) null;
    }

    private bool ResetFieldValues()
    {
      this.tableSize = Size.Empty;
      this.ResetControlCursor();
      this.mouseDownLoction = Point.Empty;
      if (this.tableToResize == null)
        return false;
      this.GridControl.Capture = false;
      this.tableToResize = (GridTableElement) null;
      return true;
    }

    private GridTableElement FindTableElement(
      int index,
      RadElementCollection children)
    {
      for (int index1 = index; index1 < children.Count; ++index1)
      {
        if (children[index1] is GridTableElement)
          return (GridTableElement) children[index1];
      }
      return (GridTableElement) null;
    }
  }
}
