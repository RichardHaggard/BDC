// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewHeaderCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewHeaderCellElement : GanttViewTextViewCellElement
  {
    private bool isMouseOverResizeRectangle;

    public GanttViewTextViewHeaderCellElement(
      GanttViewTextItemElement owner,
      GanttViewTextViewColumn column)
      : base(owner, column)
    {
    }

    public bool IsMouseOverResizeRectangle
    {
      get
      {
        return this.isMouseOverResizeRectangle;
      }
      set
      {
        this.isMouseOverResizeRectangle = value;
      }
    }

    public override void Synchronize()
    {
      this.Text = this.Data.HeaderText;
      this.FindAncestor<RadGanttViewElement>()?.OnTextViewCellFormatting(new GanttViewTextViewCellFormattingEventArgs((GanttViewDataItem) null, (GanttViewTextViewCellElement) this, this.Data));
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      Point point = this.PointFromControl(e.Location);
      if (point.X < this.Size.Width && (point.X > this.Size.Width - 5 && this.CanBeResized()))
      {
        this.ElementTree.Control.Cursor = Cursors.SizeWE;
        this.IsMouseOverResizeRectangle = true;
      }
      else
      {
        this.ElementTree.Control.Cursor = Cursors.Default;
        this.IsMouseOverResizeRectangle = false;
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.ElementTree.Control.Cursor = Cursors.Default;
      this.IsMouseOverResizeRectangle = false;
    }

    public virtual bool CanBeResized()
    {
      return true;
    }

    public override void AddEditor(IInputEditor editor)
    {
    }

    public override void RemoveEditor(IInputEditor editor)
    {
    }
  }
}
