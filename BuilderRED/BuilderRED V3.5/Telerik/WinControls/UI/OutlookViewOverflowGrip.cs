// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.OutlookViewOverflowGrip
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class OutlookViewOverflowGrip : RadPageViewElementBase
  {
    internal int DragDelta = 20;
    private Cursor oldCursor;
    private Point? lastPoint;

    public event OverflowGripDragHandler Dragged;

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (this.oldCursor == (Cursor) null)
        this.oldCursor = new Cursor(this.ElementTree.Control.Cursor.Handle);
      this.ElementTree.Control.Cursor = Cursors.SizeNS;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (!this.Capture)
        this.ElementTree.Control.Cursor = this.oldCursor;
      base.OnMouseLeave(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.Capture = true;
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.Capture = false;
      if (this.ElementTree.Control.Cursor != this.oldCursor)
        this.ElementTree.Control.Cursor = this.oldCursor;
      base.OnMouseUp(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.Capture)
        return;
      Point location = this.ControlBoundingRectangle.Location;
      bool flag = false;
      Point mousePosition = Control.MousePosition;
      if (this.lastPoint.HasValue)
        flag = this.lastPoint.Value.Y > mousePosition.Y;
      this.lastPoint = new Point?(mousePosition);
      Point client = this.ElementTree.Control.PointToClient(mousePosition);
      OverflowEventArgs args = (OverflowEventArgs) null;
      if (client.Y - location.Y > 20 && !flag)
      {
        args = new OverflowEventArgs();
        args.Up = false;
      }
      else if (location.Y - client.Y > 20 && flag)
      {
        args = new OverflowEventArgs();
        args.Up = true;
      }
      if (args == null || this.Dragged == null)
        return;
      this.Dragged((object) this, args);
    }
  }
}
