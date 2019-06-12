// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnChooserElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class ColumnChooserElement : ColumnChooserVisualElement, IGridView, IGridViewEventListener
  {
    private RadTextBoxElement filterTextBox;
    private StackLayoutElement panel;
    private RadScrollViewer scrollViewer;
    private GridViewTemplate template;
    private RadGridViewElement gridViewElement;
    private RadSortOrder sortOrder;

    public ColumnChooserElement()
    {
      this.WireEvents();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ColumnChooserFormMessage");
      this.StretchHorizontally = true;
      this.sortOrder = RadSortOrder.None;
      this.Class = nameof (ColumnChooserElement);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.scrollViewer = new RadScrollViewer();
      this.scrollViewer.StretchHorizontally = true;
      this.scrollViewer.StretchVertically = true;
      this.scrollViewer.ShowBorder = false;
      this.scrollViewer.ShowFill = false;
      this.Children.Add((RadElement) this.scrollViewer);
      this.panel = new StackLayoutElement();
      this.panel.Orientation = Orientation.Vertical;
      this.panel.StretchHorizontally = true;
      this.panel.StretchVertically = true;
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Children.Add((RadElement) this.panel);
      this.scrollViewer.Viewport = (RadElement) lightVisualElement;
      this.filterTextBox = this.CreateFilterTextBox();
      this.Children.Add((RadElement) this.filterTextBox);
    }

    protected virtual RadTextBoxElement CreateFilterTextBox()
    {
      RadTextBoxElement radTextBoxElement = new RadTextBoxElement();
      radTextBoxElement.StretchHorizontally = true;
      radTextBoxElement.ShowClearButton = true;
      radTextBoxElement.TextBoxItem.NullText = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ColumnChooserFilterTextBoxNullText");
      radTextBoxElement.Visibility = ElementVisibility.Collapsed;
      return radTextBoxElement;
    }

    [Browsable(false)]
    public StackLayoutElement ElementsHolder
    {
      get
      {
        return this.panel;
      }
    }

    public GridViewTemplate ViewTemplate
    {
      get
      {
        return this.template;
      }
      set
      {
        if (this.template == value)
          return;
        this.template = value;
        if (this.template == null)
          return;
        this.UpdateView();
      }
    }

    public IList<GridViewColumn> Columns
    {
      get
      {
        if (this.template == null)
          return (IList<GridViewColumn>) null;
        List<GridViewColumn> gridViewColumnList = new List<GridViewColumn>();
        ColumnGroupsViewDefinition viewDefinition = this.template.ViewDefinition as ColumnGroupsViewDefinition;
        if (viewDefinition != null)
          gridViewColumnList.AddRange(this.GetColumnGroups(viewDefinition.ColumnGroups));
        foreach (GridViewColumn column in (Collection<GridViewDataColumn>) this.template.Columns)
        {
          if (!column.IsVisible && column.VisibleInColumnChooser)
            gridViewColumnList.Add(column);
        }
        if (this.sortOrder != RadSortOrder.None)
          gridViewColumnList.Sort(new Comparison<GridViewColumn>(this.CompareColumns));
        return (IList<GridViewColumn>) gridViewColumnList;
      }
    }

    public RadSortOrder SortOrder
    {
      get
      {
        return this.sortOrder;
      }
      set
      {
        if (this.sortOrder == value)
          return;
        this.sortOrder = value;
        this.UpdateView();
      }
    }

    public RadScrollViewer ScrollViewer
    {
      get
      {
        return this.scrollViewer;
      }
    }

    public bool EnableFiltering
    {
      get
      {
        return this.FilterTextBox.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.FilterTextBox.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    public RadTextBoxElement FilterTextBox
    {
      get
      {
        return this.filterTextBox;
      }
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.UnwireEvents();
      this.Detach();
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      GridViewColumn dataContext = dragObject.GetDataContext() as GridViewColumn;
      if (dataContext != null && dataContext.OwnerTemplate != null && dataContext.OwnerTemplate.MasterTemplate != this.ViewTemplate.MasterTemplate)
        return false;
      return base.ProcessDragOver(currentMouseLocation, dragObject);
    }

    public void Initialize(RadGridViewElement gridViewElement, GridViewInfo viewInfo)
    {
      this.gridViewElement = gridViewElement;
      if (this.gridViewElement != null)
        gridViewElement.Template.SynchronizationService.AddListener((IGridViewEventListener) this);
      if (viewInfo == null)
        return;
      this.ViewTemplate = viewInfo.ViewTemplate;
    }

    public void Detach()
    {
      this.DetachGridElement();
      this.template = (GridViewTemplate) null;
    }

    private void DetachGridElement()
    {
      if (this.gridViewElement == null)
        return;
      this.gridViewElement.Template.SynchronizationService.RemoveListener((IGridViewEventListener) this);
      this.gridViewElement = (RadGridViewElement) null;
    }

    public void UpdateView()
    {
      if (this.template == null)
        return;
      this.panel.DisposeChildren();
      foreach (GridViewColumn column in (IEnumerable<GridViewColumn>) this.Columns)
      {
        ColumnChooserItemElementCreatingEventArgs e = new ColumnChooserItemElementCreatingEventArgs(new ColumnChooserItem(column, (IRadServiceProvider) this.gridViewElement), column, this.GridViewElement);
        this.OnItemElementCreating((object) this, e);
        if (e.ItemElement == null)
          e.ItemElement = new ColumnChooserItem(column, (IRadServiceProvider) this.gridViewElement);
        if (this.PassesFilter(e.ItemElement, this.FilterTextBox.Text))
          this.panel.Children.Add((RadElement) e.ItemElement);
      }
      if (this.panel.Children.Count == 0)
      {
        this.TextAlignment = ContentAlignment.MiddleCenter;
        this.scrollViewer.Visibility = ElementVisibility.Collapsed;
      }
      else
        this.scrollViewer.Visibility = ElementVisibility.Visible;
    }

    private IEnumerable<GridViewColumn> GetColumnGroups(
      ColumnGroupCollection groups)
    {
      foreach (GridViewColumnGroup group in (Collection<GridViewColumnGroup>) groups)
      {
        if (group.IsVisible)
        {
          foreach (GridViewColumn columnGroup in this.GetColumnGroups(group.Groups))
            yield return columnGroup;
        }
        else if (group.VisibleInColumnChooser && !group.IsVisible)
          yield return (GridViewColumn) new GridViewGroupColumn(group);
      }
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
      set
      {
        if (value == null || this.gridViewElement == value)
          return;
        this.DetachGridElement();
        this.Initialize(value, (GridViewInfo) null);
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        return (GridViewInfo) null;
      }
    }

    GridEventType IGridViewEventListener.DesiredEvents
    {
      get
      {
        return GridEventType.UI;
      }
    }

    EventListenerPriority IGridViewEventListener.Priority
    {
      get
      {
        return EventListenerPriority.Normal;
      }
    }

    GridEventProcessMode IGridViewEventListener.DesiredProcessMode
    {
      get
      {
        return GridEventProcessMode.Process;
      }
    }

    GridViewEventResult IGridViewEventListener.PreProcessEvent(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.ProcessEvent(
      GridViewEvent eventData)
    {
      GridViewColumn sender1 = eventData.Sender as GridViewColumn;
      if (sender1 != null && eventData.Info.Id == KnownEvents.PropertyChanged)
      {
        RadPropertyChangedEventArgs changedEventArgs = eventData.Arguments[0] as RadPropertyChangedEventArgs;
        if (changedEventArgs.Property == GridViewColumn.IsVisibleProperty || changedEventArgs.Property == GridViewColumn.VisibleInColumnChooserProperty)
        {
          this.template = sender1.OwnerTemplate;
          this.UpdateView();
        }
      }
      else if (eventData.Info.Id == KnownEvents.ViewChanged)
      {
        DataViewChangedEventArgs changedEventArgs = eventData.Arguments[0] as DataViewChangedEventArgs;
        GridViewTemplate sender2 = eventData.Sender as GridViewTemplate;
        if (changedEventArgs.Action == ViewChangedAction.Reset || changedEventArgs.Action == ViewChangedAction.ColumnGroupPropertyChanged)
        {
          this.template = sender2;
          this.UpdateView();
        }
      }
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.PostProcessEvent(
      GridViewEvent eventData)
    {
      throw new NotImplementedException();
    }

    bool IGridViewEventListener.AnalyzeQueue(List<GridViewEvent> events)
    {
      return false;
    }

    protected virtual int CompareColumns(GridViewColumn x, GridViewColumn y)
    {
      int num = Comparer<string>.Default.Compare(x.HeaderText, y.HeaderText);
      if (this.SortOrder == RadSortOrder.Descending)
        num *= -1;
      return num;
    }

    protected virtual bool PassesFilter(ColumnChooserItem item, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return true;
      return CultureInfo.CurrentCulture.CompareInfo.IndexOf(item.ColumnInfo.HeaderText, filter, CompareOptions.OrdinalIgnoreCase) >= 0;
    }

    protected virtual void WireEvents()
    {
      this.FilterTextBox.TextChanged += new EventHandler(this.FilterTextBoxTextChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.FilterTextBox.TextChanged -= new EventHandler(this.FilterTextBoxTextChanged);
    }

    public event ColumnChooserItemElementCreatingEventHandler ItemElementCreating;

    protected virtual void OnItemElementCreating(
      object sender,
      ColumnChooserItemElementCreatingEventArgs e)
    {
      if (this.ItemElementCreating == null)
        return;
      this.ItemElementCreating((object) this, e);
    }

    protected virtual void FilterTextBoxTextChanged(object sender, EventArgs e)
    {
      this.UpdateView();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      this.FilterTextBox.Measure(availableSize);
      availableSize.Height -= this.FilterTextBox.DesiredSize.Height;
      this.ScrollViewer.Measure(availableSize);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.FilterTextBox.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, this.FilterTextBox.DesiredSize.Height));
      this.ScrollViewer.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y + this.FilterTextBox.DesiredSize.Height, clientRectangle.Width, clientRectangle.Height - this.FilterTextBox.DesiredSize.Height));
      return sizeF;
    }
  }
}
