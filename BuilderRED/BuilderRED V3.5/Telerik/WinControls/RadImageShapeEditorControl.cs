// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadImageShapeEditorControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class RadImageShapeEditorControl : Control
  {
    private static Size gripSize = new Size(17, 17);
    private RadImageShape shape;

    public RadImageShapeEditorControl()
      : this((RadImageShape) null)
    {
    }

    public RadImageShapeEditorControl(RadImageShape shape)
    {
      this.shape = shape;
      this.ResizeRedraw = true;
      this.DoubleBuffered = true;
    }

    public RadImageShape Shape
    {
      get
      {
        return this.shape;
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Rectangle clientRect = this.GetClientRect();
      if (this.shape != null)
      {
        Rectangle rectangle = clientRect;
        rectangle.Inflate(-5, -5);
        rectangle.Width -= RadImageShapeEditorControl.gripSize.Width;
        rectangle.Height -= RadImageShapeEditorControl.gripSize.Height;
        this.shape.Paint(e.Graphics, (RectangleF) rectangle);
      }
      ControlPaint.DrawSizeGrip(e.Graphics, Color.Gray, this.GetGripRect());
      --clientRect.Width;
      --clientRect.Height;
      e.Graphics.DrawRectangle(Pens.Black, clientRect);
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 132 || !this.GetGripRect().Contains(this.PointToClient(Control.MousePosition)))
        return;
      m.Result = (IntPtr) 17;
    }

    private Rectangle GetClientRect()
    {
      return LayoutUtils.DeflateRect(this.ClientRectangle, this.Padding);
    }

    private Rectangle GetGripRect()
    {
      Rectangle clientRect = this.GetClientRect();
      return new Rectangle(clientRect.Right - RadImageShapeEditorControl.gripSize.Width, clientRect.Bottom - RadImageShapeEditorControl.gripSize.Height, RadImageShapeEditorControl.gripSize.Width, RadImageShapeEditorControl.gripSize.Height);
    }
  }
}
