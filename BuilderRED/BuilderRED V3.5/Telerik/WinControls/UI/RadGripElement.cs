// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadGripElement : RadElement
  {
    private Point downPoint = Point.Empty;
    private const int mouseResizeOffset = 15;
    private ImagePrimitive image;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MaxSize = new Size(4, 4);
      this.Margin = new Padding(7);
      this.Alignment = ContentAlignment.BottomRight;
      this.ZIndex = 1000;
    }

    protected override void CreateChildElements()
    {
      this.image = new ImagePrimitive();
      this.image.Class = "GripImage";
      this.Children.Add((RadElement) this.image);
    }

    public ImagePrimitive Image
    {
      get
      {
        return this.image;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.downPoint = e.Location;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      Cursor.Current = Cursors.Default;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      Form form = this.ElementTree.Control.FindForm();
      if (form == null)
        return;
      int wParam = 0;
      RadForm radForm = form as RadForm;
      FormBorderStyle formBorderStyle = radForm == null ? form.FormBorderStyle : radForm.FormBorderStyle;
      if (form is RadRibbonForm)
        formBorderStyle = ((RadRibbonForm) form).FormBorderStyle;
      if (this.Parent == null || form.WindowState == FormWindowState.Maximized || formBorderStyle == FormBorderStyle.None && (!(form is ShapedForm) || !(form as ShapedForm).AllowResize) && !(form is RadRibbonForm) || (formBorderStyle == FormBorderStyle.Fixed3D || formBorderStyle == FormBorderStyle.FixedDialog || (formBorderStyle == FormBorderStyle.FixedSingle || formBorderStyle == FormBorderStyle.FixedToolWindow)))
        return;
      if (this.RightToLeft)
      {
        if (e.X < 15 && e.Y > this.Parent.Size.Height - 15)
        {
          Cursor.Current = Cursors.SizeNESW;
          wParam = 16;
        }
      }
      else if (e.X > this.Parent.Size.Width - 15 && e.Y > this.Parent.Size.Height - 15)
      {
        Cursor.Current = Cursors.SizeNWSE;
        wParam = 17;
      }
      if (e.Button != MouseButtons.Left || !(this.downPoint != e.Location))
        return;
      Telerik.WinControls.NativeMethods.ReleaseCapture();
      Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) this, form.Handle), 161, wParam, IntPtr.Zero);
    }
  }
}
