// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class GroupElement : GridGroupVisualElement
  {
    private StackLayoutPanel panel;
    private GroupFieldElement[] groupFields;

    public GroupElement(TemplateGroupsElement template, GroupDescriptor groupDescription)
      : base(template, groupDescription)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BorderWidth = 0.0f;
      this.CaptureOnMouseDown = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.panel = new StackLayoutPanel();
      this.panel.Orientation = Orientation.Horizontal;
      this.Children.Add((RadElement) this.panel);
      this.UpdateGroupingFields();
    }

    public void UpdateGroupingFields()
    {
      if (this.Description == null || string.IsNullOrEmpty(this.Description.Expression))
      {
        this.groupFields = new GroupFieldElement[0];
      }
      else
      {
        this.panel.DisposeChildren();
        SortDescriptorCollection groupNames = this.Description.GroupNames;
        int count = groupNames.Count;
        List<GroupFieldElement> groupFieldElementList = new List<GroupFieldElement>();
        GridViewColumnCollection columns = this.TemplateElement.ViewTemplate.Columns;
        for (int index = 0; index < count; ++index)
        {
          GroupFieldElement groupFieldElement = new GroupFieldElement(this.TemplateElement, this.Description, groupNames[index]);
          if (groupFieldElement.Column != null)
          {
            this.panel.Children.Add((RadElement) groupFieldElement);
            groupFieldElementList.Add(groupFieldElement);
            if (index < count - 1)
              this.panel.Children.Add((RadElement) new GroupLinkElement(this.TemplateElement)
              {
                Type = GroupLinkElement.LinkType.NamesLink
              });
          }
        }
        this.groupFields = groupFieldElementList.ToArray();
      }
    }

    public ReadOnlyCollection<GroupFieldElement> GroupingFieldElements
    {
      get
      {
        return Array.AsReadOnly<GroupFieldElement>(this.groupFields);
      }
    }

    protected override bool ProcessGroupFieldDropOverride(
      Point dropLocation,
      GroupFieldDragDropContext context)
    {
      SortDescriptor sortDescription = context.SortDescription;
      GridViewTemplate viewTemplate = context.ViewTemplate;
      GroupDescriptor groupDescription = context.GroupDescription;
      groupDescription.GroupNames.Remove(sortDescription);
      if (RadGridViewDragDropService.IsDroppedAtLeft(dropLocation, this.Size.Width))
        this.Description.GroupNames.Insert(0, sortDescription);
      else
        this.Description.GroupNames.Add(sortDescription);
      if (groupDescription.GroupNames.Count == 0)
        viewTemplate.DataView.GroupDescriptors.Remove(groupDescription);
      TemplateGroupsElement.RaiseGroupByChanged(context.ViewTemplate, context.GroupDescription, NotifyCollectionChangedAction.Batch);
      return true;
    }
  }
}
