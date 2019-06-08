// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupFieldElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GroupFieldElement : GridGroupVisualElement
  {
    private SortDescriptor sortDescription;
    private ArrowPrimitive arrow;
    private RadButtonElement removeButton;
    private GridViewColumn column;

    static GroupFieldElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GroupFieldElementStateManager(), typeof (GroupFieldElement));
    }

    public GroupFieldElement(
      TemplateGroupsElement template,
      GroupDescriptor groupDescription,
      SortDescriptor sortDescription)
      : base(template, groupDescription)
    {
      this.sortDescription = sortDescription;
      this.SetSortingIndicator();
      this.column = (GridViewColumn) this.TemplateElement.ViewTemplate.Columns[this.sortDescription.PropertyName ?? string.Empty];
      if (this.column == null)
        return;
      this.UpdateButtonVisibility();
      this.column.PropertyChanged += new PropertyChangedEventHandler(this.column_PropertyChanged);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrag = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.NotifyParentOnMouseInput = true;
      this.DrawBorder = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.arrow = new ArrowPrimitive();
      this.arrow.Class = "GroupFieldArrowElement";
      this.Children.Add((RadElement) this.arrow);
      this.removeButton = (RadButtonElement) new GridGroupHeaderItemButtonElement();
      this.removeButton.Class = "HeaderItemButtonElement";
      this.removeButton.Children[0].Class = "HeaderItemButtonElementFill";
      this.removeButton.Children[2].Class = "HeaderItemButtonElementBorder";
      this.removeButton.NotifyParentOnMouseInput = false;
      this.removeButton.Click += new EventHandler(this.RemoveButton_Click);
      this.Children.Add((RadElement) this.removeButton);
    }

    public GridViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public SortDescriptor SortDescription
    {
      get
      {
        return this.sortDescription;
      }
    }

    public RadButtonElement RemoveButton
    {
      get
      {
        return this.removeButton;
      }
    }

    public ArrowPrimitive Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.column.AllowGroup || e.Button != MouseButtons.Left)
        return;
      this.TemplateElement.GroupPanelElement.GetService<RadDragDropService>()?.Start((object) this);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (!this.ControlBoundingRectangle.Contains(e.Location))
        return;
      if (this.sortDescription.Direction == ListSortDirection.Ascending)
        this.sortDescription.Direction = ListSortDirection.Descending;
      else
        this.sortDescription.Direction = ListSortDirection.Ascending;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      RadDragDropService service = this.TemplateElement.GroupPanelElement.GetService<RadDragDropService>();
      if (args.IsBegin && service.State != RadServiceState.Working)
        service.BeginDrag(this.ElementTree.Control.PointToScreen(args.Location), (ISupportDrag) this);
      if (service.State == RadServiceState.Working)
        service.DoMouseMove(this.ElementTree.Control.PointToScreen(args.Location));
      if (args.IsEnd)
        service.EndDrag();
      args.Handled = true;
    }

    private void SetSortingIndicator()
    {
      if (this.sortDescription == null)
        return;
      if (this.sortDescription.Direction == ListSortDirection.Ascending)
        this.arrow.Direction = Telerik.WinControls.ArrowDirection.Up;
      else
        this.arrow.Direction = Telerik.WinControls.ArrowDirection.Down;
    }

    private void RemoveButton_Click(object sender, EventArgs e)
    {
      if (TemplateGroupsElement.RaiseGroupByChanging(this.TemplateElement.ViewTemplate, this.Description, NotifyCollectionChangedAction.Remove))
        return;
      if (this.Description.GroupNames.Count == 1)
        this.TemplateElement.ViewTemplate.GroupDescriptors.Remove(this.Description);
      this.Description.GroupNames.Remove(this.sortDescription);
      TemplateGroupsElement.RaiseGroupByChanged(this.TemplateElement.ViewTemplate, this.Description, NotifyCollectionChangedAction.Remove);
    }

    private void column_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == GridViewColumn.AllowGroupProperty.Name))
        return;
      this.UpdateButtonVisibility();
    }

    protected override void DisposeManagedResources()
    {
      this.column.PropertyChanged -= new PropertyChangedEventHandler(this.column_PropertyChanged);
      base.DisposeManagedResources();
    }

    private void UpdateButtonVisibility()
    {
      this.Text = this.column.HeaderText;
      this.RemoveButton.Visibility = this.column.AllowGroup ? ElementVisibility.Visible : ElementVisibility.Collapsed;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.arrow != null)
      {
        this.arrow.Measure(availableSize);
        if (this.arrow.Alignment != ContentAlignment.TopCenter)
          sizeF.Width += this.arrow.DesiredSize.Width;
        sizeF.Height = Math.Max(sizeF.Height, this.arrow.DesiredSize.Height);
      }
      if (this.removeButton != null && this.removeButton.Visibility != ElementVisibility.Collapsed)
      {
        this.removeButton.Measure(availableSize);
        if (this.removeButton.Visibility == ElementVisibility.Visible)
          sizeF.Width += this.removeButton.DesiredSize.Width;
        sizeF.Height = Math.Max(sizeF.Height, this.removeButton.DesiredSize.Height);
      }
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.Layout.Arrange(clientRectangle);
      RectangleF finalRect1 = RectangleF.Empty;
      RectangleF finalRect2 = RectangleF.Empty;
      if (this.arrow.Alignment == ContentAlignment.TopCenter)
      {
        finalRect1 = !this.RightToLeft ? new RectangleF(clientRectangle.Right - this.removeButton.DesiredSize.Width, clientRectangle.Y, this.removeButton.DesiredSize.Width, this.removeButton.DesiredSize.Height) : new RectangleF(clientRectangle.X, clientRectangle.Y, this.removeButton.DesiredSize.Width, this.removeButton.DesiredSize.Height);
        finalRect2 = new RectangleF(clientRectangle.X + (float) ((double) clientRectangle.Width / 2.0 - (double) this.arrow.DesiredSize.Width / 2.0), 0.0f, this.arrow.DesiredSize.Width, this.arrow.DesiredSize.Height);
      }
      else if (this.RightToLeft)
      {
        finalRect1 = new RectangleF(clientRectangle.X, clientRectangle.Y, this.removeButton.DesiredSize.Width, this.removeButton.DesiredSize.Height);
        finalRect2 = new RectangleF(finalRect1.Right + (float) this.arrow.Margin.Left, (float) ((double) clientRectangle.Y + (double) this.arrow.Margin.Top + ((double) clientRectangle.Height - (double) this.arrow.DesiredSize.Height) / 2.0), this.arrow.DesiredSize.Width, this.arrow.DesiredSize.Height);
      }
      else
      {
        finalRect1 = new RectangleF(clientRectangle.Right - this.removeButton.DesiredSize.Width - (float) this.removeButton.Margin.Right, clientRectangle.Y, this.removeButton.DesiredSize.Width, this.removeButton.DesiredSize.Height);
        finalRect2 = new RectangleF(finalRect1.X - this.arrow.DesiredSize.Width - (float) this.arrow.Margin.Right, (float) ((double) clientRectangle.Y + (double) this.arrow.Margin.Top + ((double) clientRectangle.Height - (double) this.arrow.DesiredSize.Height) / 2.0), this.arrow.DesiredSize.Width, this.arrow.DesiredSize.Height);
      }
      this.removeButton.Arrange(finalRect1);
      this.arrow.Arrange(finalRect2);
      return finalSize;
    }

    protected override object GetDragContextCore()
    {
      return (object) new GroupFieldDragDropContext(this.Description, this.sortDescription, this.TemplateElement.ViewTemplate);
    }

    protected override bool ProcessGroupFieldDropOverride(
      Point dropLocation,
      GroupFieldDragDropContext context)
    {
      bool isDroppedAtLeft = RadGridViewDragDropService.IsDroppedAtLeft(dropLocation, this.Size.Width);
      if (this.Description != context.GroupDescription)
      {
        SortDescriptor sortDescription = context.SortDescription;
        GridViewTemplate viewTemplate = context.ViewTemplate;
        context.GroupDescription.GroupNames.Remove(sortDescription);
        if (context.GroupDescription.GroupNames.Count == 0)
          viewTemplate.DataView.GroupDescriptors.Remove(context.GroupDescription);
        RadGridViewDragDropService.InsertOnLeftOrRight<SortDescriptor>(isDroppedAtLeft, (Collection<SortDescriptor>) this.Description.GroupNames, this.sortDescription, sortDescription);
      }
      else
        RadGridViewDragDropService.MoveOnLeftOrRight<SortDescriptor>(isDroppedAtLeft, (Collection<SortDescriptor>) this.Description.GroupNames, this.sortDescription, context.SortDescription);
      TemplateGroupsElement.RaiseGroupByChanged(context.ViewTemplate, context.GroupDescription, NotifyCollectionChangedAction.Batch);
      return true;
    }

    protected override void ProcessColumnDrop(Point dropLocation, GridViewColumn column)
    {
      if (TemplateGroupsElement.RaiseGroupByChanging(this.TemplateElement.ViewTemplate, this.Description, NotifyCollectionChangedAction.ItemChanged))
        return;
      RadGridViewDragDropService.InsertOnLeftOrRight<SortDescriptor>(RadGridViewDragDropService.IsDroppedAtLeft(dropLocation, this.Size.Width), (Collection<SortDescriptor>) this.Description.GroupNames, this.sortDescription, new SortDescriptor(column.Name, ListSortDirection.Ascending));
      TemplateGroupsElement.RaiseGroupByChanged(this.TemplateElement.ViewTemplate, this.Description, NotifyCollectionChangedAction.ItemChanged);
    }
  }
}
