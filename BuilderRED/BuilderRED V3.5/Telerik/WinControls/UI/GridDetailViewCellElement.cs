// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDetailViewCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridDetailViewCellElement : GridCellElement
  {
    private GridTableElement rowView;
    private RadPageViewElement pageViewElement;
    private bool suspendTabChanging;

    public GridDetailViewCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
      if (this.ViewTemplate.Templates.Count > 1 || row.ViewTemplate.ShowChildViewCaptions)
      {
        IRadPageViewProvider pageViewProvider = this.CreatePageViewProvider();
        this.pageViewElement = this.CreatePageViewElement(pageViewProvider);
        this.UpdateTabsPosition();
        this.UpdatePageViewItems(pageViewProvider);
      }
      else
        this.Children.Add((RadElement) this.CreateChildTableElement());
    }

    protected override void DisposeManagedResources()
    {
      if (this.pageViewElement != null)
        this.pageViewElement = (RadPageViewElement) null;
      base.DisposeManagedResources();
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      base.Initialize(column, row);
      this.UpdateTabsPosition();
      this.ViewTemplate.PropertyChanged += new PropertyChangedEventHandler(this.ViewTemplate_PropertyChanged);
      if (this.pageViewElement == null)
        return;
      foreach (RadPageViewItem radPageViewItem in (IEnumerable<RadPageViewItem>) this.pageViewElement.Items)
      {
        radPageViewItem.RadPropertyChanging += new RadPropertyChangingEventHandler(this.item_RadPropertyChanging);
        radPageViewItem.RadPropertyChanged += new RadPropertyChangedEventHandler(this.item_RadPropertyChanged);
      }
    }

    public void Detach()
    {
      if (this.pageViewElement != null)
      {
        foreach (RadPageViewItem radPageViewItem in (IEnumerable<RadPageViewItem>) this.pageViewElement.Items)
        {
          radPageViewItem.RadPropertyChanging -= new RadPropertyChangingEventHandler(this.item_RadPropertyChanging);
          radPageViewItem.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.item_RadPropertyChanged);
        }
      }
      if (this.ViewTemplate != null)
        this.ViewTemplate.PropertyChanged -= new PropertyChangedEventHandler(this.ViewTemplate_PropertyChanged);
      if (this.ChildTableElement.ViewInfo == null)
        return;
      this.ChildTableElement.Detach();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
    }

    protected override bool CanUpdateInfo
    {
      get
      {
        if (!this.UpdatingInfo && this.RowElement != null)
          return this.RowElement.RowInfo != null;
        return false;
      }
    }

    protected override void UpdateInfoCore()
    {
      base.UpdateInfoCore();
      this.EnsureSelectedTabItem();
    }

    public override void SetContent()
    {
      if (this.pageViewElement is RadPageViewExplorerBarElement)
      {
        RadPageViewItem radPageViewItem = (RadPageViewItem) null;
        for (int index = this.pageViewElement.Items.Count - 1; index >= 0; --index)
        {
          if (this.HierarchyRow.Views[index].ViewTemplate != this.ViewTemplate)
          {
            RadPageViewExplorerBarItem viewExplorerBarItem = (RadPageViewExplorerBarItem) this.pageViewElement.Items[index];
            GridViewInfo view = this.HierarchyRow.Views[this.HierarchyRow.Views.Count - index - 1];
            GridTableElement child = (GridTableElement) viewExplorerBarItem.AssociatedContentAreaElement.Children[0];
            if (viewExplorerBarItem.Tag != view || child.ViewTemplate == null)
            {
              this.InitializePageViewItem((RadPageViewItem) viewExplorerBarItem, view);
              child.Detach();
              child.Initialize(this.GridViewElement, view);
              if (view == this.HierarchyRow.ActiveView)
              {
                radPageViewItem = (RadPageViewItem) viewExplorerBarItem;
                this.rowView = child;
              }
            }
          }
        }
        if (radPageViewItem == null)
          return;
        this.pageViewElement.SelectedItem = radPageViewItem;
      }
      else
      {
        if (this.pageViewElement != null)
        {
          RadPageViewItem radPageViewItem1 = (RadPageViewItem) null;
          if (this.pageViewElement is RadPageViewStripElement || this.pageViewElement is RadPageViewOutlookElement)
          {
            int index1 = this.ViewTemplate.IsSelfReference ? 1 : 0;
            int index2 = 0;
            while (index2 < this.pageViewElement.Items.Count)
            {
              if (this.HierarchyRow.Views[index1].ViewTemplate != this.ViewTemplate)
              {
                RadPageViewItem radPageViewItem2 = this.pageViewElement.Items[index2];
                GridViewInfo view = this.HierarchyRow.Views[index1];
                this.InitializePageViewItem(radPageViewItem2, view);
                if (view == this.HierarchyRow.ActiveView)
                  radPageViewItem1 = radPageViewItem2;
              }
              ++index2;
              ++index1;
            }
          }
          else
          {
            for (int index = this.pageViewElement.Items.Count - 1; index >= 0; --index)
            {
              if (this.HierarchyRow.Views[this.HierarchyRow.Views.Count - index - 1].ViewTemplate != this.ViewTemplate)
              {
                RadPageViewItem radPageViewItem2 = this.pageViewElement.Items[index];
                GridViewInfo view = this.HierarchyRow.Views[this.HierarchyRow.Views.Count - index - 1];
                this.InitializePageViewItem(radPageViewItem2, view);
                if (view == this.HierarchyRow.ActiveView)
                  radPageViewItem1 = radPageViewItem2;
              }
            }
          }
          if (radPageViewItem1 != null)
          {
            this.suspendTabChanging = true;
            this.pageViewElement.SelectedItem = radPageViewItem1;
            this.suspendTabChanging = false;
          }
        }
        if (this.rowView.ViewInfo != this.HierarchyRow.ActiveView)
        {
          bool flag = this.rowView.ViewTemplate != this.HierarchyRow.ActiveView.ViewTemplate;
          this.rowView.Detach();
          this.rowView.Initialize(this.GridViewElement, this.HierarchyRow.ActiveView);
          if (!flag)
            return;
          this.UpdateRowViewLayout();
        }
        else
          this.rowView.ViewElement.UpdateRows();
      }
    }

    public virtual void UpdateTabItemsVisibility()
    {
      if (this.PageViewElement == null || this.DetailsRow == null)
        return;
      this.InitializePageViewItem(this.PageViewElement.SelectedItem, this.HierarchyRow.ActiveView);
      if (this.PageViewElement.SelectedItem.Visibility == ElementVisibility.Visible)
        return;
      foreach (RadPageViewItem radPageViewItem in (IEnumerable<RadPageViewItem>) this.PageViewElement.Items)
      {
        if (radPageViewItem.Visibility == ElementVisibility.Visible)
        {
          this.PageViewElement.SelectedItem = radPageViewItem;
          break;
        }
      }
    }

    public override GridViewRowInfo RowInfo
    {
      get
      {
        return this.RowElement.RowInfo;
      }
      protected set
      {
      }
    }

    public GridTableElement ChildTableElement
    {
      get
      {
        return this.rowView;
      }
    }

    public RadPageViewElement PageViewElement
    {
      get
      {
        return this.pageViewElement;
      }
    }

    protected GridViewDetailsRowInfo DetailsRow
    {
      get
      {
        return (GridViewDetailsRowInfo) this.RowInfo;
      }
    }

    protected GridViewHierarchyRowInfo HierarchyRow
    {
      get
      {
        return (GridViewHierarchyRowInfo) this.DetailsRow.Owner;
      }
    }

    private void item_RadPropertyChanging(object sender, RadPropertyChangingEventArgs e)
    {
      if (this.suspendTabChanging || sender is RadPageViewExplorerBarItem || e.Property != RadPageViewItem.IsSelectedProperty)
        return;
      e.Cancel = !this.OnPageChanging((RadPageViewItem) sender);
    }

    private void item_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (this.suspendTabChanging || sender is RadPageViewExplorerBarItem || (e.Property != RadPageViewItem.IsSelectedProperty || !(bool) e.NewValue))
        return;
      this.OnPageChanged((RadPageViewItem) sender);
    }

    private void explorerBarElement_ExpandedChanged(
      object sender,
      RadPageViewExpandedChangedEventArgs e)
    {
      if (this.GridViewElement.UseScrollbarsInHierarchy)
        return;
      this.RowInfo.Height = -1;
      this.TableElement.ViewElement.UpdateRows(true);
    }

    private void ViewTemplate_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "ChildViewTabsPosition"))
        return;
      this.UpdateTabsPosition();
    }

    protected virtual bool OnPageChanging(RadPageViewItem page)
    {
      return true;
    }

    protected virtual void OnPageChanged(RadPageViewItem page)
    {
      this.SetActiveView(page);
    }

    protected virtual void SetActiveView(RadPageViewItem pageItem)
    {
      if (pageItem == null)
        return;
      if (this.GridViewElement.EditorManager.IsInEditMode)
        this.GridViewElement.EditorManager.EndEdit();
      this.HierarchyRow.ActiveView = (GridViewInfo) pageItem.Tag;
      if (this.DetailsRow == null)
        return;
      bool flag = this.rowView.ViewTemplate != this.HierarchyRow.ActiveView.ViewTemplate;
      this.rowView.Detach();
      this.rowView.Initialize(this.GridViewElement, this.HierarchyRow.ActiveView);
      if (!this.TableElement.GridViewElement.UseScrollbarsInHierarchy)
      {
        this.DetailsRow.resetActualHeight = true;
        this.DetailsRow.Height = -1;
      }
      if (!flag)
        return;
      this.UpdateRowViewLayout();
    }

    protected virtual void UpdatePageViewItems(IRadPageViewProvider pageViewProvider)
    {
      this.suspendTabChanging = true;
      if (!(pageViewProvider is RadPageViewExplorerBarProvider))
      {
        GridTableElement childTableElement = this.CreateChildTableElement();
        if (this.GridViewElement.UseScrollbarsInHierarchy && !(pageViewProvider is RadPageViewStripProvider))
          childTableElement.StretchVertically = true;
        this.pageViewElement.ContentArea.Children.Add((RadElement) childTableElement);
      }
      this.Children.Add((RadElement) this.pageViewElement);
      while (this.PageViewElement.Items.Count > 0)
      {
        this.PageViewElement.Items[0].RadPropertyChanging -= new RadPropertyChangingEventHandler(this.item_RadPropertyChanging);
        this.PageViewElement.Items[0].RadPropertyChanged -= new RadPropertyChangedEventHandler(this.item_RadPropertyChanged);
        this.PageViewElement.RemoveItem(this.PageViewElement.Items[0]);
      }
      RadPageViewItem selectedItem = (RadPageViewItem) null;
      if (this.pageViewElement is RadPageViewStripElement || this.pageViewElement is RadPageViewOutlookElement)
      {
        for (int index = 0; index < this.HierarchyRow.Views.Count; ++index)
        {
          if (this.HierarchyRow.Views[index].ViewTemplate != this.ViewTemplate)
          {
            RadPageViewItem pageViewItem = this.CreatePageViewItem(pageViewProvider, this.HierarchyRow.Views[index]);
            this.pageViewElement.AddItem(pageViewItem);
            if (pageViewItem.Tag == this.HierarchyRow.ActiveView)
              selectedItem = pageViewItem;
          }
        }
      }
      else
      {
        for (int index = this.HierarchyRow.Views.Count - 1; index >= 0; --index)
        {
          if (this.HierarchyRow.Views[index].ViewTemplate != this.ViewTemplate)
          {
            RadPageViewItem pageViewItem = this.CreatePageViewItem(pageViewProvider, this.HierarchyRow.Views[index]);
            this.pageViewElement.AddItem(pageViewItem);
            if (pageViewItem.Tag == this.HierarchyRow.ActiveView)
              selectedItem = pageViewItem;
          }
        }
      }
      this.UpdateSelectedPageViewItem(selectedItem);
      RadPageViewOutlookElement pageViewElement = this.pageViewElement as RadPageViewOutlookElement;
      pageViewElement?.HideItems(pageViewElement.Items.Count);
      this.suspendTabChanging = false;
    }

    private void UpdateRowViewLayout()
    {
      IGridRowLayout rowLayout = ((TableViewDefinition) this.HierarchyRow.ActiveView.ViewTemplate.ViewDefinition).CreateRowLayout();
      rowLayout.Initialize(this.ChildTableElement);
      this.ChildTableElement.ViewElement.RowLayout = rowLayout;
      this.ChildTableElement.Update(GridUINotifyAction.Reset);
    }

    private void UpdateTabsPosition()
    {
      if (this.ViewTemplate == null || this.pageViewElement is RadPageViewExplorerBarElement)
        return;
      RadPageViewStripElement pageViewElement1 = this.pageViewElement as RadPageViewStripElement;
      if (pageViewElement1 != null)
      {
        switch (this.ViewTemplate.ChildViewTabsPosition)
        {
          case TabPositions.Left:
            pageViewElement1.StripAlignment = StripViewAlignment.Left;
            break;
          case TabPositions.Right:
            pageViewElement1.StripAlignment = StripViewAlignment.Right;
            break;
          case TabPositions.Top:
            pageViewElement1.StripAlignment = StripViewAlignment.Top;
            break;
          case TabPositions.Bottom:
            pageViewElement1.StripAlignment = StripViewAlignment.Bottom;
            break;
        }
      }
      RadPageViewStackElement pageViewElement2 = this.pageViewElement as RadPageViewStackElement;
      if (pageViewElement2 == null)
        return;
      switch (this.ViewTemplate.ChildViewTabsPosition)
      {
        case TabPositions.Left:
          pageViewElement2.StackPosition = StackViewPosition.Left;
          break;
        case TabPositions.Right:
          pageViewElement2.StackPosition = StackViewPosition.Right;
          break;
        case TabPositions.Top:
          pageViewElement2.StackPosition = StackViewPosition.Top;
          break;
        case TabPositions.Bottom:
          pageViewElement2.StackPosition = StackViewPosition.Bottom;
          break;
      }
    }

    private void UpdateSelectedPageViewItem(RadPageViewItem selectedItem)
    {
      if (!this.HierarchyRow.ActiveView.HasChildRows())
      {
        foreach (GridViewInfo view in (IEnumerable<GridViewInfo>) this.HierarchyRow.Views)
        {
          if (view.HasChildRows())
          {
            using (IEnumerator<RadPageViewItem> enumerator = this.pageViewElement.Items.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                RadPageViewItem current = enumerator.Current;
                if (current.Tag == view)
                {
                  selectedItem = current;
                  break;
                }
              }
              break;
            }
          }
        }
      }
      this.pageViewElement.SelectedItem = selectedItem;
    }

    private void EnsureSelectedTabItem()
    {
      if (this.pageViewElement == null || this.pageViewElement.SelectedItem != null && this.pageViewElement.SelectedItem.Visibility == ElementVisibility.Visible)
        return;
      for (int index = 0; index < this.HierarchyRow.Views.Count; ++index)
      {
        if (this.pageViewElement.Items[index].Visibility == ElementVisibility.Visible)
        {
          this.SetActiveView(this.pageViewElement.Items[index]);
          break;
        }
      }
    }

    protected virtual IRadPageViewProvider CreatePageViewProvider()
    {
      IRadPageViewProvider pageViewProvider = this.TableElement.PageViewProvider;
      if (pageViewProvider == null)
      {
        switch (this.TableElement.PageViewMode)
        {
          case PageViewMode.Strip:
            return (IRadPageViewProvider) new RadPageViewStripProvider();
          case PageViewMode.Stack:
            return (IRadPageViewProvider) new RadPageViewStackProvider();
          case PageViewMode.Outlook:
            return (IRadPageViewProvider) new RadPageViewOutlookProvider();
          case PageViewMode.ExplorerBar:
            return (IRadPageViewProvider) new RadPageViewExplorerBarProvider();
        }
      }
      return pageViewProvider;
    }

    protected virtual GridTableElement CreateChildTableElement()
    {
      this.rowView = (GridTableElement) this.HierarchyRow.ActiveView.ViewTemplate.ViewDefinition.CreateViewUIElement(this.HierarchyRow.ActiveView);
      GridTableElement rowView = this.rowView;
      rowView.ViewElement.StretchVertically = false;
      rowView.StretchVertically = false;
      rowView.StretchHorizontally = true;
      return rowView;
    }

    protected virtual RadPageViewElement CreatePageViewElement(
      IRadPageViewProvider pageViewProvider)
    {
      RadPageViewElement pageViewElement = pageViewProvider.CreatePageViewElement((object) this);
      pageViewElement.StretchVertically = false;
      pageViewElement.ContentArea.StretchVertically = false;
      RadPageViewExplorerBarElement explorerBarElement = pageViewElement as RadPageViewExplorerBarElement;
      if (explorerBarElement != null)
        explorerBarElement.ExpandedChanged += new EventHandler<RadPageViewExpandedChangedEventArgs>(this.explorerBarElement_ExpandedChanged);
      pageViewElement.UpdateSelectedItemContent = false;
      return pageViewElement;
    }

    protected virtual RadPageViewItem CreatePageViewItem(
      IRadPageViewProvider pageViewProvider,
      GridViewInfo viewInfo)
    {
      RadPageViewItem pageViewItem = pageViewProvider.CreatePageViewItem((object) this);
      string str = "table";
      if (!string.IsNullOrEmpty(viewInfo.ViewTemplate.DataMember))
        str = viewInfo.ViewTemplate.DataMember;
      if (!string.IsNullOrEmpty(viewInfo.ViewTemplate.Caption))
        str = viewInfo.ViewTemplate.Caption;
      pageViewItem.Tag = (object) viewInfo;
      pageViewItem.Text = str;
      pageViewItem.RadPropertyChanging += new RadPropertyChangingEventHandler(this.item_RadPropertyChanging);
      pageViewItem.RadPropertyChanged += new RadPropertyChangedEventHandler(this.item_RadPropertyChanged);
      RadPageViewExplorerBarItem viewExplorerBarItem = pageViewItem as RadPageViewExplorerBarItem;
      if (viewExplorerBarItem != null)
      {
        GridTableElement viewUiElement = (GridTableElement) viewInfo.ViewTemplate.ViewDefinition.CreateViewUIElement(viewInfo);
        viewUiElement.ViewElement.StretchVertically = false;
        viewUiElement.StretchVertically = false;
        viewExplorerBarItem.IsExpanded = true;
        viewExplorerBarItem.AssociatedContentAreaElement = new RadPageViewContentAreaElement();
        RadPageViewExplorerBarElement pageViewElement = (RadPageViewExplorerBarElement) this.pageViewElement;
        if (pageViewElement != null && pageViewElement.StackPosition == StackViewPosition.Left)
        {
          viewUiElement.StretchHorizontally = false;
          viewExplorerBarItem.AssociatedContentAreaElement.StretchHorizontally = false;
        }
        else
        {
          viewUiElement.StretchHorizontally = true;
          viewExplorerBarItem.AssociatedContentAreaElement.StretchHorizontally = true;
        }
        this.pageViewElement.Children.Add((RadElement) viewExplorerBarItem.AssociatedContentAreaElement);
        viewExplorerBarItem.AssociatedContentAreaElement.Children.Add((RadElement) viewUiElement);
        if (this.rowView == null)
          this.rowView = viewUiElement;
        viewUiElement.Initialize(this.GridViewElement, viewInfo);
        IGridRowLayout rowLayout = ((TableViewDefinition) viewInfo.ViewTemplate.ViewDefinition).CreateRowLayout();
        rowLayout.Initialize(viewUiElement);
        viewUiElement.ViewElement.RowLayout = rowLayout;
      }
      return pageViewItem;
    }

    protected virtual void InitializePageViewItem(RadPageViewItem item, GridViewInfo viewInfo)
    {
      item.Tag = (object) viewInfo;
      if (this.pageViewElement is RadPageViewOutlookElement)
        return;
      if (viewInfo.ChildRows.Count == 0 && !viewInfo.ViewTemplate.AllowAddNewRow)
      {
        int num1 = (int) item.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      }
      else
      {
        int num2 = (int) item.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      base.OnBoundsChanged(e);
      if (!(this.ChildTableElement.ViewTemplate.ViewDefinition is HtmlViewDefinition) || this.ChildTableElement.ViewTemplate.AutoSizeColumnsMode != GridViewAutoSizeColumnsMode.Fill)
        return;
      foreach (RadElement visualRow in (IEnumerable<GridRowElement>) this.TableElement.VisualRows)
        visualRow.InvalidateMeasure(true);
    }
  }
}
