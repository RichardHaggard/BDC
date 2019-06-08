// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridNewRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class VirtualGridNewRowElement : VirtualGridRowElement
  {
    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      bool showCells = false;
      if (this.TableElement.GridElement.CurrentCell != null && this.TableElement.GridElement.CurrentCell.RowIndex == this.RowIndex && this.TableElement.GridElement.CurrentCell.ViewInfo == this.ViewInfo)
        showCells = true;
      this.UpdateContentVisibility(showCells);
      return sizeF;
    }

    protected override int MeasureRowHeight(SizeF availableSize)
    {
      return this.TableElement.ViewInfo.NewRowHeight;
    }

    public override void Synchronize()
    {
      base.Synchronize();
      bool showCells = false;
      if (this.TableElement.GridElement.CurrentCell != null && this.TableElement.GridElement.CurrentCell.RowIndex == this.RowIndex && this.TableElement.GridElement.CurrentCell.ViewInfo == this.ViewInfo)
        showCells = true;
      this.UpdateContentVisibility(showCells);
    }

    public virtual void UpdateContentVisibility(bool showCells)
    {
      ElementVisibility elementVisibility = showCells ? ElementVisibility.Visible : ElementVisibility.Hidden;
      foreach (VirtualGridCellElement cellElement in this.GetCellElements())
      {
        if (cellElement.ColumnIndex >= 0)
          cellElement.Visibility = elementVisibility;
      }
      if (showCells)
        this.Text = string.Empty;
      else
        this.Text = LocalizationProvider<RadVirtualGridLocalizationProvider>.CurrentProvider.GetLocalizedString("AddNewRowString");
    }

    public override bool IsCompatible(int data, object context)
    {
      return data == -2;
    }

    public override bool CanApplyAlternatingColor
    {
      get
      {
        return false;
      }
    }
  }
}
