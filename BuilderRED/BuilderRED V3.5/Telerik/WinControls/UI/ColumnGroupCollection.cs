// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnGroupCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class ColumnGroupCollection : ObservableCollection<GridViewColumnGroup>
  {
    private const string GroupNameBase = "group";
    private GridViewColumnGroup owner;
    private ColumnGroupsViewDefinition ownerDefinition;

    public ColumnGroupCollection(GridViewColumnGroup owner)
    {
      this.owner = owner;
    }

    public ColumnGroupCollection(ColumnGroupsViewDefinition viewDefinition)
    {
      this.ownerDefinition = viewDefinition;
    }

    public virtual GridViewColumnGroup this[string name]
    {
      get
      {
        foreach (GridViewColumnGroup gridViewColumnGroup in (Collection<GridViewColumnGroup>) this)
        {
          if (gridViewColumnGroup.Name == name)
            return gridViewColumnGroup;
        }
        return (GridViewColumnGroup) null;
      }
    }

    protected override void InsertItem(int index, GridViewColumnGroup item)
    {
      if (this.owner != null && !this.owner.ShowHeader)
        throw new InvalidOperationException("You cannot have subgroups of root group that have invisible header.");
      if (string.IsNullOrEmpty(item.Name))
        this.SetUniqueName(item);
      base.InsertItem(index, item);
      item.Parent = this.owner;
      item.ParentViewDefinition = this.ownerDefinition;
      if (this.owner == null)
        return;
      item.RootColumnGroup = this.owner.RootColumnGroup;
    }

    protected override void RemoveItem(int index)
    {
      this[index].Parent = (GridViewColumnGroup) null;
      this[index].ParentViewDefinition = (ColumnGroupsViewDefinition) null;
      this[index].RootColumnGroup = (GridViewColumnGroup) null;
      base.RemoveItem(index);
    }

    public virtual bool Contains(string name)
    {
      foreach (GridViewColumnGroup gridViewColumnGroup in (Collection<GridViewColumnGroup>) this)
      {
        if (gridViewColumnGroup.Name == name)
          return true;
      }
      return false;
    }

    private void SetUniqueName(GridViewColumnGroup item)
    {
      item.Name = GridViewHelper.GetUniqueName(this, "group");
      if (item.Text != null)
        return;
      item.Text = item.Name;
    }
  }
}
