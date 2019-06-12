// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public abstract class GridGroupVisualElement : GridVisualElement
  {
    private TemplateGroupsElement templateElement;
    private GroupDescriptor description;

    public GridGroupVisualElement(TemplateGroupsElement template, GroupDescriptor description)
    {
      this.templateElement = template;
      this.description = description;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrop = true;
      this.BypassLayoutPolicies = true;
    }

    public GroupDescriptor Description
    {
      get
      {
        return this.description;
      }
    }

    public TemplateGroupsElement TemplateElement
    {
      get
      {
        return this.templateElement;
      }
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      if (dataContext is GroupFieldDragDropContext)
      {
        this.ProcessGroupFieldDrop(dropLocation, dataContext as GroupFieldDragDropContext);
      }
      else
      {
        if (!(dataContext is GridViewColumn))
          return;
        this.ProcessColumnDrop(dropLocation, dataContext as GridViewColumn);
      }
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      bool flag = false;
      if (dataContext is GroupFieldDragDropContext)
        flag = this.templateElement.ViewTemplate == (dataContext as GroupFieldDragDropContext).ViewTemplate;
      else if (dataContext is GridViewColumn)
        flag = (dataContext as GridViewColumn).CanDragToGroup(this.templateElement.ViewTemplate);
      return flag;
    }

    protected virtual bool ProcessGroupFieldDropOverride(
      Point dropLocation,
      GroupFieldDragDropContext context)
    {
      return false;
    }

    protected virtual void ProcessColumnDrop(Point dropLocation, GridViewColumn column)
    {
      GroupDescriptor groupDescriptor = new GroupDescriptor();
      groupDescriptor.GroupNames.Add(new SortDescriptor(column.Name, ListSortDirection.Ascending));
      if (TemplateGroupsElement.RaiseGroupByChanging(this.templateElement.ViewTemplate, groupDescriptor, NotifyCollectionChangedAction.Add))
        return;
      RadGridViewDragDropService.InsertOnLeftOrRight<GroupDescriptor>(RadGridViewDragDropService.IsDroppedAtLeft(dropLocation, this.Size.Width), (Collection<GroupDescriptor>) this.description.Owner, this.description, groupDescriptor);
      TemplateGroupsElement.RaiseGroupByChanged(this.templateElement.ViewTemplate, groupDescriptor, NotifyCollectionChangedAction.Add);
    }

    private void ProcessGroupFieldDrop(Point dropLocation, GroupFieldDragDropContext context)
    {
      SortDescriptor sortDescription = context.SortDescription;
      GridViewTemplate viewTemplate = context.ViewTemplate;
      GroupDescriptor groupDescription = context.GroupDescription;
      if (TemplateGroupsElement.RaiseGroupByChanging(viewTemplate, groupDescription, NotifyCollectionChangedAction.Batch) || this.ProcessGroupFieldDropOverride(dropLocation, context))
        return;
      groupDescription.GroupNames.Remove(sortDescription);
      GroupDescriptor groupDescriptor = new GroupDescriptor();
      groupDescriptor.GroupNames.Add(sortDescription);
      RadGridViewDragDropService.InsertOnLeftOrRight<GroupDescriptor>(RadGridViewDragDropService.IsDroppedAtLeft(dropLocation, this.Size.Width), (Collection<GroupDescriptor>) this.description.Owner, this.description, groupDescriptor);
      if (groupDescription.GroupNames.Count == 0)
        viewTemplate.DataView.GroupDescriptors.Remove(groupDescription);
      TemplateGroupsElement.RaiseGroupByChanged(viewTemplate, groupDescriptor, NotifyCollectionChangedAction.Batch);
    }
  }
}
