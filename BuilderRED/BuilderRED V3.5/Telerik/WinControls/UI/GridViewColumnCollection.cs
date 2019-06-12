// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewColumnCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewColumnCollection : ObservableCollection<GridViewDataColumn>
  {
    private const string ColumnNameBase = "column";
    private GridViewTemplate owner;
    private HybridDictionary columnNames;
    private bool caseSensitive;

    public GridViewColumnCollection(GridViewTemplate owner)
    {
      this.owner = owner;
      this.columnNames = new HybridDictionary(!this.caseSensitive);
    }

    public void Add(string name)
    {
      this.Add(name, name, string.Empty);
    }

    public void Add(string name, string headerText)
    {
      this.Add(name, headerText, string.Empty);
    }

    public void Add(string name, string headerText, string fieldName)
    {
      GridViewTextBoxColumn viewTextBoxColumn = new GridViewTextBoxColumn();
      viewTextBoxColumn.Name = name;
      viewTextBoxColumn.HeaderText = headerText;
      viewTextBoxColumn.FieldName = fieldName;
      this.Add((GridViewDataColumn) viewTextBoxColumn);
    }

    public void Remove(string columnName)
    {
      int index = this.IndexOf(columnName);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    public bool Contains(string columnName)
    {
      if (string.IsNullOrEmpty(columnName))
        return false;
      return this.columnNames.Contains((object) columnName);
    }

    public int IndexOf(string columnName)
    {
      if (this.Contains(columnName))
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Name.Equals(columnName, this.AllowCaseSensitiveNames ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
            return index;
        }
      }
      return -1;
    }

    public GridViewDataColumn[] GetColumnByFieldName(string fieldName)
    {
      List<GridViewDataColumn> gridViewDataColumnList = new List<GridViewDataColumn>(10);
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].FieldName != null && this[index].FieldName.Equals(fieldName, this.AllowCaseSensitiveNames ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase))
          gridViewDataColumnList.Add(this[index]);
      }
      return gridViewDataColumnList.ToArray();
    }

    public GridViewDataColumn[] GetColumnByHeaderText(string headerText)
    {
      List<GridViewDataColumn> gridViewDataColumnList = new List<GridViewDataColumn>(10);
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].HeaderText != null && this[index].HeaderText.Equals(headerText, this.AllowCaseSensitiveNames ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase))
          gridViewDataColumnList.Add(this[index]);
      }
      return gridViewDataColumnList.ToArray();
    }

    public virtual void AddRange(params GridViewDataColumn[] gridViewColumns)
    {
      this.BeginUpdate();
      for (int index = 0; index < gridViewColumns.Length; ++index)
        this.Add(gridViewColumns[index]);
      this.EndUpdate();
    }

    public void Rename(string name, string newName)
    {
      if (!this.columnNames.Contains((object) name))
        return;
      GridViewColumn gridViewColumn = (GridViewColumn) this[name];
      this.columnNames.Remove((object) name);
      this.columnNames.Add((object) newName, (object) gridViewColumn);
      int index = this.owner.FilterDescriptors.IndexOf(name);
      if (index < 0)
        return;
      this.owner.FilterDescriptors[index].PropertyName = newName;
    }

    internal List<GridViewColumn> ToList()
    {
      List<GridViewColumn> gridViewColumnList = new List<GridViewColumn>();
      foreach (GridViewDataColumn gridViewDataColumn in (Collection<GridViewDataColumn>) this)
        gridViewColumnList.Add((GridViewColumn) gridViewDataColumn);
      return gridViewColumnList;
    }

    public bool AllowCaseSensitiveNames
    {
      get
      {
        return this.caseSensitive;
      }
      set
      {
        if (this.caseSensitive == value)
          return;
        this.caseSensitive = value;
        HybridDictionary columnNames = this.columnNames;
        this.columnNames = new HybridDictionary(!this.caseSensitive);
        foreach (GridViewDataColumn gridViewDataColumn in (IEnumerable) columnNames.Values)
          this.columnNames.Add((object) gridViewDataColumn.Name, (object) gridViewDataColumn);
      }
    }

    public GridViewTemplate Owner
    {
      get
      {
        return this.owner;
      }
    }

    public GridViewDataColumn this[string columnName]
    {
      get
      {
        return this.columnNames[(object) columnName] as GridViewDataColumn;
      }
    }

    private void SetUniqueName(GridViewDataColumn item)
    {
      if (!string.IsNullOrEmpty(item.FieldName))
      {
        if (this[item.FieldName] == null)
          item.Name = item.FieldName;
        else
          item.Name = GridViewHelper.GetUniqueName(this, item.FieldName);
      }
      else
        item.Name = GridViewHelper.GetUniqueName(this, "column");
      if (item.HeaderText != null)
        return;
      item.HeaderText = item.Name;
    }

    protected override void InsertItem(int index, GridViewDataColumn item)
    {
      if (this.Contains(item.Name))
        throw new InvalidOperationException("A column with the same Name already exists in the collection");
      if (string.IsNullOrEmpty(item.Name))
        this.SetUniqueName(item);
      this.columnNames.Add((object) item.Name, (object) item);
      item.OwnerTemplate = this.owner;
      base.InsertItem(index, item);
      if (this.Owner == null || this.Owner.MasterTemplate == null)
        return;
      List<GridViewRelation> gridViewRelationList = new List<GridViewRelation>();
      foreach (GridViewRelation relation in (Collection<GridViewRelation>) this.Owner.MasterTemplate.Relations)
      {
        if (relation.ChildTemplate != relation.ParentTemplate)
        {
          foreach (string parentColumnName in relation.ParentColumnNames)
          {
            if (parentColumnName == item.Name)
              gridViewRelationList.Add(relation);
          }
          foreach (string childColumnName in relation.ChildColumnNames)
          {
            if (childColumnName == item.Name)
              gridViewRelationList.Add(relation);
          }
        }
      }
      foreach (GridViewRelation gridViewRelation in gridViewRelationList)
      {
        if (gridViewRelation.ChildTemplate != null && gridViewRelation.ChildTemplate.MasterTemplate != null)
        {
          gridViewRelation.ChildTemplate.MasterTemplate.Relations.Remove(gridViewRelation);
          gridViewRelation.ChildTemplate.MasterTemplate.Relations.Add(gridViewRelation);
        }
      }
    }

    protected override void RemoveItem(int index)
    {
      GridViewDataColumn gridViewDataColumn = this[index];
      this.columnNames.Remove((object) gridViewDataColumn.Name);
      base.RemoveItem(index);
      if (gridViewDataColumn.IsGrouped)
        gridViewDataColumn.OwnerTemplate.GroupDescriptors.Clear();
      gridViewDataColumn.OwnerTemplate = (GridViewTemplate) null;
    }

    protected override void SetItem(int index, GridViewDataColumn item)
    {
      if (this.Contains(item.Name))
        throw new InvalidOperationException("A column with the same Name already exists in the collection");
      this.columnNames.Remove((object) this[index].Name);
      this.columnNames.Add((object) item.Name, (object) item);
      base.SetItem(index, item);
      item.OwnerTemplate = this.owner;
    }

    protected override void ClearItems()
    {
      this.Owner.GroupDescriptors.Clear();
      this.columnNames.Clear();
      base.ClearItems();
    }
  }
}
