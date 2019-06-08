// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowScroller
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RowScroller : ItemScroller<GridViewRowInfo>
  {
    private GridTableElement tableElement;

    public RowScroller(GridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    protected GridTableElement TableElement
    {
      get
      {
        return this.tableElement;
      }
    }

    private bool CanUpdateScrollRange
    {
      get
      {
        if (this.tableElement != null && this.tableElement.ViewElement != null)
          return this.tableElement.ViewElement.Visibility == ElementVisibility.Visible;
        return false;
      }
    }

    public override void UpdateScrollRange()
    {
      if (!this.CanUpdateScrollRange)
        return;
      base.UpdateScrollRange();
    }

    public override void UpdateScrollRange(int width, bool updateScrollValue)
    {
      if (!this.CanUpdateScrollRange)
        return;
      base.UpdateScrollRange(width, updateScrollValue);
    }

    protected override bool UpdateOnScroll(ScrollEventArgs e)
    {
      if (e.Type == ScrollEventType.EndScroll)
      {
        if (this.ScrollMode == ItemScrollerScrollModes.Deferred && this.TableElement.GridViewElement.AutoSizeRows)
        {
          this.TableElement.UpdateLayout();
          while (this.Scrollbar.Value > this.Scrollbar.Maximum - this.Scrollbar.LargeChange - 1)
            this.Scrollbar.Value = this.Scrollbar.Maximum - this.Scrollbar.LargeChange - 1;
        }
        else if (this.ScrollMode == ItemScrollerScrollModes.Smooth)
        {
          if (this.TableElement.GridViewElement.AutoSizeRows)
            this.TableElement.UpdateLayout();
          this.OnScrollerUpdated(EventArgs.Empty);
        }
      }
      return base.UpdateOnScroll(e);
    }

    public void ScrollToFirstRow()
    {
      this.ScrollToBegin();
      this.Scrollbar.Value = this.Scrollbar.Minimum;
    }

    public void ScrollToLastRow()
    {
      this.ScrollToEnd();
    }

    protected override void OnToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
    {
      ItemScrollerToolTipTextNeededEventArgs<GridViewRowInfo> textNeededEventArgs1 = e as ItemScrollerToolTipTextNeededEventArgs<GridViewRowInfo>;
      GridElementToolTipTextNeededEventArgs textNeededEventArgs2 = new GridElementToolTipTextNeededEventArgs(e.ToolTip, textNeededEventArgs1.ItemIndex, textNeededEventArgs1.Item, e.ToolTipText);
      base.OnToolTipTextNeeded(sender, (ToolTipTextNeededEventArgs) textNeededEventArgs2);
      e.ToolTipText = textNeededEventArgs2.ToolTipText;
    }

    protected override void OnScrollerUpdated(EventArgs e)
    {
      base.OnScrollerUpdated(e);
      this.tableElement.Invalidate();
    }
  }
}
