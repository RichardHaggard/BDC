// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupContentCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridGroupContentCellElement : GridCellElement
  {
    public GridGroupContentCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "GroupContentCell";
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.StretchHorizontally = true;
    }

    protected override bool ShouldUsePaintBuffer()
    {
      return false;
    }

    public override object Value
    {
      get
      {
        GridViewGroupRowInfo rowInfo = (GridViewGroupRowInfo) this.RowInfo;
        if (rowInfo.ViewTemplate.SummaryRowGroupHeaders.Count > 0)
          return (object) (rowInfo.HeaderText + " | " + rowInfo.GetSummary());
        return (object) rowInfo.HeaderText;
      }
      set
      {
      }
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      this.UnbindRowProperties();
      base.Initialize(column, row);
      this.BindRowProperties();
      this.SetContent();
      this.UpdateInfo();
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

    private void BindRowProperties()
    {
      int num = (int) this.BindProperty(GridCellElement.IsCurrentRowProperty, (RadObject) this.RowElement, GridRowElement.IsCurrentProperty, PropertyBindingOptions.OneWay);
      this.RowInfo.PropertyChanged += new PropertyChangedEventHandler(this.RowInfo_PropertyChanged);
    }

    private void UnbindRowProperties()
    {
      int num = (int) this.UnbindProperty(GridCellElement.IsCurrentRowProperty);
      if (this.RowInfo == null)
        return;
      this.RowInfo.PropertyChanged -= new PropertyChangedEventHandler(this.RowInfo_PropertyChanged);
    }

    protected override void DisposeManagedResources()
    {
      this.UnbindRowProperties();
      base.DisposeManagedResources();
    }

    private void RowInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.SetContent();
    }

    protected override List<CharacterRange> GetSearchHighlightRanges()
    {
      List<CharacterRange> characterRangeList = new List<CharacterRange>();
      GridViewGroupRowInfo rowInfo = this.RowInfo as GridViewGroupRowInfo;
      if (rowInfo == null)
        return characterRangeList;
      GridViewColumn groupedColumnInfo = this.GetGroupedColumnInfo(rowInfo);
      if (groupedColumnInfo == null || !this.RowInfo.SearchCache.Contains((object) groupedColumnInfo))
        return characterRangeList;
      string str = this.RowInfo.SearchCache[(object) groupedColumnInfo] as string;
      int First = -1;
      while (First + 1 < this.Text.Length)
      {
        First = !this.MasterTemplate.MasterViewInfo.TableSearchRow.CaseSensitive ? this.MasterTemplate.MasterViewInfo.TableSearchRow.Culture.CompareInfo.IndexOf(this.Text, str, First + 1, this.MasterTemplate.MasterViewInfo.TableSearchRow.CompareOptions) : this.Text.IndexOf(str, First + 1);
        if (First >= 0)
          characterRangeList.Add(new CharacterRange(First, str.Length));
        if (First < 0 || characterRangeList.Count >= 32)
          break;
      }
      return characterRangeList;
    }

    private GridViewColumn GetGroupedColumnInfo(GridViewGroupRowInfo groupRow)
    {
      GridViewColumn gridViewColumn = (GridViewColumn) null;
      if (groupRow.Group.GroupDescriptor == null)
        gridViewColumn = (GridViewColumn) this.ViewTemplate.Columns[this.ViewTemplate.GroupDescriptors[groupRow.GroupLevel].GroupNames[0].PropertyName];
      else if (groupRow.Group.GroupDescriptor.GroupNames.Count > 0)
        gridViewColumn = (GridViewColumn) this.ViewTemplate.Columns[groupRow.Group.GroupDescriptor.GroupNames[0].PropertyName];
      return gridViewColumn;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if ((double) availableSize.Width != double.PositiveInfinity && (double) availableSize.Height != double.PositiveInfinity)
        return availableSize;
      return base.MeasureOverride(availableSize);
    }
  }
}
