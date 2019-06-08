// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SizeGripItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class SizeGripItem : RadItem
  {
    private bool allowSizing = true;
    private SizeGripItem.SizingModes sizingMode = SizeGripItem.SizingModes.Both;
    private bool shouldAspectRootElement = true;
    private DateTime lastCall = DateTime.MinValue;
    internal static SizeGripItem activeGrip;
    private FillPrimitive fill;
    private BorderPrimitive border;
    private ImagePrimitive image;
    private Point mouseDown;
    private Point oldMousePos;
    private Size downSize;
    private Point downPos;
    private Rectangle downRect;
    private bool shouldAspectMinSize;

    public event ValueChangingEventHandler Sized;

    public event ValueChangingEventHandler Sizing;

    protected virtual void OnSized(object sender, ValueChangingEventArgs args)
    {
      if (this.Sized == null)
        return;
      this.Sized((object) this, args);
    }

    protected virtual void OnSizing(object sender, ValueChangingEventArgs args)
    {
      if (this.Sizing == null)
        return;
      this.Sizing((object) this, args);
    }

    public bool AllowSizing
    {
      get
      {
        return this.allowSizing;
      }
      set
      {
        this.allowSizing = value;
      }
    }

    public ImagePrimitive Image
    {
      get
      {
        return this.image;
      }
    }

    [DefaultValue(SizeGripItem.SizingModes.Both)]
    public SizeGripItem.SizingModes SizingMode
    {
      get
      {
        return this.sizingMode;
      }
      set
      {
        this.sizingMode = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool ShouldAspectRootElement
    {
      get
      {
        return this.shouldAspectRootElement;
      }
      set
      {
        this.shouldAspectRootElement = value;
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool ShouldAspectMinSize
    {
      get
      {
        return this.shouldAspectMinSize;
      }
      set
      {
        this.shouldAspectMinSize = value;
      }
    }

    protected override void CreateChildElements()
    {
      this.fill = new FillPrimitive();
      this.fill.Class = "GripItemFill";
      this.Children.Add((RadElement) this.fill);
      this.border = new BorderPrimitive();
      this.border.Class = "GripItemBorder";
      this.Children.Add((RadElement) this.border);
      this.image = new ImagePrimitive();
      this.image.MinSize = new Size(8, 7);
      this.Children.Add((RadElement) this.image);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.allowSizing)
        return;
      this.mouseDown = Control.MousePosition;
      this.OnSizing((object) this, new ValueChangingEventArgs((object) this.mouseDown));
      this.oldMousePos = this.mouseDown;
      this.downPos = this.ElementTree.Control.PointToScreen(Point.Empty);
      this.downSize = this.ElementTree.Control.Size;
      this.downRect = new Rectangle(this.downPos, this.downSize);
      this.Capture = true;
      SizeGripItem.activeGrip = this;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (!this.allowSizing || e.Button != MouseButtons.Left || (SizeGripItem.activeGrip != this || this.oldMousePos == Control.MousePosition))
        return;
      int dx = this.sizingMode == SizeGripItem.SizingModes.Vertical ? 0 : Control.MousePosition.X - this.mouseDown.X;
      int dy = this.sizingMode == SizeGripItem.SizingModes.Horizontal ? 0 : Control.MousePosition.Y - this.mouseDown.Y;
      if (this.IsSizerAtBottom())
      {
        if (this.RightToLeft)
          this.SizeControlBottomLeft(dx, dy);
        else
          this.SizeControlBottomRight(dx, dy);
      }
      else if (this.RightToLeft)
        this.SizeControlTopLeft(dx, dy);
      else
        this.SizeControlTopRight(dx, dy);
      this.oldMousePos = Control.MousePosition;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.ElementTree.Control.Cursor = Cursors.Arrow;
      this.Capture = false;
      if (!this.allowSizing || SizeGripItem.activeGrip != this)
        return;
      SizeGripItem.activeGrip = (SizeGripItem) null;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.sizingMode == SizeGripItem.SizingModes.Vertical)
        this.ElementTree.Control.Cursor = Cursors.SizeNS;
      else if (this.IsSizerAtBottom())
        this.ElementTree.Control.Cursor = !this.RightToLeft ? Cursors.SizeNWSE : Cursors.SizeNESW;
      else
        this.ElementTree.Control.Cursor = !this.RightToLeft ? Cursors.SizeNESW : Cursors.SizeNWSE;
      if (!this.allowSizing)
        base.OnMouseEnter(e);
      else
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      this.ElementTree.Control.Cursor = Cursors.Arrow;
      if (!this.allowSizing)
        base.OnMouseLeave(e);
      else
        base.OnMouseLeave(e);
    }

    public override bool RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
        this.AngleTransform = !value ? 0.0f : 90f;
      }
    }

    private bool IsSizerAtBottom()
    {
      return DockLayoutPanel.GetDock(this.Parent) == Dock.Bottom;
    }

    private Size GetMinControlSize()
    {
      if (!this.IsInValidState(true))
        return Size.Empty;
      Size size = this.Parent.Children[3].Size;
      if (this.shouldAspectRootElement)
        size = Size.Round(this.ElementTree.RootElement.DesiredSize);
      if (this.shouldAspectMinSize)
        size = Size.Round((SizeF) this.ElementTree.Control.MinimumSize) + this.Size;
      size.Width = Math.Max(size.Width, this.ElementTree.Control.MinimumSize.Width);
      size.Width = Math.Max(size.Width, 0);
      size.Height = Math.Max(size.Height, this.ElementTree.Control.MinimumSize.Height);
      size.Height = Math.Max(size.Height, 0);
      return size;
    }

    private void SizeControlTopLeft(int dx, int dy)
    {
      Size downSize = this.downSize;
      downSize.Width -= dx;
      downSize.Height -= dy;
      Size minControlSize = this.GetMinControlSize();
      Size maximumSize = this.ElementTree.Control.MaximumSize;
      if (minControlSize.Width > 0 && downSize.Width < minControlSize.Width)
        dx = this.downRect.Width - minControlSize.Width;
      if (maximumSize.Width > 0 && downSize.Width > maximumSize.Width)
        dx = this.downRect.Width - maximumSize.Width;
      if (minControlSize.Height > 0 && downSize.Height < minControlSize.Height)
        dy = this.downRect.Height - minControlSize.Height;
      if (maximumSize.Height > 0 && downSize.Height > maximumSize.Height)
        dy = this.downRect.Height - maximumSize.Height;
      Rectangle downRect = this.downRect;
      downRect.X += dx;
      downRect.Y += dy;
      downRect.Width -= dx;
      downRect.Height -= dy;
      this.ElementTree.Control.Bounds = downRect;
      this.OnSized((object) this, new ValueChangingEventArgs((object) new SizeF((float) dx, (float) dy)));
    }

    private void SizeControlTopRight(int dx, int dy)
    {
      Size downSize = this.downSize;
      downSize.Width += dx;
      downSize.Height -= dy;
      Size minControlSize = this.GetMinControlSize();
      Size maximumSize = this.ElementTree.Control.MaximumSize;
      if (minControlSize.Width > 0 && downSize.Width < minControlSize.Width)
        downSize.Width = minControlSize.Width;
      if (maximumSize.Width > 0 && downSize.Width > maximumSize.Width)
        downSize.Width = maximumSize.Width;
      if (minControlSize.Height > 0 && downSize.Height < minControlSize.Height)
        dy = this.downRect.Height - minControlSize.Height;
      if (maximumSize.Height > 0 && downSize.Height > maximumSize.Height)
        dy = this.downRect.Height - maximumSize.Height;
      Rectangle downRect = this.downRect;
      downRect.Y += dy;
      downRect.Width = downSize.Width;
      downRect.Height -= dy;
      this.ElementTree.Control.Bounds = downRect;
      this.OnSized((object) this, new ValueChangingEventArgs((object) new SizeF((float) dx, (float) dy)));
    }

    private void SizeControlBottomLeft(int dx, int dy)
    {
      Size downSize = this.downSize;
      downSize.Width -= dx;
      downSize.Height += dy;
      Size minControlSize = this.GetMinControlSize();
      Size maximumSize = this.ElementTree.Control.MaximumSize;
      if (minControlSize.Width > 0 && downSize.Width < minControlSize.Width)
        dx = this.downRect.Width - minControlSize.Width;
      if (maximumSize.Width > 0 && downSize.Width > maximumSize.Width)
        dx = this.downRect.Width - maximumSize.Width;
      if (minControlSize.Height > 0 && downSize.Height < minControlSize.Height)
        downSize.Height = minControlSize.Height;
      if (maximumSize.Height > 0 && downSize.Height > maximumSize.Height)
        downSize.Height = maximumSize.Height;
      Rectangle downRect = this.downRect;
      downRect.X += dx;
      downRect.Width -= dx;
      downRect.Height = downSize.Height;
      this.ElementTree.Control.Bounds = downRect;
      this.OnSized((object) this, new ValueChangingEventArgs((object) new SizeF((float) dx, (float) dy)));
    }

    private void SizeControlBottomRight(int dx, int dy)
    {
      if (DateTime.Now - this.lastCall < TimeSpan.FromMilliseconds(40.0))
        return;
      this.lastCall = DateTime.Now;
      Rectangle downRect = this.downRect;
      downRect.Width += dx;
      downRect.Height += dy;
      Size minControlSize = this.GetMinControlSize();
      Size maximumSize = this.ElementTree.Control.MaximumSize;
      if (minControlSize.Width > 0 && downRect.Width < minControlSize.Width)
        downRect.Width = minControlSize.Width;
      if (maximumSize.Width > 0 && downRect.Width > maximumSize.Width)
        downRect.Width = maximumSize.Width;
      if (minControlSize.Height > 0 && downRect.Height < minControlSize.Height)
        downRect.Height = minControlSize.Height;
      if (maximumSize.Height > 0 && downRect.Height > maximumSize.Height)
        downRect.Height = maximumSize.Height;
      foreach (Control control in (ArrangedElementCollection) this.ElementTree.Control.Controls)
        control.SuspendLayout();
      this.ElementTree.Control.Size = downRect.Size;
      foreach (Control control in (ArrangedElementCollection) this.ElementTree.Control.Controls)
        control.ResumeLayout(true);
      this.OnSized((object) this, new ValueChangingEventArgs((object) new SizeF((float) dx, (float) dy)));
    }

    public enum SizingModes
    {
      Horizontal,
      Vertical,
      Both,
    }
  }
}
