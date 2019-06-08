// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewCellFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewCellFormattingEventArgs : RadGanttViewEventArgs
  {
    private GanttViewTextViewCellElement cellElement;
    private GanttViewTextViewColumn column;

    public GanttViewTextViewCellFormattingEventArgs(
      GanttViewDataItem item,
      GanttViewTextViewCellElement cellElement,
      GanttViewTextViewColumn column)
      : base(item)
    {
      this.cellElement = cellElement;
      this.column = column;
    }

    public GanttViewTextViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public GanttViewTextViewCellElement CellElement
    {
      get
      {
        return this.cellElement;
      }
    }
  }
}
