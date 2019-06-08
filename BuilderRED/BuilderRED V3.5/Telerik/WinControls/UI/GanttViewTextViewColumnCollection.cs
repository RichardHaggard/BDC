// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewColumnCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Specialized;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewColumnCollection : ObservableCollection<GanttViewTextViewColumn>
  {
    private const string ColumnNameBase = "column";
    private RadGanttViewElement owner;
    private HybridDictionary columnNames;

    public GanttViewTextViewColumnCollection(RadGanttViewElement owner)
    {
      this.owner = owner;
      this.columnNames = new HybridDictionary(true);
    }

    public RadGanttViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public GanttViewTextViewColumn this[string columnName]
    {
      get
      {
        return this.columnNames[(object) columnName] as GanttViewTextViewColumn;
      }
    }

    public void Add(string name)
    {
      this.Add(name, name);
    }

    public void Add(string name, string headerText)
    {
      this.Add(new GanttViewTextViewColumn(name, headerText));
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
          if (this[index].Name.Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
            return index;
        }
      }
      return -1;
    }

    public virtual void AddRange(params GanttViewTextViewColumn[] columns)
    {
      this.BeginUpdate();
      for (int index = 0; index < columns.Length; ++index)
        this.Add(columns[index]);
      this.EndUpdate();
    }

    public virtual void AddRange(GanttViewTextViewColumnCollection columns)
    {
      this.BeginUpdate();
      for (int index = 0; index < columns.Count; ++index)
        this.Add(columns[index]);
      this.EndUpdate();
    }

    public void Rename(string name, string newName)
    {
      if (!this.columnNames.Contains((object) name))
        return;
      GanttViewTextViewColumn viewTextViewColumn = this[name];
      this.columnNames.Remove((object) name);
      this.columnNames.Add((object) newName, (object) viewTextViewColumn);
    }

    private void SetUniqueName(GanttViewTextViewColumn column)
    {
      column.Name = string.IsNullOrEmpty(column.FieldName) ? this.GetUniqueName(nameof (column)) : (this[column.FieldName] != null ? this.GetUniqueName(column.FieldName) : column.FieldName);
      if (column.HeaderText != null)
        return;
      column.HeaderText = column.Name;
    }

    public string GetUniqueName(string baseName)
    {
      if (string.IsNullOrEmpty(baseName))
        return string.Empty;
      if (this[baseName] == null && char.IsNumber(baseName[baseName.Length - 1]))
        return baseName;
      int num = 1;
      while (this.Contains(string.Format("{0}{1}", (object) baseName, (object) num)))
        ++num;
      return baseName + (object) num;
    }

    protected override void InsertItem(
      int index,
      GanttViewTextViewColumn column,
      Action<GanttViewTextViewColumn> approvedAction)
    {
      if (this.Contains(column.Name))
        throw new InvalidOperationException("A column with the same Name already exists in the collection.");
      if (string.IsNullOrEmpty(column.Name))
        this.SetUniqueName(column);
      this.columnNames.Add((object) column.Name, (object) column);
      column.Owner = this.owner;
      base.InsertItem(index, column, approvedAction);
    }

    protected override void RemoveItem(int index)
    {
      GanttViewTextViewColumn viewTextViewColumn = this[index];
      base.RemoveItem(index);
      this.columnNames.Remove((object) viewTextViewColumn.Name);
    }

    protected override void ClearItems()
    {
      base.ClearItems();
      this.columnNames.Clear();
    }

    protected override void OnCollectionChanged(Telerik.WinControls.Data.NotifyCollectionChangedEventArgs e)
    {
      base.OnCollectionChanged(e);
      if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
      {
        foreach (GanttViewTextViewColumn newItem in (IEnumerable) e.NewItems)
        {
          newItem.Owner = this.Owner;
          newItem.Initialize();
        }
      }
      this.Owner.TextViewElement.Update(RadGanttViewElement.UpdateActions.Reset);
      this.Owner.TextViewElement.InvalidateMeasure(true);
    }
  }
}
