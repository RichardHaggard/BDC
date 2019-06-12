// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SelfReferenceCellLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class SelfReferenceCellLayout : DisposableObject
  {
    private const int MAX_CACHED_LINKS = 15;
    private GridRowElement rowElement;
    private GridTreeExpanderItem expander;
    private StackLayoutElement stackLayoutElement;
    private List<GridLinkItem> hierarchyLinks;
    private List<GridLinkItem> linksCache;

    public SelfReferenceCellLayout(GridRowElement rowElement)
    {
      this.rowElement = rowElement;
      this.hierarchyLinks = new List<GridLinkItem>();
      this.linksCache = new List<GridLinkItem>();
      this.stackLayoutElement = new StackLayoutElement();
      this.stackLayoutElement.StretchVertically = true;
      this.stackLayoutElement.StretchHorizontally = true;
      int num = (int) this.stackLayoutElement.SetDefaultValueOverride(RadElement.FitToSizeModeProperty, (object) RadFitToSizeMode.FitToParentBounds);
      this.expander = new GridTreeExpanderItem(rowElement.TableElement);
      this.expander.ThemeRole = "SelfReferencingExpander";
      this.expander.StretchVertically = true;
      this.stackLayoutElement.Children.Add((RadElement) this.expander);
      this.BindRowProperties();
    }

    public virtual void CreateCellElements(GridDataCellElement dataCell)
    {
      dataCell.SuspendLayout();
      if (this.DataCell != dataCell)
      {
        if (this.DataCell != null)
          this.DataCell.Children.Remove((RadElement) this.stackLayoutElement);
        dataCell.Children.Insert(0, (RadElement) this.stackLayoutElement);
      }
      bool isLastChildRow = this.IsLastChildRow(this.RowInfo.Parent, (GridViewRowInfo) this.RowInfo);
      this.UpdateExpander(isLastChildRow);
      this.UpdateLinks(isLastChildRow);
      dataCell.ResumeLayout(true);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.DisposeLinks();
      this.UnbindRowProperties();
      if (this.DataCell == null || this.stackLayoutElement.IsDisposed)
        return;
      this.DataCell.Children.Remove((RadElement) this.stackLayoutElement);
      this.stackLayoutElement.Dispose();
      this.stackLayoutElement = (StackLayoutElement) null;
      this.expander = (GridTreeExpanderItem) null;
    }

    public virtual void DetachCellElements()
    {
      if (this.stackLayoutElement == null || this.stackLayoutElement.IsDisposed || this.DataCell == null)
        return;
      this.DataCell.Children.Remove((RadElement) this.stackLayoutElement);
    }

    protected void DisposeLinks()
    {
      for (int index = this.hierarchyLinks.Count - 1; index >= 0; --index)
      {
        GridLinkItem hierarchyLink = this.hierarchyLinks[index];
        if (!hierarchyLink.IsDisposed)
        {
          this.stackLayoutElement.Children.Remove((RadElement) hierarchyLink);
          this.hierarchyLinks.Remove(hierarchyLink);
          hierarchyLink.Dispose();
        }
      }
      for (int index = this.linksCache.Count - 1; index >= 0; --index)
      {
        GridLinkItem gridLinkItem = this.linksCache[index];
        if (!gridLinkItem.IsDisposed)
        {
          this.linksCache.Remove(gridLinkItem);
          gridLinkItem.Dispose();
        }
      }
    }

    protected GridViewHierarchyRowInfo RowInfo
    {
      get
      {
        return this.rowElement.RowInfo as GridViewHierarchyRowInfo;
      }
    }

    protected GridRowElement RowElement
    {
      get
      {
        return this.rowElement;
      }
    }

    public GridExpanderItem ExpanderItem
    {
      get
      {
        return (GridExpanderItem) this.expander;
      }
    }

    public StackLayoutElement StackLayoutElement
    {
      get
      {
        return this.stackLayoutElement;
      }
    }

    protected GridDataCellElement DataCell
    {
      get
      {
        if (this.stackLayoutElement == null)
          return (GridDataCellElement) null;
        return this.stackLayoutElement.Parent as GridDataCellElement;
      }
    }

    protected int LinkIndent
    {
      get
      {
        return this.rowElement.TableElement.TreeLevelIndent;
      }
    }

    protected int LinkCount
    {
      get
      {
        return this.hierarchyLinks.Count;
      }
    }

    protected List<GridLinkItem> Links
    {
      get
      {
        return this.hierarchyLinks;
      }
    }

    protected virtual void UpdateExpander(bool isLastChildRow)
    {
      int count = this.RowInfo.ChildRows.Count;
      this.expander.LinkOrientation = Telerik.WinControls.UI.ExpanderItem.LinkLineOrientation.None;
      this.expander.Expanded = this.RowInfo.IsExpanded;
      if (this.RowInfo.IsExpanded && this.RowElement.TableElement.ShowSelfReferenceLines)
        this.expander.LinkOrientation = Telerik.WinControls.UI.ExpanderItem.LinkLineOrientation.Bottom;
      bool flag = this.RowInfo.ChildRows.Count == 0 && this.RowInfo.ViewTemplate.Templates.Count > 0;
      if (count == 0 && !flag)
        this.expander.Visibility = ElementVisibility.Hidden;
      else
        this.expander.Visibility = ElementVisibility.Visible;
    }

    protected virtual void UpdateLinks(bool isLastChildRow)
    {
      this.UpdateLinksCore();
      if (this.LinkCount <= 0)
        return;
      this.UpdateLinkTypes(isLastChildRow);
    }

    protected virtual void UpdateLinksCore()
    {
      int num1 = this.RowInfo.Level - this.LinkCount - 1;
      if (this.RowInfo.Group != null)
        num1 -= this.RowInfo.Group.Level + 1;
      int num2 = Math.Abs(num1);
      bool flag = num1 > 0;
      for (; num2 > 0; --num2)
      {
        if (flag)
        {
          GridLinkItem linkItem = this.GetLinkItem();
          linkItem.ThemeRole = "SelfReferencingLink";
          linkItem.Visibility = ElementVisibility.Hidden;
          linkItem.StretchVertically = true;
          int num3 = (int) linkItem.SetDefaultValueOverride(RadElement.FitToSizeModeProperty, (object) RadFitToSizeMode.FitToParentBounds);
          this.stackLayoutElement.Children.Insert(0, (RadElement) linkItem);
          this.hierarchyLinks.Insert(0, linkItem);
        }
        else
        {
          GridLinkItem hierarchyLink = this.hierarchyLinks[0];
          this.stackLayoutElement.Children.Remove((RadElement) hierarchyLink);
          this.hierarchyLinks.Remove(hierarchyLink);
          this.CacheLinkItem(hierarchyLink);
        }
      }
    }

    protected virtual void UpdateLinkTypes(bool isLastChildRow)
    {
      ElementVisibility elementVisibility = ElementVisibility.Hidden;
      if (this.rowElement.TableElement.ShowSelfReferenceLines)
        elementVisibility = ElementVisibility.Visible;
      int index1 = this.LinkCount - 1;
      GridLinkItem hierarchyLink1 = this.hierarchyLinks[index1];
      hierarchyLink1.Visibility = elementVisibility;
      hierarchyLink1.Type = !isLastChildRow ? GridLinkItem.LinkType.TShape : GridLinkItem.LinkType.RightBottomAngleShape;
      GridViewHierarchyRowInfo parent = this.RowInfo.Parent as GridViewHierarchyRowInfo;
      for (int index2 = index1 - 1; index2 >= 0; --index2)
      {
        GridLinkItem hierarchyLink2 = this.hierarchyLinks[index2];
        hierarchyLink2.Visibility = elementVisibility;
        hierarchyLink2.Type = GridLinkItem.LinkType.VerticalLine;
        if (parent != null)
        {
          if (this.IsLastChildRow(parent.Parent, (GridViewRowInfo) parent))
            hierarchyLink2.Visibility = ElementVisibility.Hidden;
          parent = parent.Parent as GridViewHierarchyRowInfo;
        }
        else
          hierarchyLink2.Visibility = ElementVisibility.Hidden;
      }
    }

    protected virtual bool IsLastChildRow(IHierarchicalRow parent, GridViewRowInfo row)
    {
      ITraverser<GridViewRowInfo> traverser = this.GetTraverser(parent);
      traverser.MoveToEnd();
      return traverser.Current == row;
    }

    protected virtual bool IsFirstChildRow(IHierarchicalRow parent, GridViewRowInfo row)
    {
      ITraverser<GridViewRowInfo> traverser = this.GetTraverser(parent);
      traverser.Reset();
      traverser.MoveNext();
      return traverser.Current == row;
    }

    protected ITraverser<GridViewRowInfo> GetTraverser(
      IHierarchicalRow hierarchicalRow)
    {
      GridViewRowInfo hierarchyRow = hierarchicalRow as GridViewRowInfo;
      if (hierarchyRow != null)
        return (ITraverser<GridViewRowInfo>) new HierarchyRowTraverser(hierarchyRow);
      return (ITraverser<GridViewRowInfo>) new GridTraverser(hierarchicalRow);
    }

    protected void CacheLinkItem(GridLinkItem item)
    {
      if (this.linksCache.Count >= 15)
        return;
      this.linksCache.Add(item);
    }

    protected GridLinkItem GetLinkItem()
    {
      GridLinkItem gridLinkItem;
      if (this.linksCache.Count > 0)
      {
        gridLinkItem = this.linksCache[this.linksCache.Count - 1];
        this.linksCache.RemoveAt(this.linksCache.Count - 1);
      }
      else
        gridLinkItem = new GridLinkItem(this.rowElement.TableElement);
      return gridLinkItem;
    }

    public virtual void BindRowProperties()
    {
      PropertyBindingOptions options = PropertyBindingOptions.TwoWay | PropertyBindingOptions.PreserveAsLocalValue;
      int num = (int) this.expander.BindProperty(Telerik.WinControls.UI.ExpanderItem.ExpandedProperty, (RadObject) this.RowElement, GridDataRowElement.IsExpandedProperty, options);
    }

    public virtual void UnbindRowProperties()
    {
      int num = (int) this.expander.UnbindProperty(Telerik.WinControls.UI.ExpanderItem.ExpandedProperty);
    }
  }
}
