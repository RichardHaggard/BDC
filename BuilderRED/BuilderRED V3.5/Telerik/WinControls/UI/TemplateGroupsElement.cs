// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TemplateGroupsElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  public class TemplateGroupsElement : GridVisualElement
  {
    public static RadProperty LinkOffsetProperty = RadProperty.Register(nameof (LinkOffset), typeof (int), typeof (TemplateGroupsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsMeasure));
    public static RadProperty LinkPositionProperty = RadProperty.Register(nameof (LinkPosition), typeof (int), typeof (TemplateGroupsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TemplateGroupsElement.GroupLinkPosition.Top, ElementPropertyOptions.AffectsMeasure));
    public static RadProperty LinkWidthProperty = RadProperty.Register(nameof (LinkWidth), typeof (int), typeof (TemplateGroupsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ItemsDistanceProperty = RadProperty.Register(nameof (ItemsDistance), typeof (Size), typeof (TemplateGroupsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Size(5, 2), ElementPropertyOptions.AffectsMeasure));
    public static RadProperty TemplateGroupsBottomDistanceProperty = RadProperty.Register("ChildTemplateGroupsDistanceProperty", typeof (int), typeof (TemplateGroupsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ChildTemplateGroupsHorizontalOffsetProperty = RadProperty.Register("ChildTemplateGroupsOffsetProperty", typeof (int), typeof (TemplateGroupsElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 20, ElementPropertyOptions.AffectsMeasure));
    private GroupPanelElement groupPanelElement;
    private GridViewTemplate template;
    private GroupLinkElement[] groupLinks;
    private GroupElement[] groupElements;
    private TemplateGroupsElement[] childTemplateGroups;

    public TemplateGroupsElement(GroupPanelElement groupPanelElement, GridViewTemplate template)
    {
      this.template = template;
      this.groupPanelElement = groupPanelElement;
    }

    [RadPropertyDefaultValue("LinkOffset", typeof (RadElement))]
    [Category("Layout")]
    [Description("The group links offset")]
    public virtual int LinkOffset
    {
      get
      {
        return (int) this.GetValue(TemplateGroupsElement.LinkOffsetProperty);
      }
      set
      {
        int num = (int) this.SetValue(TemplateGroupsElement.LinkOffsetProperty, (object) value);
      }
    }

    [Category("Layout")]
    [RadPropertyDefaultValue("LinkPosition", typeof (RadElement))]
    [Description("The group links position")]
    public virtual TemplateGroupsElement.GroupLinkPosition LinkPosition
    {
      get
      {
        return (TemplateGroupsElement.GroupLinkPosition) this.GetValue(TemplateGroupsElement.LinkPositionProperty);
      }
      set
      {
        int num = (int) this.SetValue(TemplateGroupsElement.LinkPositionProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("LinkWidth", typeof (RadElement))]
    [Description("The separator's width between two consecutive GridFieldElements")]
    [Category("Layout")]
    public virtual int LinkWidth
    {
      get
      {
        return (int) this.GetValue(TemplateGroupsElement.LinkWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(TemplateGroupsElement.LinkWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ItemsDistance", typeof (RadElement))]
    [Category("Layout")]
    [Description("Sets the vertical and horizontal distance between two consecutive GroupElement")]
    public virtual Size ItemsDistance
    {
      get
      {
        return (Size) this.GetValue(TemplateGroupsElement.ItemsDistanceProperty);
      }
      set
      {
        int num = (int) this.SetValue(TemplateGroupsElement.ItemsDistanceProperty, (object) value);
      }
    }

    [Category("Layout")]
    [Description("Sets the vertical distance between two consecutive TemplateGroupsElement")]
    [RadPropertyDefaultValue("TemplateGroupsBottomDistance", typeof (RadElement))]
    public virtual int TemplateGroupsBottomDistance
    {
      get
      {
        return (int) this.GetValue(TemplateGroupsElement.TemplateGroupsBottomDistanceProperty);
      }
      set
      {
        int num = (int) this.SetValue(TemplateGroupsElement.TemplateGroupsBottomDistanceProperty, (object) value);
      }
    }

    public int ChildTemplateGroupsHorizontalOffset
    {
      get
      {
        return (int) this.GetValue(TemplateGroupsElement.ChildTemplateGroupsHorizontalOffsetProperty);
      }
      set
      {
        int num = (int) this.SetValue(TemplateGroupsElement.ChildTemplateGroupsHorizontalOffsetProperty, (object) value);
      }
    }

    public ReadOnlyCollection<GroupElement> GroupElements
    {
      get
      {
        return Array.AsReadOnly<GroupElement>(this.groupElements);
      }
    }

    public ReadOnlyCollection<TemplateGroupsElement> ChildTemplateGroupsElements
    {
      get
      {
        return Array.AsReadOnly<TemplateGroupsElement>(this.childTemplateGroups);
      }
    }

    public GroupPanelElement GroupPanelElement
    {
      get
      {
        return this.groupPanelElement;
      }
    }

    public GridViewTemplate ViewTemplate
    {
      get
      {
        return this.template;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrop = true;
      this.Class = nameof (TemplateGroupsElement);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.UpdateHierarchy();
    }

    public void UpdateHierarchy()
    {
      if (this.template == null)
        return;
      this.DisposeChildren();
      this.UpdateGroups();
      List<TemplateGroupsElement> templateGroupsElementList = new List<TemplateGroupsElement>();
      for (int index = 0; index < this.template.Templates.Count; ++index)
      {
        TemplateGroupsElement templateGroupsElement = new TemplateGroupsElement(this.groupPanelElement, this.template.Templates[index]);
        templateGroupsElement.UpdateHierarchy();
        if (templateGroupsElement.GroupElements.Count != 0 || templateGroupsElement.ChildTemplateGroupsElements.Count != 0)
        {
          templateGroupsElementList.Add(templateGroupsElement);
          this.Children.Add((RadElement) templateGroupsElement);
        }
      }
      this.childTemplateGroups = templateGroupsElementList.ToArray();
    }

    private void UpdateGroups()
    {
      List<GroupElement> groupElementList = new List<GroupElement>();
      List<GroupLinkElement> groupLinkElementList = new List<GroupLinkElement>();
      for (int index = 0; index < this.template.DataView.GroupDescriptors.Count && this.template.ColumnCount > 0; ++index)
      {
        GroupElement groupElement = new GroupElement(this, this.template.DataView.GroupDescriptors[index]);
        groupElement.UpdateGroupingFields();
        if (groupElement.GroupingFieldElements.Count != 0)
        {
          groupElementList.Add(groupElement);
          this.Children.Add((RadElement) groupElement);
          if (index > 0)
          {
            GroupLinkElement groupLinkElement = new GroupLinkElement(this);
            groupLinkElementList.Add(groupLinkElement);
            this.Children.Add((RadElement) groupLinkElement);
          }
        }
      }
      this.groupElements = groupElementList.ToArray();
      this.groupLinks = groupLinkElementList.ToArray();
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.RightToLeft)
        return this.ArrangeRightToLeft(finalSize);
      return this.ArrangeLeftToRight(finalSize);
    }

    protected virtual void ArrangeGroupLink(
      GroupLinkElement linkElement,
      PointF beginPoint,
      PointF endPoint)
    {
      float width = endPoint.X - beginPoint.X;
      float height = endPoint.Y - beginPoint.Y;
      Point point = this.PointFromControl(Point.Ceiling(beginPoint));
      SizeF size = new SizeF(width, height);
      RectangleF finalRect = new RectangleF(new PointF((float) point.X, (float) point.Y), size);
      linkElement.Arrange(finalRect);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = (SizeF) Size.Empty;
      if (this.groupElements != null)
        this.MeasureGroupElements(availableSize, ref empty);
      this.MeasureTemplateGroupElements(availableSize, ref empty);
      Padding borderThickness = this.GetBorderThickness(false);
      empty.Width += (float) (this.Padding.Horizontal + borderThickness.Horizontal);
      empty.Height += (float) (this.Padding.Vertical + borderThickness.Vertical);
      return empty;
    }

    protected virtual void MeasureTemplateGroupElements(SizeF availableSize, ref SizeF sizeNeeded)
    {
      int length = this.childTemplateGroups.Length;
      for (int index = 0; index < length; ++index)
      {
        TemplateGroupsElement childTemplateGroup = this.childTemplateGroups[index];
        childTemplateGroup.Measure(availableSize);
        SizeF desiredSize = childTemplateGroup.DesiredSize;
        int num = this.GroupElements.Count > 0 ? this.ChildTemplateGroupsHorizontalOffset : 0;
        sizeNeeded.Width = (float) (int) Math.Max(sizeNeeded.Width, desiredSize.Width + (float) num);
        sizeNeeded.Height += (float) (int) desiredSize.Height;
      }
      sizeNeeded.Height += (float) (length * this.TemplateGroupsBottomDistance);
    }

    protected virtual void MeasureGroupElements(SizeF availableSize, ref SizeF sizeNeeded)
    {
      int length = this.groupElements.Length;
      for (int index = 0; index < length; ++index)
      {
        GroupElement groupElement = this.groupElements[index];
        groupElement.Measure(availableSize);
        SizeF desiredSize = groupElement.DesiredSize;
        sizeNeeded.Width += desiredSize.Width;
        sizeNeeded.Height += desiredSize.Height;
      }
      sizeNeeded.Height += (float) ((length - 1) * this.ItemsDistance.Height);
      sizeNeeded.Width += (float) ((length - 1) * this.ItemsDistance.Width);
    }

    protected virtual SizeF ArrangeLeftToRight(SizeF finalSize)
    {
      PointF location = this.GetClientRectangle(finalSize).Location;
      float currentBottom = 0.0f;
      if (this.groupElements != null)
        this.ArrangeLeftToRightGroupElements(ref location, ref currentBottom);
      float y = currentBottom + (this.GroupElements.Count > 0 ? (float) this.TemplateGroupsBottomDistance : 0.0f);
      int num = this.GroupElements.Count > 0 ? this.ChildTemplateGroupsHorizontalOffset : 0;
      for (int index = 0; index < this.childTemplateGroups.Length; ++index)
      {
        TemplateGroupsElement childTemplateGroup = this.childTemplateGroups[index];
        RectangleF finalRect = new RectangleF((float) num, y, finalSize.Width - (float) num, childTemplateGroup.DesiredSize.Height);
        childTemplateGroup.Arrange(finalRect);
        y += childTemplateGroup.DesiredSize.Height;
        if (index < this.childTemplateGroups.Length - 1)
          y += (float) this.TemplateGroupsBottomDistance;
      }
      return finalSize;
    }

    protected virtual void ArrangeLeftToRightGroupElements(
      ref PointF current,
      ref float currentBottom)
    {
      GroupElement fromGroupElement = (GroupElement) null;
      for (int index = 0; index < this.groupElements.Length; ++index)
      {
        GroupElement groupElement = this.groupElements[index];
        SizeF desiredSize = groupElement.DesiredSize;
        RectangleF finalRect = new RectangleF(current, desiredSize);
        groupElement.Arrange(finalRect);
        currentBottom = Math.Max(currentBottom, current.Y + desiredSize.Height);
        current.X += desiredSize.Width + (float) this.ItemsDistance.Width;
        current.Y += desiredSize.Height + (float) this.ItemsDistance.Height;
        if (index > 0)
        {
          GroupLinkElement groupLink = this.groupLinks[index - 1];
          PointF beingPoint = PointF.Empty;
          PointF endPoint = PointF.Empty;
          this.CalcFinalRectLeftToRightBottomGroupLink(fromGroupElement, groupElement, out beingPoint, out endPoint);
          this.ArrangeGroupLink(groupLink, beingPoint, endPoint);
        }
        fromGroupElement = groupElement;
      }
    }

    protected virtual void CalcFinalRectLeftToRightBottomGroupLink(
      GroupElement fromGroupElement,
      GroupElement toGroupElement,
      out PointF beingPoint,
      out PointF endPoint)
    {
      GroupFieldElement groupingFieldElement1 = toGroupElement.GroupingFieldElements[0];
      GroupFieldElement groupingFieldElement2 = fromGroupElement.GroupingFieldElements[fromGroupElement.GroupingFieldElements.Count - 1];
      Rectangle boundingRectangle1 = groupingFieldElement1.ControlBoundingRectangle;
      Rectangle boundingRectangle2 = groupingFieldElement2.ControlBoundingRectangle;
      float x1;
      float y1;
      float x2;
      float y2;
      if (this.LinkPosition == TemplateGroupsElement.GroupLinkPosition.Bottom)
      {
        x1 = (float) (boundingRectangle2.X + this.LinkOffset);
        y1 = (float) (boundingRectangle2.Bottom + groupingFieldElement2.Padding.Bottom);
        x2 = (float) (boundingRectangle1.X - groupingFieldElement1.Padding.Left);
        y2 = (float) (boundingRectangle1.Bottom + Math.Abs(boundingRectangle2.Bottom - boundingRectangle1.Y));
      }
      else
      {
        x1 = (float) (boundingRectangle2.Right + groupingFieldElement2.Padding.Right);
        y1 = (float) (boundingRectangle2.Y - Math.Abs(boundingRectangle2.Bottom - boundingRectangle1.Y));
        x2 = (float) (boundingRectangle1.Right - this.LinkOffset);
        y2 = (float) (boundingRectangle1.Y - groupingFieldElement1.Padding.Top);
      }
      beingPoint = new PointF(x1, y1);
      endPoint = new PointF(x2, y2);
    }

    protected virtual SizeF ArrangeRightToLeft(SizeF finalSize)
    {
      PointF current = new PointF(this.GetClientRectangle(finalSize).Right, 0.0f);
      float currentBottom = 0.0f;
      if (this.groupElements != null)
        this.ArrangeRightToLeftGroupElements(ref current, ref currentBottom);
      float y = currentBottom + (this.GroupElements.Count > 0 ? (float) this.TemplateGroupsBottomDistance : 0.0f);
      int num = this.GroupElements.Count > 0 ? this.ChildTemplateGroupsHorizontalOffset : 0;
      for (int index = 0; index < this.childTemplateGroups.Length; ++index)
      {
        TemplateGroupsElement childTemplateGroup = this.childTemplateGroups[index];
        RectangleF finalRect = new RectangleF(0.0f, y, finalSize.Width - (float) num, childTemplateGroup.DesiredSize.Height);
        childTemplateGroup.Arrange(finalRect);
        y += childTemplateGroup.DesiredSize.Height;
        if (index < this.childTemplateGroups.Length - 1)
          y += (float) this.TemplateGroupsBottomDistance;
      }
      return finalSize;
    }

    protected virtual void ArrangeRightToLeftGroupElements(
      ref PointF current,
      ref float currentBottom)
    {
      GroupElement fromGroupElement = (GroupElement) null;
      PointF beingPoint = PointF.Empty;
      PointF endPoint = PointF.Empty;
      for (int index = 0; index < this.groupElements.Length; ++index)
      {
        GroupElement groupElement = this.groupElements[index];
        SizeF desiredSize = groupElement.DesiredSize;
        RectangleF finalRect = new RectangleF(new PointF(current.X - desiredSize.Width, current.Y), desiredSize);
        groupElement.Arrange(finalRect);
        currentBottom = Math.Max(currentBottom, current.Y + desiredSize.Height);
        current.X -= desiredSize.Width + (float) this.ItemsDistance.Width;
        current.Y += desiredSize.Height + (float) this.ItemsDistance.Height;
        if (index > 0)
        {
          GroupLinkElement groupLink = this.groupLinks[index - 1];
          this.CalcFinalRectRightToLeftBottomGroupLink(fromGroupElement, groupElement, out beingPoint, out endPoint);
          this.ArrangeGroupLink(groupLink, beingPoint, endPoint);
        }
        fromGroupElement = groupElement;
      }
    }

    protected virtual void CalcFinalRectRightToLeftBottomGroupLink(
      GroupElement fromGroupElement,
      GroupElement toGroupElement,
      out PointF beingPoint,
      out PointF endPoint)
    {
      GroupFieldElement groupingFieldElement1 = toGroupElement.GroupingFieldElements[0];
      GroupFieldElement groupingFieldElement2 = fromGroupElement.GroupingFieldElements[fromGroupElement.GroupingFieldElements.Count - 1];
      Rectangle boundingRectangle1 = groupingFieldElement1.ControlBoundingRectangle;
      Rectangle boundingRectangle2 = groupingFieldElement2.ControlBoundingRectangle;
      float x1;
      float y1;
      float x2;
      float y2;
      if (this.LinkPosition == TemplateGroupsElement.GroupLinkPosition.Bottom)
      {
        x1 = (float) (boundingRectangle1.Right + groupingFieldElement1.Padding.Right);
        y1 = (float) (boundingRectangle2.Bottom + groupingFieldElement2.Padding.Bottom);
        x2 = (float) (boundingRectangle2.Right - this.LinkOffset);
        y2 = (float) (boundingRectangle1.Bottom + Math.Abs(boundingRectangle2.Bottom - boundingRectangle1.Y));
      }
      else
      {
        x1 = (float) (boundingRectangle1.X + this.LinkOffset);
        y1 = (float) (boundingRectangle2.Y - Math.Abs(boundingRectangle2.Bottom - boundingRectangle1.Y));
        x2 = (float) (boundingRectangle2.X - groupingFieldElement2.Padding.Left);
        y2 = (float) (boundingRectangle1.Y - groupingFieldElement1.Padding.Top);
      }
      beingPoint = new PointF(x1, y1);
      endPoint = new PointF(x2, y2);
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      if (dataContext is GridViewDataColumn)
      {
        this.ProcessDragDropColumn(dataContext);
      }
      else
      {
        if (!(dataContext is GroupFieldDragDropContext))
          return;
        this.ProcessDragDropFieldContext(dataContext);
      }
    }

    protected virtual void ProcessDragDropFieldContext(object dataContext)
    {
      GroupFieldDragDropContext fieldDragDropContext = dataContext as GroupFieldDragDropContext;
      GroupDescriptor groupDescription = fieldDragDropContext.GroupDescription;
      SortDescriptor sortDescription = fieldDragDropContext.SortDescription;
      GridViewTemplate viewTemplate = fieldDragDropContext.ViewTemplate;
      if (TemplateGroupsElement.RaiseGroupByChanging(this.template, groupDescription, NotifyCollectionChangedAction.Batch))
        return;
      if (groupDescription.GroupNames.Count == 1)
      {
        this.ViewTemplate.DataView.GroupDescriptors.Move(this.ViewTemplate.DataView.GroupDescriptors.IndexOf(groupDescription), this.ViewTemplate.DataView.GroupDescriptors.Count - 1);
      }
      else
      {
        groupDescription.GroupNames.Remove(sortDescription);
        GroupDescriptor groupDescriptor = new GroupDescriptor();
        groupDescriptor.GroupNames.Add(sortDescription);
        this.ViewTemplate.DataView.GroupDescriptors.Add(groupDescriptor);
      }
      TemplateGroupsElement.RaiseGroupByChanged(this.template, groupDescription, NotifyCollectionChangedAction.Batch);
    }

    protected virtual void ProcessDragDropColumn(object dataContext)
    {
      GridViewColumn gridViewColumn = dataContext as GridViewColumn;
      GroupDescriptor groupDescription = new GroupDescriptor();
      groupDescription.GroupNames.Add(new SortDescriptor(gridViewColumn.Name, ListSortDirection.Ascending));
      if (TemplateGroupsElement.RaiseGroupByChanging(this.template, groupDescription, NotifyCollectionChangedAction.Add))
        return;
      this.template.DataView.GroupDescriptors.Add(groupDescription);
      TemplateGroupsElement.RaiseGroupByChanged(this.template, groupDescription, NotifyCollectionChangedAction.Add);
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      return this.CanDragOver(dragObject);
    }

    public bool CanDragOver(ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      if (dataContext is GridViewDataColumn)
        return (dataContext as GridViewColumn).CanDragToGroup(this.ViewTemplate);
      return dataContext is GroupFieldDragDropContext && this.ViewTemplate == (dataContext as GroupFieldDragDropContext).ViewTemplate;
    }

    internal static bool RaiseGroupByChanging(
      GridViewTemplate template,
      GroupDescriptor groupDescription,
      NotifyCollectionChangedAction action)
    {
      GridGroupByExpression fromDescriptor = GridGroupByExpression.CreateFromDescriptor(groupDescription);
      GridViewCollectionChangingEventArgs args;
      if (action == NotifyCollectionChangedAction.Add || action == NotifyCollectionChangedAction.ItemChanged || action == NotifyCollectionChangedAction.Remove)
      {
        args = new GridViewCollectionChangingEventArgs(template, action, (object) fromDescriptor, 0, 0);
      }
      else
      {
        if (action != NotifyCollectionChangedAction.Batch)
          throw new ArgumentException("Invalid action");
        IList newItems = (IList) new List<GridGroupByExpression>();
        newItems.Add((object) fromDescriptor);
        args = new GridViewCollectionChangingEventArgs(template, action, newItems, (IList) template.GroupDescriptors, 0, 0, (PropertyChangingEventArgsEx) null);
      }
      template.EventDispatcher.RaiseEvent<GridViewCollectionChangingEventArgs>(EventDispatcher.GroupByChanging, (object) template, args);
      if (!args.Cancel)
      {
        template.EventDispatcher.SuspendEvent(EventDispatcher.GroupByChanging);
        template.EventDispatcher.SuspendEvent(EventDispatcher.GroupByChanged);
        template.MasterTemplate.SynchronizationService.BeginDispatch();
      }
      return args.Cancel;
    }

    internal static void RaiseGroupByChanged(
      GridViewTemplate template,
      GroupDescriptor groupDescription,
      NotifyCollectionChangedAction action)
    {
      GridGroupByExpression fromDescriptor = GridGroupByExpression.CreateFromDescriptor(groupDescription);
      template.EventDispatcher.ResumeEvent(EventDispatcher.GroupByChanging);
      template.EventDispatcher.ResumeEvent(EventDispatcher.GroupByChanged);
      if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.ItemChanged && (action != NotifyCollectionChangedAction.Batch && action != NotifyCollectionChangedAction.Remove))
        throw new ArgumentException("Invalid action");
      template.MasterTemplate.SynchronizationService.EndDispatch(true);
      GridViewCollectionChangedEventArgs args = new GridViewCollectionChangedEventArgs(template, action, (object) fromDescriptor, (object) null, 0, string.Empty);
      template.EventDispatcher.RaiseEvent<GridViewCollectionChangedEventArgs>(EventDispatcher.GroupByChanged, (object) template, args);
    }

    public enum GroupLinkPosition
    {
      Top,
      Bottom,
    }
  }
}
