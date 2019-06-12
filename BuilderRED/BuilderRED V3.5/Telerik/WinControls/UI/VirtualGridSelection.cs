// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridSelection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class VirtualGridSelection
  {
    private SelectionRegion boundingRegion = SelectionRegion.Empty;
    private bool multiselect;
    private VirtualGridSelectionMode selectionMode;
    private List<SelectionRegion> regions;
    private int startRow;
    private int startColumn;
    private int endRow;
    private int endColumn;
    private VirtualGridViewInfo currentViewInfo;

    public VirtualGridSelection()
    {
      this.regions = new List<SelectionRegion>();
    }

    [DefaultValue(false)]
    public bool Multiselect
    {
      get
      {
        return this.multiselect;
      }
      set
      {
        this.multiselect = value;
      }
    }

    [DefaultValue(VirtualGridSelectionMode.CellSelect)]
    public VirtualGridSelectionMode SelectionMode
    {
      get
      {
        return this.selectionMode;
      }
      set
      {
        this.selectionMode = value;
      }
    }

    public SelectionRegion SelectedRegion
    {
      get
      {
        return new SelectionRegion(Math.Min(this.startRow, this.endRow), Math.Min(this.startColumn, this.endColumn), Math.Max(this.startRow, this.endRow), Math.Max(this.startColumn, this.endColumn), this.currentViewInfo);
      }
    }

    public IEnumerable<SelectionRegion> SelectedRegions
    {
      get
      {
        foreach (SelectionRegion region in this.regions)
          yield return region;
        yield return this.SelectedRegion;
      }
    }

    public VirtualGridViewInfo CurrentViewInfo
    {
      get
      {
        return this.currentViewInfo;
      }
      internal set
      {
        this.currentViewInfo = value;
      }
    }

    public bool HasSelection
    {
      get
      {
        if (this.CurrentViewInfo != null)
          return this.SelectedRegion != SelectionRegion.Empty;
        return false;
      }
    }

    public int CurrentRowIndex
    {
      get
      {
        return this.startRow;
      }
    }

    public int CurrentColumnIndex
    {
      get
      {
        return this.startColumn;
      }
    }

    public int MinRowIndex
    {
      get
      {
        return this.boundingRegion.Top;
      }
    }

    public int MaxRowIndex
    {
      get
      {
        return this.boundingRegion.Bottom;
      }
    }

    public int MinColumnIndex
    {
      get
      {
        return this.boundingRegion.Left;
      }
    }

    public int MaxColumnIndex
    {
      get
      {
        return this.boundingRegion.Right;
      }
    }

    public void BeginSelection(
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo,
      bool keepSelection)
    {
      keepSelection &= this.currentViewInfo == viewInfo;
      VirtualGridSelectionChangingEventArgs args = new VirtualGridSelectionChangingEventArgs(keepSelection ? VirtualGridSelectionAction.BeginSelection : VirtualGridSelectionAction.ClearAndBeginSelection, rowIndex, columnIndex, viewInfo);
      this.OnSelectionChanging(args);
      if (args.Cancel)
        return;
      if (!keepSelection)
      {
        this.ClearSelectedRegions();
        this.boundingRegion = SelectionRegion.Empty;
      }
      else
        this.AddSelectedRegions(this.SelectedRegion);
      this.currentViewInfo = viewInfo;
      this.startRow = this.endRow = rowIndex;
      if (this.SelectionMode == VirtualGridSelectionMode.CellSelect)
        this.startColumn = this.endColumn = columnIndex;
      else if (this.SelectionMode == VirtualGridSelectionMode.FullRowSelect)
      {
        this.startColumn = 0;
        this.endColumn = this.CurrentViewInfo.ColumnCount - 1;
      }
      this.OnSelectionChanged();
    }

    public void AddSelectedRegions(SelectionRegion selectionRegion)
    {
      this.regions.Add(selectionRegion);
    }

    public void ClearSelectedRegions()
    {
      this.regions.Clear();
    }

    public void ExtendCurrentRegion(int rowIndex, int columnIndex)
    {
      if ((this.SelectionMode != VirtualGridSelectionMode.FullRowSelect || this.endRow == rowIndex) && (this.SelectionMode != VirtualGridSelectionMode.CellSelect || this.endColumn == columnIndex && this.endRow == rowIndex))
        return;
      VirtualGridSelectionChangingEventArgs args = new VirtualGridSelectionChangingEventArgs(VirtualGridSelectionAction.ExtendSelection, rowIndex, columnIndex, this.CurrentViewInfo);
      this.OnSelectionChanging(args);
      if (args.Cancel)
        return;
      this.endRow = rowIndex;
      if (this.SelectionMode == VirtualGridSelectionMode.CellSelect)
        this.endColumn = columnIndex;
      else if (this.SelectionMode == VirtualGridSelectionMode.FullRowSelect)
        this.endColumn = this.CurrentViewInfo.ColumnCount - 1;
      this.OnSelectionChanged();
    }

    public bool IsSelected(int rowIndex, int columnIndex, VirtualGridViewInfo viewInfo)
    {
      foreach (SelectionRegion selectedRegion in this.SelectedRegions)
      {
        if (selectedRegion.ViewInfo == viewInfo && selectedRegion.Contains(rowIndex, columnIndex))
          return true;
      }
      if (this.CurrentViewInfo == viewInfo)
        return this.SelectedRegion.Contains(rowIndex, columnIndex);
      return false;
    }

    public bool RowContainsSelection(int rowIndex, VirtualGridViewInfo viewInfo)
    {
      foreach (SelectionRegion selectedRegion in this.SelectedRegions)
      {
        if (selectedRegion.ViewInfo == viewInfo && selectedRegion.ContainsRow(rowIndex))
          return true;
      }
      if (this.CurrentViewInfo == viewInfo)
        return this.SelectedRegion.ContainsRow(rowIndex);
      return false;
    }

    public bool ColumnContainsSelection(int columnIndex, VirtualGridViewInfo viewInfo)
    {
      foreach (SelectionRegion selectedRegion in this.SelectedRegions)
      {
        if (selectedRegion.ViewInfo == viewInfo && selectedRegion.ContainsColumn(columnIndex))
          return true;
      }
      if (this.CurrentViewInfo == viewInfo)
        return this.SelectedRegion.ContainsColumn(columnIndex);
      return false;
    }

    public void ClearSelection()
    {
      VirtualGridSelectionChangingEventArgs args = new VirtualGridSelectionChangingEventArgs(VirtualGridSelectionAction.ClearSelection, 0, 0, this.CurrentViewInfo);
      this.OnSelectionChanging(args);
      if (args.Cancel)
        return;
      this.ClearSelectedRegions();
      this.startRow = this.endRow = this.startColumn = this.endColumn = -1;
      this.boundingRegion = SelectionRegion.Empty;
      this.OnSelectionChanged();
    }

    public void SelectAll()
    {
      if (!this.Multiselect)
        return;
      VirtualGridSelectionChangingEventArgs args = new VirtualGridSelectionChangingEventArgs(VirtualGridSelectionAction.SelectAll, 0, 0, this.CurrentViewInfo);
      this.OnSelectionChanging(args);
      if (args.Cancel)
        return;
      this.ClearSelectedRegions();
      this.startRow = this.startColumn = 0;
      this.endRow = this.CurrentViewInfo.RowCount - 1;
      this.endColumn = this.CurrentViewInfo.ColumnCount - 1;
      this.OnSelectionChanged();
    }

    public event EventHandler SelectionChanged;

    public event VirtualGridSelectionChangingEventHandler SelectionChanging;

    protected virtual void OnSelectionChanged()
    {
      SelectionRegion selectedRegion = this.SelectedRegion;
      this.boundingRegion = !this.HasSelection ? SelectionRegion.Empty : (!(this.boundingRegion != SelectionRegion.Empty) ? this.SelectedRegion : new SelectionRegion(Math.Min(this.boundingRegion.Top, selectedRegion.Top), Math.Min(this.boundingRegion.Left, selectedRegion.Left), Math.Max(this.boundingRegion.Bottom, selectedRegion.Bottom), Math.Max(this.boundingRegion.Right, selectedRegion.Right), selectedRegion.ViewInfo));
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, EventArgs.Empty);
    }

    protected virtual void OnSelectionChanging(VirtualGridSelectionChangingEventArgs args)
    {
      if (this.SelectionChanging == null)
        return;
      this.SelectionChanging((object) this, args);
    }
  }
}
