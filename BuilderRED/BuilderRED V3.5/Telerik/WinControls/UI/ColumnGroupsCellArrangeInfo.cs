// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnGroupsCellArrangeInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ColumnGroupsCellArrangeInfo : CellArrangeInfo
  {
    private GridViewColumnGroup group;
    private GridViewColumnGroupRow row;
    private int rowIndex;
    private int depth;

    public ColumnGroupsCellArrangeInfo(GridViewColumn column)
      : base(column)
    {
    }

    public void Initialize(
      GridViewColumnGroup group,
      GridViewColumnGroupRow row,
      int rowIndex,
      Rectangle bounds)
    {
      this.group = group;
      this.row = row;
      this.rowIndex = rowIndex;
      this.Bounds = (RectangleF) bounds;
    }

    public GridViewColumnGroup Group
    {
      get
      {
        return this.group;
      }
      set
      {
        this.group = value;
      }
    }

    public GridViewColumnGroupRow Row
    {
      get
      {
        return this.row;
      }
      set
      {
        this.row = value;
      }
    }

    public int Depth
    {
      get
      {
        return this.depth;
      }
      set
      {
        this.depth = value;
      }
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
      set
      {
        this.rowIndex = value;
      }
    }
  }
}
