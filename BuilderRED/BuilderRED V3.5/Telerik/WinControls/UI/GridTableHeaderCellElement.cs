// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridTableHeaderCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms.VisualStyles;

namespace Telerik.WinControls.UI
{
  public class GridTableHeaderCellElement : GridRowHeaderCellElement
  {
    public GridTableHeaderCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "TableHeaderCell";
    }

    public override VisualStyleElement GetXPVisualStyle()
    {
      return VistaAeroTheme.Header.Item.Normal;
    }

    public override VisualStyleElement GetVistaVisualStyle()
    {
      return VistaAeroTheme.Header.Item.Normal;
    }
  }
}
