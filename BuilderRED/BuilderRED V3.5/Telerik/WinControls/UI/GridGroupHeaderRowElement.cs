// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupHeaderRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridGroupHeaderRowElement : GridRowElement
  {
    public static RadProperty IsExpandedProperty = RadProperty.Register(nameof (IsExpanded), typeof (bool), typeof (GridGroupHeaderRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ContainsGroupsProperty = RadProperty.Register(nameof (ContainsGroups), typeof (bool), typeof (GridGroupHeaderRowElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private GridGroupContentCellElement contentCell;
    private GridGroupExpanderCellElement expanderCell;
    private int groupLevel;

    public override void Initialize(GridViewRowInfo rowInfo)
    {
      if (this.Children.Count == 0)
      {
        this.RowInfo = rowInfo;
        bool flag = false;
        foreach (GridViewColumn renderColumn in (IEnumerable<GridViewColumn>) this.TableElement.ViewElement.RowLayout.RenderColumns)
        {
          if (renderColumn is GridViewIndentColumn || renderColumn is GridViewRowHeaderColumn)
          {
            GridCellElement cell = this.CreateCell(renderColumn);
            if (cell != null)
            {
              if (cell is GridGroupExpanderCellElement)
              {
                this.expanderCell = (GridGroupExpanderCellElement) cell;
                flag = true;
              }
              if (cell is GridIndentCellElement && flag)
                cell.ThemeRole = "HierarchyIndentCell";
              this.Children.Add((RadElement) cell);
            }
            else
              break;
          }
          if (renderColumn is GridViewDataColumn)
            break;
        }
        this.contentCell = (GridGroupContentCellElement) this.CreateCell((GridViewColumn) null);
        this.Children.Add((RadElement) this.contentCell);
        this.RowInfo = (GridViewRowInfo) null;
      }
      GridViewGroupRowInfo viewGroupRowInfo = (GridViewGroupRowInfo) rowInfo;
      this.groupLevel = viewGroupRowInfo.GroupLevel;
      if (viewGroupRowInfo.ChildRows.Count > 0)
      {
        int num1 = (int) this.SetValue(GridGroupHeaderRowElement.ContainsGroupsProperty, (object) (viewGroupRowInfo.Group.Groups.Count > 0));
      }
      else
      {
        int num2 = (int) this.SetValue(GridGroupHeaderRowElement.ContainsGroupsProperty, (object) false);
      }
      base.Initialize(rowInfo);
      int index = 0;
      for (IList<GridViewColumn> renderColumns = this.TableElement.ViewElement.RowLayout.RenderColumns; index < this.Children.Count && index < renderColumns.Count; ++index)
      {
        GridCellElement child = this.Children[index] as GridCellElement;
        GridViewColumn column = renderColumns[index];
        if (child != null)
        {
          if (child is GridGroupContentCellElement)
            column = (GridViewColumn) null;
          child.Initialize(column, (GridRowElement) this);
        }
      }
      if (this.expanderCell == null)
        return;
      this.expanderCell.Initialize(this.expanderCell.ColumnInfo, (GridRowElement) this);
    }

    public override void Detach()
    {
      foreach (GridCellElement child in this.Children)
        (child as GridVirtualizedCellElement)?.Detach();
      base.Detach();
    }

    public override bool IsCompatible(GridViewRowInfo data, object context)
    {
      GridViewGroupRowInfo viewGroupRowInfo = data as GridViewGroupRowInfo;
      if (viewGroupRowInfo != null)
        return viewGroupRowInfo.GroupLevel == this.groupLevel;
      return false;
    }

    public override Type GetCellType(GridViewColumn column)
    {
      GridViewGroupRowInfo rowInfo = (GridViewGroupRowInfo) this.RowInfo;
      GridViewIndentColumn viewIndentColumn = column as GridViewIndentColumn;
      if (viewIndentColumn != null)
      {
        if (rowInfo.Group.Level == viewIndentColumn.IndentLevel)
          return typeof (GridGroupExpanderCellElement);
        if (rowInfo.Group.Level < viewIndentColumn.IndentLevel)
          return (Type) null;
      }
      if (column == null)
        return typeof (GridGroupContentCellElement);
      return base.GetCellType(column);
    }

    public override void UpdateContent()
    {
      foreach (GridCellElement child in this.Children)
        child.SetContent();
    }

    protected override bool ShouldUsePaintBuffer()
    {
      return false;
    }

    [Category("Appearance")]
    public virtual bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(GridGroupHeaderRowElement.IsExpandedProperty);
      }
    }

    [Category("Appearance")]
    public virtual bool ContainsGroups
    {
      get
      {
        return (bool) this.GetValue(GridGroupHeaderRowElement.ContainsGroupsProperty);
      }
    }

    public override bool CanApplyFormatting
    {
      get
      {
        return false;
      }
    }

    public GridGroupContentCellElement ContentCell
    {
      get
      {
        return this.contentCell;
      }
    }

    public GridGroupExpanderCellElement ExpanderCell
    {
      get
      {
        return this.expanderCell;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.TableElement.ViewElement.RowLayout.MeasureRow(availableSize);
      SizeF elementSize = this.TableElement.RowScroller.ElementProvider.GetElementSize(this.RowInfo);
      if ((double) elementSize.Width != 0.0)
        availableSize.Width = Math.Min(availableSize.Width, elementSize.Width);
      if (!float.IsInfinity(availableSize.Height))
        availableSize.Height = elementSize.Height;
      this.Layout.Measure(this.GetClientRectangle(availableSize).Size);
      foreach (GridCellElement child in this.Children)
      {
        if (child != this.contentCell)
        {
          child.Measure(availableSize);
          availableSize.Width -= child.DesiredSize.Width + (float) this.TableElement.CellSpacing;
        }
      }
      if (this.contentCell != null)
        this.contentCell.Measure(availableSize);
      if (float.IsInfinity(availableSize.Height))
      {
        int num = this.RowInfo.Height == -1 ? (int) elementSize.Height : this.RowInfo.Height;
        elementSize.Height = Math.Max(this.contentCell.DesiredSize.Height, (float) this.RowInfo.MinHeight);
        this.RowInfo.SuspendPropertyNotifications();
        this.RowInfo.Height = (int) elementSize.Height;
        this.RowInfo.ResumePropertyNotifications();
        this.TableElement.RowScroller.UpdateScrollRange(this.TableElement.RowScroller.Scrollbar.Maximum + (this.RowInfo.Height - num), false);
      }
      return elementSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float num1 = 0.0f;
      foreach (GridCellElement child in this.Children)
      {
        if (child != this.contentCell)
        {
          float y = clientRectangle.Y;
          float width = this.TableElement.ColumnScroller.ElementProvider.GetElementSize(child.ColumnInfo).Width;
          float height = clientRectangle.Height;
          float num2 = clientRectangle.Right;
          if (child.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
          {
            y = 0.0f;
            height = finalSize.Height;
            num2 = finalSize.Width;
          }
          child.Arrange(new RectangleF(this.RightToLeft ? num2 - num1 - width : num1, y, width, height));
          num1 += width + (float) this.TableElement.CellSpacing;
        }
      }
      if (this.contentCell != null)
      {
        float width = clientRectangle.Width - num1;
        float num2 = clientRectangle.Left;
        float y = clientRectangle.Top;
        float height = clientRectangle.Height;
        if (this.contentCell.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
        {
          width = finalSize.Width - num1;
          num2 = 0.0f;
          y = 0.0f;
          height = finalSize.Height;
        }
        this.contentCell.Arrange(new RectangleF(this.RightToLeft ? num2 : num1, y, width, height));
      }
      return finalSize;
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      if (dragObject.GetDataContext() is GroupFieldDragDropContext)
        return true;
      return base.ProcessDragOver(currentMouseLocation, dragObject);
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      GroupFieldDragDropContext dataContext = dragObject.GetDataContext() as GroupFieldDragDropContext;
      if (dataContext != null)
        dataContext.ViewTemplate.GroupDescriptors.Remove(dataContext.GroupDescription);
      else
        base.ProcessDragDrop(dropLocation, dragObject);
    }
  }
}
