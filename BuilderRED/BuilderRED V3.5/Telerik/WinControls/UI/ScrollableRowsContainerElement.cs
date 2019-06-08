// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollableRowsContainerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ScrollableRowsContainerElement : VirtualizedStackContainer<GridViewRowInfo>
  {
    private ScrollableRowsUpdateAction updateAction = ScrollableRowsUpdateAction.None;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ItemSpacing = -1;
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.Orientation = Orientation.Vertical;
    }

    public void UpdateOnScroll(ScrollableRowsUpdateAction action)
    {
      this.updateAction = action;
      foreach (RadElement child in this.Children)
        child.InvalidateMeasure();
      this.InvalidateMeasure();
    }

    public void ClearItems()
    {
      this.SuspendLayout();
      this.DisposeChildren();
      this.ResumeLayout(true);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RowsContainerElement parent = (RowsContainerElement) this.Parent;
      bool flag = parent.RowLayout.DesiredSize == SizeF.Empty;
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (flag)
        parent.TableElement.RowScroller.UpdateScrollRange();
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      this.CorrectScrollbarRangeInHierarchy();
      return finalSize;
    }

    protected override SizeF MeasureElementCore(RadElement element, SizeF availableSize)
    {
      if (element is GridDetailViewRowElement)
      {
        if (this.Orientation == Orientation.Vertical)
        {
          if (!this.FitElementsToSize)
            availableSize.Width = float.PositiveInfinity;
          if (((GridViewDetailsRowInfo) ((GridRowElement) element).RowInfo).IsLastRow)
            availableSize.Height = float.PositiveInfinity;
        }
        else if (!this.FitElementsToSize)
          availableSize.Height = float.PositiveInfinity;
        element.Measure(availableSize);
        return element.DesiredSize;
      }
      if (!((RowsContainerElement) this.Parent).TableElement.GridViewElement.AutoSizeRows)
        return base.MeasureElementCore(element, availableSize);
      element.Measure(new SizeF(availableSize.Width, float.PositiveInfinity));
      return element.DesiredSize;
    }

    protected override int FindCompatibleElement(int position, GridViewRowInfo data)
    {
      if (data is GridViewFilteringRowInfo)
      {
        for (int index = position + 1; index < this.Children.Count; ++index)
        {
          if ((object) ((GridRowElement) this.Children[index]).RowInfo.GetType() == (object) data.GetType())
            return index;
        }
      }
      if (data is GridViewDetailsRowInfo)
      {
        for (int index = position + 1; index < this.Children.Count; ++index)
        {
          GridViewDetailsRowInfo rowInfo = ((GridRowElement) this.Children[index]).RowInfo as GridViewDetailsRowInfo;
          if (rowInfo != null && ((GridViewDetailsRowInfo) data).ChildViewInfo == rowInfo.ChildViewInfo)
            return index;
        }
      }
      return -1;
    }

    protected override void RemoveElement(int position)
    {
      RadGridViewElement gridViewElement = ((RowsContainerElement) this.Parent).TableElement.GridViewElement;
      BaseGridEditor activeEditor = gridViewElement.ActiveEditor as BaseGridEditor;
      if (gridViewElement.IsInEditMode && (activeEditor == null || !activeEditor.IsInBeginEditMode))
      {
        GridViewRowInfo gridViewRowInfo = ((GridRowElement) this.Children[position]).RowInfo;
        GridViewDetailsRowInfo viewDetailsRowInfo = gridViewRowInfo as GridViewDetailsRowInfo;
        if (viewDetailsRowInfo != null)
          gridViewRowInfo = (GridViewRowInfo) viewDetailsRowInfo.Owner;
        if (gridViewRowInfo == gridViewElement.CurrentRow || gridViewElement.CurrentRow != null && gridViewElement.CurrentRow.ViewInfo != null && (gridViewElement.CurrentRow.ViewInfo.ParentRow != null && !gridViewElement.CurrentRow.ViewInfo.ParentRow.IsPinned) && gridViewElement.CurrentRow.ViewInfo.ParentRow.ViewInfo == gridViewRowInfo.ViewInfo)
          gridViewElement.CloseEditor();
      }
      base.RemoveElement(position);
    }

    protected override IVirtualizedElement<GridViewRowInfo> UpdateElement(
      int position,
      GridViewRowInfo data)
    {
      IVirtualizedElement<GridViewRowInfo> virtualizedElement = this.updateAction != ScrollableRowsUpdateAction.ScrollUp ? base.UpdateElement(position, data) : this.UpdateElementOnScrollUp(position, data);
      this.updateAction = ScrollableRowsUpdateAction.None;
      return virtualizedElement;
    }

    protected virtual IVirtualizedElement<GridViewRowInfo> UpdateElementOnScrollUp(
      int position,
      GridViewRowInfo data)
    {
      object elementContext = this.GetElementContext();
      if (position < this.Children.Count)
      {
        IVirtualizedElement<GridViewRowInfo> child = (IVirtualizedElement<GridViewRowInfo>) this.Children[position];
        if (!this.ElementProvider.ShouldUpdate(child, data, elementContext))
          return child;
      }
      IVirtualizedElement<GridViewRowInfo> element = this.ElementProvider.GetElement(data, elementContext);
      this.InsertElement(position, element, data);
      return element;
    }

    public override void ResetStyleSettings(bool recursive, RadProperty property)
    {
      this.SuspendLayout();
      base.ResetStyleSettings(recursive, property);
      this.ResumeLayout(true);
    }

    private void CorrectScrollbarRangeInHierarchy()
    {
      GridTableElement tableElement = ((RowsContainerElement) this.Parent).TableElement;
      RadGridViewElement gridViewElement = tableElement.GridViewElement;
      RadScrollBarElement vscrollBar = tableElement.VScrollBar;
      MasterGridViewTemplate masterTemplate = tableElement.MasterTemplate;
      if (masterTemplate == null || masterTemplate.Templates.Count == 0 || (gridViewElement.UseScrollbarsInHierarchy || tableElement.ViewInfo.ParentRow != null) || (this.Children.Count == 0 || vscrollBar.Value < vscrollBar.Maximum - vscrollBar.LargeChange + 1))
        return;
      GridDetailViewRowElement child1 = this.Children[this.Children.Count - 1] as GridDetailViewRowElement;
      if (child1 == null || child1.ContentCell.ChildTableElement.ViewInfo == null)
        return;
      ScrollableRowsContainerElement scrollableRows = child1.ContentCell.ChildTableElement.ViewElement.ScrollableRows;
      int count = scrollableRows.Children.Count;
      GridTraverser gridTraverser = new GridTraverser(child1.ContentCell.ChildTableElement.ViewInfo);
      gridTraverser.TraversalMode = GridTraverser.TraversalModes.ScrollableRows;
      int num1 = 0;
      while (gridTraverser.MoveNext())
      {
        ++num1;
        if (num1 > count)
        {
          int height = (int) child1.ContentCell.ChildTableElement.RowElementProvider.GetElementSize(gridTraverser.Current).Height;
          tableElement.RowScroller.UpdateScrollRange(vscrollBar.Maximum + height, false);
          return;
        }
      }
      if (scrollableRows.Children.Count <= 0)
        return;
      GridRowElement child2 = (GridRowElement) scrollableRows.Children[scrollableRows.Children.Count - 1];
      if (child2.ControlBoundingRectangle.Bottom <= scrollableRows.ControlBoundingRectangle.Bottom)
        return;
      int num2 = child2.ControlBoundingRectangle.Bottom - scrollableRows.ControlBoundingRectangle.Bottom;
      tableElement.RowScroller.UpdateScrollRange(vscrollBar.Maximum + num2, false);
    }
  }
}
