// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewHyperlinkCellInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GridViewHyperlinkCellInfo : GridViewCellInfo
  {
    private bool visited;

    public GridViewHyperlinkCellInfo(
      GridViewRowInfo row,
      GridViewColumn column,
      GridViewCellInfoCollection owner)
      : base(row, column, owner)
    {
      this.visited = false;
    }

    public bool Visited
    {
      get
      {
        return this.visited;
      }
      protected internal set
      {
        this.visited = value;
      }
    }
  }
}
