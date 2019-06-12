// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridNewRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class GridNewRowElement : GridVirtualizedRowElement
  {
    public override void Initialize(GridViewRowInfo rowInfo)
    {
      base.Initialize(rowInfo);
      this.UpdateContentVisibility(rowInfo.IsCurrent);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "NewRowFill";
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ScrollableColumns.ChildrenChanged += new ChildrenChangedEventHandler(this.Columns_ChildrenChanged);
      this.LeftPinnedColumns.ChildrenChanged += new ChildrenChangedEventHandler(this.Columns_ChildrenChanged);
      this.RightPinnedColumns.ChildrenChanged += new ChildrenChangedEventHandler(this.Columns_ChildrenChanged);
    }

    public override GridCellElement CreateCell(GridViewColumn column)
    {
      GridCellElement cell = base.CreateCell(column);
      if (cell is GridHyperlinkCellElement)
        cell.Class = "NewRowHyperlinkCellElement";
      return cell;
    }

    public override bool CanApplyFormatting
    {
      get
      {
        return false;
      }
    }

    public override void Attach(GridViewRowInfo row, object context)
    {
      base.Attach(row, context);
      this.ViewTemplate.PropertyChanged += new PropertyChangedEventHandler(this.ViewTemplate_PropertyChanged);
    }

    public override void Detach()
    {
      this.ScrollableColumns.ChildrenChanged -= new ChildrenChangedEventHandler(this.Columns_ChildrenChanged);
      this.LeftPinnedColumns.ChildrenChanged -= new ChildrenChangedEventHandler(this.Columns_ChildrenChanged);
      this.RightPinnedColumns.ChildrenChanged -= new ChildrenChangedEventHandler(this.Columns_ChildrenChanged);
      if (this.ViewTemplate != null)
        this.ViewTemplate.PropertyChanged -= new PropertyChangedEventHandler(this.ViewTemplate_PropertyChanged);
      base.Detach();
    }

    public override RadDropDownMenu MergeMenus(
      RadDropDownMenu contextMenu,
      params object[] parameters)
    {
      if (contextMenu == null)
        return (RadDropDownMenu) null;
      int index = contextMenu.Items.Count - 1;
      if (index >= 0)
        contextMenu.Items.RemoveAt(index);
      return contextMenu;
    }

    private void Columns_ChildrenChanged(object sender, ChildrenChangedEventArgs e)
    {
      GridCellElement child = e.Child as GridCellElement;
      if (child == null)
        return;
      if (!this.RowInfo.IsCurrent)
      {
        if (child.ColumnInfo is GridViewIndentColumn || child.ColumnInfo is GridViewRowHeaderColumn)
          return;
        child.Visibility = ElementVisibility.Hidden;
      }
      else
        child.Visibility = ElementVisibility.Visible;
    }

    private void ViewTemplate_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "NewRowText"))
        return;
      this.UpdateContentVisibility(this.RowInfo.IsCurrent);
    }

    protected override void OnRowPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnRowPropertyChanged(e);
      if (!(e.PropertyName == "IsCurrent"))
        return;
      this.UpdateContentVisibility(this.RowInfo.IsCurrent);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      this.UpdateContentVisibility(this.RowInfo.IsCurrent);
      return sizeF;
    }

    public override bool IsCompatible(GridViewRowInfo data, object context)
    {
      return data is GridViewNewRowInfo;
    }

    public virtual void UpdateContentVisibility(bool showCells)
    {
      ElementVisibility elementVisibility = showCells ? ElementVisibility.Visible : ElementVisibility.Hidden;
      foreach (GridCellElement visualCell in this.VisualCells)
      {
        if (!(visualCell.ColumnInfo is GridViewIndentColumn) && !(visualCell.ColumnInfo is GridViewRowHeaderColumn))
          visualCell.Visibility = elementVisibility;
      }
      if (showCells)
        this.Text = string.Empty;
      else if (!string.IsNullOrEmpty(this.ViewTemplate.NewRowText))
        this.Text = this.ViewTemplate.NewRowText;
      else
        this.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("AddNewRowString");
    }
  }
}
