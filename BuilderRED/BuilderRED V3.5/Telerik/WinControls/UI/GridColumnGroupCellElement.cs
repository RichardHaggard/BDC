// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridColumnGroupCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class GridColumnGroupCellElement : GridHeaderCellElement
  {
    public GridColumnGroupCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void DisposeManagedResources()
    {
      if (this.ContextMenu != null)
      {
        this.ContextMenu.Dispose();
        this.ContextMenu = (RadDropDownMenu) null;
      }
      base.DisposeManagedResources();
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      if (data is GridViewGroupColumn)
        return context is GridTableHeaderRowElement;
      return false;
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (GridHeaderCellElement);
      }
    }

    protected GridViewGroupColumn GridViewGroupColumn
    {
      get
      {
        return this.ColumnInfo as GridViewGroupColumn;
      }
    }

    protected override void CreateContextMenuItems(RadDropDownMenu menu)
    {
      if (this.GridViewGroupColumn.RootColumnGroup == this.GridViewGroupColumn.Group)
        this.CreateColumnPinningMenuItems(menu);
      this.CreateColumnChooserMenuItems(menu);
      if (this.ElementTree == null || this.ElementTree.Control.Site == null)
        return;
      RadMenuItem radMenuItem = new RadMenuItem("Delete Group");
      radMenuItem.Click += new EventHandler(this.deleteItem_Click);
      menu.Items.Add((RadItem) radMenuItem);
    }

    private void deleteItem_Click(object sender, EventArgs e)
    {
      GridViewColumnGroup group = this.GridViewGroupColumn.Group;
      GridViewTemplate viewTemplate = this.ViewTemplate;
      if (group == null)
        return;
      this.HideColumnsInGroup(group);
      if (group.Parent != null)
        group.Parent.Groups.Remove(group);
      else if (group.ParentViewDefinition != null)
        group.ParentViewDefinition.ColumnGroups.Remove(group);
      this.TableElement.ViewElement.RowLayout.InvalidateLayout();
      this.TableElement.ViewElement.RowLayout.InvalidateRenderColumns();
      this.TableElement.ViewElement.UpdateRows(true);
      this.TableElement.UpdateView();
      viewTemplate?.OnNotifyPropertyChanged("ViewDefinition");
    }

    private void HideColumnsInGroup(GridViewColumnGroup group)
    {
      if (group.Rows.Count > 0)
      {
        foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group.Rows)
        {
          foreach (string columnName in (Collection<string>) row.ColumnNames)
            this.ViewTemplate.Columns[columnName].IsVisible = false;
        }
      }
      else
      {
        foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) group.Groups)
          this.HideColumnsInGroup(group1);
      }
    }

    private void MenuItemColumnChooser_Click(object sender, EventArgs e)
    {
      this.GridViewElement.ShowColumnChooser(this.ViewTemplate);
    }

    private void MenuItemHide_Click(object sender, EventArgs e)
    {
      this.GridViewElement.ColumnChooser.Template = this.ColumnInfo.OwnerTemplate;
      this.GridViewGroupColumn.Group.IsVisible = false;
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      return this.CanHideGroup(this.GridViewGroupColumn.Group);
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      if (!this.ViewTemplate.AllowColumnReorder)
        return false;
      if (dataContext is GridViewColumnGroup)
        return true;
      if (dataContext is GridViewGroupColumn)
      {
        GridViewGroupColumn gridViewGroupColumn = (GridViewGroupColumn) dataContext;
        for (GridViewColumnGroup gridViewColumnGroup = this.GridViewGroupColumn.Group; gridViewColumnGroup != null; gridViewColumnGroup = gridViewColumnGroup.Parent)
        {
          if (gridViewColumnGroup == gridViewGroupColumn.Group)
            return false;
        }
      }
      return base.ProcessDragOver(currentMouseLocation, dragObject);
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      RadPosition dropPosition = RadGridViewDragDropService.GetDropPosition(dropLocation, this.Size);
      ColumnGroupsViewDefinition viewDefinition = this.ViewTemplate.ViewDefinition as ColumnGroupsViewDefinition;
      if (viewDefinition != null)
      {
        if (dataContext is GridViewColumnGroup)
        {
          ((GridViewColumnGroup) dataContext).IsVisible = true;
          this.TableElement.UpdateLayout();
          if (!this.ViewTemplate.AllowColumnReorder)
            return;
          this.DropColumnGroup(viewDefinition, (GridViewColumnGroup) dataContext, dropPosition);
          return;
        }
        if (dataContext is GridViewDataColumn || dataContext is GridViewGroupColumn)
        {
          this.DropInColumnGroupsView(viewDefinition, dataContext as GridViewColumn, dropPosition);
          return;
        }
      }
      base.ProcessDragDrop(dropLocation, dragObject);
    }

    protected override void DropInColumnGroupsView(
      ColumnGroupsViewDefinition view,
      GridViewColumn column,
      RadPosition dropPosition)
    {
      GridViewDataColumn gridViewDataColumn = column as GridViewDataColumn;
      if (gridViewDataColumn != null)
      {
        if (this.ViewTemplate.AllowColumnReorder && this.GridViewGroupColumn.Group.Groups.Count == 0)
        {
          GridViewColumnGroupRow row = this.FindRow(view.ColumnGroups, (GridViewColumn) gridViewDataColumn);
          if (this.GridViewGroupColumn.Group != null && this.GridViewGroupColumn.Group.Rows.Count == 0 && (dropPosition & RadPosition.Bottom) != RadPosition.None)
            this.GridViewGroupColumn.Group.Rows.Add(new GridViewColumnGroupRow());
          if (this.GridViewGroupColumn.Group != null && this.GridViewGroupColumn.Group.Rows.Count > 0)
          {
            if (row != null)
            {
              row.ColumnNames.Remove(gridViewDataColumn.Name);
              if (row.ColumnNames.Count == 0)
                this.FindGroup(view.ColumnGroups, row)?.Rows.Remove(row);
            }
            if ((dropPosition & RadPosition.Left) != RadPosition.None)
              this.GridViewGroupColumn.Group.Rows[0].ColumnNames.Insert(0, gridViewDataColumn.Name);
            else
              this.GridViewGroupColumn.Group.Rows[0].ColumnNames.Add(gridViewDataColumn.Name);
          }
        }
        if (!column.IsVisible && this.GridViewGroupColumn.Group.Groups.Count == 0)
        {
          column.IsVisible = true;
        }
        else
        {
          ColumnGroupRowLayout rowLayout = this.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
          rowLayout.InvalidateLayout();
          rowLayout.InvalidateRenderColumns();
          this.TableElement.ViewElement.UpdateRows(true);
        }
      }
      else
      {
        if (!(column is GridViewGroupColumn))
          return;
        this.DropColumnGroup(view, ((GridViewGroupColumn) column).Group, dropPosition);
      }
    }

    private void DropColumnGroup(
      ColumnGroupsViewDefinition view,
      GridViewColumnGroup group,
      RadPosition dropPosition)
    {
      ColumnGroupCollection groupCollection1 = this.FindGroupCollection(view, this.GridViewGroupColumn.Group);
      ColumnGroupCollection groupCollection2 = this.FindGroupCollection(view, group);
      int index = groupCollection1.IndexOf(this.GridViewGroupColumn.Group);
      if (groupCollection1 == groupCollection2 && groupCollection2 != null && groupCollection2.IndexOf(group) < index)
        --index;
      if (group.Parent != null)
        group.Parent.Groups.Remove(group);
      else
        view.ColumnGroups.Remove(group);
      if ((dropPosition & RadPosition.Bottom) != RadPosition.None && this.GridViewGroupColumn.Group.Rows.Count == 0)
        this.GridViewGroupColumn.Group.Groups.Add(group);
      else if ((dropPosition & RadPosition.Left) != RadPosition.None)
        groupCollection1.Insert(index, group);
      else if (index < groupCollection1.Count - 1)
      {
        groupCollection1.Remove(group);
        groupCollection1.Insert(index + 1, group);
      }
      else
        groupCollection1.Add(group);
      if (!group.IsVisible)
        group.IsVisible = true;
      ColumnGroupRowLayout rowLayout = this.TableElement.ViewElement.RowLayout as ColumnGroupRowLayout;
      rowLayout.InvalidateLayout();
      rowLayout.InvalidateRenderColumns();
      this.TableElement.ViewElement.UpdateRows(true);
    }

    private ColumnGroupCollection FindGroupCollection(
      ColumnGroupsViewDefinition view,
      GridViewColumnGroup group)
    {
      if (group.Parent != null)
        return group.Parent.Groups;
      if (group.ParentViewDefinition == view)
        return view.ColumnGroups;
      return (ColumnGroupCollection) null;
    }

    protected override void CreateColumnChooserMenuItems(RadDropDownMenu contextMenu)
    {
      if (!this.ViewTemplate.AllowColumnChooser)
        return;
      RadMenuItem radMenuItem1 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ColumnChooserMenuItem"));
      radMenuItem1.Click += new EventHandler(this.MenuItemColumnChooser_Click);
      contextMenu.Items.Add((RadItem) radMenuItem1);
      if (!this.GridViewGroupColumn.Group.AllowHide || !this.CanHideGroup(this.GridViewGroupColumn.Group))
        return;
      RadMenuItem radMenuItem2 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("HideGroupMenuItem"));
      radMenuItem2.Click += new EventHandler(this.MenuItemHide_Click);
      contextMenu.Items.Add((RadItem) radMenuItem2);
    }

    protected override void PinColumn(PinnedColumnPosition position)
    {
      this.GridViewGroupColumn.RootColumnGroup.PinPosition = position;
    }

    protected override void UpdateFilterButtonVisibility()
    {
      this.FilterButton.Visibility = ElementVisibility.Collapsed;
    }
  }
}
