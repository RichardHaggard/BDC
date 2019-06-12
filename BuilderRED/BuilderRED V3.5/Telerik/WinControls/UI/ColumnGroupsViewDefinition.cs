// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnGroupsViewDefinition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class ColumnGroupsViewDefinition : TableViewDefinition, IDisposable
  {
    private ColumnGroupCollection columnGroups;
    private GridViewTemplate template;

    public ColumnGroupsViewDefinition()
    {
      this.columnGroups = new ColumnGroupCollection(this);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GridViewTemplate ViewTemplate
    {
      get
      {
        return this.template;
      }
      internal set
      {
        this.template = value;
        this.ProcessPinnedGroupsCore();
      }
    }

    protected virtual void ProcessPinnedGroupsCore()
    {
      foreach (GridViewColumnGroup columnGroup in (Collection<GridViewColumnGroup>) this.ColumnGroups)
      {
        PinnedColumnPosition pinPosition = columnGroup.PinPosition;
        if (columnGroup.IsPinned)
          columnGroup.SetPinPosition(columnGroup, pinPosition, this.ViewTemplate);
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection containing column groups.")]
    public ColumnGroupCollection ColumnGroups
    {
      get
      {
        return this.columnGroups;
      }
    }

    public override IRowView CreateViewUIElement(GridViewInfo viewInfo)
    {
      GridTableElement gridTableElement = new GridTableElement();
      gridTableElement.ViewElement.RowLayout = this.CreateRowLayout();
      return (IRowView) gridTableElement;
    }

    public override IGridRowLayout CreateRowLayout()
    {
      return (IGridRowLayout) new ColumnGroupRowLayout(this);
    }

    public GridViewColumnGroup FindGroup(GridViewColumn column)
    {
      GridViewGroupColumn gridViewGroupColumn = column as GridViewGroupColumn;
      if (gridViewGroupColumn != null)
        return gridViewGroupColumn.Group;
      return this.FindGroup(this.ColumnGroups, column);
    }

    public IEnumerable<GridViewColumnGroup> GetAllGroups()
    {
      foreach (GridViewColumnGroup allGroup in this.GetAllGroups(this.ColumnGroups))
        yield return allGroup;
    }

    private IEnumerable<GridViewColumnGroup> GetAllGroups(
      ColumnGroupCollection collection)
    {
      foreach (GridViewColumnGroup gridViewColumnGroup in (Collection<GridViewColumnGroup>) collection)
      {
        yield return gridViewColumnGroup;
        foreach (GridViewColumnGroup allGroup in this.GetAllGroups(gridViewColumnGroup.Groups))
          yield return allGroup;
      }
    }

    public void Dispose()
    {
      foreach (GridViewColumnGroup columnGroup in (Collection<GridViewColumnGroup>) this.ColumnGroups)
      {
        columnGroup.SuspendNotifications();
        columnGroup.PinPosition = PinnedColumnPosition.None;
        columnGroup.ResumeNotifications();
      }
    }

    private GridViewColumnGroup FindGroup(
      ColumnGroupCollection groups,
      GridViewColumn column)
    {
      foreach (GridViewColumnGroup group1 in (Collection<GridViewColumnGroup>) groups)
      {
        if (group1.Groups.Count > 0)
        {
          GridViewColumnGroup group2 = this.FindGroup(group1.Groups, column);
          if (group2 != null)
            return group2;
        }
        else
        {
          foreach (GridViewColumnGroupRow row in (Collection<GridViewColumnGroupRow>) group1.Rows)
          {
            foreach (string columnName in (Collection<string>) row.ColumnNames)
            {
              if (columnName == column.Name)
                return group1;
            }
          }
        }
      }
      return (GridViewColumnGroup) null;
    }
  }
}
