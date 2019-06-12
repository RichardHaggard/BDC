// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRelationCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewRelationCollection : NotifyCollection<GridViewRelation>
  {
    public void AddSelfReference(
      GridViewTemplate template,
      string parentColumnName,
      string childColumnName)
    {
      this.AddSelfReference(template, new string[1]
      {
        parentColumnName
      }, new string[1]{ childColumnName });
    }

    public void AddSelfReference(
      GridViewTemplate template,
      string[] parentColumnNames,
      string[] childColumnNames)
    {
      if (this.Find(template, template) != null)
        throw new InvalidOperationException("SelfReference relation for this GridViewTemplate already exsist");
      GridViewRelation gridViewRelation = new GridViewRelation(template);
      gridViewRelation.ChildTemplate = template;
      gridViewRelation.ParentColumnNames.AddRange(parentColumnNames);
      gridViewRelation.ChildColumnNames.AddRange(childColumnNames);
      this.Add(gridViewRelation);
    }

    public GridViewRelation Find(GridViewTemplate parent, GridViewTemplate child)
    {
      if (parent == null && child == null)
        return (GridViewRelation) null;
      if (parent == null)
        return this.Find(child, child);
      if (child == null)
        return this.Find(parent, parent);
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ParentTemplate == parent && this[index].ChildTemplate == child)
          return this[index];
      }
      return (GridViewRelation) null;
    }

    public bool Contains(string relationName)
    {
      return this.IndexOf(relationName) != -1;
    }

    public int IndexOf(string relationName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].RelationName == relationName)
          return index;
      }
      return -1;
    }

    protected override void InsertItem(int index, GridViewRelation item)
    {
      if (this.Find(item.ParentTemplate, item.ChildTemplate) != null)
        throw new InvalidOperationException("Relation for these GridViewTemplates already exsist");
      base.InsertItem(index, item);
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
      this.NotifyTemplate(item, "insert");
    }

    protected override void SetItem(int index, GridViewRelation item)
    {
      GridViewRelation gridViewRelation = this[index];
      gridViewRelation.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
      base.SetItem(index, item);
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
      this.NotifyTemplate(gridViewRelation, "set");
    }

    protected override void RemoveItem(int index)
    {
      GridViewRelation gridViewRelation = this[index];
      gridViewRelation.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
      base.RemoveItem(index);
      this.NotifyTemplate(gridViewRelation, "remove");
    }

    protected override void ClearItems()
    {
      List<GridViewRelation> gridViewRelationList = new List<GridViewRelation>((IEnumerable<GridViewRelation>) this);
      base.ClearItems();
      foreach (GridViewRelation gridViewRelation in gridViewRelationList)
      {
        gridViewRelation.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
        this.NotifyTemplate(gridViewRelation, "remove");
      }
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "ParentTemplate") && !(e.PropertyName == "ChildTemplate") && (!(e.PropertyName == "ParentColumnNames") && !(e.PropertyName == "ChildColumnNames")))
        return;
      int index = this.IndexOf(sender as GridViewRelation);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, sender, index));
    }

    private void NotifyTemplate(GridViewRelation item, string command)
    {
      if (item.ChildTemplate == null || item.ChildTemplate.MasterTemplate == null)
        return;
      item.ChildTemplate.MasterTemplate.SynchronizationService.DispatchEvent(new GridViewEvent((object) this, (object) item.ChildTemplate, new object[1]
      {
        (object) command
      }, new GridViewEventInfo(KnownEvents.HierarchyChanged, GridEventType.Both, GridEventDispatchMode.Send)));
    }
  }
}
