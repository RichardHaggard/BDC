// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewColumnCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [Editor("Telerik.WinControls.UI.Design.ListViewColumnCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  [Serializable]
  public class ListViewColumnCollection : ObservableCollection<ListViewDetailColumn>
  {
    private RadListViewElement owner;
    private HybridDictionary columnNames;
    private bool caseSensitive;

    public ListViewColumnCollection(RadListViewElement owner)
    {
      this.owner = owner;
      this.columnNames = new HybridDictionary(!this.caseSensitive);
    }

    public RadListViewElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public ListViewDetailColumn this[string columnName]
    {
      get
      {
        return this.columnNames[(object) columnName] as ListViewDetailColumn;
      }
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
        foreach (ListViewDetailColumn viewDetailColumn in (IEnumerable) columnNames.Values)
          this.columnNames.Add((object) viewDetailColumn.Name, (object) viewDetailColumn);
      }
    }

    public void Add(string name)
    {
      this.Add(name, name);
    }

    public void Add(string name, string headerText)
    {
      this.Add(new ListViewDetailColumn(name, headerText));
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

    public virtual void AddRange(params ListViewDetailColumn[] listViewColumns)
    {
      this.BeginUpdate();
      for (int index = 0; index < listViewColumns.Length; ++index)
        this.Add(listViewColumns[index]);
      this.EndUpdate();
    }

    public void Rename(string name, string newName)
    {
      if (!this.columnNames.Contains((object) name))
        return;
      ListViewDetailColumn viewDetailColumn = this[name];
      this.columnNames.Remove((object) name);
      this.columnNames.Add((object) newName, (object) viewDetailColumn);
      int index = this.owner.FilterDescriptors.IndexOf(name);
      if (index < 0)
        return;
      this.owner.FilterDescriptors[index].PropertyName = newName;
    }

    protected override void InsertItem(
      int index,
      ListViewDetailColumn column,
      Action<ListViewDetailColumn> approvedAction)
    {
      this.columnNames.Add((object) column.Name, (object) column);
      column.Owner = this.owner;
      base.InsertItem(index, column, approvedAction);
    }

    protected override void RemoveItem(int index)
    {
      ListViewDetailColumn viewDetailColumn = this[index];
      base.RemoveItem(index);
      this.columnNames.Remove((object) viewDetailColumn.Name);
    }

    protected override void ClearItems()
    {
      base.ClearItems();
      this.columnNames.Clear();
    }
  }
}
