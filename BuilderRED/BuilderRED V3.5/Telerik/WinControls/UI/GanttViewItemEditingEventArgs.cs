// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewItemEditingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewItemEditingEventArgs : RadGanttViewCancelEventArgs
  {
    private GanttViewTextViewColumn column;
    private IInputEditor editor;

    public GanttViewItemEditingEventArgs(
      GanttViewDataItem item,
      GanttViewTextViewColumn column,
      IInputEditor editor)
      : base(item)
    {
      this.column = column;
      this.editor = editor;
    }

    public GanttViewTextViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }
  }
}
