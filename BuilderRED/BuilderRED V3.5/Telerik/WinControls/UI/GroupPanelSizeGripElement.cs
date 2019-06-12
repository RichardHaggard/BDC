// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupPanelSizeGripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GroupPanelSizeGripElement : LightVisualElement
  {
    private Point downPoint;
    private bool mouseIsDown;
    private int initialHeight;
    private Cursor originalCursor;

    public GroupPanelElement GroupPanel
    {
      get
      {
        return this.FindAncestor<GroupPanelElement>();
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (GroupPanelSizeGripElement);
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.MinSize = new Size(0, 4);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left || this.GroupPanel.ScrollView.VScrollBar.Visibility != ElementVisibility.Visible && this.GroupPanel.ScrollView.MaxSize.Height == this.GroupPanel.ScrollView.MinSize.Height)
        return;
      this.downPoint = e.Location;
      this.mouseIsDown = true;
      this.Capture = true;
      this.initialHeight = Math.Min(this.GroupPanel.ScrollView.ViewElement.ControlBoundingRectangle.Height, this.GroupPanel.ScrollView.MaxSize.Height);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.mouseIsDown)
      {
        int num = e.Location.Y - this.downPoint.Y;
        if (this.GroupPanel.ScrollView.MinSize.Height >= this.initialHeight + num)
          return;
        this.GroupPanel.ScrollView.MaxSize = new Size(0, this.initialHeight + num);
      }
      else
      {
        if (e.Button != MouseButtons.None || this.ElementTree.Control.Capture || !(this.originalCursor == (Cursor) null))
          return;
        this.originalCursor = this.ElementTree.Control.Cursor;
        this.ElementTree.Control.Cursor = Cursors.SizeNS;
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (!(this.originalCursor != (Cursor) null))
        return;
      this.ElementTree.Control.Cursor = this.originalCursor;
      this.originalCursor = (Cursor) null;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.mouseIsDown)
      {
        this.mouseIsDown = false;
        this.Capture = false;
      }
      if (!(this.originalCursor != (Cursor) null))
        return;
      this.ElementTree.Control.Cursor = this.originalCursor;
      this.originalCursor = (Cursor) null;
    }
  }
}
