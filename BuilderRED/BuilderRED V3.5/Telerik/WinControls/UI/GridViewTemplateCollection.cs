// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewTemplateCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewTemplateCollection : ObservableCollection<GridViewTemplate>
  {
    private GridViewTemplate owner;

    public GridViewTemplateCollection(GridViewTemplate owner)
    {
      this.owner = owner;
    }

    public void AddRange(params GridViewTemplate[] gridViewTemplates)
    {
      this.BeginUpdate();
      for (int index = 0; index < gridViewTemplates.Length; ++index)
      {
        gridViewTemplates[index].BeginAddRange();
        this.Add(gridViewTemplates[index]);
        gridViewTemplates[index].EndAddRange();
      }
      this.EndUpdate();
    }

    public GridViewTemplate[] GetTemplateByCaption(string caption)
    {
      List<GridViewTemplate> gridViewTemplateList = new List<GridViewTemplate>(10);
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Caption.Equals(caption, StringComparison.CurrentCultureIgnoreCase))
          gridViewTemplateList.Add(this[index]);
      }
      return gridViewTemplateList.ToArray();
    }

    public GridViewTemplate Owner
    {
      get
      {
        return this.owner;
      }
    }

    protected override void InsertItem(int index, GridViewTemplate item)
    {
      base.InsertItem(index, item);
      item.Parent = this.owner;
      this.NotifyTemplate(item, "insert");
    }

    protected override void RemoveItem(int index)
    {
      GridViewTemplate gridViewTemplate = this[index];
      base.RemoveItem(index);
      this.NotifyTemplate(gridViewTemplate, "remove");
    }

    protected override void SetItem(int index, GridViewTemplate item)
    {
      base.SetItem(index, item);
      this.NotifyTemplate(item, "set");
    }

    protected override void ClearItems()
    {
      if (this.Owner.OwnerSite == null && this.Owner.Site == null)
      {
        foreach (GridViewTemplate gridViewTemplate in (Collection<GridViewTemplate>) this)
        {
          gridViewTemplate.Templates.Clear();
          gridViewTemplate.UnwireEvents();
        }
      }
      base.ClearItems();
      this.NotifyTemplate((GridViewTemplate) null, "clear");
    }

    private void NotifyTemplate(GridViewTemplate item, string command)
    {
      if (this.owner.MasterTemplate == null)
        return;
      this.owner.MasterTemplate.SynchronizationService.DispatchEvent(new GridViewEvent((object) this, (object) item, new object[1]
      {
        (object) command
      }, new GridViewEventInfo(KnownEvents.HierarchyChanged, GridEventType.Both, GridEventDispatchMode.Send)));
    }
  }
}
