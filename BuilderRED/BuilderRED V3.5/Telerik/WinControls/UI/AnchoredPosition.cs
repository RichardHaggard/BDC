// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AnchoredPosition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class AnchoredPosition
  {
    private GridViewColumn column;
    private GridTraverser.GridTraverserPosition rowPosition;

    public AnchoredPosition(GridViewColumn column, GridTraverser.GridTraverserPosition rowPosition)
    {
      this.column = column;
      this.rowPosition = rowPosition;
    }

    public GridTraverser.GridTraverserPosition RowPosition
    {
      get
      {
        return this.rowPosition;
      }
      set
      {
        this.rowPosition = value;
      }
    }

    public GridViewColumn Column
    {
      get
      {
        return this.column;
      }
      set
      {
        this.column = value;
      }
    }
  }
}
