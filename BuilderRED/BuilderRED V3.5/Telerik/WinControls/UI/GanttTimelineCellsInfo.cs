// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttTimelineCellsInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public struct GanttTimelineCellsInfo
  {
    private int numberOfCells;
    private int startIndex;

    public GanttTimelineCellsInfo(int numberOfCells)
    {
      this = new GanttTimelineCellsInfo(numberOfCells, 0);
    }

    public GanttTimelineCellsInfo(int numberOfCells, int startIndex)
    {
      this.numberOfCells = numberOfCells;
      this.startIndex = startIndex;
    }

    public int NumberOfcells
    {
      get
      {
        return this.numberOfCells;
      }
      set
      {
        this.numberOfCells = value;
      }
    }

    public int StartIndex
    {
      get
      {
        return this.startIndex;
      }
      set
      {
        this.startIndex = value;
      }
    }
  }
}
