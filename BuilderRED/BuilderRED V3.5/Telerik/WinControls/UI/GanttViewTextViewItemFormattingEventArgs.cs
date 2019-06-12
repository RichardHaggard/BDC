// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewItemFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewItemFormattingEventArgs : RadGanttViewEventArgs
  {
    private GanttViewTextItemElement itemElement;

    public GanttViewTextViewItemFormattingEventArgs(
      GanttViewDataItem item,
      GanttViewTextItemElement itemElement)
      : base(item)
    {
      this.itemElement = itemElement;
    }

    public GanttViewTextItemElement ItemElement
    {
      get
      {
        return this.itemElement;
      }
    }
  }
}
