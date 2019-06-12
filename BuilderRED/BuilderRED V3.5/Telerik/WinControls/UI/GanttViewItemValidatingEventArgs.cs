// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewItemValidatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewItemValidatingEventArgs : RadGanttViewCancelEventArgs
  {
    private GanttViewTextViewColumn column;
    private object oldValue;
    private object newValue;

    public GanttViewItemValidatingEventArgs(
      GanttViewDataItem item,
      GanttViewTextViewColumn column,
      object newValue,
      object oldValue)
      : base(item)
    {
      this.column = column;
      this.newValue = newValue;
      this.oldValue = oldValue;
    }

    public GanttViewTextViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public object NewValue
    {
      get
      {
        return this.newValue;
      }
    }

    public object OldValue
    {
      get
      {
        return this.oldValue;
      }
    }
  }
}
