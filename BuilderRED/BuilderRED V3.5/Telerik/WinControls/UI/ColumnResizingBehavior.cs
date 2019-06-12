// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnResizingBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ColumnResizingBehavior
  {
    private bool allowColumnResize = true;
    protected Point lastMousePosition = Point.Empty;
    private ListViewDetailColumn resizedColumn;
    private bool isResizing;
    private Cursor originalMouseCursor;
    private RadListViewElement owner;

    public ColumnResizingBehavior(RadListViewElement owner)
    {
      this.owner = owner;
    }

    public RadListViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public bool AllowColumnResize
    {
      get
      {
        return this.allowColumnResize;
      }
      set
      {
        this.allowColumnResize = value;
      }
    }

    public ListViewDetailColumn ResizedColumn
    {
      get
      {
        return this.resizedColumn;
      }
    }

    public bool IsResizing
    {
      get
      {
        return this.isResizing;
      }
    }

    public virtual bool BeginResize(ListViewDetailColumn column, Point mousePosition)
    {
      if (!this.allowColumnResize || this.isResizing || this.owner.ViewType != ListViewType.DetailsView)
        return false;
      this.owner.Capture = true;
      this.lastMousePosition = mousePosition;
      this.isResizing = true;
      this.resizedColumn = column;
      this.originalMouseCursor = this.owner.ElementTree.Control.Cursor;
      this.owner.ElementTree.Control.Cursor = Cursors.SizeWE;
      return true;
    }

    public virtual bool Resize(int offset)
    {
      if (!this.isResizing || this.resizedColumn == null)
        return false;
      float width = this.resizedColumn.Width;
      this.resizedColumn.Width += (float) offset;
      return (double) width != (double) this.resizedColumn.Width;
    }

    public virtual void HandleMouseMove(Point mousePosition)
    {
      if (!this.IsResizing)
        return;
      int offset = mousePosition.X - this.lastMousePosition.X;
      if (this.owner.RightToLeft)
        offset = -offset;
      if (!this.Resize(offset))
        return;
      this.lastMousePosition.X = mousePosition.X;
    }

    public virtual void EndResize()
    {
      this.owner.Capture = false;
      this.isResizing = false;
      this.resizedColumn = (ListViewDetailColumn) null;
      if (!(this.originalMouseCursor != (Cursor) null))
        return;
      this.owner.ElementTree.Control.Cursor = this.originalMouseCursor;
      this.originalMouseCursor = (Cursor) null;
    }
  }
}
