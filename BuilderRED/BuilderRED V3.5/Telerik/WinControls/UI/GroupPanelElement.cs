// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupPanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class GroupPanelElement : GridVisualElement, IGridView, IRadServiceProvider, IGridViewEventListener
  {
    public static RadProperty FieldDragHintProperty = RadProperty.Register(nameof (FieldDragHint), typeof (RadImageShape), typeof (GroupPanelElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    private Size DefaultScrollViewerMinSize = new Size(0, 23);
    private Size DefaultScrollViewerMaxSize = new Size(0, 100);
    private bool allowResize = true;
    private bool showScrollBars = true;
    private GroupPanelSizeGripElement sizeGripElement;
    private RadGridViewElement gridViewElement;
    private LightVisualElement groupHeader;
    private GridViewInfo viewInfo;
    private ScrollViewElement scrollViewer;
    private TemplateGroupsElement tableGroups;
    private IRadServiceProvider serviceProvider;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.AllowDrop = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.groupHeader = new LightVisualElement();
      this.groupHeader.TextAlignment = ContentAlignment.TopCenter;
      this.groupHeader.Padding = new Padding(0, 10, 0, 0);
      this.groupHeader.StretchHorizontally = false;
      this.groupHeader.StretchVertically = true;
      this.groupHeader.Class = "GroupPanelHeader";
      this.groupHeader.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.groupHeader);
      this.scrollViewer = new ScrollViewElement();
      this.scrollViewer.AutoSize = true;
      this.scrollViewer.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.scrollViewer.ViewElement.Children.Add((RadElement) new GroupPanelContainer());
      this.Children.Add((RadElement) this.scrollViewer);
      this.sizeGripElement = new GroupPanelSizeGripElement();
      this.sizeGripElement.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.sizeGripElement);
      this.TextAlignment = ContentAlignment.MiddleCenter;
      this.GradientStyle = GradientStyles.Solid;
      this.UpdateView();
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.Detach();
    }

    [VsbBrowsable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadImageShape FieldDragHint
    {
      get
      {
        return (RadImageShape) this.GetValue(GroupPanelElement.FieldDragHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(GroupPanelElement.FieldDragHintProperty, (object) value);
      }
    }

    public IRadServiceProvider ServiceProvider
    {
      get
      {
        return this.serviceProvider;
      }
      set
      {
        this.serviceProvider = value;
      }
    }

    public GroupPanelContainer PanelContainer
    {
      get
      {
        return (GroupPanelContainer) this.scrollViewer.ViewElement.Children[0];
      }
    }

    public LightVisualElement Header
    {
      get
      {
        return this.groupHeader;
      }
    }

    public GroupPanelSizeGripElement SizeGrip
    {
      get
      {
        return this.sizeGripElement;
      }
    }

    public ScrollViewElement ScrollView
    {
      get
      {
        return this.scrollViewer;
      }
    }

    public bool AllowResize
    {
      get
      {
        return this.allowResize;
      }
      set
      {
        if (this.allowResize == value)
          return;
        this.allowResize = value;
        this.UpdateSizeGripVisibility();
      }
    }

    public int MaxHeight
    {
      get
      {
        return this.scrollViewer.MaxSize.Height;
      }
      set
      {
        if (this.scrollViewer.MaxSize.Height == value || this.scrollViewer.MinSize.Height >= value)
          return;
        this.scrollViewer.MaxSize = new Size(0, value);
      }
    }

    [DefaultValue(true)]
    public bool ShowScrollBars
    {
      get
      {
        return this.showScrollBars;
      }
      set
      {
        if (this.showScrollBars == value)
          return;
        this.showScrollBars = value;
        this.InvalidateMeasure(false);
        this.UpdateLayout();
      }
    }

    public void UpdateVisibility()
    {
      if (this.viewInfo == null || this.viewInfo.ViewTemplate.Columns.Count == 0 || (!this.GridViewElement.ShowGroupPanel || !this.ViewInfo.ViewTemplate.EnableGrouping))
      {
        this.Visibility = ElementVisibility.Collapsed;
        if (this.GridViewElement == null)
          return;
        if (this.viewInfo == null || this.viewInfo.ViewTemplate.Columns.Count == 0)
        {
          int num1 = (int) this.GridViewElement.TableElement.SetDefaultValueOverride(RadElement.PositionOffsetProperty, (object) new SizeF(0.0f, 0.0f));
          int num2 = (int) this.GridViewElement.TableElement.SetDefaultValueOverride(RadElement.MarginProperty, (object) new Padding(0, 0, 0, 0));
        }
        else
        {
          int num1 = (int) this.GridViewElement.TableElement.SetDefaultValueOverride(RadElement.PositionOffsetProperty, (object) new SizeF(0.0f, -1f));
          int num2 = (int) this.GridViewElement.TableElement.SetDefaultValueOverride(RadElement.MarginProperty, (object) new Padding(0, 0, 0, -1));
        }
      }
      else
      {
        this.Visibility = ElementVisibility.Visible;
        if (this.GridViewElement == null)
          return;
        int num1 = (int) this.GridViewElement.TableElement.SetDefaultValueOverride(RadElement.PositionOffsetProperty, (object) new SizeF(0.0f, 0.0f));
        int num2 = (int) this.GridViewElement.TableElement.SetDefaultValueOverride(RadElement.MarginProperty, (object) new Padding(0, 0, 0, 0));
      }
    }

    public void Initialize(RadGridViewElement rootElement, GridViewInfo viewInfo)
    {
      this.gridViewElement = rootElement;
      this.viewInfo = viewInfo;
      this.viewInfo.ViewTemplate.MasterTemplate.SynchronizationService.AddListener((IGridViewEventListener) this);
    }

    public void Detach()
    {
      this.viewInfo.ViewTemplate.MasterTemplate.SynchronizationService.RemoveListener((IGridViewEventListener) this);
    }

    public void UpdateView()
    {
      this.scrollViewer.Visibility = ElementVisibility.Hidden;
      if (this.GridViewElement == null || this.GridViewElement.Template.AllowDragToGroup)
        this.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("GroupingPanelDefaultMessage");
      else
        this.Text = string.Empty;
      this.UpdateVisibility();
      if (this.viewInfo == null)
        return;
      if (this.tableGroups != null)
      {
        this.PanelContainer.Children.Remove((RadElement) this.tableGroups);
        this.tableGroups.Dispose();
        this.tableGroups = (TemplateGroupsElement) null;
      }
      this.tableGroups = new TemplateGroupsElement(this, this.viewInfo.ViewTemplate);
      this.tableGroups.ZIndex = 0;
      this.tableGroups.Margin = new Padding(5);
      this.tableGroups.StretchHorizontally = true;
      this.tableGroups.StretchVertically = true;
      this.tableGroups.UpdateHierarchy();
      if (this.tableGroups.Children.Count == 0)
      {
        this.groupHeader.Visibility = ElementVisibility.Collapsed;
        this.tableGroups.Dispose();
        this.tableGroups = (TemplateGroupsElement) null;
      }
      else
      {
        this.scrollViewer.Visibility = ElementVisibility.Visible;
        this.groupHeader.Visibility = ElementVisibility.Visible;
        this.groupHeader.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("GroupingPanelHeader");
        this.PanelContainer.Children.Add((RadElement) this.tableGroups);
        this.Text = (string) null;
      }
      this.InvalidateMeasure(true);
      if (this.GridViewElement.SplitMode != RadGridViewSplitMode.None)
      {
        this.UpdateLayout();
        this.GridViewElement.InitalizeSplitGridSizes();
      }
      this.UpdateSizeGripVisibility();
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    public GridViewInfo ViewInfo
    {
      get
      {
        return this.viewInfo;
      }
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      object dataContext = dragObject.GetDataContext();
      if (dataContext is GridViewDataColumn)
      {
        GridViewColumn gridViewColumn = dataContext as GridViewColumn;
        GroupDescriptor groupDescription = new GroupDescriptor();
        groupDescription.GroupNames.Add(new SortDescriptor(gridViewColumn.Name, ListSortDirection.Ascending));
        if (TemplateGroupsElement.RaiseGroupByChanging(gridViewColumn.OwnerTemplate, groupDescription, NotifyCollectionChangedAction.Add))
          return;
        gridViewColumn.OwnerTemplate.DataView.GroupDescriptors.Add(groupDescription);
        TemplateGroupsElement.RaiseGroupByChanged(gridViewColumn.OwnerTemplate, groupDescription, NotifyCollectionChangedAction.Add);
      }
      else
      {
        if (!(dataContext is GroupFieldDragDropContext))
          return;
        GroupFieldDragDropContext fieldDragDropContext = dataContext as GroupFieldDragDropContext;
        SortDescriptor sortDescription = fieldDragDropContext.SortDescription;
        GridViewTemplate viewTemplate = fieldDragDropContext.ViewTemplate;
        GroupDescriptor groupDescription1 = fieldDragDropContext.GroupDescription;
        if (TemplateGroupsElement.RaiseGroupByChanging(viewTemplate, groupDescription1, NotifyCollectionChangedAction.Batch))
          return;
        groupDescription1.GroupNames.Remove(sortDescription);
        GroupDescriptor groupDescription2 = new GroupDescriptor();
        groupDescription2.GroupNames.Add(sortDescription);
        viewTemplate.GroupDescriptors.Add(groupDescription2);
        if (groupDescription1.GroupNames.Count == 0)
          viewTemplate.DataView.GroupDescriptors.Remove(groupDescription1);
        TemplateGroupsElement.RaiseGroupByChanged(viewTemplate, groupDescription2, NotifyCollectionChangedAction.Batch);
      }
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      object dataContext1 = dragObject.GetDataContext();
      if (dataContext1 is GridViewColumn)
      {
        GridViewColumn dataContext2 = dragObject.GetDataContext() as GridViewColumn;
        if (this.GridViewElement.Template.VirtualMode)
          return false;
        return dataContext2.CanDragToGroup();
      }
      if (dataContext1 is GroupFieldDragDropContext)
        return (dataContext1 as GroupFieldDragDropContext).GroupDescription.GroupNames.Count > 1;
      return false;
    }

    public T GetService<T>() where T : RadService
    {
      if (this.serviceProvider != null)
        return this.serviceProvider.GetService<T>();
      return default (T);
    }

    public void RegisterService(RadService service)
    {
      if (this.serviceProvider == null)
        return;
      this.serviceProvider.RegisterService(service);
    }

    public GridEventType DesiredEvents
    {
      get
      {
        return GridEventType.UI;
      }
    }

    public EventListenerPriority Priority
    {
      get
      {
        return EventListenerPriority.Normal;
      }
    }

    public GridEventProcessMode DesiredProcessMode
    {
      get
      {
        return GridEventProcessMode.Process | GridEventProcessMode.AnalyzeQueue;
      }
    }

    public GridViewEventResult PreProcessEvent(GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    public GridViewEventResult ProcessEvent(GridViewEvent eventData)
    {
      bool flag1 = GridViewSynchronizationService.IsTemplatePropertyChangedEvent(eventData) && (eventData.Arguments[0] as PropertyChangedEventArgs).PropertyName == "EnableGrouping";
      bool flag2 = GridViewSynchronizationService.IsTemplatePropertyChangedEvent(eventData) && (eventData.Arguments[0] as PropertyChangedEventArgs).PropertyName == "AllowDragToGroup";
      if (flag1 || GridViewSynchronizationService.IsColumnsCollectionChangedEvent(eventData))
        this.UpdateVisibility();
      else if (GridViewSynchronizationService.IsGroupCollectionChangedEvent(eventData) || eventData.Info.Id == KnownEvents.ViewChanged && (eventData.Arguments[0] as DataViewChangedEventArgs).Action == ViewChangedAction.Reset)
        this.UpdateView();
      else if (flag2 || eventData.Info.Id == KnownEvents.LocalizationProviderChanged)
        this.UpdateView();
      return (GridViewEventResult) null;
    }

    public GridViewEventResult PostProcessEvent(GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    public bool AnalyzeQueue(List<GridViewEvent> events)
    {
      bool flag1 = false;
      GridViewEvent gridViewEvent1 = (GridViewEvent) null;
      for (int index = events.Count - 1; index >= 0; --index)
      {
        GridViewEvent gridViewEvent2 = events[index];
        bool flag2 = GridViewSynchronizationService.IsGroupCollectionChangedEvent(gridViewEvent2);
        if (flag2)
        {
          if (gridViewEvent1 == null || gridViewEvent1 == gridViewEvent2)
            gridViewEvent1 = gridViewEvent2;
          else if (flag2)
          {
            events.Remove(gridViewEvent2);
            flag1 = true;
          }
        }
      }
      return flag1;
    }

    private void UpdateSizeGripVisibility()
    {
      if (this.AllowResize && this.PanelContainer.Children.Count > 0)
        this.sizeGripElement.Visibility = ElementVisibility.Visible;
      else
        this.sizeGripElement.Visibility = ElementVisibility.Collapsed;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      int num = this.scrollViewer.VScrollBar.Value - args.Offset.Height;
      RadScrollBarElement vscrollBar = this.scrollViewer.VScrollBar;
      if (vscrollBar.ControlBoundingRectangle.Contains(args.Location) || this.ElementTree.GetElementAtPoint(args.Location) is GroupFieldElement)
        return;
      if (num > vscrollBar.Maximum - vscrollBar.LargeChange + 1)
        num = vscrollBar.Maximum - vscrollBar.LargeChange + 1;
      if (num < vscrollBar.Minimum)
        num = vscrollBar.Minimum;
      this.scrollViewer.VScrollBar.Value = num;
      args.Handled = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF1 = base.MeasureOverride(availableSize);
      if (this.PanelContainer.Children.Count == 0)
      {
        SizeF sizeF2 = new SizeF(sizeF1.Width, (float) this.DefaultScrollViewerMinSize.Height);
        if ((double) this.DpiScaleFactor.Width != 1.0 || (double) this.DpiScaleFactor.Height != 1.0)
        {
          sizeF2.Width *= this.DpiScaleFactor.Width;
          sizeF2.Height *= this.DpiScaleFactor.Height;
        }
        return sizeF2;
      }
      if (this.ShowScrollBars)
      {
        if (this.DefaultScrollViewerMaxSize.Height != 0)
          availableSize.Height = Math.Min(availableSize.Height, (float) this.DefaultScrollViewerMaxSize.Height);
        if (this.DefaultScrollViewerMaxSize.Width != 0)
          availableSize.Width = Math.Min(availableSize.Width, (float) this.DefaultScrollViewerMaxSize.Width);
      }
      this.sizeGripElement.Measure(availableSize);
      availableSize.Height -= this.sizeGripElement.DesiredSize.Height;
      this.groupHeader.Measure(availableSize);
      availableSize.Width -= this.groupHeader.DesiredSize.Width;
      this.scrollViewer.Measure(availableSize);
      SizeF sizeF3 = new SizeF(availableSize.Width, Math.Max(this.scrollViewer.DesiredSize.Height, this.groupHeader.DesiredSize.Height) + this.sizeGripElement.DesiredSize.Height);
      if (this.ShowScrollBars)
      {
        if (this.DefaultScrollViewerMinSize.Height != 0)
          sizeF3.Height = Math.Max(sizeF3.Height, (float) this.DefaultScrollViewerMinSize.Height);
        if (this.DefaultScrollViewerMinSize.Width != 0)
          sizeF3.Width = Math.Max(sizeF3.Width, (float) this.DefaultScrollViewerMinSize.Width);
      }
      return sizeF3;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.sizeGripElement.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Bottom - this.sizeGripElement.DesiredSize.Height, clientRectangle.Width, this.sizeGripElement.DesiredSize.Height));
      clientRectangle.Height -= this.sizeGripElement.DesiredSize.Height;
      this.groupHeader.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, this.groupHeader.DesiredSize.Width, clientRectangle.Height));
      clientRectangle.Width -= this.groupHeader.DesiredSize.Width;
      RectangleF finalRect = new RectangleF(clientRectangle.X + this.groupHeader.DesiredSize.Width, clientRectangle.Y, clientRectangle.Width, clientRectangle.Height);
      if ((double) finalRect.Width > 20.0)
        this.scrollViewer.Arrange(finalRect);
      return finalSize;
    }
  }
}
